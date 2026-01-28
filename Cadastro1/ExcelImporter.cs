// =============================================
// IMPORTADOR DE PLANILHAS EXCEL/CSV
// Arquivo: ExcelImporter.cs
// Lê planilhas e extrai CPFs para mala direta
// =============================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cadastro1
{
    /// <summary>
    /// Resultado da importação de planilha
    /// </summary>
    public class ResultadoImportacao
    {
        public int TotalCPFs { get; set; }
        public int CPFsEncontrados { get; set; }
        public int CPFsNaoEncontrados { get; set; }
        public int DuplicatasRemovidas { get; set; }
        public List<string> CPFsValidos { get; set; }
        public List<string> CPFsNaoCadastrados { get; set; }
        public List<Cliente> ClientesEncontrados { get; set; }

        public ResultadoImportacao()
        {
            CPFsValidos = new List<string>();
            CPFsNaoCadastrados = new List<string>();
            ClientesEncontrados = new List<Cliente>();
        }

        public string GerarRelatorio()
        {
            var relatorio = "╔═══════════════════════════════════════════════════════════╗\n";
            relatorio += "║           📊 RELATÓRIO DE IMPORTAÇÃO DE PLANILHA          ║\n";
            relatorio += "╚═══════════════════════════════════════════════════════════╝\n\n";

            relatorio += "📄 RESUMO GERAL:\n";
            relatorio += "─────────────────────────────────────────────────────────\n";
            relatorio += $"   Total de CPFs lidos da planilha:  {TotalCPFs}\n";
            relatorio += $"   Duplicatas removidas:              {DuplicatasRemovidas}\n";
            relatorio += $"   CPFs válidos (únicos):             {CPFsValidos.Count}\n\n";

            relatorio += "✅ RESULTADO DA BUSCA:\n";
            relatorio += "─────────────────────────────────────────────────────────\n";
            relatorio += $"   ✓ CPFs encontrados no cadastro:    {CPFsEncontrados}\n";
            relatorio += $"   ✗ CPFs NÃO encontrados:            {CPFsNaoEncontrados}\n\n";

            double percentualEncontrado = CPFsValidos.Count > 0
                ? (double)CPFsEncontrados / CPFsValidos.Count * 100
                : 0;

            relatorio += $"📊 Taxa de sucesso: {percentualEncontrado:F1}%\n\n";

            if (CPFsEncontrados > 0)
            {
                relatorio += "✓ AÇÃO REALIZADA:\n";
                relatorio += "─────────────────────────────────────────────────────────\n";
                relatorio += $"   {CPFsEncontrados} clientes foram MARCADOS automaticamente\n";
                relatorio += "   Você pode ajustar a seleção manualmente se necessário\n\n";
            }

            if (CPFsNaoCadastrados.Count > 0)
            {
                relatorio += "⚠️  CPFs NÃO ENCONTRADOS NO CADASTRO:\n";
                relatorio += "─────────────────────────────────────────────────────────\n";

                int maxMostrar = Math.Min(30, CPFsNaoCadastrados.Count);

                for (int i = 0; i < maxMostrar; i++)
                {
                    relatorio += $"   {i + 1,3}. {FormatarCPF(CPFsNaoCadastrados[i])}\n";
                }

                if (CPFsNaoCadastrados.Count > maxMostrar)
                {
                    relatorio += $"\n   ... e mais {CPFsNaoCadastrados.Count - maxMostrar} CPFs\n";
                }

                relatorio += "\n💡 SUGESTÃO: Cadastre estes clientes antes de gerar a mala direta\n";
            }
            else
            {
                relatorio += "🎉 PERFEITO!\n";
                relatorio += "─────────────────────────────────────────────────────────\n";
                relatorio += "   Todos os CPFs da planilha foram encontrados!\n";
                relatorio += "   Você já pode gerar o PDF da mala direta.\n";
            }

            return relatorio;
        }

        private string FormatarCPF(string cpf)
        {
            if (cpf.Length == 11)
            {
                return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";
            }
            return cpf;
        }
    }

    /// <summary>
    /// Importador de planilhas Excel/CSV
    /// </summary>
    public class ExcelImporter
    {
        private ClienteDAL clienteDAL;

        public ExcelImporter()
        {
            clienteDAL = new ClienteDAL();
        }

        /// <summary>
        /// Importa CPFs de arquivo Excel ou CSV
        /// </summary>
        public ResultadoImportacao ImportarPlanilha(string caminhoArquivo)
        {
            var resultado = new ResultadoImportacao();

            try
            {
                // Detectar tipo de arquivo
                string extensao = Path.GetExtension(caminhoArquivo).ToLower();
                List<string> cpfsLidos;

                if (extensao == ".csv")
                {
                    cpfsLidos = LerCSV(caminhoArquivo);
                }
                else if (extensao == ".xlsx" || extensao == ".xls")
                {
                    cpfsLidos = LerExcel(caminhoArquivo);
                }
                else
                {
                    throw new Exception($"Formato não suportado: {extensao}\n\nUse arquivos .xlsx, .xls ou .csv");
                }

                resultado.TotalCPFs = cpfsLidos.Count;

                // Remover duplicatas
                var cpfsUnicos = new HashSet<string>(cpfsLidos);
                resultado.DuplicatasRemovidas = resultado.TotalCPFs - cpfsUnicos.Count;
                resultado.CPFsValidos.AddRange(cpfsUnicos);

                // Buscar clientes no banco de dados
                ProcessarCPFs(resultado);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao importar planilha:\n\n{ex.Message}");
            }
        }

        /// <summary>
        /// Lê arquivo CSV e extrai CPFs
        /// </summary>
        private List<string> LerCSV(string caminhoArquivo)
        {
            List<string> cpfs = new List<string>();

            try
            {
                string[] linhas = File.ReadAllLines(caminhoArquivo);

                // Detectar separador (ponto-e-vírgula ou vírgula)
                string separador = linhas.Length > 0 && linhas[0].Contains(";") ? ";" : ",";

                // Processar linhas (pular cabeçalho)
                for (int i = 1; i < linhas.Length; i++)
                {
                    string linha = linhas[i].Trim();
                    if (string.IsNullOrWhiteSpace(linha)) continue;

                    string[] colunas = linha.Split(new[] { separador }, StringSplitOptions.None);

                    // CPF está na coluna 2 (índice 1)
                    if (colunas.Length > 1)
                    {
                        string cpf = LimparCPF(colunas[1]);
                        if (ValidarCPF(cpf))
                        {
                            cpfs.Add(cpf);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao ler CSV: {ex.Message}");
            }

            return cpfs;
        }

        /// <summary>
        /// Lê arquivo Excel (.xlsx ou .xls) e extrai CPFs
        /// Usa a biblioteca EPPlus ou similar
        /// </summary>
        private List<string> LerExcel(string caminhoArquivo)
        {
            // NOTA: Para ler arquivos .xlsx, seria necessário usar EPPlus ou ClosedXML
            // Como a biblioteca não está instalada, vamos sugerir conversão para CSV
            // ou implementar leitura básica via OleDb

            throw new Exception(
                "⚠️ ARQUIVO EXCEL DETECTADO\n\n" +
                "Para arquivos .xlsx ou .xls, por favor:\n\n" +
                "1. Abra a planilha no Excel\n" +
                "2. Vá em 'Arquivo' > 'Salvar Como'\n" +
                "3. Escolha o formato 'CSV (delimitado por vírgulas)'\n" +
                "4. Importe o arquivo .csv gerado\n\n" +
                "Ou instale a biblioteca EPPlus no projeto para suporte direto a Excel."
            );
        }

        /// <summary>
        /// Remove formatação do CPF e deixa só números
        /// Completa com zero à esquerda se tiver 10 dígitos
        /// </summary>
        private string LimparCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return "";

            string cpfLimpo = new string(cpf.Where(char.IsDigit).ToArray());

            // Se tem 10 dígitos, adiciona zero à esquerda
            if (cpfLimpo.Length == 10)
            {
                cpfLimpo = "0" + cpfLimpo;
            }

            return cpfLimpo;
        }

        /// <summary>
        /// Valida se o CPF tem 10 ou 11 dígitos
        /// </summary>
        private bool ValidarCPF(string cpf)
        {
            return !string.IsNullOrWhiteSpace(cpf) &&
                   (cpf.Length == 10 || cpf.Length == 11) &&
                   cpf.All(char.IsDigit);
        }

        /// <summary>
        /// Processa os CPFs e busca clientes no banco
        /// </summary>
        private void ProcessarCPFs(ResultadoImportacao resultado)
        {
            // Buscar todos os clientes do banco
            var todosClientes = clienteDAL.ListarTodosClientes();

            foreach (string cpf in resultado.CPFsValidos)
            {
                // Buscar cliente pelo CPF
                var cliente = todosClientes.FirstOrDefault(c => LimparCPF(c.CPF) == cpf);

                if (cliente != null)
                {
                    resultado.ClientesEncontrados.Add(cliente);
                    resultado.CPFsEncontrados++;
                }
                else
                {
                    resultado.CPFsNaoCadastrados.Add(cpf);
                    resultado.CPFsNaoEncontrados++;
                }
            }
        }
    }
}