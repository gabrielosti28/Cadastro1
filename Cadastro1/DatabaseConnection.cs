using System.Data.SqlClient;

namespace Cadastro1
{
    public class DatabaseConnection
    {
        // STRING DE CONEXÃO CORRETA (Windows Authentication)
        private static string connectionString =
            "Data Source=DESKTOP-83DNHA9\\GOSTI;Initial Catalog=projeto1;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
