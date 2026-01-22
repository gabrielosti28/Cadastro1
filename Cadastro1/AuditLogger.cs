// =============================================
// SISTEMA DE LOG DE AUDITORIA
// Arquivo: AuditLogger.cs
// Sistema Profissional de Cadastro
// =============================================
using System;
using System.Data;
using System.Data.SqlClient;

namespace Cadastro1
{
    /// <summary>
    /// Registra todas as operações realizadas no sistema para auditoria
    /// Permite rastrear quem fez o quê e quando
    /// </summary>
    public static class AuditLogger
    {
        /// <summary>
        /// Tipo de operação realizada
        /// </summary>
        public enum TipoOperacao
        {
            INSERT,
            UPDATE,
            DELETE,
            LOGIN,
            LOGOUT,
            BACKUP,
            RESTORE,
            ERRO
        }

        /// <summary>
        /// Registra uma operação de auditoria
        /// </summary>
        public static void RegistrarOperacao(
            TipoOperacao tipo,
            string tabela,
            string detalhes,
            int? registroID = null)
        {
            try
            {
                string usuario = Usuario.UsuarioLogado?.Nome ?? "Sistema";
                string login = Usuario.UsuarioLogado?.Login ?? "SYSTEM";

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    string sql = @"
                        INSERT INTO AuditLog 
                        (TipoOperacao, Tabela, RegistroID, Usuario, Login, Detalhes, DataHora)
                        VALUES 
                        (@TipoOperacao, @Tabela, @RegistroID, @Usuario, @Login, @Detalhes, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TipoOperacao", tipo.ToString());
                        cmd.Parameters.AddWithValue("@Tabela", tabela ?? "");
                        cmd.Parameters.AddWithValue("@RegistroID",
                            registroID.HasValue ? (object)registroID.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@Usuario", usuario);
                        cmd.Parameters.AddWithValue("@Login", login);
                        cmd.Parameters.AddWithValue("@Detalhes", detalhes ?? "");

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Não propagar erro de auditoria para não impactar operação principal
                System.Diagnostics.Debug.WriteLine($"Erro ao registrar auditoria: {ex.Message}");
            }
        }

        /// <summary>
        /// Registra inserção de registro
        /// </summary>
        public static void RegistrarInsercao(string tabela, int registroID, string detalhes = "")
        {
            RegistrarOperacao(TipoOperacao.INSERT, tabela, detalhes, registroID);
        }

        /// <summary>
        /// Registra atualização de registro
        /// </summary>
        public static void RegistrarAtualizacao(string tabela, int registroID, string detalhes = "")
        {
            RegistrarOperacao(TipoOperacao.UPDATE, tabela, detalhes, registroID);
        }

        /// <summary>
        /// Registra exclusão de registro
        /// </summary>
        public static void RegistrarExclusao(string tabela, int registroID, string detalhes = "")
        {
            RegistrarOperacao(TipoOperacao.DELETE, tabela, detalhes, registroID);
        }

        /// <summary>
        /// Registra login de usuário
        /// </summary>
        public static void RegistrarLogin(string login, bool sucesso)
        {
            string detalhes = sucesso ? "Login bem-sucedido" : "Tentativa de login falhou";
            RegistrarOperacao(TipoOperacao.LOGIN, "Usuario", detalhes);
        }

        /// <summary>
        /// Registra logout de usuário
        /// </summary>
        public static void RegistrarLogout()
        {
            RegistrarOperacao(TipoOperacao.LOGOUT, "Usuario", "Logout realizado");
        }

        /// <summary>
        /// Registra backup realizado
        /// </summary>
        public static void RegistrarBackup(string caminhoBackup, bool sucesso)
        {
            string detalhes = sucesso
                ? $"Backup criado: {caminhoBackup}"
                : $"Falha ao criar backup: {caminhoBackup}";

            RegistrarOperacao(TipoOperacao.BACKUP, "Sistema", detalhes);
        }

        /// <summary>
        /// Registra restauração de backup
        /// </summary>
        public static void RegistrarRestauracao(string caminhoBackup, bool sucesso)
        {
            string detalhes = sucesso
                ? $"Backup restaurado: {caminhoBackup}"
                : $"Falha ao restaurar backup: {caminhoBackup}";

            RegistrarOperacao(TipoOperacao.RESTORE, "Sistema", detalhes);
        }

        /// <summary>
        /// Registra erro crítico
        /// </summary>
        public static void RegistrarErro(string contexto, string mensagemErro)
        {
            RegistrarOperacao(TipoOperacao.ERRO, contexto,
                $"ERRO: {mensagemErro}");
        }

        /// <summary>
        /// Busca logs de auditoria com filtros
        /// </summary>
        public static DataTable BuscarLogs(
            DateTime? dataInicio = null,
            DateTime? dataFim = null,
            string tipoOperacao = null,
            string usuario = null)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    string sql = @"
                        SELECT 
                            LogID,
                            TipoOperacao,
                            Tabela,
                            RegistroID,
                            Usuario,
                            Login,
                            Detalhes,
                            DataHora
                        FROM AuditLog
                        WHERE 1=1";

                    if (dataInicio.HasValue)
                        sql += " AND DataHora >= @DataInicio";

                    if (dataFim.HasValue)
                        sql += " AND DataHora <= @DataFim";

                    if (!string.IsNullOrEmpty(tipoOperacao))
                        sql += " AND TipoOperacao = @TipoOperacao";

                    if (!string.IsNullOrEmpty(usuario))
                        sql += " AND Login LIKE @Usuario";

                    sql += " ORDER BY DataHora DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (dataInicio.HasValue)
                            cmd.Parameters.AddWithValue("@DataInicio", dataInicio.Value);

                        if (dataFim.HasValue)
                            cmd.Parameters.AddWithValue("@DataFim", dataFim.Value.AddDays(1));

                        if (!string.IsNullOrEmpty(tipoOperacao))
                            cmd.Parameters.AddWithValue("@TipoOperacao", tipoOperacao);

                        if (!string.IsNullOrEmpty(usuario))
                            cmd.Parameters.AddWithValue("@Usuario", "%" + usuario + "%");

                        DataTable dt = new DataTable();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar logs: {ex.Message}");
            }
        }
    }
}