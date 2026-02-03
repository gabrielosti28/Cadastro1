// =============================================
// FORMULÁRIO - LISTA DE CLIENTES - ATUALIZADO
// Arquivo: FormListaClientes.cs
// NOVO: Filtro por cidade antes de exibir
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
        private string cidadeFiltro; // Cidade selecionada para filtro
        private bool mostrarTodas; // Se deve mostrar todas as cidades
        private int paginaAtual = 1;
        private const int REGISTROS_POR_PAGINA = 100;
        private int totalPaginas = 0;
        private int totalRegistros = 0; // Adicionada a variável que estava faltando

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
            // =============================================
            // NOVO: PRIMEIRO SELECIONAR A CIDADE
            // =============================================
            try
            {
                // Buscar todas as cidades cadastradas
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

                // Obter lista única de cidades
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

                // Mostrar formulário de seleção de cidade
                using (FormSelecionarCidade formCidade = new FormSelecionarCidade(cidades))
                {
                    if (formCidade.ShowDialog() == DialogResult.OK)
                    {
                        cidadeFiltro = formCidade.CidadeSelecionada;
                        mostrarTodas = formCidade.VerTodasCidades;

                        // Atualizar título
                        if (mostrarTodas)
                        {
                            lblTitulo.Text = "📋 TODOS OS CLIENTES";
                        }
                        else
                        {
                            lblTitulo.Text = $"📋 CLIENTES DE {cidadeFiltro.ToUpper()}";
                        }

                        // Carregar clientes com o filtro selecionado
                        CarregarClientes();
                    }
                    else
                    {
                        // Usuário cancelou a seleção
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
                // USAR NOVO MÉTODO COM PAGINAÇÃO
                var resultado = clienteDAL.ListarClientesPaginado(
                    paginaAtual,
                    REGISTROS_POR_PAGINA,
                    cidadeFiltro,
                    mostrarTodas
                );

                dgvClientes.DataSource = null;
                dgvClientes.DataSource = resultado.Clientes;

                totalRegistros = resultado.TotalRegistros; // Corrigido
                totalPaginas = (int)Math.Ceiling((double)resultado.TotalRegistros / REGISTROS_POR_PAGINA);

                // Configurar colunas
                ConfigurarColunasDataGridView();

                // ADICIONAR CONTROLES DE PAGINAÇÃO
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
                // Esconder colunas desnecessárias
                dgvClientes.Columns["ClienteID"].Visible = false;
                dgvClientes.Columns["DataCadastro"].Visible = false;
                dgvClientes.Columns["BeneficioINSS2"].Visible = false;
                dgvClientes.Columns["Endereco"].Visible = false;

                // Renomear colunas
                dgvClientes.Columns["NomeCompleto"].HeaderText = "NOME COMPLETO";
                dgvClientes.Columns["CPF"].HeaderText = "CPF";
                dgvClientes.Columns["DataNascimento"].HeaderText = "NASCIMENTO";
                dgvClientes.Columns["Cidade"].HeaderText = "CIDADE";
                dgvClientes.Columns["CEP"].HeaderText = "CEP";
                dgvClientes.Columns["Telefone"].HeaderText = "TELEFONE";
                dgvClientes.Columns["BeneficioINSS"].HeaderText = "INSS";

                // Formatar data
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

        private void DgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                Cliente cliente = (Cliente)dgvClientes.Rows[e.RowIndex].DataBoundItem;
                string nomeColuna = dgvClientes.Columns[e.ColumnIndex].Name;

                if (nomeColuna == "btnDocumentos")
                {
                    try
                    {
                        FormAnexosCliente formAnexos = new FormAnexosCliente(cliente);
                        formAnexos.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            "Erro ao abrir documentos:\n\n" + ex.Message,
                            "Erro",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
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
                else
                {
                    MostrarDetalhesRapidos(cliente);
                }
            }
        }

        private void MostrarDetalhesRapidos(Cliente cliente)
        {
            try
            {
                int idade = ClienteDAL.CalcularIdade(cliente.DataNascimento);

                string detalhes = $"📋 INFORMAÇÕES DO CLIENTE\n\n";
                detalhes += $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n";
                detalhes += $"👤 Nome: {cliente.NomeCompleto}\n\n";
                detalhes += $"🆔 CPF: {FormatarCPF(cliente.CPF)}\n\n";
                detalhes += $"🎂 Nascimento: {cliente.DataNascimento:dd/MM/yyyy} ({idade} anos)\n\n";
                detalhes += $"🏠 Endereço: {cliente.Endereco}\n\n";
                detalhes += $"🏙️ Cidade: {cliente.Cidade}\n\n";
                detalhes += $"📮 CEP: {FormatarCEP(cliente.CEP)}\n\n";

                if (!string.IsNullOrEmpty(cliente.Telefone))
                    detalhes += $"📞 Telefone: {cliente.Telefone}\n\n";

                detalhes += $"💼 INSS: {cliente.BeneficioINSS}\n\n";

                if (!string.IsNullOrEmpty(cliente.BeneficioINSS2))
                    detalhes += $"💼 2º INSS: {cliente.BeneficioINSS2}\n\n";

                detalhes += $"📅 Cadastrado em: {cliente.DataCadastro:dd/MM/yyyy HH:mm}\n\n";
                detalhes += $"🔢 Código: {cliente.ClienteID:D5}";

                MessageBox.Show(
                    detalhes,
                    "Detalhes do Cliente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao exibir detalhes:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string FormatarCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) return "";

            cpf = cpf.Replace("-", "").Replace(".", "").Trim();

            if (cpf.Length == 11)
            {
                return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";
            }

            return cpf;
        }

        private string FormatarCEP(string cep)
        {
            if (string.IsNullOrEmpty(cep)) return "";

            cep = cep.Replace("-", "").Trim();

            if (cep.Length == 8)
            {
                return $"{cep.Substring(0, 5)}-{cep.Substring(5, 3)}";
            }

            return cep;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}