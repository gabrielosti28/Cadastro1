// =============================================
// DESIGNER - FORMULÁRIO RELATÓRIO
// Arquivo: FormRelatorioImportacao.Designer.cs
// =============================================
namespace Cadastro1
{
    partial class FormRelatorioImportacao
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
            this.txtRelatorio = new System.Windows.Forms.TextBox();
            this.btnSalvarRelatorio = new System.Windows.Forms.Button();
            this.btnCopiar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.panelContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelContainer.Controls.Add(this.lblTitulo);
            this.panelContainer.Controls.Add(this.txtRelatorio);
            this.panelContainer.Controls.Add(this.btnSalvarRelatorio);
            this.panelContainer.Controls.Add(this.btnCopiar);
            this.panelContainer.Controls.Add(this.btnFechar);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(900, 650);
            this.panelContainer.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTitulo.Location = new System.Drawing.Point(250, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(400, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "📊 RELATÓRIO DE IMPORTAÇÃO";
            // 
            // txtRelatorio
            // 
            this.txtRelatorio.BackColor = System.Drawing.Color.White;
            this.txtRelatorio.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtRelatorio.Location = new System.Drawing.Point(30, 70);
            this.txtRelatorio.Multiline = true;
            this.txtRelatorio.Name = "txtRelatorio";
            this.txtRelatorio.ReadOnly = true;
            this.txtRelatorio.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRelatorio.Size = new System.Drawing.Size(840, 500);
            this.txtRelatorio.TabIndex = 1;
            // 
            // btnSalvarRelatorio
            // 
            this.btnSalvarRelatorio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSalvarRelatorio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvarRelatorio.FlatAppearance.BorderSize = 0;
            this.btnSalvarRelatorio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvarRelatorio.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSalvarRelatorio.ForeColor = System.Drawing.Color.White;
            this.btnSalvarRelatorio.Location = new System.Drawing.Point(180, 585);
            this.btnSalvarRelatorio.Name = "btnSalvarRelatorio";
            this.btnSalvarRelatorio.Size = new System.Drawing.Size(200, 45);
            this.btnSalvarRelatorio.TabIndex = 2;
            this.btnSalvarRelatorio.Text = "💾 SALVAR";
            this.btnSalvarRelatorio.UseVisualStyleBackColor = false;
            this.btnSalvarRelatorio.Click += new System.EventHandler(this.btnSalvarRelatorio_Click);
            // 
            // btnCopiar
            // 
            this.btnCopiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnCopiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCopiar.FlatAppearance.BorderSize = 0;
            this.btnCopiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopiar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCopiar.ForeColor = System.Drawing.Color.White;
            this.btnCopiar.Location = new System.Drawing.Point(400, 585);
            this.btnCopiar.Name = "btnCopiar";
            this.btnCopiar.Size = new System.Drawing.Size(200, 45);
            this.btnCopiar.TabIndex = 3;
            this.btnCopiar.Text = "📋 COPIAR";
            this.btnCopiar.UseVisualStyleBackColor = false;
            this.btnCopiar.Click += new System.EventHandler(this.btnCopiar_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(620, 585);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(200, 45);
            this.btnFechar.TabIndex = 4;
            this.btnFechar.Text = "✖ FECHAR";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // FormRelatorioImportacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 650);
            this.Controls.Add(this.panelContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormRelatorioImportacao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Relatório Detalhado da Importação";
            this.Load += new System.EventHandler(this.FormRelatorioImportacao_Load);
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TextBox txtRelatorio;
        private System.Windows.Forms.Button btnSalvarRelatorio;
        private System.Windows.Forms.Button btnCopiar;
        private System.Windows.Forms.Button btnFechar;
    }
}