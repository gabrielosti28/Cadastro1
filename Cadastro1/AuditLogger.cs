using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Cadastro1
{
    public static class AuditLogger
    {
        public enum TipoOperacao
        {
            INSERT, UPDATE, DELETE, LOGIN, LOGOUT,
            BACKUP, RESTORE, ERRO
        }

        private const int MAX_DETALHES = 1000;

        public static void RegistrarOperacao(TipoOperacao tipo, string tabela, string detalhes, int? registroID = null)
        {
            try
            {
                string usuario = Usuario.UsuarioLogado?.Nome ?? "Sistema";
                string login = Usuario.UsuarioLogado?.Login ?? "SYSTEM";
                string detalhesTruncados = TruncarTexto(detalhes, MAX_DETALHES);

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    string sql = @"INSERT INTO AuditLog (TipoOperacao, Tabela, RegistroID, Usuario, Login, Detalhes, DataHora)
                                   VALUES (@TipoOperacao, @Tabela, @RegistroID, @Usuario, @Login, @Detalhes, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TipoOperacao", tipo.ToString());
                        cmd.Parameters.AddWithValue("@Tabela", tabela ?? "");
                        cmd.Parameters.AddWithValue("@RegistroID", (object)registroID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Usuario", usuario);
                        cmd.Parameters.AddWithValue("@Login", login);
                        cmd.Parameters.AddWithValue("@Detalhes", detalhesTruncados ?? "");
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERRO AUDITORIA: {ex.Message}");
            }
        }

        private static string TruncarTexto(string texto, int maxLength)
        {
            if (string.IsNullOrEmpty(texto)) return texto;
            return texto.Length > maxLength ? texto.Substring(0, maxLength - 3) + "..." : texto;
        }

        public static void RegistrarInsercao(string tabela, int registroID, string detalhes = "") =>
            RegistrarOperacao(TipoOperacao.INSERT, tabela, detalhes, registroID);

        public static void RegistrarAtualizacao(string tabela, int registroID, string detalhes = "") =>
            RegistrarOperacao(TipoOperacao.UPDATE, tabela, detalhes, registroID);

        public static void RegistrarExclusao(string tabela, int registroID, string detalhes = "") =>
            RegistrarOperacao(TipoOperacao.DELETE, tabela, detalhes, registroID);

        public static void RegistrarLogin(string login, bool sucesso) =>
            RegistrarOperacao(TipoOperacao.LOGIN, "Usuario", sucesso ? "Login bem-sucedido" : "Tentativa falhou");

        public static void RegistrarLogout() =>
            RegistrarOperacao(TipoOperacao.LOGOUT, "Usuario", "Logout realizado");

        public static void RegistrarBackup(string caminhoBackup, bool sucesso) =>
            RegistrarOperacao(TipoOperacao.BACKUP, "Sistema",
                sucesso ? $"Backup criado: {caminhoBackup}" : $"Falha: {caminhoBackup}");

        public static void RegistrarRestauracao(string caminhoBackup, bool sucesso) =>
            RegistrarOperacao(TipoOperacao.RESTORE, "Sistema",
                sucesso ? $"Backup restaurado: {caminhoBackup}" : $"Falha: {caminhoBackup}");

        public static void RegistrarErro(string contexto, string mensagemErro) =>
            RegistrarOperacao(TipoOperacao.ERRO, contexto, $"ERRO: {TruncarTexto(mensagemErro, 900)}");

        public static DataTable BuscarLogs(DateTime? dataInicio = null, DateTime? dataFim = null,
            string tipoOperacao = null, string usuario = null)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    string sql = @"SELECT LogID, TipoOperacao, Tabela, RegistroID, Usuario, Login, Detalhes, DataHora
                                   FROM AuditLog WHERE 1=1";

                    if (dataInicio.HasValue) sql += " AND DataHora >= @DataInicio";
                    if (dataFim.HasValue) sql += " AND DataHora <= @DataFim";
                    if (!string.IsNullOrEmpty(tipoOperacao)) sql += " AND TipoOperacao = @TipoOperacao";
                    if (!string.IsNullOrEmpty(usuario)) sql += " AND Login LIKE @Usuario";

                    sql += " ORDER BY DataHora DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (dataInicio.HasValue) cmd.Parameters.AddWithValue("@DataInicio", dataInicio.Value);
                        if (dataFim.HasValue) cmd.Parameters.AddWithValue("@DataFim", dataFim.Value.AddDays(1));
                        if (!string.IsNullOrEmpty(tipoOperacao)) cmd.Parameters.AddWithValue("@TipoOperacao", tipoOperacao);
                        if (!string.IsNullOrEmpty(usuario)) cmd.Parameters.AddWithValue("@Usuario", $"%{usuario}%");

                        DataTable dt = new DataTable();
                        new SqlDataAdapter(cmd).Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao buscar logs: {ex.Message}");
                throw new Exception($"Erro ao buscar logs: {ex.Message}");
            }
        }
    }
}