// =============================================
// GERENCIADOR DE BACKUP AUTOMÁTICO - CORRIGIDO
// Arquivo: BackupManager.cs
// Sistema Profissional de Cadastro
// =============================================
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
    /// Protege contra perda de dados por falhas de energia, erros de sistema, etc.
    /// </summary>
    public class BackupManager
    {
        private static BackupManager _instance;
        private Timer _backupTimer;
        private readonly string _backupDirectory;
        private readonly int _backupIntervalHours = 24; // Backup diário
        private readonly int _maxBackupsToKeep = 30; // Manter 30 dias de backup

        // Singleton para garantir uma única instância
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
            // CORRIGIDO: Usar diretório temporário do sistema se Documentos não funcionar
            try
            {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                _backupDirectory = Path.Combine(documentsPath, "SistemaCadastroClientes", "Backups");
            }
            catch
            {
                // Fallback para pasta temporária
                string tempPath = Path.GetTempPath();
                _backupDirectory = Path.Combine(tempPath, "SistemaCadastroClientes", "Backups");
            }

            // Criar diretório se não existir
            try
            {
                if (!Directory.Exists(_backupDirectory))
                {
                    Directory.CreateDirectory(_backupDirectory);
                }
            }
            catch (Exception ex)
            {
                // Se não conseguir criar, usar diretório da aplicação
                string appPath = Application.StartupPath;
                _backupDirectory = Path.Combine(appPath, "Backups");
                Directory.CreateDirectory(_backupDirectory);

                LogBackup($"AVISO: Usando diretório alternativo: {_backupDirectory}");
            }
        }

        /// <summary>
        /// Inicia o sistema de backup automático
        /// </summary>
        public void IniciarBackupAutomatico()
        {
            try
            {
                // Verificar se já existe um backup recente (últimas 24h)
                if (!ExisteBackupRecente())
                {
                    // NÃO fazer backup imediato - pode causar problemas
                    // Apenas agendar o próximo
                    LogBackup("Sistema de backup iniciado. Próximo backup em 24 horas.");
                }

                // Configurar timer para backup automático
                TimeSpan intervalo = TimeSpan.FromHours(_backupIntervalHours);
                _backupTimer = new Timer(
                    callback: BackupTimerCallback,
                    state: null,
                    dueTime: intervalo,
                    period: intervalo
                );

                LogBackup("Sistema de backup automático iniciado com sucesso.");
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

        /// <summary>
        /// Callback do timer de backup
        /// </summary>
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
        /// <param name="automatico">Se true, é um backup automático. Se false, é manual.</param>
        /// <returns>Caminho do arquivo de backup criado</returns>
        public string RealizarBackup(bool automatico = false)
        {
            try
            {
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string tipo = automatico ? "AUTO" : "MANUAL";
                string nomeArquivo = $"Backup_{tipo}_{timestamp}.bak";
                string caminhoCompleto = Path.Combine(_backupDirectory, nomeArquivo);

                LogBackup($"Iniciando backup {tipo}...");
                LogBackup($"Caminho do backup: {caminhoCompleto}");

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // CORRIGIDO: Verificar se temos permissão para fazer backup
                    // Primeiro tentar obter o diretório padrão de backup do SQL Server
                    string sqlServerBackupPath = ObterDiretorioBackupSQLServer(conn);

                    if (!string.IsNullOrEmpty(sqlServerBackupPath))
                    {
                        // Usar diretório do SQL Server
                        caminhoCompleto = Path.Combine(sqlServerBackupPath, nomeArquivo);
                        LogBackup($"Usando diretório SQL Server: {caminhoCompleto}");
                    }

                    // CORRIGIDO: Comando SQL mais robusto
                    string sqlBackup = $@"
                        BACKUP DATABASE [projeto1]
                        TO DISK = @BackupPath
                        WITH 
                             NAME = 'Backup {tipo} - ' + CONVERT(VARCHAR, GETDATE(), 120),
                             SKIP,
                             NOREWIND,
                             NOUNLOAD,
                             STATS = 10";

                    using (SqlCommand cmd = new SqlCommand(sqlBackup, conn))
                    {
                        cmd.CommandTimeout = 600; // 10 minutos timeout
                        cmd.Parameters.AddWithValue("@BackupPath", caminhoCompleto);

                        cmd.ExecuteNonQuery();
                    }
                }

                // Salvar informações do backup
                SalvarInfoBackup(caminhoCompleto, tipo);

                // Limpar backups antigos
                LimparBackupsAntigos();

                LogBackup($"Backup {tipo} concluído com sucesso: {nomeArquivo}");

                return caminhoCompleto;
            }
            catch (Exception ex)
            {
                LogBackup($"ERRO DETALHADO ao realizar backup: {ex.ToString()}");
                throw new Exception($"Falha ao criar backup:\n\n{ex.Message}\n\nVerifique:\n1. Permissões do SQL Server\n2. Espaço em disco\n3. Logs em: {_backupDirectory}\\backup_log.txt");
            }
        }

        /// <summary>
        /// Obtém o diretório padrão de backup do SQL Server
        /// </summary>
        private string ObterDiretorioBackupSQLServer(SqlConnection conn)
        {
            try
            {
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
                        string path = result.ToString();
                        // Criar subpasta para nosso sistema
                        string ourPath = Path.Combine(path, "SistemaCadastro");

                        // Tentar criar a pasta (SQL Server precisa ter permissão)
                        try
                        {
                            string createDirSql = $@"
                                EXEC xp_create_subdir '{ourPath}'";
                            using (SqlCommand createCmd = new SqlCommand(createDirSql, conn))
                            {
                                createCmd.ExecuteNonQuery();
                            }
                            return ourPath;
                        }
                        catch
                        {
                            // Se não conseguir criar subpasta, usar diretório principal
                            return path;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogBackup($"Não foi possível obter diretório SQL Server: {ex.Message}");
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

                LogBackup($"Iniciando restauração do backup: {Path.GetFileName(caminhoBackup)}");

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // CORRIGIDO: Matar todas as conexões ativas primeiro
                    string sqlKillConnections = @"
                        USE master;
                        DECLARE @SQL VARCHAR(MAX) = '';
                        SELECT @SQL = @SQL + 'KILL ' + CAST(session_id AS VARCHAR) + ';'
                        FROM sys.dm_exec_sessions
                        WHERE database_id = DB_ID('projeto1')
                        AND session_id <> @@SPID;
                        EXEC(@SQL);";

                    using (SqlCommand cmd = new SqlCommand(sqlKillConnections, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Colocar banco em modo single user
                    string sqlSingleUser = @"
                        USE master;
                        ALTER DATABASE [projeto1] 
                        SET SINGLE_USER 
                        WITH ROLLBACK IMMEDIATE;";

                    using (SqlCommand cmd = new SqlCommand(sqlSingleUser, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Restaurar backup
                    string sqlRestore = $@"
                        USE master;
                        RESTORE DATABASE [projeto1]
                        FROM DISK = @BackupPath
                        WITH REPLACE,
                             RECOVERY,
                             STATS = 10;";

                    using (SqlCommand cmd = new SqlCommand(sqlRestore, conn))
                    {
                        cmd.CommandTimeout = 600; // 10 minutos timeout
                        cmd.Parameters.AddWithValue("@BackupPath", caminhoBackup);

                        cmd.ExecuteNonQuery();
                    }

                    // Voltar para modo multi user
                    string sqlMultiUser = @"
                        ALTER DATABASE [projeto1] 
                        SET MULTI_USER;";

                    using (SqlCommand cmd = new SqlCommand(sqlMultiUser, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                LogBackup("Backup restaurado com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                LogBackup($"ERRO ao restaurar backup: {ex.ToString()}");
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
                // CORRIGIDO: Verificar se diretório existe
                if (!Directory.Exists(_backupDirectory))
                {
                    LogBackup($"Diretório de backup não existe: {_backupDirectory}");
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

                // Ordenar por data (mais recente primeiro)
                Array.Sort(backups, (a, b) => b.DataCriacao.CompareTo(a.DataCriacao));

                return backups;
            }
            catch (Exception ex)
            {
                LogBackup($"ERRO ao listar backups: {ex.Message}");
                return new BackupInfo[0];
            }
        }

        /// <summary>
        /// Verifica se existe backup recente (últimas 24h)
        /// </summary>
        private bool ExisteBackupRecente()
        {
            try
            {
                BackupInfo[] backups = ListarBackups();
                if (backups.Length == 0)
                    return false;

                TimeSpan diferenca = DateTime.Now - backups[0].DataCriacao;
                return diferenca.TotalHours < _backupIntervalHours;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Remove backups antigos mantendo apenas os últimos N backups
        /// </summary>
        private void LimparBackupsAntigos()
        {
            try
            {
                BackupInfo[] backups = ListarBackups();

                if (backups.Length > _maxBackupsToKeep)
                {
                    // Remover backups excedentes (mais antigos)
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
                LogBackup($"ERRO ao limpar backups antigos: {ex.Message}");
            }
        }

        /// <summary>
        /// Salva informações sobre o backup realizado
        /// </summary>
        private void SalvarInfoBackup(string caminhoBackup, string tipo)
        {
            try
            {
                FileInfo info = new FileInfo(caminhoBackup);
                string infoFile = Path.Combine(_backupDirectory, "backup_history.txt");

                string linha = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}|{tipo}|{info.Name}|{info.Length} bytes";

                File.AppendAllText(infoFile, linha + Environment.NewLine);
            }
            catch (Exception ex)
            {
                LogBackup($"ERRO ao salvar info do backup: {ex.Message}");
            }
        }

        /// <summary>
        /// Registra log de operações de backup
        /// </summary>
        private void LogBackup(string mensagem)
        {
            try
            {
                string logFile = Path.Combine(_backupDirectory, "backup_log.txt");
                string linha = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {mensagem}";

                File.AppendAllText(logFile, linha + Environment.NewLine);

                // Também escrever no console de debug
                System.Diagnostics.Debug.WriteLine(linha);
            }
            catch
            {
                // Silenciar erros de log
            }
        }

        /// <summary>
        /// Verifica a integridade de um backup
        /// </summary>
        public bool VerificarIntegridadeBackup(string caminhoBackup)
        {
            try
            {
                if (!File.Exists(caminhoBackup))
                    return false;

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    string sqlVerify = @"
                        RESTORE VERIFYONLY
                        FROM DISK = @BackupPath";

                    using (SqlCommand cmd = new SqlCommand(sqlVerify, conn))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@BackupPath", caminhoBackup);

                        cmd.ExecuteNonQuery();
                    }
                }

                LogBackup($"Integridade verificada com sucesso: {Path.GetFileName(caminhoBackup)}");
                return true;
            }
            catch (Exception ex)
            {
                LogBackup($"ERRO na verificação de integridade: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Obtém o diretório onde os backups são salvos
        /// </summary>
        public string ObterDiretorioBackup()
        {
            return _backupDirectory;
        }
    }

    /// <summary>
    /// Classe para armazenar informações de backup
    /// </summary>
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