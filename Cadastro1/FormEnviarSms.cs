// =============================================
// FORMULÁRIO - ENVIO DE SMS EM MASSA
// Arquivo: FormEnviarSms.cs
// Sistema profissional de envio de mensagens
// CORRIGIDO: Removido PlaceholderText (incompatível com .NET Framework 4.7.2)
// =============================================
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cadastro1
{
    public partial class FormEnviarSms : Form
    {
        private SmsService smsService;
        private ClienteDAL clienteDAL;
        private MensagemTemplateDAL templateDAL;
        private List<Cliente> todosClientes;
        private List<Cliente> clientesFiltrados;

        // Controles da UI
        private TabControl tabControl;
        private TabPage tabEnviar;
        private TabPage tabTemplates;
        private TabPage tabHistorico;
        private TabPage tabConfig;

        // Tab Enviar
        private TextBox txtBusca;
        private CheckedListBox chkClientes;
        private ComboBox cmbTemplates;
        private TextBox txtMensagem;
        private Label lblContador;
        private Label lblCusto;
        private Label lblSegmentos;
        private Button btnEnviar;
        private Button btnMarcarTodos;
        private Button btnLimpar;
        private ProgressBar progressBar;
        private Label lblStatus;

        // Tab Templates
        private ListBox lstTemplates;
        private TextBox txtNomeTemplate;
        private TextBox txtConteudoTemplate;
        private Button btnSalvarTemplate;
        private Button btnExcluirTemplate;
        private Button btnNovoTemplate;

        // Tab Histórico
        private DataGridView dgvHistorico;
        private DateTimePicker dtpInicio;
        private DateTimePicker dtpFim;
        private Button btnFiltrar;
        private Label lblEstatisticas;

        // Tab Configuração
        private TextBox txtAccountSID;
        private TextBox txtAuthToken;
        private TextBox txtNumeroTwilio;
        private Button btnSalvarConfig;
        private Button btnTestarConexao;
        private Label lblStatusConfig;

        public FormEnviarSms()
        {
            InitializeComponent();
            InicializarServicos();
            CarregarDados();
        }

        private void InitializeComponent()
        {
            // Configuração do Form
            this.Text = "Envio de SMS em Massa";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 248, 255);

            // TabControl Principal
            tabControl = new TabControl
            {
                Location = new Point(10, 10),
                Size = new Size(1170, 740),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            // Criar abas
            CriarTabEnviar();
            CriarTabTemplates();
            CriarTabHistorico();
            CriarTabConfiguracao();

            this.Controls.Add(tabControl);
        }

        // ========== TAB ENVIAR ==========
        private void CriarTabEnviar()
        {
            tabEnviar = new TabPage("📱 Enviar SMS");
            tabEnviar.BackColor = Color.White;

            // Busca
            Label lblBusca = new Label
            {
                Text = "Buscar clientes (Nome, CPF ou Cidade):",
                Location = new Point(20, 20),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            txtBusca = new TextBox
            {
                Location = new Point(20, 50),
                Size = new Size(400, 25),
                Font = new Font("Segoe UI", 10F)
            };
            txtBusca.TextChanged += TxtBusca_TextChanged;

            btnMarcarTodos = new Button
            {
                Text = "Marcar Todos",
                Location = new Point(430, 50),
                Size = new Size(120, 25),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnMarcarTodos.Click += BtnMarcarTodos_Click;

            btnLimpar = new Button
            {
                Text = "Limpar Busca",
                Location = new Point(560, 50),
                Size = new Size(120, 25),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnLimpar.Click += BtnLimpar_Click;

            // Lista de clientes
            chkClientes = new CheckedListBox
            {
                Location = new Point(20, 90),
                Size = new Size(680, 450),
                Font = new Font("Consolas", 9F),
                CheckOnClick = true
            };
            chkClientes.ItemCheck += ChkClientes_ItemCheck;

            // Painel de mensagem
            Label lblTemplate = new Label
            {
                Text = "Template:",
                Location = new Point(720, 20),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            cmbTemplates = new ComboBox
            {
                Location = new Point(720, 50),
                Size = new Size(420, 25),
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbTemplates.SelectedIndexChanged += CmbTemplates_SelectedIndexChanged;

            Label lblMensagem = new Label
            {
                Text = "Mensagem:",
                Location = new Point(720, 85),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            txtMensagem = new TextBox
            {
                Location = new Point(720, 115),
                Size = new Size(420, 250),
                Multiline = true,
                Font = new Font("Segoe UI", 10F),
                ScrollBars = ScrollBars.Vertical
            };
            txtMensagem.TextChanged += TxtMensagem_TextChanged;

            lblContador = new Label
            {
                Text = "Caracteres: 0",
                Location = new Point(720, 375),
                Size = new Size(200, 20),
                Font = new Font("Segoe UI", 9F)
            };

            lblSegmentos = new Label
            {
                Text = "Segmentos: 0",
                Location = new Point(920, 375),
                Size = new Size(200, 20),
                Font = new Font("Segoe UI", 9F)
            };

            lblCusto = new Label
            {
                Text = "Custo estimado: R$ 0,00",
                Location = new Point(720, 400),
                Size = new Size(400, 25),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(230, 126, 34)
            };

            // Botão Enviar
            btnEnviar = new Button
            {
                Text = "📱 ENVIAR SMS",
                Location = new Point(720, 440),
                Size = new Size(420, 50),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };
            btnEnviar.Click += BtnEnviar_Click;

            // Barra de progresso
            progressBar = new ProgressBar
            {
                Location = new Point(720, 500),
                Size = new Size(420, 25),
                Visible = false
            };

            lblStatus = new Label
            {
                Location = new Point(20, 550),
                Size = new Size(1120, 140),
                Font = new Font("Consolas", 9F),
                BackColor = Color.FromArgb(236, 240, 241),
                BorderStyle = BorderStyle.FixedSingle,
                Text = "Aguardando configuração..."
            };

            // Adicionar controles
            tabEnviar.Controls.AddRange(new Control[] {
                lblBusca, txtBusca, btnMarcarTodos, btnLimpar,
                chkClientes, lblTemplate, cmbTemplates,
                lblMensagem, txtMensagem, lblContador, lblSegmentos,
                lblCusto, btnEnviar, progressBar, lblStatus
            });

            tabControl.TabPages.Add(tabEnviar);
        }

        // ========== TAB TEMPLATES ==========
        private void CriarTabTemplates()
        {
            tabTemplates = new TabPage("📝 Templates");
            tabTemplates.BackColor = Color.White;

            Label lblLista = new Label
            {
                Text = "Templates salvos:",
                Location = new Point(20, 20),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            lstTemplates = new ListBox
            {
                Location = new Point(20, 50),
                Size = new Size(400, 550),
                Font = new Font("Segoe UI", 10F)
            };
            lstTemplates.SelectedIndexChanged += LstTemplates_SelectedIndexChanged;

            Label lblNome = new Label
            {
                Text = "Nome do template:",
                Location = new Point(440, 20),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            txtNomeTemplate = new TextBox
            {
                Location = new Point(440, 50),
                Size = new Size(680, 25),
                Font = new Font("Segoe UI", 10F)
            };

            Label lblConteudo = new Label
            {
                Text = "Conteúdo:",
                Location = new Point(440, 85),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            txtConteudoTemplate = new TextBox
            {
                Location = new Point(440, 115),
                Size = new Size(680, 400),
                Multiline = true,
                Font = new Font("Segoe UI", 10F),
                ScrollBars = ScrollBars.Vertical
            };

            btnSalvarTemplate = new Button
            {
                Text = "💾 Salvar Template",
                Location = new Point(440, 530),
                Size = new Size(220, 40),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnSalvarTemplate.Click += BtnSalvarTemplate_Click;

            btnNovoTemplate = new Button
            {
                Text = "➕ Novo",
                Location = new Point(670, 530),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnNovoTemplate.Click += BtnNovoTemplate_Click;

            btnExcluirTemplate = new Button
            {
                Text = "🗑️ Excluir",
                Location = new Point(830, 530),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnExcluirTemplate.Click += BtnExcluirTemplate_Click;

            Label lblAjuda = new Label
            {
                Text = "💡 Variáveis disponíveis: {NOME}, {CIDADE}, {TELEFONE}, {CPF}, {DATA}, {HORA}",
                Location = new Point(440, 580),
                Size = new Size(680, 25),
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            tabTemplates.Controls.AddRange(new Control[] {
                lblLista, lstTemplates, lblNome, txtNomeTemplate,
                lblConteudo, txtConteudoTemplate, btnSalvarTemplate,
                btnNovoTemplate, btnExcluirTemplate, lblAjuda
            });

            tabControl.TabPages.Add(tabTemplates);
        }

        // ========== TAB HISTÓRICO ==========
        private void CriarTabHistorico()
        {
            tabHistorico = new TabPage("📊 Histórico");
            tabHistorico.BackColor = Color.White;

            Label lblPeriodo = new Label
            {
                Text = "Período:",
                Location = new Point(20, 20),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            dtpInicio = new DateTimePicker
            {
                Location = new Point(120, 20),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10F)
            };
            dtpInicio.Value = DateTime.Now.AddMonths(-1);

            Label lblAte = new Label
            {
                Text = "até",
                Location = new Point(330, 20),
                Size = new Size(30, 25),
                Font = new Font("Segoe UI", 10F)
            };

            dtpFim = new DateTimePicker
            {
                Location = new Point(370, 20),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10F)
            };

            btnFiltrar = new Button
            {
                Text = "🔍 Filtrar",
                Location = new Point(580, 20),
                Size = new Size(120, 25),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnFiltrar.Click += BtnFiltrar_Click;

            lblEstatisticas = new Label
            {
                Location = new Point(720, 20),
                Size = new Size(420, 150),
                Font = new Font("Consolas", 9F),
                BackColor = Color.FromArgb(236, 240, 241),
                BorderStyle = BorderStyle.FixedSingle,
                Text = "Estatísticas carregando..."
            };

            dgvHistorico = new DataGridView
            {
                Location = new Point(20, 180),
                Size = new Size(1120, 510),
                Font = new Font("Segoe UI", 9F),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            tabHistorico.Controls.AddRange(new Control[] {
                lblPeriodo, dtpInicio, lblAte, dtpFim, btnFiltrar,
                lblEstatisticas, dgvHistorico
            });

            tabControl.TabPages.Add(tabHistorico);
        }

        // ========== TAB CONFIGURAÇÃO ==========
        private void CriarTabConfiguracao()
        {
            tabConfig = new TabPage("⚙️ Configuração");
            tabConfig.BackColor = Color.White;

            Label lblTitulo = new Label
            {
                Text = "Configuração do Twilio",
                Location = new Point(20, 20),
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            Label lblInstrucoes = new Label
            {
                Text = "Para enviar SMS, você precisa de uma conta Twilio.\n" +
                       "1. Acesse: https://www.twilio.com/try-twilio\n" +
                       "2. Crie sua conta gratuita\n" +
                       "3. Obtenha suas credenciais no Console\n" +
                       "4. Preencha os campos abaixo",
                Location = new Point(20, 60),
                Size = new Size(1100, 80),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(255, 243, 205),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblAccountSID = new Label
            {
                Text = "Account SID:",
                Location = new Point(20, 160),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            txtAccountSID = new TextBox
            {
                Location = new Point(180, 160),
                Size = new Size(500, 25),
                Font = new Font("Segoe UI", 10F)
            };

            Label lblAuthToken = new Label
            {
                Text = "Auth Token:",
                Location = new Point(20, 200),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            txtAuthToken = new TextBox
            {
                Location = new Point(180, 200),
                Size = new Size(500, 25),
                Font = new Font("Segoe UI", 10F),
                PasswordChar = '*'
            };

            Label lblNumero = new Label
            {
                Text = "Número Twilio:",
                Location = new Point(20, 240),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            // CORRIGIDO: Removido PlaceholderText
            txtNumeroTwilio = new TextBox
            {
                Location = new Point(180, 240),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10F)
                // PlaceholderText NÃO EXISTE no .NET Framework 4.7.2
            };

            // Adicionar Label de ajuda em vez de PlaceholderText
            Label lblNumeroAjuda = new Label
            {
                Text = "Formato: +5511999999999",
                Location = new Point(390, 240),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Color.Gray
            };

            btnSalvarConfig = new Button
            {
                Text = "💾 Salvar Configuração",
                Location = new Point(20, 290),
                Size = new Size(200, 40),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnSalvarConfig.Click += BtnSalvarConfig_Click;

            btnTestarConexao = new Button
            {
                Text = "🔍 Testar Conexão",
                Location = new Point(230, 290),
                Size = new Size(200, 40),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnTestarConexao.Click += BtnTestarConexao_Click;

            lblStatusConfig = new Label
            {
                Location = new Point(20, 350),
                Size = new Size(660, 250),
                Font = new Font("Consolas", 9F),
                BackColor = Color.FromArgb(236, 240, 241),
                BorderStyle = BorderStyle.FixedSingle,
                Text = "Status: Aguardando configuração..."
            };

            tabConfig.Controls.AddRange(new Control[] {
                lblTitulo, lblInstrucoes, lblAccountSID, txtAccountSID,
                lblAuthToken, txtAuthToken, lblNumero, txtNumeroTwilio,
                lblNumeroAjuda, // ← ADICIONADO: Label de ajuda
                btnSalvarConfig, btnTestarConexao, lblStatusConfig
            });

            tabControl.TabPages.Add(tabConfig);
        }

        // Continua na próxima parte...

        private void InicializarServicos()
        {
            smsService = new SmsService();
            clienteDAL = new ClienteDAL();
            templateDAL = new MensagemTemplateDAL();
            todosClientes = new List<Cliente>();
            clientesFiltrados = new List<Cliente>();
        }

        private void CarregarDados()
        {
            // Verificar se Twilio está configurado
            if (!smsService.EstaConfigurado())
            {
                lblStatus.Text = "⚠️ ATENÇÃO: Configure o Twilio na aba 'Configuração' antes de enviar SMS!";
                lblStatus.ForeColor = Color.FromArgb(231, 76, 60);
                tabControl.SelectedTab = tabConfig;
                return;
            }

            lblStatus.Text = "✓ Twilio configurado. Pronto para enviar SMS!";
            lblStatus.ForeColor = Color.FromArgb(46, 204, 113);

            // Carregar templates
            CarregarTemplates();

            // Carregar estatísticas
            AtualizarEstatisticas();

            // Carregar configuração
            CarregarConfiguracao();
        }

        private void TxtBusca_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBusca.Text.Trim();

            if (filtro.Length < 3)
            {
                chkClientes.Items.Clear();
                return;
            }

            // Buscar clientes
            clientesFiltrados = clienteDAL.BuscarClientesPorFiltro(filtro, 500)
                .Where(c => !string.IsNullOrWhiteSpace(c.Telefone))
                .ToList();

            AtualizarListaClientes();
        }

        private void AtualizarListaClientes()
        {
            chkClientes.Items.Clear();

            foreach (var cliente in clientesFiltrados)
            {
                string item = $"{cliente.CPF.PadRight(15)} | {cliente.NomeCompleto.PadRight(40)} | {cliente.Telefone}";
                chkClientes.Items.Add(item, false);
            }

            AtualizarContadores();
        }

        private void ChkClientes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke(new Action(() => AtualizarContadores()));
        }

        private void BtnMarcarTodos_Click(object sender, EventArgs e)
        {
            bool marcar = chkClientes.CheckedItems.Count < chkClientes.Items.Count / 2;

            for (int i = 0; i < chkClientes.Items.Count; i++)
            {
                chkClientes.SetItemChecked(i, marcar);
            }
        }

        private void BtnLimpar_Click(object sender, EventArgs e)
        {
            txtBusca.Clear();
            chkClientes.Items.Clear();
        }

        private void CmbTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTemplates.SelectedItem is MensagemTemplate template)
            {
                txtMensagem.Text = template.Conteudo;
            }
        }

        private void TxtMensagem_TextChanged(object sender, EventArgs e)
        {
            AtualizarContadores();
        }

        private void AtualizarContadores()
        {
            int clientesSelecionados = chkClientes.CheckedItems.Count;
            string mensagem = txtMensagem.Text;

            // Atualizar contador de caracteres
            lblContador.Text = $"Caracteres: {mensagem.Length}";

            // Calcular segmentos
            int segmentos = smsService.CalcularSegmentos(mensagem);
            lblSegmentos.Text = $"Segmentos: {segmentos} SMS";

            // Calcular custo
            decimal custoUnitario = smsService.CalcularCustoEstimado(mensagem);
            decimal custoTotal = custoUnitario * clientesSelecionados;

            lblCusto.Text = $"Custo estimado: R$ {custoTotal:F2} ({clientesSelecionados} clientes × R$ {custoUnitario:F2})";

            // Habilitar/desabilitar botão enviar
            btnEnviar.Enabled = clientesSelecionados > 0 &&
                                !string.IsNullOrWhiteSpace(mensagem) &&
                                smsService.EstaConfigurado();

            // Atualizar cor do custo baseado no valor
            if (custoTotal > 50)
                lblCusto.ForeColor = Color.FromArgb(231, 76, 60); // Vermelho
            else if (custoTotal > 20)
                lblCusto.ForeColor = Color.FromArgb(230, 126, 34); // Laranja
            else
                lblCusto.ForeColor = Color.FromArgb(46, 204, 113); // Verde
        }

        private async void BtnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obter clientes selecionados
                List<Cliente> clientesSelecionados = new List<Cliente>();

                foreach (int index in chkClientes.CheckedIndices)
                {
                    if (index < clientesFiltrados.Count)
                    {
                        clientesSelecionados.Add(clientesFiltrados[index]);
                    }
                }

                if (clientesSelecionados.Count == 0)
                {
                    MessageBox.Show(
                        "Selecione pelo menos um cliente!",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Confirmação
                decimal custoTotal = smsService.CalcularCustoEstimado(txtMensagem.Text) * clientesSelecionados.Count;

                DialogResult confirmacao = MessageBox.Show(
                    $"📱 CONFIRMAR ENVIO DE SMS\n\n" +
                    $"Clientes: {clientesSelecionados.Count}\n" +
                    $"Custo estimado: R$ {custoTotal:F2}\n\n" +
                    $"Deseja continuar?",
                    "Confirmar Envio",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacao != DialogResult.Yes)
                    return;

                // Desabilitar controles
                HabilitarControles(false);
                progressBar.Visible = true;
                progressBar.Style = ProgressBarStyle.Marquee;
                lblStatus.Text = "⏳ Enviando SMS...";

                // Enviar
                var resultado = await smsService.EnviarSmsLote(clientesSelecionados, txtMensagem.Text);

                // Habilitar controles
                HabilitarControles(true);
                progressBar.Visible = false;

                // Mostrar resultado
                lblStatus.Text = resultado.GerarRelatorio();

                MessageBox.Show(
                    $"✓ Envio concluído!\n\n" +
                    $"Sucessos: {resultado.Sucessos}\n" +
                    $"Falhas: {resultado.Falhas}\n" +
                    $"Custo total: R$ {resultado.CustoTotal:F2}",
                    "Envio Concluído",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Atualizar estatísticas
                AtualizarEstatisticas();

                // Registrar utilização do template
                if (cmbTemplates.SelectedItem is MensagemTemplate template)
                {
                    templateDAL.RegistrarUtilizacao(template.TemplateID);
                }
            }
            catch (Exception ex)
            {
                HabilitarControles(true);
                progressBar.Visible = false;

                MessageBox.Show(
                    $"Erro ao enviar SMS:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void HabilitarControles(bool habilitar)
        {
            txtBusca.Enabled = habilitar;
            chkClientes.Enabled = habilitar;
            cmbTemplates.Enabled = habilitar;
            txtMensagem.Enabled = habilitar;
            btnEnviar.Enabled = habilitar && chkClientes.CheckedItems.Count > 0;
            btnMarcarTodos.Enabled = habilitar;
            btnLimpar.Enabled = habilitar;
        }

        // ========== EVENTOS TAB TEMPLATES ==========

        private void CarregarTemplates()
        {
            try
            {
                var templates = templateDAL.ListarTemplates();

                // Atualizar ComboBox da aba Enviar
                cmbTemplates.Items.Clear();
                cmbTemplates.Items.Add("(Selecione um template)");
                foreach (var t in templates)
                {
                    cmbTemplates.Items.Add(t);
                }
                cmbTemplates.DisplayMember = "Nome";
                cmbTemplates.SelectedIndex = 0;

                // Atualizar ListBox da aba Templates
                lstTemplates.Items.Clear();
                foreach (var t in templates)
                {
                    lstTemplates.Items.Add(t);
                }
                lstTemplates.DisplayMember = "Nome";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar templates: {ex.Message}");
            }
        }

        private void LstTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTemplates.SelectedItem is MensagemTemplate template)
            {
                txtNomeTemplate.Text = template.Nome;
                txtConteudoTemplate.Text = template.Conteudo;
            }
        }

        private void BtnSalvarTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNomeTemplate.Text))
                {
                    MessageBox.Show("Digite um nome para o template!", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtConteudoTemplate.Text))
                {
                    MessageBox.Show("Digite o conteúdo da mensagem!", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MensagemTemplate template = new MensagemTemplate
                {
                    Nome = txtNomeTemplate.Text.Trim(),
                    Conteudo = txtConteudoTemplate.Text.Trim()
                };

                // Se tem template selecionado, atualizar
                if (lstTemplates.SelectedItem is MensagemTemplate templateSelecionado)
                {
                    template.TemplateID = templateSelecionado.TemplateID;
                }

                var validacao = template.Validar();
                if (!validacao.valido)
                {
                    MessageBox.Show(validacao.mensagemErro, "Validação",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                templateDAL.SalvarTemplate(template);

                MessageBox.Show("✓ Template salvo!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CarregarTemplates();
                BtnNovoTemplate_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNovoTemplate_Click(object sender, EventArgs e)
        {
            txtNomeTemplate.Clear();
            txtConteudoTemplate.Clear();
            lstTemplates.ClearSelected();
            txtNomeTemplate.Focus();
        }

        private void BtnExcluirTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstTemplates.SelectedItem is MensagemTemplate template)
                {
                    DialogResult confirmacao = MessageBox.Show(
                        $"Excluir template '{template.Nome}'?",
                        "Confirmar",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (confirmacao == DialogResult.Yes)
                    {
                        templateDAL.ExcluirTemplate(template.TemplateID);
                        MessageBox.Show("✓ Template excluído!", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CarregarTemplates();
                        BtnNovoTemplate_Click(null, null);
                    }
                }
                else
                {
                    MessageBox.Show("Selecione um template para excluir!", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== EVENTOS TAB HISTÓRICO ==========

        private void BtnFiltrar_Click(object sender, EventArgs e)
        {
            CarregarHistorico();
        }

        private void CarregarHistorico()
        {
            try
            {
                var historicoDAL = new EnvioSmsHistoricoDAL();
                var historico = historicoDAL.ListarEnviosPorPeriodo(
                    dtpInicio.Value.Date,
                    dtpFim.Value.Date.AddDays(1).AddSeconds(-1)
                );

                dgvHistorico.DataSource = historico;

                if (dgvHistorico.Columns.Count > 0)
                {
                    // Ocultar colunas
                    dgvHistorico.Columns["EnvioID"].Visible = false;
                    dgvHistorico.Columns["SIDTwilio"].Visible = false;
                    dgvHistorico.Columns["Resumo"].Visible = false;

                    // Configurar colunas visíveis
                    dgvHistorico.Columns["IconeStatus"].HeaderText = "";
                    dgvHistorico.Columns["IconeStatus"].Width = 40;

                    dgvHistorico.Columns["DataHoraEnvio"].HeaderText = "Data/Hora";
                    dgvHistorico.Columns["NomeCliente"].HeaderText = "Cliente";
                    dgvHistorico.Columns["TelefoneDestino"].HeaderText = "Telefone";
                    dgvHistorico.Columns["Mensagem"].HeaderText = "Mensagem";
                    dgvHistorico.Columns["QuantidadeSegmentos"].HeaderText = "Segmentos";
                    dgvHistorico.Columns["CustoEstimado"].HeaderText = "Custo";
                    dgvHistorico.Columns["DescricaoStatus"].HeaderText = "Status";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar histórico: {ex.Message}");
            }
        }

        private void AtualizarEstatisticas()
        {
            try
            {
                var stats = smsService.ObterEstatisticas();
                lblEstatisticas.Text = stats.ResumoMes;
            }
            catch
            {
                lblEstatisticas.Text = "Erro ao carregar estatísticas";
            }
        }

        // ========== EVENTOS TAB CONFIGURAÇÃO ==========

        private void CarregarConfiguracao()
        {
            try
            {
                var config = smsService.ObterConfiguracao();

                txtAccountSID.Text = config.AccountSID ?? "";
                txtAuthToken.Text = config.AuthToken ?? "";
                txtNumeroTwilio.Text = config.NumeroOrigem ?? "";

                if (config.EstaConfigurado())
                {
                    lblStatusConfig.Text = "✓ Twilio configurado!\n\n" +
                        "Você pode começar a enviar SMS.";
                    lblStatusConfig.ForeColor = Color.FromArgb(46, 204, 113);
                }
                else
                {
                    lblStatusConfig.Text = "⚠️ Configuração incompleta!\n\n" +
                        "Preencha todos os campos e salve.";
                    lblStatusConfig.ForeColor = Color.FromArgb(231, 76, 60);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        private void BtnSalvarConfig_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtAccountSID.Text) ||
                    string.IsNullOrWhiteSpace(txtAuthToken.Text) ||
                    string.IsNullOrWhiteSpace(txtNumeroTwilio.Text))
                {
                    MessageBox.Show(
                        "Preencha todos os campos!",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var config = new TwilioConfig
                {
                    AccountSID = txtAccountSID.Text.Trim(),
                    AuthToken = txtAuthToken.Text.Trim(),
                    NumeroOrigem = txtNumeroTwilio.Text.Trim()
                };

                smsService.SalvarConfiguracao(config);

                MessageBox.Show(
                    "✓ Configuração salva!\n\nClique em 'Testar Conexão' para verificar.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CarregarConfiguracao();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnTestarConexao_Click(object sender, EventArgs e)
        {
            try
            {
                lblStatusConfig.Text = "⏳ Testando conexão...";
                Application.DoEvents();

                var resultado = smsService.TestarConexao();

                if (resultado.sucesso)
                {
                    lblStatusConfig.Text = $"✓ CONEXÃO OK!\n\n{resultado.mensagem}";
                    lblStatusConfig.ForeColor = Color.FromArgb(46, 204, 113);

                    MessageBox.Show(
                        resultado.mensagem,
                        "Conexão OK",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Habilitar envio
                    CarregarDados();
                }
                else
                {
                    lblStatusConfig.Text = $"✗ FALHA NA CONEXÃO\n\n{resultado.mensagem}";
                    lblStatusConfig.ForeColor = Color.FromArgb(231, 76, 60);

                    MessageBox.Show(
                        resultado.mensagem,
                        "Erro na Conexão",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}