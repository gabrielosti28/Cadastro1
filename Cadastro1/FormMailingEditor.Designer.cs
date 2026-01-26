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
            this.panelControles = new System.Windows.Forms.Panel();
            this.txtNomeTemplate = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.btnCarregarImagem = new System.Windows.Forms.Button();
            this.btnSalvarTemplate = new System.Windows.Forms.Button();
            this.lblClientes = new System.Windows.Forms.Label();
            this.chkClientes = new System.Windows.Forms.CheckedListBox();
            this.btnSelecionarClientes = new System.Windows.Forms.Button();
            this.btnGerarPDF = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnFechar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.panelControles.SuspendLayout();
            this.SuspendLayout();
            // 
            // picPreview
            // 
            this.picPreview.BackColor = System.Drawing.Color.White;
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Location = new System.Drawing.Point(50, 160);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(595, 842);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 0;
            this.picPreview.TabStop = false;
            this.picPreview.Click += new System.EventHandler(this.PicPreview_Click);
            // 
            // panelControles
            // 
            this.panelControles.BackColor = System.Drawing.Color.White;
            this.panelControles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelControles.Controls.Add(this.txtNomeTemplate);
            this.panelControles.Controls.Add(this.lblNome);
            this.panelControles.Controls.Add(this.btnCarregarImagem);
            this.panelControles.Controls.Add(this.btnSalvarTemplate);
            this.panelControles.Controls.Add(this.lblClientes);
            this.panelControles.Controls.Add(this.chkClientes);
            this.panelControles.Controls.Add(this.btnSelecionarClientes);
            this.panelControles.Controls.Add(this.btnGerarPDF);
            this.panelControles.Location = new System.Drawing.Point(670, 160);
            this.panelControles.Name = "panelControles";
            this.panelControles.Size = new System.Drawing.Size(500, 480);
            this.panelControles.TabIndex = 1;
            // 
            // txtNomeTemplate
            // 
            this.txtNomeTemplate.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNomeTemplate.Location = new System.Drawing.Point(20, 45);
            this.txtNomeTemplate.Name = "txtNomeTemplate";
            this.txtNomeTemplate.Size = new System.Drawing.Size(460, 27);
            this.txtNomeTemplate.TabIndex = 0;
            this.txtNomeTemplate.Text = "Meu Template";
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNome.Location = new System.Drawing.Point(20, 20);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(142, 19);
            this.lblNome.TabIndex = 1;
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
            this.btnCarregarImagem.Location = new System.Drawing.Point(20, 85);
            this.btnCarregarImagem.Name = "btnCarregarImagem";
            this.btnCarregarImagem.Size = new System.Drawing.Size(220, 40);
            this.btnCarregarImagem.TabIndex = 1;
            this.btnCarregarImagem.Text = "📄 Carregar Imagem A4";
            this.btnCarregarImagem.UseVisualStyleBackColor = false;
            this.btnCarregarImagem.Click += new System.EventHandler(this.BtnCarregarImagem_Click);
            // 
            // btnSalvarTemplate
            // 
            this.btnSalvarTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSalvarTemplate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvarTemplate.FlatAppearance.BorderSize = 0;
            this.btnSalvarTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvarTemplate.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSalvarTemplate.ForeColor = System.Drawing.Color.White;
            this.btnSalvarTemplate.Location = new System.Drawing.Point(260, 85);
            this.btnSalvarTemplate.Name = "btnSalvarTemplate";
            this.btnSalvarTemplate.Size = new System.Drawing.Size(220, 40);
            this.btnSalvarTemplate.TabIndex = 2;
            this.btnSalvarTemplate.Text = "💾 Salvar Template";
            this.btnSalvarTemplate.UseVisualStyleBackColor = false;
            this.btnSalvarTemplate.Click += new System.EventHandler(this.BtnSalvarTemplate_Click);
            // 
            // lblClientes
            // 
            this.lblClientes.AutoSize = true;
            this.lblClientes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblClientes.Location = new System.Drawing.Point(20, 140);
            this.lblClientes.Name = "lblClientes";
            this.lblClientes.Size = new System.Drawing.Size(152, 19);
            this.lblClientes.TabIndex = 3;
            this.lblClientes.Text = "Selecione os Clientes:";
            // 
            // chkClientes
            // 
            this.chkClientes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkClientes.FormattingEnabled = true;
            this.chkClientes.Location = new System.Drawing.Point(20, 165);
            this.chkClientes.Name = "chkClientes";
            this.chkClientes.Size = new System.Drawing.Size(460, 184);
            this.chkClientes.TabIndex = 3;
            // 
            // btnSelecionarClientes
            // 
            this.btnSelecionarClientes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnSelecionarClientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelecionarClientes.FlatAppearance.BorderSize = 0;
            this.btnSelecionarClientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecionarClientes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSelecionarClientes.ForeColor = System.Drawing.Color.White;
            this.btnSelecionarClientes.Location = new System.Drawing.Point(20, 375);
            this.btnSelecionarClientes.Name = "btnSelecionarClientes";
            this.btnSelecionarClientes.Size = new System.Drawing.Size(150, 35);
            this.btnSelecionarClientes.TabIndex = 4;
            this.btnSelecionarClientes.Text = "✓ Marcar Todos";
            this.btnSelecionarClientes.UseVisualStyleBackColor = false;
            this.btnSelecionarClientes.Click += new System.EventHandler(this.BtnSelecionarClientes_Click);
            // 
            // btnGerarPDF
            // 
            this.btnGerarPDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnGerarPDF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGerarPDF.FlatAppearance.BorderSize = 0;
            this.btnGerarPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGerarPDF.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnGerarPDF.ForeColor = System.Drawing.Color.White;
            this.btnGerarPDF.Location = new System.Drawing.Point(20, 420);
            this.btnGerarPDF.Name = "btnGerarPDF";
            this.btnGerarPDF.Size = new System.Drawing.Size(460, 45);
            this.btnGerarPDF.TabIndex = 5;
            this.btnGerarPDF.Text = "📄 GERAR PDF";
            this.btnGerarPDF.UseVisualStyleBackColor = false;
            this.btnGerarPDF.Click += new System.EventHandler(this.BtnGerarPDF_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTitulo.Location = new System.Drawing.Point(400, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(344, 32);
            this.lblTitulo.TabIndex = 2;
            this.lblTitulo.Text = "📮 EDITOR DE MALA DIRETA";
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.lblInfo.Location = new System.Drawing.Point(50, 70);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(500, 80);
            this.lblInfo.TabIndex = 3;
            this.lblInfo.Text = "1. Carregue uma imagem A4 do seu papel\r\n2. Clique onde quer colocar cada campo\r\n3" +
    ". Selecione os clientes\r\n4. Gere o PDF para impressão";
            // 
            // btnFechar
            // 
            this.btnFechar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(670, 871);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(150, 40);
            this.btnFechar.TabIndex = 4;
            this.btnFechar.Text = "✖ Fechar";
            this.btnFechar.UseVisualStyleBackColor = false;
            // 
            // FormMailingEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1200, 1061);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.panelControles);
            this.Controls.Add(this.picPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormMailingEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editor de Mala Direta";
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.panelControles.ResumeLayout(false);
            this.panelControles.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Panel panelControles;
        private System.Windows.Forms.TextBox txtNomeTemplate;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Button btnCarregarImagem;
        private System.Windows.Forms.Button btnSalvarTemplate;
        private System.Windows.Forms.Label lblClientes;
        private System.Windows.Forms.CheckedListBox chkClientes;
        private System.Windows.Forms.Button btnSelecionarClientes;
        private System.Windows.Forms.Button btnGerarPDF;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnFechar;
    }
}