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
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validação: Nome
                if (string.IsNullOrWhiteSpace(txtNome.Text))
                {
                    MessageBox.Show("⚠ Por favor, preencha o NOME COMPLETO do cliente!",
                        "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNome.Focus();
                    return;
                }

                // Validação: CPF
                string cpf = txtCPF.Text.Replace(" ", "").Replace("-", "").Replace(".", "");
                if (cpf.Length != 11)
                {
                    MessageBox.Show("⚠ O CPF deve conter exatamente 11 números!\n\nVerifique se digitou todos os números.",
                        "CPF Incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCPF.Focus();
                    return;
                }

                // Validação: Endereço
                if (string.IsNullOrWhiteSpace(txtEndereco.Text))
                {
                    MessageBox.Show("⚠ Por favor, preencha o ENDEREÇO do cliente!",
                        "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEndereco.Focus();
                    return;
                }

                // Validação: CEP (OBRIGATÓRIO)
                string cep = txtCEP.Text.Replace("-", "").Replace("_", "").Trim();
                if (cep.Length != 8)
                {
                    MessageBox.Show("⚠ O CEP deve conter exatamente 8 números!\n\nFormato: 00000-000",
                        "CEP Incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCEP.Focus();
                    return;
                }

                // Validação: Cidade
                if (string.IsNullOrWhiteSpace(txtCidade.Text))
                {
                    MessageBox.Show("⚠ Por favor, preencha a CIDADE do cliente!",
                        "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCidade.Focus();
                    return;
                }

                // Validação: Benefício INSS (OBRIGATÓRIO)
                string inss = txtINSS.Text.Replace(" ", "").Replace("_", "").Trim();
                if (inss.Length != 10)
                {
                    MessageBox.Show("⚠ O Benefício INSS deve conter exatamente 10 números!\n\nVerifique se digitou todos os números.",
                        "INSS Incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtINSS.Focus();
                    return;
                }

                // Telefone é opcional - apenas validar se foi preenchido
                string telefone = txtTelefone.Text.Replace("(", "").Replace(")", "")
                    .Replace("-", "").Replace(" ", "").Replace("_", "").Trim();
                if (!string.IsNullOrEmpty(txtTelefone.Text) && telefone.Length < 10)
                {
                    MessageBox.Show("⚠ Se informar o telefone, preencha corretamente!\n\nFormato: (00) 00000-0000",
                        "Telefone Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTelefone.Focus();
                    return;
                }

                // 2º Benefício INSS é opcional - apenas validar se foi preenchido
                string inss2 = txtINSS2.Text.Replace(" ", "").Replace("_", "").Trim();
                if (!string.IsNullOrEmpty(txtINSS2.Text) && inss2.Length != 10)
                {
                    MessageBox.Show("⚠ Se informar o 2º Benefício INSS, deve conter 10 números!",
                        "2º INSS Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtINSS2.Focus();
                    return;
                }

                // Confirmação
                DialogResult resultado = MessageBox.Show(
                    "Deseja realmente SALVAR este cliente?\n\n" +
                    $"Nome: {txtNome.Text}\n" +
                    $"CPF: {cpf}\n" +
                    $"CEP: {cep}",
                    "Confirmar Cadastro",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (resultado == DialogResult.No)
                    return;

                // Criar objeto Cliente
                Cliente cliente = new Cliente
                {
                    NomeCompleto = txtNome.Text.Trim(),
                    CPF = cpf,
                    DataNascimento = dtpDataNasc.Value,
                    Endereco = txtEndereco.Text.Trim(),
                    Cidade = txtCidade.Text.Trim(),
                    CEP = cep,
                    Telefone = string.IsNullOrEmpty(telefone) ? null : txtTelefone.Text.Trim(),
                    BeneficioINSS = inss,
                    BeneficioINSS2 = string.IsNullOrEmpty(inss2) ? null : inss2
                };

                // Salvar no banco
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
    }
}