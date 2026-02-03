// =============================================
// IMPORTADOR DE CLIENTES EM LOTE - VERSÃO FINAL
// Arquivo: ImportadorClientesLote.cs
// ATUALIZAÇÕES:
// - Aceita CPFs, CEPs, INSS com qualquer quantidade de dígitos
// - Preenche automaticamente campos vazios com placeholders
// - Apenas NOME e CPF são obrigatórios
// =============================================
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Cadastro1
{
    public class ResultadoCadastroCliente
    {
        public bool Sucesso { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string MensagemErro { get; set; }
        public List<string> CamposFaltantes { get; set; }
        public Dictionary<string, string> DadosExcedentes { get; set; }
        public int? ClienteID { get; set; }
        public List<string> CamposPreenchidosAutomaticamente { get; set; }

        public ResultadoCadastroCliente()
        {
            CamposFaltantes = new List<string>();
            DadosExcedentes = new Dictionary<string, string>();
            CamposPreenchidosAutomaticamente = new List<string>();
        }
    }

    public class ResultadoImportacaoLote
    {
        public int TotalLinhas { get; set; }
        public int Sucessos { get; set; }
        public int Falhas { get; set; }
        public int CPFsDuplicados { get; set; }
        public int CamposPreenchidosAuto { get; set; }
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
            sb.AppendLine($"   🔄 CPFs já existentes (pulados): {CPFsDuplicados}");
            sb.AppendLine($"   ⚠️  Campos preenchidos auto:     {CamposPreenchidosAuto}\n");

            double taxaSucesso = TotalLinhas > 0 ? (double)Sucessos / TotalLinhas * 100 : 0;
            sb.AppendLine($"📊 Taxa de sucesso: {taxaSucesso:F1}%\n");

            if (CamposPreenchidosAuto > 0)
            {
                sb.AppendLine("⚠️  ATENÇÃO: DADOS PREENCHIDOS AUTOMATICAMENTE");
                sb.AppendLine("─────────────────────────────────────────────────────────────────────");
                sb.AppendLine("   Alguns clientes foram cadastrados com dados placeholder:");
                sb.AppendLine("   • CEP: 99999-999 (quando não informado)");
                sb.AppendLine("   • Endereço: 'ENDEREÇO NÃO INFORMADO - ATUALIZAR'");
                sb.AppendLine("   • Cidade: 'CIDADE NÃO INFORMADA - ATUALIZAR'");
                sb.AppendLine("   • INSS: 9999999999 (quando não informado)");
                sb.AppendLine("   • Data Nascimento: 01/01/1900 (quando não informada)");
                sb.AppendLine();
                sb.AppendLine("   🔧 IMPORTANTE: Atualize estes dados posteriormente!\n");
            }

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

                    if (r.CamposPreenchidosAutomaticamente.Count > 0)
                    {
                        sb.AppendLine($"        ⚠️  Campos auto: {string.Join(", ", r.CamposPreenchidosAutomaticamente)}");
                    }

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

    public class ImportadorClientesLote
    {
        private ClienteDAL clienteDAL;
        private ClienteAnexoDAL anexoDAL;

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

        public ResultadoImportacaoLote ImportarCSV(string caminhoArquivo)
        {
            ResultadoImportacaoLote resultado = new ResultadoImportacaoLote();

            try
            {
                string[] linhas = File.ReadAllLines(caminhoArquivo, Encoding.GetEncoding("ISO-8859-1"));

                if (linhas.Length == 0)
                    throw new Exception("Arquivo vazio!");

                string separador = linhas[0].Contains(";") ? ";" : ",";
                string[] colunas = linhas[0].Split(new[] { separador }, StringSplitOptions.None);

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
                            if (resultadoCliente.CamposPreenchidosAutomaticamente.Count > 0)
                                resultado.CamposPreenchidosAuto++;
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

        private ResultadoCadastroCliente ProcessarLinhaCSV(string linha, string[] nomeColunas, string separador)
        {
            ResultadoCadastroCliente resultado = new ResultadoCadastroCliente();

            try
            {
                string[] valores = linha.Split(new[] { separador }, StringSplitOptions.None);

                Dictionary<string, string> dadosLinha = new Dictionary<string, string>();
                for (int i = 0; i < Math.Min(valores.Length, nomeColunas.Length); i++)
                {
                    dadosLinha[nomeColunas[i].Trim()] = valores[i].Trim();
                }

                // Extrair campos
                string nome = ExtrairCampo(dadosLinha, camposObrigatorios["Nome"]);
                string cpf = ExtrairCampo(dadosLinha, camposObrigatorios["CPF"]);
                string dataNascStr = ExtrairCampo(dadosLinha, camposObrigatorios["DataNascimento"]);
                string endereco = ExtrairCampo(dadosLinha, camposObrigatorios["Endereco"]);
                string cidade = ExtrairCampo(dadosLinha, camposObrigatorios["Cidade"]);
                string cep = ExtrairCampo(dadosLinha, camposObrigatorios["CEP"]);
                string inss = ExtrairCampo(dadosLinha, camposObrigatorios["BeneficioINSS"]);

                // =====================================================
                // SISTEMA DE PREENCHIMENTO AUTOMÁTICO
                // =====================================================

                // CEP - Se vazio, preencher com 99999999
                if (string.IsNullOrWhiteSpace(cep))
                {
                    cep = "99999999";
                    resultado.CamposPreenchidosAutomaticamente.Add("CEP");
                }

                // ENDEREÇO - Se vazio, preencher com texto identificável
                if (string.IsNullOrWhiteSpace(endereco))
                {
                    endereco = "ENDEREÇO NÃO INFORMADO - ATUALIZAR";
                    resultado.CamposPreenchidosAutomaticamente.Add("Endereço");
                }

                // CIDADE - Se vazia, preencher com texto identificável
                if (string.IsNullOrWhiteSpace(cidade))
                {
                    cidade = "CIDADE NÃO INFORMADA - ATUALIZAR";
                    resultado.CamposPreenchidosAutomaticamente.Add("Cidade");
                }

                // BENEFÍCIO INSS - Se vazio, preencher com 9999999999
                if (string.IsNullOrWhiteSpace(inss))
                {
                    inss = "9999999999";
                    resultado.CamposPreenchidosAutomaticamente.Add("INSS");
                }

                // DATA DE NASCIMENTO - Se vazia, usar 01/01/1900
                if (string.IsNullOrWhiteSpace(dataNascStr))
                {
                    dataNascStr = "01/01/1900";
                    resultado.CamposPreenchidosAutomaticamente.Add("Data Nascimento");
                }

                resultado.Nome = nome;

                // =====================================================
                // VALIDAÇÃO DE CPF (Obrigatório - não pode ser vazio)
                // =====================================================
                string cpfLimpo = LimparCPF(cpf);

                if (string.IsNullOrEmpty(cpfLimpo))
                {
                    resultado.CamposFaltantes.Add("CPF");
                    resultado.Sucesso = false;
                    resultado.MensagemErro = "CPF é obrigatório";
                    return resultado;
                }

                if (cpfLimpo.Length < 11)
                    cpfLimpo = cpfLimpo.PadLeft(11, '0');
                else if (cpfLimpo.Length > 11)
                    cpfLimpo = cpfLimpo.Substring(0, 11);

                resultado.CPF = cpfLimpo;

                // =====================================================
                // VALIDAÇÃO DE NOME (Obrigatório - não pode ser vazio)
                // =====================================================
                if (string.IsNullOrWhiteSpace(nome))
                {
                    resultado.CamposFaltantes.Add("Nome");
                    resultado.Sucesso = false;
                    resultado.MensagemErro = "Nome é obrigatório";
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
                    // Se falhar, usar data padrão
                    dataNasc = new DateTime(1900, 1, 1);
                    if (!resultado.CamposPreenchidosAutomaticamente.Contains("Data Nascimento"))
                        resultado.CamposPreenchidosAutomaticamente.Add("Data Nascimento");
                }

                // Limpar e ajustar CEP
                string cepLimpo = LimparCEP(cep);
                if (cepLimpo.Length < 8)
                    cepLimpo = cepLimpo.PadLeft(8, '9');
                else if (cepLimpo.Length > 8)
                    cepLimpo = cepLimpo.Substring(0, 8);

                // Limpar e ajustar INSS
                string inssLimpo = LimparNumeros(inss);
                if (inssLimpo.Length < 10)
                    inssLimpo = inssLimpo.PadLeft(10, '9');
                else if (inssLimpo.Length > 10)
                    inssLimpo = inssLimpo.Substring(0, 10);

                // Extrair campos opcionais
                string telefone = ExtrairCampo(dadosLinha, camposOpcionais["Telefone"]);
                string inss2 = ExtrairCampo(dadosLinha, camposOpcionais["BeneficioINSS2"]);

                string telefoneLimpo = null;
                if (!string.IsNullOrWhiteSpace(telefone))
                {
                    telefoneLimpo = LimparNumeros(telefone);
                    if (telefoneLimpo.Length < 10)
                        telefoneLimpo = telefoneLimpo.PadLeft(10, '0');
                    else if (telefoneLimpo.Length > 11)
                        telefoneLimpo = telefoneLimpo.Substring(0, 11);
                }

                string inss2Limpo = null;
                if (!string.IsNullOrWhiteSpace(inss2))
                {
                    inss2Limpo = LimparNumeros(inss2);
                    if (inss2Limpo.Length < 10)
                        inss2Limpo = inss2Limpo.PadLeft(10, '9');
                    else if (inss2Limpo.Length > 10)
                        inss2Limpo = inss2Limpo.Substring(0, 10);
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
                    Cliente clienteCadastrado = clienteDAL.ConsultarPorCPF(cpfLimpo);
                    resultado.ClienteID = clienteCadastrado?.ClienteID;

                    // Identificar dados excedentes
                    HashSet<string> camposUsados = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                    foreach (var campo in camposObrigatorios.Values)
                        foreach (var n in campo)
                            camposUsados.Add(n);

                    foreach (var campo in camposOpcionais.Values)
                        foreach (var n in campo)
                            camposUsados.Add(n);

                    foreach (var kvp in dadosLinha)
                    {
                        if (!camposUsados.Contains(kvp.Key) && !string.IsNullOrWhiteSpace(kvp.Value))
                        {
                            resultado.DadosExcedentes[kvp.Key] = kvp.Value;
                        }
                    }

                    // Adicionar aviso sobre campos preenchidos automaticamente
                    if (resultado.CamposPreenchidosAutomaticamente.Count > 0)
                    {
                        resultado.DadosExcedentes["⚠️_CAMPOS_AUTO"] =
                            $"ATENÇÃO: {string.Join(", ", resultado.CamposPreenchidosAutomaticamente)} " +
                            "foram preenchidos automaticamente com valores placeholder. ATUALIZE posteriormente!";
                    }

                    // Salvar dados excedentes como anexo
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

        private void SalvarDadosExcedentesComoAnexo(int clienteID, Dictionary<string, string> dadosExcedentes, string nomeCliente)
        {
            try
            {
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

                string arquivoTemp = Path.Combine(Path.GetTempPath(),
                    $"DadosAdicionais_{DateTime.Now:yyyyMMddHHmmss}.txt");

                File.WriteAllText(arquivoTemp, sb.ToString(), Encoding.UTF8);

                ClienteAnexo anexo = new ClienteAnexo
                {
                    ClienteID = clienteID,
                    NomeOriginal = "Dados Adicionais da Importação.txt",
                    Descricao = "Informações extras importadas da planilha + avisos de campos auto",
                    UploadPor = "Sistema - Importação Automática"
                };

                anexoDAL.InserirAnexo(anexo, arquivoTemp);

                try { File.Delete(arquivoTemp); } catch { }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao salvar anexo: {ex.Message}");
            }
        }

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

        private string LimparCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return "";
            return new string(cpf.Where(char.IsDigit).ToArray());
        }

        private string LimparCEP(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep)) return "";
            return new string(cep.Where(char.IsDigit).ToArray());
        }

        private string LimparNumeros(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return "";
            return new string(texto.Where(char.IsDigit).ToArray());
        }

        private bool TentarConverterData(string dataStr, out DateTime data)
        {
            data = DateTime.MinValue;

            if (string.IsNullOrWhiteSpace(dataStr))
                return false;

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
                    int idade = DateTime.Now.Year - data.Year;
                    if (idade >= 18 && idade <= 120)
                        return true;
                }
            }

            return false;
        }
    }
}