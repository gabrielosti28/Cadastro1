// =============================================
// MODELO DE TEMPLATE DE MALA DIRETA
// Arquivo: MailingTemplate.cs
// =============================================
using System;
using System.Collections.Generic;

namespace Cadastro1
{
    /// <summary>
    /// Representa um campo de dados no template
    /// </summary>
    public class TemplateCampo
    {
        public string Nome { get; set; }              // Nome do campo (ex: "Nome", "Endereco")
        public float PosicaoX { get; set; }           // Posição X em mm
        public float PosicaoY { get; set; }           // Posição Y em mm
        public string FonteNome { get; set; }         // Nome da fonte
        public float FonteTamanho { get; set; }       // Tamanho da fonte
        public bool Negrito { get; set; }             // Se é negrito
        public int LarguraMaxima { get; set; }        // Largura máxima em mm (0 = sem limite)
    }

    /// <summary>
    /// Representa um template de mala direta
    /// </summary>
    public class MailingTemplate
    {
        public int TemplateID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string CaminhoImagemFundo { get; set; }  // Caminho da imagem A4 de fundo
        public List<TemplateCampo> Campos { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }

        public MailingTemplate()
        {
            Campos = new List<TemplateCampo>();
            DataCriacao = DateTime.Now;
            Ativo = true;
        }

        /// <summary>
        /// Adiciona um campo padrão ao template
        /// </summary>
        public void AdicionarCampoPadrao(string nome, float x, float y)
        {
            Campos.Add(new TemplateCampo
            {
                Nome = nome,
                PosicaoX = x,
                PosicaoY = y,
                FonteNome = "Arial",
                FonteTamanho = 11,
                Negrito = false,
                LarguraMaxima = 100
            });
        }

        /// <summary>
        /// Cria um template padrão brasileiro para envelope com janela
        /// </summary>
        public static MailingTemplate CriarTemplatePadrao()
        {
            var template = new MailingTemplate
            {
                Nome = "Template Padrão - Envelope com Janela",
                Descricao = "Template padrão para envelope com janela centralizada"
            };

            // Posições típicas para envelope com janela (em mm do topo/esquerda)
            // A janela geralmente fica entre 20-100mm do topo e 20-120mm da esquerda

            template.AdicionarCampoPadrao("NomeCompleto", 25, 100);
            template.AdicionarCampoPadrao("Endereco", 25, 110);
            template.AdicionarCampoPadrao("Cidade", 25, 120);
            template.AdicionarCampoPadrao("CEP", 25, 130);

            return template;
        }
    }
}