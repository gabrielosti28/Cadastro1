// =============================================
// CLASSE DE ACESSO A DADOS - TEMPLATES
// Arquivo: MailingTemplateDAL.cs
// ATUALIZADO: Usa ConfiguracaoPastas
// =============================================
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using JsonFormatting = Newtonsoft.Json.Formatting;

namespace Cadastro1
{
    public class MailingTemplateDAL
    {
        public MailingTemplateDAL()
        {
            // Garantir que a pasta existe
            ConfiguracaoPastas.GarantirPastasExistem();
        }

        /// <summary>
        /// Obtém o diretório de templates
        /// </summary>
        private string ObterDiretorioTemplates()
        {
            return ConfiguracaoPastas.PastaTemplates;
        }

        /// <summary>
        /// Salva um template (em JSON por simplicidade)
        /// </summary>
        public bool SalvarTemplate(MailingTemplate template)
        {
            try
            {
                string diretorio = ObterDiretorioTemplates();

                // Garantir que o diretório existe
                if (!Directory.Exists(diretorio))
                {
                    Directory.CreateDirectory(diretorio);
                }

                string nomeArquivo = $"Template_{DateTime.Now:yyyyMMddHHmmss}.json";
                string caminhoCompleto = Path.Combine(diretorio, nomeArquivo);

                string json = JsonConvert.SerializeObject(template, JsonFormatting.Indented);
                File.WriteAllText(caminhoCompleto, json);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar template: " + ex.Message);
            }
        }

        /// <summary>
        /// Lista todos os templates salvos
        /// </summary>
        public List<MailingTemplate> ListarTemplates()
        {
            List<MailingTemplate> templates = new List<MailingTemplate>();

            try
            {
                string diretorio = ObterDiretorioTemplates();

                if (!Directory.Exists(diretorio))
                {
                    return templates;
                }

                string[] arquivos = Directory.GetFiles(diretorio, "*.json");

                foreach (string arquivo in arquivos)
                {
                    try
                    {
                        string json = File.ReadAllText(arquivo);
                        MailingTemplate template = JsonConvert.DeserializeObject<MailingTemplate>(json);
                        templates.Add(template);
                    }
                    catch
                    {
                        // Ignorar arquivos corrompidos
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar templates: " + ex.Message);
            }

            return templates;
        }

        /// <summary>
        /// Carrega um template específico
        /// </summary>
        public MailingTemplate CarregarTemplate(string nomeTemplate)
        {
            try
            {
                string diretorio = ObterDiretorioTemplates();

                if (!Directory.Exists(diretorio))
                {
                    return null;
                }

                string[] arquivos = Directory.GetFiles(diretorio, "*.json");

                foreach (string arquivo in arquivos)
                {
                    string json = File.ReadAllText(arquivo);
                    MailingTemplate template = JsonConvert.DeserializeObject<MailingTemplate>(json);

                    if (template.Nome == nomeTemplate)
                    {
                        return template;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar template: " + ex.Message);
            }
        }

        public string ObterDiretorioTemplatesAtual()
        {
            return ObterDiretorioTemplates();
        }
    }
}