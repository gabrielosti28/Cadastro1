// =============================================
// CLASSE DE SEGURANÇA E CRIPTOGRAFIA
// Arquivo: SecurityHelper.cs
// Sistema Profissional de Cadastro
// =============================================
using System;
using System.Security.Cryptography;
using System.Text;

namespace Cadastro1
{
    public static class SecurityHelper
    {
        /// <summary>
        /// Gera um Salt aleatório para aumentar a segurança
        /// </summary>
        public static string GerarSalt()
        {
            byte[] saltBytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        /// <summary>
        /// Criptografa a senha usando SHA256 + Salt
        /// Este é um método seguro que impede que a senha seja descoberta
        /// </summary>
        public static string CriptografarSenha(string senha, string salt)
        {
            // Combinar senha com salt
            string senhaComSalt = senha + salt;

            // Criar hash SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(senhaComSalt);
                byte[] hash = sha256.ComputeHash(bytes);

                // Converter para string hexadecimal
                StringBuilder resultado = new StringBuilder();
                foreach (byte b in hash)
                {
                    resultado.Append(b.ToString("x2"));
                }
                return resultado.ToString().ToUpper();
            }
        }

        /// <summary>
        /// Valida se a senha atende aos requisitos mínimos de segurança
        /// </summary>
        public static bool ValidarForcaSenha(string senha, out string mensagemErro)
        {
            mensagemErro = "";

            if (string.IsNullOrWhiteSpace(senha))
            {
                mensagemErro = "A senha não pode estar vazia!";
                return false;
            }

            if (senha.Length < 6)
            {
                mensagemErro = "A senha deve ter no mínimo 6 caracteres!";
                return false;
            }

            // Verificar se tem pelo menos uma letra
            bool temLetra = false;
            bool temNumero = false;

            foreach (char c in senha)
            {
                if (char.IsLetter(c)) temLetra = true;
                if (char.IsDigit(c)) temNumero = true;
            }

            if (!temLetra)
            {
                mensagemErro = "A senha deve conter pelo menos uma letra!";
                return false;
            }

            if (!temNumero)
            {
                mensagemErro = "A senha deve conter pelo menos um número!";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gera uma senha temporária aleatória segura
        /// </summary>
        public static string GerarSenhaTemporaria()
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789@#$";
            var random = new Random();
            var senha = new char[10];

            for (int i = 0; i < senha.Length; i++)
            {
                senha[i] = chars[random.Next(chars.Length)];
            }

            return new string(senha);
        }
    }
}