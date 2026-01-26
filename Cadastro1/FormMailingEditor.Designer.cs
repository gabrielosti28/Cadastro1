namespace Cadastro1
{
    partial class FormMailingEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.panelTemplate = new System.Windows.Forms.Panel();
            this.lblStatusPosicoes = new System.Windows.Forms.Label();
            this.btnLimparPosicoes = new System.Windows.Forms.Button();
            this.btnSalvarTemplate = new System.Windows.Forms.Button();
            this.lblInfoPosicoes = new System.Windows.Forms.Label();
            this.btnDefinirCEP = new System.Windows.Forms.Button();
            this.btnDefinirCidade = new System.Windows.Forms.Button();
            this.btnDefinirEndereco = new System.Windows.Forms.Button();
            this.btnDefinirNome = new System.Windows.Forms.Button();
            this.txtNomeTemplate = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.btnCarregarImagem = new System.Windows.Forms.Button();
            this.lblTemplateTitulo = new System.Windows.Forms.Label();
            this.panelClientes = new System.Windows.Forms.Panel();
            this.btnGerarPDF = new System.Windows.Forms.Button();
            this.lblTotalSelecionados = new System.Windows.Forms.Label();
            this.btnMarcarTodos = new System.Windows.Forms.Button();
            this.chkClientes = new System.Windows.Forms.CheckedListBox();
            this.btnLimparBusca = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscaCPF = new System.Windows.Forms.TextBox();
            this.lblBusca = new System.Windows.Forms.Label();
            this.lblClientesTitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblInstrucoes = new System.Windows.Forms.Label();
            this.panelPreview = new System.Windows.Forms.Panel();
            this.lblPreviewTitulo = new System.Windows.Forms.Label();
            this.btnFechar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.panelTemplate.SuspendLayout();
            this.panelClientes.SuspendLayout();
            this.panelPreview.SuspendLayout();
            this.SuspendLayout();
            // 
            // picPreview
            // 
            this.picPreview.BackColor = System.Drawing.Color.White;
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picPreview.Location = new System.Drawing.Point(10, 40);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(595, 530);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 0;
            this.picPreview.TabStop = false;
            this.picPreview.Click += new System.EventHandler(this.PicPreview_Click);
            this.picPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.PicPreview_Paint);
            // 
            // panelTemplate
            // 
            this.panelTemplate.BackColor = System.Drawing.Color.White;
            this.panelTemplate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTemplate.Controls.Add(this.lblStatusPosicoes);
            this.panelTemplate.Controls.Add(this.btnLimparPosicoes);
            this.panelTemplate.Controls.Add(this.btnSalvarTemplate);
            this.panelTemplate.Controls.Add(this.lblInfoPosicoes);
            this.panelTemplate.Controls.Add(this.btnDefinirCEP);
            this.panelTemplate.Controls.Add(this.btnDefinirCidade);
            this.panelTemplate.Controls.Add(this.btnDefinirEndereco);
            this.panelTemplate.Controls.Add(this.btnDefinirNome);
            this.panelTemplate.Controls.Add(this.txtNomeTemplate);
            this.panelTemplate.Controls.Add(this.lblNome);
            this.panelTemplate.Controls.Add(this.btnCarregarImagem);
            this.panelTemplate.Controls.Add(this.lblTemplateTitulo);
            this.panelTemplate.Location = new System.Drawing.Point(670, 70);
            this.panelTemplate.Name = "panelTemplate";
            this.panelTemplate.Size = new System.Drawing.Size(700, 290);
            this.panelTemplate.TabIndex = 1;
            // 
            // lblStatusPosicoes
            // 
            this.lblStatusPosicoes.Font = new System.Drawing.Font("Consolas", 8F);
            this.lblStatusPosicoes.Location = new System.Drawing.Point(15, 230);
            this.lblStatusPosicoes.Name = "lblStatusPosicoes";
            this.lblStatusPosicoes.Size = new System.Drawing.Size(670, 50);
            this.lblStatusPosicoes.TabIndex = 11;
            this.lblStatusPosicoes.Text = "✓ Nome: Não definido\r\n✓ Endereço: Não definido\r\n✓ Cidade: Não definido\r\n✓ CEP: Nã" +
    "o definido";
            // 
            // btnLimparPosicoes
            // 
            this.btnLimparPosicoes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnLimparPosicoes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimparPosicoes.FlatAppearance.BorderSize = 0;
            this.btnLimparPosicoes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimparPosicoes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLimparPosicoes.ForeColor = System.Drawing.Color.White;
            this.btnLimparPosicoes.Location = new System.Drawing.Point(15, 185);
            this.btnLimparPosicoes.Name = "btnLimparPosicoes";
            this.btnLimparPosicoes.Size = new System.Drawing.Size(160, 35);
            this.btnLimparPosicoes.TabIndex = 10;
            this.btnLimparPosicoes.Text = "🗑️ Limpar Posições";
            this.btnLimparPosicoes.UseVisualStyleBackColor = false;
            // 
            // btnSalvarTemplate
            // 
            this.btnSalvarTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnSalvarTemplate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvarTemplate.FlatAppearance.BorderSize = 0;
            this.btnSalvarTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvarTemplate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSalvarTemplate.ForeColor = System.Drawing.Color.White;
            this.btnSalvarTemplate.Location = new System.Drawing.Point(190, 185);
            this.btnSalvarTemplate.Name = "btnSalvarTemplate";
            this.btnSalvarTemplate.Size = new System.Drawing.Size(240, 35);
            this.btnSalvarTemplate.TabIndex = 9;
            this.btnSalvarTemplate.Text = "💾 Salvar Template";
            this.btnSalvarTemplate.UseVisualStyleBackColor = false;
            this.btnSalvarTemplate.Click += new System.EventHandler(this.BtnSalvarTemplate_Click);
            // 
            // lblInfoPosicoes
            // 
            this.lblInfoPosicoes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblInfoPosicoes.ForeColor = System.Drawing.Color.Gray;
            this.lblInfoPosicoes.Location = new System.Drawing.Point(15, 105);
            this.lblInfoPosicoes.Name = "lblInfoPosicoes";
            this.lblInfoPosicoes.Size = new System.Drawing.Size(670, 20);
            this.lblInfoPosicoes.TabIndex = 8;
            this.lblInfoPosicoes.Text = "Clique nos botões abaixo e depois clique na imagem:";
            // 
            // btnDefinirCEP
            // 
            this.btnDefinirCEP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDefinirCEP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDefinirCEP.FlatAppearance.BorderSize = 0;
            this.btnDefinirCEP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDefinirCEP.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDefinirCEP.ForeColor = System.Drawing.Color.White;
            this.btnDefinirCEP.Location = new System.Drawing.Point(510, 130);
            this.btnDefinirCEP.Name = "btnDefinirCEP";
            this.btnDefinirCEP.Size = new System.Drawing.Size(155, 40);
            this.btnDefinirCEP.TabIndex = 7;
            this.btnDefinirCEP.Text = "📮 CEP";
            this.btnDefinirCEP.UseVisualStyleBackColor = false;
            // 
            // btnDefinirCidade
            // 
            this.btnDefinirCidade.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnDefinirCidade.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDefinirCidade.FlatAppearance.BorderSize = 0;
            this.btnDefinirCidade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDefinirCidade.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDefinirCidade.ForeColor = System.Drawing.Color.White;
            this.btnDefinirCidade.Location = new System.Drawing.Point(345, 130);
            this.btnDefinirCidade.Name = "btnDefinirCidade";
            this.btnDefinirCidade.Size = new System.Drawing.Size(155, 40);
            this.btnDefinirCidade.TabIndex = 6;
            this.btnDefinirCidade.Text = "🌆 Cidade";
            this.btnDefinirCidade.UseVisualStyleBackColor = false;
            // 
            // btnDefinirEndereco
            // 
            this.btnDefinirEndereco.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnDefinirEndereco.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDefinirEndereco.FlatAppearance.BorderSize = 0;
            this.btnDefinirEndereco.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDefinirEndereco.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDefinirEndereco.ForeColor = System.Drawing.Color.White;
            this.btnDefinirEndereco.Location = new System.Drawing.Point(180, 130);
            this.btnDefinirEndereco.Name = "btnDefinirEndereco";
            this.btnDefinirEndereco.Size = new System.Drawing.Size(155, 40);
            this.btnDefinirEndereco.TabIndex = 5;
            this.btnDefinirEndereco.Text = "🏠 Endereço";
            this.btnDefinirEndereco.UseVisualStyleBackColor = false;
            // 
            // btnDefinirNome
            // 
            this.btnDefinirNome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnDefinirNome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDefinirNome.FlatAppearance.BorderSize = 0;
            this.btnDefinirNome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDefinirNome.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDefinirNome.ForeColor = System.Drawing.Color.White;
            this.btnDefinirNome.Location = new System.Drawing.Point(15, 130);
            this.btnDefinirNome.Name = "btnDefinirNome";
            this.btnDefinirNome.Size = new System.Drawing.Size(155, 40);
            this.btnDefinirNome.TabIndex = 4;
            this.btnDefinirNome.Text = "👤 Nome";
            this.btnDefinirNome.UseVisualStyleBackColor = false;
            // 
            // txtNomeTemplate
            // 
            this.txtNomeTemplate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNomeTemplate.Location = new System.Drawing.Point(15, 65);
            this.txtNomeTemplate.Name = "txtNomeTemplate";
            this.txtNomeTemplate.Size = new System.Drawing.Size(300, 25);
            this.txtNomeTemplate.TabIndex = 3;
            this.txtNomeTemplate.Text = "Template_20250117";
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNome.Location = new System.Drawing.Point(15, 45);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(110, 15);
            this.lblNome.TabIndex = 2;
            this.lblNome.Text = "Nome do Template:";
            // 
            // btnCarregarImagem
            // 
            this.btnCarregarImagem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnCarregarImagem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCarregarImagem.FlatAppearance.BorderSize = 0;
            this.btnCarregarImagem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCarregarImagem.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCarregarImagem.ForeColor = System.Drawing.Color.White;
            this.btnCarregarImagem.Location = new System.Drawing.Point(330, 45);
            this.btnCarregarImagem.Name = "btnCarregarImagem";
            this.btnCarregarImagem.Size = new System.Drawing.Size(350, 45);
            this.btnCarregarImagem.TabIndex = 1;
            this.btnCarregarImagem.Text = "📄 1. CARREGAR IMAGEM A4";
            this.btnCarregarImagem.UseVisualStyleBackColor = false;
            this.btnCarregarImagem.Click += new System.EventHandler(this.BtnCarregarImagem_Click);
            // 
            // lblTemplateTitulo
            // 
            this.lblTemplateTitulo.AutoSize = true;
            this.lblTemplateTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTemplateTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTemplateTitulo.Location = new System.Drawing.Point(15, 10);
            this.lblTemplateTitulo.Name = "lblTemplateTitulo";
            this.lblTemplateTitulo.Size = new System.Drawing.Size(257, 21);
            this.lblTemplateTitulo.TabIndex = 0;
            this.lblTemplateTitulo.Text = "🎯 ETAPA 1: CONFIGURAR POSIÇÕES";
            // 
            // panelClientes
            // 
            this.panelClientes.BackColor = System.Drawing.Color.White;
            this.panelClientes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelClientes.Controls.Add(this.btnGerarPDF);
            this.panelClientes.Controls.Add(this.lblTotalSelecionados);
            this.panelClientes.Controls.Add(this.btnMarcarTodos);
            this.panelClientes.Controls.Add(this.chkClientes);
            this.panelClientes.Controls.Add(this.btnLimparBusca);
            this.panelClientes.Controls.Add(this.btnBuscar);
            this.panelClientes.Controls.Add(this.txtBuscaCPF);
            this.panelClientes.Controls.Add(this.lblBusca);
            this.panelClientes.Controls.Add(this.lblClientesTitulo);
            this.panelClientes.Location = new System.Drawing.Point(670, 380);
            this.panelClientes.Name = "panelClientes";
            this.panelClientes.Size = new System.Drawing.Size(700, 380);
            this.panelClientes.TabIndex = 2;
            // 
            // btnGerarPDF
            // 
            this.btnGerarPDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnGerarPDF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGerarPDF.FlatAppearance.BorderSize = 0;
            this.btnGerarPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGerarPDF.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnGerarPDF.ForeColor = System.Drawing.Color.White;
            this.btnGerarPDF.Location = new System.Drawing.Point(390, 300);
            this.btnGerarPDF.Name = "btnGerarPDF";
            this.btnGerarPDF.Size = new System.Drawing.Size(295, 40);
            this.btnGerarPDF.TabIndex = 8;
            this.btnGerarPDF.Text = "📄 GERAR PDF PARA IMPRESSÃO";
            this.btnGerarPDF.UseVisualStyleBackColor = false;
            this.btnGerarPDF.Click += new System.EventHandler(this.BtnGerarPDF_Click);
            // 
            // lblTotalSelecionados
            // 
            this.lblTotalSelecionados.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalSelecionados.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTotalSelecionados.Location = new System.Drawing.Point(180, 305);
            this.lblTotalSelecionados.Name = "lblTotalSelecionados";
            this.lblTotalSelecionados.Size = new System.Drawing.Size(200, 30);
            this.lblTotalSelecionados.TabIndex = 7;
            this.lblTotalSelecionados.Text = "0 clientes selecionados";
            this.lblTotalSelecionados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnMarcarTodos
            // 
            this.btnMarcarTodos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnMarcarTodos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMarcarTodos.FlatAppearance.BorderSize = 0;
            this.btnMarcarTodos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarcarTodos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnMarcarTodos.ForeColor = System.Drawing.Color.White;
            this.btnMarcarTodos.Location = new System.Drawing.Point(15, 305);
            this.btnMarcarTodos.Name = "btnMarcarTodos";
            this.btnMarcarTodos.Size = new System.Drawing.Size(150, 30);
            this.btnMarcarTodos.TabIndex = 6;
            this.btnMarcarTodos.Text = "✓ Marcar Todos";
            this.btnMarcarTodos.UseVisualStyleBackColor = false;
            this.btnMarcarTodos.Click += new System.EventHandler(this.BtnMarcarTodos_Click);
            // 
            // chkClientes
            // 
            this.chkClientes.CheckOnClick = true;
            this.chkClientes.Font = new System.Drawing.Font("Consolas", 9F);
            this.chkClientes.FormattingEnabled = true;
            this.chkClientes.Location = new System.Drawing.Point(15, 105);
            this.chkClientes.Name = "chkClientes";
            this.chkClientes.Size = new System.Drawing.Size(670, 190);
            this.chkClientes.TabIndex = 5;
            // 
            // btnLimparBusca
            // 
            this.btnLimparBusca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnLimparBusca.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimparBusca.FlatAppearance.BorderSize = 0;
            this.btnLimparBusca.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimparBusca.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLimparBusca.ForeColor = System.Drawing.Color.White;
            this.btnLimparBusca.Location = new System.Drawing.Point(475, 63);
            this.btnLimparBusca.Name = "btnLimparBusca";
            this.btnLimparBusca.Size = new System.Drawing.Size(40, 30);
            this.btnLimparBusca.TabIndex = 4;
            this.btnLimparBusca.Text = "✖";
            this.btnLimparBusca.UseVisualStyleBackColor = false;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(425, 63);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(40, 30);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "🔍";
            this.btnBuscar.UseVisualStyleBackColor = false;
            // 
            // txtBuscaCPF
            // 
            this.txtBuscaCPF.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtBuscaCPF.Location = new System.Drawing.Point(15, 65);
            this.txtBuscaCPF.Name = "txtBuscaCPF";
            this.txtBuscaCPF.Size = new System.Drawing.Size(400, 27);
            this.txtBuscaCPF.TabIndex = 2;
            this.txtBuscaCPF.TextChanged += new System.EventHandler(this.TxtBuscaCPF_TextChanged);
            // 
            // lblBusca
            // 
            this.lblBusca.AutoSize = true;
            this.lblBusca.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBusca.Location = new System.Drawing.Point(15, 45);
            this.lblBusca.Name = "lblBusca";
            this.lblBusca.Size = new System.Drawing.Size(96, 15);
            this.lblBusca.TabIndex = 1;
            this.lblBusca.Text = "🔍 Buscar por CPF:";
            // 
            // lblClientesTitulo
            // 
            this.lblClientesTitulo.AutoSize = true;
            this.lblClientesTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblClientesTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblClientesTitulo.Location = new System.Drawing.Point(15, 10);
            this.lblClientesTitulo.Name = "lblClientesTitulo";
            this.lblClientesTitulo.Size = new System.Drawing.Size(260, 21);
            this.lblClientesTitulo.TabIndex = 0;
            this.lblClientesTitulo.Text = "👥 ETAPA 2: SELECIONAR CLIENTES";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTitulo.Location = new System.Drawing.Point(450, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(316, 37);
            this.lblTitulo.TabIndex = 3;
            this.lblTitulo.Text = "📮 EDITOR DE MALA DIRETA";
            // 
            // lblInstrucoes
            // 
            this.lblInstrucoes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
            this.lblInstrucoes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInstrucoes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblInstrucoes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.lblInstrucoes.Location = new System.Drawing.Point(30, 70);
            this.lblInstrucoes.Name = "lblInstrucoes";
            this.lblInstrucoes.Padding = new System.Windows.Forms.Padding(10);
            this.lblInstrucoes.Size = new System.Drawing.Size(400, 90);
            this.lblInstrucoes.TabIndex = 4;
            this.lblInstrucoes.Text = "📌 COMO USAR:\r\n1. Carregue a imagem do seu papel A4\r\n2. Clique nos botões azuis e" +
    " depois clique na imagem onde quer cada campo\r\n3. Busque e selecione os clientes" +
    "\r\n4. Clique em GERAR PDF";
            // 
            // panelPreview
            // 
            this.panelPreview.BackColor = System.Drawing.Color.White;
            this.panelPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPreview.Controls.Add(this.lblPreviewTitulo);
            this.panelPreview.Controls.Add(this.picPreview);
            this.panelPreview.Location = new System.Drawing.Point(30, 180);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(620, 580);
            this.panelPreview.TabIndex = 5;
            // 
            // lblPreviewTitulo
            // 
            this.lblPreviewTitulo.AutoSize = true;
            this.lblPreviewTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPreviewTitulo.Location = new System.Drawing.Point(10, 10);
            this.lblPreviewTitulo.Name = "lblPreviewTitulo";
            this.lblPreviewTitulo.Size = new System.Drawing.Size(216, 20);
            this.lblPreviewTitulo.TabIndex = 1;
            this.lblPreviewTitulo.Text = "📄 PREVIEW DA SUA FOLHA A4";
            // 
            // btnFechar
            // 
            this.btnFechar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(1220, 15);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(150, 40);
            this.btnFechar.TabIndex = 6;
            this.btnFechar.Text = "✖ FECHAR";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.BtnFechar_Click);
            // 
            // FormMailingEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1400, 800);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.panelPreview);
            this.Controls.Add(this.lblInstrucoes);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.panelClientes);
            this.Controls.Add(this.panelTemplate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormMailingEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editor de Mala Direta - Simples e Fácil";
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.panelTemplate.ResumeLayout(false);
            this.panelTemplate.PerformLayout();
            this.panelClientes.ResumeLayout(false);
            this.panelClientes.PerformLayout();
            this.panelPreview.ResumeLayout(false);
            this.panelPreview.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Panel panelTemplate;
        private System.Windows.Forms.Label lblTemplateTitulo;
        private System.Windows.Forms.Label lblStatusPosicoes;
        private System.Windows.Forms.Button btnLimparPosicoes;
        private System.Windows.Forms.Button btnSalvarTemplate;
        private System.Windows.Forms.Label lblInfoPosicoes;
        private System.Windows.Forms.Button btnDefinirCEP;
        private System.Windows.Forms.Button btnDefinirCidade;
        private System.Windows.Forms.Button btnDefinirEndereco;
        private System.Windows.Forms.Button btnDefinirNome;
        private System.Windows.Forms.TextBox txtNomeTemplate;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Button btnCarregarImagem;
        private System.Windows.Forms.Panel panelClientes;
        private System.Windows.Forms.Button btnGerarPDF;
        private System.Windows.Forms.Label lblTotalSelecionados;
        private System.Windows.Forms.Button btnMarcarTodos;
        private System.Windows.Forms.CheckedListBox chkClientes;
        private System.Windows.Forms.Button btnLimparBusca;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscaCPF;
        private System.Windows.Forms.Label lblBusca;
        private System.Windows.Forms.Label lblClientesTitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblInstrucoes;
        private System.Windows.Forms.Panel panelPreview;
        private System.Windows.Forms.Label lblPreviewTitulo;
        private System.Windows.Forms.Button btnFechar;
    }
}