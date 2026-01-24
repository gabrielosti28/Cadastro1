using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cadastro1
{
    public partial class FormLogin : Form
    {
        private UsuarioDAL usuarioDAL;

        public FormLogin()
        {
            InitializeComponent();
            usuarioDAL = new UsuarioDAL();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            txtUsuario.Focus();
        }

        private void TxtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtSenha.Focus();
                e.Handled = true;
            }
        }

        private void TxtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnEntrar_Click(sender, e);
                e.Handled = true;
            }
        }

        private void BtnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validações
                if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                {
                    MessageBox.Show(
                        "⚠ Digite o usuário!",
                        "Campo Obrigatório",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtUsuario.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSenha.Text))
                {
                    MessageBox.Show(
                        "⚠ Digite a senha!",
                        "Campo Obrigatório",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtSenha.Focus();
                    return;
                }

                // Desabilitar botão para evitar múltiplos cliques
                btnEntrar.Enabled = false;
                btnEntrar.Text = "Validando...";
                Application.DoEvents();

                // Tentar fazer login
                Usuario usuario = usuarioDAL.ValidarLogin(
                    txtUsuario.Text.Trim(),
                    txtSenha.Text
                );

                if (usuario != null)
                {
                    // Armazenar usuário logado
                    Usuario.UsuarioLogado = usuario;

                    // Login bem-sucedido
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                btnEntrar.Enabled = true;
                btnEntrar.Text = "🔓 ENTRAR";

                MessageBox.Show(
                    "❌ " + ex.Message,
                    "Erro no Login",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                txtSenha.Clear();
                txtSenha.Focus();
            }
        }

        private void BtnSair_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Application.Exit();
        }
    }
}