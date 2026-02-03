// =============================================
// FORMULÁRIO - INFORMAÇÕES E ANEXOS DO CLIENTE
// Arquivo: FormAnexosCliente.cs (NOVA VERSÃO COM ABAS)
// Sistema Profissional de Cadastro
// =============================================
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cadastro1
{
    public partial class FormAnexosCliente : Form
    {
        private Cliente cliente;
        private ClienteAnexoDAL anexoDAL;
        private ClienteDAL clienteDAL;

        public FormAnexosCliente(Cliente clienteSelecionado)
        {
            InitializeComponent();
            this.cliente = clienteSelecionado;
            this.anexoDAL = new ClienteAnexoDAL();
            this.clienteDAL = new ClienteDAL();
        }

        private void FormAnexosCliente_Load(object sender, EventArgs e)
        {
            // Atualizar título da janela
            this.Text = $"Cliente: {cliente.NomeCompleto}";

            // Carregar informações do cliente
            CarregarInformacoesCliente();

            // Carregar anexos
            CarregarAnexos();

            // Selecionar primeira aba
            tabControl.SelectedIndex = 0;
        }

        // =============================================
        // ABA 1: INFORMAÇÕES DO CLIENTE
        // =============================================
        private void CarregarInformacoesCliente()
        {
            try
            {
                // Calcular idade
                int idade = ClienteDAL.CalcularIdade(cliente.DataNascimento);

                // Preencher labels com informações
                lblNomeValor.Text = cliente.NomeCompleto;
                lblCPFValor.Text = FormatarCPF(cliente.CPF);
                lblDataNascValor.Text = $"{cliente.DataNascimento:dd/MM/yyyy} ({idade} anos)";
                lblEnderecoValor.Text = cliente.Endereco;
                lblCidadeValor.Text = cliente.Cidade;
                lblCEPValor.Text = FormatarCEP(cliente.CEP);
                lblTelefoneValor.Text = string.IsNullOrEmpty(cliente.Telefone) ? "Não informado" : cliente.Telefone;
                lblINSSValor.Text = cliente.BeneficioINSS;
                lblINSS2Valor.Text = string.IsNullOrEmpty(cliente.BeneficioINSS2) ? "Não possui" : cliente.BeneficioINSS2;
                lblDataCadastroValor.Text = cliente.DataCadastro.ToString("dd/MM/yyyy HH:mm");
                lblCodigoValor.Text = cliente.ClienteID.ToString("D5");

                // Contar anexos
                int totalAnexos = anexoDAL.ContarAnexosCliente(cliente.ClienteID);
                lblTotalAnexosValor.Text = totalAnexos.ToString();

                // Atualizar título da aba
                tabPageInfo.Text = $"ℹ️ Informações ({cliente.NomeCompleto})";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao carregar informações:\n\n" + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =============================================
        // ABA 2: DOCUMENTOS E ANEXOS
        // =============================================
        private void CarregarAnexos()
        {
            try
            {
                var anexos = anexoDAL.ListarAnexosCliente(cliente.ClienteID);

                dgvAnexos.DataSource = null;
                dgvAnexos.DataSource = anexos;

                // Configurar colunas
                if (dgvAnexos.Columns.Count > 0)
                {
                    // Ocultar colunas desnecessárias
                    if (dgvAnexos.Columns.Contains("AnexoID"))
                        dgvAnexos.Columns["AnexoID"].Visible = false;
                    if (dgvAnexos.Columns.Contains("ClienteID"))
                        dgvAnexos.Columns["ClienteID"].Visible = false;
                    if (dgvAnexos.Columns.Contains("NomeArquivo"))
                        dgvAnexos.Columns["NomeArquivo"].Visible = false;
                    if (dgvAnexos.Columns.Contains("CaminhoArquivo"))
                        dgvAnexos.Columns["CaminhoArquivo"].Visible = false;
                    if (dgvAnexos.Columns.Contains("TamanhoBytes"))
                        dgvAnexos.Columns["TamanhoBytes"].Visible = false;
                    if (dgvAnexos.Columns.Contains("Ativo"))
                        dgvAnexos.Columns["Ativo"].Visible = false;
                    if (dgvAnexos.Columns.Contains("UploadPor"))
                        dgvAnexos.Columns["UploadPor"].Visible = false;
                    if (dgvAnexos.Columns.Contains("EhImagem"))
                        dgvAnexos.Columns["EhImagem"].Visible = false;

                    // Configurar colunas visíveis
                    if (dgvAnexos.Columns.Contains("IconeArquivo"))
                    {
                        dgvAnexos.Columns["IconeArquivo"].HeaderText = "";
                        dgvAnexos.Columns["IconeArquivo"].Width = 50;
                        dgvAnexos.Columns["IconeArquivo"].DefaultCellStyle.Alignment =
                            DataGridViewContentAlignment.MiddleCenter;
                    }

                    if (dgvAnexos.Columns.Contains("NomeOriginal"))
                    {
                        dgvAnexos.Columns["NomeOriginal"].HeaderText = "ARQUIVO";
                        dgvAnexos.Columns["NomeOriginal"].AutoSizeMode =
                            DataGridViewAutoSizeColumnMode.Fill;
                    }

                    if (dgvAnexos.Columns.Contains("TipoArquivo"))
                    {
                        dgvAnexos.Columns["TipoArquivo"].HeaderText = "TIPO";
                        dgvAnexos.Columns["TipoArquivo"].Width = 80;
                        dgvAnexos.Columns["TipoArquivo"].DefaultCellStyle.Alignment =
                            DataGridViewContentAlignment.MiddleCenter;
                    }

                    if (dgvAnexos.Columns.Contains("TamanhoFormatado"))
                    {
                        dgvAnexos.Columns["TamanhoFormatado"].HeaderText = "TAMANHO";
                        dgvAnexos.Columns["TamanhoFormatado"].Width = 100;
                        dgvAnexos.Columns["TamanhoFormatado"].DefaultCellStyle.Alignment =
                            DataGridViewContentAlignment.MiddleCenter;
                    }

                    if (dgvAnexos.Columns.Contains("Descricao"))
                    {
                        dgvAnexos.Columns["Descricao"].HeaderText = "DESCRIÇÃO";
                        dgvAnexos.Columns["Descricao"].Width = 200;
                    }

                    if (dgvAnexos.Columns.Contains("DataUpload"))
                    {
                        dgvAnexos.Columns["DataUpload"].HeaderText = "DATA UPLOAD";
                        dgvAnexos.Columns["DataUpload"].Width = 150;
                        dgvAnexos.Columns["DataUpload"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                        dgvAnexos.Columns["DataUpload"].DefaultCellStyle.Alignment =
                            DataGridViewContentAlignment.MiddleCenter;
                    }
                }

                // Atualizar título da aba com contagem
                tabPageAnexos.Text = $"📎 Documentos ({anexos.Count})";

                // Habilitar/desabilitar botões
                bool temAnexos = anexos.Count > 0;
                btnAbrirAnexo.Enabled = temAnexos;
                btnExcluirAnexo.Enabled = temAnexos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao carregar anexos:\n\n" + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =============================================
        // BOTÕES DE AÇÃO - ANEXOS
        // =============================================
        private void btnAdicionarAnexo_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Selecione o arquivo para anexar";
                    openFileDialog.Filter =
                        "Todos os Arquivos Permitidos|*.pdf;*.doc;*.docx;*.txt;*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.xls;*.xlsx;*.csv|" +
                        "Documentos PDF|*.pdf|" +
                        "Documentos Word|*.doc;*.docx|" +
                        "Planilhas Excel|*.xls;*.xlsx;*.csv|" +
                        "Imagens|*.jpg;*.jpeg;*.png;*.gif;*.bmp|" +
                        "Arquivos de Texto|*.txt";

                    openFileDialog.FilterIndex = 1;
                    openFileDialog.Multiselect = false;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Solicitar descrição (opcional)
                        string descricao = "";
                        using (FormDescricaoAnexo formDesc = new FormDescricaoAnexo())
                        {
                            if (formDesc.ShowDialog() == DialogResult.OK)
                            {
                                descricao = formDesc.Descricao;
                            }
                        }

                        // Criar objeto anexo
                        ClienteAnexo anexo = new ClienteAnexo
                        {
                            ClienteID = cliente.ClienteID,
                            NomeOriginal = System.IO.Path.GetFileName(openFileDialog.FileName),
                            Descricao = descricao,
                            UploadPor = Usuario.UsuarioLogado?.Nome ?? Environment.UserName
                        };

                        // Inserir anexo
                        int anexoID = anexoDAL.InserirAnexo(anexo, openFileDialog.FileName);

                        if (anexoID > 0)
                        {
                            MessageBox.Show(
                                "✔ Arquivo anexado com sucesso!\n\n" +
                                $"Arquivo: {anexo.NomeOriginal}",
                                "Sucesso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            CarregarAnexos();
                            CarregarInformacoesCliente(); // Atualizar contagem
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao adicionar anexo:\n\n" + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnAbrirAnexo_Click(object sender, EventArgs e)
        {
            AbrirAnexoSelecionado();
        }

        private void btnExcluirAnexo_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAnexos.SelectedRows.Count == 0)
                {
                    MessageBox.Show(
                        "⚠ Selecione um arquivo para excluir!",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                ClienteAnexo anexo = (ClienteAnexo)dgvAnexos.SelectedRows[0].DataBoundItem;

                DialogResult resultado = MessageBox.Show(
                    $"⚠ ATENÇÃO!\n\n" +
                    $"Deseja realmente EXCLUIR este arquivo?\n\n" +
                    $"Arquivo: {anexo.NomeOriginal}\n" +
                    $"Tipo: {anexo.TipoArquivo}\n\n" +
                    "Esta ação NÃO pode ser desfeita!",
                    "Confirmar Exclusão",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    if (anexoDAL.ExcluirAnexo(anexo.AnexoID))
                    {
                        MessageBox.Show(
                            "✔ Arquivo excluído com sucesso!",
                            "Sucesso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        CarregarAnexos();
                        CarregarInformacoesCliente(); // Atualizar contagem
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao excluir anexo:\n\n" + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dgvAnexos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                AbrirAnexoSelecionado();
            }
        }

        private void AbrirAnexoSelecionado()
        {
            try
            {
                if (dgvAnexos.SelectedRows.Count == 0)
                {
                    MessageBox.Show(
                        "⚠ Selecione um arquivo para abrir!",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                ClienteAnexo anexo = (ClienteAnexo)dgvAnexos.SelectedRows[0].DataBoundItem;
                anexoDAL.AbrirAnexo(anexo.AnexoID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao abrir arquivo:\n\n" + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =============================================
        // BOTÕES DE AÇÃO - EDITAR CLIENTE
        // =============================================
        private void btnEditarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormEditarCliente formEditar = new FormEditarCliente(cliente))
                {
                    if (formEditar.ShowDialog() == DialogResult.OK)
                    {
                        // Recarregar dados do cliente
                        Cliente clienteAtualizado = clienteDAL.ConsultarPorCPF(cliente.CPF);
                        if (clienteAtualizado != null)
                        {
                            this.cliente = clienteAtualizado;
                            CarregarInformacoesCliente();

                            MessageBox.Show(
                                "✓ Dados do cliente atualizados com sucesso!",
                                "Sucesso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao editar cliente:\n\n" + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // =============================================
        // MÉTODOS AUXILIARES
        // =============================================
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
    }

    // =============================================
    // FORMULÁRIO AUXILIAR - DESCRIÇÃO DO ANEXO
    // =============================================
    public partial class FormDescricaoAnexo : Form
    {
        public string Descricao { get; private set; }

        private TextBox txtDescricao;
        private Button btnOK;
        private Button btnCancelar;
        private Label lblTitulo;

        public FormDescricaoAnexo()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new Label();
            this.txtDescricao = new TextBox();
            this.btnOK = new Button();
            this.btnCancelar = new Button();

            this.SuspendLayout();

            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblTitulo.Location = new Point(20, 20);
            this.lblTitulo.Text = "Descrição do arquivo (opcional):";

            // txtDescricao
            this.txtDescricao.Font = new Font("Segoe UI", 11F);
            this.txtDescricao.Location = new Point(20, 50);
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Size = new Size(440, 80);
            this.txtDescricao.MaxLength = 500;

            // btnOK
            this.btnOK.BackColor = Color.FromArgb(46, 204, 113);
            this.btnOK.FlatStyle = FlatStyle.Flat;
            this.btnOK.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnOK.ForeColor = Color.White;
            this.btnOK.Location = new Point(120, 150);
            this.btnOK.Size = new Size(120, 40);
            this.btnOK.Text = "✔ OK";
            this.btnOK.Click += BtnOK_Click;

            // btnCancelar
            this.btnCancelar.BackColor = Color.FromArgb(149, 165, 166);
            this.btnCancelar.FlatStyle = FlatStyle.Flat;
            this.btnCancelar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnCancelar.ForeColor = Color.White;
            this.btnCancelar.Location = new Point(260, 150);
            this.btnCancelar.Size = new Size(120, 40);
            this.btnCancelar.Text = "Pular";
            this.btnCancelar.Click += BtnCancelar_Click;

            // FormDescricaoAnexo
            this.BackColor = Color.FromArgb(240, 248, 255);
            this.ClientSize = new Size(480, 210);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Adicionar Descrição";

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            Descricao = txtDescricao.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Descricao = "";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}