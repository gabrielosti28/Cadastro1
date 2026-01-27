// =============================================
// FORMULARIO - EDITOR DE MALA DIRETA
// Arquivo: FormMailingEditor.Designer.cs
// PARTE VISUAL/INTERFACE
// =============================================
using System.Windows.Forms;

namespace Cadastro1
{
    partial class FormMailingEditor
    {
        private System.ComponentModel.IContainer components = null;
        private PictureBox picPreview;
        private TextBox txtNomeTemplate;
        private TextBox txtBuscaCPF;
        private CheckedListBox chkClientes;
        private Label lblInfoPosicoes;
        private Label lblTotalSelecionados;
        private Label lblStatusPosicoes;

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
            this.SuspendLayout();
            CriarInterface();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void CriarInterface()
        {
            this.Text = "Editor de Mala Direta";
            this.Size = new System.Drawing.Size(1400, 800);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // TITULO
            Label lblTitulo = new Label();
            lblTitulo.Text = "EDITOR DE MALA DIRETA";
            lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20, System.Drawing.FontStyle.Bold);
            lblTitulo.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new System.Drawing.Point(500, 20);
            this.Controls.Add(lblTitulo);

            // INSTRUCOES
            Label lblInstrucoes = new Label();
            lblInstrucoes.Text = "PASSO A PASSO:\n1. Carregue a imagem do papel A4\n2. Clique em POSICIONAR TUDO\n3. Clique 3 vezes na imagem (Endereco, Cidade, CEP)\n4. Selecione clientes e gere o PDF";
            lblInstrucoes.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
            lblInstrucoes.Location = new System.Drawing.Point(30, 80);
            lblInstrucoes.Size = new System.Drawing.Size(450, 80);
            lblInstrucoes.BackColor = System.Drawing.Color.FromArgb(255, 255, 200);
            lblInstrucoes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lblInstrucoes.Padding = new System.Windows.Forms.Padding(10);
            this.Controls.Add(lblInstrucoes);

            // PAINEL PREVIEW
            Panel panelPreview = new Panel();
            panelPreview.Location = new System.Drawing.Point(30, 180);
            panelPreview.Size = new System.Drawing.Size(620, 580);
            panelPreview.BackColor = System.Drawing.Color.White;
            panelPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(panelPreview);

            Label lblPreviewTitulo = new Label();
            lblPreviewTitulo.Text = "SUA FOLHA A4";
            lblPreviewTitulo.Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold);
            lblPreviewTitulo.Location = new System.Drawing.Point(10, 10);
            lblPreviewTitulo.AutoSize = true;
            panelPreview.Controls.Add(lblPreviewTitulo);

            picPreview = new PictureBox();
            picPreview.Location = new System.Drawing.Point(10, 40);
            picPreview.Size = new System.Drawing.Size(595, 530);
            picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            picPreview.BackColor = System.Drawing.Color.White;
            picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            picPreview.Cursor = System.Windows.Forms.Cursors.Default;
            panelPreview.Controls.Add(picPreview);

            // PAINEL TEMPLATE
            Panel panelTemplate = new Panel();
            panelTemplate.Location = new System.Drawing.Point(670, 80);
            panelTemplate.Size = new System.Drawing.Size(700, 280);
            panelTemplate.BackColor = System.Drawing.Color.White;
            panelTemplate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(panelTemplate);

            Label lblTituloTemplate = new Label();
            lblTituloTemplate.Text = "ETAPA 1: CONFIGURAR POSICOES";
            lblTituloTemplate.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
            lblTituloTemplate.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            lblTituloTemplate.Location = new System.Drawing.Point(15, 10);
            lblTituloTemplate.AutoSize = true;
            panelTemplate.Controls.Add(lblTituloTemplate);

            Label lblNomeTemplate = new Label();
            lblNomeTemplate.Text = "Nome do Template:";
            lblNomeTemplate.Location = new System.Drawing.Point(15, 45);
            lblNomeTemplate.AutoSize = true;
            lblNomeTemplate.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
            panelTemplate.Controls.Add(lblNomeTemplate);

            txtNomeTemplate = new TextBox();
            txtNomeTemplate.Location = new System.Drawing.Point(15, 65);
            txtNomeTemplate.Size = new System.Drawing.Size(300, 25);
            txtNomeTemplate.Font = new System.Drawing.Font("Segoe UI", 10);
            txtNomeTemplate.Text = "Template_" + System.DateTime.Now.ToString("yyyyMMdd");
            panelTemplate.Controls.Add(txtNomeTemplate);

            Button btnCarregar = new Button();
            btnCarregar.Text = "1. CARREGAR IMAGEM A4";
            btnCarregar.Location = new System.Drawing.Point(330, 45);
            btnCarregar.Size = new System.Drawing.Size(350, 45);
            btnCarregar.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            btnCarregar.ForeColor = System.Drawing.Color.White;
            btnCarregar.Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold);
            btnCarregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCarregar.FlatAppearance.BorderSize = 0;
            btnCarregar.Cursor = System.Windows.Forms.Cursors.Hand;
            panelTemplate.Controls.Add(btnCarregar);

            lblInfoPosicoes = new Label();
            lblInfoPosicoes.Text = "Depois de carregar a imagem, clique no botao verde:";
            lblInfoPosicoes.Location = new System.Drawing.Point(15, 105);
            lblInfoPosicoes.Size = new System.Drawing.Size(670, 20);
            lblInfoPosicoes.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
            lblInfoPosicoes.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            panelTemplate.Controls.Add(lblInfoPosicoes);

            Button btnPosicionar = new Button();
            btnPosicionar.Text = "2. POSICIONAR TUDO (3 cliques)";
            btnPosicionar.Location = new System.Drawing.Point(15, 130);
            btnPosicionar.Size = new System.Drawing.Size(665, 50);
            btnPosicionar.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            btnPosicionar.ForeColor = System.Drawing.Color.White;
            btnPosicionar.Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold);
            btnPosicionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnPosicionar.FlatAppearance.BorderSize = 0;
            btnPosicionar.Cursor = System.Windows.Forms.Cursors.Hand;
            panelTemplate.Controls.Add(btnPosicionar);

            Button btnLimpar = new Button();
            btnLimpar.Text = "Limpar Posicoes";
            btnLimpar.Location = new System.Drawing.Point(15, 195);
            btnLimpar.Size = new System.Drawing.Size(160, 35);
            btnLimpar.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            btnLimpar.ForeColor = System.Drawing.Color.White;
            btnLimpar.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
            btnLimpar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLimpar.FlatAppearance.BorderSize = 0;
            btnLimpar.Cursor = System.Windows.Forms.Cursors.Hand;
            panelTemplate.Controls.Add(btnLimpar);

            Button btnSalvar = new Button();
            btnSalvar.Text = "Salvar Template";
            btnSalvar.Location = new System.Drawing.Point(190, 195);
            btnSalvar.Size = new System.Drawing.Size(240, 35);
            btnSalvar.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            btnSalvar.ForeColor = System.Drawing.Color.White;
            btnSalvar.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSalvar.FlatAppearance.BorderSize = 0;
            btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            panelTemplate.Controls.Add(btnSalvar);

            lblStatusPosicoes = new Label();
            lblStatusPosicoes.Text = "Endereco: Nao definido\nCidade: Nao definido\nCEP: Nao definido";
            lblStatusPosicoes.Location = new System.Drawing.Point(15, 240);
            lblStatusPosicoes.Size = new System.Drawing.Size(670, 35);
            lblStatusPosicoes.Font = new System.Drawing.Font("Consolas", 9);
            panelTemplate.Controls.Add(lblStatusPosicoes);

            // PAINEL CLIENTES
            Panel panelClientes = new Panel();
            panelClientes.Location = new System.Drawing.Point(670, 380);
            panelClientes.Size = new System.Drawing.Size(700, 380);
            panelClientes.BackColor = System.Drawing.Color.White;
            panelClientes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(panelClientes);

            Label lblTituloClientes = new Label();
            lblTituloClientes.Text = "ETAPA 2: SELECIONAR CLIENTES";
            lblTituloClientes.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
            lblTituloClientes.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            lblTituloClientes.Location = new System.Drawing.Point(15, 10);
            lblTituloClientes.AutoSize = true;
            panelClientes.Controls.Add(lblTituloClientes);

            Label lblBusca = new Label();
            lblBusca.Text = "Buscar por CPF ou Nome:";
            lblBusca.Location = new System.Drawing.Point(15, 45);
            lblBusca.AutoSize = true;
            lblBusca.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
            panelClientes.Controls.Add(lblBusca);

            txtBuscaCPF = new TextBox();
            txtBuscaCPF.Location = new System.Drawing.Point(15, 65);
            txtBuscaCPF.Size = new System.Drawing.Size(500, 25);
            txtBuscaCPF.Font = new System.Drawing.Font("Segoe UI", 11);
            panelClientes.Controls.Add(txtBuscaCPF);

            Button btnLimparBusca = new Button();
            btnLimparBusca.Text = "Limpar";
            btnLimparBusca.Location = new System.Drawing.Point(525, 63);
            btnLimparBusca.Size = new System.Drawing.Size(80, 30);
            btnLimparBusca.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            btnLimparBusca.ForeColor = System.Drawing.Color.White;
            btnLimparBusca.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
            btnLimparBusca.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLimparBusca.FlatAppearance.BorderSize = 0;
            btnLimparBusca.Cursor = System.Windows.Forms.Cursors.Hand;
            panelClientes.Controls.Add(btnLimparBusca);

            chkClientes = new CheckedListBox();
            chkClientes.Location = new System.Drawing.Point(15, 105);
            chkClientes.Size = new System.Drawing.Size(670, 190);
            chkClientes.Font = new System.Drawing.Font("Consolas", 9);
            chkClientes.CheckOnClick = true;
            panelClientes.Controls.Add(chkClientes);

            Button btnMarcar = new Button();
            btnMarcar.Text = "Marcar Todos";
            btnMarcar.Location = new System.Drawing.Point(15, 305);
            btnMarcar.Size = new System.Drawing.Size(150, 30);
            btnMarcar.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            btnMarcar.ForeColor = System.Drawing.Color.White;
            btnMarcar.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
            btnMarcar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnMarcar.FlatAppearance.BorderSize = 0;
            btnMarcar.Cursor = System.Windows.Forms.Cursors.Hand;
            panelClientes.Controls.Add(btnMarcar);

            lblTotalSelecionados = new Label();
            lblTotalSelecionados.Text = "0 clientes selecionados";
            lblTotalSelecionados.Location = new System.Drawing.Point(180, 305);
            lblTotalSelecionados.Size = new System.Drawing.Size(200, 30);
            lblTotalSelecionados.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            lblTotalSelecionados.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            lblTotalSelecionados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            panelClientes.Controls.Add(lblTotalSelecionados);

            Button btnGerar = new Button();
            btnGerar.Text = "GERAR PDF";
            btnGerar.Location = new System.Drawing.Point(390, 300);
            btnGerar.Size = new System.Drawing.Size(295, 40);
            btnGerar.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            btnGerar.ForeColor = System.Drawing.Color.White;
            btnGerar.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
            btnGerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnGerar.FlatAppearance.BorderSize = 0;
            btnGerar.Cursor = System.Windows.Forms.Cursors.Hand;
            panelClientes.Controls.Add(btnGerar);

            // BOTAO FECHAR
            Button btnFechar = new Button();
            btnFechar.Text = "FECHAR";
            btnFechar.Location = new System.Drawing.Point(1220, 20);
            btnFechar.Size = new System.Drawing.Size(150, 40);
            btnFechar.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            btnFechar.ForeColor = System.Drawing.Color.White;
            btnFechar.Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold);
            btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnFechar.FlatAppearance.BorderSize = 0;
            btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Controls.Add(btnFechar);
        }
    }
}