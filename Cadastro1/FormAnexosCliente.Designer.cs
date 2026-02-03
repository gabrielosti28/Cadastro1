// =============================================
// DESIGNER - FORMULÁRIO DE ANEXOS COM ABAS
// Arquivo: FormAnexosCliente.Designer.cs (NOVA VERSÃO)
// =============================================
namespace Cadastro1
{
    partial class FormAnexosCliente
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageInfo = new System.Windows.Forms.TabPage();
            this.panelInfoCliente = new System.Windows.Forms.Panel();
            this.lblCodigoValor = new System.Windows.Forms.Label();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblDataCadastroValor = new System.Windows.Forms.Label();
            this.lblDataCadastro = new System.Windows.Forms.Label();
            this.lblTotalAnexosValor = new System.Windows.Forms.Label();
            this.lblTotalAnexos = new System.Windows.Forms.Label();
            this.lblINSS2Valor = new System.Windows.Forms.Label();
            this.lblINSS2 = new System.Windows.Forms.Label();
            this.lblINSSValor = new System.Windows.Forms.Label();
            this.lblINSS = new System.Windows.Forms.Label();
            this.lblTelefoneValor = new System.Windows.Forms.Label();
            this.lblTelefone = new System.Windows.Forms.Label();
            this.lblCEPValor = new System.Windows.Forms.Label();
            this.lblCEP = new System.Windows.Forms.Label();
            this.lblCidadeValor = new System.Windows.Forms.Label();
            this.lblCidade = new System.Windows.Forms.Label();
            this.lblEnderecoValor = new System.Windows.Forms.Label();
            this.lblEndereco = new System.Windows.Forms.Label();
            this.lblDataNascValor = new System.Windows.Forms.Label();
            this.lblDataNasc = new System.Windows.Forms.Label();
            this.lblCPFValor = new System.Windows.Forms.Label();
            this.lblCPF = new System.Windows.Forms.Label();
            this.lblNomeValor = new System.Windows.Forms.Label();
            this.lblNome = new System.Windows.Forms.Label();
            this.lblTituloInfo = new System.Windows.Forms.Label();
            this.btnEditarCliente = new System.Windows.Forms.Button();
            this.tabPageAnexos = new System.Windows.Forms.TabPage();
            this.lblInfoAnexos = new System.Windows.Forms.Label();
            this.dgvAnexos = new System.Windows.Forms.DataGridView();
            this.panelBotoesAnexos = new System.Windows.Forms.Panel();
            this.btnAdicionarAnexo = new System.Windows.Forms.Button();
            this.btnAbrirAnexo = new System.Windows.Forms.Button();
            this.btnExcluirAnexo = new System.Windows.Forms.Button();
            this.lblTituloAnexos = new System.Windows.Forms.Label();
            this.btnFechar = new System.Windows.Forms.Button();
            this.panelContainer.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageInfo.SuspendLayout();
            this.panelInfoCliente.SuspendLayout();
            this.tabPageAnexos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnexos)).BeginInit();
            this.panelBotoesAnexos.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelContainer.Controls.Add(this.tabControl);
            this.panelContainer.Controls.Add(this.btnFechar);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1100, 700);
            this.panelContainer.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageInfo);
            this.tabControl.Controls.Add(this.tabPageAnexos);
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1076, 620);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageInfo
            // 
            this.tabPageInfo.BackColor = System.Drawing.Color.White;
            this.tabPageInfo.Controls.Add(this.panelInfoCliente);
            this.tabPageInfo.Location = new System.Drawing.Point(4, 29);
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInfo.Size = new System.Drawing.Size(1068, 587);
            this.tabPageInfo.TabIndex = 0;
            this.tabPageInfo.Text = "ℹ️ Informações do Cliente";
            // 
            // panelInfoCliente
            // 
            this.panelInfoCliente.BackColor = System.Drawing.Color.White;
            this.panelInfoCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInfoCliente.Controls.Add(this.lblCodigoValor);
            this.panelInfoCliente.Controls.Add(this.lblCodigo);
            this.panelInfoCliente.Controls.Add(this.lblDataCadastroValor);
            this.panelInfoCliente.Controls.Add(this.lblDataCadastro);
            this.panelInfoCliente.Controls.Add(this.lblTotalAnexosValor);
            this.panelInfoCliente.Controls.Add(this.lblTotalAnexos);
            this.panelInfoCliente.Controls.Add(this.lblINSS2Valor);
            this.panelInfoCliente.Controls.Add(this.lblINSS2);
            this.panelInfoCliente.Controls.Add(this.lblINSSValor);
            this.panelInfoCliente.Controls.Add(this.lblINSS);
            this.panelInfoCliente.Controls.Add(this.lblTelefoneValor);
            this.panelInfoCliente.Controls.Add(this.lblTelefone);
            this.panelInfoCliente.Controls.Add(this.lblCEPValor);
            this.panelInfoCliente.Controls.Add(this.lblCEP);
            this.panelInfoCliente.Controls.Add(this.lblCidadeValor);
            this.panelInfoCliente.Controls.Add(this.lblCidade);
            this.panelInfoCliente.Controls.Add(this.lblEnderecoValor);
            this.panelInfoCliente.Controls.Add(this.lblEndereco);
            this.panelInfoCliente.Controls.Add(this.lblDataNascValor);
            this.panelInfoCliente.Controls.Add(this.lblDataNasc);
            this.panelInfoCliente.Controls.Add(this.lblCPFValor);
            this.panelInfoCliente.Controls.Add(this.lblCPF);
            this.panelInfoCliente.Controls.Add(this.lblNomeValor);
            this.panelInfoCliente.Controls.Add(this.lblNome);
            this.panelInfoCliente.Controls.Add(this.lblTituloInfo);
            this.panelInfoCliente.Controls.Add(this.btnEditarCliente);
            this.panelInfoCliente.Location = new System.Drawing.Point(20, 15);
            this.panelInfoCliente.Name = "panelInfoCliente";
            this.panelInfoCliente.Size = new System.Drawing.Size(1028, 555);
            this.panelInfoCliente.TabIndex = 0;
            // 
            // lblTituloInfo
            // 
            this.lblTituloInfo.AutoSize = true;
            this.lblTituloInfo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTituloInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTituloInfo.Location = new System.Drawing.Point(300, 15);
            this.lblTituloInfo.Name = "lblTituloInfo";
            this.lblTituloInfo.Size = new System.Drawing.Size(380, 30);
            this.lblTituloInfo.TabIndex = 0;
            this.lblTituloInfo.Text = "📋 INFORMAÇÕES CADASTRAIS";
            // 
            // Campos de informação (labels)
            int yPos = 70;
            int spacing = 45;

            // Nome
            this.lblNome = new System.Windows.Forms.Label();
            this.lblNome.AutoSize = true;
            this.lblNome.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNome.Location = new System.Drawing.Point(40, yPos);
            this.lblNome.Text = "👤 Nome Completo:";

            this.lblNomeValor = new System.Windows.Forms.Label();
            this.lblNomeValor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNomeValor.Location = new System.Drawing.Point(250, yPos);
            this.lblNomeValor.Size = new System.Drawing.Size(700, 25);
            this.lblNomeValor.Text = "-";

            yPos += spacing;

            // CPF
            this.lblCPF = new System.Windows.Forms.Label();
            this.lblCPF.AutoSize = true;
            this.lblCPF.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCPF.Location = new System.Drawing.Point(40, yPos);
            this.lblCPF.Text = "🆔 CPF:";

            this.lblCPFValor = new System.Windows.Forms.Label();
            this.lblCPFValor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCPFValor.Location = new System.Drawing.Point(250, yPos);
            this.lblCPFValor.Size = new System.Drawing.Size(300, 25);
            this.lblCPFValor.Text = "-";

            yPos += spacing;

            // Data Nascimento
            this.lblDataNasc = new System.Windows.Forms.Label();
            this.lblDataNasc.AutoSize = true;
            this.lblDataNasc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDataNasc.Location = new System.Drawing.Point(40, yPos);
            this.lblDataNasc.Text = "🎂 Data de Nascimento:";

            this.lblDataNascValor = new System.Windows.Forms.Label();
            this.lblDataNascValor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDataNascValor.Location = new System.Drawing.Point(250, yPos);
            this.lblDataNascValor.Size = new System.Drawing.Size(300, 25);
            this.lblDataNascValor.Text = "-";

            yPos += spacing;

            // Endereço
            this.lblEndereco = new System.Windows.Forms.Label();
            this.lblEndereco.AutoSize = true;
            this.lblEndereco.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEndereco.Location = new System.Drawing.Point(40, yPos);
            this.lblEndereco.Text = "🏠 Endereço:";

            this.lblEnderecoValor = new System.Windows.Forms.Label();
            this.lblEnderecoValor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEnderecoValor.Location = new System.Drawing.Point(250, yPos);
            this.lblEnderecoValor.Size = new System.Drawing.Size(700, 25);
            this.lblEnderecoValor.Text = "-";

            yPos += spacing;

            // Cidade
            this.lblCidade = new System.Windows.Forms.Label();
            this.lblCidade.AutoSize = true;
            this.lblCidade.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCidade.Location = new System.Drawing.Point(40, yPos);
            this.lblCidade.Text = "🏙️ Cidade:";

            this.lblCidadeValor = new System.Windows.Forms.Label();
            this.lblCidadeValor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCidadeValor.Location = new System.Drawing.Point(250, yPos);
            this.lblCidadeValor.Size = new System.Drawing.Size(300, 25);
            this.lblCidadeValor.Text = "-";

            yPos += spacing;

            // CEP
            this.lblCEP = new System.Windows.Forms.Label();
            this.lblCEP.AutoSize = true;
            this.lblCEP.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCEP.Location = new System.Drawing.Point(40, yPos);
            this.lblCEP.Text = "📮 CEP:";

            this.lblCEPValor = new System.Windows.Forms.Label();
            this.lblCEPValor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCEPValor.Location = new System.Drawing.Point(250, yPos);
            this.lblCEPValor.Size = new System.Drawing.Size(200, 25);
            this.lblCEPValor.Text = "-";

            yPos += spacing;

            // Telefone
            this.lblTelefone = new System.Windows.Forms.Label();
            this.lblTelefone.AutoSize = true;
            this.lblTelefone.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTelefone.Location = new System.Drawing.Point(40, yPos);
            this.lblTelefone.Text = "📞 Telefone:";

            this.lblTelefoneValor = new System.Windows.Forms.Label();
            this.lblTelefoneValor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTelefoneValor.Location = new System.Drawing.Point(250, yPos);
            this.lblTelefoneValor.Size = new System.Drawing.Size(300, 25);
            this.lblTelefoneValor.Text = "-";

            yPos += spacing;

            // INSS
            this.lblINSS = new System.Windows.Forms.Label();
            this.lblINSS.AutoSize = true;
            this.lblINSS.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblINSS.Location = new System.Drawing.Point(40, yPos);
            this.lblINSS.Text = "💼 Benefício INSS:";

            this.lblINSSValor = new System.Windows.Forms.Label();
            this.lblINSSValor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblINSSValor.Location = new System.Drawing.Point(250, yPos);
            this.lblINSSValor.Size = new System.Drawing.Size(300, 25);
            this.lblINSSValor.Text = "-";

            yPos += spacing;

            // INSS 2
            this.lblINSS2 = new System.Windows.Forms.Label();
            this.lblINSS2.AutoSize = true;
            this.lblINSS2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblINSS2.Location = new System.Drawing.Point(40, yPos);
            this.lblINSS2.Text = "💼 2º Benefício INSS:";

            this.lblINSS2Valor = new System.Windows.Forms.Label();
            this.lblINSS2Valor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblINSS2Valor.Location = new System.Drawing.Point(250, yPos);
            this.lblINSS2Valor.Size = new System.Drawing.Size(300, 25);
            this.lblINSS2Valor.Text = "-";

            yPos += spacing;

            // Total Anexos
            this.lblTotalAnexos = new System.Windows.Forms.Label();
            this.lblTotalAnexos.AutoSize = true;
            this.lblTotalAnexos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalAnexos.Location = new System.Drawing.Point(40, yPos);
            this.lblTotalAnexos.Text = "📎 Total de Documentos:";

            this.lblTotalAnexosValor = new System.Windows.Forms.Label();
            this.lblTotalAnexosValor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotalAnexosValor.Location = new System.Drawing.Point(250, yPos);
            this.lblTotalAnexosValor.Size = new System.Drawing.Size(100, 25);
            this.lblTotalAnexosValor.Text = "-";

            yPos += spacing;

            // Data Cadastro
            this.lblDataCadastro = new System.Windows.Forms.Label();
            this.lblDataCadastro.AutoSize = true;
            this.lblDataCadastro.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDataCadastro.Location = new System.Drawing.Point(40, yPos);
            this.lblDataCadastro.Text = "📅 Cadastrado em:";

            this.lblDataCadastroValor = new System.Windows.Forms.Label();
            this.lblDataCadastroValor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDataCadastroValor.Location = new System.Drawing.Point(250, yPos);
            this.lblDataCadastroValor.Size = new System.Drawing.Size(300, 25);
            this.lblDataCadastroValor.Text = "-";

            yPos += spacing;

            // Código
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCodigo.Location = new System.Drawing.Point(40, yPos);
            this.lblCodigo.Text = "🔢 Código do Cliente:";

            this.lblCodigoValor = new System.Windows.Forms.Label();
            this.lblCodigoValor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCodigoValor.Location = new System.Drawing.Point(250, yPos);
            this.lblCodigoValor.Size = new System.Drawing.Size(100, 25);
            this.lblCodigoValor.Text = "-";

            // btnEditarCliente
            this.btnEditarCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnEditarCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditarCliente.FlatAppearance.BorderSize = 0;
            this.btnEditarCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditarCliente.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnEditarCliente.ForeColor = System.Drawing.Color.White;
            this.btnEditarCliente.Location = new System.Drawing.Point(400, 495);
            this.btnEditarCliente.Name = "btnEditarCliente";
            this.btnEditarCliente.Size = new System.Drawing.Size(200, 45);
            this.btnEditarCliente.TabIndex = 25;
            this.btnEditarCliente.Text = "✏️ EDITAR DADOS";
            this.btnEditarCliente.UseVisualStyleBackColor = false;
            this.btnEditarCliente.Click += new System.EventHandler(this.btnEditarCliente_Click);
            // 
            // tabPageAnexos
            // 
            this.tabPageAnexos.BackColor = System.Drawing.Color.White;
            this.tabPageAnexos.Controls.Add(this.lblInfoAnexos);
            this.tabPageAnexos.Controls.Add(this.dgvAnexos);
            this.tabPageAnexos.Controls.Add(this.panelBotoesAnexos);
            this.tabPageAnexos.Controls.Add(this.lblTituloAnexos);
            this.tabPageAnexos.Location = new System.Drawing.Point(4, 29);
            this.tabPageAnexos.Name = "tabPageAnexos";
            this.tabPageAnexos.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAnexos.Size = new System.Drawing.Size(1068, 587);
            this.tabPageAnexos.TabIndex = 1;
            this.tabPageAnexos.Text = "📎 Documentos e Anexos";
            // 
            // lblTituloAnexos
            // 
            this.lblTituloAnexos.AutoSize = true;
            this.lblTituloAnexos.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTituloAnexos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTituloAnexos.Location = new System.Drawing.Point(330, 15);
            this.lblTituloAnexos.Name = "lblTituloAnexos";
            this.lblTituloAnexos.Size = new System.Drawing.Size(370, 30);
            this.lblTituloAnexos.TabIndex = 0;
            this.lblTituloAnexos.Text = "📎 DOCUMENTOS DO CLIENTE";
            // 
            // panelBotoesAnexos
            // 
            this.panelBotoesAnexos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelBotoesAnexos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBotoesAnexos.Controls.Add(this.btnAdicionarAnexo);
            this.panelBotoesAnexos.Controls.Add(this.btnAbrirAnexo);
            this.panelBotoesAnexos.Controls.Add(this.btnExcluirAnexo);
            this.panelBotoesAnexos.Location = new System.Drawing.Point(20, 60);
            this.panelBotoesAnexos.Name = "panelBotoesAnexos";
            this.panelBotoesAnexos.Size = new System.Drawing.Size(1028, 60);
            this.panelBotoesAnexos.TabIndex = 1;
            // 
            // btnAdicionarAnexo
            // 
            this.btnAdicionarAnexo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAdicionarAnexo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdicionarAnexo.FlatAppearance.BorderSize = 0;
            this.btnAdicionarAnexo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdicionarAnexo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnAdicionarAnexo.ForeColor = System.Drawing.Color.White;
            this.btnAdicionarAnexo.Location = new System.Drawing.Point(200, 10);
            this.btnAdicionarAnexo.Name = "btnAdicionarAnexo";
            this.btnAdicionarAnexo.Size = new System.Drawing.Size(200, 38);
            this.btnAdicionarAnexo.TabIndex = 0;
            this.btnAdicionarAnexo.Text = "➕ ADICIONAR";
            this.btnAdicionarAnexo.UseVisualStyleBackColor = false;
            this.btnAdicionarAnexo.Click += new System.EventHandler(this.btnAdicionarAnexo_Click);
            // 
            // btnAbrirAnexo
            // 
            this.btnAbrirAnexo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnAbrirAnexo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbrirAnexo.FlatAppearance.BorderSize = 0;
            this.btnAbrirAnexo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbrirAnexo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnAbrirAnexo.ForeColor = System.Drawing.Color.White;
            this.btnAbrirAnexo.Location = new System.Drawing.Point(415, 10);
            this.btnAbrirAnexo.Name = "btnAbrirAnexo";
            this.btnAbrirAnexo.Size = new System.Drawing.Size(200, 38);
            this.btnAbrirAnexo.TabIndex = 1;
            this.btnAbrirAnexo.Text = "📂 ABRIR";
            this.btnAbrirAnexo.UseVisualStyleBackColor = false;
            this.btnAbrirAnexo.Click += new System.EventHandler(this.btnAbrirAnexo_Click);
            // 
            // btnExcluirAnexo
            // 
            this.btnExcluirAnexo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnExcluirAnexo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcluirAnexo.FlatAppearance.BorderSize = 0;
            this.btnExcluirAnexo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcluirAnexo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnExcluirAnexo.ForeColor = System.Drawing.Color.White;
            this.btnExcluirAnexo.Location = new System.Drawing.Point(630, 10);
            this.btnExcluirAnexo.Name = "btnExcluirAnexo";
            this.btnExcluirAnexo.Size = new System.Drawing.Size(200, 38);
            this.btnExcluirAnexo.TabIndex = 2;
            this.btnExcluirAnexo.Text = "🗑️ EXCLUIR";
            this.btnExcluirAnexo.UseVisualStyleBackColor = false;
            this.btnExcluirAnexo.Click += new System.EventHandler(this.btnExcluirAnexo_Click);
            // 
            // dgvAnexos
            // 
            this.dgvAnexos.AllowUserToAddRows = false;
            this.dgvAnexos.AllowUserToDeleteRows = false;
            this.dgvAnexos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAnexos.BackgroundColor = System.Drawing.Color.White;
            this.dgvAnexos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAnexos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAnexos.ColumnHeadersHeight = 40;
            this.dgvAnexos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAnexos.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAnexos.EnableHeadersVisualStyles = false;
            this.dgvAnexos.Location = new System.Drawing.Point(20, 165);
            this.dgvAnexos.MultiSelect = false;
            this.dgvAnexos.Name = "dgvAnexos";
            this.dgvAnexos.ReadOnly = true;
            this.dgvAnexos.RowHeadersVisible = false;
            this.dgvAnexos.RowTemplate.Height = 35;
            this.dgvAnexos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAnexos.Size = new System.Drawing.Size(1028, 405);
            this.dgvAnexos.TabIndex = 2;
            this.dgvAnexos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAnexos_CellDoubleClick);
            // 
            // lblInfoAnexos
            // 
            this.lblInfoAnexos.AutoSize = true;
            this.lblInfoAnexos.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic);
            this.lblInfoAnexos.ForeColor = System.Drawing.Color.Gray;
            this.lblInfoAnexos.Location = new System.Drawing.Point(20, 135);
            this.lblInfoAnexos.Name = "lblInfoAnexos";
            this.lblInfoAnexos.Size = new System.Drawing.Size(650, 17);
            this.lblInfoAnexos.TabIndex = 3;
            this.lblInfoAnexos.Text = "💡 Dica: Clique duas vezes em um arquivo para abri-lo. Tipos permitidos: PDF, Word, Excel, Imagens, TXT (max: 50MB)";
            // 
            // btnFechar
            // 
            this.btnFechar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(450, 645);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(200, 45);
            this.btnFechar.TabIndex = 1;
            this.btnFechar.Text = "✖ FECHAR";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // FormAnexosCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.panelContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormAnexosCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Informações e Documentos do Cliente";
            this.Load += new System.EventHandler(this.FormAnexosCliente_Load);
            this.panelContainer.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageInfo.ResumeLayout(false);
            this.panelInfoCliente.ResumeLayout(false);
            this.panelInfoCliente.PerformLayout();
            this.tabPageAnexos.ResumeLayout(false);
            this.tabPageAnexos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnexos)).EndInit();
            this.panelBotoesAnexos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageInfo;
        private System.Windows.Forms.TabPage tabPageAnexos;
        private System.Windows.Forms.Panel panelInfoCliente;
        private System.Windows.Forms.Label lblTituloInfo;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Label lblNomeValor;
        private System.Windows.Forms.Label lblCPF;
        private System.Windows.Forms.Label lblCPFValor;
        private System.Windows.Forms.Label lblDataNasc;
        private System.Windows.Forms.Label lblDataNascValor;
        private System.Windows.Forms.Label lblEndereco;
        private System.Windows.Forms.Label lblEnderecoValor;
        private System.Windows.Forms.Label lblCidade;
        private System.Windows.Forms.Label lblCidadeValor;
        private System.Windows.Forms.Label lblCEP;
        private System.Windows.Forms.Label lblCEPValor;
        private System.Windows.Forms.Label lblTelefone;
        private System.Windows.Forms.Label lblTelefoneValor;
        private System.Windows.Forms.Label lblINSS;
        private System.Windows.Forms.Label lblINSSValor;
        private System.Windows.Forms.Label lblINSS2;
        private System.Windows.Forms.Label lblINSS2Valor;
        private System.Windows.Forms.Label lblTotalAnexos;
        private System.Windows.Forms.Label lblTotalAnexosValor;
        private System.Windows.Forms.Label lblDataCadastro;
        private System.Windows.Forms.Label lblDataCadastroValor;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblCodigoValor;
        private System.Windows.Forms.Button btnEditarCliente;
        private System.Windows.Forms.Label lblTituloAnexos;
        private System.Windows.Forms.Panel panelBotoesAnexos;
        private System.Windows.Forms.Button btnAdicionarAnexo;
        private System.Windows.Forms.Button btnAbrirAnexo;
        private System.Windows.Forms.Button btnExcluirAnexo;
        private System.Windows.Forms.DataGridView dgvAnexos;
        private System.Windows.Forms.Label lblInfoAnexos;
        private System.Windows.Forms.Button btnFechar;
    }
}