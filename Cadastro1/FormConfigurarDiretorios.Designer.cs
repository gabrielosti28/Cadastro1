// =============================================
// FORMULÁRIO DE CONFIGURAÇÃO DE DIRETÓRIOS
// Arquivo: FormConfigurarDiretorios.Designer.cs
// Interface para configurar todos os caminhos do sistema
// =============================================
using System.Drawing;
using System.Windows.Forms;

namespace Cadastro1
{
    partial class FormConfigurarDiretorios
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

        private void InitializeComponent()
        {
            this.panelContainer = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblAviso = new System.Windows.Forms.Label();
            this.panelDiretorios = new System.Windows.Forms.Panel();

            // Backups
            this.lblBackups = new System.Windows.Forms.Label();
            this.txtBackups = new System.Windows.Forms.TextBox();
            this.btnEscolherBackups = new System.Windows.Forms.Button();
            this.btnAbrirBackups = new System.Windows.Forms.Button();

            // Anexos
            this.lblAnexos = new System.Windows.Forms.Label();
            this.txtAnexos = new System.Windows.Forms.TextBox();
            this.btnEscolherAnexos = new System.Windows.Forms.Button();
            this.btnAbrirAnexos = new System.Windows.Forms.Button();

            // Templates
            this.lblTemplates = new System.Windows.Forms.Label();
            this.txtTemplates = new System.Windows.Forms.TextBox();
            this.btnEscolherTemplates = new System.Windows.Forms.Button();
            this.btnAbrirTemplates = new System.Windows.Forms.Button();

            // PDFs
            this.lblPDFs = new System.Windows.Forms.Label();
            this.txtPDFs = new System.Windows.Forms.TextBox();
            this.btnEscolherPDFs = new System.Windows.Forms.Button();
            this.btnAbrirPDFs = new System.Windows.Forms.Button();

            // Logs
            this.lblLogs = new System.Windows.Forms.Label();
            this.txtLogs = new System.Windows.Forms.TextBox();
            this.btnEscolherLogs = new System.Windows.Forms.Button();
            this.btnAbrirLogs = new System.Windows.Forms.Button();

            // Botões de ação
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnRestaurar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();

            this.panelContainer.SuspendLayout();
            this.panelDiretorios.SuspendLayout();
            this.SuspendLayout();

            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = Color.FromArgb(240, 248, 255);
            this.panelContainer.Controls.Add(this.lblTitulo);
            this.panelContainer.Controls.Add(this.lblAviso);
            this.panelContainer.Controls.Add(this.panelDiretorios);
            this.panelContainer.Controls.Add(this.btnSalvar);
            this.panelContainer.Controls.Add(this.btnRestaurar);
            this.panelContainer.Controls.Add(this.btnFechar);
            this.panelContainer.Dock = DockStyle.Fill;
            this.panelContainer.Location = new Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new Size(900, 700);
            this.panelContainer.TabIndex = 0;

            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(0, 102, 204);
            this.lblTitulo.Location = new Point(20, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new Size(860, 35);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "⚙️ CONFIGURAÇÃO DE DIRETÓRIOS";
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblAviso
            // 
            this.lblAviso.BackColor = Color.FromArgb(255, 243, 205);
            this.lblAviso.BorderStyle = BorderStyle.FixedSingle;
            this.lblAviso.Font = new Font("Segoe UI", 9F);
            this.lblAviso.ForeColor = Color.FromArgb(133, 100, 4);
            this.lblAviso.Location = new Point(20, 70);
            this.lblAviso.Name = "lblAviso";
            this.lblAviso.Padding = new Padding(10);
            this.lblAviso.Size = new Size(860, 60);
            this.lblAviso.TabIndex = 1;
            this.lblAviso.Text = "⚠️ IMPORTANTE: Configure aqui onde o sistema salvará arquivos no seu computador.\n" +
                "Isso permite mover o programa entre computadores mantendo seus dados organizados.";
            this.lblAviso.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // panelDiretorios
            // 
            this.panelDiretorios.BackColor = Color.White;
            this.panelDiretorios.BorderStyle = BorderStyle.FixedSingle;
            this.panelDiretorios.Location = new Point(20, 145);
            this.panelDiretorios.Name = "panelDiretorios";
            this.panelDiretorios.Size = new Size(860, 470);
            this.panelDiretorios.TabIndex = 2;

            // ========== BACKUPS ==========

            this.lblBackups.AutoSize = true;
            this.lblBackups.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblBackups.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblBackups.Location = new Point(20, 20);
            this.lblBackups.Name = "lblBackups";
            this.lblBackups.Size = new Size(200, 20);
            this.lblBackups.TabIndex = 0;
            this.lblBackups.Text = "💾 BACKUPS DO BANCO:";

            this.txtBackups.Font = new Font("Segoe UI", 10F);
            this.txtBackups.Location = new Point(20, 45);
            this.txtBackups.Name = "txtBackups";
            this.txtBackups.ReadOnly = true;
            this.txtBackups.Size = new Size(540, 25);
            this.txtBackups.TabIndex = 1;

            this.btnEscolherBackups.BackColor = Color.FromArgb(52, 152, 219);
            this.btnEscolherBackups.Cursor = Cursors.Hand;
            this.btnEscolherBackups.FlatAppearance.BorderSize = 0;
            this.btnEscolherBackups.FlatStyle = FlatStyle.Flat;
            this.btnEscolherBackups.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnEscolherBackups.ForeColor = Color.White;
            this.btnEscolherBackups.Location = new Point(570, 43);
            this.btnEscolherBackups.Name = "btnEscolherBackups";
            this.btnEscolherBackups.Size = new Size(130, 30);
            this.btnEscolherBackups.TabIndex = 2;
            this.btnEscolherBackups.Text = "📁 Escolher";
            this.btnEscolherBackups.UseVisualStyleBackColor = false;
            this.btnEscolherBackups.Click += new System.EventHandler(this.BtnEscolherBackups_Click);

            this.btnAbrirBackups.BackColor = Color.FromArgb(155, 89, 182);
            this.btnAbrirBackups.Cursor = Cursors.Hand;
            this.btnAbrirBackups.FlatAppearance.BorderSize = 0;
            this.btnAbrirBackups.FlatStyle = FlatStyle.Flat;
            this.btnAbrirBackups.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnAbrirBackups.ForeColor = Color.White;
            this.btnAbrirBackups.Location = new Point(710, 43);
            this.btnAbrirBackups.Name = "btnAbrirBackups";
            this.btnAbrirBackups.Size = new Size(130, 30);
            this.btnAbrirBackups.TabIndex = 3;
            this.btnAbrirBackups.Text = "🗂️ Abrir Pasta";
            this.btnAbrirBackups.UseVisualStyleBackColor = false;
            this.btnAbrirBackups.Click += new System.EventHandler(this.BtnAbrirBackups_Click);

            // ========== ANEXOS ==========

            this.lblAnexos.AutoSize = true;
            this.lblAnexos.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblAnexos.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblAnexos.Location = new Point(20, 100);
            this.lblAnexos.Name = "lblAnexos";
            this.lblAnexos.Size = new Size(250, 20);
            this.lblAnexos.TabIndex = 4;
            this.lblAnexos.Text = "📎 ANEXOS DOS CLIENTES:";

            this.txtAnexos.Font = new Font("Segoe UI", 10F);
            this.txtAnexos.Location = new Point(20, 125);
            this.txtAnexos.Name = "txtAnexos";
            this.txtAnexos.ReadOnly = true;
            this.txtAnexos.Size = new Size(540, 25);
            this.txtAnexos.TabIndex = 5;

            this.btnEscolherAnexos.BackColor = Color.FromArgb(52, 152, 219);
            this.btnEscolherAnexos.Cursor = Cursors.Hand;
            this.btnEscolherAnexos.FlatAppearance.BorderSize = 0;
            this.btnEscolherAnexos.FlatStyle = FlatStyle.Flat;
            this.btnEscolherAnexos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnEscolherAnexos.ForeColor = Color.White;
            this.btnEscolherAnexos.Location = new Point(570, 123);
            this.btnEscolherAnexos.Name = "btnEscolherAnexos";
            this.btnEscolherAnexos.Size = new Size(130, 30);
            this.btnEscolherAnexos.TabIndex = 6;
            this.btnEscolherAnexos.Text = "📁 Escolher";
            this.btnEscolherAnexos.UseVisualStyleBackColor = false;
            this.btnEscolherAnexos.Click += new System.EventHandler(this.BtnEscolherAnexos_Click);

            this.btnAbrirAnexos.BackColor = Color.FromArgb(155, 89, 182);
            this.btnAbrirAnexos.Cursor = Cursors.Hand;
            this.btnAbrirAnexos.FlatAppearance.BorderSize = 0;
            this.btnAbrirAnexos.FlatStyle = FlatStyle.Flat;
            this.btnAbrirAnexos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnAbrirAnexos.ForeColor = Color.White;
            this.btnAbrirAnexos.Location = new Point(710, 123);
            this.btnAbrirAnexos.Name = "btnAbrirAnexos";
            this.btnAbrirAnexos.Size = new Size(130, 30);
            this.btnAbrirAnexos.TabIndex = 7;
            this.btnAbrirAnexos.Text = "🗂️ Abrir Pasta";
            this.btnAbrirAnexos.UseVisualStyleBackColor = false;
            this.btnAbrirAnexos.Click += new System.EventHandler(this.BtnAbrirAnexos_Click);

            // ========== TEMPLATES ==========

            this.lblTemplates.AutoSize = true;
            this.lblTemplates.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblTemplates.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblTemplates.Location = new Point(20, 180);
            this.lblTemplates.Name = "lblTemplates";
            this.lblTemplates.Size = new Size(250, 20);
            this.lblTemplates.TabIndex = 8;
            this.lblTemplates.Text = "📄 TEMPLATES DE MALA DIRETA:";

            this.txtTemplates.Font = new Font("Segoe UI", 10F);
            this.txtTemplates.Location = new Point(20, 205);
            this.txtTemplates.Name = "txtTemplates";
            this.txtTemplates.ReadOnly = true;
            this.txtTemplates.Size = new Size(540, 25);
            this.txtTemplates.TabIndex = 9;

            this.btnEscolherTemplates.BackColor = Color.FromArgb(52, 152, 219);
            this.btnEscolherTemplates.Cursor = Cursors.Hand;
            this.btnEscolherTemplates.FlatAppearance.BorderSize = 0;
            this.btnEscolherTemplates.FlatStyle = FlatStyle.Flat;
            this.btnEscolherTemplates.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnEscolherTemplates.ForeColor = Color.White;
            this.btnEscolherTemplates.Location = new Point(570, 203);
            this.btnEscolherTemplates.Name = "btnEscolherTemplates";
            this.btnEscolherTemplates.Size = new Size(130, 30);
            this.btnEscolherTemplates.TabIndex = 10;
            this.btnEscolherTemplates.Text = "📁 Escolher";
            this.btnEscolherTemplates.UseVisualStyleBackColor = false;
            this.btnEscolherTemplates.Click += new System.EventHandler(this.BtnEscolherTemplates_Click);

            this.btnAbrirTemplates.BackColor = Color.FromArgb(155, 89, 182);
            this.btnAbrirTemplates.Cursor = Cursors.Hand;
            this.btnAbrirTemplates.FlatAppearance.BorderSize = 0;
            this.btnAbrirTemplates.FlatStyle = FlatStyle.Flat;
            this.btnAbrirTemplates.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnAbrirTemplates.ForeColor = Color.White;
            this.btnAbrirTemplates.Location = new Point(710, 203);
            this.btnAbrirTemplates.Name = "btnAbrirTemplates";
            this.btnAbrirTemplates.Size = new Size(130, 30);
            this.btnAbrirTemplates.TabIndex = 11;
            this.btnAbrirTemplates.Text = "🗂️ Abrir Pasta";
            this.btnAbrirTemplates.UseVisualStyleBackColor = false;
            this.btnAbrirTemplates.Click += new System.EventHandler(this.BtnAbrirTemplates_Click);

            // ========== PDFs ==========

            this.lblPDFs.AutoSize = true;
            this.lblPDFs.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblPDFs.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblPDFs.Location = new Point(20, 260);
            this.lblPDFs.Name = "lblPDFs";
            this.lblPDFs.Size = new Size(250, 20);
            this.lblPDFs.TabIndex = 12;
            this.lblPDFs.Text = "📮 PDFs DE MALA DIRETA:";

            this.txtPDFs.Font = new Font("Segoe UI", 10F);
            this.txtPDFs.Location = new Point(20, 285);
            this.txtPDFs.Name = "txtPDFs";
            this.txtPDFs.ReadOnly = true;
            this.txtPDFs.Size = new Size(540, 25);
            this.txtPDFs.TabIndex = 13;

            this.btnEscolherPDFs.BackColor = Color.FromArgb(52, 152, 219);
            this.btnEscolherPDFs.Cursor = Cursors.Hand;
            this.btnEscolherPDFs.FlatAppearance.BorderSize = 0;
            this.btnEscolherPDFs.FlatStyle = FlatStyle.Flat;
            this.btnEscolherPDFs.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnEscolherPDFs.ForeColor = Color.White;
            this.btnEscolherPDFs.Location = new Point(570, 283);
            this.btnEscolherPDFs.Name = "btnEscolherPDFs";
            this.btnEscolherPDFs.Size = new Size(130, 30);
            this.btnEscolherPDFs.TabIndex = 14;
            this.btnEscolherPDFs.Text = "📁 Escolher";
            this.btnEscolherPDFs.UseVisualStyleBackColor = false;
            this.btnEscolherPDFs.Click += new System.EventHandler(this.BtnEscolherPDFs_Click);

            this.btnAbrirPDFs.BackColor = Color.FromArgb(155, 89, 182);
            this.btnAbrirPDFs.Cursor = Cursors.Hand;
            this.btnAbrirPDFs.FlatAppearance.BorderSize = 0;
            this.btnAbrirPDFs.FlatStyle = FlatStyle.Flat;
            this.btnAbrirPDFs.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnAbrirPDFs.ForeColor = Color.White;
            this.btnAbrirPDFs.Location = new Point(710, 283);
            this.btnAbrirPDFs.Name = "btnAbrirPDFs";
            this.btnAbrirPDFs.Size = new Size(130, 30);
            this.btnAbrirPDFs.TabIndex = 15;
            this.btnAbrirPDFs.Text = "🗂️ Abrir Pasta";
            this.btnAbrirPDFs.UseVisualStyleBackColor = false;
            this.btnAbrirPDFs.Click += new System.EventHandler(this.BtnAbrirPDFs_Click);

            // ========== LOGS ==========

            this.lblLogs.AutoSize = true;
            this.lblLogs.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblLogs.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblLogs.Location = new Point(20, 340);
            this.lblLogs.Name = "lblLogs";
            this.lblLogs.Size = new Size(250, 20);
            this.lblLogs.TabIndex = 16;
            this.lblLogs.Text = "📝 LOGS DO SISTEMA:";

            this.txtLogs.Font = new Font("Segoe UI", 10F);
            this.txtLogs.Location = new Point(20, 365);
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.ReadOnly = true;
            this.txtLogs.Size = new Size(540, 25);
            this.txtLogs.TabIndex = 17;

            this.btnEscolherLogs.BackColor = Color.FromArgb(52, 152, 219);
            this.btnEscolherLogs.Cursor = Cursors.Hand;
            this.btnEscolherLogs.FlatAppearance.BorderSize = 0;
            this.btnEscolherLogs.FlatStyle = FlatStyle.Flat;
            this.btnEscolherLogs.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnEscolherLogs.ForeColor = Color.White;
            this.btnEscolherLogs.Location = new Point(570, 363);
            this.btnEscolherLogs.Name = "btnEscolherLogs";
            this.btnEscolherLogs.Size = new Size(130, 30);
            this.btnEscolherLogs.TabIndex = 18;
            this.btnEscolherLogs.Text = "📁 Escolher";
            this.btnEscolherLogs.UseVisualStyleBackColor = false;
            this.btnEscolherLogs.Click += new System.EventHandler(this.BtnEscolherLogs_Click);

            this.btnAbrirLogs.BackColor = Color.FromArgb(155, 89, 182);
            this.btnAbrirLogs.Cursor = Cursors.Hand;
            this.btnAbrirLogs.FlatAppearance.BorderSize = 0;
            this.btnAbrirLogs.FlatStyle = FlatStyle.Flat;
            this.btnAbrirLogs.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnAbrirLogs.ForeColor = Color.White;
            this.btnAbrirLogs.Location = new Point(710, 363);
            this.btnAbrirLogs.Name = "btnAbrirLogs";
            this.btnAbrirLogs.Size = new Size(130, 30);
            this.btnAbrirLogs.TabIndex = 19;
            this.btnAbrirLogs.Text = "🗂️ Abrir Pasta";
            this.btnAbrirLogs.UseVisualStyleBackColor = false;
            this.btnAbrirLogs.Click += new System.EventHandler(this.BtnAbrirLogs_Click);

            // Adicionar todos os controles ao painel
            this.panelDiretorios.Controls.Add(this.lblBackups);
            this.panelDiretorios.Controls.Add(this.txtBackups);
            this.panelDiretorios.Controls.Add(this.btnEscolherBackups);
            this.panelDiretorios.Controls.Add(this.btnAbrirBackups);

            this.panelDiretorios.Controls.Add(this.lblAnexos);
            this.panelDiretorios.Controls.Add(this.txtAnexos);
            this.panelDiretorios.Controls.Add(this.btnEscolherAnexos);
            this.panelDiretorios.Controls.Add(this.btnAbrirAnexos);

            this.panelDiretorios.Controls.Add(this.lblTemplates);
            this.panelDiretorios.Controls.Add(this.txtTemplates);
            this.panelDiretorios.Controls.Add(this.btnEscolherTemplates);
            this.panelDiretorios.Controls.Add(this.btnAbrirTemplates);

            this.panelDiretorios.Controls.Add(this.lblPDFs);
            this.panelDiretorios.Controls.Add(this.txtPDFs);
            this.panelDiretorios.Controls.Add(this.btnEscolherPDFs);
            this.panelDiretorios.Controls.Add(this.btnAbrirPDFs);

            this.panelDiretorios.Controls.Add(this.lblLogs);
            this.panelDiretorios.Controls.Add(this.txtLogs);
            this.panelDiretorios.Controls.Add(this.btnEscolherLogs);
            this.panelDiretorios.Controls.Add(this.btnAbrirLogs);

            // ========== BOTÕES DE AÇÃO ==========

            this.btnSalvar.BackColor = Color.FromArgb(46, 204, 113);
            this.btnSalvar.Cursor = Cursors.Hand;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatStyle = FlatStyle.Flat;
            this.btnSalvar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnSalvar.ForeColor = Color.White;
            this.btnSalvar.Location = new Point(150, 630);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new Size(200, 45);
            this.btnSalvar.TabIndex = 20;
            this.btnSalvar.Text = "✔️ SALVAR TUDO";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.BtnSalvar_Click);

            this.btnRestaurar.BackColor = Color.FromArgb(230, 126, 34);
            this.btnRestaurar.Cursor = Cursors.Hand;
            this.btnRestaurar.FlatAppearance.BorderSize = 0;
            this.btnRestaurar.FlatStyle = FlatStyle.Flat;
            this.btnRestaurar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnRestaurar.ForeColor = Color.White;
            this.btnRestaurar.Location = new Point(370, 630);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new Size(200, 45);
            this.btnRestaurar.TabIndex = 21;
            this.btnRestaurar.Text = "🔄 RESTAURAR PADRÃO";
            this.btnRestaurar.UseVisualStyleBackColor = false;
            this.btnRestaurar.Click += new System.EventHandler(this.BtnRestaurar_Click);

            this.btnFechar.BackColor = Color.FromArgb(149, 165, 166);
            this.btnFechar.Cursor = Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = FlatStyle.Flat;
            this.btnFechar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnFechar.ForeColor = Color.White;
            this.btnFechar.Location = new Point(590, 630);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new Size(160, 45);
            this.btnFechar.TabIndex = 22;
            this.btnFechar.Text = "✖️ FECHAR";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.BtnFechar_Click);

            // 
            // FormConfigurarDiretorios
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(900, 700);
            this.Controls.Add(this.panelContainer);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfigurarDiretorios";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Configuração de Diretórios do Sistema";
            this.Load += new System.EventHandler(this.FormConfigurarDiretorios_Load);
            this.panelContainer.ResumeLayout(false);
            this.panelDiretorios.ResumeLayout(false);
            this.panelDiretorios.PerformLayout();
            this.ResumeLayout(false);
        }

        private Panel panelContainer;
        private Label lblTitulo;
        private Label lblAviso;
        private Panel panelDiretorios;

        // Backups
        private Label lblBackups;
        private TextBox txtBackups;
        private Button btnEscolherBackups;
        private Button btnAbrirBackups;

        // Anexos
        private Label lblAnexos;
        private TextBox txtAnexos;
        private Button btnEscolherAnexos;
        private Button btnAbrirAnexos;

        // Templates
        private Label lblTemplates;
        private TextBox txtTemplates;
        private Button btnEscolherTemplates;
        private Button btnAbrirTemplates;

        // PDFs
        private Label lblPDFs;
        private TextBox txtPDFs;
        private Button btnEscolherPDFs;
        private Button btnAbrirPDFs;

        // Logs
        private Label lblLogs;
        private TextBox txtLogs;
        private Button btnEscolherLogs;
        private Button btnAbrirLogs;

        // Botões de ação
        private Button btnSalvar;
        private Button btnRestaurar;
        private Button btnFechar;
    }
}