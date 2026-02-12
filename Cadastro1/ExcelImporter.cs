// =============================================
// IMPORTADOR DE PLANILHAS - VERSÃO MELHORADA
// Arquivo: ExcelImporter.cs (ATUALIZADO)
// SUPORTA: CSV, XLSX, XLS, TXT, TSV
// COM TRATAMENTO AMIGÁVEL DE ERROS
// =============================================
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;

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
    /// Importador universal de planilhas
    /// SUPORTA: CSV, XLSX, XLS, TXT, TSV
    /// </summary>
    public class ExcelImporter
    {
        private ClienteDAL clienteDAL;

        public ExcelImporter()
        {
            clienteDAL = new ClienteDAL();
        }

        /// <summary>
        /// Importa CPFs de qualquer formato de planilha
        /// </summary>
        public ResultadoImportacao ImportarPlanilha(string caminhoArquivo)
        {
            var resultado = new ResultadoImportacao();

            try
            {
                // Detectar tipo de arquivo pela extensão
                string extensao = Path.GetExtension(caminhoArquivo).ToLower();
                List<string> cpfsLidos;

                switch (extensao)
                {
                    case ".csv":
                        cpfsLidos = LerCSV(caminhoArquivo);
                        break;

                    case ".txt":
                    case ".tsv":
                        cpfsLidos = LerTXT(caminhoArquivo);
                        break;

                    case ".xlsx":
                    case ".xls":
                        // =============================================
                        // TRATAMENTO MELHORADO PARA EXCEL
                        // =============================================
                        cpfsLidos = TentarLerExcel(caminhoArquivo, extensao);
                        break;

                    default:
                        throw new Exception(
                            $"❌ FORMATO NÃO SUPORTADO: {extensao}\n\n" +
                            "Formatos aceitos:\n" +
                            "✓ .CSV (separado por vírgula ou ponto-e-vírgula)\n" +
                            "✓ .TXT (texto delimitado)\n" +
                            "✓ .TSV (separado por tabulação)\n" +
                            "✓ .XLSX (Excel moderno)\n" +
                            "✓ .XLS (Excel antigo)\n\n" +
                            "Converta seu arquivo para um destes formatos.");
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
                throw new Exception($"❌ Erro ao importar planilha:\n\n{ex.Message}");
            }
        }

        /// <summary>
        /// Tenta ler arquivo Excel com tratamento de erro melhorado
        /// </summary>
        private List<string> TentarLerExcel(string caminhoArquivo, string extensao)
        {
            try
            {
                // Tentar ler com OleDb
                return LerExcelOleDb(caminhoArquivo);
            }
            catch (Exception ex)
            {
                // =============================================
                // ERRO AO LER EXCEL - INSTRUÇÕES AMIGÁVEIS
                // =============================================

                if (ex.Message.Contains("not registered") ||
                    ex.Message.Contains("não registrado") ||
                    ex.Message.Contains("não foi possível encontrar") ||
                    ex.Message.Contains("Could not find"))
                {
                    throw new Exception(
                        "❌ PARA IMPORTAR ARQUIVOS EXCEL\n\n" +
                        "📝 PARA IMPORTAR ARQUIVOS EXCEL:\n\n" +
                        "Por favor, converta para CSV:\n\n" +
                        "📋 Abra o arquivo no Excel\n" +
                        "📋 Clique em 'Arquivo' → 'Salvar Como'\n" +
                        "📋 Escolha 'CSV (separado por vírgulas)'\n" +
                        "📋 Importe o arquivo .CSV gerado\n\n" +
                        "✅ VANTAGENS:\n" +
                        "• Funciona em qualquer computador\n" +
                        "• Não requer drivers ou bibliotecas\n" +
                        "• Importação mais rápida e confiável");
                }

                // Outro tipo de erro
                throw new Exception(
                    $"❌ Erro ao ler arquivo Excel:\n\n{ex.Message}\n\n" +
                    "💡 SOLUÇÃO:\n" +
                    "Converta o arquivo para CSV e tente novamente.");
            }
        }

        /// <summary>
        /// Lê arquivo CSV (vírgula ou ponto-e-vírgula)
        /// </summary>
        private List<string> LerCSV(string caminhoArquivo)
        {
            List<string> cpfs = new List<string>();

            try
            {
                // Tentar diferentes encodings
                string[] encodings = { "UTF-8", "ISO-8859-1", "Windows-1252" };
                string[] linhas = null;

                foreach (string enc in encodings)
                {
                    try
                    {
                        linhas = File.ReadAllLines(caminhoArquivo, Encoding.GetEncoding(enc));
                        break;
                    }
                    catch
                    {
                        continue;
                    }
                }

                if (linhas == null || linhas.Length == 0)
                {
                    throw new Exception("Arquivo vazio ou não foi possível ler com nenhum encoding.");
                }

                // Detectar separador (vírgula, ponto-e-vírgula ou tab)
                string separador = DetectarSeparador(linhas[0]);

                // Processar linhas (pular cabeçalho)
                for (int i = 1; i < linhas.Length; i++)
                {
                    string linha = linhas[i].Trim();
                    if (string.IsNullOrWhiteSpace(linha)) continue;

                    string[] colunas = linha.Split(new[] { separador }, StringSplitOptions.None);

                    // Tentar encontrar CPF em qualquer coluna
                    foreach (string coluna in colunas)
                    {
                        string cpf = LimparCPF(coluna);
                        if (ValidarCPF(cpf))
                        {
                            cpfs.Add(cpf);
                            break; // Pegar apenas o primeiro CPF válido da linha
                        }
                    }
                }

                return cpfs;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao ler CSV: {ex.Message}");
            }
        }

        /// <summary>
        /// Lê arquivo TXT/TSV (texto delimitado)
        /// </summary>
        private List<string> LerTXT(string caminhoArquivo)
        {
            // TXT usa mesma lógica que CSV
            return LerCSV(caminhoArquivo);
        }

        /// <summary>
        /// Lê arquivos Excel (.xlsx ou .xls) usando OleDb
        /// Funciona sem bibliotecas externas
        /// </summary>
        private List<string> LerExcelOleDb(string caminhoArquivo)
        {
            List<string> cpfs = new List<string>();

            string extensao = Path.GetExtension(caminhoArquivo).ToLower();
            string connectionString;

            // String de conexão diferente para .xlsx e .xls
            if (extensao == ".xlsx")
            {
                connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={caminhoArquivo};" +
                                  "Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
            }
            else // .xls
            {
                connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={caminhoArquivo};" +
                                  "Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
            }

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                // Obter nome da primeira planilha
                DataTable dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dtSchema == null || dtSchema.Rows.Count == 0)
                {
                    throw new Exception("Planilha não contém dados.");
                }

                string nomePlanilha = dtSchema.Rows[0]["TABLE_NAME"].ToString();

                // Ler dados da planilha
                string query = $"SELECT * FROM [{nomePlanilha}]";
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Processar cada linha
                    foreach (DataRow row in dt.Rows)
                    {
                        // Tentar encontrar CPF em qualquer coluna
                        foreach (var item in row.ItemArray)
                        {
                            if (item == null || item == DBNull.Value) continue;

                            string valor = item.ToString();
                            string cpf = LimparCPF(valor);

                            if (ValidarCPF(cpf))
                            {
                                cpfs.Add(cpf);
                                break; // Pegar apenas o primeiro CPF válido da linha
                            }
                        }
                    }
                }
            }

            return cpfs;
        }

        /// <summary>
        /// Detecta o separador usado no arquivo (vírgula, ponto-e-vírgula ou tab)
        /// </summary>
        private string DetectarSeparador(string linha)
        {
            if (string.IsNullOrWhiteSpace(linha))
                return ";";

            int virgulas = linha.Count(c => c == ',');
            int pontosVirgula = linha.Count(c => c == ';');
            int tabs = linha.Count(c => c == '\t');

            // Retornar o separador mais frequente
            if (tabs > virgulas && tabs > pontosVirgula)
                return "\t";
            else if (pontosVirgula > virgulas)
                return ";";
            else
                return ",";
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