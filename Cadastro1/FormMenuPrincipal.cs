// =============================================
// MENU PRINCIPAL - ATUALIZADO COM CONFIGURAÇÃO DE PASTAS
// Arquivo: FormMenuPrincipal.cs (ATUALIZADO)
// LÓGICA E EVENTOS - APENAS PARTE DINÂMICA
// =============================================
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Cadastro1
{
    public partial class FormMenuPrincipal : Form
    {
        private ClienteDAL clienteDAL;
        private Button btnEnviarSms; // Declaração do novo botão

        public FormMenuPrincipal()
        {
            InitializeComponent();
            clienteDAL = new ClienteDAL();
            InicializarBotaoSms(); // Adiciona o botão após a inicialização
            CarregarAniversariantes();
            AtualizarUsuarioLogado();
        }

        private void InicializarBotaoSms()
        {
            // Criar o botão Enviar SMS
            btnEnviarSms = new Button
            {
                Text = "📱 Enviar SMS",
                Location = new Point(370, 15),
                Size = new Size(160, 35),
                BackColor = Color.FromArgb(155, 89, 182),
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                TabIndex = 5 // Mantém a ordem de tabulação
            };

            // Adicionar eventos
            btnEnviarSms.Click += BtnEnviarSms_Click;
            btnEnviarSms.MouseEnter += Botao_MouseEnter;
            btnEnviarSms.MouseLeave += Botao_MouseLeave;

            // Adicionar ao painel container
            panelContainer.Controls.Add(btnEnviarSms);
        }

        // NOVO MÉTODO: Evento do botão Enviar SMS
        private void BtnEnviarSms_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormEnviarSms form = new FormEnviarSms())
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao abrir tela de envio de SMS:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void AtualizarUsuarioLogado()
        {
            // Atualizar label do usuário logado
            lblUsuarioLogado.Text = $"👤 Usuário: {Usuario.UsuarioLogado?.Nome ?? "Não identificado"}";
        }

        private void CarregarAniversariantes()
        {
            try
            {
                // Buscar aniversariantes
                List<Cliente> aniversariantesHoje = clienteDAL.BuscarAniversariantesHoje();
                List<Cliente> aniversariantesSemana = clienteDAL.BuscarAniversariantesSemana();

                // Limpar painel (exceto o título)
                for (int i = panelAniversariantes.Controls.Count - 1; i >= 0; i--)
                {
                    if (panelAniversariantes.Controls[i] != lblTituloAniversariantes)
                    {
                        panelAniversariantes.Controls[i].Dispose();
                    }
                }

                int yPos = 50;

                // ===== ANIVERSARIANTES DE HOJE =====
                Panel panelHoje = new Panel
                {
                    Location = new Point(10, yPos),
                    Size = new Size(465, aniversariantesHoje.Count > 0 ? 30 + (aniversariantesHoje.Count * 25) : 55),
                    BackColor = Color.FromArgb(255, 243, 205),
                    BorderStyle = BorderStyle.FixedSingle
                };

                Label lblHoje = new Label
                {
                    Text = "🎉 HOJE",
                    Location = new Point(5, 5),
                    Size = new Size(455, 20),
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(133, 100, 4),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                panelHoje.Controls.Add(lblHoje);

                if (aniversariantesHoje.Count == 0)
                {
                    Label lblSemAniversariantes = new Label
                    {
                        Text = "Nenhum aniversariante hoje",
                        Location = new Point(5, 30),
                        Size = new Size(455, 20),
                        Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    panelHoje.Controls.Add(lblSemAniversariantes);
                }
                else
                {
                    int yPosHoje = 30;
                    foreach (var cliente in aniversariantesHoje)
                    {
                        int idade = ClienteDAL.CalcularIdade(cliente.DataNascimento);
                        Label lblCliente = new Label
                        {
                            Text = $"• {cliente.NomeCompleto} ({idade} anos)",
                            Location = new Point(10, yPosHoje),
                            Size = new Size(450, 20),
                            Font = new Font("Segoe UI", 9F),
                            ForeColor = Color.FromArgb(52, 73, 94),
                            Cursor = Cursors.Hand,
                            Tag = cliente
                        };
                        lblCliente.Click += (s, e) => AbrirDetalhesCliente((Cliente)((Label)s).Tag);
                        panelHoje.Controls.Add(lblCliente);
                        yPosHoje += 25;
                    }
                }

                panelAniversariantes.Controls.Add(panelHoje);
                yPos += panelHoje.Height + 15;

                // ===== ANIVERSARIANTES DA SEMANA =====
                // Remover os de hoje da lista da semana
                var aniversariantesSemanaFiltrados = aniversariantesSemana
                    .Where(c => !aniversariantesHoje.Any(h => h.ClienteID == c.ClienteID))
                    .ToList();

                Panel panelSemana = new Panel
                {
                    Location = new Point(10, yPos),
                    Size = new Size(465, Math.Min(400, aniversariantesSemanaFiltrados.Count > 0 ? 30 + (aniversariantesSemanaFiltrados.Count * 30) : 55)),
                    BackColor = Color.FromArgb(212, 237, 218),
                    BorderStyle = BorderStyle.FixedSingle,
                    AutoScroll = aniversariantesSemanaFiltrados.Count > 9
                };

                Label lblSemana = new Label
                {
                    Text = "📅 ESTA SEMANA",
                    Location = new Point(5, 5),
                    Size = new Size(455, 20),
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(21, 87, 36),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                panelSemana.Controls.Add(lblSemana);

                if (aniversariantesSemanaFiltrados.Count == 0)
                {
                    Label lblSemAniversariantes = new Label
                    {
                        Text = "Nenhum aniversariante esta semana",
                        Location = new Point(5, 30),
                        Size = new Size(455, 20),
                        Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    panelSemana.Controls.Add(lblSemAniversariantes);
                }
                else
                {
                    int yPosSemana = 30;
                    foreach (var cliente in aniversariantesSemanaFiltrados)
                    {
                        DateTime proximoAniversario = new DateTime(
                            DateTime.Now.Year,
                            cliente.DataNascimento.Month,
                            cliente.DataNascimento.Day
                        );

                        if (proximoAniversario < DateTime.Now)
                            proximoAniversario = proximoAniversario.AddYears(1);

                        int idade = ClienteDAL.CalcularIdade(cliente.DataNascimento);
                        string diaSemana = ObterDiaSemanaAbreviado(proximoAniversario);

                        Label lblCliente = new Label
                        {
                            Text = $"• {cliente.NomeCompleto}",
                            Location = new Point(10, yPosSemana),
                            Size = new Size(445, 15),
                            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                            ForeColor = Color.FromArgb(52, 73, 94),
                            Cursor = Cursors.Hand,
                            Tag = cliente
                        };
                        lblCliente.Click += (s, e) => AbrirDetalhesCliente((Cliente)((Label)s).Tag);

                        Label lblData = new Label
                        {
                            Text = $"  {diaSemana}, {proximoAniversario:dd/MM} ({idade} anos)",
                            Location = new Point(10, yPosSemana + 15),
                            Size = new Size(445, 15),
                            Font = new Font("Segoe UI", 8F, FontStyle.Italic),
                            ForeColor = Color.FromArgb(100, 100, 100)
                        };

                        panelSemana.Controls.Add(lblCliente);
                        panelSemana.Controls.Add(lblData);
                        yPosSemana += 30;
                    }
                }

                panelAniversariantes.Controls.Add(panelSemana);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao carregar aniversariantes:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private string ObterDiaSemanaAbreviado(DateTime data)
        {
            switch (data.DayOfWeek)
            {
                case DayOfWeek.Sunday: return "Dom";
                case DayOfWeek.Monday: return "Seg";
                case DayOfWeek.Tuesday: return "Ter";
                case DayOfWeek.Wednesday: return "Qua";
                case DayOfWeek.Thursday: return "Qui";
                case DayOfWeek.Friday: return "Sex";
                case DayOfWeek.Saturday: return "Sáb";
                default: return "";
            }
        }

        private void AbrirDetalhesCliente(Cliente cliente)
        {
            try
            {
                string idade = ClienteDAL.CalcularIdade(cliente.DataNascimento).ToString();
                DateTime proximoAniversario = new DateTime(
                    DateTime.Now.Year,
                    cliente.DataNascimento.Month,
                    cliente.DataNascimento.Day
                );

                if (proximoAniversario < DateTime.Now)
                    proximoAniversario = proximoAniversario.AddYears(1);

                int diasFaltando = (proximoAniversario - DateTime.Now).Days;

                string mensagem = $"🎂 ANIVERSARIANTE\n\n" +
                    $"Nome: {cliente.NomeCompleto}\n" +
                    $"Data de Nascimento: {cliente.DataNascimento:dd/MM/yyyy}\n" +
                    $"Idade: {idade} anos\n" +
                    $"Telefone: {(string.IsNullOrEmpty(cliente.Telefone) ? "Não cadastrado" : cliente.Telefone)}\n" +
                    $"Cidade: {cliente.Cidade}\n\n";

                if (diasFaltando == 0)
                    mensagem += "🎉 FAZ ANIVERSÁRIO HOJE!";
                else
                    mensagem += $"📅 Faltam {diasFaltando} dia{(diasFaltando > 1 ? "s" : "")} para o aniversário";

                DialogResult resultado = MessageBox.Show(
                    mensagem,
                    "Detalhes do Aniversariante",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao exibir detalhes:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void BtnGerenciarBackup_Click(object sender, EventArgs e)
        {
            using (FormGerenciarBackup formBackup = new FormGerenciarBackup())
            {
                formBackup.ShowDialog();
            }
        }

        private void BtnAlterarSenha_Click(object sender, EventArgs e)
        {
            using (FormAlterarSenha formSenha = new FormAlterarSenha())
            {
                formSenha.ShowDialog();
            }
        }

        private void BtnMalaDireta_Click(object sender, EventArgs e)
        {
            using (FormMailingEditor form = new FormMailingEditor())
            {
                form.ShowDialog();
            }
        }

        private void BtnConfigurarPastas_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormConfigurarDiretorios form = new FormConfigurarDiretorios())
                {
                    DialogResult result = form.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        // Configurações salvas com sucesso
                        MessageBox.Show(
                            "✅ As novas configurações de pastas já estão ativas!\n\n" +
                            "Todos os novos arquivos serão salvos nos locais configurados.",
                            "Configuração Aplicada",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao abrir configuração de pastas:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void Botao_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                Color corAtual = btn.BackColor;
                btn.BackColor = ControlPaint.Dark(corAtual, 0.1f);
            }
        }

        private void Botao_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                switch (btn.Text)
                {
                    case string s when s.Contains("CADASTRAR"):
                        btn.BackColor = Color.FromArgb(230, 126, 34);
                        break;
                    case string s when s.Contains("BUSCAR"):
                        btn.BackColor = Color.FromArgb(52, 152, 219);
                        break;
                    case string s when s.Contains("VER TODOS"):
                        btn.BackColor = Color.FromArgb(46, 204, 113);
                        break;
                    case string s when s.Contains("IMPORTAR"):
                        btn.BackColor = Color.FromArgb(155, 89, 182);
                        break;
                    case string s when s.Contains("Gerenciar Backup"):
                        btn.BackColor = Color.FromArgb(52, 152, 219);
                        break;
                    case string s when s.Contains("Alterar Senha"):
                        btn.BackColor = Color.FromArgb(230, 126, 34);
                        break;
                    case string s when s.Contains("Mala Direta"):
                        btn.BackColor = Color.FromArgb(41, 128, 185);
                        break;
                    case string s when s.Contains("Configurar Pastas"):
                        btn.BackColor = Color.FromArgb(142, 68, 173);
                        break;
                    case string s when s.Contains("Enviar SMS"):
                        btn.BackColor = Color.FromArgb(155, 89, 182);
                        break;
                    case string s when s.Contains("Sair"):
                        btn.BackColor = Color.FromArgb(231, 76, 60);
                        break;
                }
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            FormCadastroCliente form = new FormCadastroCliente();
            form.ShowDialog();
            // Recarregar aniversariantes após cadastro
            CarregarAniversariantes();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            FormBuscaCliente form = new FormBuscaCliente();
            form.ShowDialog();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            FormListaClientes form = new FormListaClientes();
            form.ShowDialog();
        }

        private void btnImportarLote_Click(object sender, EventArgs e)
        {
            try
            {
                FormImportarClientesLote formImportar = new FormImportarClientesLote();

                if (formImportar.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show(
                        "✅ Importação concluída com sucesso!\n\n" +
                        "Os clientes foram cadastrados no sistema.",
                        "Sucesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Recarregar aniversariantes após importação
                    CarregarAniversariantes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao abrir importação:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "Deseja realmente sair do sistema?\n\n" +
                "Você precisará fazer login novamente.\n\n" +
                "💾 Lembre-se: O sistema faz backups automáticos diários,\n" +
                "mas você pode fazer backups manuais em 'Gerenciar Backup'.",
                "Confirmar Saída",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                Usuario.Logout();

                MessageBox.Show(
                    "✓ Logout realizado com sucesso!",
                    "Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Application.Exit();
            }
        }

        private void FormMenuPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                ConfiguracaoPastas.GarantirPastasExistem();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Aviso: Não foi possível criar algumas pastas.\n{ex.Message}",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}