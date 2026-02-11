using System.Data.SqlClient;

namespace Cadastro1
{
    public class DatabaseConnection
    {
        // STRING DE CONEXÃO CORRETA (Windows Authentication)
        private static string connectionString =
            "Data Source=DESKTOP-83DNHA9\\GOSTI;Initial Catalog=projeto22;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static bool TestarConexao()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }


    }
}
