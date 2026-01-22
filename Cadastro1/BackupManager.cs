// =============================================
// GERENCIADOR DE BACKUP - VERSÃO CORRIGIDA
// Arquivo: BackupManager.cs
// =============================================
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
    /// </summary>
    public class BackupManager
    {
        private static BackupManager _instance;
        private System.Threading.Timer _backupTimer; // ← ESPECIFICAR O NAMESPACE COMPLETO
        private string _backupDirectory;
        private readonly int _backupIntervalHours = 24;
        private readonly int _maxBackupsToKeep = 30;
        private const string CONFIG_KEY = "BackupDirectory";

        // Singleton
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
            // Carregar diretório salvo ou usar padrão
            _backupDirectory = CarregarDiretorioSalvo();

            // Criar diretório se não existir
            CriarDiretorioBackup();
        }

        /// <summary>
        /// Carrega o diretório de backup salvo no registro do Windows
        /// </summary>
        private string CarregarDiretorioSalvo()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\SistemaCadastro"))
                {
                    string saved = key.GetValue(CONFIG_KEY) as string;
                    if (!string.IsNullOrEmpty(saved) && Directory.Exists(Path.GetDirectoryName(saved)))
                    {
                        return saved;
                    }
                }
            }
            catch { }

            // Diretório padrão: D:\SQLBckp
            // Se D: não existir, usa Documentos como fallback
            string diretorioPadrao = @"D:\SQLBckp";

            if (Directory.Exists("D:\\"))
            {
                return diretorioPadrao;
            }
            else
            {
                // Fallback se disco D: não existir
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "SistemaCadastroClientes",
                    "Backups"
                );
            }
        }

        /// <summary>
        /// Salva o diretório de backup no registro do Windows
        /// </summary>
        private void SalvarDiretorio(string diretorio)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\SistemaCadastro"))
                {
                    key.SetValue(CONFIG_KEY, diretorio);
                }
            }
            catch (Exception ex)
            {
                LogBackup($"Erro ao salvar diretório: {ex.Message}");
            }
        }

        /// <summary>
        /// Permite o usuário escolher a pasta de destino dos backups
        /// </summary>
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

                    try
                    {
                        // Criar a pasta se não existir
                        if (!Directory.Exists(novaPasta))
                        {
                            Directory.CreateDirectory(novaPasta);
                        }

                        // Verificar se temos permissão de escrita
                        string testFile = Path.Combine(novaPasta, "test.tmp");
                        File.WriteAllText(testFile, "test");
                        File.Delete(testFile);

                        // Atualizar e salvar
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
            }
            return false;
        }

        /// <summary>
        /// Cria o diretório de backup se não existir
        /// </summary>
        private void CriarDiretorioBackup()
        {
            try
            {
                if (!Directory.Exists(_backupDirectory))
                {
                    Directory.CreateDirectory(_backupDirectory);
                    LogBackup($"Diretório criado: {_backupDirectory}");
                }
            }
            catch (Exception ex)
            {
                LogBackup($"ERRO ao criar diretório: {ex.Message}");

                // Tentar usar diretório temporário como fallback
                string tempDir = Path.Combine(Path.GetTempPath(), "SistemaCadastro", "Backups");
                try
                {
                    Directory.CreateDirectory(tempDir);
                    _backupDirectory = tempDir;
                    LogBackup($"Usando diretório alternativo: {_backupDirectory}");
                }
                catch
                {
                    throw new Exception($"Não foi possível criar diretório de backup: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Inicia o sistema de backup automático
        /// </summary>
        public void IniciarBackupAutomatico()
        {
            try
            {
                if (!ExisteBackupRecente())
                {
                    LogBackup("Sistema iniciado. Próximo backup em 24 horas.");
                }

                TimeSpan intervalo = TimeSpan.FromHours(_backupIntervalHours);

                // ← USAR System.Threading.Timer EXPLICITAMENTE
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

        /// <summary>
        /// Para o sistema de backup automático
        /// </summary>
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

        /// <summary>
        /// Realiza o backup do banco de dados
        /// </summary>
        public string RealizarBackup(bool automatico = false)
        {
            string caminhoCompleto = "";

            try
            {
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string tipo = automatico ? "AUTO" : "MANUAL";
                string nomeArquivo = $"Backup_{tipo}_{timestamp}.bak";

                // IMPORTANTE: Usar diretório que o SQL Server tenha acesso
                string sqlServerBackupPath = ObterOuCriarDiretorioSQLServer();

                if (!string.IsNullOrEmpty(sqlServerBackupPath))
                {
                    caminhoCompleto = Path.Combine(sqlServerBackupPath, nomeArquivo);
                }
                else
                {
                    // Fallback para o diretório do usuário
                    caminhoCompleto = Path.Combine(_backupDirectory, nomeArquivo);
                }

                LogBackup($"Iniciando backup {tipo}...");
                LogBackup($"Caminho: {caminhoCompleto}");

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Comando SQL para backup
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
                        cmd.CommandTimeout = 600; // 10 minutos
                        cmd.Parameters.AddWithValue("@BackupPath", caminhoCompleto);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Copiar para o diretório do usuário se necessário
                if (sqlServerBackupPath != _backupDirectory && File.Exists(caminhoCompleto))
                {
                    string destinoUsuario = Path.Combine(_backupDirectory, nomeArquivo);
                    try
                    {
                        File.Copy(caminhoCompleto, destinoUsuario, true);
                        LogBackup($"Cópia criada em: {destinoUsuario}");
                    }
                    catch (Exception exCopy)
                    {
                        LogBackup($"Aviso: Não foi possível copiar para pasta do usuário: {exCopy.Message}");
                    }
                }

                SalvarInfoBackup(caminhoCompleto, tipo);
                LimparBackupsAntigos();

                LogBackup($"Backup {tipo} concluído: {nomeArquivo}");
                return caminhoCompleto;
            }
            catch (Exception ex)
            {
                LogBackup($"ERRO DETALHADO: {ex.ToString()}");
                throw new Exception(
                    $"Falha ao criar backup:\n\n{ex.Message}\n\n" +
                    "SOLUÇÕES:\n" +
                    "1. Execute o SQL Server Management Studio como Administrador\n" +
                    "2. Execute este comando SQL:\n" +
                    "   EXEC xp_create_subdir 'C:\\SQLBackups'\n" +
                    "3. Ou escolha uma pasta diferente no botão 'Configurar Pasta'\n" +
                    $"4. Logs em: {_backupDirectory}\\backup_log.txt"
                );
            }
        }

        /// <summary>
        /// Obtém ou cria o diretório de backup do SQL Server
        /// </summary>
        private string ObterOuCriarDiretorioSQLServer()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Primeiro tentar obter o diretório padrão
                    string sql = @"
                        DECLARE @BackupDirectory NVARCHAR(4000)
                        EXEC master.dbo.xp_instance_regread 
                            N'HKEY_LOCAL_MACHINE',
                            N'Software\Microsoft\MSSQLServer\MSSQLServer',
                            N'BackupDirectory', 
                            @BackupDirectory OUTPUT
                        SELECT @BackupDirectory AS BackupDirectory";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            string baseDir = result.ToString();
                            string ourDir = Path.Combine(baseDir, "SistemaCadastro");

                            // Tentar criar pasta via SQL
                            try
                            {
                                string createDirSql = $"EXEC xp_create_subdir '{ourDir}'";
                                using (SqlCommand createCmd = new SqlCommand(createDirSql, conn))
                                {
                                    createCmd.ExecuteNonQuery();
                                }
                                LogBackup($"Pasta SQL Server criada: {ourDir}");
                                return ourDir;
                            }
                            catch
                            {
                                // Se falhar, tentar usar C:\SQLBackups
                                try
                                {
                                    string fallbackDir = @"C:\SQLBackups";
                                    string createFallbackSql = $"EXEC xp_create_subdir '{fallbackDir}'";
                                    using (SqlCommand createCmd = new SqlCommand(createFallbackSql, conn))
                                    {
                                        createCmd.ExecuteNonQuery();
                                    }
                                    LogBackup($"Pasta alternativa criada: {fallbackDir}");
                                    return fallbackDir;
                                }
                                catch
                                {
                                    return baseDir; // Usar diretório padrão
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogBackup($"Não foi possível configurar pasta SQL Server: {ex.Message}");
            }
            return null;
        }

        /// <summary>
        /// Restaura o banco de dados a partir de um backup
        /// </summary>
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
                    string sqlKill = @"
                        USE master;
                        DECLARE @SQL VARCHAR(MAX) = '';
                        SELECT @SQL = @SQL + 'KILL ' + CAST(session_id AS VARCHAR) + ';'
                        FROM sys.dm_exec_sessions
                        WHERE database_id = DB_ID('projeto1')
                        AND session_id <> @@SPID;
                        EXEC(@SQL);";

                    using (SqlCommand cmd = new SqlCommand(sqlKill, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Single user
                    string sqlSingle = @"
                        USE master;
                        ALTER DATABASE [projeto1] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";

                    using (SqlCommand cmd = new SqlCommand(sqlSingle, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Restaurar
                    string sqlRestore = @"
                        USE master;
                        RESTORE DATABASE [projeto1]
                        FROM DISK = @BackupPath
                        WITH REPLACE, RECOVERY, STATS = 10;";

                    using (SqlCommand cmd = new SqlCommand(sqlRestore, conn))
                    {
                        cmd.CommandTimeout = 600;
                        cmd.Parameters.AddWithValue("@BackupPath", caminhoBackup);
                        cmd.ExecuteNonQuery();
                    }

                    // Multi user
                    string sqlMulti = "ALTER DATABASE [projeto1] SET MULTI_USER;";
                    using (SqlCommand cmd = new SqlCommand(sqlMulti, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                LogBackup("Backup restaurado com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                LogBackup($"ERRO ao restaurar: {ex.ToString()}");
                throw new Exception($"Falha ao restaurar backup:\n\n{ex.Message}");
            }
        }

        /// <summary>
        /// Lista todos os backups disponíveis
        /// </summary>
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

        private void LogBackup(string mensagem)
        {
            try
            {
                string logFile = Path.Combine(_backupDirectory, "backup_log.txt");
                string linha = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {mensagem}";
                File.AppendAllText(logFile, linha + Environment.NewLine);
                System.Diagnostics.Debug.WriteLine(linha);
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