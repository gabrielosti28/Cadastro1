// =============================================
// CLASSE MODELO - TEMPLATE DE MENSAGEM SMS
// Arquivo: MensagemTemplate.cs
// Sistema de Envio de SMS em Massa
// =============================================
using System;
using System.Linq;

namespace Cadastro1
{
    /// <summary>
    /// Representa um template de mensagem SMS
    /// </summary>
    public class MensagemTemplate
    {
        public int TemplateID { get; set; }
        public string Nome { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? UltimaUtilizacao { get; set; }
        public int TotalUtilizacoes { get; set; }
        public bool Ativo { get; set; }

        public MensagemTemplate()
        {
            DataCriacao = DateTime.Now;
            TotalUtilizacoes = 0;
            Ativo = true;
        }

        /// <summary>
        /// Substitui variáveis no template
        /// Variáveis suportadas: {NOME}, {CIDADE}, {TELEFONE}, {CPF}
        /// </summary>
        public string ProcessarTemplate(Cliente cliente)
        {
            if (string.IsNullOrEmpty(Conteudo))
                return "";

            string mensagem = Conteudo;

            // Substituir variáveis
            mensagem = mensagem.Replace("{NOME}", cliente.NomeCompleto ?? "");
            mensagem = mensagem.Replace("{CIDADE}", cliente.Cidade ?? "");
            mensagem = mensagem.Replace("{TELEFONE}", cliente.Telefone ?? "");
            mensagem = mensagem.Replace("{CPF}", FormatarCPF(cliente.CPF));

            // Variáveis de data
            mensagem = mensagem.Replace("{DATA}", DateTime.Now.ToString("dd/MM/yyyy"));
            mensagem = mensagem.Replace("{HORA}", DateTime.Now.ToString("HH:mm"));

            return mensagem;
        }

        /// <summary>
        /// Valida o conteúdo do template
        /// </summary>
        public (bool valido, string mensagemErro) Validar()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                return (false, "O nome do template é obrigatório");

            if (string.IsNullOrWhiteSpace(Conteudo))
                return (false, "O conteúdo da mensagem é obrigatório");

            if (Conteudo.Length > 1600)
                return (false, "Mensagem muito longa (máximo 1600 caracteres)");

            return (true, "");
        }

        /// <summary>
        /// Conta quantos SMS serão necessários para enviar esta mensagem
        /// SMS padrão = 160 caracteres
        /// Com caracteres especiais (acentos) = 70 caracteres
        /// </summary>
        public int ContarSegmentos(string mensagemProcessada)
        {
            if (string.IsNullOrEmpty(mensagemProcessada))
                return 0;

            // Verificar se tem caracteres especiais
            bool temEspeciais = mensagemProcessada.Any(c => c > 127);
            int limitePorSms = temEspeciais ? 70 : 160;

            return (int)Math.Ceiling((double)mensagemProcessada.Length / limitePorSms);
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

        /// <summary>
        /// Templates padrão do sistema
        /// </summary>
        public static MensagemTemplate[] ObterTemplatesPadrao()
        {
            return new MensagemTemplate[]
            {
                new MensagemTemplate
                {
                    Nome = "Boas-vindas",
                    Conteudo = "Olá {NOME}! Seja bem-vindo(a) ao nosso sistema. Estamos à disposição!"
                },
                new MensagemTemplate
                {
                    Nome = "Lembrete Geral",
                    Conteudo = "Olá {NOME}! Este é um lembrete importante. Entre em contato conosco."
                },
                new MensagemTemplate
                {
                    Nome = "Promoção",
                    Conteudo = "Atenção {NOME}! Temos uma promoção especial para você. Não perca!"
                },
                new MensagemTemplate
                {
                    Nome = "Aviso Importante",
                    Conteudo = "IMPORTANTE: {NOME}, informamos que houve mudanças. Entre em contato."
                },
                new MensagemTemplate
                {
                    Nome = "Confirmação",
                    Conteudo = "Olá {NOME}, confirmamos seu cadastro em {DATA}. Obrigado!"
                }
            };
        }
    }
}