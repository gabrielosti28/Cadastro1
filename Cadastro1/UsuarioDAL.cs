// =============================================
// CLASSE DE ACESSO A DADOS - USUÁRIO
// Arquivo: UsuarioDAL.cs
// Sistema Profissional de Cadastro
// =============================================
using System;
using System.Data;
using System.Data.SqlClient;

namespace Cadastro1
{
    public class UsuarioDAL
    {
        /// <summary>
        /// Valida login do usuário
        /// </summary>
        public Usuario ValidarLogin(string login, string senha)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Primeiro, obter o Salt do usuário
                    string salt = "";
                    using (SqlCommand cmdSalt = new SqlCommand(
                        "SELECT Salt FROM Usuario WHERE Login = @Login AND Ativo = 1", conn))
                    {
                        cmdSalt.Parameters.AddWithValue("@Login", login);
                        object result = cmdSalt.ExecuteScalar();

                        if (result == null)
                        {
                            throw new Exception("Usuário não encontrado!");
                        }

                        salt = result.ToString();
                    }

                    // Criptografar a senha com o salt
                    string senhaHash = SecurityHelper.CriptografarSenha(senha, salt);

                    // Validar login com stored procedure
                    using (SqlCommand cmd = new SqlCommand("SP_ValidarLogin", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Login", login);
                        cmd.Parameters.AddWithValue("@SenhaHash", senhaHash);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int resultado = Convert.ToInt32(reader["Resultado"]);
                                string mensagem = reader["Mensagem"].ToString();

                                if (resultado == 1)
                                {
                                    // Login bem-sucedido
                                    return new Usuario
                                    {
                                        UsuarioID = Convert.ToInt32(reader["UsuarioID"]),
                                        Login = login,
                                        Nome = reader["Nome"].ToString(),
                                        Ativo = true
                                    };
                                }
                                else
                                {
                                    // Falha no login
                                    throw new Exception(mensagem);
                                }
                            }
                        }
                    }
                }

                throw new Exception("Erro ao processar login!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Altera a senha do usuário
        /// </summary>
        public bool AlterarSenha(string login, string senhaAtual, string novaSenha)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Obter salt atual
                    string saltAtual = "";
                    using (SqlCommand cmdSalt = new SqlCommand(
                        "SELECT Salt FROM Usuario WHERE Login = @Login", conn))
                    {
                        cmdSalt.Parameters.AddWithValue("@Login", login);
                        saltAtual = cmdSalt.ExecuteScalar().ToString();
                    }

                    // Gerar novo salt para a nova senha
                    string novoSalt = SecurityHelper.GerarSalt();

                    // Criptografar senhas
                    string senhaAtualHash = SecurityHelper.CriptografarSenha(senhaAtual, saltAtual);
                    string novaSenhaHash = SecurityHelper.CriptografarSenha(novaSenha, novoSalt);

                    // Chamar procedure de alteração
                    using (SqlCommand cmd = new SqlCommand("SP_AlterarSenha", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Login", login);
                        cmd.Parameters.AddWithValue("@SenhaAtualHash", senhaAtualHash);
                        cmd.Parameters.AddWithValue("@NovaSenhaHash", novaSenhaHash);
                        cmd.Parameters.AddWithValue("@NovoSalt", novoSalt);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int resultado = Convert.ToInt32(reader["Resultado"]);
                                string mensagem = reader["Mensagem"].ToString();

                                if (resultado == 1)
                                    return true;
                                else
                                    throw new Exception(mensagem);
                            }
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao alterar senha: " + ex.Message);
            }
        }

        /// <summary>
        /// Cria o primeiro usuário do sistema
        /// </summary>
        public bool CriarPrimeiroUsuario(string login, string senha, string nome)
        {
            try
            {
                // Validar força da senha
                string mensagemErro;
                if (!SecurityHelper.ValidarForcaSenha(senha, out mensagemErro))
                {
                    throw new Exception(mensagemErro);
                }

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Gerar salt único
                    string salt = SecurityHelper.GerarSalt();

                    // Criptografar senha
                    string senhaHash = SecurityHelper.CriptografarSenha(senha, salt);

                    // Inserir usuário
                    string query = @"
                        INSERT INTO Usuario (Login, SenhaHash, Salt, Nome, Ativo)
                        VALUES (@Login, @SenhaHash, @Salt, @Nome, 1)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Login", login);
                        cmd.Parameters.AddWithValue("@SenhaHash", senhaHash);
                        cmd.Parameters.AddWithValue("@Salt", salt);
                        cmd.Parameters.AddWithValue("@Nome", nome);

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar usuário: " + ex.Message);
            }
        }

        /// <summary>
        /// Verifica se é o primeiro acesso (não existe nenhum usuário)
        /// </summary>
        public bool VerificarPrimeiroAcesso()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_VerificarPrimeiroAcesso", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return Convert.ToInt32(reader["PrimeiroAcesso"]) == 1;
                            }
                        }
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}