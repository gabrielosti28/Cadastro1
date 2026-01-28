// =============================================
// FORMULARIO - EDITOR DE MALA DIRETA
// Arquivo: FormMailingEditor.Designer.cs
// VERSÃO CORRIGIDA E FUNCIONAL
// =============================================
using System.Windows.Forms;
using System.Drawing;

namespace Cadastro1
{
    partial class FormMailingEditor
    {
        private System.ComponentModel.IContainer components = null;

        // Controles principais
        private Panel panelContainer;
        private Panel panelPreview;
        private Panel panelTemplate;
        private Panel panelClientes;
        private PictureBox picPreview;

        // Controles de template
        private Label lblTitulo;
        private Label lblInstrucoes;
        private Label lblPreviewTitulo;
        private Label lblTituloTemplate;
        private Label lblNomeTemplate;
        private TextBox txtNomeTemplate;
        private Button btnCarregar;
        private Label lblInfoPosicoes;
        private Button btnPosicionar;
        private Button btnLimpar;
        private Button btnSalvar;
        private Label lblStatusPosicoes;

        // Controles de clientes
        private Label lblTituloClientes;
        private Label lblBusca;
        private TextBox txtBuscaCPF;
        private Button btnLimparBusca;
        private CheckedListBox chkClientes;
        private Button btnMarcar;
        private Label lblTotalSelecionados;
        private Button btnGerar;

        // Controles de importação
        private Button btnImportarPlanilha;
        private Panel panelStatusImportacao;
        private Label lblStatusImportacao;

        // Botão fechar
        private Button btnFechar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelContainer = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblInstrucoes = new System.Windows.Forms.Label();
            this.panelPreview = new System.Windows.Forms.Panel();
            this.lblPreviewTitulo = new System.Windows.Forms.Label();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.panelTemplate = new System.Windows.Forms.Panel();
            this.lblTituloTemplate = new System.Windows.Forms.Label();
            this.lblNomeTemplate = new System.Windows.Forms.Label();
            this.txtNomeTemplate = new System.Windows.Forms.TextBox();
            this.btnCarregar = new System.Windows.Forms.Button();
            this.lblInfoPosicoes = new System.Windows.Forms.Label();
            this.btnPosicionar = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.lblStatusPosicoes = new System.Windows.Forms.Label();
            this.panelClientes = new System.Windows.Forms.Panel();
            this.lblTituloClientes = new System.Windows.Forms.Label();
            this.lblBusca = new System.Windows.Forms.Label();
            this.txtBuscaCPF = new System.Windows.Forms.TextBox();
            this.btnLimparBusca = new System.Windows.Forms.Button();
            this.chkClientes = new System.Windows.Forms.CheckedListBox();
            this.btnMarcar = new System.Windows.Forms.Button();
            this.lblTotalSelecionados = new System.Windows.Forms.Label();
            this.btnGerar = new System.Windows.Forms.Button();
            this.btnImportarPlanilha = new System.Windows.Forms.Button();
            this.panelStatusImportacao = new System.Windows.Forms.Panel();
            this.lblStatusImportacao = new System.Windows.Forms.Label();
            this.btnFechar = new System.Windows.Forms.Button();
            this.panelContainer.SuspendLayout();
            this.panelPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.panelTemplate.SuspendLayout();
            this.panelClientes.SuspendLayout();
            this.panelStatusImportacao.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.lblTitulo);
            this.panelContainer.Controls.Add(this.lblInstrucoes);
            this.panelContainer.Controls.Add(this.panelPreview);
            this.panelContainer.Controls.Add(this.panelTemplate);
            this.panelContainer.Controls.Add(this.panelClientes);
            this.panelContainer.Controls.Add(this.btnFechar);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1400, 974);
            this.panelContainer.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTitulo.Location = new System.Drawing.Point(500, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(382, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "📮 EDITOR DE MALA DIRETA";
            // 
            // lblInstrucoes
            // 
            this.lblInstrucoes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
            this.lblInstrucoes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInstrucoes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblInstrucoes.Location = new System.Drawing.Point(30, 80);
            this.lblInstrucoes.Name = "lblInstrucoes";
            this.lblInstrucoes.Padding = new System.Windows.Forms.Padding(10);
            this.lblInstrucoes.Size = new System.Drawing.Size(450, 80);
            this.lblInstrucoes.TabIndex = 1;
            this.lblInstrucoes.Text = "PASSO A PASSO:\n1. Carregue a imagem do papel A4\n2. Clique em POSICIONAR TUDO\n3. C" +
    "lique 3 vezes na imagem (Endereco, Cidade, CEP)\n4. Selecione clientes e gere o P" +
    "DF";
            // 
            // panelPreview
            // 
            this.panelPreview.BackColor = System.Drawing.Color.White;
            this.panelPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPreview.Controls.Add(this.lblPreviewTitulo);
            this.panelPreview.Controls.Add(this.picPreview);
            this.panelPreview.Location = new System.Drawing.Point(30, 180);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(620, 647);
            this.panelPreview.TabIndex = 2;
            // 
            // lblPreviewTitulo
            // 
            this.lblPreviewTitulo.AutoSize = true;
            this.lblPreviewTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPreviewTitulo.Location = new System.Drawing.Point(10, 10);
            this.lblPreviewTitulo.Name = "lblPreviewTitulo";
            this.lblPreviewTitulo.Size = new System.Drawing.Size(116, 20);
            this.lblPreviewTitulo.TabIndex = 0;
            this.lblPreviewTitulo.Text = "SUA FOLHA A4";
            // 
            // picPreview
            // 
            this.picPreview.BackColor = System.Drawing.Color.White;
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Cursor = System.Windows.Forms.Cursors.Default;
            this.picPreview.Location = new System.Drawing.Point(10, 40);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(595, 591);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 1;
            this.picPreview.TabStop = false;
            this.picPreview.Click += new System.EventHandler(this.PicPreview_Click);
            this.picPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.PicPreview_Paint);
            // 
            // panelTemplate
            // 
            this.panelTemplate.BackColor = System.Drawing.Color.White;
            this.panelTemplate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTemplate.Controls.Add(this.lblTituloTemplate);
            this.panelTemplate.Controls.Add(this.lblNomeTemplate);
            this.panelTemplate.Controls.Add(this.txtNomeTemplate);
            this.panelTemplate.Controls.Add(this.btnCarregar);
            this.panelTemplate.Controls.Add(this.lblInfoPosicoes);
            this.panelTemplate.Controls.Add(this.btnPosicionar);
            this.panelTemplate.Controls.Add(this.btnLimpar);
            this.panelTemplate.Controls.Add(this.btnSalvar);
            this.panelTemplate.Controls.Add(this.lblStatusPosicoes);
            this.panelTemplate.Location = new System.Drawing.Point(670, 80);
            this.panelTemplate.Name = "panelTemplate";
            this.panelTemplate.Size = new System.Drawing.Size(700, 280);
            this.panelTemplate.TabIndex = 3;
            // 
            // lblTituloTemplate
            // 
            this.lblTituloTemplate.AutoSize = true;
            this.lblTituloTemplate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTituloTemplate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTituloTemplate.Location = new System.Drawing.Point(15, 10);
            this.lblTituloTemplate.Name = "lblTituloTemplate";
            this.lblTituloTemplate.Size = new System.Drawing.Size(261, 21);
            this.lblTituloTemplate.TabIndex = 0;
            this.lblTituloTemplate.Text = "ETAPA 1: CONFIGURAR POSIÇÕES";
            // 
            // lblNomeTemplate
            // 
            this.lblNomeTemplate.AutoSize = true;
            this.lblNomeTemplate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNomeTemplate.Location = new System.Drawing.Point(15, 45);
            this.lblNomeTemplate.Name = "lblNomeTemplate";
            this.lblNomeTemplate.Size = new System.Drawing.Size(116, 15);
            this.lblNomeTemplate.TabIndex = 1;
            this.lblNomeTemplate.Text = "Nome do Template:";
            // 
            // txtNomeTemplate
            // 
            this.txtNomeTemplate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNomeTemplate.Location = new System.Drawing.Point(15, 65);
            this.txtNomeTemplate.Name = "txtNomeTemplate";
            this.txtNomeTemplate.Size = new System.Drawing.Size(300, 25);
            this.txtNomeTemplate.TabIndex = 2;
            this.txtNomeTemplate.Text = "Template_20260128";
            // 
            // btnCarregar
            // 
            this.btnCarregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnCarregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCarregar.FlatAppearance.BorderSize = 0;
            this.btnCarregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCarregar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCarregar.ForeColor = System.Drawing.Color.White;
            this.btnCarregar.Location = new System.Drawing.Point(330, 45);
            this.btnCarregar.Name = "btnCarregar";
            this.btnCarregar.Size = new System.Drawing.Size(350, 45);
            this.btnCarregar.TabIndex = 3;
            this.btnCarregar.Text = "1. CARREGAR IMAGEM A4";
            this.btnCarregar.UseVisualStyleBackColor = false;
            this.btnCarregar.Click += new System.EventHandler(this.BtnCarregar_Click);
            // 
            // lblInfoPosicoes
            // 
            this.lblInfoPosicoes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblInfoPosicoes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblInfoPosicoes.Location = new System.Drawing.Point(15, 105);
            this.lblInfoPosicoes.Name = "lblInfoPosicoes";
            this.lblInfoPosicoes.Size = new System.Drawing.Size(670, 20);
            this.lblInfoPosicoes.TabIndex = 4;
            this.lblInfoPosicoes.Text = "Depois de carregar a imagem, clique no botão verde:";
            // 
            // btnPosicionar
            // 
            this.btnPosicionar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnPosicionar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPosicionar.FlatAppearance.BorderSize = 0;
            this.btnPosicionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPosicionar.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnPosicionar.ForeColor = System.Drawing.Color.White;
            this.btnPosicionar.Location = new System.Drawing.Point(15, 130);
            this.btnPosicionar.Name = "btnPosicionar";
            this.btnPosicionar.Size = new System.Drawing.Size(665, 50);
            this.btnPosicionar.TabIndex = 5;
            this.btnPosicionar.Text = "2. POSICIONAR TUDO (3 cliques)";
            this.btnPosicionar.UseVisualStyleBackColor = false;
            this.btnPosicionar.Click += new System.EventHandler(this.BtnPosicionar_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnLimpar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpar.FlatAppearance.BorderSize = 0;
            this.btnLimpar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLimpar.ForeColor = System.Drawing.Color.White;
            this.btnLimpar.Location = new System.Drawing.Point(15, 195);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(160, 35);
            this.btnLimpar.TabIndex = 6;
            this.btnLimpar.Text = "Limpar Posições";
            this.btnLimpar.UseVisualStyleBackColor = false;
            this.btnLimpar.Click += new System.EventHandler(this.BtnLimpar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(190, 195);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(240, 35);
            this.btnSalvar.TabIndex = 7;
            this.btnSalvar.Text = "Salvar Template";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.BtnSalvar_Click);
            // 
            // lblStatusPosicoes
            // 
            this.lblStatusPosicoes.Font = new System.Drawing.Font("Consolas", 9F);
            this.lblStatusPosicoes.Location = new System.Drawing.Point(21, 233);
            this.lblStatusPosicoes.Name = "lblStatusPosicoes";
            this.lblStatusPosicoes.Size = new System.Drawing.Size(670, 51);
            this.lblStatusPosicoes.TabIndex = 8;
            this.lblStatusPosicoes.Text = "Endereço: Não definido\nCidade: Não definido\nCEP: Não definido";
            // 
            // panelClientes
            // 
            this.panelClientes.BackColor = System.Drawing.Color.White;
            this.panelClientes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelClientes.Controls.Add(this.lblTituloClientes);
            this.panelClientes.Controls.Add(this.lblBusca);
            this.panelClientes.Controls.Add(this.txtBuscaCPF);
            this.panelClientes.Controls.Add(this.btnLimparBusca);
            this.panelClientes.Controls.Add(this.chkClientes);
            this.panelClientes.Controls.Add(this.btnMarcar);
            this.panelClientes.Controls.Add(this.lblTotalSelecionados);
            this.panelClientes.Controls.Add(this.btnGerar);
            this.panelClientes.Controls.Add(this.btnImportarPlanilha);
            this.panelClientes.Controls.Add(this.panelStatusImportacao);
            this.panelClientes.Location = new System.Drawing.Point(670, 380);
            this.panelClientes.Name = "panelClientes";
            this.panelClientes.Size = new System.Drawing.Size(700, 538);
            this.panelClientes.TabIndex = 4;
            // 
            // lblTituloClientes
            // 
            this.lblTituloClientes.AutoSize = true;
            this.lblTituloClientes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTituloClientes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTituloClientes.Location = new System.Drawing.Point(15, 10);
            this.lblTituloClientes.Name = "lblTituloClientes";
            this.lblTituloClientes.Size = new System.Drawing.Size(251, 21);
            this.lblTituloClientes.TabIndex = 0;
            this.lblTituloClientes.Text = "ETAPA 2: SELECIONAR CLIENTES";
            // 
            // lblBusca
            // 
            this.lblBusca.AutoSize = true;
            this.lblBusca.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBusca.Location = new System.Drawing.Point(15, 45);
            this.lblBusca.Name = "lblBusca";
            this.lblBusca.Size = new System.Drawing.Size(146, 15);
            this.lblBusca.TabIndex = 1;
            this.lblBusca.Text = "Buscar por CPF ou Nome:";
            // 
            // txtBuscaCPF
            // 
            this.txtBuscaCPF.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtBuscaCPF.Location = new System.Drawing.Point(15, 65);
            this.txtBuscaCPF.Name = "txtBuscaCPF";
            this.txtBuscaCPF.Size = new System.Drawing.Size(500, 27);
            this.txtBuscaCPF.TabIndex = 2;
            this.txtBuscaCPF.TextChanged += new System.EventHandler(this.TxtBusca_TextChanged);
            // 
            // btnLimparBusca
            // 
            this.btnLimparBusca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnLimparBusca.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimparBusca.FlatAppearance.BorderSize = 0;
            this.btnLimparBusca.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimparBusca.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLimparBusca.ForeColor = System.Drawing.Color.White;
            this.btnLimparBusca.Location = new System.Drawing.Point(525, 63);
            this.btnLimparBusca.Name = "btnLimparBusca";
            this.btnLimparBusca.Size = new System.Drawing.Size(80, 30);
            this.btnLimparBusca.TabIndex = 3;
            this.btnLimparBusca.Text = "Limpar";
            this.btnLimparBusca.UseVisualStyleBackColor = false;
            this.btnLimparBusca.Click += new System.EventHandler(this.BtnLimparBusca_Click);
            // 
            // chkClientes
            // 
            this.chkClientes.CheckOnClick = true;
            this.chkClientes.Font = new System.Drawing.Font("Consolas", 9F);
            this.chkClientes.Location = new System.Drawing.Point(15, 105);
            this.chkClientes.Name = "chkClientes";
            this.chkClientes.Size = new System.Drawing.Size(670, 174);
            this.chkClientes.TabIndex = 4;
            this.chkClientes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ChkClientes_ItemCheck);
            // 
            // btnMarcar
            // 
            this.btnMarcar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnMarcar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMarcar.FlatAppearance.BorderSize = 0;
            this.btnMarcar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarcar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnMarcar.ForeColor = System.Drawing.Color.White;
            this.btnMarcar.Location = new System.Drawing.Point(15, 305);
            this.btnMarcar.Name = "btnMarcar";
            this.btnMarcar.Size = new System.Drawing.Size(150, 30);
            this.btnMarcar.TabIndex = 5;
            this.btnMarcar.Text = "Marcar Todos";
            this.btnMarcar.UseVisualStyleBackColor = false;
            this.btnMarcar.Click += new System.EventHandler(this.BtnMarcar_Click);
            // 
            // lblTotalSelecionados
            // 
            this.lblTotalSelecionados.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalSelecionados.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTotalSelecionados.Location = new System.Drawing.Point(180, 305);
            this.lblTotalSelecionados.Name = "lblTotalSelecionados";
            this.lblTotalSelecionados.Size = new System.Drawing.Size(200, 30);
            this.lblTotalSelecionados.TabIndex = 6;
            this.lblTotalSelecionados.Text = "0 clientes selecionados";
            this.lblTotalSelecionados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnGerar
            // 
            this.btnGerar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnGerar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGerar.FlatAppearance.BorderSize = 0;
            this.btnGerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGerar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnGerar.ForeColor = System.Drawing.Color.White;
            this.btnGerar.Location = new System.Drawing.Point(390, 300);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(295, 40);
            this.btnGerar.TabIndex = 7;
            this.btnGerar.Text = "GERAR PDF";
            this.btnGerar.UseVisualStyleBackColor = false;
            this.btnGerar.Click += new System.EventHandler(this.BtnGerar_Click);
            // 
            // btnImportarPlanilha
            // 
            this.btnImportarPlanilha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnImportarPlanilha.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportarPlanilha.FlatAppearance.BorderSize = 0;
            this.btnImportarPlanilha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportarPlanilha.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnImportarPlanilha.ForeColor = System.Drawing.Color.White;
            this.btnImportarPlanilha.Location = new System.Drawing.Point(15, 350);
            this.btnImportarPlanilha.Name = "btnImportarPlanilha";
            this.btnImportarPlanilha.Size = new System.Drawing.Size(670, 40);
            this.btnImportarPlanilha.TabIndex = 8;
            this.btnImportarPlanilha.Text = "📊 IMPORTAR CPFs DA PLANILHA (Excel/CSV)";
            this.btnImportarPlanilha.UseVisualStyleBackColor = false;
            this.btnImportarPlanilha.Click += new System.EventHandler(this.BtnImportarPlanilha_Click);
            // 
            // panelStatusImportacao
            // 
            this.panelStatusImportacao.AutoScroll = true;
            this.panelStatusImportacao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelStatusImportacao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStatusImportacao.Controls.Add(this.lblStatusImportacao);
            this.panelStatusImportacao.Location = new System.Drawing.Point(15, 400);
            this.panelStatusImportacao.Name = "panelStatusImportacao";
            this.panelStatusImportacao.Size = new System.Drawing.Size(670, 113);
            this.panelStatusImportacao.TabIndex = 9;
            this.panelStatusImportacao.Visible = false;
            // 
            // lblStatusImportacao
            // 
            this.lblStatusImportacao.AutoSize = true;
            this.lblStatusImportacao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatusImportacao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblStatusImportacao.Location = new System.Drawing.Point(5, 5);
            this.lblStatusImportacao.Name = "lblStatusImportacao";
            this.lblStatusImportacao.Size = new System.Drawing.Size(146, 15);
            this.lblStatusImportacao.TabIndex = 0;
            this.lblStatusImportacao.Text = "Aguardando importação...";
            // 
            // btnFechar
            // 
            this.btnFechar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(1220, 20);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(150, 40);
            this.btnFechar.TabIndex = 5;
            this.btnFechar.Text = "FECHAR";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.BtnFechar_Click);
            // 
            // FormMailingEditor
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1400, 974);
            this.Controls.Add(this.panelContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormMailingEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editor de Mala Direta";
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            this.panelPreview.ResumeLayout(false);
            this.panelPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.panelTemplate.ResumeLayout(false);
            this.panelTemplate.PerformLayout();
            this.panelClientes.ResumeLayout(false);
            this.panelClientes.PerformLayout();
            this.panelStatusImportacao.ResumeLayout(false);
            this.panelStatusImportacao.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}