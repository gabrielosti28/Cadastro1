// =============================================
// GERENCIADOR DE BACKUPS - VERSÃO CORRIGIDA
// Arquivo: BackupManager.cs
// COM TRATAMENTO ROBUSTO DE PERMISSÕES SQL
// =============================================
using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Cadastro1
{
    /// <summary>
    /// Gerencia backups automáticos e manuais do banco de dados
    /// VERSÃO CORRIGIDA com validação de permissões SQL Server
    /// </summary>
    public class BackupManager
    {
        private static BackupManager _instance;
        private System.Threading.Timer _backupTimer;
        private readonly int _backupIntervalHours = 24; // Alterado para 24h
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
            // Garantir que as pastas existem
            try
            {
                ConfiguracaoPastas.GarantirPastasExistem();
            }
            catch (Exception ex)
            {
                LogBackup($"Aviso ao inicializar: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém o diretório de backups configurado
        /// </summary>
        private string ObterDiretorioBackup()
        {
            try
            {
                return ConfiguracaoPastas.PastaBackups;
            }
            catch
            {
                // Fallback para pasta padrão
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "SistemaCadastroClientes", "Backups");
            }
        }

        public void IniciarBackupAutomatico()
        {
            try
            {
                LogBackup("Sistema de backup automático iniciado (intervalo: 24 horas).");

                TimeSpan intervalo = TimeSpan.FromHours(_backupIntervalHours);

                _backupTimer = new System.Threading.Timer(
                    callback: BackupTimerCallback,
                    state: null,
                    dueTime: intervalo,
                    period: intervalo
                );
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

                // VALIDAÇÃO CRÍTICA: Verificar se o diretório é válido
                if (string.IsNullOrWhiteSpace(diretorioBackup))
                {
                    throw new Exception(
                        "⚠️ PASTA DE BACKUPS NÃO CONFIGURADA!\n\n" +
                        "Por favor:\n" +
                        "1. Clique no botão '⚙️ Configurar Pastas' no menu principal\n" +
                        "2. Configure a pasta de backups\n" +
                        "3. Teste as permissões SQL antes de continuar");
                }

                // Garantir que o diretório existe
                if (!Directory.Exists(diretorioBackup))
                {
                    try
                    {
                        Directory.CreateDirectory(diretorioBackup);
                    }
                    catch (Exception exDir)
                    {
                        throw new Exception(
                            $"❌ NÃO FOI POSSÍVEL CRIAR O DIRETÓRIO:\n\n{diretorioBackup}\n\n" +
                            $"Erro: {exDir.Message}\n\n" +
                            "Solução: Configure uma pasta diferente em 'Configurar Pastas'");
                    }
                }

                // Testar permissão de escrita do Windows ANTES de tentar o backup SQL
                try
                {
                    string arquivoTeste = Path.Combine(diretorioBackup, $"teste_{Guid.NewGuid()}.tmp");
                    File.WriteAllText(arquivoTeste, "teste");
                    File.Delete(arquivoTeste);
                }
                catch (Exception exPerm)
                {
                    throw new Exception(
                        $"❌ SEM PERMISSÃO DE ESCRITA (Windows):\n\n{diretorioBackup}\n\n" +
                        $"Erro: {exPerm.Message}\n\n" +
                        "Soluções:\n" +
                        "1. Use a pasta padrão (Documentos)\n" +
                        "2. Escolha outra pasta em 'Configurar Pastas'");
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
            catch (SqlException sqlEx)
            {
                LogBackup($"ERRO SQL: {sqlEx.Message}");

                string mensagemErro = "❌ ERRO AO CRIAR BACKUP NO SQL SERVER\n\n";

                if (sqlEx.Message.Contains("Operating system error 5") ||
                    sqlEx.Message.Contains("Access is denied") ||
                    sqlEx.Message.Contains("acesso negado") ||
                    sqlEx.Message.Contains("permission"))
                {
                    mensagemErro +=
                        "O SQL Server não tem permissão para gravar nesta pasta.\n\n" +
                        "🔧 SOLUÇÃO RÁPIDA:\n\n" +
                        "1️⃣ Clique no botão '⚙️ Configurar Pastas'\n" +
                        "2️⃣ Clique em 'Restaurar Padrão'\n" +
                        "3️⃣ Clique em 'Testar Permissões SQL'\n" +
                        "4️⃣ Se o teste funcionar, salve e tente novamente\n\n" +
                        $"📁 Pasta atual: {ObterDiretorioBackup()}\n\n" +
                        "💡 A pasta padrão (Documentos) geralmente funciona sempre!";
                }
                else
                {
                    mensagemErro += $"Erro técnico:\n{sqlEx.Message}\n\n" +
                                   "Entre em contato com o suporte técnico.";
                }

                throw new Exception(mensagemErro);
            }
            catch (Exception ex)
            {
                LogBackup($"ERRO GERAL: {ex.Message}");
                throw; // Re-lançar a exceção original se já foi tratada
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