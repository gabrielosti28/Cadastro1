// =============================================
// CLASSE DE ACESSO A DADOS
// Arquivo: ClienteDAL.cs
// =============================================
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
                    cmd.Parameters.AddWithValue("@BeneficioINSS", cliente.BeneficioINSS);

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
                                BeneficioINSS = reader["BeneficioINSS"].ToString(),
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
                                BeneficioINSS = reader["BeneficioINSS"].ToString(),
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
                    cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                    cmd.Parameters.AddWithValue("@Cidade", cliente.Cidade);

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
}
