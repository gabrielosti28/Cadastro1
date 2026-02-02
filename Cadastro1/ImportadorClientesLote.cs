// =============================================
// IMPORTADOR DE CLIENTES EM LOTE
// Arquivo: ImportadorClientesLote.cs
// Cadastro automático via planilhas
// =============================================
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Cadastro1
{
    /// <summary>
    /// Resultado do cadastro de um único cliente
    /// </summary>
    public class ResultadoCadastroCliente
    {
        public bool Sucesso { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string MensagemErro { get; set; }
        public List<string> CamposFaltantes { get; set; }
        public Dictionary<string, string> DadosExcedentes { get; set; }
        public int? ClienteID { get; set; }

        public ResultadoCadastroCliente()
        {
            CamposFaltantes = new List<string>();
            DadosExcedentes = new Dictionary<string, string>();
        }
    }

    /// <summary>
    /// Resultado completo da importação em lote
    /// </summary>
    public class ResultadoImportacaoLote
    {
        public int TotalLinhas { get; set; }
        public int Sucessos { get; set; }
        public int Falhas { get; set; }
        public int CPFsDuplicados { get; set; }
        public List<ResultadoCadastroCliente> Resultados { get; set; }

        public ResultadoImportacaoLote()
        {
            Resultados = new List<ResultadoCadastroCliente>();
        }

        public string GerarRelatorioDetalhado()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("╔═══════════════════════════════════════════════════════════════════╗");
            sb.AppendLine("║         📊 RELATÓRIO DE IMPORTAÇÃO EM LOTE DE CLIENTES           ║");
            sb.AppendLine("╚═══════════════════════════════════════════════════════════════════╝\n");

            sb.AppendLine("📈 RESUMO GERAL:");
            sb.AppendLine("─────────────────────────────────────────────────────────────────────");
            sb.AppendLine($"   Total de linhas processadas:    {TotalLinhas}");
            sb.AppendLine($"   ✅ Cadastros bem-sucedidos:      {Sucessos}");
            sb.AppendLine($"   ❌ Falhas no cadastro:           {Falhas}");
            sb.AppendLine($"   🔄 CPFs já existentes (pulados): {CPFsDuplicados}\n");

            double taxaSucesso = TotalLinhas > 0 ? (double)Sucessos / TotalLinhas * 100 : 0;
            sb.AppendLine($"📊 Taxa de sucesso: {taxaSucesso:F1}%\n");

            // SUCESSOS
            var sucessos = Resultados.Where(r => r.Sucesso).ToList();
            if (sucessos.Count > 0)
            {
                sb.AppendLine("✅ CLIENTES CADASTRADOS COM SUCESSO:");
                sb.AppendLine("─────────────────────────────────────────────────────────────────────");

                int mostrar = Math.Min(20, sucessos.Count);
                for (int i = 0; i < mostrar; i++)
                {
                    var r = sucessos[i];
                    sb.AppendLine($"   {i + 1,3}. {r.Nome.PadRight(40)} | CPF: {FormatarCPF(r.CPF)}");

                    if (r.DadosExcedentes.Count > 0)
                    {
                        sb.AppendLine($"        📎 {r.DadosExcedentes.Count} campo(s) salvos em anexo");
                    }
                }

                if (sucessos.Count > mostrar)
                {
                    sb.AppendLine($"\n   ... e mais {sucessos.Count - mostrar} clientes cadastrados\n");
                }
                else
                {
                    sb.AppendLine();
                }
            }

            // FALHAS
            var falhas = Resultados.Where(r => !r.Sucesso).ToList();
            if (falhas.Count > 0)
            {
                sb.AppendLine("❌ FALHAS NO CADASTRO:");
                sb.AppendLine("─────────────────────────────────────────────────────────────────────");

                int mostrar = Math.Min(30, falhas.Count);
                for (int i = 0; i < mostrar; i++)
                {
                    var r = falhas[i];
                    sb.AppendLine($"   {i + 1,3}. Nome: {r.Nome ?? "NÃO ENCONTRADO"}");
                    sb.AppendLine($"        CPF: {(string.IsNullOrEmpty(r.CPF) ? "NÃO ENCONTRADO" : FormatarCPF(r.CPF))}");

                    if (r.CamposFaltantes.Count > 0)
                    {
                        sb.AppendLine($"        ⚠️  Campos faltantes: {string.Join(", ", r.CamposFaltantes)}");
                    }

                    if (!string.IsNullOrEmpty(r.MensagemErro))
                    {
                        sb.AppendLine($"        💬 Erro: {r.MensagemErro}");
                    }

                    sb.AppendLine();
                }

                if (falhas.Count > mostrar)
                {
                    sb.AppendLine($"   ... e mais {falhas.Count - mostrar} falhas\n");
                }
            }

            sb.AppendLine("═══════════════════════════════════════════════════════════════════");
            sb.AppendLine($"Processamento concluído em: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            sb.AppendLine("═══════════════════════════════════════════════════════════════════");

            return sb.ToString();
        }

        private string FormatarCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) return "";
            cpf = cpf.Replace("-", "").Replace(".", "").Trim();

            if (cpf.Length == 10)
                cpf = "0" + cpf;

            if (cpf.Length == 11)
                return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";

            return cpf;
        }
    }

    /// <summary>
    /// Importador de clientes em lote a partir de planilhas
    /// </summary>
    public class ImportadorClientesLote
    {
        private ClienteDAL clienteDAL;
        private ClienteAnexoDAL anexoDAL;

        // Mapeamento de colunas obrigatórias
        private readonly Dictionary<string, string[]> camposObrigatorios = new Dictionary<string, string[]>
        {
            { "Nome", new[] { "Nome", "NomeCompleto", "Cliente", "NomeCliente" } },
            { "CPF", new[] { "CPF", "Cpf", "DocumentoCPF" } },
            { "DataNascimento", new[] { "DataNascimento", "Nascimento", "DataNasc", "DtNascimento" } },
            { "Endereco", new[] { "Endereco", "Endereço", "Rua", "Logradouro" } },
            { "Cidade", new[] { "Cidade", "Municipio", "Município" } },
            { "CEP", new[] { "CEP", "Cep", "CodigoPostal" } },
            { "BeneficioINSS", new[] { "Beneficio", "BeneficioINSS", "INSS", "NumeroINSS", "NB" } }
        };

        // Campos opcionais
        private readonly Dictionary<string, string[]> camposOpcionais = new Dictionary<string, string[]>
        {
            { "Telefone", new[] { "Telefone", "Fone", "Celular", "telefone_1", "Tel" } },
            { "BeneficioINSS2", new[] { "Beneficio2", "SegundoBeneficio", "OutroBeneficio" } }
        };

        public ImportadorClientesLote()
        {
            clienteDAL = new ClienteDAL();
            anexoDAL = new ClienteAnexoDAL();
        }

        /// <summary>
        /// Importa clientes de arquivo CSV
        /// </summary>
        public ResultadoImportacaoLote ImportarCSV(string caminhoArquivo)
        {
            ResultadoImportacaoLote resultado = new ResultadoImportacaoLote();

            try
            {
                // Ler todas as linhas
                string[] linhas = File.ReadAllLines(caminhoArquivo, Encoding.GetEncoding("ISO-8859-1"));

                if (linhas.Length == 0)
                {
                    throw new Exception("Arquivo vazio!");
                }

                // Detectar separador
                string separador = linhas[0].Contains(";") ? ";" : ",";

                // Primeira linha = cabeçalho
                string[] colunas = linhas[0].Split(new[] { separador }, StringSplitOptions.None);

                // Processar cada linha (pular cabeçalho)
                for (int i = 1; i < linhas.Length; i++)
                {
                    string linha = linhas[i].Trim();
                    if (string.IsNullOrWhiteSpace(linha)) continue;

                    resultado.TotalLinhas++;

                    try
                    {
                        ResultadoCadastroCliente resultadoCliente = ProcessarLinhaCSV(linha, colunas, separador);
                        resultado.Resultados.Add(resultadoCliente);

                        if (resultadoCliente.Sucesso)
                        {
                            resultado.Sucessos++;
                        }
                        else
                        {
                            if (resultadoCliente.MensagemErro != null &&
                                resultadoCliente.MensagemErro.Contains("já cadastrado"))
                            {
                                resultado.CPFsDuplicados++;
                            }
                            resultado.Falhas++;
                        }
                    }
                    catch (Exception ex)
                    {
                        resultado.Falhas++;
                        resultado.Resultados.Add(new ResultadoCadastroCliente
                        {
                            Sucesso = false,
                            MensagemErro = $"Erro ao processar linha {i}: {ex.Message}"
                        });
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao importar CSV: {ex.Message}");
            }
        }

        /// <summary>
        /// Processa uma linha do CSV e tenta cadastrar o cliente
        /// </summary>
        private ResultadoCadastroCliente ProcessarLinhaCSV(string linha, string[] nomeColunas, string separador)
        {
            ResultadoCadastroCliente resultado = new ResultadoCadastroCliente();

            try
            {
                // Dividir valores
                string[] valores = linha.Split(new[] { separador }, StringSplitOptions.None);

                // Criar dicionário de dados
                Dictionary<string, string> dadosLinha = new Dictionary<string, string>();
                for (int i = 0; i < Math.Min(valores.Length, nomeColunas.Length); i++)
                {
                    dadosLinha[nomeColunas[i].Trim()] = valores[i].Trim();
                }

                // Extrair campos obrigatórios
                string nome = ExtrairCampo(dadosLinha, camposObrigatorios["Nome"]);
                string cpf = ExtrairCampo(dadosLinha, camposObrigatorios["CPF"]);
                string dataNascStr = ExtrairCampo(dadosLinha, camposObrigatorios["DataNascimento"]);
                string endereco = ExtrairCampo(dadosLinha, camposObrigatorios["Endereco"]);
                string cidade = ExtrairCampo(dadosLinha, camposObrigatorios["Cidade"]);
                string cep = ExtrairCampo(dadosLinha, camposObrigatorios["CEP"]);
                string inss = ExtrairCampo(dadosLinha, camposObrigatorios["BeneficioINSS"]);

                resultado.Nome = nome;
                resultado.CPF = LimparCPF(cpf);

                // Validar campos obrigatórios
                if (string.IsNullOrWhiteSpace(nome))
                    resultado.CamposFaltantes.Add("Nome");

                if (string.IsNullOrWhiteSpace(cpf))
                    resultado.CamposFaltantes.Add("CPF");

                if (string.IsNullOrWhiteSpace(dataNascStr))
                    resultado.CamposFaltantes.Add("DataNascimento");

                if (string.IsNullOrWhiteSpace(endereco))
                    resultado.CamposFaltantes.Add("Endereco");

                if (string.IsNullOrWhiteSpace(cidade))
                    resultado.CamposFaltantes.Add("Cidade");

                if (string.IsNullOrWhiteSpace(cep))
                    resultado.CamposFaltantes.Add("CEP");

                if (string.IsNullOrWhiteSpace(inss))
                    resultado.CamposFaltantes.Add("BeneficioINSS");

                // Se faltam campos, não cadastrar
                if (resultado.CamposFaltantes.Count > 0)
                {
                    resultado.Sucesso = false;
                    resultado.MensagemErro = "Campos obrigatórios faltando";
                    return resultado;
                }

                // Limpar e validar CPF
                string cpfLimpo = LimparCPF(cpf);
                if (cpfLimpo.Length != 11)
                {
                    resultado.Sucesso = false;
                    resultado.MensagemErro = "CPF inválido (deve ter 11 dígitos)";
                    return resultado;
                }

                // Verificar se CPF já existe
                Cliente clienteExistente = clienteDAL.ConsultarPorCPF(cpfLimpo);
                if (clienteExistente != null)
                {
                    resultado.Sucesso = false;
                    resultado.MensagemErro = "CPF já cadastrado no sistema";
                    return resultado;
                }

                // Converter data de nascimento
                DateTime dataNasc;
                if (!TentarConverterData(dataNascStr, out dataNasc))
                {
                    resultado.Sucesso = false;
                    resultado.MensagemErro = "Data de nascimento inválida";
                    return resultado;
                }

                // Limpar CEP
                string cepLimpo = LimparCEP(cep);
                if (cepLimpo.Length != 8)
                {
                    resultado.Sucesso = false;
                    resultado.MensagemErro = "CEP inválido (deve ter 8 dígitos)";
                    return resultado;
                }

                // Limpar INSS
                string inssLimpo = LimparNumeros(inss);
                if (inssLimpo.Length != 10)
                {
                    resultado.Sucesso = false;
                    resultado.MensagemErro = "Benefício INSS inválido (deve ter 10 dígitos)";
                    return resultado;
                }

                // Extrair campos opcionais
                string telefone = ExtrairCampo(dadosLinha, camposOpcionais["Telefone"]);
                string inss2 = ExtrairCampo(dadosLinha, camposOpcionais["BeneficioINSS2"]);

                string telefoneLimpo = null;
                if (!string.IsNullOrWhiteSpace(telefone))
                {
                    telefoneLimpo = LimparNumeros(telefone);
                    if (telefoneLimpo.Length < 10 || telefoneLimpo.Length > 11)
                        telefoneLimpo = null; // Ignorar telefone inválido
                }

                string inss2Limpo = null;
                if (!string.IsNullOrWhiteSpace(inss2))
                {
                    inss2Limpo = LimparNumeros(inss2);
                    if (inss2Limpo.Length != 10)
                        inss2Limpo = null; // Ignorar 2º INSS inválido
                }

                // Criar objeto Cliente
                Cliente novoCliente = new Cliente
                {
                    NomeCompleto = nome,
                    CPF = cpfLimpo,
                    DataNascimento = dataNasc,
                    Endereco = endereco,
                    Cidade = cidade,
                    CEP = cepLimpo,
                    BeneficioINSS = inssLimpo,
                    Telefone = telefoneLimpo,
                    BeneficioINSS2 = inss2Limpo,
                    Ativo = true
                };

                // Cadastrar cliente
                if (clienteDAL.InserirCliente(novoCliente))
                {
                    // Obter ID do cliente recém-cadastrado
                    Cliente clienteCadastrado = clienteDAL.ConsultarPorCPF(cpfLimpo);
                    resultado.ClienteID = clienteCadastrado?.ClienteID;

                    // Identificar dados excedentes
                    HashSet<string> camposUsados = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                    // Adicionar todos os nomes possíveis dos campos usados
                    foreach (var campo in camposObrigatorios.Values)
                        foreach (var Nome in campo)
                            camposUsados.Add(nome);

                    foreach (var campo in camposOpcionais.Values)
                        foreach (var Nome in campo)
                            camposUsados.Add(nome);

                    // Coletar dados excedentes
                    foreach (var kvp in dadosLinha)
                    {
                        if (!camposUsados.Contains(kvp.Key) && !string.IsNullOrWhiteSpace(kvp.Value))
                        {
                            resultado.DadosExcedentes[kvp.Key] = kvp.Value;
                        }
                    }

                    // Salvar dados excedentes como anexo se houver
                    if (resultado.DadosExcedentes.Count > 0 && resultado.ClienteID.HasValue)
                    {
                        SalvarDadosExcedentesComoAnexo(resultado.ClienteID.Value, resultado.DadosExcedentes, nome);
                    }

                    resultado.Sucesso = true;
                }
                else
                {
                    resultado.Sucesso = false;
                    resultado.MensagemErro = "Falha ao inserir no banco de dados";
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Sucesso = false;
                resultado.MensagemErro = ex.Message;
                return resultado;
            }
        }

        /// <summary>
        /// Salva dados excedentes como arquivo anexo do cliente
        /// </summary>
        private void SalvarDadosExcedentesComoAnexo(int clienteID, Dictionary<string, string> dadosExcedentes, string nomeCliente)
        {
            try
            {
                // Criar conteúdo do arquivo
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("═══════════════════════════════════════════════════════");
                sb.AppendLine("      DADOS ADICIONAIS DA IMPORTAÇÃO");
                sb.AppendLine($"      Cliente: {nomeCliente}");
                sb.AppendLine($"      Data: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                sb.AppendLine("═══════════════════════════════════════════════════════");
                sb.AppendLine();

                foreach (var kvp in dadosExcedentes.OrderBy(x => x.Key))
                {
                    sb.AppendLine($"{kvp.Key}: {kvp.Value}");
                }

                sb.AppendLine();
                sb.AppendLine("═══════════════════════════════════════════════════════");
                sb.AppendLine("Estes dados foram importados da planilha mas não fazem");
                sb.AppendLine("parte dos campos principais de cadastro do cliente.");
                sb.AppendLine("═══════════════════════════════════════════════════════");

                // Criar arquivo temporário
                string arquivoTemp = Path.Combine(Path.GetTempPath(),
                    $"DadosAdicionais_{DateTime.Now:yyyyMMddHHmmss}.txt");

                File.WriteAllText(arquivoTemp, sb.ToString(), Encoding.UTF8);

                // Criar anexo
                ClienteAnexo anexo = new ClienteAnexo
                {
                    ClienteID = clienteID,
                    NomeOriginal = "Dados Adicionais da Importação.txt",
                    Descricao = "Informações extras importadas da planilha",
                    UploadPor = "Sistema - Importação Automática"
                };

                anexoDAL.InserirAnexo(anexo, arquivoTemp);

                // Limpar arquivo temporário
                try { File.Delete(arquivoTemp); } catch { }
            }
            catch (Exception ex)
            {
                // Não falhar cadastro se anexo der erro
                System.Diagnostics.Debug.WriteLine($"Erro ao salvar anexo: {ex.Message}");
            }
        }

        /// <summary>
        /// Extrai valor de um campo tentando vários nomes possíveis
        /// </summary>
        private string ExtrairCampo(Dictionary<string, string> dados, string[] nomespossiveis)
        {
            foreach (string nome in nomespossiveis)
            {
                var kvp = dados.FirstOrDefault(x =>
                    x.Key.Equals(nome, StringComparison.OrdinalIgnoreCase));

                if (!string.IsNullOrWhiteSpace(kvp.Value))
                    return kvp.Value;
            }
            return null;
        }

        /// <summary>
        /// Limpa CPF removendo formatação
        /// </summary>
        private string LimparCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return "";

            string limpo = new string(cpf.Where(char.IsDigit).ToArray());

            // Completar com zero à esquerda se tiver 10 dígitos
            if (limpo.Length == 10)
                limpo = "0" + limpo;

            return limpo;
        }

        /// <summary>
        /// Limpa CEP removendo formatação
        /// </summary>
        private string LimparCEP(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep)) return "";
            return new string(cep.Where(char.IsDigit).ToArray());
        }

        /// <summary>
        /// Remove tudo exceto números
        /// </summary>
        private string LimparNumeros(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return "";
            return new string(texto.Where(char.IsDigit).ToArray());
        }

        /// <summary>
        /// Tenta converter string para data
        /// Aceita formatos: dd/MM/yyyy, yyyy-MM-dd, dd-MM-yyyy
        /// </summary>
        private bool TentarConverterData(string dataStr, out DateTime data)
        {
            data = DateTime.MinValue;

            if (string.IsNullOrWhiteSpace(dataStr))
                return false;

            // Tentar formatos comuns
            string[] formatos = {
                "dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy", "d-M-yyyy",
                "yyyy-MM-dd", "yyyy/MM/dd",
                "dd/MM/yy", "d/M/yy"
            };

            foreach (string formato in formatos)
            {
                if (DateTime.TryParseExact(dataStr, formato,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out data))
                {
                    // Validar idade razoável (18-120 anos)
                    int idade = DateTime.Now.Year - data.Year;
                    if (idade >= 18 && idade <= 120)
                        return true;
                }
            }

            return false;
        }
    }
}