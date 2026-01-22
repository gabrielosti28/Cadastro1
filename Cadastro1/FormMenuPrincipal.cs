// =============================================
// MENU PRINCIPAL - COM BOTÃO DE BACKUP
// Arquivo: FormMenuPrincipal.cs (ATUALIZADO)
// Sistema Profissional de Cadastro
// =============================================
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cadastro1
{
    public partial class FormMenuPrincipal : Form
    {
        private Label lblUsuarioLogado;
        private Button btnAlterarSenha;
        private Button btnGerenciarBackup;

        public FormMenuPrincipal()
        {
            InitializeComponent();
            ConfigurarInterface();
            ConfigurarSeguranca();
        }

        private void ConfigurarInterface()
        {
            this.BackColor = Color.FromArgb(240, 248, 255);
        }

        private void ConfigurarSeguranca()
        {
            // Criar label para mostrar usuário logado
            lblUsuarioLogado = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204),
                Location = new Point(20, 20),
                Text = $"👤 Usuário: {Usuario.UsuarioLogado?.Nome ?? "Não identificado"}"
            };
            panelContainer.Controls.Add(lblUsuarioLogado);
            lblUsuarioLogado.BringToFront();

            // Criar botão de gerenciar backup
            btnGerenciarBackup = new Button
            {
                BackColor = Color.FromArgb(52, 152, 219),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(350, 15),
                Size = new Size(180, 35),
                Text = "💾 Gerenciar Backup",
                Cursor = Cursors.Hand
            };
            btnGerenciarBackup.FlatAppearance.BorderSize = 0;
            btnGerenciarBackup.Click += BtnGerenciarBackup_Click;
            btnGerenciarBackup.MouseEnter += Botao_MouseEnter;
            btnGerenciarBackup.MouseLeave += Botao_MouseLeave;

            panelContainer.Controls.Add(btnGerenciarBackup);
            btnGerenciarBackup.BringToFront();

            // Criar botão de alterar senha
            btnAlterarSenha = new Button
            {
                BackColor = Color.FromArgb(230, 126, 34),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(550, 15),
                Size = new Size(160, 35),
                Text = "🔑 Alterar Senha",
                Cursor = Cursors.Hand
            };
            btnAlterarSenha.FlatAppearance.BorderSize = 0;
            btnAlterarSenha.Click += BtnAlterarSenha_Click;
            btnAlterarSenha.MouseEnter += Botao_MouseEnter;
            btnAlterarSenha.MouseLeave += Botao_MouseLeave;

            panelContainer.Controls.Add(btnAlterarSenha);
            btnAlterarSenha.BringToFront();

            // Modificar o botão sair para fazer logout
            btnSair.Text = "🔒 Sair e Fazer Logout";
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
                        btn.BackColor = Color.FromArgb(46, 204, 113);
                        break;
                    case string s when s.Contains("BUSCAR"):
                        btn.BackColor = Color.FromArgb(52, 152, 219);
                        break;
                    case string s when s.Contains("VER TODOS"):
                        btn.BackColor = Color.FromArgb(155, 89, 182);
                        break;
                    case string s when s.Contains("Gerenciar Backup"):
                        btn.BackColor = Color.FromArgb(52, 152, 219);
                        break;
                    case string s when s.Contains("Alterar Senha"):
                        btn.BackColor = Color.FromArgb(230, 126, 34);
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
                // Fazer logout
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
    }
}