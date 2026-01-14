// =============================================
// FORM PRINCIPAL - MENU INICIAL
// Arquivo: FormMenuPrincipal.cs
// =============================================
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cadastro1
{
    public partial class FormMenuPrincipal : Form
    {
        public FormMenuPrincipal()
        {
            InitializeComponent();
            CriarInterface();
        }

        private void CriarInterface()
        {
            this.Text = "Sistema de Cadastro de Clientes";
            this.Size = new Size(700, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 248, 255);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Label lblTitulo = new Label
            {
                Text = "SISTEMA DE CADASTRO",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204),
                Location = new Point(150, 40),
                AutoSize = true
            };

            Label lblSubtitulo = new Label
            {
                Text = "Escolha uma opção abaixo:",
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                ForeColor = Color.FromArgb(70, 70, 70),
                Location = new Point(220, 90),
                AutoSize = true
            };

            Panel panelBotoes = new Panel
            {
                Location = new Point(50, 150),
                Size = new Size(600, 320),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Button btnNovo = CriarBotaoGrande(
                "➕ CADASTRAR NOVO CLIENTE",
                new Point(100, 30),
                Color.FromArgb(46, 204, 113)
            );
            btnNovo.Click += (s, e) =>
            {
                FormCadastroCliente form = new FormCadastroCliente();
                form.ShowDialog();
            };

            Button btnBuscar = CriarBotaoGrande(
                "🔍 BUSCAR CLIENTE POR CPF",
                new Point(100, 110),
                Color.FromArgb(52, 152, 219)
            );
            btnBuscar.Click += (s, e) =>
            {
                FormBuscaCliente form = new FormBuscaCliente();
                form.ShowDialog();
            };

            Button btnListar = CriarBotaoGrande(
                "📋 VER TODOS OS CLIENTES",
                new Point(100, 190),
                Color.FromArgb(155, 89, 182)
            );
            btnListar.Click += (s, e) =>
            {
                FormListaClientes form = new FormListaClientes();
                form.ShowDialog();
            };

            Button btnSair = new Button
            {
                Text = "✖ Sair do Sistema",
                Location = new Point(220, 440),
                Size = new Size(250, 50),
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSair.FlatAppearance.BorderSize = 0;
            btnSair.Click += (s, e) =>
            {
                if (MessageBox.Show("Deseja realmente sair?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            };

            panelBotoes.Controls.AddRange(new Control[] { btnNovo, btnBuscar, btnListar });
            this.Controls.AddRange(new Control[] { lblTitulo, lblSubtitulo, panelBotoes, btnSair });
        }

        private Button CriarBotaoGrande(string texto, Point localizacao, Color cor)
        {
            Button btn = new Button
            {
                Text = texto,
                Location = localizacao,
                Size = new Size(400, 60),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                BackColor = cor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;

            btn.MouseEnter += (s, e) => { btn.BackColor = ControlPaint.Dark(cor, 0.1f); };
            btn.MouseLeave += (s, e) => { btn.BackColor = cor; };

            return btn;
        }
    }
}
