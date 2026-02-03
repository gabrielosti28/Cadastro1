// =============================================
// GERADOR DE PDF PARA MALA DIRETA
// Arquivo: MailingPdfGenerator.cs
// ATUALIZADO: Divisão automática em múltiplos arquivos
// =============================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Cadastro1
{
    /// <summary>
    /// Resultado da geração de PDFs
    /// </summary>
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
            sb.AppendLine("💡 DICA: Os arquivos foram divididos para melhor performance!");
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
        private const float MM_TO_POINTS = 2.834645f; // Conversão mm para pontos PDF
        private const int CLIENTES_POR_ARQUIVO = 20; // Máximo de páginas por PDF

        public MailingPdfGenerator()
        {
            // Garantir que a pasta existe
            ConfiguracaoPastas.GarantirPastasExistem();
        }

        /// <summary>
        /// Obtém o diretório de PDFs
        /// </summary>
        private string ObterDiretorioPDFs()
        {
            return ConfiguracaoPastas.PastaPDFs;
        }

        /// <summary>
        /// Gera PDFs com mala direta para múltiplos clientes
        /// ATUALIZADO: Divide automaticamente em arquivos de até 20 páginas
        /// </summary>
        public ResultadoGeracaoPDF GerarPDF(MailingTemplate template, List<Cliente> clientes, string caminhoSaida = null)
        {
            try
            {
                // Se não foi especificado caminho, usar o diretório configurado
                if (string.IsNullOrEmpty(caminhoSaida))
                {
                    caminhoSaida = ObterDiretorioPDFs();
                }

                // Garantir que o diretório existe
                if (!Directory.Exists(caminhoSaida))
                {
                    Directory.CreateDirectory(caminhoSaida);
                }

                ResultadoGeracaoPDF resultado = new ResultadoGeracaoPDF
                {
                    TotalClientes = clientes.Count
                };

                // Calcular quantos arquivos serão necessários
                int totalArquivos = (int)Math.Ceiling((double)clientes.Count / CLIENTES_POR_ARQUIVO);
                resultado.TotalArquivos = totalArquivos;

                // Timestamp base para nomenclatura
                string timestampBase = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                // Gerar cada arquivo
                for (int arquivoNum = 0; arquivoNum < totalArquivos; arquivoNum++)
                {
                    // Calcular índices dos clientes para este arquivo
                    int indiceInicio = arquivoNum * CLIENTES_POR_ARQUIVO;
                    int indiceFim = Math.Min(indiceInicio + CLIENTES_POR_ARQUIVO, clientes.Count);
                    int clientesNesteArquivo = indiceFim - indiceInicio;

                    // Nome do arquivo
                    string nomeArquivo;
                    if (totalArquivos == 1)
                    {
                        // Se for apenas um arquivo, não adicionar numeração
                        nomeArquivo = $"MailaDireta_{timestampBase}.pdf";
                    }
                    else
                    {
                        // Múltiplos arquivos - adicionar numeração
                        nomeArquivo = $"MailaDireta_{timestampBase}_Parte{arquivoNum + 1}de{totalArquivos}.pdf";
                    }

                    string caminhoArquivo = Path.Combine(caminhoSaida, nomeArquivo);

                    // Criar o PDF
                    Document document = new Document(PageSize.A4, 0, 0, 0, 0);
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(caminhoArquivo, FileMode.Create));
                    document.Open();

                    // Adicionar clientes deste lote
                    for (int i = indiceInicio; i < indiceFim; i++)
                    {
                        AdicionarPaginaCliente(document, writer, template, clientes[i]);

                        // Adicionar nova página se não for o último cliente do arquivo
                        if (i < indiceFim - 1)
                        {
                            document.NewPage();
                        }
                    }

                    // Fechar documento
                    document.Close();
                    writer.Close();

                    // Adicionar ao resultado
                    resultado.ArquivosGerados.Add(caminhoArquivo);

                    // Calcular tamanho
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

        /// <summary>
        /// Adiciona uma página com dados de um cliente
        /// </summary>
        private void AdicionarPaginaCliente(Document document, PdfWriter writer,
            MailingTemplate template, Cliente cliente)
        {
            PdfContentByte cb = writer.DirectContent;

            // Se houver imagem de fundo, adicionar
            if (!string.IsNullOrEmpty(template.CaminhoImagemFundo) &&
                File.Exists(template.CaminhoImagemFundo))
            {
                try
                {
                    Image imgFundo = Image.GetInstance(template.CaminhoImagemFundo);
                    imgFundo.ScaleToFit(PageSize.A4.Width, PageSize.A4.Height);
                    imgFundo.SetAbsolutePosition(0, 0);
                    document.Add(imgFundo);
                }
                catch
                {
                    // Se falhar ao carregar imagem, continuar sem ela
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

        /// <summary>
        /// Adiciona texto em posição específica do PDF
        /// </summary>
        private void AdicionarTexto(PdfContentByte cb, string texto, TemplateCampo campo)
        {
            // Converter mm para pontos
            float x = campo.PosicaoX * MM_TO_POINTS;
            // Y em PDF é de baixo para cima, então inverter
            float y = PageSize.A4.Height - (campo.PosicaoY * MM_TO_POINTS);

            // Definir fonte
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

            cb.BeginText();
            cb.SetFontAndSize(bf, campo.FonteTamanho);
            cb.SetTextMatrix(x, y);
            cb.ShowText(texto);
            cb.EndText();
        }

        /// <summary>
        /// Obtém o valor de um campo do cliente
        /// </summary>
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
                    return $"{cliente.Cidade} - SP"; // Assumindo SP

                case "CEP":
                    return FormatarCEP(cliente.CEP);

                case "ENDERECO_COMPLETO":
                    return $"{cliente.Endereco}\n{cliente.Cidade} - SP\nCEP: {FormatarCEP(cliente.CEP)}";

                default:
                    return "";
            }
        }

        /// <summary>
        /// Formata CEP para exibição
        /// </summary>
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