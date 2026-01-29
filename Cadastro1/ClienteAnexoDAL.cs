// =============================================
// CLASSE DE ACESSO A DADOS - ANEXOS
// Arquivo: ClienteAnexoDAL.cs
// ATUALIZADO: Usa ConfiguracaoPastas
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
        private const long TAMANHO_MAXIMO_MB = 50;
        private const long TAMANHO_MAXIMO_BYTES = TAMANHO_MAXIMO_MB * 1024 * 1024;

        private static readonly string[] ExtensoesPermitidas = {
            ".pdf", ".doc", ".docx", ".txt", ".jpg", ".jpeg",
            ".png", ".gif", ".bmp", ".xls", ".xlsx", ".csv"
        };

        public ClienteAnexoDAL()
        {
            // Garantir que a pasta existe
            ConfiguracaoPastas.GarantirPastasExistem();
        }

        /// <summary>
        /// Obtém o diretório base de anexos
        /// </summary>
        private string ObterDiretorioBase()
        {
            return ConfiguracaoPastas.PastaAnexos;
        }

        /// <summary>
        /// SIMPLIFICADO: Validações consolidadas em método único
        /// </summary>
        private void ValidarArquivo(string arquivoOrigem)
        {
            if (!File.Exists(arquivoOrigem))
                throw new Exception("Arquivo não encontrado.");

            FileInfo fileInfo = new FileInfo(arquivoOrigem);

            if (fileInfo.Length > TAMANHO_MAXIMO_BYTES)
                throw new Exception($"Arquivo muito grande. Tamanho máximo: {TAMANHO_MAXIMO_MB}MB");

            string extensao = fileInfo.Extension.ToLowerInvariant();
            bool extensaoValida = Array.Exists(ExtensoesPermitidas, ext => ext == extensao);

            if (!extensaoValida)
            {
                throw new Exception(
                    "Tipo de arquivo não permitido.\n\n" +
                    "Tipos permitidos: PDF, Word, Excel, Imagens (JPG, PNG), TXT"
                );
            }
        }

        /// <summary>
        /// SIMPLIFICADO: Criação de diretório e nome de arquivo em método separado
        /// </summary>
        private string PrepararArquivoDestino(int clienteID, string extensao)
        {
            string diretorioBase = ObterDiretorioBase();
            string diretorioCliente = Path.Combine(diretorioBase, $"Cliente_{clienteID}");

            if (!Directory.Exists(diretorioCliente))
            {
                Directory.CreateDirectory(diretorioCliente);
            }

            string nomeArquivo = $"{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid():N}{extensao}";
            return Path.Combine(diretorioCliente, nomeArquivo);
        }

        /// <summary>
        /// Insere um novo anexo no banco e copia o arquivo físico
        /// </summary>
        public int InserirAnexo(ClienteAnexo anexo, string arquivoOrigem)
        {
            try
            {
                // Validar arquivo
                ValidarArquivo(arquivoOrigem);

                FileInfo fileInfo = new FileInfo(arquivoOrigem);

                // Preparar destino
                string caminhoDestino = PrepararArquivoDestino(anexo.ClienteID, fileInfo.Extension);

                // Copiar arquivo
                File.Copy(arquivoOrigem, caminhoDestino, true);

                // Inserir no banco de dados
                return InserirAnexoBanco(anexo, fileInfo, caminhoDestino);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao anexar arquivo: " + ex.Message);
            }
        }

        private int InserirAnexoBanco(ClienteAnexo anexo, FileInfo fileInfo, string caminhoDestino)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SP_InserirAnexo", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ClienteID", anexo.ClienteID);
                    cmd.Parameters.AddWithValue("@NomeArquivo", Path.GetFileName(caminhoDestino));
                    cmd.Parameters.AddWithValue("@NomeOriginal", anexo.NomeOriginal);
                    cmd.Parameters.AddWithValue("@TipoArquivo", fileInfo.Extension);
                    cmd.Parameters.AddWithValue("@TamanhoBytes", fileInfo.Length);
                    cmd.Parameters.AddWithValue("@CaminhoArquivo", caminhoDestino);
                    cmd.Parameters.AddWithValue("@Descricao",
                        string.IsNullOrEmpty(anexo.Descricao) ? (object)DBNull.Value : anexo.Descricao);
                    cmd.Parameters.AddWithValue("@UploadPor",
                        string.IsNullOrEmpty(anexo.UploadPor) ? (object)DBNull.Value : anexo.UploadPor);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

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
                                anexos.Add(CriarAnexoDoReader(reader));
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

        private ClienteAnexo CriarAnexoDoReader(SqlDataReader reader)
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
                Descricao = reader["Descricao"] == DBNull.Value ? "" : reader["Descricao"].ToString(),
                DataUpload = Convert.ToDateTime(reader["DataUpload"]),
                UploadPor = reader["UploadPor"] == DBNull.Value ? "" : reader["UploadPor"].ToString(),
                Ativo = Convert.ToBoolean(reader["Ativo"])
            };
        }

        public bool ExcluirAnexo(int anexoID)
        {
            try
            {
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
                                return CriarAnexoDoReader(reader);
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

        public int ContarAnexosCliente(int clienteID)
        {
            return ListarAnexosCliente(clienteID).Count;
        }
    }
}