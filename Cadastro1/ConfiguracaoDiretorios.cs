// =============================================
// CONFIGURAÇÃO CENTRALIZADA DE DIRETÓRIOS
// Arquivo: ConfiguracaoDiretorios.cs
// Gerencia todos os caminhos de pastas do sistema
// =============================================
using System;
using System.IO;
using Newtonsoft.Json;

namespace Cadastro1
{
    /// <summary>
    /// Classe que gerencia todas as configurações de diretórios do sistema
    /// </summary>
    public class ConfiguracaoDiretorios
    {
        private static ConfiguracaoDiretorios _instancia;
        private static readonly object _lock = new object();

        // ========== PROPRIEDADES DOS DIRETÓRIOS ==========

        /// <summary>
        /// Diretório onde os backups do banco de dados serão salvos
        /// </summary>
        public string DiretorioBackups { get; set; }

        /// <summary>
        /// Diretório onde os anexos dos clientes serão salvos
        /// </summary>
        public string DiretorioAnexos { get; set; }

        /// <summary>
        /// Diretório onde os templates de mala direta serão salvos
        /// </summary>
        public string DiretorioTemplates { get; set; }

        /// <summary>
        /// Diretório onde os PDFs de mala direta serão salvos
        /// </summary>
        public string DiretorioPDFs { get; set; }

        /// <summary>
        /// Diretório onde os logs do sistema serão salvos
        /// </summary>
        public string DiretorioLogs { get; set; }

        /// <summary>
        /// Caminho do arquivo de configuração
        /// </summary>
        private static string CaminhoArquivoConfig
        {
            get
            {
                string pastaApp = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "SistemaCadastroClientes"
                );

                if (!Directory.Exists(pastaApp))
                    Directory.CreateDirectory(pastaApp);

                return Path.Combine(pastaApp, "config_diretorios.json");
            }
        }

        // ========== SINGLETON ==========

        public static ConfiguracaoDiretorios Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (_lock)
                    {
                        if (_instancia == null)
                        {
                            _instancia = Carregar();
                        }
                    }
                }
                return _instancia;
            }
        }

        // ========== CONSTRUTOR PRIVADO ==========

        private ConfiguracaoDiretorios()
        {
            // Valores padrão
            DefinirDiretoriosPadrao();
        }

        // ========== MÉTODOS ==========

        /// <summary>
        /// Define os diretórios padrão (Documentos do usuário)
        /// </summary>
        private void DefinirDiretoriosPadrao()
        {
            string pastaDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string pastaBase = Path.Combine(pastaDocumentos, "SistemaCadastroClientes");

            DiretorioBackups = Path.Combine(pastaBase, "Backups");
            DiretorioAnexos = Path.Combine(pastaBase, "Anexos");
            DiretorioTemplates = Path.Combine(pastaBase, "Templates");
            DiretorioPDFs = Path.Combine(pastaBase, "PDFs");
            DiretorioLogs = Path.Combine(pastaBase, "Logs");
        }

        /// <summary>
        /// Carrega a configuração do arquivo JSON
        /// </summary>
        private static ConfiguracaoDiretorios Carregar()
        {
            try
            {
                if (File.Exists(CaminhoArquivoConfig))
                {
                    string json = File.ReadAllText(CaminhoArquivoConfig);
                    var config = JsonConvert.DeserializeObject<ConfiguracaoDiretorios>(json);

                    // Validar se os diretórios ainda existem
                    if (config.ValidarDiretorios())
                    {
                        return config;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao carregar configuração: {ex.Message}");
            }

            // Se falhou, retornar nova configuração com valores padrão
            return new ConfiguracaoDiretorios();
        }

        /// <summary>
        /// Salva a configuração no arquivo JSON
        /// </summary>
        public void Salvar()
        {
            try
            {
                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(CaminhoArquivoConfig, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao salvar configuração: {ex.Message}");
            }
        }

        /// <summary>
        /// Valida se todos os diretórios existem ou podem ser criados
        /// </summary>
        public bool ValidarDiretorios()
        {
            try
            {
                // Verificar se as pastas pai existem
                return !string.IsNullOrEmpty(DiretorioBackups) &&
                       Directory.Exists(Path.GetDirectoryName(DiretorioBackups)) &&
                       !string.IsNullOrEmpty(DiretorioAnexos) &&
                       Directory.Exists(Path.GetDirectoryName(DiretorioAnexos)) &&
                       !string.IsNullOrEmpty(DiretorioTemplates) &&
                       Directory.Exists(Path.GetDirectoryName(DiretorioTemplates)) &&
                       !string.IsNullOrEmpty(DiretorioPDFs) &&
                       Directory.Exists(Path.GetDirectoryName(DiretorioPDFs)) &&
                       !string.IsNullOrEmpty(DiretorioLogs) &&
                       Directory.Exists(Path.GetDirectoryName(DiretorioLogs));
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Cria todos os diretórios configurados se não existirem
        /// </summary>
        public void CriarDiretorios()
        {
            CriarDiretorioSeNaoExistir(DiretorioBackups);
            CriarDiretorioSeNaoExistir(DiretorioAnexos);
            CriarDiretorioSeNaoExistir(DiretorioTemplates);
            CriarDiretorioSeNaoExistir(DiretorioPDFs);
            CriarDiretorioSeNaoExistir(DiretorioLogs);
        }

        /// <summary>
        /// Cria um diretório se ele não existir
        /// </summary>
        private void CriarDiretorioSeNaoExistir(string caminho)
        {
            try
            {
                if (!string.IsNullOrEmpty(caminho) && !Directory.Exists(caminho))
                {
                    Directory.CreateDirectory(caminho);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar diretório {caminho}: {ex.Message}");
            }
        }

        /// <summary>
        /// Restaura as configurações padrão
        /// </summary>
        public void RestaurarPadrao()
        {
            DefinirDiretoriosPadrao();
            Salvar();
        }

        /// <summary>
        /// Verifica se um diretório tem permissão de escrita
        /// </summary>
        public static bool VerificarPermissaoEscrita(string caminho)
        {
            try
            {
                // Garantir que o diretório existe
                if (!Directory.Exists(caminho))
                {
                    Directory.CreateDirectory(caminho);
                }

                // Tentar criar arquivo de teste
                string arquivoTeste = Path.Combine(caminho, $"test_{Guid.NewGuid()}.tmp");
                File.WriteAllText(arquivoTeste, "teste");
                File.Delete(arquivoTeste);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Obtém um resumo das configurações
        /// </summary>
        public string ObterResumo()
        {
            return $"Backups: {DiretorioBackups}\n" +
                   $"Anexos: {DiretorioAnexos}\n" +
                   $"Templates: {DiretorioTemplates}\n" +
                   $"PDFs: {DiretorioPDFs}\n" +
                   $"Logs: {DiretorioLogs}";
        }
    }
}