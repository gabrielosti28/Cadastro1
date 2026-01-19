// =============================================
// DESIGNER - FORMULÁRIO DE ANEXOS
// Arquivo: FormAnexosCliente.Designer.cs
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblNomeCliente = new System.Windows.Forms.Label();
            this.panelBotoes = new System.Windows.Forms.Panel();
            this.btnAdicionarAnexo = new System.Windows.Forms.Button();
            this.btnAbrirAnexo = new System.Windows.Forms.Button();
            this.btnExcluirAnexo = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.dgvAnexos = new System.Windows.Forms.DataGridView();
            this.lblInfo = new System.Windows.Forms.Label();
            this.panelContainer.SuspendLayout();
            this.panelBotoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnexos)).BeginInit();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelContainer.Controls.Add(this.lblInfo);
            this.panelContainer.Controls.Add(this.dgvAnexos);
            this.panelContainer.Controls.Add(this.panelBotoes);
            this.panelContainer.Controls.Add(this.lblNomeCliente);
            this.panelContainer.Controls.Add(this.lblTitulo);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(900, 600);
            this.panelContainer.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTitulo.Location = new System.Drawing.Point(250, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(387, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "📎 DOCUMENTOS DO CLIENTE";
            // 
            // lblNomeCliente
            // 
            this.lblNomeCliente.AutoSize = true;
            this.lblNomeCliente.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblNomeCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblNomeCliente.Location = new System.Drawing.Point(30, 65);
            this.lblNomeCliente.Name = "lblNomeCliente";
            this.lblNomeCliente.Size = new System.Drawing.Size(71, 21);
            this.lblNomeCliente.TabIndex = 1;
            this.lblNomeCliente.Text = "Cliente: ";
            // 
            // panelBotoes
            // 
            this.panelBotoes.BackColor = System.Drawing.Color.White;
            this.panelBotoes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBotoes.Controls.Add(this.btnAdicionarAnexo);
            this.panelBotoes.Controls.Add(this.btnAbrirAnexo);
            this.panelBotoes.Controls.Add(this.btnExcluirAnexo);
            this.panelBotoes.Controls.Add(this.btnFechar);
            this.panelBotoes.Location = new System.Drawing.Point(30, 100);
            this.panelBotoes.Name = "panelBotoes";
            this.panelBotoes.Size = new System.Drawing.Size(840, 70);
            this.panelBotoes.TabIndex = 2;
            // 
            // btnAdicionarAnexo
            // 
            this.btnAdicionarAnexo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAdicionarAnexo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdicionarAnexo.FlatAppearance.BorderSize = 0;
            this.btnAdicionarAnexo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdicionarAnexo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnAdicionarAnexo.ForeColor = System.Drawing.Color.White;
            this.btnAdicionarAnexo.Location = new System.Drawing.Point(15, 15);
            this.btnAdicionarAnexo.Name = "btnAdicionarAnexo";
            this.btnAdicionarAnexo.Size = new System.Drawing.Size(180, 40);
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
            this.btnAbrirAnexo.Location = new System.Drawing.Point(215, 15);
            this.btnAbrirAnexo.Name = "btnAbrirAnexo";
            this.btnAbrirAnexo.Size = new System.Drawing.Size(180, 40);
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
            this.btnExcluirAnexo.Location = new System.Drawing.Point(415, 15);
            this.btnExcluirAnexo.Name = "btnExcluirAnexo";
            this.btnExcluirAnexo.Size = new System.Drawing.Size(180, 40);
            this.btnExcluirAnexo.TabIndex = 2;
            this.btnExcluirAnexo.Text = "🗑️ EXCLUIR";
            this.btnExcluirAnexo.UseVisualStyleBackColor = false;
            this.btnExcluirAnexo.Click += new System.EventHandler(this.btnExcluirAnexo_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(645, 15);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(180, 40);
            this.btnFechar.TabIndex = 3;
            this.btnFechar.Text = "✖ FECHAR";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
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
            this.dgvAnexos.Location = new System.Drawing.Point(30, 220);
            this.dgvAnexos.MultiSelect = false;
            this.dgvAnexos.Name = "dgvAnexos";
            this.dgvAnexos.ReadOnly = true;
            this.dgvAnexos.RowHeadersVisible = false;
            this.dgvAnexos.RowTemplate.Height = 35;
            this.dgvAnexos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAnexos.Size = new System.Drawing.Size(840, 330);
            this.dgvAnexos.TabIndex = 3;
            this.dgvAnexos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAnexos_CellDoubleClick);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic);
            this.lblInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblInfo.Location = new System.Drawing.Point(30, 185);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(545, 17);
            this.lblInfo.TabIndex = 4;
            this.lblInfo.Text = "💡 Dica: Clique duas vezes em um arquivo para abri-lo. Tipos permitidos: PDF, Word, Excel, Imagens, TXT";
            // 
            // FormAnexosCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.panelContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormAnexosCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Documentos e Anexos do Cliente";
            this.Load += new System.EventHandler(this.FormAnexosCliente_Load);
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            this.panelBotoes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnexos)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblNomeCliente;
        private System.Windows.Forms.Panel panelBotoes;
        private System.Windows.Forms.Button btnAdicionarAnexo;
        private System.Windows.Forms.Button btnAbrirAnexo;
        private System.Windows.Forms.Button btnExcluirAnexo;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.DataGridView dgvAnexos;
        private System.Windows.Forms.Label lblInfo;
    }
}