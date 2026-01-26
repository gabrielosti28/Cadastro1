// =============================================
// GERADOR DE PDF PARA MALA DIRETA
// Arquivo: MailingPdfGenerator.cs
// Usa iTextSharp para gerar PDFs
// =============================================
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Cadastro1
{
    public class MailingPdfGenerator
    {
        private const float MM_TO_POINTS = 2.834645f; // Conversão mm para pontos PDF

        /// <summary>
        /// Gera PDF com mala direta para múltiplos clientes
        /// </summary>
        public string GerarPDF(MailingTemplate template, List<Cliente> clientes, string caminhoSaida)
        {
            try
            {
                // Criar documento A4
                Document document = new Document(PageSize.A4, 0, 0, 0, 0);
                string arquivoSaida = Path.Combine(caminhoSaida,
                    $"MailaDireta_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(arquivoSaida, FileMode.Create));
                document.Open();

                // Para cada cliente, criar uma página
                foreach (Cliente cliente in clientes)
                {
                    AdicionarPaginaCliente(document, writer, template, cliente);

                    // Adicionar nova página se não for o último
                    if (cliente != clientes[clientes.Count - 1])
                    {
                        document.NewPage();
                    }
                }

                document.Close();
                return arquivoSaida;
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