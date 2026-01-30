namespace Cadastro1
{
    partial class FormConfiguracaoPastas
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
            this.panelContainer = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnRestaurarPadrao = new System.Windows.Forms.Button();
            this.btnTestarPermissoes = new System.Windows.Forms.Button();
            this.panelAjuda = new System.Windows.Forms.Panel();
            this.txtAjuda = new System.Windows.Forms.RichTextBox();
            this.lblAjudaTitulo = new System.Windows.Forms.Label();
            this.panelAnexos = new System.Windows.Forms.Panel();
            this.lblStatusAnexos = new System.Windows.Forms.Label();
            this.btnEscolherAnexos = new System.Windows.Forms.Button();
            this.txtPastaAnexos = new System.Windows.Forms.TextBox();
            this.lblAnexos = new System.Windows.Forms.Label();
            this.panelBackups = new System.Windows.Forms.Panel();
            this.lblStatusBackups = new System.Windows.Forms.Label();
            this.btnEscolherBackups = new System.Windows.Forms.Button();
            this.txtPastaBackups = new System.Windows.Forms.TextBox();
            this.lblBackups = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelContainer.SuspendLayout();
            this.panelAjuda.SuspendLayout();
            this.panelAnexos.SuspendLayout();
            this.panelBackups.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelContainer.Controls.Add(this.btnCancelar);
            this.panelContainer.Controls.Add(this.btnSalvar);
            this.panelContainer.Controls.Add(this.btnRestaurarPadrao);
            this.panelContainer.Controls.Add(this.btnTestarPermissoes);
            this.panelContainer.Controls.Add(this.panelAjuda);
            this.panelContainer.Controls.Add(this.panelAnexos);
            this.panelContainer.Controls.Add(this.panelBackups);
            this.panelContainer.Controls.Add(this.lblTitulo);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(800, 650);
            this.panelContainer.TabIndex = 0;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(630, 535);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(130, 45);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "❌ Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(490, 535);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(130, 45);
            this.btnSalvar.TabIndex = 6;
            this.btnSalvar.Text = "✅ SALVAR";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.BtnSalvar_Click);
            // 
            // btnRestaurarPadrao
            // 
            this.btnRestaurarPadrao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnRestaurarPadrao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestaurarPadrao.FlatAppearance.BorderSize = 0;
            this.btnRestaurarPadrao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestaurarPadrao.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRestaurarPadrao.ForeColor = System.Drawing.Color.White;
            this.btnRestaurarPadrao.Location = new System.Drawing.Point(235, 535);
            this.btnRestaurarPadrao.Name = "btnRestaurarPadrao";
            this.btnRestaurarPadrao.Size = new System.Drawing.Size(160, 45);
            this.btnRestaurarPadrao.TabIndex = 5;
            this.btnRestaurarPadrao.Text = "🔄 Restaurar Padrão";
            this.btnRestaurarPadrao.UseVisualStyleBackColor = false;
            this.btnRestaurarPadrao.Click += new System.EventHandler(this.BtnRestaurarPadrao_Click);
            // 
            // btnTestarPermissoes
            // 
            this.btnTestarPermissoes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnTestarPermissoes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTestarPermissoes.FlatAppearance.BorderSize = 0;
            this.btnTestarPermissoes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestarPermissoes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTestarPermissoes.ForeColor = System.Drawing.Color.White;
            this.btnTestarPermissoes.Location = new System.Drawing.Point(20, 535);
            this.btnTestarPermissoes.Name = "btnTestarPermissoes";
            this.btnTestarPermissoes.Size = new System.Drawing.Size(200, 45);
            this.btnTestarPermissoes.TabIndex = 4;
            this.btnTestarPermissoes.Text = "🔍 TESTAR PERMISSÕES SQL";
            this.btnTestarPermissoes.UseVisualStyleBackColor = false;
            this.btnTestarPermissoes.Click += new System.EventHandler(this.BtnTestarPermissoes_Click);
            // 
            // panelAjuda
            // 
            this.panelAjuda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(243)))), ((int)(((byte)(205)))));
            this.panelAjuda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAjuda.Controls.Add(this.txtAjuda);
            this.panelAjuda.Controls.Add(this.lblAjudaTitulo);
            this.panelAjuda.Location = new System.Drawing.Point(20, 340);
            this.panelAjuda.Name = "panelAjuda";
            this.panelAjuda.Size = new System.Drawing.Size(740, 180);
            this.panelAjuda.TabIndex = 3;
            // 
            // txtAjuda
            // 
            this.txtAjuda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(243)))), ((int)(((byte)(205)))));
            this.txtAjuda.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAjuda.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAjuda.Location = new System.Drawing.Point(10, 35);
            this.txtAjuda.Name = "txtAjuda";
            this.txtAjuda.ReadOnly = true;
            this.txtAjuda.Size = new System.Drawing.Size(710, 135);
            this.txtAjuda.TabIndex = 1;
            this.txtAjuda.Text = "⚠️ O SQL Server precisa de permissão para gravar na pasta de backups!\n\nSOLUÇÃO R" +
    "ECOMENDADA:\n1. Use a pasta padrão (Documentos)\n2. Clique em \'Testar Permissões " +
    "SQL\' para configurar automaticamente\n\nSe escolher outra pasta:\n• A conta NT SER" +
    "VICE\\MSSQLSERVER precisa ter permissão de escrita\n• O sistema tentará configur" +
    "ar automaticamente";
            // 
            // lblAjudaTitulo
            // 
            this.lblAjudaTitulo.AutoSize = true;
            this.lblAjudaTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAjudaTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(100)))), ((int)(((byte)(4)))));
            this.lblAjudaTitulo.Location = new System.Drawing.Point(10, 5);
            this.lblAjudaTitulo.Name = "lblAjudaTitulo";
            this.lblAjudaTitulo.Size = new System.Drawing.Size(282, 19);
            this.lblAjudaTitulo.TabIndex = 0;
            this.lblAjudaTitulo.Text = "ℹ️ IMPORTANTE - Permissões SQL Server";
            // 
            // panelAnexos
            // 
            this.panelAnexos.BackColor = System.Drawing.Color.White;
            this.panelAnexos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAnexos.Controls.Add(this.lblStatusAnexos);
            this.panelAnexos.Controls.Add(this.btnEscolherAnexos);
            this.panelAnexos.Controls.Add(this.txtPastaAnexos);
            this.panelAnexos.Controls.Add(this.lblAnexos);
            this.panelAnexos.Location = new System.Drawing.Point(20, 210);
            this.panelAnexos.Name = "panelAnexos";
            this.panelAnexos.Size = new System.Drawing.Size(740, 120);
            this.panelAnexos.TabIndex = 2;
            // 
            // lblStatusAnexos
            // 
            this.lblStatusAnexos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatusAnexos.ForeColor = System.Drawing.Color.Gray;
            this.lblStatusAnexos.Location = new System.Drawing.Point(10, 75);
            this.lblStatusAnexos.Name = "lblStatusAnexos";
            this.lblStatusAnexos.Size = new System.Drawing.Size(710, 35);
            this.lblStatusAnexos.TabIndex = 3;
            this.lblStatusAnexos.Text = "Status: Aguardando configuração...";
            // 
            // btnEscolherAnexos
            // 
            this.btnEscolherAnexos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnEscolherAnexos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEscolherAnexos.FlatAppearance.BorderSize = 0;
            this.btnEscolherAnexos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEscolherAnexos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEscolherAnexos.ForeColor = System.Drawing.Color.White;
            this.btnEscolherAnexos.Location = new System.Drawing.Point(650, 38);
            this.btnEscolherAnexos.Name = "btnEscolherAnexos";
            this.btnEscolherAnexos.Size = new System.Drawing.Size(70, 28);
            this.btnEscolherAnexos.TabIndex = 2;
            this.btnEscolherAnexos.Text = "...";
            this.btnEscolherAnexos.UseVisualStyleBackColor = false;
            this.btnEscolherAnexos.Click += new System.EventHandler(this.BtnEscolherAnexos_Click);
            // 
            // txtPastaAnexos
            // 
            this.txtPastaAnexos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPastaAnexos.Location = new System.Drawing.Point(10, 40);
            this.txtPastaAnexos.Name = "txtPastaAnexos";
            this.txtPastaAnexos.ReadOnly = true;
            this.txtPastaAnexos.Size = new System.Drawing.Size(630, 25);
            this.txtPastaAnexos.TabIndex = 1;
            // 
            // lblAnexos
            // 
            this.lblAnexos.AutoSize = true;
            this.lblAnexos.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAnexos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.lblAnexos.Location = new System.Drawing.Point(10, 10);
            this.lblAnexos.Name = "lblAnexos";
            this.lblAnexos.Size = new System.Drawing.Size(159, 20);
            this.lblAnexos.TabIndex = 0;
            this.lblAnexos.Text = "📎 PASTA DE ANEXOS";
            // 
            // panelBackups
            // 
            this.panelBackups.BackColor = System.Drawing.Color.White;
            this.panelBackups.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBackups.Controls.Add(this.lblStatusBackups);
            this.panelBackups.Controls.Add(this.btnEscolherBackups);
            this.panelBackups.Controls.Add(this.txtPastaBackups);
            this.panelBackups.Controls.Add(this.lblBackups);
            this.panelBackups.Location = new System.Drawing.Point(20, 80);
            this.panelBackups.Name = "panelBackups";
            this.panelBackups.Size = new System.Drawing.Size(740, 120);
            this.panelBackups.TabIndex = 1;
            // 
            // lblStatusBackups
            // 
            this.lblStatusBackups.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatusBackups.ForeColor = System.Drawing.Color.Gray;
            this.lblStatusBackups.Location = new System.Drawing.Point(10, 75);
            this.lblStatusBackups.Name = "lblStatusBackups";
            this.lblStatusBackups.Size = new System.Drawing.Size(710, 35);
            this.lblStatusBackups.TabIndex = 3;
            this.lblStatusBackups.Text = "Status: Aguardando configuração...";
            // 
            // btnEscolherBackups
            // 
            this.btnEscolherBackups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnEscolherBackups.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEscolherBackups.FlatAppearance.BorderSize = 0;
            this.btnEscolherBackups.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEscolherBackups.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEscolherBackups.ForeColor = System.Drawing.Color.White;
            this.btnEscolherBackups.Location = new System.Drawing.Point(650, 38);
            this.btnEscolherBackups.Name = "btnEscolherBackups";
            this.btnEscolherBackups.Size = new System.Drawing.Size(70, 28);
            this.btnEscolherBackups.TabIndex = 2;
            this.btnEscolherBackups.Text = "...";
            this.btnEscolherBackups.UseVisualStyleBackColor = false;
            this.btnEscolherBackups.Click += new System.EventHandler(this.BtnEscolherBackups_Click);
            // 
            // txtPastaBackups
            // 
            this.txtPastaBackups.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPastaBackups.Location = new System.Drawing.Point(10, 40);
            this.txtPastaBackups.Name = "txtPastaBackups";
            this.txtPastaBackups.ReadOnly = true;
            this.txtPastaBackups.Size = new System.Drawing.Size(630, 25);
            this.txtPastaBackups.TabIndex = 1;
            // 
            // lblBackups
            // 
            this.lblBackups.AutoSize = true;
            this.lblBackups.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblBackups.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblBackups.Location = new System.Drawing.Point(10, 10);
            this.lblBackups.Name = "lblBackups";
            this.lblBackups.Size = new System.Drawing.Size(165, 20);
            this.lblBackups.TabIndex = 0;
            this.lblBackups.Text = "💾 PASTA DE BACKUPS";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(760, 40);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "⚙️ CONFIGURAÇÃO DE PASTAS DO SISTEMA";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormConfiguracaoPastas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 650);
            this.Controls.Add(this.panelContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfiguracaoPastas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuração de Pastas do Sistema";
            this.panelContainer.ResumeLayout(false);
            this.panelAjuda.ResumeLayout(false);
            this.panelAjuda.PerformLayout();
            this.panelAnexos.ResumeLayout(false);
            this.panelAnexos.PerformLayout();
            this.panelBackups.ResumeLayout(false);
            this.panelBackups.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnRestaurarPadrao;
        private System.Windows.Forms.Button btnTestarPermissoes;
        private System.Windows.Forms.Panel panelAjuda;
        private System.Windows.Forms.RichTextBox txtAjuda;
        private System.Windows.Forms.Label lblAjudaTitulo;
        private System.Windows.Forms.Panel panelAnexos;
        private System.Windows.Forms.Label lblStatusAnexos;
        private System.Windows.Forms.Button btnEscolherAnexos;
        private System.Windows.Forms.TextBox txtPastaAnexos;
        private System.Windows.Forms.Label lblAnexos;
        private System.Windows.Forms.Panel panelBackups;
        private System.Windows.Forms.Label lblStatusBackups;
        private System.Windows.Forms.Button btnEscolherBackups;
        private System.Windows.Forms.TextBox txtPastaBackups;
        private System.Windows.Forms.Label lblBackups;
        private System.Windows.Forms.Label lblTitulo;
    }
}