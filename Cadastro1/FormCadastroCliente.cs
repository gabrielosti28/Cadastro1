using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cadastro1
{
    public partial class FormCadastroCliente : Form
    {
        private ClienteDAL clienteDAL;
        private TextBox txtNome, txtEndereco, txtCidade;
        private MaskedTextBox txtCPF, txtINSS;
        private DateTimePicker dtpDataNasc;

        public FormCadastroCliente()
        {
            InitializeComponent();
            clienteDAL = new ClienteDAL();
            CriarInterface();
        }

        private void CriarInterface()
        {
            this.Text = "Cadastrar Novo Cliente";
            this.Size = new Size(750, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 248, 255);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Label lblTitulo = new Label
            {
                Text = "➕ CADASTRAR NOVO CLIENTE",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204),
                Location = new Point(150, 20),
                AutoSize = true
            };

            Panel panelForm = new Panel
            {
                Location = new Point(50, 80),
                Size = new Size(650, 420),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            int yPos = 30;
            int espacamento = 80;

            Label lblNome = CriarLabel("NOME COMPLETO:", yPos);
            txtNome = CriarTextBox(yPos + 30, 500);

            yPos += espacamento;
            Label lblCPF = CriarLabel("CPF (somente números):", yPos);
            txtCPF = new MaskedTextBox
            {
                Location = new Point(40, yPos + 30),
                Size = new Size(280, 35),
                Font = new Font("Segoe UI", 14),
                Mask = "00000000000"
            };
            Label lblDicaCPF = new Label
            {
                Text = "Digite 11 números",
                Location = new Point(330, yPos + 35),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.Gray
            };

            yPos += espacamento;
            Label lblDataNasc = CriarLabel("DATA DE NASCIMENTO:", yPos);
            dtpDataNasc = new DateTimePicker
            {
                Location = new Point(40, yPos + 30),
                Size = new Size(280, 35),
                Font = new Font("Segoe UI", 14),
                Format = DateTimePickerFormat.Short
            };

            yPos += espacamento;
            Label lblEndereco = CriarLabel("ENDEREÇO COMPLETO:", yPos);
            txtEndereco = CriarTextBox(yPos + 30, 500);

            yPos += espacamento;
            Label lblCidade = CriarLabel("CIDADE:", yPos);
            txtCidade = CriarTextBox(yPos + 30, 400);

            yPos += espacamento - 10;
            Label lblINSS = CriarLabel("BENEFÍCIO INSS:", yPos);
            txtINSS = new MaskedTextBox
            {
                Location = new Point(40, yPos + 30),
                Size = new Size(280, 35),
                Font = new Font("Segoe UI", 14),
                Mask = "0000000000"
            };
            Label lblDicaINSS = new Label
            {
                Text = "Digite 10 números",
                Location = new Point(330, yPos + 35),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.Gray
            };

            panelForm.Controls.AddRange(new Control[] {
                lblNome, txtNome, lblCPF, txtCPF, lblDicaCPF,
                lblDataNasc, dtpDataNasc, lblEndereco, txtEndereco,
                lblCidade, txtCidade, lblINSS, txtINSS, lblDicaINSS
            });

            Button btnSalvar = new Button
            {
                Text = "✔ SALVAR CLIENTE",
                Location = new Point(150, 530),
                Size = new Size(220, 55),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSalvar.FlatAppearance.BorderSize = 0;
            btnSalvar.Click += BtnSalvar_Click;

            Button btnCancelar = new Button
            {
                Text = "✖ CANCELAR",
                Location = new Point(390, 530),
                Size = new Size(180, 55),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { lblTitulo, panelForm, btnSalvar, btnCancelar });
        }

        private Label CriarLabel(string texto, int y)
        {
            return new Label
            {
                Text = texto,
                Location = new Point(40, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };
        }

        private TextBox CriarTextBox(int y, int largura)
        {
            return new TextBox
            {
                Location = new Point(40, y),
                Size = new Size(largura, 35),
                Font = new Font("Segoe UI", 14)
            };
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
    }
}
