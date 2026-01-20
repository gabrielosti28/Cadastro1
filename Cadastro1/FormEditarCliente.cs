using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cadastro1
{
    public partial class FormEditarCliente : Form
    {
        private ClienteDAL clienteDAL;
        private Cliente clienteOriginal;

        public FormEditarCliente(Cliente cliente)
        {
            InitializeComponent();
            clienteDAL = new ClienteDAL();
            clienteOriginal = cliente;
        }

        private void FormEditarCliente_Load(object sender, EventArgs e)
        {
            // Preencher campos com dados do cliente
            txtNome.Text = clienteOriginal.NomeCompleto;
            txtCPF.Text = clienteOriginal.CPF;
            dtpDataNasc.Value = clienteOriginal.DataNascimento;
            txtEndereco.Text = clienteOriginal.Endereco;
            txtCEP.Text = clienteOriginal.CEP;
            txtCidade.Text = clienteOriginal.Cidade;
            txtTelefone.Text = clienteOriginal.Telefone ?? "";
            txtINSS.Text = clienteOriginal.BeneficioINSS;
            txtINSS2.Text = clienteOriginal.BeneficioINSS2 ?? "";

            // Focar no primeiro campo editável
            txtNome.Focus();
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

                // Verificar se houve alguma alteração
                bool houveAlteracao =
                    txtNome.Text.Trim() != clienteOriginal.NomeCompleto ||
                    dtpDataNasc.Value != clienteOriginal.DataNascimento ||
                    txtEndereco.Text.Trim() != clienteOriginal.Endereco ||
                    cep != clienteOriginal.CEP ||
                    txtCidade.Text.Trim() != clienteOriginal.Cidade ||
                    (string.IsNullOrEmpty(telefone) ? "" : txtTelefone.Text.Trim()) != (clienteOriginal.Telefone ?? "") ||
                    inss != clienteOriginal.BeneficioINSS ||
                    (string.IsNullOrEmpty(inss2) ? "" : inss2) != (clienteOriginal.BeneficioINSS2 ?? "");

                if (!houveAlteracao)
                {
                    MessageBox.Show(
                        "ℹ Nenhuma alteração foi detectada!\n\nFaça alguma modificação antes de salvar.",
                        "Sem Alterações",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Confirmação
                DialogResult resultado = MessageBox.Show(
                    "Deseja realmente SALVAR as alterações?\n\n" +
                    $"Cliente: {txtNome.Text}\n" +
                    $"CPF: {clienteOriginal.CPF}",
                    "Confirmar Alterações",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (resultado == DialogResult.No)
                    return;

                // Atualizar objeto Cliente
                clienteOriginal.NomeCompleto = txtNome.Text.Trim();
                clienteOriginal.DataNascimento = dtpDataNasc.Value;
                clienteOriginal.Endereco = txtEndereco.Text.Trim();
                clienteOriginal.Cidade = txtCidade.Text.Trim();
                clienteOriginal.CEP = cep;
                clienteOriginal.Telefone = string.IsNullOrEmpty(telefone) ? null : txtTelefone.Text.Trim();
                clienteOriginal.BeneficioINSS = inss;
                clienteOriginal.BeneficioINSS2 = string.IsNullOrEmpty(inss2) ? null : inss2;

                // Salvar no banco
                btnSalvar.Enabled = false;
                btnSalvar.Text = "Salvando...";
                Application.DoEvents();

                if (clienteDAL.AtualizarCliente(clienteOriginal))
                {
                    MessageBox.Show(
                        "✔ Cliente atualizado com SUCESSO!\n\nAs alterações foram salvas no sistema.",
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
                btnSalvar.Enabled = true;
                btnSalvar.Text = "✔ SALVAR ALTERAÇÕES";

                MessageBox.Show(
                    "✖ Erro ao atualizar cliente:\n\n" + ex.Message,
                    "Erro!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "Deseja realmente CANCELAR?\n\nTodas as alterações serão perdidas!",
                "Confirmar Cancelamento",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (resultado == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}