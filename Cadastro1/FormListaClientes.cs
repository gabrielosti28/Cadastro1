using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cadastro1
{
    public partial class FormListaClientes : Form
    {
        private ClienteDAL clienteDAL;

        public FormListaClientes()
        {
            InitializeComponent();
            clienteDAL = new ClienteDAL();
            ConfigurarInterface();
        }

        private void ConfigurarInterface()
        {
            // Configurações visuais básicas
            this.BackColor = Color.FromArgb(240, 248, 255);
        }

        private void FormListaClientes_Load(object sender, EventArgs e)
        {
            CarregarClientes();
        }

        private void CarregarClientes()
        {
            try
            {
                var clientes = clienteDAL.ListarTodosClientes();

                if (clientes.Count == 0)
                {
                    MessageBox.Show("ℹ Nenhum cliente cadastrado ainda!\n\nCadastre o primeiro cliente no menu principal.",
                        "Lista Vazia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }

                dgvClientes.DataSource = clientes;

                // Configurar colunas existentes
                if (dgvClientes.Columns.Contains("ClienteID"))
                {
                    dgvClientes.Columns["ClienteID"].HeaderText = "CÓDIGO";
                    dgvClientes.Columns["ClienteID"].Width = 80;
                }

                if (dgvClientes.Columns.Contains("NomeCompleto"))
                {
                    dgvClientes.Columns["NomeCompleto"].HeaderText = "NOME COMPLETO";
                }

                if (dgvClientes.Columns.Contains("CPF"))
                {
                    dgvClientes.Columns["CPF"].HeaderText = "CPF";
                    dgvClientes.Columns["CPF"].Width = 120;
                }

                if (dgvClientes.Columns.Contains("DataNascimento"))
                {
                    dgvClientes.Columns["DataNascimento"].HeaderText = "NASCIMENTO";
                    dgvClientes.Columns["DataNascimento"].Width = 130;
                    dgvClientes.Columns["DataNascimento"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }

                if (dgvClientes.Columns.Contains("Endereco"))
                {
                    dgvClientes.Columns["Endereco"].HeaderText = "ENDEREÇO";
                }

                if (dgvClientes.Columns.Contains("Cidade"))
                {
                    dgvClientes.Columns["Cidade"].HeaderText = "CIDADE";
                    dgvClientes.Columns["Cidade"].Width = 150;
                }

                if (dgvClientes.Columns.Contains("BeneficioINSS"))
                {
                    dgvClientes.Columns["BeneficioINSS"].HeaderText = "BENEFÍCIO INSS";
                    dgvClientes.Columns["BeneficioINSS"].Width = 140;
                }

                if (dgvClientes.Columns.Contains("DataCadastro"))
                {
                    dgvClientes.Columns["DataCadastro"].HeaderText = "CADASTRADO EM";
                    dgvClientes.Columns["DataCadastro"].Width = 150;
                    dgvClientes.Columns["DataCadastro"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                }

                if (dgvClientes.Columns.Contains("Ativo"))
                {
                    dgvClientes.Columns["Ativo"].Visible = false;
                }

                // NOVO: Adicionar coluna de botão para anexos
                if (!dgvClientes.Columns.Contains("btnAnexos"))
                {
                    DataGridViewButtonColumn btnAnexos = new DataGridViewButtonColumn
                    {
                        Name = "btnAnexos",
                        HeaderText = "DOCUMENTOS",
                        Text = "📎 Ver",
                        UseColumnTextForButtonValue = true,
                        Width = 100
                    };
                    dgvClientes.Columns.Add(btnAnexos);
                }

                // Adicionar evento de clique no botão
                dgvClientes.CellClick -= DgvClientes_CellClick; // Remover se já existir
                dgvClientes.CellClick += DgvClientes_CellClick;

                // Centralizar cabeçalhos e algumas colunas
                foreach (DataGridViewColumn coluna in dgvClientes.Columns)
                {
                    if (coluna.HeaderText.Contains("CÓDIGO") ||
                        coluna.HeaderText.Contains("CPF") ||
                        coluna.HeaderText.Contains("NASCIMENTO") ||
                        coluna.HeaderText.Contains("CADASTRADO"))
                    {
                        coluna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("✖ Erro ao carregar clientes:\n\n" + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ADICIONE este novo método ao FormListaClientes:

        private void DgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar se clicou na coluna de botão de anexos
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dgvClientes.Columns[e.ColumnIndex].Name == "btnAnexos")
                {
                    try
                    {
                        Cliente cliente = (Cliente)dgvClientes.Rows[e.RowIndex].DataBoundItem;
                        FormAnexosCliente formAnexos = new FormAnexosCliente(cliente);
                        formAnexos.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            "Erro ao abrir anexos:\n\n" + ex.Message,
                            "Erro",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}