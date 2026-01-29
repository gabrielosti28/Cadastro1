using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cadastro1
{
    /// <summary>
    /// Gerencia backups automáticos e manuais do banco de dados
    /// ATUALIZADO: Usa ConfiguracaoPastas para diretório configurável
    /// </summary>
    public class BackupManager
    {
        private static BackupManager _instance;
        private System.Threading.Timer _backupTimer;
        private readonly int _backupIntervalHours = 12;
        private readonly int _maxBackupsToKeep = 15;

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
            // Garantir que a pasta existe
            ConfiguracaoPastas.GarantirPastasExistem();
        }

        /// <summary>
        /// Obtém o diretório de backups configurado
        /// </summary>
        private string ObterDiretorioBackup()
        {
            return ConfiguracaoPastas.PastaBackups;
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
                string diretorioBackup = ObterDiretorioBackup();

                // Garantir que o diretório existe
                if (!Directory.Exists(diretorioBackup))
                {
                    Directory.CreateDirectory(diretorioBackup);
                }

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string tipo = automatico ? "AUTO" : "MANUAL";
                string nomeArquivo = $"Backup_{tipo}_{timestamp}.bak";

                caminhoCompleto = Path.Combine(diretorioBackup, nomeArquivo);

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
                                   "1️⃣ Clique em 'Configurar Pastas' no menu principal\n" +
                                   $"2️⃣ Dê permissão para a conta do SQL Server na pasta:\n   {ObterDiretorioBackup()}";
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
                string diretorioBackup = ObterDiretorioBackup();

                if (!Directory.Exists(diretorioBackup))
                {
                    LogBackup($"Diretório não existe: {diretorioBackup}");
                    return new BackupInfo[0];
                }

                string[] arquivos = Directory.GetFiles(diretorioBackup, "*.bak");
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
                string infoFile = Path.Combine(ObterDiretorioBackup(), "backup_history.txt");
                string linha = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}|{tipo}|{info.Name}|{info.Length} bytes";
                File.AppendAllText(infoFile, linha + Environment.NewLine);
            }
            catch { }
        }

        private static int _contadorLog = 0;

        private void LogBackup(string mensagem)
        {
            try
            {
                string logFile = Path.Combine(ObterDiretorioBackup(), "backup_log.txt");
                string linha = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {mensagem}";

                File.AppendAllText(logFile, linha + Environment.NewLine);

                _contadorLog++;
                if (_contadorLog >= 100)
                {
                    LimitarTamanhoLog(logFile);
                    _contadorLog = 0;
                }

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

        public string ObterDiretorioBackupAtual()
        {
            return ObterDiretorioBackup();
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