// =============================================
// FORMULÁRIO DE CONFIGURAÇÃO DE DIRETÓRIOS
// Arquivo: FormConfigurarDiretorios.Designer.cs
// Interface para configurar todos os caminhos do sistema
// CORRIGIDO: Adicionados controles de Logs SMS
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
            this.lblBackups = new System.Windows.Forms.Label();
            this.txtBackups = new System.Windows.Forms.TextBox();
            this.btnEscolherBackups = new System.Windows.Forms.Button();
            this.btnAbrirBackups = new System.Windows.Forms.Button();
            this.lblAnexos = new System.Windows.Forms.Label();
            this.txtAnexos = new System.Windows.Forms.TextBox();
            this.btnEscolherAnexos = new System.Windows.Forms.Button();
            this.btnAbrirAnexos = new System.Windows.Forms.Button();
            this.lblTemplates = new System.Windows.Forms.Label();
            this.txtTemplates = new System.Windows.Forms.TextBox();
            this.btnEscolherTemplates = new System.Windows.Forms.Button();
            this.btnAbrirTemplates = new System.Windows.Forms.Button();
            this.lblPDFs = new System.Windows.Forms.Label();
            this.txtPDFs = new System.Windows.Forms.TextBox();
            this.btnEscolherPDFs = new System.Windows.Forms.Button();
            this.btnAbrirPDFs = new System.Windows.Forms.Button();
            this.lblLogs = new System.Windows.Forms.Label();
            this.txtLogs = new System.Windows.Forms.TextBox();
            this.btnEscolherLogs = new System.Windows.Forms.Button();
            this.btnAbrirLogs = new System.Windows.Forms.Button();
            this.lblLogsSms = new System.Windows.Forms.Label();
            this.txtLogsSms = new System.Windows.Forms.TextBox();
            this.btnEscolherLogsSms = new System.Windows.Forms.Button();
            this.btnAbrirLogsSms = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnRestaurar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panelContainer.SuspendLayout();
            this.panelDiretorios.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelContainer.Controls.Add(this.button1);
            this.panelContainer.Controls.Add(this.lblTitulo);
            this.panelContainer.Controls.Add(this.lblAviso);
            this.panelContainer.Controls.Add(this.panelDiretorios);
            this.panelContainer.Controls.Add(this.btnSalvar);
            this.panelContainer.Controls.Add(this.btnRestaurar);
            this.panelContainer.Controls.Add(this.btnFechar);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(900, 750);
            this.panelContainer.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(860, 35);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "⚙️ CONFIGURAÇÃO DE DIRETÓRIOS";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAviso
            // 
            this.lblAviso.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(243)))), ((int)(((byte)(205)))));
            this.lblAviso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAviso.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAviso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(100)))), ((int)(((byte)(4)))));
            this.lblAviso.Location = new System.Drawing.Point(20, 70);
            this.lblAviso.Name = "lblAviso";
            this.lblAviso.Padding = new System.Windows.Forms.Padding(10);
            this.lblAviso.Size = new System.Drawing.Size(860, 60);
            this.lblAviso.TabIndex = 1;
            this.lblAviso.Text = "⚠️ IMPORTANTE: Configure aqui onde o sistema salvará arquivos no seu computador.\n" +
    "Isso permite mover o programa entre computadores mantendo seus dados organizados" +
    ".";
            this.lblAviso.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelDiretorios
            // 
            this.panelDiretorios.BackColor = System.Drawing.Color.White;
            this.panelDiretorios.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.panelDiretorios.Controls.Add(this.lblLogsSms);
            this.panelDiretorios.Controls.Add(this.txtLogsSms);
            this.panelDiretorios.Controls.Add(this.btnEscolherLogsSms);
            this.panelDiretorios.Controls.Add(this.btnAbrirLogsSms);
            this.panelDiretorios.Location = new System.Drawing.Point(20, 145);
            this.panelDiretorios.Name = "panelDiretorios";
            this.panelDiretorios.Size = new System.Drawing.Size(860, 520);
            this.panelDiretorios.TabIndex = 2;
            // 
            // lblBackups
            // 
            this.lblBackups.AutoSize = true;
            this.lblBackups.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblBackups.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblBackups.Location = new System.Drawing.Point(20, 20);
            this.lblBackups.Name = "lblBackups";
            this.lblBackups.Size = new System.Drawing.Size(189, 20);
            this.lblBackups.TabIndex = 0;
            this.lblBackups.Text = "💾 BACKUPS DO BANCO:";
            // 
            // txtBackups
            // 
            this.txtBackups.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBackups.Location = new System.Drawing.Point(20, 45);
            this.txtBackups.Name = "txtBackups";
            this.txtBackups.ReadOnly = true;
            this.txtBackups.Size = new System.Drawing.Size(540, 25);
            this.txtBackups.TabIndex = 1;
            // 
            // btnEscolherBackups
            // 
            this.btnEscolherBackups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnEscolherBackups.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEscolherBackups.FlatAppearance.BorderSize = 0;
            this.btnEscolherBackups.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEscolherBackups.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEscolherBackups.ForeColor = System.Drawing.Color.White;
            this.btnEscolherBackups.Location = new System.Drawing.Point(570, 43);
            this.btnEscolherBackups.Name = "btnEscolherBackups";
            this.btnEscolherBackups.Size = new System.Drawing.Size(130, 30);
            this.btnEscolherBackups.TabIndex = 2;
            this.btnEscolherBackups.Text = "📁 Escolher";
            this.btnEscolherBackups.UseVisualStyleBackColor = false;
            this.btnEscolherBackups.Click += new System.EventHandler(this.BtnEscolherBackups_Click);
            // 
            // btnAbrirBackups
            // 
            this.btnAbrirBackups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnAbrirBackups.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbrirBackups.FlatAppearance.BorderSize = 0;
            this.btnAbrirBackups.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbrirBackups.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAbrirBackups.ForeColor = System.Drawing.Color.White;
            this.btnAbrirBackups.Location = new System.Drawing.Point(710, 43);
            this.btnAbrirBackups.Name = "btnAbrirBackups";
            this.btnAbrirBackups.Size = new System.Drawing.Size(130, 30);
            this.btnAbrirBackups.TabIndex = 3;
            this.btnAbrirBackups.Text = "🗂️ Abrir Pasta";
            this.btnAbrirBackups.UseVisualStyleBackColor = false;
            this.btnAbrirBackups.Click += new System.EventHandler(this.BtnAbrirBackups_Click);
            // 
            // lblAnexos
            // 
            this.lblAnexos.AutoSize = true;
            this.lblAnexos.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAnexos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblAnexos.Location = new System.Drawing.Point(20, 100);
            this.lblAnexos.Name = "lblAnexos";
            this.lblAnexos.Size = new System.Drawing.Size(193, 20);
            this.lblAnexos.TabIndex = 4;
            this.lblAnexos.Text = "📎 ANEXOS DOS CLIENTES:";
            // 
            // txtAnexos
            // 
            this.txtAnexos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAnexos.Location = new System.Drawing.Point(20, 125);
            this.txtAnexos.Name = "txtAnexos";
            this.txtAnexos.ReadOnly = true;
            this.txtAnexos.Size = new System.Drawing.Size(540, 25);
            this.txtAnexos.TabIndex = 5;
            // 
            // btnEscolherAnexos
            // 
            this.btnEscolherAnexos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnEscolherAnexos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEscolherAnexos.FlatAppearance.BorderSize = 0;
            this.btnEscolherAnexos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEscolherAnexos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEscolherAnexos.ForeColor = System.Drawing.Color.White;
            this.btnEscolherAnexos.Location = new System.Drawing.Point(570, 123);
            this.btnEscolherAnexos.Name = "btnEscolherAnexos";
            this.btnEscolherAnexos.Size = new System.Drawing.Size(130, 30);
            this.btnEscolherAnexos.TabIndex = 6;
            this.btnEscolherAnexos.Text = "📁 Escolher";
            this.btnEscolherAnexos.UseVisualStyleBackColor = false;
            this.btnEscolherAnexos.Click += new System.EventHandler(this.BtnEscolherAnexos_Click);
            // 
            // btnAbrirAnexos
            // 
            this.btnAbrirAnexos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnAbrirAnexos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbrirAnexos.FlatAppearance.BorderSize = 0;
            this.btnAbrirAnexos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbrirAnexos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAbrirAnexos.ForeColor = System.Drawing.Color.White;
            this.btnAbrirAnexos.Location = new System.Drawing.Point(710, 123);
            this.btnAbrirAnexos.Name = "btnAbrirAnexos";
            this.btnAbrirAnexos.Size = new System.Drawing.Size(130, 30);
            this.btnAbrirAnexos.TabIndex = 7;
            this.btnAbrirAnexos.Text = "🗂️ Abrir Pasta";
            this.btnAbrirAnexos.UseVisualStyleBackColor = false;
            this.btnAbrirAnexos.Click += new System.EventHandler(this.BtnAbrirAnexos_Click);
            // 
            // lblTemplates
            // 
            this.lblTemplates.AutoSize = true;
            this.lblTemplates.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTemplates.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTemplates.Location = new System.Drawing.Point(20, 180);
            this.lblTemplates.Name = "lblTemplates";
            this.lblTemplates.Size = new System.Drawing.Size(246, 20);
            this.lblTemplates.TabIndex = 8;
            this.lblTemplates.Text = "📄 TEMPLATES DE MALA DIRETA:";
            // 
            // txtTemplates
            // 
            this.txtTemplates.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTemplates.Location = new System.Drawing.Point(20, 205);
            this.txtTemplates.Name = "txtTemplates";
            this.txtTemplates.ReadOnly = true;
            this.txtTemplates.Size = new System.Drawing.Size(540, 25);
            this.txtTemplates.TabIndex = 9;
            // 
            // btnEscolherTemplates
            // 
            this.btnEscolherTemplates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnEscolherTemplates.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEscolherTemplates.FlatAppearance.BorderSize = 0;
            this.btnEscolherTemplates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEscolherTemplates.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEscolherTemplates.ForeColor = System.Drawing.Color.White;
            this.btnEscolherTemplates.Location = new System.Drawing.Point(570, 203);
            this.btnEscolherTemplates.Name = "btnEscolherTemplates";
            this.btnEscolherTemplates.Size = new System.Drawing.Size(130, 30);
            this.btnEscolherTemplates.TabIndex = 10;
            this.btnEscolherTemplates.Text = "📁 Escolher";
            this.btnEscolherTemplates.UseVisualStyleBackColor = false;
            this.btnEscolherTemplates.Click += new System.EventHandler(this.BtnEscolherTemplates_Click);
            // 
            // btnAbrirTemplates
            // 
            this.btnAbrirTemplates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnAbrirTemplates.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbrirTemplates.FlatAppearance.BorderSize = 0;
            this.btnAbrirTemplates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbrirTemplates.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAbrirTemplates.ForeColor = System.Drawing.Color.White;
            this.btnAbrirTemplates.Location = new System.Drawing.Point(710, 203);
            this.btnAbrirTemplates.Name = "btnAbrirTemplates";
            this.btnAbrirTemplates.Size = new System.Drawing.Size(130, 30);
            this.btnAbrirTemplates.TabIndex = 11;
            this.btnAbrirTemplates.Text = "🗂️ Abrir Pasta";
            this.btnAbrirTemplates.UseVisualStyleBackColor = false;
            this.btnAbrirTemplates.Click += new System.EventHandler(this.BtnAbrirTemplates_Click);
            // 
            // lblPDFs
            // 
            this.lblPDFs.AutoSize = true;
            this.lblPDFs.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPDFs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblPDFs.Location = new System.Drawing.Point(20, 260);
            this.lblPDFs.Name = "lblPDFs";
            this.lblPDFs.Size = new System.Drawing.Size(200, 20);
            this.lblPDFs.TabIndex = 12;
            this.lblPDFs.Text = "📮 PDFs DE MALA DIRETA:";
            // 
            // txtPDFs
            // 
            this.txtPDFs.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPDFs.Location = new System.Drawing.Point(20, 285);
            this.txtPDFs.Name = "txtPDFs";
            this.txtPDFs.ReadOnly = true;
            this.txtPDFs.Size = new System.Drawing.Size(540, 25);
            this.txtPDFs.TabIndex = 13;
            // 
            // btnEscolherPDFs
            // 
            this.btnEscolherPDFs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnEscolherPDFs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEscolherPDFs.FlatAppearance.BorderSize = 0;
            this.btnEscolherPDFs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEscolherPDFs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEscolherPDFs.ForeColor = System.Drawing.Color.White;
            this.btnEscolherPDFs.Location = new System.Drawing.Point(570, 283);
            this.btnEscolherPDFs.Name = "btnEscolherPDFs";
            this.btnEscolherPDFs.Size = new System.Drawing.Size(130, 30);
            this.btnEscolherPDFs.TabIndex = 14;
            this.btnEscolherPDFs.Text = "📁 Escolher";
            this.btnEscolherPDFs.UseVisualStyleBackColor = false;
            this.btnEscolherPDFs.Click += new System.EventHandler(this.BtnEscolherPDFs_Click);
            // 
            // btnAbrirPDFs
            // 
            this.btnAbrirPDFs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnAbrirPDFs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbrirPDFs.FlatAppearance.BorderSize = 0;
            this.btnAbrirPDFs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbrirPDFs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAbrirPDFs.ForeColor = System.Drawing.Color.White;
            this.btnAbrirPDFs.Location = new System.Drawing.Point(710, 283);
            this.btnAbrirPDFs.Name = "btnAbrirPDFs";
            this.btnAbrirPDFs.Size = new System.Drawing.Size(130, 30);
            this.btnAbrirPDFs.TabIndex = 15;
            this.btnAbrirPDFs.Text = "🗂️ Abrir Pasta";
            this.btnAbrirPDFs.UseVisualStyleBackColor = false;
            this.btnAbrirPDFs.Click += new System.EventHandler(this.BtnAbrirPDFs_Click);
            // 
            // lblLogs
            // 
            this.lblLogs.AutoSize = true;
            this.lblLogs.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblLogs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblLogs.Location = new System.Drawing.Point(20, 340);
            this.lblLogs.Name = "lblLogs";
            this.lblLogs.Size = new System.Drawing.Size(170, 20);
            this.lblLogs.TabIndex = 16;
            this.lblLogs.Text = "📝 LOGS DO SISTEMA:";
            // 
            // txtLogs
            // 
            this.txtLogs.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLogs.Location = new System.Drawing.Point(20, 365);
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.ReadOnly = true;
            this.txtLogs.Size = new System.Drawing.Size(540, 25);
            this.txtLogs.TabIndex = 17;
            // 
            // btnEscolherLogs
            // 
            this.btnEscolherLogs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnEscolherLogs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEscolherLogs.FlatAppearance.BorderSize = 0;
            this.btnEscolherLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEscolherLogs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEscolherLogs.ForeColor = System.Drawing.Color.White;
            this.btnEscolherLogs.Location = new System.Drawing.Point(570, 363);
            this.btnEscolherLogs.Name = "btnEscolherLogs";
            this.btnEscolherLogs.Size = new System.Drawing.Size(130, 30);
            this.btnEscolherLogs.TabIndex = 18;
            this.btnEscolherLogs.Text = "📁 Escolher";
            this.btnEscolherLogs.UseVisualStyleBackColor = false;
            this.btnEscolherLogs.Click += new System.EventHandler(this.BtnEscolherLogs_Click);
            // 
            // btnAbrirLogs
            // 
            this.btnAbrirLogs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnAbrirLogs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbrirLogs.FlatAppearance.BorderSize = 0;
            this.btnAbrirLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbrirLogs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAbrirLogs.ForeColor = System.Drawing.Color.White;
            this.btnAbrirLogs.Location = new System.Drawing.Point(710, 363);
            this.btnAbrirLogs.Name = "btnAbrirLogs";
            this.btnAbrirLogs.Size = new System.Drawing.Size(130, 30);
            this.btnAbrirLogs.TabIndex = 19;
            this.btnAbrirLogs.Text = "🗂️ Abrir Pasta";
            this.btnAbrirLogs.UseVisualStyleBackColor = false;
            this.btnAbrirLogs.Click += new System.EventHandler(this.BtnAbrirLogs_Click);
            // 
            // lblLogsSms
            // 
            this.lblLogsSms.AutoSize = true;
            this.lblLogsSms.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblLogsSms.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblLogsSms.Location = new System.Drawing.Point(20, 420);
            this.lblLogsSms.Name = "lblLogsSms";
            this.lblLogsSms.Size = new System.Drawing.Size(127, 20);
            this.lblLogsSms.TabIndex = 20;
            this.lblLogsSms.Text = "📱 LOGS DE SMS:";
            // 
            // txtLogsSms
            // 
            this.txtLogsSms.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLogsSms.Location = new System.Drawing.Point(20, 445);
            this.txtLogsSms.Name = "txtLogsSms";
            this.txtLogsSms.ReadOnly = true;
            this.txtLogsSms.Size = new System.Drawing.Size(540, 25);
            this.txtLogsSms.TabIndex = 21;
            // 
            // btnEscolherLogsSms
            // 
            this.btnEscolherLogsSms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnEscolherLogsSms.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEscolherLogsSms.FlatAppearance.BorderSize = 0;
            this.btnEscolherLogsSms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEscolherLogsSms.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEscolherLogsSms.ForeColor = System.Drawing.Color.White;
            this.btnEscolherLogsSms.Location = new System.Drawing.Point(570, 443);
            this.btnEscolherLogsSms.Name = "btnEscolherLogsSms";
            this.btnEscolherLogsSms.Size = new System.Drawing.Size(130, 30);
            this.btnEscolherLogsSms.TabIndex = 22;
            this.btnEscolherLogsSms.Text = "📁 Escolher";
            this.btnEscolherLogsSms.UseVisualStyleBackColor = false;
            this.btnEscolherLogsSms.Click += new System.EventHandler(this.BtnEscolherLogsSms_Click);
            // 
            // btnAbrirLogsSms
            // 
            this.btnAbrirLogsSms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnAbrirLogsSms.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbrirLogsSms.FlatAppearance.BorderSize = 0;
            this.btnAbrirLogsSms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbrirLogsSms.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAbrirLogsSms.ForeColor = System.Drawing.Color.White;
            this.btnAbrirLogsSms.Location = new System.Drawing.Point(710, 443);
            this.btnAbrirLogsSms.Name = "btnAbrirLogsSms";
            this.btnAbrirLogsSms.Size = new System.Drawing.Size(130, 30);
            this.btnAbrirLogsSms.TabIndex = 23;
            this.btnAbrirLogsSms.Text = "🗂️ Abrir Pasta";
            this.btnAbrirLogsSms.UseVisualStyleBackColor = false;
            this.btnAbrirLogsSms.Click += new System.EventHandler(this.BtnAbrirLogsSms_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(150, 680);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(200, 45);
            this.btnSalvar.TabIndex = 24;
            this.btnSalvar.Text = "✔️ SALVAR TUDO";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.BtnSalvar_Click);
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnRestaurar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestaurar.FlatAppearance.BorderSize = 0;
            this.btnRestaurar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestaurar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRestaurar.ForeColor = System.Drawing.Color.White;
            this.btnRestaurar.Location = new System.Drawing.Point(370, 680);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(200, 45);
            this.btnRestaurar.TabIndex = 25;
            this.btnRestaurar.Text = "🔄 RESTAURAR PADRÃO";
            this.btnRestaurar.UseVisualStyleBackColor = false;
            this.btnRestaurar.Click += new System.EventHandler(this.BtnRestaurar_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(590, 680);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(160, 45);
            this.btnFechar.TabIndex = 26;
            this.btnFechar.Text = "✖️ FECHAR";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.BtnFechar_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(811, 705);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 27;
            this.button1.Text = "diretorio";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormConfigurarDiretorios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 750);
            this.Controls.Add(this.panelContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfigurarDiretorios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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

        // Logs SMS (NOVOS CONTROLES ADICIONADOS)
        private Label lblLogsSms;
        private TextBox txtLogsSms;
        private Button btnEscolherLogsSms;
        private Button btnAbrirLogsSms;

        // Botões de ação
        private Button btnSalvar;
        private Button btnRestaurar;
        private Button btnFechar;
        private Button button1;
    }
}