using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Cadastro1.Utilidades
{
    /// <summary>
    /// Validador universal de campos
    /// </summary>
    public static class Validador
    {
        public static bool CampoObrigatorio(TextBox campo, string nomeCampo)
        {
            if (string.IsNullOrWhiteSpace(campo.Text))
            {
                Mensagens.ExibirAviso(string.Format(Mensagens.CAMPO_OBRIGATORIO, nomeCampo));
                campo.Focus();
                return false;
            }
            return true;
        }

        public static bool ValidarCPF(TextBox campo)
        {
            if (!ValidadorCPF.Validar(campo.Text))
            {
                Mensagens.ExibirAviso(Mensagens.CPF_INVALIDO);
                campo.Focus();
                return false;
            }
            return true;
        }

        public static bool ValidarCEP(TextBox campo)
        {
            string cep = new string(campo.Text.Where(char.IsDigit).ToArray());

            if (cep.Length != 8)
            {
                Mensagens.ExibirAviso(Mensagens.CEP_INVALIDO);
                campo.Focus();
                return false;
            }
            return true;
        }

        public static bool ValidarEmail(TextBox campo)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(campo.Text, pattern))
            {
                Mensagens.ExibirAviso(Mensagens.EMAIL_INVALIDO);
                campo.Focus();
                return false;
            }
            return true;
        }

        public static bool ValidarTelefone(TextBox campo)
        {
            string telefone = new string(campo.Text.Where(char.IsDigit).ToArray());

            if (telefone.Length < 10 || telefone.Length > 11)
            {
                Mensagens.ExibirAviso(Mensagens.TELEFONE_INVALIDO);
                campo.Focus();
                return false;
            }
            return true;
        }
    }
}