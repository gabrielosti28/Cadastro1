namespace Cadastro1
{
    partial class FormCadastroCliente
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
            this.panelForm = new System.Windows.Forms.Panel();
            this.txtCidade = new System.Windows.Forms.TextBox();
            this.lblCidade = new System.Windows.Forms.Label();
            this.txtEndereco = new System.Windows.Forms.TextBox();
            this.lblEndereco = new System.Windows.Forms.Label();
            this.dtpDataNasc = new System.Windows.Forms.DateTimePicker();
            this.lblDataNasc = new System.Windows.Forms.Label();
            this.txtINSS = new System.Windows.Forms.MaskedTextBox();
            this.lblDicaINSS = new System.Windows.Forms.Label();
            this.lblINSS = new System.Windows.Forms.Label();
            this.txtCPF = new System.Windows.Forms.MaskedTextBox();
            this.lblDicaCPF = new System.Windows.Forms.Label();
            this.lblCPF = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panelContainer.SuspendLayout();
            this.panelForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelContainer.Controls.Add(this.panelForm);
            this.panelContainer.Controls.Add(this.lblTitulo);
            this.panelContainer.Controls.Add(this.btnSalvar);
            this.panelContainer.Controls.Add(this.btnCancelar);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(769, 672);
            this.panelContainer.TabIndex = 0;
            // 
            // panelForm
            // 
            this.panelForm.BackColor = System.Drawing.Color.White;
            this.panelForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelForm.Controls.Add(this.txtCidade);
            this.panelForm.Controls.Add(this.lblCidade);
            this.panelForm.Controls.Add(this.txtEndereco);
            this.panelForm.Controls.Add(this.lblEndereco);
            this.panelForm.Controls.Add(this.dtpDataNasc);
            this.panelForm.Controls.Add(this.lblDataNasc);
            this.panelForm.Controls.Add(this.txtINSS);
            this.panelForm.Controls.Add(this.lblDicaINSS);
            this.panelForm.Controls.Add(this.lblINSS);
            this.panelForm.Controls.Add(this.txtCPF);
            this.panelForm.Controls.Add(this.lblDicaCPF);
            this.panelForm.Controls.Add(this.lblCPF);
            this.panelForm.Controls.Add(this.txtNome);
            this.panelForm.Controls.Add(this.lblNome);
            this.panelForm.Location = new System.Drawing.Point(50, 74);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(650, 493);
            this.panelForm.TabIndex = 1;
            // 
            // txtCidade
            // 
            this.txtCidade.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtCidade.Location = new System.Drawing.Point(40, 350);
            this.txtCidade.Name = "txtCidade";
            this.txtCidade.Size = new System.Drawing.Size(400, 32);
            this.txtCidade.TabIndex = 4;
            // 
            // lblCidade
            // 
            this.lblCidade.AutoSize = true;
            this.lblCidade.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCidade.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblCidade.Location = new System.Drawing.Point(36, 320);
            this.lblCidade.Name = "lblCidade";
            this.lblCidade.Size = new System.Drawing.Size(68, 20);
            this.lblCidade.TabIndex = 12;
            this.lblCidade.Text = "CIDADE:";
            // 
            // txtEndereco
            // 
            this.txtEndereco.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtEndereco.Location = new System.Drawing.Point(40, 270);
            this.txtEndereco.Name = "txtEndereco";
            this.txtEndereco.Size = new System.Drawing.Size(500, 32);
            this.txtEndereco.TabIndex = 3;
            // 
            // lblEndereco
            // 
            this.lblEndereco.AutoSize = true;
            this.lblEndereco.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblEndereco.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblEndereco.Location = new System.Drawing.Point(36, 240);
            this.lblEndereco.Name = "lblEndereco";
            this.lblEndereco.Size = new System.Drawing.Size(172, 20);
            this.lblEndereco.TabIndex = 10;
            this.lblEndereco.Text = "ENDEREÇO COMPLETO:";
            // 
            // dtpDataNasc
            // 
            this.dtpDataNasc.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.dtpDataNasc.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataNasc.Location = new System.Drawing.Point(40, 190);
            this.dtpDataNasc.Name = "dtpDataNasc";
            this.dtpDataNasc.Size = new System.Drawing.Size(280, 32);
            this.dtpDataNasc.TabIndex = 2;
            // 
            // lblDataNasc
            // 
            this.lblDataNasc.AutoSize = true;
            this.lblDataNasc.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDataNasc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblDataNasc.Location = new System.Drawing.Point(36, 160);
            this.lblDataNasc.Name = "lblDataNasc";
            this.lblDataNasc.Size = new System.Drawing.Size(178, 20);
            this.lblDataNasc.TabIndex = 8;
            this.lblDataNasc.Text = "DATA DE NASCIMENTO:";
            // 
            // txtINSS
            // 
            this.txtINSS.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtINSS.Location = new System.Drawing.Point(40, 430);
            this.txtINSS.Mask = "0000000000";
            this.txtINSS.Name = "txtINSS";
            this.txtINSS.Size = new System.Drawing.Size(280, 32);
            this.txtINSS.TabIndex = 5;
            // 
            // lblDicaINSS
            // 
            this.lblDicaINSS.AutoSize = true;
            this.lblDicaINSS.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblDicaINSS.ForeColor = System.Drawing.Color.Gray;
            this.lblDicaINSS.Location = new System.Drawing.Point(330, 435);
            this.lblDicaINSS.Name = "lblDicaINSS";
            this.lblDicaINSS.Size = new System.Drawing.Size(102, 15);
            this.lblDicaINSS.TabIndex = 15;
            this.lblDicaINSS.Text = "Digite 10 números";
            // 
            // lblINSS
            // 
            this.lblINSS.AutoSize = true;
            this.lblINSS.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblINSS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblINSS.Location = new System.Drawing.Point(36, 400);
            this.lblINSS.Name = "lblINSS";
            this.lblINSS.Size = new System.Drawing.Size(126, 20);
            this.lblINSS.TabIndex = 6;
            this.lblINSS.Text = "BENEFÍCIO INSS:";
            // 
            // txtCPF
            // 
            this.txtCPF.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtCPF.Location = new System.Drawing.Point(40, 110);
            this.txtCPF.Mask = "00000000000";
            this.txtCPF.Name = "txtCPF";
            this.txtCPF.Size = new System.Drawing.Size(280, 32);
            this.txtCPF.TabIndex = 1;
            // 
            // lblDicaCPF
            // 
            this.lblDicaCPF.AutoSize = true;
            this.lblDicaCPF.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblDicaCPF.ForeColor = System.Drawing.Color.Gray;
            this.lblDicaCPF.Location = new System.Drawing.Point(330, 115);
            this.lblDicaCPF.Name = "lblDicaCPF";
            this.lblDicaCPF.Size = new System.Drawing.Size(102, 15);
            this.lblDicaCPF.TabIndex = 13;
            this.lblDicaCPF.Text = "Digite 11 números";
            // 
            // lblCPF
            // 
            this.lblCPF.AutoSize = true;
            this.lblCPF.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCPF.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblCPF.Location = new System.Drawing.Point(36, 80);
            this.lblCPF.Name = "lblCPF";
            this.lblCPF.Size = new System.Drawing.Size(182, 20);
            this.lblCPF.TabIndex = 4;
            this.lblCPF.Text = "CPF (somente números):";
            // 
            // txtNome
            // 
            this.txtNome.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtNome.Location = new System.Drawing.Point(40, 30);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(500, 32);
            this.txtNome.TabIndex = 0;
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblNome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblNome.Location = new System.Drawing.Point(36, 7);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(140, 20);
            this.lblNome.TabIndex = 0;
            this.lblNome.Text = "NOME COMPLETO:";
            this.lblNome.Click += new System.EventHandler(this.lblNome_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTitulo.Location = new System.Drawing.Point(150, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(421, 37);
            this.lblTitulo.TabIndex = 2;
            this.lblTitulo.Text = "➕ CADASTRAR NOVO CLIENTE";
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(103, 585);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(220, 55);
            this.btnSalvar.TabIndex = 6;
            this.btnSalvar.Text = "✔ SALVAR CLIENTE";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.BtnSalvar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(411, 585);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(180, 55);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "✖ CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // FormCadastroCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 672);
            this.Controls.Add(this.panelContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormCadastroCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastrar Novo Cliente";
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.TextBox txtCidade;
        private System.Windows.Forms.Label lblCidade;
        private System.Windows.Forms.TextBox txtEndereco;
        private System.Windows.Forms.Label lblEndereco;
        private System.Windows.Forms.DateTimePicker dtpDataNasc;
        private System.Windows.Forms.Label lblDataNasc;
        private System.Windows.Forms.MaskedTextBox txtINSS;
        private System.Windows.Forms.Label lblDicaINSS;
        private System.Windows.Forms.Label lblINSS;
        private System.Windows.Forms.MaskedTextBox txtCPF;
        private System.Windows.Forms.Label lblDicaCPF;
        private System.Windows.Forms.Label lblCPF;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
    }
}