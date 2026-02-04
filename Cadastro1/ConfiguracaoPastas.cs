// =============================================
// CLASSE DE CONFIGURAÇÃO DE PASTAS - ATUALIZADA
// Arquivo: ConfiguracaoPastas.cs
// Gerencia centralmente todas as pastas do sistema
// ADICIONADO: PastaLogsSms
// =============================================
using Microsoft.Win32;
using System;
using System.IO;

namespace Cadastro1
{
    public static class ConfiguracaoPastas
    {
        private const string CHAVE_REGISTRO = @"Software\SistemaCadastroClientes";
        private static readonly string PastaPadrao = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "SistemaCadastroClientes");

        // Propriedades para cada tipo de pasta
        public static string PastaBackups
        {
            get => ObterPasta("PastaBackups", Path.Combine(PastaPadrao, "Backups"));
            set => SalvarPasta("PastaBackups", value);
        }

        public static string PastaAnexos
        {
            get => ObterPasta("PastaAnexos", Path.Combine(PastaPadrao, "Anexos"));
            set => SalvarPasta("PastaAnexos", value);
        }

        public static string PastaTemplates
        {
            get => ObterPasta("PastaTemplates", Path.Combine(PastaPadrao, "Templates"));
            set => SalvarPasta("PastaTemplates", value);
        }

        public static string PastaPDFs
        {
            get => ObterPasta("PastaPDFs", Path.Combine(PastaPadrao, "PDFs"));
            set => SalvarPasta("PastaPDFs", value);
        }

        public static string PastaLogs
        {
            get => ObterPasta("PastaLogs", Path.Combine(PastaPadrao, "Logs"));
            set => SalvarPasta("PastaLogs", value);
        }

        // NOVO: Pasta para logs de SMS
        public static string PastaLogsSms
        {
            get => ObterPasta("PastaLogsSms", Path.Combine(PastaPadrao, "LogsSMS"));
            set => SalvarPasta("PastaLogsSms", value);
        }

        // Obter pasta do registro ou usar padrão
        private static string ObterPasta(string nomePasta, string valorPadrao)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(CHAVE_REGISTRO))
                {
                    if (key != null)
                    {
                        string valor = key.GetValue(nomePasta) as string;
                        if (!string.IsNullOrEmpty(valor))
                        {
                            return valor;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Em caso de erro, usar valor padrão
            }

            return valorPadrao;
        }

        // Salvar pasta no registro
        private static void SalvarPasta(string nomePasta, string caminho)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(CHAVE_REGISTRO))
                {
                    if (key != null)
                    {
                        key.SetValue(nomePasta, caminho);
                    }
                }

                // Criar a pasta se não existir
                if (!Directory.Exists(caminho))
                {
                    Directory.CreateDirectory(caminho);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao salvar configuração de pasta '{nomePasta}': {ex.Message}");
            }
        }

        // Resetar todas as pastas para os valores padrão
        public static void ResetarParaPadrao()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(CHAVE_REGISTRO))
                {
                    if (key != null)
                    {
                        key.DeleteValue("PastaBackups", false);
                        key.DeleteValue("PastaAnexos", false);
                        key.DeleteValue("PastaTemplates", false);
                        key.DeleteValue("PastaPDFs", false);
                        key.DeleteValue("PastaLogs", false);
                        key.DeleteValue("PastaLogsSms", false);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao resetar configurações: {ex.Message}");
            }
        }

        // Garantir que todas as pastas existam
        public static void GarantirPastasExistem()
        {
            CriarSeNaoExistir(PastaBackups);
            CriarSeNaoExistir(PastaAnexos);
            CriarSeNaoExistir(PastaTemplates);
            CriarSeNaoExistir(PastaPDFs);
            CriarSeNaoExistir(PastaLogs);
            CriarSeNaoExistir(PastaLogsSms);
        }

        // Criar pasta se não existir
        private static void CriarSeNaoExistir(string caminho)
        {
            try
            {
                if (!Directory.Exists(caminho))
                {
                    Directory.CreateDirectory(caminho);
                }
            }
            catch (Exception)
            {
                // Silenciosamente ignorar erros de criação
            }
        }

        // Verificar se todas as pastas estão configuradas
        public static bool TodasPastasConfiguradas()
        {
            return !string.IsNullOrWhiteSpace(PastaBackups) &&
                   !string.IsNullOrWhiteSpace(PastaAnexos) &&
                   !string.IsNullOrWhiteSpace(PastaTemplates) &&
                   !string.IsNullOrWhiteSpace(PastaPDFs) &&
                   !string.IsNullOrWhiteSpace(PastaLogs) &&
                   !string.IsNullOrWhiteSpace(PastaLogsSms);
        }
    }
}