// =============================================
// FORM DE LISTAGEM
// Arquivo: FormListaClientes.cs
// =============================================
using System;
using System.Drawing;
using System.Windows.Forms;

public partial class FormListaClientes : Form
{
    private ClienteDAL clienteDAL;

    public FormListaClientes()
    {
        InitializeComponent();
        clienteDAL = new ClienteDAL();
        CarregarClientes();
    }

    private void InitializeComponent()
    {
        this.Text = "Todos os Clientes Cadastrados";
        this.Size = new Size(1100, 700);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.BackColor = Color.FromArgb(240, 248, 255);

        // Título
        Label lblTitulo = new Label
        {
            Text = "📋 TODOS OS CLIENTES",
            Font = new Font("Segoe UI", 20, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 102, 204),
            Location = new Point(380, 20),
            AutoSize = true
        };

        // Grade de dados com fonte maior e cores
        DataGridView dgvClientes = new DataGridView
        {
            Name = "dgvClientes",
            Location = new Point(30, 80),
            Size = new Size(1030, 520),
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            BackgroundColor = Color.White,
            Font = new Font("Segoe UI", 11),
            RowHeadersVisible = false,
            BorderStyle = BorderStyle.None,
            AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(236, 240, 241)
            },
            DefaultCellStyle = new DataGridViewCellStyle
            {
                SelectionBackColor = Color.FromArgb(52, 152, 219),
                SelectionForeColor = Color.White,
                Padding = new Padding(5)
            },
            ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            },
            EnableHeadersVisualStyles = false,
            RowTemplate = { Height = 35 },
            ColumnHeadersHeight = 40
        };

        Button btnFechar = new Button
        {
            Text = "✖ FECHAR",
            Location = new Point(460, 620),
            Size = new Size(180, 45),
            Font = new Font("Segoe UI", 13, FontStyle.Bold),
            BackColor = Color.FromArgb(231, 76, 60),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        btnFechar.FlatAppearance.BorderSize = 0;
        btnFechar.Click += (s, e) => this.Close();

        this.Controls.AddRange(new Control[] { lblTitulo, dgvClientes, btnFechar });
    }

    private void CarregarClientes()
    {
        try
        {
            DataGridView dgv = (DataGridView)this.Controls["dgvClientes"];
            var clientes = clienteDAL.ListarTodosClientes();

            if (clientes.Count == 0)
            {
                MessageBox.Show("ℹ Nenhum cliente cadastrado ainda!\n\nCadastre o primeiro cliente no menu principal.",
                    "Lista Vazia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            dgv.DataSource = clientes;

            // Configurar aparência das colunas
            dgv.Columns["ClienteID"].HeaderText = "CÓDIGO";
            dgv.Columns["ClienteID"].Width = 80;

            dgv.Columns["NomeCompleto"].HeaderText = "NOME COMPLETO";
            dgv.Columns["CPF"].HeaderText = "CPF";
            dgv.Columns["CPF"].Width = 120;

            dgv.Columns["DataNascimento"].HeaderText = "NASCIMENTO";
            dgv.Columns["DataNascimento"].Width = 130;
            dgv.Columns["DataNascimento"].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgv.Columns["Endereco"].HeaderText = "ENDEREÇO";
            dgv.Columns["Cidade"].HeaderText = "CIDADE";
            dgv.Columns["Cidade"].Width = 150;

            dgv.Columns["BeneficioINSS"].HeaderText = "BENEFÍCIO INSS";
            dgv.Columns["BeneficioINSS"].Width = 140;

            dgv.Columns["DataCadastro"].HeaderText = "CADASTRADO EM";
            dgv.Columns["DataCadastro"].Width = 150;
            dgv.Columns["DataCadastro"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            dgv.Columns["Ativo"].Visible = false;
        }
        catch (Exception ex)
        {
            MessageBox.Show("✖ Erro ao carregar clientes:\n\n" + ex.Message,
                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}