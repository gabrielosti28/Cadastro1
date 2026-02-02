// =============================================
// DESIGNER - FORMULÁRIO IMPORTAÇÃO LOTE
// Arquivo: FormImportarClientesLote.Designer.cs
// =============================================
namespace Cadastro1
{
    partial class FormImportarClientesLote
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
            this.panelContainer = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCaminhoArquivo = new System.Windows.Forms.TextBox();
            this.btnSelecionarArquivo = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnIniciarImportacao = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtResumo = new System.Windows.Forms.TextBox();
            this.dgvResultados = new System.Windows.Forms.DataGridView();
            this.btnVerRelatorio = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.panelContainer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelContainer.Controls.Add(this.lblTitulo);
            this.panelContainer.Controls.Add(this.groupBox1);
            this.panelContainer.Controls.Add(this.lblStatus);
            this.panelContainer.Controls.Add(this.progressBar);
            this.panelContainer.Controls.Add(this.btnIniciarImportacao);
            this.panelContainer.Controls.Add(this.groupBox2);
            this.panelContainer.Controls.Add(this.btnVerRelatorio);
            this.panelContainer.Controls.Add(this.btnFechar);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1100, 700);
            this.panelContainer.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTitulo.Location = new System.Drawing.Point(250, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(600, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "📥 IMPORTAÇÃO EM LOTE DE CLIENTES";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCaminhoArquivo);
            this.groupBox1.Controls.Add(this.btnSelecionarArquivo);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(30, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1040, 80);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1. Selecione o arquivo";
            // 
            // txtCaminhoArquivo
            // 
            this.txtCaminhoArquivo.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtCaminhoArquivo.Location = new System.Drawing.Point(20, 35);
            this.txtCaminhoArquivo.Name = "txtCaminhoArquivo";
            this.txtCaminhoArquivo.ReadOnly = true;
            this.txtCaminhoArquivo.Size = new System.Drawing.Size(800, 27);
            this.txtCaminhoArquivo.TabIndex = 0;
            // 
            // btnSelecionarArquivo
            // 
            this.btnSelecionarArquivo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnSelecionarArquivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelecionarArquivo.FlatAppearance.BorderSize = 0;
            this.btnSelecionarArquivo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecionarArquivo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSelecionarArquivo.ForeColor = System.Drawing.Color.White;
            this.btnSelecionarArquivo.Location = new System.Drawing.Point(840, 30);
            this.btnSelecionarArquivo.Name = "btnSelecionarArquivo";
            this.btnSelecionarArquivo.Size = new System.Drawing.Size(180, 40);
            this.btnSelecionarArquivo.TabIndex = 1;
            this.btnSelecionarArquivo.Text = "📁 SELECIONAR";
            this.btnSelecionarArquivo.UseVisualStyleBackColor = false;
            this.btnSelecionarArquivo.Click += new System.EventHandler(this.btnSelecionarArquivo_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(30, 235);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(300, 20);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Aguardando seleção de arquivo...";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(30, 265);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(840, 25);
            this.progressBar.TabIndex = 4;
            this.progressBar.Visible = false;
            // 
            // btnIniciarImportacao
            // 
            this.btnIniciarImportacao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnIniciarImportacao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIniciarImportacao.Enabled = false;
            this.btnIniciarImportacao.FlatAppearance.BorderSize = 0;
            this.btnIniciarImportacao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIniciarImportacao.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnIniciarImportacao.ForeColor = System.Drawing.Color.White;
            this.btnIniciarImportacao.Location = new System.Drawing.Point(350, 175);
            this.btnIniciarImportacao.Name = "btnIniciarImportacao";
            this.btnIniciarImportacao.Size = new System.Drawing.Size(400, 50);
            this.btnIniciarImportacao.TabIndex = 2;
            this.btnIniciarImportacao.Text = "🚀 INICIAR IMPORTAÇÃO";
            this.btnIniciarImportacao.UseVisualStyleBackColor = false;
            this.btnIniciarImportacao.Click += new System.EventHandler(this.btnIniciarImportacao_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtResumo);
            this.groupBox2.Controls.Add(this.dgvResultados);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(30, 305);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1040, 320);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Resultados";
            // 
            // txtResumo
            // 
            this.txtResumo.BackColor = System.Drawing.Color.White;
            this.txtResumo.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.txtResumo.Location = new System.Drawing.Point(800, 30);
            this.txtResumo.Multiline = true;
            this.txtResumo.Name = "txtResumo";
            this.txtResumo.ReadOnly = true;
            this.txtResumo.Size = new System.Drawing.Size(220, 270);
            this.txtResumo.TabIndex = 1;
            // 
            // dgvResultados
            // 
            this.dgvResultados.AllowUserToAddRows = false;
            this.dgvResultados.AllowUserToDeleteRows = false;
            this.dgvResultados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResultados.BackgroundColor = System.Drawing.Color.White;
            this.dgvResultados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultados.Location = new System.Drawing.Point(20, 30);
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.ReadOnly = true;
            this.dgvResultados.RowHeadersVisible = false;
            this.dgvResultados.Size = new System.Drawing.Size(760, 270);
            this.dgvResultados.TabIndex = 0;
            // 
            // btnVerRelatorio
            // 
            this.btnVerRelatorio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnVerRelatorio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerRelatorio.Enabled = false;
            this.btnVerRelatorio.FlatAppearance.BorderSize = 0;
            this.btnVerRelatorio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerRelatorio.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnVerRelatorio.ForeColor = System.Drawing.Color.White;
            this.btnVerRelatorio.Location = new System.Drawing.Point(320, 640);
            this.btnVerRelatorio.Name = "btnVerRelatorio";
            this.btnVerRelatorio.Size = new System.Drawing.Size(220, 45);
            this.btnVerRelatorio.TabIndex = 6;
            this.btnVerRelatorio.Text = "📊 VER RELATÓRIO";
            this.btnVerRelatorio.UseVisualStyleBackColor = false;
            this.btnVerRelatorio.Click += new System.EventHandler(this.btnVerRelatorio_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(560, 640);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(220, 45);
            this.btnFechar.TabIndex = 7;
            this.btnFechar.Text = "✖ FECHAR";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // FormImportarClientesLote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.panelContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormImportarClientesLote";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importação em Lote de Clientes";
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCaminhoArquivo;
        private System.Windows.Forms.Button btnSelecionarArquivo;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnIniciarImportacao;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtResumo;
        private System.Windows.Forms.DataGridView dgvResultados;
        private System.Windows.Forms.Button btnVerRelatorio;
        private System.Windows.Forms.Button btnFechar;
    }
}