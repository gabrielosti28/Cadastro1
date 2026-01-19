using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cadastro1
{
    public partial class FormBuscaCliente : Form
    {
        private ClienteDAL clienteDAL;

        public FormBuscaCliente()
        {
            InitializeComponent();
            clienteDAL = new ClienteDAL();
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

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}