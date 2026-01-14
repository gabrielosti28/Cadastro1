using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cadastro1
{
    public partial class FormBuscaCliente : Form
    {
        private ClienteDAL clienteDAL;
        private MaskedTextBox txtCPFBusca;
        private Panel panelResultado;

        public FormBuscaCliente()
        {
            InitializeComponent();
            clienteDAL = new ClienteDAL();
            CriarInterface();
        }

        private void CriarInterface()
        {
            this.Text = "Buscar Cliente por CPF";
            this.Size = new Size(750, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 248, 255);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Label lblTitulo = new Label
            {
                Text = "🔍 BUSCAR CLIENTE",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204),
                Location = new Point(230, 20),
                AutoSize = true
            };

            Panel panelBusca = new Panel
            {
                Location = new Point(50, 80),
                Size = new Size(650, 120),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblCPF = new Label
            {
                Text = "Digite o CPF (11 números):",
                Location = new Point(30, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            txtCPFBusca = new MaskedTextBox
            {
                Location = new Point(30, 50),
                Size = new Size(300, 40),
                Font = new Font("Segoe UI", 16),
                Mask = "00000000000"
            };

            Button btnBuscar = new Button
            {
                Text = "🔍 BUSCAR",
                Location = new Point(350, 45),
                Size = new Size(180, 45),
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.Click += BtnBuscar_Click;

            panelBusca.Controls.AddRange(new Control[] { lblCPF, txtCPFBusca, btnBuscar });

            panelResultado = new Panel
            {
                Location = new Point(50, 220),
                Size = new Size(650, 330),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false
            };

            Button btnFechar = new Button
            {
                Text = "✖ FECHAR",
                Location = new Point(280, 565),
                Size = new Size(180, 45),
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnFechar.FlatAppearance.BorderSize = 0;
            btnFechar.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { lblTitulo, panelBusca, panelResultado, btnFechar });
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string cpf = txtCPFBusca.Text.Replace(" ", "");

                if (cpf.Length != 11)
                {
                    MessageBox.Show("⚠ Digite um CPF completo com 11 números!",
                        "CPF Incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCPFBusca.Focus();
                    return;
                }

                Cliente cliente = clienteDAL.ConsultarPorCPF(cpf);

                if (cliente != null)
                {
                    MostrarResultado(cliente);
                }
                else
                {
                    panelResultado.Visible = false;
                    MessageBox.Show(
                        "❌ Cliente não encontrado!\n\nO CPF informado não está cadastrado no sistema.",
                        "Não Encontrado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("✖ Erro ao buscar: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarResultado(Cliente cliente)
        {
            panelResultado.Controls.Clear();

            Label lblTituloRes = new Label
            {
                Text = "✔ CLIENTE ENCONTRADO!",
                Location = new Point(180, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 204, 113)
            };

            int y = 60;
            panelResultado.Controls.Add(lblTituloRes);
            panelResultado.Controls.Add(CriarCampoResultado("Nome:", cliente.NomeCompleto, y));
            panelResultado.Controls.Add(CriarCampoResultado("CPF:", cliente.CPF, y += 50));
            panelResultado.Controls.Add(CriarCampoResultado("Nascimento:", cliente.DataNascimento.ToShortDateString(), y += 50));
            panelResultado.Controls.Add(CriarCampoResultado("Endereço:", cliente.Endereco, y += 50));
            panelResultado.Controls.Add(CriarCampoResultado("Cidade:", cliente.Cidade, y += 50));
            panelResultado.Controls.Add(CriarCampoResultado("INSS:", cliente.BeneficioINSS, y += 50));

            panelResultado.Visible = true;
        }

        private Panel CriarCampoResultado(string titulo, string valor, int y)
        {
            Panel p = new Panel
            {
                Location = new Point(30, y),
                Size = new Size(590, 40),
                BackColor = Color.FromArgb(236, 240, 241)
            };

            Label lbl = new Label
            {
                Text = titulo,
                Location = new Point(10, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            Label val = new Label
            {
                Text = valor,
                Location = new Point(150, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };

            p.Controls.AddRange(new Control[] { lbl, val });
            return p;
        }
    }
}