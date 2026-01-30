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
            ConfiguracaoPastas.GarantirPastasExistem();
        }

        public int InserirAnexo(ClienteAnexo anexo, string arquivoOrigem)
        {
            FileInfo fileInfo = new FileInfo(arquivoOrigem);
            ValidarArquivo(fileInfo);

            string caminhoDestino = PrepararArquivoDestino(anexo.ClienteID, fileInfo.Extension);
            File.Copy(arquivoOrigem, caminhoDestino, true);

            return InserirAnexoBanco(anexo, fileInfo, caminhoDestino);
        }

        private void ValidarArquivo(FileInfo fileInfo)
        {
            if (!fileInfo.Exists)
                throw new Exception("Arquivo não encontrado.");

            if (fileInfo.Length > TAMANHO_MAXIMO_BYTES)
                throw new Exception($"Arquivo muito grande. Máximo: {TAMANHO_MAXIMO_MB}MB");

            string extensao = fileInfo.Extension.ToLowerInvariant();
            if (!Array.Exists(ExtensoesPermitidas, ext => ext == extensao))
                throw new Exception("Tipo de arquivo não permitido.\nPermitidos: PDF, Word, Excel, Imagens, TXT");
        }

        private string PrepararArquivoDestino(int clienteID, string extensao)
        {
            string diretorioCliente = Path.Combine(ConfiguracaoPastas.PastaAnexos, $"Cliente_{clienteID}");

            if (!Directory.Exists(diretorioCliente))
                Directory.CreateDirectory(diretorioCliente);

            string nomeArquivo = $"{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid():N}{extensao}";
            return Path.Combine(diretorioCliente, nomeArquivo);
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
                    cmd.Parameters.AddWithValue("@Descricao", (object)anexo.Descricao ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UploadPor", (object)anexo.UploadPor ?? DBNull.Value);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public List<ClienteAnexo> ListarAnexosCliente(int clienteID)
        {
            List<ClienteAnexo> anexos = new List<ClienteAnexo>();

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
                            anexos.Add(CriarAnexoDoReader(reader));
                    }
                }
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
            ClienteAnexo anexo = ObterAnexo(anexoID);
            if (anexo == null)
                throw new Exception("Anexo não encontrado.");

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

            if (File.Exists(anexo.CaminhoArquivo))
                File.Delete(anexo.CaminhoArquivo);

            return true;
        }

        public ClienteAnexo ObterAnexo(int anexoID)
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
                            return CriarAnexoDoReader(reader);
                    }
                }
            }
            return null;
        }

        public void AbrirAnexo(int anexoID)
        {
            ClienteAnexo anexo = ObterAnexo(anexoID);
            if (anexo == null)
                throw new Exception("Anexo não encontrado.");

            if (!File.Exists(anexo.CaminhoArquivo))
                throw new Exception("Arquivo não encontrado no disco.");

            System.Diagnostics.Process.Start(anexo.CaminhoArquivo);
        }

        public int ContarAnexosCliente(int clienteID) => ListarAnexosCliente(clienteID).Count;
    }
}