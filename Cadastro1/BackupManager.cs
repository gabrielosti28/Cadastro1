using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Cadastro1
{
    /// <summary>
    /// Gerencia backups automáticos e manuais do banco de dados
    /// OTIMIZADO: Consolidação de lógica de diretório
    /// </summary>
    public class BackupManager
    {
        private static BackupManager _instance;
        private System.Threading.Timer _backupTimer;
        private string _backupDirectory;
        private readonly int _backupIntervalHours = 12;
        private readonly int _maxBackupsToKeep = 15;
        private const string CONFIG_KEY = "BackupDirectory";

        public static BackupManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BackupManager();
                return _instance;
            }
        }

        private BackupManager()
        {
            _backupDirectory = ObterOuCriarDiretorioBackup();
        }

        /// <summary>
        /// SIMPLIFICAÇÃO: Consolidação de 3 métodos em 1
        /// - CarregarDiretorioSalvo()
        /// - CriarDiretorioBackup()
        /// - Validações duplicadas
        /// </summary>
        private string ObterOuCriarDiretorioBackup()
        {
            // 1. Tentar carregar do registro
            string diretorioSalvo = TentarCarregarDoRegistro();
            if (ValidarDiretorio(diretorioSalvo))
                return diretorioSalvo;

            // 2. Tentar diretório padrão D:\SQLBckp
            string diretorioPadrao = @"D:\SQLBckp";
            if (CriarDiretorioSeNecessario(diretorioPadrao))
                return diretorioPadrao;

            // 3. Fallback: Documentos do usuário
            string diretorioFallback = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "SistemaCadastroClientes",
                "Backups"
            );

            if (CriarDiretorioSeNecessario(diretorioFallback))
                return diretorioFallback;

            // 4. Último recurso: Temp
            string diretorioTemp = Path.Combine(Path.GetTempPath(), "SistemaCadastro", "Backups");
            CriarDiretorioSeNecessario(diretorioTemp);

            LogBackup($"AVISO: Usando diretório temporário: {diretorioTemp}");
            return diretorioTemp;
        }

        private string TentarCarregarDoRegistro()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\SistemaCadastro"))
                {
                    return key?.GetValue(CONFIG_KEY) as string;
                }
            }
            catch
            {
                return null;
            }
        }

        private bool ValidarDiretorio(string diretorio)
        {
            return !string.IsNullOrEmpty(diretorio) && Directory.Exists(Path.GetDirectoryName(diretorio));
        }

        private bool CriarDiretorioSeNecessario(string diretorio)
        {
            try
            {
                if (!Directory.Exists(diretorio))
                {
                    Directory.CreateDirectory(diretorio);
                    LogBackup($"Diretório criado: {diretorio}");
                }
                return true;
            }
            catch (Exception ex)
            {
                LogBackup($"Não foi possível criar {diretorio}: {ex.Message}");
                return false;
            }
        }

        private void SalvarDiretorio(string diretorio)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\SistemaCadastro"))
                {
                    key?.SetValue(CONFIG_KEY, diretorio);
                }
            }
            catch (Exception ex)
            {
                LogBackup($"Erro ao salvar diretório: {ex.Message}");
            }
        }

        public bool EscolherDiretorioBackup()
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Escolha a pasta onde os backups serão salvos:";
                dialog.SelectedPath = _backupDirectory;
                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string novaPasta = Path.Combine(dialog.SelectedPath, "Backups");

                    if (CriarDiretorioSeNecessario(novaPasta) && TestarPermissaoEscrita(novaPasta))
                    {
                        _backupDirectory = novaPasta;
                        SalvarDiretorio(_backupDirectory);

                        MessageBox.Show(
                            $"✓ Pasta de backup configurada com sucesso!\n\n{_backupDirectory}",
                            "Sucesso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        return true;
                    }
                }
            }
            return false;
        }

        private bool TestarPermissaoEscrita(string diretorio)
        {
            try
            {
                string testFile = Path.Combine(diretorio, "test.tmp");
                File.WriteAllText(testFile, "test");
                File.Delete(testFile);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"✖ Erro ao configurar pasta:\n\n{ex.Message}\n\n" +
                    "Verifique se você tem permissão de escrita nesta pasta.",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }
        }

        public void IniciarBackupAutomatico()
        {
            try
            {
                if (!ExisteBackupRecente())
                {
                    LogBackup("Sistema iniciado. Próximo backup em 24 horas.");
                }

                TimeSpan intervalo = TimeSpan.FromHours(_backupIntervalHours);

                _backupTimer = new System.Threading.Timer(
                    callback: BackupTimerCallback,
                    state: null,
                    dueTime: intervalo,
                    period: intervalo
                );

                LogBackup("Sistema de backup automático iniciado.");
            }
            catch (Exception ex)
            {
                LogBackup($"ERRO ao iniciar backup automático: {ex.Message}");
            }
        }

        public void PararBackupAutomatico()
        {
            if (_backupTimer != null)
            {
                _backupTimer.Dispose();
                _backupTimer = null;
                LogBackup("Sistema de backup automático parado.");
            }
        }

        private void BackupTimerCallback(object state)
        {
            try
            {
                Task.Run(() => RealizarBackup(true));
            }
            catch (Exception ex)
            {
                LogBackup($"ERRO no callback do timer: {ex.Message}");
            }
        }

        public string RealizarBackup(bool automatico = false)
        {
            string caminhoCompleto = "";

            try
            {
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string tipo = automatico ? "AUTO" : "MANUAL";
                string nomeArquivo = $"Backup_{tipo}_{timestamp}.bak";

                caminhoCompleto = Path.Combine(_backupDirectory, nomeArquivo);

                LogBackup($"Iniciando backup {tipo} em: {caminhoCompleto}");

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    string sqlBackup = $@"
                        BACKUP DATABASE [projeto1]
                        TO DISK = @BackupPath
                        WITH FORMAT,
                             NAME = 'Backup {tipo} - {DateTime.Now:yyyy-MM-dd HH:mm:ss}',
                             SKIP,
                             NOREWIND,
                             NOUNLOAD,
                             COMPRESSION,
                             STATS = 10";

                    using (SqlCommand cmd = new SqlCommand(sqlBackup, conn))
                    {
                        cmd.CommandTimeout = 600;
                        cmd.Parameters.AddWithValue("@BackupPath", caminhoCompleto);
                        cmd.ExecuteNonQuery();
                    }
                }

                SalvarInfoBackup(caminhoCompleto, tipo);
                LimparBackupsAntigos();

                LogBackup($"✓ Backup {tipo} concluído: {caminhoCompleto}");
                return caminhoCompleto;
            }
            catch (Exception ex)
            {
                LogBackup($"ERRO: {ex.Message}");

                string mensagemErro = $"Falha ao criar backup:\n\n{ex.Message}\n\n";

                if (ex.Message.Contains("permission") || ex.Message.Contains("denied") ||
                    ex.Message.Contains("acesso negado") || ex.Message.Contains("Operating system error 5"))
                {
                    mensagemErro += "❌ ERRO DE PERMISSÃO!\n\n" +
                                   "O SQL Server não tem permissão para criar arquivos nesta pasta.\n\n" +
                                   "SOLUÇÕES:\n" +
                                   "1️⃣ Clique em 'Configurar Pasta' e escolha outra pasta\n" +
                                   $"2️⃣ Dê permissão para a conta do SQL Server na pasta:\n   {_backupDirectory}";
                }

                throw new Exception(mensagemErro);
            }
        }

        public bool RestaurarBackup(string caminhoBackup)
        {
            try
            {
                if (!File.Exists(caminhoBackup))
                {
                    throw new Exception("Arquivo de backup não encontrado!");
                }

                LogBackup($"Iniciando restauração: {Path.GetFileName(caminhoBackup)}");

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Matar conexões ativas
                    ExecutarSQL(conn, @"
                        USE master;
                        DECLARE @SQL VARCHAR(MAX) = '';
                        SELECT @SQL = @SQL + 'KILL ' + CAST(session_id AS VARCHAR) + ';'
                        FROM sys.dm_exec_sessions
                        WHERE database_id = DB_ID('projeto1')
                        AND session_id <> @@SPID;
                        EXEC(@SQL);");

                    // Single user
                    ExecutarSQL(conn, "ALTER DATABASE [projeto1] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;");

                    // Restaurar
                    using (SqlCommand cmd = new SqlCommand(@"
                        RESTORE DATABASE [projeto1]
                        FROM DISK = @BackupPath
                        WITH REPLACE, RECOVERY, STATS = 10;", conn))
                    {
                        cmd.CommandTimeout = 600;
                        cmd.Parameters.AddWithValue("@BackupPath", caminhoBackup);
                        cmd.ExecuteNonQuery();
                    }

                    // Multi user
                    ExecutarSQL(conn, "ALTER DATABASE [projeto1] SET MULTI_USER;");
                }

                LogBackup("Backup restaurado com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                LogBackup($"ERRO ao restaurar: {ex.Message}");
                throw new Exception($"Falha ao restaurar backup:\n\n{ex.Message}");
            }
        }

        private void ExecutarSQL(SqlConnection conn, string sql)
        {
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public BackupInfo[] ListarBackups()
        {
            try
            {
                if (!Directory.Exists(_backupDirectory))
                {
                    LogBackup($"Diretório não existe: {_backupDirectory}");
                    return new BackupInfo[0];
                }

                string[] arquivos = Directory.GetFiles(_backupDirectory, "*.bak");
                BackupInfo[] backups = new BackupInfo[arquivos.Length];

                for (int i = 0; i < arquivos.Length; i++)
                {
                    FileInfo info = new FileInfo(arquivos[i]);
                    backups[i] = new BackupInfo
                    {
                        CaminhoCompleto = arquivos[i],
                        NomeArquivo = info.Name,
                        DataCriacao = info.CreationTime,
                        TamanhoBytes = info.Length,
                        Tipo = arquivos[i].Contains("AUTO") ? "Automático" : "Manual"
                    };
                }

                Array.Sort(backups, (a, b) => b.DataCriacao.CompareTo(a.DataCriacao));
                return backups;
            }
            catch (Exception ex)
            {
                LogBackup($"ERRO ao listar backups: {ex.Message}");
                return new BackupInfo[0];
            }
        }

        private bool ExisteBackupRecente()
        {
            try
            {
                BackupInfo[] backups = ListarBackups();
                if (backups.Length == 0) return false;
                TimeSpan diferenca = DateTime.Now - backups[0].DataCriacao;
                return diferenca.TotalHours < _backupIntervalHours;
            }
            catch { return false; }
        }

        private void LimparBackupsAntigos()
        {
            try
            {
                BackupInfo[] backups = ListarBackups();
                if (backups.Length > _maxBackupsToKeep)
                {
                    for (int i = _maxBackupsToKeep; i < backups.Length; i++)
                    {
                        try
                        {
                            File.Delete(backups[i].CaminhoCompleto);
                            LogBackup($"Backup antigo removido: {backups[i].NomeArquivo}");
                        }
                        catch (Exception ex)
                        {
                            LogBackup($"Erro ao excluir {backups[i].NomeArquivo}: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogBackup($"ERRO ao limpar backups: {ex.Message}");
            }
        }

        private void SalvarInfoBackup(string caminhoBackup, string tipo)
        {
            try
            {
                FileInfo info = new FileInfo(caminhoBackup);
                string infoFile = Path.Combine(_backupDirectory, "backup_history.txt");
                string linha = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}|{tipo}|{info.Name}|{info.Length} bytes";
                File.AppendAllText(infoFile, linha + Environment.NewLine);
            }
            catch { }
        }

        /// <summary>
        /// OTIMIZAÇÃO: Limite de 1000 linhas no log para evitar arquivo gigante
        /// </summary>
        private void LogBackup(string mensagem)
        {
            try
            {
                string logFile = Path.Combine(_backupDirectory, "backup_log.txt");
                string linha = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {mensagem}";

                // Adicionar linha
                File.AppendAllText(logFile, linha + Environment.NewLine);

                // Limitar tamanho do log (manter últimas 1000 linhas)
                LimitarTamanhoLog(logFile);

                System.Diagnostics.Debug.WriteLine(linha);
            }
            catch { }
        }

        private void LimitarTamanhoLog(string logFile)
        {
            try
            {
                if (File.Exists(logFile))
                {
                    var linhas = File.ReadAllLines(logFile);
                    if (linhas.Length > 1000)
                    {
                        var ultimasLinhas = new string[1000];
                        Array.Copy(linhas, linhas.Length - 1000, ultimasLinhas, 0, 1000);
                        File.WriteAllLines(logFile, ultimasLinhas);
                    }
                }
            }
            catch { }
        }

        public bool VerificarIntegridadeBackup(string caminhoBackup)
        {
            try
            {
                if (!File.Exists(caminhoBackup)) return false;

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string sql = "RESTORE VERIFYONLY FROM DISK = @BackupPath";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@BackupPath", caminhoBackup);
                        cmd.ExecuteNonQuery();
                    }
                }

                LogBackup($"Integridade OK: {Path.GetFileName(caminhoBackup)}");
                return true;
            }
            catch (Exception ex)
            {
                LogBackup($"Integridade FALHOU: {ex.Message}");
                return false;
            }
        }

        public string ObterDiretorioBackup()
        {
            return _backupDirectory;
        }
    }

    public class BackupInfo
    {
        public string CaminhoCompleto { get; set; }
        public string NomeArquivo { get; set; }
        public DateTime DataCriacao { get; set; }
        public long TamanhoBytes { get; set; }
        public string Tipo { get; set; }

        public string TamanhoFormatado
        {
            get
            {
                string[] tamanhos = { "B", "KB", "MB", "GB" };
                double tam = TamanhoBytes;
                int ordem = 0;
                while (tam >= 1024 && ordem < tamanhos.Length - 1)
                {
                    ordem++;
                    tam = tam / 1024;
                }
                return string.Format("{0:0.##} {1}", tam, tamanhos[ordem]);
            }
        }
    }
}