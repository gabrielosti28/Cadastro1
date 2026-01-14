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