// =============================================
// CLASSE DE ACESSO A DADOS - ANEXOS
// Arquivo: ClienteAnexoDAL.cs
// Sistema Profissional de Cadastro
// =============================================
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Cadastro1
{
    public class ClienteAnexoDAL
    {
        private string diretorioBase;

        public ClienteAnexoDAL()
        {
            // Diretório base para armazenamento de anexos
            // Em ambiente de produção, use um caminho adequado
            diretorioBase = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "SistemaCadastroClientes",
                "Anexos"
            );

            // Criar diretório se não existir
            if (!Directory.Exists(diretorioBase))
            {
                Directory.CreateDirectory(diretorioBase);
            }
        }

        /// <summary>
        /// Insere um novo anexo no banco e copia o arquivo físico
        /// </summary>
        public int InserirAnexo(ClienteAnexo anexo, string arquivoOrigem)
        {
            try
            {
                // Validações de segurança
                if (!File.Exists(arquivoOrigem))
                    throw new Exception("Arquivo não encontrado.");

                FileInfo fileInfo = new FileInfo(arquivoOrigem);

                // Limite de tamanho: 50MB
                if (fileInfo.Length > 50 * 1024 * 1024)
                    throw new Exception("Arquivo muito grande. Tamanho máximo: 50MB");

                // Validar extensão
                string[] extensoesPermitidas = {
                    ".pdf", ".doc", ".docx", ".txt", ".jpg", ".jpeg",
                    ".png", ".gif", ".bmp", ".xls", ".xlsx", ".csv"
                };

                if (!Array.Exists(extensoesPermitidas,
                    ext => ext.Equals(fileInfo.Extension, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception(
                        "Tipo de arquivo não permitido.\n\n" +
                        "Tipos permitidos: PDF, Word, Excel, Imagens (JPG, PNG), TXT"
                    );
                }

                // Criar diretório do cliente
                string diretorioCliente = Path.Combine(
                    diretorioBase,
                    $"Cliente_{anexo.ClienteID}"
                );

                if (!Directory.Exists(diretorioCliente))
                {
                    Directory.CreateDirectory(diretorioCliente);
                }

                // Gerar nome único para o arquivo
                string nomeArquivo = $"{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid().ToString("N").Substring(0, 8)}{fileInfo.Extension}";
                string caminhoDestino = Path.Combine(diretorioCliente, nomeArquivo);

                // Copiar arquivo
                File.Copy(arquivoOrigem, caminhoDestino, true);

                // Inserir no banco de dados
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_InserirAnexo", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ClienteID", anexo.ClienteID);
                        cmd.Parameters.AddWithValue("@NomeArquivo", nomeArquivo);
                        cmd.Parameters.AddWithValue("@NomeOriginal", anexo.NomeOriginal);
                        cmd.Parameters.AddWithValue("@TipoArquivo", fileInfo.Extension);
                        cmd.Parameters.AddWithValue("@TamanhoBytes", fileInfo.Length);
                        cmd.Parameters.AddWithValue("@CaminhoArquivo", caminhoDestino);
                        cmd.Parameters.AddWithValue("@Descricao",
                            string.IsNullOrEmpty(anexo.Descricao) ? (object)DBNull.Value : anexo.Descricao);
                        cmd.Parameters.AddWithValue("@UploadPor",
                            string.IsNullOrEmpty(anexo.UploadPor) ? (object)DBNull.Value : anexo.UploadPor);

                        object resultado = cmd.ExecuteScalar();
                        return Convert.ToInt32(resultado);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao anexar arquivo: " + ex.Message);
            }
        }

        /// <summary>
        /// Lista todos os anexos de um cliente
        /// </summary>
        public List<ClienteAnexo> ListarAnexosCliente(int clienteID)
        {
            List<ClienteAnexo> anexos = new List<ClienteAnexo>();

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_ListarAnexosCliente", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ClienteID", clienteID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                anexos.Add(new ClienteAnexo
                                {
                                    AnexoID = Convert.ToInt32(reader["AnexoID"]),
                                    ClienteID = Convert.ToInt32(reader["ClienteID"]),
                                    NomeArquivo = reader["NomeArquivo"].ToString(),
                                    NomeOriginal = reader["NomeOriginal"].ToString(),
                                    TipoArquivo = reader["TipoArquivo"].ToString(),
                                    TamanhoBytes = Convert.ToInt64(reader["TamanhoBytes"]),
                                    CaminhoArquivo = reader["CaminhoArquivo"].ToString(),
                                    Descricao = reader["Descricao"] == DBNull.Value ?
                                        "" : reader["Descricao"].ToString(),
                                    DataUpload = Convert.ToDateTime(reader["DataUpload"]),
                                    UploadPor = reader["UploadPor"] == DBNull.Value ?
                                        "" : reader["UploadPor"].ToString(),
                                    Ativo = Convert.ToBoolean(reader["Ativo"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar anexos: " + ex.Message);
            }

            return anexos;
        }

        /// <summary>
        /// Exclui um anexo (soft delete no banco e remove arquivo físico)
        /// </summary>
        public bool ExcluirAnexo(int anexoID)
        {
            try
            {
                // Primeiro obter informações do anexo
                ClienteAnexo anexo = ObterAnexo(anexoID);
                if (anexo == null)
                    throw new Exception("Anexo não encontrado.");

                // Excluir do banco (soft delete)
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_ExcluirAnexo", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AnexoID", anexoID);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Excluir arquivo físico
                if (File.Exists(anexo.CaminhoArquivo))
                {
                    File.Delete(anexo.CaminhoArquivo);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir anexo: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtém informações de um anexo específico
        /// </summary>
        public ClienteAnexo ObterAnexo(int anexoID)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_ObterAnexo", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AnexoID", anexoID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new ClienteAnexo
                                {
                                    AnexoID = Convert.ToInt32(reader["AnexoID"]),
                                    ClienteID = Convert.ToInt32(reader["ClienteID"]),
                                    NomeArquivo = reader["NomeArquivo"].ToString(),
                                    NomeOriginal = reader["NomeOriginal"].ToString(),
                                    TipoArquivo = reader["TipoArquivo"].ToString(),
                                    TamanhoBytes = Convert.ToInt64(reader["TamanhoBytes"]),
                                    CaminhoArquivo = reader["CaminhoArquivo"].ToString(),
                                    Descricao = reader["Descricao"] == DBNull.Value ?
                                        "" : reader["Descricao"].ToString(),
                                    DataUpload = Convert.ToDateTime(reader["DataUpload"]),
                                    UploadPor = reader["UploadPor"] == DBNull.Value ?
                                        "" : reader["UploadPor"].ToString(),
                                    Ativo = Convert.ToBoolean(reader["Ativo"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter anexo: " + ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Abre um anexo com o programa padrão do Windows
        /// </summary>
        public void AbrirAnexo(int anexoID)
        {
            try
            {
                ClienteAnexo anexo = ObterAnexo(anexoID);
                if (anexo == null)
                    throw new Exception("Anexo não encontrado.");

                if (!File.Exists(anexo.CaminhoArquivo))
                    throw new Exception("Arquivo não encontrado no disco.");

                System.Diagnostics.Process.Start(anexo.CaminhoArquivo);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao abrir anexo: " + ex.Message);
            }
        }

        /// <summary>
        /// Conta quantos anexos um cliente possui
        /// </summary>
        public int ContarAnexosCliente(int clienteID)
        {
            return ListarAnexosCliente(clienteID).Count;
        }
    }
}