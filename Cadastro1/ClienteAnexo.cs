// =============================================
// CLASSE MODELO - ANEXO DO CLIENTE
// Arquivo: ClienteAnexo.cs
// Sistema Profissional de Cadastro
// =============================================
using System;

namespace Cadastro1
{
    public class ClienteAnexo
    {
        public int AnexoID { get; set; }
        public int ClienteID { get; set; }
        public string NomeArquivo { get; set; }
        public string NomeOriginal { get; set; }
        public string TipoArquivo { get; set; }
        public long TamanhoBytes { get; set; }
        public string CaminhoArquivo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataUpload { get; set; }
        public string UploadPor { get; set; }
        public bool Ativo { get; set; }

        // Propriedades auxiliares para exibição
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

        public string IconeArquivo
        {
            get
            {
                string ext = TipoArquivo.ToLower();

                if (ext.Contains("pdf")) return "📄";
                if (ext.Contains("doc") || ext.Contains("txt")) return "📝";
                if (ext.Contains("xls") || ext.Contains("csv")) return "📊";
                if (ext.Contains("jpg") || ext.Contains("jpeg") ||
                    ext.Contains("png") || ext.Contains("gif") ||
                    ext.Contains("bmp")) return "🖼️";
                if (ext.Contains("zip") || ext.Contains("rar")) return "📦";

                return "📎";
            }
        }

        public bool EhImagem
        {
            get
            {
                string ext = TipoArquivo.ToLower();
                return ext.Contains("jpg") || ext.Contains("jpeg") ||
                       ext.Contains("png") || ext.Contains("gif") ||
                       ext.Contains("bmp");
            }
        }
    }
}