// =============================================
// CLASSE DE CONEXÃO COM O BANCO DE DADOS
// Arquivo: DatabaseConnection.cs
// =============================================
using System.Data.SqlClient;

namespace Cadastro1
{
    public class DatabaseConnection
    {
        // ALTERE AQUI SEUS DADOS DE CONEXÃO:
        private static string connectionString =
           //"Data Source=localhost\\SQLEXPRESS;Initial Catalog=projeto1;Integrated Security=True";

        // OPÇÃO 2: Se usar LocalDB
        // private static string connectionString =
        //     "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=projeto1;Integrated Security=True";

        // OPÇÃO 3: Se usar instância nomeada diferente
        // private static string connectionString =
        //     "Data Source=NOME_DO_SEU_PC\\DEVELOPER;Initial Catalog=projeto1;Integrated Security=True";

        // OPÇÃO 4: Instância padrão sem nome
        // private static string connectionString =
             "Data Source=localhost;Initial Catalog=projeto1;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}