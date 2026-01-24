// =============================================
// GERENCIADOR DE TRANSAÇÕES SEGURAS
// Arquivo: TransactionManager.cs
// Sistema Profissional de Cadastro
// =============================================
using System;
using System.Data;
using System.Data.SqlClient;

namespace Cadastro1
{
    /// <summary>
    /// Gerencia transações do banco de dados com proteção contra perda de dados
    /// Implementa padrão de transação com rollback automático em caso de falha
    /// </summary>
    public class TransactionManager : IDisposable
    {
        private SqlConnection _connection;
        private SqlTransaction _transaction;
        private bool _disposed = false;
        private bool _committed = false;

        /// <summary>
        /// Inicia uma nova transação
        /// </summary>
        public TransactionManager()
        {
            _connection = DatabaseConnection.GetConnection();
            _connection.Open();
            _transaction = _connection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// Obtém a conexão atual
        /// </summary>
        public SqlConnection Connection => _connection;

        /// <summary>
        /// Obtém a transação atual
        /// </summary>
        public SqlTransaction Transaction => _transaction;

        /// <summary>
        /// Executa um comando dentro da transação
        /// </summary>
        public int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, _connection, _transaction))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao executar comando: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Executa uma stored procedure dentro da transação
        /// </summary>
        public int ExecuteStoredProcedure(string procedureName, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, _connection, _transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao executar {procedureName}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Executa um comando escalar dentro da transação
        /// </summary>
        public object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, _connection, _transaction))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao executar scalar: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Confirma a transação (salva as alterações)
        /// </summary>
        public void Commit()
        {
            try
            {
                if (_transaction != null && !_committed)
                {
                    _transaction.Commit();
                    _committed = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao confirmar transação: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Desfaz a transação (reverte as alterações)
        /// </summary>
        public void Rollback()
        {
            try
            {
                if (_transaction != null && !_committed)
                {
                    _transaction.Rollback();
                }
            }
            catch (Exception ex)
            {
                // Log do erro mas não propagar para não mascarar o erro original
                System.Diagnostics.Debug.WriteLine($"Erro ao fazer rollback: {ex.Message}");
            }
        }

        /// <summary>
        /// Dispose automático com rollback se não foi feito commit
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Se não foi feito commit, fazer rollback automático
                    if (!_committed)
                    {
                        Rollback();
                    }

                    // Limpar recursos
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }

                    if (_connection != null)
                    {
                        if (_connection.State == ConnectionState.Open)
                        {
                            _connection.Close();
                        }
                        _connection.Dispose();
                        _connection = null;
                    }
                }

                _disposed = true;
            }
        }
    }

    /// <summary>
    /// Métodos auxiliares para operações transacionais comuns
    /// </summary>
    public static class TransactionHelper
    {
        /// <summary>
        /// Executa uma ação dentro de uma transação segura
        /// Faz rollback automático em caso de erro
        /// </summary>
        public static T ExecutarComTransacao<T>(Func<TransactionManager, T> action)
        {
            using (TransactionManager tm = new TransactionManager())
            {
                try
                {
                    T resultado = action(tm);
                    tm.Commit();
                    return resultado;
                }
                catch (Exception ex)
                {
                    tm.Rollback();
                    throw new Exception($"Transação falhou e foi revertida: {ex.Message}", ex);
                }
            }
        }

        /// <summary>
        /// Executa uma ação dentro de uma transação segura (sem retorno)
        /// Faz rollback automático em caso de erro
        /// </summary>
        public static void ExecutarComTransacao(Action<TransactionManager> action)
        {
            using (TransactionManager tm = new TransactionManager())
            {
                try
                {
                    action(tm);
                    tm.Commit();
                }
                catch (Exception ex)
                {
                    tm.Rollback();
                    throw new Exception($"Transação falhou e foi revertida: {ex.Message}", ex);
                }
            }
        }
    }
}