// =============================================
// CLASSE MODELO - HISTÓRICO DE ENVIO SMS
// Arquivo: EnvioSmsHistorico.cs
// Registra todos os envios realizados
// =============================================
using System;
using System.Collections.Generic;

namespace Cadastro1
{
    /// <summary>
    /// Representa um envio de SMS no histórico
    /// </summary>
    public class EnvioSmsHistorico
    {
        public int EnvioID { get; set; }
        public DateTime DataHoraEnvio { get; set; }
        public string NomeCliente { get; set; }
        public string TelefoneDestino { get; set; }
        public string Mensagem { get; set; }
        public int QuantidadeSegmentos { get; set; }
        public decimal CustoEstimado { get; set; }
        public bool Sucesso { get; set; }
        public string StatusTwilio { get; set; }
        public string MensagemErro { get; set; }
        public string SIDTwilio { get; set; } // ID da mensagem no Twilio
        public string UsuarioEnvio { get; set; }

        public EnvioSmsHistorico()
        {
            DataHoraEnvio = DateTime.Now;
            Sucesso = false;
        }

        /// <summary>
        /// Descrição resumida do envio
        /// </summary>
        public string Resumo
        {
            get
            {
                string status = Sucesso ? "✓ Enviado" : "✗ Falhou";
                return $"{status} - {NomeCliente} ({TelefoneDestino}) - {DataHoraEnvio:dd/MM/yyyy HH:mm}";
            }
        }

        /// <summary>
        /// Ícone de status
        /// </summary>
        public string IconeStatus
        {
            get
            {
                if (Sucesso)
                {
                    if (StatusTwilio == "delivered")
                        return "✓✓"; // Entregue
                    else if (StatusTwilio == "sent" || StatusTwilio == "queued")
                        return "✓"; // Enviado mas não entregue ainda
                    else
                        return "⏳"; // Processando
                }
                else
                {
                    return "✗"; // Falhou
                }
            }
        }

        /// <summary>
        /// Descrição do status
        /// </summary>
        public string DescricaoStatus
        {
            get
            {
                if (!Sucesso)
                    return "Falha no envio";

                switch (StatusTwilio?.ToLower())
                {
                    case "queued":
                        return "Na fila";
                    case "sent":
                        return "Enviado";
                    case "delivered":
                        return "Entregue";
                    case "failed":
                        return "Falhou";
                    case "undelivered":
                        return "Não entregue";
                    default:
                        return "Processando";
                }
            }
        }
    }

    /// <summary>
    /// Resultado de um envio em lote
    /// </summary>
    public class ResultadoEnvioLote
    {
        public int TotalClientes { get; set; }
        public int EnviosRealizados { get; set; }
        public int Sucessos { get; set; }
        public int Falhas { get; set; }
        public decimal CustoTotal { get; set; }
        public int TotalSegmentos { get; set; }
        public List<EnvioSmsHistorico> Historico { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }

        public ResultadoEnvioLote()
        {
            Historico = new List<EnvioSmsHistorico>();
            DataHoraInicio = DateTime.Now;
        }

        /// <summary>
        /// Finaliza o envio lote
        /// </summary>
        public void Finalizar()
        {
            DataHoraFim = DateTime.Now;
        }

        /// <summary>
        /// Duração do envio
        /// </summary>
        public TimeSpan Duracao
        {
            get
            {
                if (DataHoraFim == DateTime.MinValue)
                    return TimeSpan.Zero;
                return DataHoraFim - DataHoraInicio;
            }
        }

        /// <summary>
        /// Taxa de sucesso
        /// </summary>
        public double TaxaSucesso
        {
            get
            {
                if (EnviosRealizados == 0)
                    return 0;
                return (double)Sucessos / EnviosRealizados * 100;
            }
        }

        /// <summary>
        /// Gera relatório do envio
        /// </summary>
        public string GerarRelatorio()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.AppendLine("╔═══════════════════════════════════════════════════════════╗");
            sb.AppendLine("║           📱 RELATÓRIO DE ENVIO DE SMS EM MASSA          ║");
            sb.AppendLine("╚═══════════════════════════════════════════════════════════╝\n");

            sb.AppendLine("📊 RESUMO DO ENVIO:");
            sb.AppendLine("─────────────────────────────────────────────────────────");
            sb.AppendLine($"   Total de clientes selecionados:  {TotalClientes}");
            sb.AppendLine($"   Mensagens enviadas:              {EnviosRealizados}");
            sb.AppendLine($"   ✓ Sucessos:                      {Sucessos}");
            sb.AppendLine($"   ✗ Falhas:                        {Falhas}");
            sb.AppendLine($"   Taxa de sucesso:                 {TaxaSucesso:F1}%\n");

            sb.AppendLine("💰 CUSTOS:");
            sb.AppendLine("─────────────────────────────────────────────────────────");
            sb.AppendLine($"   Total de segmentos SMS:          {TotalSegmentos}");
            sb.AppendLine($"   Custo estimado:                  R$ {CustoTotal:F2}\n");

            sb.AppendLine("⏱️  TEMPO:");
            sb.AppendLine("─────────────────────────────────────────────────────────");
            sb.AppendLine($"   Início:                          {DataHoraInicio:dd/MM/yyyy HH:mm:ss}");
            sb.AppendLine($"   Fim:                             {DataHoraFim:dd/MM/yyyy HH:mm:ss}");
            sb.AppendLine($"   Duração:                         {Duracao.TotalSeconds:F1} segundos\n");

            if (Falhas > 0)
            {
                sb.AppendLine("❌ FALHAS:");
                sb.AppendLine("─────────────────────────────────────────────────────────");

                var falhas = Historico.Where(h => !h.Sucesso).Take(10).ToList();

                foreach (var falha in falhas)
                {
                    sb.AppendLine($"   • {falha.NomeCliente} ({falha.TelefoneDestino})");
                    sb.AppendLine($"     Erro: {falha.MensagemErro}\n");
                }

                if (Falhas > 10)
                {
                    sb.AppendLine($"   ... e mais {Falhas - 10} falhas\n");
                }
            }

            sb.AppendLine("═══════════════════════════════════════════════════════════");

            return sb.ToString();
        }
    }
}