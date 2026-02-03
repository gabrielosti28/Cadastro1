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
                var todosClientes = clienteDAL.ListarTodosClientes();

                // =============================================
                // APLICAR FILTRO DE CIDADE
                // =============================================
                List<Cliente> clientesFiltrados;

                if (mostrarTodas)
                {
                    clientesFiltrados = todosClientes;
                }
                else
                {
                    clientesFiltrados = todosClientes
                        .Where(c => c.Cidade.Equals(cidadeFiltro, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                if (clientesFiltrados.Count == 0)
                {
                    MessageBox.Show(
                        $"ℹ Nenhum cliente encontrado na cidade '{cidadeFiltro}'!",
                        "Lista Vazia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    this.Close();
                    return;
                }

                dgvClientes.DataSource = null;
                dgvClientes.DataSource = clientesFiltrados;

                // ===== CONFIGURAR ORDEM E VISIBILIDADE DAS COLUNAS =====

                // 1. OCULTAR colunas que não queremos mostrar
                if (dgvClientes.Columns.Contains("ClienteID"))
                    dgvClientes.Columns["ClienteID"].Visible = false;

                if (dgvClientes.Columns.Contains("Ativo"))
                    dgvClientes.Columns["Ativo"].Visible = false;

                if (dgvClientes.Columns.Contains("CEP"))
                    dgvClientes.Columns["CEP"].Visible = false;

                if (dgvClientes.Columns.Contains("Telefone"))
                    dgvClientes.Columns["Telefone"].Visible = false;

                if (dgvClientes.Columns.Contains("BeneficioINSS2"))
                    dgvClientes.Columns["BeneficioINSS2"].Visible = false;

                if (dgvClientes.Columns.Contains("Endereco"))
                    dgvClientes.Columns["Endereco"].Visible = false;

                // 2. REMOVER botões se já existirem (para evitar duplicação ao recarregar)
                if (dgvClientes.Columns.Contains("btnDocumentos"))
                    dgvClientes.Columns.Remove("btnDocumentos");

                if (dgvClientes.Columns.Contains("btnEditar"))
                    dgvClientes.Columns.Remove("btnEditar");

                if (dgvClientes.Columns.Contains("colCodigo"))
                    dgvClientes.Columns.Remove("colCodigo");

                // 3. CONFIGURAR colunas de dados existentes
                if (dgvClientes.Columns.Contains("NomeCompleto"))
                {
                    dgvClientes.Columns["NomeCompleto"].HeaderText = "NOME COMPLETO";
                    dgvClientes.Columns["NomeCompleto"].DisplayIndex = 0;
                    dgvClientes.Columns["NomeCompleto"].Width = 300;
                }

                if (dgvClientes.Columns.Contains("CPF"))
                {
                    dgvClientes.Columns["CPF"].HeaderText = "CPF";
                    dgvClientes.Columns["CPF"].DisplayIndex = 1;
                    dgvClientes.Columns["CPF"].Width = 120;
                    dgvClientes.Columns["CPF"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvClientes.Columns.Contains("DataNascimento"))
                {
                    dgvClientes.Columns["DataNascimento"].HeaderText = "NASCIMENTO";
                    dgvClientes.Columns["DataNascimento"].DisplayIndex = 2;
                    dgvClientes.Columns["DataNascimento"].Width = 110;
                    dgvClientes.Columns["DataNascimento"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgvClientes.Columns["DataNascimento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvClientes.Columns.Contains("Cidade"))
                {
                    dgvClientes.Columns["Cidade"].HeaderText = "CIDADE";
                    dgvClientes.Columns["Cidade"].DisplayIndex = 3;
                    dgvClientes.Columns["Cidade"].Width = 150;
                }

                if (dgvClientes.Columns.Contains("BeneficioINSS"))
                {
                    dgvClientes.Columns["BeneficioINSS"].HeaderText = "BENEFÍCIO INSS";
                    dgvClientes.Columns["BeneficioINSS"].DisplayIndex = 4;
                    dgvClientes.Columns["BeneficioINSS"].Width = 130;
                    dgvClientes.Columns["BeneficioINSS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvClientes.Columns.Contains("DataCadastro"))
                {
                    dgvClientes.Columns["DataCadastro"].HeaderText = "CADASTRADO EM";
                    dgvClientes.Columns["DataCadastro"].DisplayIndex = 5;
                    dgvClientes.Columns["DataCadastro"].Width = 140;
                    dgvClientes.Columns["DataCadastro"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                    dgvClientes.Columns["DataCadastro"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                // 4. ADICIONAR coluna de CÓDIGO (informativa)
                DataGridViewTextBoxColumn colCodigo = new DataGridViewTextBoxColumn
                {
                    Name = "colCodigo",
                    HeaderText = "CÓDIGO",
                    Width = 80,
                    ReadOnly = true,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleCenter,
                        BackColor = Color.FromArgb(236, 240, 241),
                        ForeColor = Color.FromArgb(52, 73, 94),
                        Font = new Font("Consolas", 9, FontStyle.Bold)
                    }
                };
                dgvClientes.Columns.Add(colCodigo);
                colCodigo.DisplayIndex = 6;

                // Preencher valores da coluna de código
                foreach (DataGridViewRow row in dgvClientes.Rows)
                {
                    if (row.DataBoundItem != null)
                    {
                        Cliente cliente = (Cliente)row.DataBoundItem;
                        row.Cells["colCodigo"].Value = cliente.ClienteID.ToString("D5");
                    }
                }

                // 5. ADICIONAR botão de DOCUMENTOS
                DataGridViewButtonColumn btnDocumentos = new DataGridViewButtonColumn
                {
                    Name = "btnDocumentos",
                    HeaderText = "DOCUMENTOS",
                    Text = "📎 Ver",
                    UseColumnTextForButtonValue = true,
                    Width = 110,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        BackColor = Color.FromArgb(52, 152, 219),
                        ForeColor = Color.White,
                        SelectionBackColor = Color.FromArgb(41, 128, 185),
                        SelectionForeColor = Color.White,
                        Font = new Font("Segoe UI", 9, FontStyle.Bold)
                    }
                };
                dgvClientes.Columns.Add(btnDocumentos);
                btnDocumentos.DisplayIndex = 7;

                // 6. ADICIONAR botão de EDITAR
                DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn
                {
                    Name = "btnEditar",
                    HeaderText = "EDITAR",
                    Text = "✏️ Editar",
                    UseColumnTextForButtonValue = true,
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        BackColor = Color.FromArgb(230, 126, 34),
                        ForeColor = Color.White,
                        SelectionBackColor = Color.FromArgb(211, 84, 0),
                        SelectionForeColor = Color.White,
                        Font = new Font("Segoe UI", 9, FontStyle.Bold)
                    }
                };
                dgvClientes.Columns.Add(btnEditar);
                btnEditar.DisplayIndex = 8;

                // 7. ADICIONAR evento de clique
                dgvClientes.CellClick -= DgvClientes_CellClick;
                dgvClientes.CellClick += DgvClientes_CellClick;

                // 8. ESTILIZAÇÃO adicional
                dgvClientes.EnableHeadersVisualStyles = false;
                dgvClientes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
                dgvClientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvClientes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvClientes.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvClientes.ColumnHeadersHeight = 40;

                dgvClientes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(236, 240, 241);

                // =============================================
                // ATUALIZAR TÍTULO COM FILTRO APLICADO
                // =============================================
                if (mostrarTodas)
                {
                    lblTitulo.Text = $"📋 TODOS OS CLIENTES ({clientesFiltrados.Count})";
                }
                else
                {
                    lblTitulo.Text = $"📋 CLIENTES DE {cidadeFiltro.ToUpper()} ({clientesFiltrados.Count})";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "✖ Erro ao carregar clientes:\n\n" + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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