using System;
using System.IO;
using System.Text;

namespace Cadastro1.Utilidades
{
    /// <summary>
    /// Sistema profissional de logging com níveis e rotação de arquivos
    /// </summary>
    public static class Logger
    {
        public enum NivelLog
        {
            Debug,
            Info,
            Warning,
            Error,
            Fatal
        }

        private static readonly string CaminhoLogs = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Logs"
        );

        private static readonly object lockObj = new object();

        static Logger()
        {
            // Cria diretório de logs se não existir
            if (!Directory.Exists(CaminhoLogs))
                Directory.CreateDirectory(CaminhoLogs);

            // Remove logs antigos (mais de 30 dias)
            LimparLogsAntigos();
        }

        public static void Debug(string mensagem)
        {
            Gravar(NivelLog.Debug, mensagem);
        }

        public static void Info(string mensagem)
        {
            Gravar(NivelLog.Info, mensagem);
        }

        public static void Warning(string mensagem)
        {
            Gravar(NivelLog.Warning, mensagem);
        }

        public static void Error(string mensagem, Exception ex = null)
        {
            if (ex != null)
                mensagem += $"\n   Exception: {ex.Message}\n   StackTrace: {ex.StackTrace}";

            Gravar(NivelLog.Error, mensagem);
        }

        public static void Fatal(string mensagem, Exception ex = null)
        {
            if (ex != null)
                mensagem += $"\n   Exception: {ex.Message}\n   StackTrace: {ex.StackTrace}";

            Gravar(NivelLog.Fatal, mensagem);
        }

        private static void Gravar(NivelLog nivel, string mensagem)
        {
            try
            {
                lock (lockObj)
                {
                    string nomeArquivo = $"log_{DateTime.Now:yyyy-MM-dd}.txt";
                    string caminhoCompleto = Path.Combine(CaminhoLogs, nomeArquivo);

                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    string nivelStr = nivel.ToString().ToUpper().PadRight(7);
                    string linha = $"[{timestamp}] [{nivelStr}] {mensagem}\n";

                    File.AppendAllText(caminhoCompleto, linha, Encoding.UTF8);
                }
            }
            catch
            {
                // Falha silenciosa para não quebrar a aplicação
            }
        }

        private static void LimparLogsAntigos()
        {
            try
            {
                var arquivos = Directory.GetFiles(CaminhoLogs, "log_*.txt");
                var dataLimite = DateTime.Now.AddDays(-30);

                foreach (var arquivo in arquivos)
                {
                    var dataArquivo = File.GetCreationTime(arquivo);
                    if (dataArquivo < dataLimite)
                    {
                        File.Delete(arquivo);
                    }
                }
            }
            catch
            {
                // Falha silenciosa
            }
        }
    }
}