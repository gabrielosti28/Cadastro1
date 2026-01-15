using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cadastro1
{
    public partial class FormCadastroCliente : Form
    {
        private ClienteDAL clienteDAL;

        public FormCadastroCliente()
        {
            InitializeComponent();
            clienteDAL = new ClienteDAL();
            ConfigurarInterface();
        }

        private void ConfigurarInterface()
        {
            // Remova a criação dinâmica dos controles
            // Agora tudo é feito pelo Designer
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNome.Text))
                {
                    MessageBox.Show("⚠ Por favor, preencha o NOME COMPLETO do cliente!",
                        "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNome.Focus();
                    return;
                }

                string cpf = txtCPF.Text.Replace(" ", "").Replace("-", "").Replace(".", "");
                if (cpf.Length != 11)
                {
                    MessageBox.Show("⚠ O CPF deve conter exatamente 11 números!\n\nVerifique se digitou todos os números.",
                        "CPF Incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCPF.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEndereco.Text))
                {
                    MessageBox.Show("⚠ Por favor, preencha o ENDEREÇO do cliente!",
                        "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEndereco.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCidade.Text))
                {
                    MessageBox.Show("⚠ Por favor, preencha a CIDADE do cliente!",
                        "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCidade.Focus();
                    return;
                }

                string inss = txtINSS.Text.Replace(" ", "");
                if (inss.Length != 10)
                {
                    MessageBox.Show("⚠ O Benefício INSS deve conter exatamente 10 números!\n\nVerifique se digitou todos os números.",
                        "INSS Incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtINSS.Focus();
                    return;
                }

                DialogResult resultado = MessageBox.Show(
                    "Deseja realmente SALVAR este cliente?\n\n" +
                    $"Nome: {txtNome.Text}\n" +
                    $"CPF: {cpf}",
                    "Confirmar Cadastro",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (resultado == DialogResult.No)
                    return;

                Cliente cliente = new Cliente
                {
                    NomeCompleto = txtNome.Text.Trim(),
                    CPF = cpf,
                    DataNascimento = dtpDataNasc.Value,
                    Endereco = txtEndereco.Text.Trim(),
                    Cidade = txtCidade.Text.Trim(),
                    BeneficioINSS = inss
                };

                if (clienteDAL.InserirCliente(cliente))
                {
                    MessageBox.Show(
                        "✔ Cliente cadastrado com SUCESSO!\n\nO cliente foi salvo no sistema.",
                        "Sucesso!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "✖ Erro ao salvar cliente:\n\n" + ex.Message,
                    "Erro!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblNome_Click(object sender, EventArgs e)
        {

        }
    }
}