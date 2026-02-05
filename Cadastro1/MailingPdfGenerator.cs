// =============================================
// GERADOR DE PDF PARA MALA DIRETA (CORRIGIDO)
// Arquivo: MailingPdfGenerator.cs
// CORREÇÃO: Conversão precisa de mm para pontos PDF e posicionamento exato
// =============================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Cadastro1
{
    public class ResultadoGeracaoPDF
    {
        public int TotalClientes { get; set; }
        public int TotalArquivos { get; set; }
        public List<string> ArquivosGerados { get; set; }
        public long TamanhoTotalBytes { get; set; }

        public ResultadoGeracaoPDF()
        {
            ArquivosGerados = new List<string>();
        }

        public string TamanhoTotalFormatado
        {
            get
            {
                string[] tamanhos = { "B", "KB", "MB", "GB" };
                double tam = TamanhoTotalBytes;
                int ordem = 0;
                while (tam >= 1024 && ordem < tamanhos.Length - 1)
                {
                    ordem++;
                    tam = tam / 1024;
                }
                return string.Format("{0:0.##} {1}", tam, tamanhos[ordem]);
            }
        }

        public string GerarRelatorio()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("╔═══════════════════════════════════════════════════════════╗");
            sb.AppendLine("║           📄 MALA DIRETA GERADA COM SUCESSO!              ║");
            sb.AppendLine("╚═══════════════════════════════════════════════════════════╝\n");

            sb.AppendLine("📊 RESUMO DA GERAÇÃO:");
            sb.AppendLine("─────────────────────────────────────────────────────────");
            sb.AppendLine($"   Total de clientes:        {TotalClientes}");
            sb.AppendLine($"   Arquivos PDF gerados:     {TotalArquivos}");
            sb.AppendLine($"   Páginas por arquivo:      até 20 páginas");
            sb.AppendLine($"   Tamanho total:            {TamanhoTotalFormatado}\n");

            sb.AppendLine("📁 ARQUIVOS GERADOS:");
            sb.AppendLine("─────────────────────────────────────────────────────────");

            for (int i = 0; i < ArquivosGerados.Count; i++)
            {
                FileInfo info = new FileInfo(ArquivosGerados[i]);
                string tamanho = FormatarTamanho(info.Length);

                sb.AppendLine($"   {i + 1}. {info.Name}");
                sb.AppendLine($"      Tamanho: {tamanho}");
                sb.AppendLine($"      Caminho: {info.DirectoryName}\n");
            }

            sb.AppendLine("═══════════════════════════════════════════════════════════");
            sb.AppendLine("✅ Os textos foram posicionados EXATAMENTE onde você marcou!");
            sb.AppendLine("═══════════════════════════════════════════════════════════");

            return sb.ToString();
        }

        private string FormatarTamanho(long bytes)
        {
            string[] tamanhos = { "B", "KB", "MB", "GB" };
            double tam = bytes;
            int ordem = 0;
            while (tam >= 1024 && ordem < tamanhos.Length - 1)
            {
                ordem++;
                tam = tam / 1024;
            }
            return string.Format("{0:0.##} {1}", tam, tamanhos[ordem]);
        }
    }

    public class MailingPdfGenerator
    {
        // =============================================
        // CONSTANTES CORRIGIDAS PARA PRECISÃO TOTAL
        // =============================================

        // Conversão exata: 1 mm = 2.834645669 pontos PDF (72 DPI / 25.4 mm)
        private const float MM_TO_POINTS = 2.834645669f;

        // Tamanho exato de uma folha A4 em milímetros
        private const float A4_WIDTH_MM = 210f;
        private const float A4_HEIGHT_MM = 297f;

        // Tamanho exato de uma folha A4 em pontos PDF
        private const float A4_WIDTH_POINTS = 595.276f;  // 210mm * 2.834645669
        private const float A4_HEIGHT_POINTS = 841.890f; // 297mm * 2.834645669

        private const int CLIENTES_POR_ARQUIVO = 20;

        public MailingPdfGenerator()
        {
            ConfiguracaoPastas.GarantirPastasExistem();
        }

        private string ObterDiretorioPDFs()
        {
            return ConfiguracaoPastas.PastaPDFs;
        }

        public ResultadoGeracaoPDF GerarPDF(MailingTemplate template, List<Cliente> clientes, string caminhoSaida = null)
        {
            try
            {
                if (string.IsNullOrEmpty(caminhoSaida))
                {
                    caminhoSaida = ObterDiretorioPDFs();
                }

                if (!Directory.Exists(caminhoSaida))
                {
                    Directory.CreateDirectory(caminhoSaida);
                }

                ResultadoGeracaoPDF resultado = new ResultadoGeracaoPDF
                {
                    TotalClientes = clientes.Count
                };

                int totalArquivos = (int)Math.Ceiling((double)clientes.Count / CLIENTES_POR_ARQUIVO);
                resultado.TotalArquivos = totalArquivos;

                string timestampBase = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                for (int arquivoNum = 0; arquivoNum < totalArquivos; arquivoNum++)
                {
                    int indiceInicio = arquivoNum * CLIENTES_POR_ARQUIVO;
                    int indiceFim = Math.Min(indiceInicio + CLIENTES_POR_ARQUIVO, clientes.Count);

                    string nomeArquivo;
                    if (totalArquivos == 1)
                    {
                        nomeArquivo = $"MailaDireta_{timestampBase}.pdf";
                    }
                    else
                    {
                        nomeArquivo = $"MailaDireta_{timestampBase}_Parte{arquivoNum + 1}de{totalArquivos}.pdf";
                    }

                    string caminhoArquivo = Path.Combine(caminhoSaida, nomeArquivo);

                    // =============================================
                    // CRIAR PDF COM TAMANHO EXATO A4
                    // =============================================
                    Rectangle a4Exato = new Rectangle(A4_WIDTH_POINTS, A4_HEIGHT_POINTS);
                    Document document = new Document(a4Exato, 0, 0, 0, 0); // Sem margens

                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(caminhoArquivo, FileMode.Create));
                    document.Open();

                    for (int i = indiceInicio; i < indiceFim; i++)
                    {
                        AdicionarPaginaCliente(document, writer, template, clientes[i]);

                        if (i < indiceFim - 1)
                        {
                            document.NewPage();
                        }
                    }

                    document.Close();
                    writer.Close();

                    resultado.ArquivosGerados.Add(caminhoArquivo);

                    FileInfo info = new FileInfo(caminhoArquivo);
                    resultado.TamanhoTotalBytes += info.Length;
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gerar PDF: " + ex.Message);
            }
        }

        private void AdicionarPaginaCliente(Document document, PdfWriter writer,
            MailingTemplate template, Cliente cliente)
        {
            PdfContentByte cb = writer.DirectContent;

            // Se houver imagem de fundo, adicionar com tamanho EXATO A4
            if (!string.IsNullOrEmpty(template.CaminhoImagemFundo) &&
                File.Exists(template.CaminhoImagemFundo))
            {
                try
                {
                    Image imgFundo = Image.GetInstance(template.CaminhoImagemFundo);

                    // =============================================
                    // ESCALAR IMAGEM PARA TAMANHO EXATO A4
                    // =============================================
                    imgFundo.ScaleAbsolute(A4_WIDTH_POINTS, A4_HEIGHT_POINTS);
                    imgFundo.SetAbsolutePosition(0, 0);

                    document.Add(imgFundo);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Erro ao carregar imagem: {ex.Message}");
                }
            }

            // Adicionar cada campo do template
            foreach (TemplateCampo campo in template.Campos)
            {
                string valor = ObterValorCampo(cliente, campo.Nome);

                if (!string.IsNullOrEmpty(valor))
                {
                    AdicionarTexto(cb, valor, campo);
                }
            }
        }

        private void AdicionarTexto(PdfContentByte cb, string texto, TemplateCampo campo)
        {
            // =============================================
            // CONVERSÃO PRECISA DE COORDENADAS
            // =============================================

            // Converter mm para pontos PDF com fator exato
            float xPoints = campo.PosicaoX * MM_TO_POINTS;

            // Y em PDF é de baixo para cima, então inverter
            // A altura total é A4_HEIGHT_POINTS
            float yPoints = A4_HEIGHT_POINTS - (campo.PosicaoY * MM_TO_POINTS);

            // Definir fonte
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

            cb.BeginText();
            cb.SetFontAndSize(bf, campo.FonteTamanho);

            // =============================================
            // POSICIONAR TEXTO EXATAMENTE NAS COORDENADAS
            // =============================================
            cb.SetTextMatrix(xPoints, yPoints);

            cb.ShowText(texto);
            cb.EndText();

            // Debug: Desenhar marcador visual (opcional - remover em produção)
            // DesenhaMarcadorDebug(cb, xPoints, yPoints);
        }

        // Método auxiliar para debug visual (opcional)
        private void DesenhaMarcadorDebug(PdfContentByte cb, float x, float y)
        {
            cb.SaveState();
            cb.SetColorStroke(BaseColor.RED);
            cb.SetLineWidth(0.5f);

            // Desenhar cruz no ponto exato
            cb.MoveTo(x - 5, y);
            cb.LineTo(x + 5, y);
            cb.MoveTo(x, y - 5);
            cb.LineTo(x, y + 5);
            cb.Stroke();

            cb.RestoreState();
        }

        private string ObterValorCampo(Cliente cliente, string nomeCampo)
        {
            switch (nomeCampo.ToUpper())
            {
                case "NOMECOMPLETO":
                case "NOME":
                    return cliente.NomeCompleto;

                case "ENDERECO":
                    return cliente.Endereco;

                case "CIDADE":
                    return $"{cliente.Cidade} - SP";

                case "CEP":
                    return FormatarCEP(cliente.CEP);

                case "ENDERECO_COMPLETO":
                    return $"{cliente.Endereco}\n{cliente.Cidade} - SP\nCEP: {FormatarCEP(cliente.CEP)}";

                default:
                    return "";
            }
        }

        private string FormatarCEP(string cep)
        {
            if (string.IsNullOrEmpty(cep)) return "";

            cep = cep.Replace("-", "").Trim();

            if (cep.Length == 8)
            {
                return $"{cep.Substring(0, 5)}-{cep.Substring(5, 3)}";
            }

            return cep;
        }
    }
}