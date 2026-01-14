// =============================================
// CLASSE DE CONEXÃO COM O BANCO DE DADOS
// Arquivo: DatabaseConnection.cs
// =============================================
using System;
using System.Data.SqlClient;

public class DatabaseConnection
{
    // ALTERE AQUI SEUS DADOS DE CONEXÃO:
    private static string connectionString =
        "Data Source=localhost\\DEVELOPER;Initial Catalog=projeto1;Integrated Security=True";

    public static SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }
}