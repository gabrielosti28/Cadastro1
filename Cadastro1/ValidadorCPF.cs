using System;
using System.Linq;

namespace Cadastro1.Utilidades
{
    /// <summary>
    /// Validador profissional de CPF com verificação de dígitos
    /// </summary>
    public static class ValidadorCPF
    {
        /// <summary>
        /// Valida CPF com verificação de dígitos verificadores
        /// </summary>
        public static bool Validar(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            // Remove formatação
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            // Completa com zeros à esquerda se necessário
            if (cpf.Length == 10)
                cpf = "0" + cpf;

            // Verifica tamanho
            if (cpf.Length != 11)
                return false;

            // Verifica CPFs inválidos conhecidos
            if (cpf == "00000000000" || cpf == "11111111111" ||
                cpf == "22222222222" || cpf == "33333333333" ||
                cpf == "44444444444" || cpf == "55555555555" ||
                cpf == "66666666666" || cpf == "77777777777" ||
                cpf == "88888888888" || cpf == "99999999999")
                return false;

            // Validação do primeiro dígito verificador
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * (10 - i);

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            if (int.Parse(cpf[9].ToString()) != digito1)
                return false;

            // Validação do segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * (11 - i);

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return int.Parse(cpf[10].ToString()) == digito2;
        }

        /// <summary>
        /// Remove formatação e retorna apenas dígitos
        /// Completa com zero à esquerda se tiver 10 dígitos
        /// </summary>
        public static string Limpar(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return "";

            string cpfLimpo = new string(cpf.Where(char.IsDigit).ToArray());

            // Completa com zero à esquerda se necessário
            if (cpfLimpo.Length == 10)
                cpfLimpo = "0" + cpfLimpo;

            return cpfLimpo;
        }

        /// <summary>
        /// Formata CPF para padrão 000.000.000-00
        /// </summary>
        public static string Formatar(string cpf)
        {
            cpf = Limpar(cpf);

            if (cpf.Length == 11)
            {
                return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";
            }

            return cpf;
        }

        /// <summary>
        /// Valida e formata CPF em uma única operação
        /// </summary>
        public static (bool valido, string cpfFormatado) ValidarEFormatar(string cpf)
        {
            string cpfLimpo = Limpar(cpf);
            bool valido = Validar(cpfLimpo);
            string formatado = valido ? Formatar(cpfLimpo) : cpf;

            return (valido, formatado);
        }
    }
}