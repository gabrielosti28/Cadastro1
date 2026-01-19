using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cadastro1
{
    public partial class FormPrimeiroAcesso : Form
    {
        private UsuarioDAL usuarioDAL;

        public FormPrimeiroAcesso()
        {
            InitializeComponent();
            usuarioDAL = new UsuarioDAL();
        }

        private void BtnCriar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validações
                if (string.IsNullOrWhiteSpace(txtNome.Text))
                {
                    MessageBox.Show("⚠ Digite seu nome completo!", "Campo Obrigatório",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNome.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtLogin.Text))
                {
                    MessageBox.Show("⚠ Digite um nome de usuário!", "Campo Obrigatório",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLogin.Focus();
                    return;
                }

                if (txtLogin.Text.Length < 3)
                {
                    MessageBox.Show("⚠ O nome de usuário deve ter no mínimo 3 caracteres!",
                        "Usuário Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLogin.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSenha.Text))
                {
                    MessageBox.Show("⚠ Digite uma senha!", "Campo Obrigatório",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSenha.Focus();
                    return;
                }

                // Validar força da senha
                string mensagemErro;
                if (!SecurityHelper.ValidarForcaSenha(txtSenha.Text, out mensagemErro))
                {
                    MessageBox.Show("⚠ " + mensagemErro, "Senha Inválida",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSenha.Focus();
                    return;
                }

                if (txtSenha.Text != txtConfirmarSenha.Text)
                {
                    MessageBox.Show("⚠ As senhas não coincidem!\n\nDigite a mesma senha nos dois campos.",
                        "Senhas Diferentes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtConfirmarSenha.Clear();
                    txtConfirmarSenha.Focus();
                    return;
                }

                // Confirmação
                DialogResult confirmacao = MessageBox.Show(
                    "⚠ ATENÇÃO - CONFIGURAÇÃO IMPORTANTE!\n\n" +
                    $"Nome: {txtNome.Text}\n" +
                    $"Usuário: {txtLogin.Text}\n\n" +
                    "Após criar este usuário, ANOTE as credenciais em local seguro!\n\n" +
                    "Deseja continuar?",
                    "Confirmar Criação",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacao == DialogResult.No)
                    return;

                // Criar usuário
                btnCriar.Enabled = false;
                btnCriar.Text = "Criando...";
                Application.DoEvents();

                if (usuarioDAL.CriarPrimeiroUsuario(
                    txtLogin.Text.Trim(),
                    txtSenha.Text,
                    txtNome.Text.Trim()
                ))
                {
                    MessageBox.Show(
                        "✔ USUÁRIO CRIADO COM SUCESSO!\n\n" +
                        $"Usuário: {txtLogin.Text}\n\n" +
                        "O sistema será iniciado.\n" +
                        "Use estas credenciais para fazer login.",
                        "Sucesso!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                btnCriar.Enabled = true;
                btnCriar.Text = "✔ CRIAR USUÁRIO E INICIAR";

                MessageBox.Show(
                    "❌ Erro ao criar usuário:\n\n" + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void lblDicas_Click(object sender, EventArgs e)
        {

        }
    }
}