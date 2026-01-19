// =============================================
// CLASSE MODELO USUÁRIO
// Arquivo: Usuario.cs
// Sistema Profissional de Cadastro
// =============================================
using System;

namespace Cadastro1
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? UltimoAcesso { get; set; }
        public bool Ativo { get; set; }

        // Propriedade estática para armazenar usuário logado no sistema
        public static Usuario UsuarioLogado { get; set; }

        /// <summary>
        /// Verifica se existe um usuário logado no sistema
        /// </summary>
        public static bool EstaLogado()
        {
            return UsuarioLogado != null;
        }

        /// <summary>
        /// Faz logout do sistema
        /// </summary>
        public static void Logout()
        {
            UsuarioLogado = null;
        }
    }
}