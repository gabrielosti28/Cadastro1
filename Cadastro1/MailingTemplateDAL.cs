// =============================================
// CLASSE DE ACESSO A DADOS - TEMPLATES
// Arquivo: MailingTemplateDAL.cs
// =============================================
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json;
using JsonFormatting = Newtonsoft.Json.Formatting;

namespace Cadastro1
{
    public class MailingTemplateDAL
    {
        private readonly string diretorioTemplates;

        public MailingTemplateDAL()
        {
            // Diretório para salvar templates
            diretorioTemplates = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "SistemaCadastroClientes",
                "Templates"
            );

            if (!Directory.Exists(diretorioTemplates))
            {
                Directory.CreateDirectory(diretorioTemplates);
            }
        }

        /// <summary>
        /// Salva um template (em JSON por simplicidade)
        /// </summary>
        public bool SalvarTemplate(MailingTemplate template)
        {
            try
            {
                string nomeArquivo = $"Template_{DateTime.Now:yyyyMMddHHmmss}.json";
                string caminhoCompleto = Path.Combine(diretorioTemplates, nomeArquivo);

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
                string[] arquivos = Directory.GetFiles(diretorioTemplates, "*.json");

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
                string[] arquivos = Directory.GetFiles(diretorioTemplates, "*.json");

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

        public string ObterDiretorioTemplates()
        {
            return diretorioTemplates;
        }
    }
}