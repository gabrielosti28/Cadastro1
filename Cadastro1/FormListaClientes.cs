// =============================================
// FORMULÁRIO - LISTA DE CLIENTES - ATUALIZADO
// Arquivo: FormListaClientes.cs
// NOVO: Duplo clique abre FormAnexosCliente
// =============================================
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Cadastro1
{
    public partial class FormListaClientes : Form
    {
        private ClienteDAL clienteDAL;
        private string cidadeFiltro;
        private bool mostrarTodas;
        private int paginaAtual = 1;
        private const int REGISTROS_POR_PAGINA = 100;
        private int totalPaginas = 0;
        private int totalRegistros = 0;

        public FormListaClientes()
        {
            InitializeComponent();
            clienteDAL = new ClienteDAL();
            ConfigurarInterface();
        }

        private void ConfigurarInterface()
        {
            this.BackColor = Color.FromArgb(240, 248, 255);
        }

        private void FormListaClientes_Load(object sender, EventArgs e)
        {
            try
            {
                var todosClientes = clienteDAL.ListarTodosClientes();

                if (todosClientes.Count == 0)
                {
                    MessageBox.Show(
                        "ℹ Nenhum cliente cadastrado ainda!\n\nCadastre o primeiro cliente no menu principal.",
                        "Lista Vazia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    this.Close();
                    return;
                }

                var cidades = todosClientes
                    .Select(c => c.Cidade)
                    .Where(c => !string.IsNullOrWhiteSpace(c))
                    .Distinct()
                    .ToList();

                if (cidades.Count == 0)
                {
                    MessageBox.Show(
                        "⚠ Nenhuma cidade cadastrada!\n\nCadastre clientes com cidades primeiro.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                using (FormSelecionarCidade formCidade = new FormSelecionarCidade(cidades))
                {
                    if (formCidade.ShowDialog() == DialogResult.OK)
                    {
                        cidadeFiltro = formCidade.CidadeSelecionada;
                        mostrarTodas = formCidade.VerTodasCidades;

                        if (mostrarTodas)
                        {
                            lblTitulo.Text = "📋 TODOS OS CLIENTES";
                        }
                        else
                        {
                            lblTitulo.Text = $"📋 CLIENTES DE {cidadeFiltro.ToUpper()}";
                        }

                        CarregarClientes();
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao carregar cidades:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void CarregarClientes()
        {
            try
            {
                var resultado = clienteDAL.ListarClientesPaginado(
                    paginaAtual,
                    REGISTROS_POR_PAGINA,
                    cidadeFiltro,
                    mostrarTodas
                );

                dgvClientes.DataSource = null;
                dgvClientes.DataSource = resultado.Clientes;

                totalRegistros = resultado.TotalRegistros;
                totalPaginas = (int)Math.Ceiling((double)resultado.TotalRegistros / REGISTROS_POR_PAGINA);

                ConfigurarColunasDataGridView();
                AtualizarControlePaginacao();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        private void ConfigurarColunasDataGridView()
        {
            if (dgvClientes.Columns.Count > 0)
            {
                dgvClientes.Columns["ClienteID"].Visible = false;
                dgvClientes.Columns["DataCadastro"].Visible = false;
                dgvClientes.Columns["BeneficioINSS2"].Visible = false;
                dgvClientes.Columns["Endereco"].Visible = false;

                dgvClientes.Columns["NomeCompleto"].HeaderText = "NOME COMPLETO";
                dgvClientes.Columns["CPF"].HeaderText = "CPF";
                dgvClientes.Columns["DataNascimento"].HeaderText = "NASCIMENTO";
                dgvClientes.Columns["Cidade"].HeaderText = "CIDADE";
                dgvClientes.Columns["CEP"].HeaderText = "CEP";
                dgvClientes.Columns["Telefone"].HeaderText = "TELEFONE";
                dgvClientes.Columns["BeneficioINSS"].HeaderText = "INSS";

                dgvClientes.Columns["DataNascimento"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }

        private void AtualizarControlePaginacao()
        {
            lblPaginacao.Text = $"Página {paginaAtual} de {totalPaginas} | Total: {totalRegistros} clientes";
            btnAnterior.Enabled = paginaAtual > 1;
            btnProximo.Enabled = paginaAtual < totalPaginas;
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (paginaAtual < totalPaginas)
            {
                paginaAtual++;
                CarregarClientes();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaAtual > 1)
            {
                paginaAtual--;
                CarregarClientes();
            }
        }

        private void Botao_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                Color corAtual = btn.BackColor;
                btn.BackColor = ControlPaint.Dark(corAtual, 0.1f);
            }
        }

        private void Botao_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn == btnFechar)
                    btn.BackColor = Color.FromArgb(231, 76, 60);
                else if (btn == btnAnterior)
                    btn.BackColor = Color.FromArgb(52, 152, 219);
                else if (btn == btnProximo)
                    btn.BackColor = Color.FromArgb(46, 204, 113);
            }
        }

        // =============================================
        // ATUALIZAÇÃO: DUPLO CLIQUE ABRE ANEXOS
        // =============================================
        private void DgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Mantém compatibilidade com cliques simples em colunas de botão
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                Cliente cliente = (Cliente)dgvClientes.Rows[e.RowIndex].DataBoundItem;
                string nomeColuna = dgvClientes.Columns[e.ColumnIndex].Name;

                if (nomeColuna == "btnDocumentos")
                {
                    AbrirFormAnexosCliente(cliente);
                }
                else if (nomeColuna == "btnEditar")
                {
                    try
                    {
                        FormEditarCliente formEditar = new FormEditarCliente(cliente);
                        if (formEditar.ShowDialog() == DialogResult.OK)
                        {
                            CarregarClientes();

                            MessageBox.Show(
                                "✓ Cliente atualizado com sucesso!",
                                "Sucesso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            "Erro ao abrir edição:\n\n" + ex.Message,
                            "Erro",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        // NOVO: Evento de duplo clique - substitui a antiga exibição de mensagem
        private void DgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    Cliente cliente = (Cliente)dgvClientes.Rows[e.RowIndex].DataBoundItem;
                    AbrirFormAnexosCliente(cliente);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Erro ao abrir detalhes do cliente:\n\n{ex.Message}",
                        "Erro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Abre o formulário de anexos/informações do cliente
        /// </summary>
        private void AbrirFormAnexosCliente(Cliente cliente)
        {
            try
            {
                using (FormAnexosCliente formAnexos = new FormAnexosCliente(cliente))
                {
                    formAnexos.ShowDialog();

                    // Recarregar lista caso tenha havido alterações
                    CarregarClientes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao abrir informações do cliente:\n\n" + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}