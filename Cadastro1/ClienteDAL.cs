// =============================================
// CLASSE DE ACESSO A DADOS - ATUALIZADA
// Arquivo: ClienteDAL.cs
// =============================================
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Cadastro1
{
    public class ClienteDAL
    {
       
        
        
        
        
        public bool InserirCliente(Cliente cliente)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_InserirCliente", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@NomeCompleto", cliente.NomeCompleto);
                        cmd.Parameters.AddWithValue("@CPF", cliente.CPF);
                        cmd.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);
                        cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                        cmd.Parameters.AddWithValue("@Cidade", cliente.Cidade);
                        cmd.Parameters.AddWithValue("@CEP", cliente.CEP);
                        cmd.Parameters.AddWithValue("@Telefone",
                            string.IsNullOrEmpty(cliente.Telefone) ? (object)DBNull.Value : cliente.Telefone);
                        cmd.Parameters.AddWithValue("@BeneficioINSS", cliente.BeneficioINSS);
                        cmd.Parameters.AddWithValue("@BeneficioINSS2",
                            string.IsNullOrEmpty(cliente.BeneficioINSS2) ? (object)DBNull.Value : cliente.BeneficioINSS2);

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir cliente: " + ex.Message);
            }
        }

        public bool AtualizarCliente(Cliente cliente)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_AtualizarCliente", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ClienteID", cliente.ClienteID);
                        cmd.Parameters.AddWithValue("@NomeCompleto", cliente.NomeCompleto);
                        cmd.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);
                        cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                        cmd.Parameters.AddWithValue("@Cidade", cliente.Cidade);
                        cmd.Parameters.AddWithValue("@CEP", cliente.CEP);
                        cmd.Parameters.AddWithValue("@Telefone",
                            string.IsNullOrEmpty(cliente.Telefone) ? (object)DBNull.Value : cliente.Telefone);
                        cmd.Parameters.AddWithValue("@BeneficioINSS", cliente.BeneficioINSS);
                        cmd.Parameters.AddWithValue("@BeneficioINSS2",
                            string.IsNullOrEmpty(cliente.BeneficioINSS2) ? (object)DBNull.Value : cliente.BeneficioINSS2);

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar cliente: " + ex.Message);
            }
        }

        public Cliente ConsultarPorCPF(string cpf)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_ConsultarClientePorCPF", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CPF", cpf);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Cliente
                                {
                                    ClienteID = Convert.ToInt32(reader["ClienteID"]),
                                    NomeCompleto = reader["NomeCompleto"].ToString(),
                                    CPF = reader["CPF"].ToString(),
                                    DataNascimento = Convert.ToDateTime(reader["DataNascimento"]),
                                    Endereco = reader["Endereco"].ToString(),
                                    Cidade = reader["Cidade"].ToString(),
                                    CEP = reader["CEP"].ToString(),
                                    Telefone = reader["Telefone"] == DBNull.Value ? "" : reader["Telefone"].ToString(),
                                    BeneficioINSS = reader["BeneficioINSS"].ToString(),
                                    BeneficioINSS2 = reader["BeneficioINSS2"] == DBNull.Value ? "" : reader["BeneficioINSS2"].ToString(),
                                    DataCadastro = Convert.ToDateTime(reader["DataCadastro"]),
                                    Ativo = Convert.ToBoolean(reader["Ativo"])
                                };
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao consultar cliente: " + ex.Message);
            }
        }

        public List<Cliente> ListarTodosClientes()
        {
            List<Cliente> clientes = new List<Cliente>();

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Clientes WHERE Ativo = 1 ORDER BY NomeCompleto";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clientes.Add(new Cliente
                                {
                                    ClienteID = Convert.ToInt32(reader["ClienteID"]),
                                    NomeCompleto = reader["NomeCompleto"].ToString(),
                                    CPF = reader["CPF"].ToString(),
                                    DataNascimento = Convert.ToDateTime(reader["DataNascimento"]),
                                    Endereco = reader["Endereco"].ToString(),
                                    Cidade = reader["Cidade"].ToString(),
                                    CEP = reader["CEP"].ToString(),
                                    Telefone = reader["Telefone"] == DBNull.Value ? "" : reader["Telefone"].ToString(),
                                    BeneficioINSS = reader["BeneficioINSS"].ToString(),
                                    BeneficioINSS2 = reader["BeneficioINSS2"] == DBNull.Value ? "" : reader["BeneficioINSS2"].ToString(),
                                    DataCadastro = Convert.ToDateTime(reader["DataCadastro"]),
                                    Ativo = Convert.ToBoolean(reader["Ativo"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar clientes: " + ex.Message);
            }

            return clientes;
        }

        /// <summary>
        /// Busca clientes que fazem aniversário hoje
        /// </summary>
        public List<Cliente> BuscarAniversariantesHoje()
        {
            List<Cliente> clientes = new List<Cliente>();

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    string query = @"
                SELECT * FROM Clientes 
                WHERE Ativo = 1 
                AND DAY(DataNascimento) = DAY(GETDATE())
                AND MONTH(DataNascimento) = MONTH(GETDATE())
                ORDER BY NomeCompleto";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clientes.Add(new Cliente
                                {
                                    ClienteID = Convert.ToInt32(reader["ClienteID"]),
                                    NomeCompleto = reader["NomeCompleto"].ToString(),
                                    CPF = reader["CPF"].ToString(),
                                    DataNascimento = Convert.ToDateTime(reader["DataNascimento"]),
                                    Endereco = reader["Endereco"].ToString(),
                                    Cidade = reader["Cidade"].ToString(),
                                    CEP = reader["CEP"].ToString(),
                                    Telefone = reader["Telefone"] == DBNull.Value ? "" : reader["Telefone"].ToString(),
                                    BeneficioINSS = reader["BeneficioINSS"].ToString(),
                                    BeneficioINSS2 = reader["BeneficioINSS2"] == DBNull.Value ? "" : reader["BeneficioINSS2"].ToString(),
                                    DataCadastro = Convert.ToDateTime(reader["DataCadastro"]),
                                    Ativo = Convert.ToBoolean(reader["Ativo"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar aniversariantes de hoje: " + ex.Message);
            }

            return clientes;
        }

        /// <summary>
        /// Busca clientes que fazem aniversário na semana atual
        /// </summary>
        public List<Cliente> BuscarAniversariantesSemana()
        {
            List<Cliente> clientes = new List<Cliente>();

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    string query = @"
                SELECT * FROM Clientes 
                WHERE Ativo = 1 
                AND (
                    -- Para aniversários na mesma semana do ano
                    DATEPART(WEEK, DataNascimento) = DATEPART(WEEK, GETDATE())
                    OR
                    -- Para casos onde a data de hoje está na mesma semana mas ano diferente
                    (
                        DATEDIFF(DAY, 
                            DATEADD(YEAR, DATEDIFF(YEAR, DataNascimento, GETDATE()), DataNascimento),
                            GETDATE()
                        ) BETWEEN 0 AND 6
                    )
                )
                ORDER BY 
                    MONTH(DataNascimento),
                    DAY(DataNascimento),
                    NomeCompleto";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clientes.Add(new Cliente
                                {
                                    ClienteID = Convert.ToInt32(reader["ClienteID"]),
                                    NomeCompleto = reader["NomeCompleto"].ToString(),
                                    CPF = reader["CPF"].ToString(),
                                    DataNascimento = Convert.ToDateTime(reader["DataNascimento"]),
                                    Endereco = reader["Endereco"].ToString(),
                                    Cidade = reader["Cidade"].ToString(),
                                    CEP = reader["CEP"].ToString(),
                                    Telefone = reader["Telefone"] == DBNull.Value ? "" : reader["Telefone"].ToString(),
                                    BeneficioINSS = reader["BeneficioINSS"].ToString(),
                                    BeneficioINSS2 = reader["BeneficioINSS2"] == DBNull.Value ? "" : reader["BeneficioINSS2"].ToString(),
                                    DataCadastro = Convert.ToDateTime(reader["DataCadastro"]),
                                    Ativo = Convert.ToBoolean(reader["Ativo"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar aniversariantes da semana: " + ex.Message);
            }

            return clientes;
        }

        /// <summary>
        /// Calcula a idade atual do cliente
        /// </summary>
        public static int CalcularIdade(DateTime dataNascimento)
        {
            int idade = DateTime.Now.Year - dataNascimento.Year;
            if (DateTime.Now.DayOfYear < dataNascimento.DayOfYear)
                idade--;
            return idade;
        }



    }
}