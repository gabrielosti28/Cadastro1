// =============================================
// IMPORTADOR DE PASTAS DE CLIENTES EM LOTE
// Arquivo: ImportadorPastas.cs
// FUNÇÃO: Varre D:\A 1 ACLIENTES\AAA CLIENTES
//         Cadastra cada pasta como cliente
//         Importa todos os arquivos como anexos
// =============================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cadastro1
{
    // =============================================
    // RESULTADO DE CADA PASTA PROCESSADA
    // =============================================
    public class ResultadoPasta
    {
        public string NomePasta { get; set; }
        public string CaminhoCompleto { get; set; }
        public bool ClienteCadastrado { get; set; }
        public int? ClienteID { get; set; }
        public int ArquivosImportados { get; set; }
        public int ArquivosIgnorados { get; set; }
        public List<string> ArquivosComErro { get; set; }
        public string MensagemErro { get; set; }
        public bool Sucesso => ClienteCadastrado && string.IsNullOrEmpty(MensagemErro);

        public ResultadoPasta()
        {
            ArquivosComErro = new List<string>();
        }
    }

    // =============================================
    // RESULTADO GERAL DA IMPORTAÇÃO
    // =============================================
    public class ResultadoImportacaoPastas
    {
        public int TotalPastas { get; set; }
        public int PastasSucesso { get; set; }
        public int PastasFalha { get; set; }
        public int PastasJaCadastradas { get; set; }
        public int TotalArquivosImportados { get; set; }
        public List<ResultadoPasta> Resultados { get; set; }

        public ResultadoImportacaoPastas()
        {
            Resultados = new List<ResultadoPasta>();
        }

        public string GerarRelatorio()
        {
            var sb = new StringBuilder();
            sb.AppendLine("╔══════════════════════════════════════════════════════════════╗");
            sb.AppendLine("║     📁 RELATÓRIO DE IMPORTAÇÃO DE PASTAS DE CLIENTES        ║");
            sb.AppendLine("╚══════════════════════════════════════════════════════════════╝\n");

            sb.AppendLine("📈 RESUMO GERAL:");
            sb.AppendLine("────────────────────────────────────────────────────────────");
            sb.AppendLine($"   Total de pastas encontradas:     {TotalPastas}");
            sb.AppendLine($"   ✅ Clientes cadastrados:          {PastasSucesso}");
            sb.AppendLine($"   🔄 Já estavam cadastrados:        {PastasJaCadastradas}");
            sb.AppendLine($"   ❌ Falhas:                        {PastasFalha}");
            sb.AppendLine($"   📎 Total de arquivos importados:  {TotalArquivosImportados}\n");

            var sucessos = Resultados.Where(r => r.Sucesso).ToList();
            if (sucessos.Any())
            {
                sb.AppendLine("✅ CLIENTES CADASTRADOS COM SUCESSO:");
                sb.AppendLine("────────────────────────────────────────────────────────────");
                foreach (var r in sucessos.Take(30))
                {
                    sb.AppendLine($"   ✓ {r.NomePasta.PadRight(40)} | {r.ArquivosImportados} arquivo(s)");
                }
                if (sucessos.Count > 30)
                    sb.AppendLine($"   ... e mais {sucessos.Count - 30} clientes\n");
                else
                    sb.AppendLine();
            }

            var falhas = Resultados.Where(r => !r.Sucesso).ToList();
            if (falhas.Any())
            {
                sb.AppendLine("❌ FALHAS:");
                sb.AppendLine("────────────────────────────────────────────────────────────");
                foreach (var r in falhas)
                {
                    sb.AppendLine($"   ✗ {r.NomePasta}");
                    sb.AppendLine($"     Erro: {r.MensagemErro}");
                }
            }

            sb.AppendLine("\n⚠️  IMPORTANTE: Clientes foram cadastrados com dados placeholder.");
            sb.AppendLine("   Acesse cada cliente e complete: CPF, Data Nascimento,");
            sb.AppendLine("   Endereço, CEP e Benefício INSS.");
            sb.AppendLine("\n════════════════════════════════════════════════════════════");
            sb.AppendLine($"Processado em: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            sb.AppendLine("════════════════════════════════════════════════════════════");

            return sb.ToString();
        }
    }

    // =============================================
    // IMPORTADOR PRINCIPAL
    // =============================================
    public class ImportadorPastas
    {
        private ClienteDAL clienteDAL;
        private ClienteAnexoDAL anexoDAL;

        // Extensões de arquivo permitidas pelo sistema
        private static readonly string[] ExtensoesPermitidas = {
            ".pdf", ".doc", ".docx", ".txt", ".jpg", ".jpeg",
            ".png", ".gif", ".bmp", ".xls", ".xlsx", ".csv"
        };

        // Tamanho máximo: 50MB por arquivo
        private const long TamanhoMaximoBytes = 50L * 1024 * 1024;

        public ImportadorPastas()
        {
            clienteDAL = new ClienteDAL();
            anexoDAL = new ClienteAnexoDAL();
        }

        // =============================================
        // MÉTODO PRINCIPAL - IMPORTAR TODAS AS PASTAS
        // =============================================
        public ResultadoImportacaoPastas ImportarPastas(
            string diretorioRaiz,
            Action<int, int, string> progressoCallback = null)
        {
            var resultado = new ResultadoImportacaoPastas();

            if (!Directory.Exists(diretorioRaiz))
                throw new Exception($"Diretório não encontrado:\n{diretorioRaiz}");

            // Listar todas as subpastas (cada uma é um cliente)
            string[] pastas = Directory.GetDirectories(diretorioRaiz)
                                       .OrderBy(p => p)
                                       .ToArray();

            resultado.TotalPastas = pastas.Length;

            for (int i = 0; i < pastas.Length; i++)
            {
                string caminhoPasta = pastas[i];
                string nomePasta = Path.GetFileName(caminhoPasta);

                // Reportar progresso
                progressoCallback?.Invoke(i + 1, pastas.Length, nomePasta);

                var resultadoPasta = ProcessarPasta(caminhoPasta, nomePasta);
                resultado.Resultados.Add(resultadoPasta);

                if (resultadoPasta.Sucesso)
                    resultado.PastasSucesso++;
                else if (resultadoPasta.MensagemErro?.Contains("já cadastrado") == true)
                    resultado.PastasJaCadastradas++;
                else
                    resultado.PastasFalha++;

                resultado.TotalArquivosImportados += resultadoPasta.ArquivosImportados;
            }

            return resultado;
        }

        // =============================================
        // PROCESSAR UMA PASTA (UM CLIENTE)
        // =============================================
        private ResultadoPasta ProcessarPasta(string caminhoPasta, string nomePasta)
        {
            var resultado = new ResultadoPasta
            {
                NomePasta = nomePasta,
                CaminhoCompleto = caminhoPasta
            };

            try
            {
                // Limpar o nome da pasta para usar como nome do cliente
                // Remove números extras no final (ex: "ADRIANA DA SILVA 2462566" → "ADRIANA DA SILVA")
                string nomeCliente = LimparNomeCliente(nomePasta);

                // Verificar se já existe cliente com esse nome exato
                // (verificação por nome já que não temos CPF ainda)
                Cliente clienteExistente = BuscarClientePorNome(nomeCliente);

                if (clienteExistente != null)
                {
                    resultado.MensagemErro = "Cliente já cadastrado no sistema";
                    resultado.ClienteID = clienteExistente.ClienteID;

                    // Mesmo já existindo, importar os arquivos se ainda não foram importados
                    ImportarArquivosCliente(caminhoPasta, clienteExistente.ClienteID, resultado);
                    return resultado;
                }

                // Criar novo cliente com dados placeholder
                // O operador irá completar os dados depois
                Cliente novoCliente = new Cliente
                {
                    NomeCompleto = nomeCliente,
                    CPF = GerarCPFPlaceholder(), // CPF placeholder único
                    DataNascimento = new DateTime(1900, 1, 1),
                    Endereco = "ENDEREÇO NÃO INFORMADO - ATUALIZAR",
                    Cidade = "CIDADE NÃO INFORMADA - ATUALIZAR",
                    CEP = "99999999",
                    Telefone = null,
                    BeneficioINSS = "9999999999",
                    BeneficioINSS2 = null,
                    Ativo = true
                };

                // Cadastrar no banco
                bool cadastrado = clienteDAL.InserirCliente(novoCliente);

                if (!cadastrado)
                {
                    resultado.MensagemErro = "Falha ao inserir cliente no banco";
                    return resultado;
                }

                resultado.ClienteCadastrado = true;

                // Buscar o cliente recém-cadastrado para obter o ClienteID
                Cliente clienteCadastrado = BuscarClientePorNome(nomeCliente);

                if (clienteCadastrado == null)
                {
                    resultado.MensagemErro = "Cliente cadastrado mas não foi possível recuperar o ID";
                    return resultado;
                }

                resultado.ClienteID = clienteCadastrado.ClienteID;

                // Importar todos os arquivos da pasta como anexos
                ImportarArquivosCliente(caminhoPasta, clienteCadastrado.ClienteID, resultado);

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.MensagemErro = ex.Message;
                return resultado;
            }
        }

        // =============================================
        // IMPORTAR ARQUIVOS DE UMA PASTA COMO ANEXOS
        // =============================================
        private void ImportarArquivosCliente(
            string caminhoPasta,
            int clienteID,
            ResultadoPasta resultado)
        {
            try
            {
                // Listar todos os arquivos da pasta (não subpastas)
                string[] arquivos = Directory.GetFiles(caminhoPasta);

                foreach (string caminhoArquivo in arquivos)
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(caminhoArquivo);
                        string extensao = fileInfo.Extension.ToLowerInvariant();

                        // Verificar extensão permitida
                        if (!ExtensoesPermitidas.Contains(extensao))
                        {
                            resultado.ArquivosIgnorados++;
                            continue;
                        }

                        // Verificar tamanho
                        if (fileInfo.Length > TamanhoMaximoBytes)
                        {
                            resultado.ArquivosIgnorados++;
                            resultado.ArquivosComErro.Add(
                                $"{fileInfo.Name} (muito grande: {fileInfo.Length / 1024 / 1024}MB)");
                            continue;
                        }

                        // Determinar descrição baseada no nome do arquivo
                        string descricao = GerarDescricaoArquivo(fileInfo.Name);

                        // Criar objeto de anexo
                        ClienteAnexo anexo = new ClienteAnexo
                        {
                            ClienteID = clienteID,
                            NomeOriginal = fileInfo.Name,
                            Descricao = descricao,
                            UploadPor = "Sistema - Importação em Lote de Pastas"
                        };

                        // Inserir o anexo (copia o arquivo para pasta de anexos do sistema)
                        anexoDAL.InserirAnexo(anexo, caminhoArquivo);
                        resultado.ArquivosImportados++;
                    }
                    catch (Exception exArquivo)
                    {
                        resultado.ArquivosIgnorados++;
                        resultado.ArquivosComErro.Add(
                            $"{Path.GetFileName(caminhoArquivo)}: {exArquivo.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.ArquivosComErro.Add($"Erro ao listar arquivos: {ex.Message}");
            }
        }

        // =============================================
        // BUSCAR CLIENTE POR NOME (sem CPF disponível)
        // =============================================
        private Cliente BuscarClientePorNome(string nome)
        {
            try
            {
                var clientes = clienteDAL.BuscarClientesPorFiltro(nome, 10);
                return clientes.FirstOrDefault(c =>
                    c.NomeCompleto.Equals(nome, StringComparison.OrdinalIgnoreCase));
            }
            catch
            {
                return null;
            }
        }

        // =============================================
        // LIMPAR NOME DA PASTA
        // Remove números extras no final do nome
        // Ex: "ADRIANA DA SILVA 2462566" → "ADRIANA DA SILVA"
        // =============================================
        private string LimparNomeCliente(string nomePasta)
        {
            if (string.IsNullOrWhiteSpace(nomePasta))
                return nomePasta;

            string nome = nomePasta.Trim();

            // Remover números no final (ex: "NOME 1234567")
            // Mas preservar casos como "JOSE 2" (pode ser parte do nome)
            var partes = nome.Split(' ');

            // Verificar se a última parte é só números e tem mais de 4 dígitos
            if (partes.Length > 1)
            {
                string ultimaParte = partes.Last();
                if (ultimaParte.All(char.IsDigit) && ultimaParte.Length > 4)
                {
                    nome = string.Join(" ", partes.Take(partes.Length - 1));
                }
            }

            return nome.Trim();
        }

        // =============================================
        // GERAR CPF PLACEHOLDER ÚNICO
        // Será substituído quando operador atualizar o cliente
        // =============================================
        private static int _contadorCPF = 0;
        private string GerarCPFPlaceholder()
        {
            _contadorCPF++;
            // Formato: 00000XXXXX onde XXXXX é um número sequencial
            // Isso evita duplicação entre placeholders
            string timestamp = DateTime.Now.ToString("MMddHHmmss");
            return $"00000{_contadorCPF:D5}0"; // 11 dígitos
        }

        // =============================================
        // GERAR DESCRIÇÃO AMIGÁVEL BASEADA NO NOME DO ARQUIVO
        // =============================================
        private string GerarDescricaoArquivo(string nomeArquivo)
        {
            string nomeLower = nomeArquivo.ToLower();

            if (nomeLower.Contains("rg") || nomeLower.Contains("identidade"))
                return "📋 Documento de Identidade (RG)";

            if (nomeLower.Contains("cpf"))
                return "🆔 CPF";

            if (nomeLower.Contains("extrato"))
                return "📊 Extrato Bancário / INSS";

            if (nomeLower.Contains("historico") || nomeLower.Contains("histórico"))
                return "📄 Histórico de Créditos";

            if (nomeLower.Contains("ccb"))
                return "📃 CCB - Cédula de Crédito Bancário";

            if (nomeLower.Contains("pro") || nomeLower.Contains("proposta"))
                return "📝 Proposta";

            if (nomeLower.Contains("selfie") || nomeLower.Contains("foto"))
                return "📷 Foto do Cliente";

            if (nomeLower.Contains("end") || nomeLower.Contains("comprovante"))
                return "🏠 Comprovante de Endereço";

            if (nomeLower.Contains("contra") || nomeLower.Contains("holerite"))
                return "💼 Contracheque / Holerite";

            // Nome parece ser número (ex: 37858357.pdf = número do benefício)
            string semExtensao = Path.GetFileNameWithoutExtension(nomeArquivo);
            if (semExtensao.All(char.IsDigit) && semExtensao.Length >= 6)
                return $"📄 Documento nº {semExtensao} (verificar tipo)";

            return "📎 Documento importado automaticamente";
        }

        // =============================================
        // LISTAR PASTAS DISPONÍVEIS (para preview)
        // =============================================
        public List<string> ListarPastas(string diretorioRaiz)
        {
            if (!Directory.Exists(diretorioRaiz))
                return new List<string>();

            return Directory.GetDirectories(diretorioRaiz)
                           .Select(p => Path.GetFileName(p))
                           .OrderBy(n => n)
                           .ToList();
        }

        // =============================================
        // CONTAR ARQUIVOS EM UMA PASTA
        // =============================================
        public int ContarArquivosPermitidos(string caminhoPasta)
        {
            if (!Directory.Exists(caminhoPasta)) return 0;

            return Directory.GetFiles(caminhoPasta)
                           .Count(f => ExtensoesPermitidas.Contains(
                               Path.GetExtension(f).ToLowerInvariant()));
        }
    }
}