// =============================================
// CLASSE DE ACESSO A DADOS - TEMPLATES SMS
// Arquivo: MensagemTemplateDAL.cs
// Gerencia templates salvos em JSON
// =============================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Cadastro1
{
    public class MensagemTemplateDAL
    {
        private string ObterDiretorioTemplates()
        {
            // Usar a mesma pasta de templates de mala direta
            return ConfiguracaoPastas.PastaTemplates;
        }

        private string ObterCaminhoArquivo()
        {
            return Path.Combine(ObterDiretorioTemplates(), "templates_sms.json");
        }

        /// <summary>
        /// Salva um template
        /// </summary>
        public bool SalvarTemplate(MensagemTemplate template)
        {
            try
            {
                ConfiguracaoPastas.GarantirPastasExistem();

                var templates = ListarTemplates();

                // Verificar se já existe
                var existente = templates.FirstOrDefault(t => t.TemplateID == template.TemplateID);

                if (existente != null)
                {
                    // Atualizar
                    templates.Remove(existente);
                    templates.Add(template);
                }
                else
                {
                    // Novo template - gerar ID
                    template.TemplateID = templates.Count > 0 ? templates.Max(t => t.TemplateID) + 1 : 1;
                    templates.Add(template);
                }

                SalvarTodos(templates);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao salvar template: {ex.Message}");
            }
        }

        /// <summary>
        /// Lista todos os templates
        /// </summary>
        public List<MensagemTemplate> ListarTemplates()
        {
            try
            {
                string caminho = ObterCaminhoArquivo();

                if (!File.Exists(caminho))
                {
                    // Criar arquivo com templates padrão
                    var templatesPadrao = MensagemTemplate.ObterTemplatesPadrao().ToList();
                    SalvarTodos(templatesPadrao);
                    return templatesPadrao;
                }

                string json = File.ReadAllText(caminho);
                var templates = JsonConvert.DeserializeObject<List<MensagemTemplate>>(json);

                return templates ?? new List<MensagemTemplate>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar templates: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém um template por ID
        /// </summary>
        public MensagemTemplate ObterTemplate(int templateID)
        {
            var templates = ListarTemplates();
            return templates.FirstOrDefault(t => t.TemplateID == templateID);
        }

        /// <summary>
        /// Exclui um template
        /// </summary>
        public bool ExcluirTemplate(int templateID)
        {
            try
            {
                var templates = ListarTemplates();
                var template = templates.FirstOrDefault(t => t.TemplateID == templateID);

                if (template != null)
                {
                    templates.Remove(template);
                    SalvarTodos(templates);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir template: {ex.Message}");
            }
        }

        /// <summary>
        /// Registra utilização do template
        /// </summary>
        public void RegistrarUtilizacao(int templateID)
        {
            try
            {
                var templates = ListarTemplates();
                var template = templates.FirstOrDefault(t => t.TemplateID == templateID);

                if (template != null)
                {
                    template.TotalUtilizacoes++;
                    template.UltimaUtilizacao = DateTime.Now;
                    SalvarTodos(templates);
                }
            }
            catch
            {
                // Falha silenciosa - não é crítico
            }
        }

        /// <summary>
        /// Salva todos os templates no arquivo
        /// </summary>
        private void SalvarTodos(List<MensagemTemplate> templates)
        {
            string caminho = ObterCaminhoArquivo();
            string json = JsonConvert.SerializeObject(templates, Formatting.Indented);
            File.WriteAllText(caminho, json);
        }
    }
}