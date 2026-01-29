// =============================================
// FORMULÁRIO DE CONFIGURAÇÃO DE PASTAS
// Arquivo: FormConfiguracaoPastas.Designer.cs
// Permite ao usuário configurar todas as pastas do sistema
// =============================================
namespace Cadastro1
{
    partial class FormConfiguracaoPastas
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
            this.lblInfo = new System.Windows.Forms.Label();
            this.panelPastas = new System.Windows.Forms.Panel();
            this.lblBackups = new System.Windows.Forms.Label();
            this.txtBackups = new System.Windows.Forms.TextBox();
            this.btnBackups = new System.Windows.Forms.Button();
            this.lblAnexos = new System.Windows.Forms.Label();
            this.txtAnexos = new System.Windows.Forms.TextBox();
            this.btnAnexos = new System.Windows.Forms.Button();
            this.lblTemplates = new System.Windows.Forms.Label();
            this.txtTemplates = new System.Windows.Forms.TextBox();
            this.btnTemplates = new System.Windows.Forms.Button();
            this.lblPDFs = new System.Windows.Forms.Label();
            this.txtPDFs = new System.Windows.Forms.TextBox();
            this.btnPDFs = new System.Windows.Forms.Button();
            this.lblLogs = new System.Windows.Forms.Label();
            this.txtLogs = new System.Windows.Forms.TextBox();
            this.btnLogs = new System.Windows.Forms.Button();
            this.panelBotoes = new System.Windows.Forms.Panel();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnRestaurarPadrao = new System.Windows.Forms.Button();
            this.btnTestarPermissoes = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelContainer.SuspendLayout();
            this.panelPastas.SuspendLayout();
            this.panelBotoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelContainer.Controls.Add(this.lblTitulo);
            this.panelContainer.Controls.Add(this.lblInfo);
            this.panelContainer.Controls.Add(this.panelPastas);
            this.panelContainer.Controls.Add(this.panelBotoes);
            this.panelContainer.Controls.Add(this.lblStatus);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(900, 700);
            this.panelContainer.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(860, 40);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "📁 CONFIGURAÇÃO DE PASTAS DO SISTEMA";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInfo
            // 
            this.lblInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(243)))), ((int)(((byte)(205)))));
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(100)))), ((int)(((byte)(4)))));
            this.lblInfo.Location = new System.Drawing.Point(20, 70);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Padding = new System.Windows.Forms.Padding(10);
            this.lblInfo.Size = new System.Drawing.Size(860, 80);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "💡 IMPORTANTE: Configure aqui onde o sistema salvará os arquivos.\r\nEscolha pasta" +
    "s com permissão de escrita e espaço em disco suficiente.\r\nAs pastas serão criad" +
    "as automaticamente se não existirem.";
            // 
            // panelPastas
            // 
            this.panelPastas.BackColor = System.Drawing.Color.White;
            this.panelPastas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPastas.Controls.Add(this.lblBackups);
            this.panelPastas.Controls.Add(this.txtBackups);
            this.panelPastas.Controls.Add(this.btnBackups);
            this.panelPastas.Controls.Add(this.lblAnexos);
            this.panelPastas.Controls.Add(this.txtAnexos);
            this.panelPastas.Controls.Add(this.btnAnexos);
            this.panelPastas.Controls.Add(this.lblTemplates);
            this.panelPastas.Controls.Add(this.txtTemplates);
            this.panelPastas.Controls.Add(this.btnTemplates);
            this.panelPastas.Controls.Add(this.lblPDFs);
            this.panelPastas.Controls.Add(this.txtPDFs);
            this.panelPastas.Controls.Add(this.btnPDFs);
            this.panelPastas.Controls.Add(this.lblLogs);
            this.panelPastas.Controls.Add(this.txtLogs);
            this.panelPastas.Controls.Add(this.btnLogs);
            this.panelPastas.Location = new System.Drawing.Point(20, 160);
            this.panelPastas.Name = "panelPastas";
            this.panelPastas.Size = new System.Drawing.Size(860, 420);
            this.panelPastas.TabIndex = 2;
            // 
            // lblBackups
            // 
            this.lblBackups.AutoSize = true;
            this.lblBackups.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblBackups.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblBackups.Location = new System.Drawing.Point(20, 20);
            this.lblBackups.Name = "lblBackups";
            this.lblBackups.Size = new System.Drawing.Size(299, 20);
            this.lblBackups.TabIndex = 0;
            this.lblBackups.Text = "💾 BACKUPS DO BANCO DE DADOS:";
            // 
            // txtBackups
            // 
            this.txtBackups.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBackups.Location = new System.Drawing.Point(20, 45);
            this.txtBackups.Name = "txtBackups";
            this.txtBackups.ReadOnly = true;
            this.txtBackups.Size = new System.Drawing.Size(650, 25);
            this.txtBackups.TabIndex = 1;
            // 
            // btnBackups
            // 
            this.btnBackups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnBackups.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackups.FlatAppearance.BorderSize = 0;
            this.btnBackups.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackups.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBackups.ForeColor = System.Drawing.Color.White;
            this.btnBackups.Location = new System.Drawing.Point(680, 43);
            this.btnBackups.Name = "btnBackups";
            this.btnBackups.Size = new System.Drawing.Size(160, 28);
            this.btnBackups.TabIndex = 2;
            this.btnBackups.Text = "📂 Escolher Pasta";
            this.btnBackups.UseVisualStyleBackColor = false;
            this.btnBackups.Click += new System.EventHandler(this.BtnBackups_Click);
            // 
            // lblAnexos
            // 
            this.lblAnexos.AutoSize = true;
            this.lblAnexos.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAnexos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblAnexos.Location = new System.Drawing.Point(20, 90);
            this.lblAnexos.Name = "lblAnexos";
            this.lblAnexos.Size = new System.Drawing.Size(329, 20);
            this.lblAnexos.TabIndex = 3;
            this.lblAnexos.Text = "📎 ANEXOS E DOCUMENTOS DOS CLIENTES:";
            // 
            // txtAnexos
            // 
            this.txtAnexos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAnexos.Location = new System.Drawing.Point(20, 115);
            this.txtAnexos.Name = "txtAnexos";
            this.txtAnexos.ReadOnly = true;
            this.txtAnexos.Size = new System.Drawing.Size(650, 25);
            this.txtAnexos.TabIndex = 4;
            // 
            // btnAnexos
            // 
            this.btnAnexos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnAnexos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnexos.FlatAppearance.BorderSize = 0;
            this.btnAnexos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnexos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAnexos.ForeColor = System.Drawing.Color.White;
            this.btnAnexos.Location = new System.Drawing.Point(680, 113);
            this.btnAnexos.Name = "btnAnexos";
            this.btnAnexos.Size = new System.Drawing.Size(160, 28);
            this.btnAnexos.TabIndex = 5;
            this.btnAnexos.Text = "📂 Escolher Pasta";
            this.btnAnexos.UseVisualStyleBackColor = false;
            this.btnAnexos.Click += new System.EventHandler(this.BtnAnexos_Click);
            // 
            // lblTemplates
            // 
            this.lblTemplates.AutoSize = true;
            this.lblTemplates.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTemplates.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTemplates.Location = new System.Drawing.Point(20, 160);
            this.lblTemplates.Name = "lblTemplates";
            this.lblTemplates.Size = new System.Drawing.Size(297, 20);
            this.lblTemplates.TabIndex = 6;
            this.lblTemplates.Text = "📋 TEMPLATES DE MALA DIRETA:";
            // 
            // txtTemplates
            // 
            this.txtTemplates.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTemplates.Location = new System.Drawing.Point(20, 185);
            this.txtTemplates.Name = "txtTemplates";
            this.txtTemplates.ReadOnly = true;
            this.txtTemplates.Size = new System.Drawing.Size(650, 25);
            this.txtTemplates.TabIndex = 7;
            // 
            // btnTemplates
            // 
            this.btnTemplates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnTemplates.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTemplates.FlatAppearance.BorderSize = 0;
            this.btnTemplates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTemplates.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTemplates.ForeColor = System.Drawing.Color.White;
            this.btnTemplates.Location = new System.Drawing.Point(680, 183);
            this.btnTemplates.Name = "btnTemplates";
            this.btnTemplates.Size = new System.Drawing.Size(160, 28);
            this.btnTemplates.TabIndex = 8;
            this.btnTemplates.Text = "📂 Escolher Pasta";
            this.btnTemplates.UseVisualStyleBackColor = false;
            this.btnTemplates.Click += new System.EventHandler(this.BtnTemplates_Click);
            // 
            // lblPDFs
            // 
            this.lblPDFs.AutoSize = true;
            this.lblPDFs.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPDFs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblPDFs.Location = new System.Drawing.Point(20, 230);
            this.lblPDFs.Name = "lblPDFs";
            this.lblPDFs.Size = new System.Drawing.Size(253, 20);
            this.lblPDFs.TabIndex = 9;
            this.lblPDFs.Text = "📄 PDFs DE MALA DIRETA:";
            // 
            // txtPDFs
            // 
            this.txtPDFs.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPDFs.Location = new System.Drawing.Point(20, 255);
            this.txtPDFs.Name = "txtPDFs";
            this.txtPDFs.ReadOnly = true;
            this.txtPDFs.Size = new System.Drawing.Size(650, 25);
            this.txtPDFs.TabIndex = 10;
            // 
            // btnPDFs
            // 
            this.btnPDFs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnPDFs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPDFs.FlatAppearance.BorderSize = 0;
            this.btnPDFs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPDFs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPDFs.ForeColor = System.Drawing.Color.White;
            this.btnPDFs.Location = new System.Drawing.Point(680, 253);
            this.btnPDFs.Name = "btnPDFs";
            this.btnPDFs.Size = new System.Drawing.Size(160, 28);
            this.btnPDFs.TabIndex = 11;
            this.btnPDFs.Text = "📂 Escolher Pasta";
            this.btnPDFs.UseVisualStyleBackColor = false;
            this.btnPDFs.Click += new System.EventHandler(this.BtnPDFs_Click);
            // 
            // lblLogs
            // 
            this.lblLogs.AutoSize = true;
            this.lblLogs.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblLogs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblLogs.Location = new System.Drawing.Point(20, 300);
            this.lblLogs.Name = "lblLogs";
            this.lblLogs.Size = new System.Drawing.Size(242, 20);
            this.lblLogs.TabIndex = 12;
            this.lblLogs.Text = "📝 LOGS DO SISTEMA:";
            // 
            // txtLogs
            // 
            this.txtLogs.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLogs.Location = new System.Drawing.Point(20, 325);
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.ReadOnly = true;
            this.txtLogs.Size = new System.Drawing.Size(650, 25);
            this.txtLogs.TabIndex = 13;
            // 
            // btnLogs
            // 
            this.btnLogs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnLogs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogs.FlatAppearance.BorderSize = 0;
            this.btnLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLogs.ForeColor = System.Drawing.Color.White;
            this.btnLogs.Location = new System.Drawing.Point(680, 323);
            this.btnLogs.Name = "btnLogs";
            this.btnLogs.Size = new System.Drawing.Size(160, 28);
            this.btnLogs.TabIndex = 14;
            this.btnLogs.Text = "📂 Escolher Pasta";
            this.btnLogs.UseVisualStyleBackColor = false;
            this.btnLogs.Click += new System.EventHandler(this.BtnLogs_Click);
            // 
            // panelBotoes
            // 
            this.panelBotoes.BackColor = System.Drawing.Color.White;
            this.panelBotoes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBotoes.Controls.Add(this.btnSalvar);
            this.panelBotoes.Controls.Add(this.btnRestaurarPadrao);
            this.panelBotoes.Controls.Add(this.btnTestarPermissoes);
            this.panelBotoes.Controls.Add(this.btnFechar);
            this.panelBotoes.Location = new System.Drawing.Point(20, 590);
            this.panelBotoes.Name = "panelBotoes";
            this.panelBotoes.Size = new System.Drawing.Size(860, 70);
            this.panelBotoes.TabIndex = 3;
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(20, 15);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(200, 40);
            this.btnSalvar.TabIndex = 0;
            this.btnSalvar.Text = "💾 SALVAR";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.BtnSalvar_Click);
            // 
            // btnRestaurarPadrao
            // 
            this.btnRestaurarPadrao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnRestaurarPadrao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestaurarPadrao.FlatAppearance.BorderSize = 0;
            this.btnRestaurarPadrao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestaurarPadrao.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRestaurarPadrao.ForeColor = System.Drawing.Color.White;
            this.btnRestaurarPadrao.Location = new System.Drawing.Point(235, 15);
            this.btnRestaurarPadrao.Name = "btnRestaurarPadrao";
            this.btnRestaurarPadrao.Size = new System.Drawing.Size(200, 40);
            this.btnRestaurarPadrao.TabIndex = 1;
            this.btnRestaurarPadrao.Text = "🔄 Restaurar Padrão";
            this.btnRestaurarPadrao.UseVisualStyleBackColor = false;
            this.btnRestaurarPadrao.Click += new System.EventHandler(this.BtnRestaurarPadrao_Click);
            // 
            // btnTestarPermissoes
            // 
            this.btnTestarPermissoes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnTestarPermissoes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTestarPermissoes.FlatAppearance.BorderSize = 0;
            this.btnTestarPermissoes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestarPermissoes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTestarPermissoes.ForeColor = System.Drawing.Color.White;
            this.btnTestarPermissoes.Location = new System.Drawing.Point(450, 15);
            this.btnTestarPermissoes.Name = "btnTestarPermissoes";
            this.btnTestarPermissoes.Size = new System.Drawing.Size(200, 40);
            this.btnTestarPermissoes.TabIndex = 2;
            this.btnTestarPermissoes.Text = "✓ Testar Permissões";
            this.btnTestarPermissoes.UseVisualStyleBackColor = false;
            this.btnTestarPermissoes.Click += new System.EventHandler(this.BtnTestarPermissoes_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(665, 15);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(175, 40);
            this.btnFechar.TabIndex = 3;
            this.btnFechar.Text = "✖ Fechar";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.BtnFechar_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblStatus.Location = new System.Drawing.Point(20, 670);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(860, 20);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Pronto";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormConfiguracaoPastas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 700);
            this.Controls.Add(this.panelContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormConfiguracaoPastas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuração de Pastas do Sistema";
            this.Load += new System.EventHandler(this.FormConfiguracaoPastas_Load);
            this.panelContainer.ResumeLayout(false);
            this.panelPastas.ResumeLayout(false);
            this.panelPastas.PerformLayout();
            this.panelBotoes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Panel panelPastas;
        private System.Windows.Forms.Label lblBackups;
        private System.Windows.Forms.TextBox txtBackups;
        private System.Windows.Forms.Button btnBackups;
        private System.Windows.Forms.Label lblAnexos;
        private System.Windows.Forms.TextBox txtAnexos;
        private System.Windows.Forms.Button btnAnexos;
        private System.Windows.Forms.Label lblTemplates;
        private System.Windows.Forms.TextBox txtTemplates;
        private System.Windows.Forms.Button btnTemplates;
        private System.Windows.Forms.Label lblPDFs;
        private System.Windows.Forms.TextBox txtPDFs;
        private System.Windows.Forms.Button btnPDFs;
        private System.Windows.Forms.Label lblLogs;
        private System.Windows.Forms.TextBox txtLogs;
        private System.Windows.Forms.Button btnLogs;
        private System.Windows.Forms.Panel panelBotoes;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnRestaurarPadrao;
        private System.Windows.Forms.Button btnTestarPermissoes;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Label lblStatus;
    }
}