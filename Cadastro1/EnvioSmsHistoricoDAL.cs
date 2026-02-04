// =============================================
// CLASSE DE ACESSO A DADOS - HISTÓRICO SMS
// Arquivo: EnvioSmsHistoricoDAL.cs
// Gerencia histórico de envios em JSON
// CORRIGIDO: Adicionado using System.Linq
// =============================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; // ← ADICIONADO: Necessário para usar Where, OrderBy, etc
using Newtonsoft.Json;

namespace Cadastro1
{
    public class EnvioSmsHistoricoDAL
    {
        private string ObterDiretorioLogs()
        {
            return ConfiguracaoPastas.PastaLogsSms;
        }

        private string ObterCaminhoArquivoAtual()
        {
            // Um arquivo por mês para facilitar gestão
            string nomeArquivo = $"historico_sms_{DateTime.Now:yyyy_MM}.json";
            return Path.Combine(ObterDiretorioLogs(), nomeArquivo);
        }

        /// <summary>
        /// Registra um envio no histórico
        /// </summary>
        public bool RegistrarEnvio(EnvioSmsHistorico envio)
        {
            try
            {
                ConfiguracaoPastas.GarantirPastasExistem();

                var historico = ListarEnviosMesAtual();

                // Gerar ID
                envio.EnvioID = historico.Count > 0 ? historico.Max(h => h.EnvioID) + 1 : 1;

                historico.Add(envio);

                SalvarHistorico(historico);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao registrar envio: {ex.Message}");
            }
        }

        /// <summary>
        /// Registra múltiplos envios
        /// </summary>
        public bool RegistrarEnvios(List<EnvioSmsHistorico> envios)
        {
            try
            {
                ConfiguracaoPastas.GarantirPastasExistem();

                var historico = ListarEnviosMesAtual();

                int proximoID = historico.Count > 0 ? historico.Max(h => h.EnvioID) + 1 : 1;

                foreach (var envio in envios)
                {
                    envio.EnvioID = proximoID++;
                    historico.Add(envio);
                }

                SalvarHistorico(historico);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao registrar envios: {ex.Message}");
            }
        }

        /// <summary>
        /// Lista envios do mês atual
        /// </summary>
        public List<EnvioSmsHistorico> ListarEnviosMesAtual()
        {
            try
            {
                string caminho = ObterCaminhoArquivoAtual();

                if (!File.Exists(caminho))
                {
                    return new List<EnvioSmsHistorico>();
                }

                string json = File.ReadAllText(caminho);
                var historico = JsonConvert.DeserializeObject<List<EnvioSmsHistorico>>(json);

                return historico ?? new List<EnvioSmsHistorico>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar envios: {ex.Message}");
            }
        }

        /// <summary>
        /// Lista envios por período
        /// </summary>
        public List<EnvioSmsHistorico> ListarEnviosPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                List<EnvioSmsHistorico> todosEnvios = new List<EnvioSmsHistorico>();

                string diretorio = ObterDiretorioLogs();

                if (!Directory.Exists(diretorio))
                    return todosEnvios;

                // Listar todos os arquivos JSON no diretório
                string[] arquivos = Directory.GetFiles(diretorio, "historico_sms_*.json");

                foreach (string arquivo in arquivos)
                {
                    try
                    {
                        string json = File.ReadAllText(arquivo);
                        var historico = JsonConvert.DeserializeObject<List<EnvioSmsHistorico>>(json);

                        if (historico != null)
                        {
                            // Filtrar por data
                            var enviosFiltrados = historico.Where(e =>
                                e.DataHoraEnvio >= dataInicio &&
                                e.DataHoraEnvio <= dataFim
                            ).ToList();

                            todosEnvios.AddRange(enviosFiltrados);
                        }
                    }
                    catch
                    {
                        // Ignorar arquivos corrompidos
                    }
                }

                return todosEnvios.OrderByDescending(e => e.DataHoraEnvio).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar envios por período: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém estatísticas gerais
        /// </summary>
        public EstatisticasSms ObterEstatisticas()
        {
            try
            {
                var enviosMes = ListarEnviosMesAtual();

                return new EstatisticasSms
                {
                    TotalEnviosMes = enviosMes.Count,
                    SucessosMes = enviosMes.Count(e => e.Sucesso),
                    FalhasMes = enviosMes.Count(e => !e.Sucesso),
                    CustoTotalMes = enviosMes.Sum(e => e.CustoEstimado),
                    TotalSegmentosMes = enviosMes.Sum(e => e.QuantidadeSegmentos),
                    UltimoEnvio = enviosMes.OrderByDescending(e => e.DataHoraEnvio).FirstOrDefault()?.DataHoraEnvio
                };
            }
            catch
            {
                return new EstatisticasSms();
            }
        }

        /// <summary>
        /// Limpa históricos antigos (mais de 6 meses)
        /// </summary>
        public void LimparHistoricosAntigos()
        {
            try
            {
                string diretorio = ObterDiretorioLogs();

                if (!Directory.Exists(diretorio))
                    return;

                DateTime dataLimite = DateTime.Now.AddMonths(-6);

                string[] arquivos = Directory.GetFiles(diretorio, "historico_sms_*.json");

                foreach (string arquivo in arquivos)
                {
                    try
                    {
                        FileInfo info = new FileInfo(arquivo);

                        if (info.CreationTime < dataLimite)
                        {
                            File.Delete(arquivo);
                        }
                    }
                    catch
                    {
                        // Ignorar falhas ao deletar
                    }
                }
            }
            catch
            {
                // Falha silenciosa
            }
        }

        /// <summary>
        /// Salva histórico no arquivo
        /// </summary>
        private void SalvarHistorico(List<EnvioSmsHistorico> historico)
        {
            string caminho = ObterCaminhoArquivoAtual();
            string json = JsonConvert.SerializeObject(historico, Formatting.Indented);
            File.WriteAllText(caminho, json);
        }
    }

    /// <summary>
    /// Estatísticas de envio de SMS
    /// </summary>
    public class EstatisticasSms
    {
        public int TotalEnviosMes { get; set; }
        public int SucessosMes { get; set; }
        public int FalhasMes { get; set; }
        public decimal CustoTotalMes { get; set; }
        public int TotalSegmentosMes { get; set; }
        public DateTime? UltimoEnvio { get; set; }

        public double TaxaSucessoMes
        {
            get
            {
                if (TotalEnviosMes == 0)
                    return 0;
                return (double)SucessosMes / TotalEnviosMes * 100;
            }
        }

        public string ResumoMes
        {
            get
            {
                return $"📊 MÊS ATUAL:\n" +
                       $"   Enviados: {TotalEnviosMes}\n" +
                       $"   Sucessos: {SucessosMes}\n" +
                       $"   Falhas: {FalhasMes}\n" +
                       $"   Taxa sucesso: {TaxaSucessoMes:F1}%\n" +
                       $"   Custo: R$ {CustoTotalMes:F2}\n" +
                       $"   Segmentos: {TotalSegmentosMes}";
            }
        }
    }
}