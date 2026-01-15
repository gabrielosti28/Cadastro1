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
            ConfigurarInterface();
        }

        private void ConfigurarInterface()
        {
            // Configurações visuais básicas
            this.BackColor = Color.FromArgb(240, 248, 255);
        }

        private void Botao_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                // Escurece a cor do botão em 10%
                Color corAtual = btn.BackColor;
                btn.BackColor = ControlPaint.Dark(corAtual, 0.1f);
            }
        }

        private void Botao_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                // Retorna à cor original baseada no texto do botão
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
            if (MessageBox.Show("Deseja realmente sair?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}