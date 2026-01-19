using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cadastro1
{
    public partial class FormAlterarSenha : Form
    {
        private UsuarioDAL usuarioDAL;

        public FormAlterarSenha()
        {
            InitializeComponent();
            usuarioDAL = new UsuarioDAL();
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validações
                if (string.IsNullOrWhiteSpace(txtSenhaAtual.Text))
                {
                    MessageBox.Show("⚠ Digite a senha atual!", "Campo Obrigatório",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSenhaAtual.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNovaSenha.Text))
                {
                    MessageBox.Show("⚠ Digite a nova senha!", "Campo Obrigatório",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNovaSenha.Focus();
                    return;
                }

                // Validar força da nova senha
                string mensagemErro;
                if (!SecurityHelper.ValidarForcaSenha(txtNovaSenha.Text, out mensagemErro))
                {
                    MessageBox.Show("⚠ " + mensagemErro, "Senha Inválida",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNovaSenha.Focus();
                    return;
                }

                if (txtNovaSenha.Text != txtConfirmarNovaSenha.Text)
                {
                    MessageBox.Show("⚠ As senhas não coincidem!", "Senhas Diferentes",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtConfirmarNovaSenha.Clear();
                    txtConfirmarNovaSenha.Focus();
                    return;
                }

                if (txtSenhaAtual.Text == txtNovaSenha.Text)
                {
                    MessageBox.Show("⚠ A nova senha deve ser diferente da senha atual!",
                        "Senha Igual", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNovaSenha.Clear();
                    txtConfirmarNovaSenha.Clear();
                    txtNovaSenha.Focus();
                    return;
                }

                // Tentar alterar senha
                btnSalvar.Enabled = false;
                btnSalvar.Text = "Salvando...";
                Application.DoEvents();

                if (usuarioDAL.AlterarSenha(
                    Usuario.UsuarioLogado.Login,
                    txtSenhaAtual.Text,
                    txtNovaSenha.Text
                ))
                {
                    MessageBox.Show(
                        "✔ SENHA ALTERADA COM SUCESSO!\n\n" +
                        "Sua senha foi atualizada com segurança.",
                        "Sucesso!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                btnSalvar.Enabled = true;
                btnSalvar.Text = "✔ SALVAR";

                MessageBox.Show(
                    "❌ " + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                txtSenhaAtual.Clear();
                txtSenhaAtual.Focus();
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}