// =============================================
// SERVIÇO DE ENVIO DE SMS VIA TWILIO
// Arquivo: SmsService.cs
// Integração profissional com Twilio
// CORRIGIDO: Método CalcularSegmentos tornado público
// =============================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Cadastro1
{
    /// <summary>
    /// Configurações do Twilio
    /// </summary>
    public class TwilioConfig
    {
        public string AccountSID { get; set; }
        public string AuthToken { get; set; }
        public string NumeroOrigem { get; set; }

        public bool EstaConfigurado()
        {
            return !string.IsNullOrEmpty(AccountSID) &&
                   !string.IsNullOrEmpty(AuthToken) &&
                   !string.IsNullOrEmpty(NumeroOrigem);
        }
    }

    /// <summary>
    /// Serviço de envio de SMS
    /// </summary>
    public class SmsService
    {
        private TwilioConfig config;
        private EnvioSmsHistoricoDAL historicoDAL;
        private const decimal CUSTO_POR_SEGMENTO = 0.15m; // R$ 0,15 por SMS (ajustar conforme plano Twilio)

        public SmsService()
        {
            config = CarregarConfiguracao();
            historicoDAL = new EnvioSmsHistoricoDAL();
        }

        /// <summary>
        /// Carrega configuração do Twilio do registro
        /// </summary>
        private TwilioConfig CarregarConfiguracao()
        {
            try
            {
                using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\SistemaCadastroClientes\Twilio"))
                {
                    if (key != null)
                    {
                        return new TwilioConfig
                        {
                            AccountSID = key.GetValue("AccountSID") as string,
                            AuthToken = key.GetValue("AuthToken") as string,
                            NumeroOrigem = key.GetValue("NumeroOrigem") as string
                        };
                    }
                }
            }
            catch { }

            return new TwilioConfig();
        }

        /// <summary>
        /// Salva configuração do Twilio no registro
        /// </summary>
        public void SalvarConfiguracao(TwilioConfig novaConfig)
        {
            try
            {
                using (var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\SistemaCadastroClientes\Twilio"))
                {
                    key.SetValue("AccountSID", novaConfig.AccountSID ?? "");
                    key.SetValue("AuthToken", novaConfig.AuthToken ?? "");
                    key.SetValue("NumeroOrigem", novaConfig.NumeroOrigem ?? "");
                }

                config = novaConfig;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao salvar configuração: {ex.Message}");
            }
        }

        /// <summary>
        /// Verifica se o Twilio está configurado
        /// </summary>
        public bool EstaConfigurado()
        {
            return config.EstaConfigurado();
        }

        /// <summary>
        /// Testa a conexão com o Twilio
        /// </summary>
        public (bool sucesso, string mensagem) TestarConexao()
        {
            try
            {
                if (!config.EstaConfigurado())
                {
                    return (false, "Twilio não configurado. Configure Account SID, Auth Token e Número.");
                }

                TwilioClient.Init(config.AccountSID, config.AuthToken);

                // Tentar obter informações da conta
                var account = Twilio.Rest.Api.V2010.AccountResource.Fetch();

                if (account != null)
                {
                    return (true, $"✓ Conexão OK!\nConta: {account.FriendlyName}\nStatus: {account.Status}");
                }

                return (false, "Não foi possível verificar a conta.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro na conexão:\n{ex.Message}");
            }
        }

        /// <summary>
        /// Envia SMS para um único cliente
        /// </summary>
        public EnvioSmsHistorico EnviarSms(Cliente cliente, string mensagem)
        {
            EnvioSmsHistorico historico = new EnvioSmsHistorico
            {
                NomeCliente = cliente.NomeCompleto,
                TelefoneDestino = cliente.Telefone,
                Mensagem = mensagem,
                UsuarioEnvio = Usuario.UsuarioLogado?.Nome ?? "Sistema"
            };

            try
            {
                // Validar telefone
                string telefone = LimparTelefone(cliente.Telefone);
                if (string.IsNullOrEmpty(telefone))
                {
                    historico.Sucesso = false;
                    historico.MensagemErro = "Telefone inválido ou não cadastrado";
                    return historico;
                }

                // Calcular segmentos
                historico.QuantidadeSegmentos = CalcularSegmentos(mensagem);
                historico.CustoEstimado = historico.QuantidadeSegmentos * CUSTO_POR_SEGMENTO;

                // Inicializar Twilio
                TwilioClient.Init(config.AccountSID, config.AuthToken);

                // Enviar SMS
                var message = MessageResource.Create(
                    body: mensagem,
                    from: new PhoneNumber(config.NumeroOrigem),
                    to: new PhoneNumber($"+55{telefone}")
                );

                historico.Sucesso = true;
                historico.StatusTwilio = message.Status.ToString();
                historico.SIDTwilio = message.Sid;
            }
            catch (Exception ex)
            {
                historico.Sucesso = false;
                historico.MensagemErro = ex.Message;
            }

            // Registrar no histórico
            try
            {
                historicoDAL.RegistrarEnvio(historico);
            }
            catch
            {
                // Falha ao registrar não deve impedir o envio
            }

            return historico;
        }

        /// <summary>
        /// Envia SMS para múltiplos clientes
        /// </summary>
        public async Task<ResultadoEnvioLote> EnviarSmsLote(List<Cliente> clientes, string mensagemTemplate)
        {
            ResultadoEnvioLote resultado = new ResultadoEnvioLote
            {
                TotalClientes = clientes.Count
            };

            if (!config.EstaConfigurado())
            {
                throw new Exception("Twilio não configurado!");
            }

            TwilioClient.Init(config.AccountSID, config.AuthToken);

            foreach (var cliente in clientes)
            {
                // Processar template
                MensagemTemplate template = new MensagemTemplate { Conteudo = mensagemTemplate };
                string mensagemFinal = template.ProcessarTemplate(cliente);

                // Enviar
                var historico = EnviarSms(cliente, mensagemFinal);

                resultado.Historico.Add(historico);
                resultado.EnviosRealizados++;

                if (historico.Sucesso)
                {
                    resultado.Sucessos++;
                }
                else
                {
                    resultado.Falhas++;
                }

                resultado.CustoTotal += historico.CustoEstimado;
                resultado.TotalSegmentos += historico.QuantidadeSegmentos;

                // Aguardar 1 segundo entre envios (limite Twilio)
                await Task.Delay(1000);
            }

            resultado.Finalizar();

            // Salvar histórico do lote
            try
            {
                historicoDAL.RegistrarEnvios(resultado.Historico);
            }
            catch
            {
                // Falha ao salvar não deve impedir conclusão
            }

            return resultado;
        }

        /// <summary>
        /// Limpa e valida telefone
        /// </summary>
        private string LimparTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
                return null;

            // Remover tudo que não é dígito
            string limpo = new string(telefone.Where(char.IsDigit).ToArray());

            // Validar comprimento (10 ou 11 dígitos)
            if (limpo.Length == 10 || limpo.Length == 11)
            {
                return limpo;
            }

            return null;
        }

        /// <summary>
        /// Calcula quantidade de segmentos SMS necessários
        /// CORRIGIDO: Alterado de private para public
        /// </summary>
        public int CalcularSegmentos(string mensagem)
        {
            if (string.IsNullOrEmpty(mensagem))
                return 0;

            // Verificar se tem caracteres especiais (acentuação)
            bool temEspeciais = mensagem.Any(c => c > 127);
            int limitePorSms = temEspeciais ? 70 : 160;

            return (int)Math.Ceiling((double)mensagem.Length / limitePorSms);
        }

        /// <summary>
        /// Calcula custo estimado de uma mensagem
        /// </summary>
        public decimal CalcularCustoEstimado(string mensagem)
        {
            int segmentos = CalcularSegmentos(mensagem);
            return segmentos * CUSTO_POR_SEGMENTO;
        }

        /// <summary>
        /// Obtém estatísticas de uso
        /// </summary>
        public EstatisticasSms ObterEstatisticas()
        {
            return historicoDAL.ObterEstatisticas();
        }

        /// <summary>
        /// Obtém configuração atual
        /// </summary>
        public TwilioConfig ObterConfiguracao()
        {
            return config;
        }
    }
}