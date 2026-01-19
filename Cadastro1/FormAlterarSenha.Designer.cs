namespace Cadastro1
{
    partial class FormAlterarSenha
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblSenhaAtual = new System.Windows.Forms.Label();
            this.txtSenhaAtual = new System.Windows.Forms.TextBox();
            this.lblNovaSenha = new System.Windows.Forms.Label();
            this.txtNovaSenha = new System.Windows.Forms.TextBox();
            this.lblConfirmarNovaSenha = new System.Windows.Forms.Label();
            this.txtConfirmarNovaSenha = new System.Windows.Forms.TextBox();
            this.lblDicas = new System.Windows.Forms.Label();
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
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(500, 550);
            this.panelContainer.TabIndex = 0;
            // 
            // panelForm
            // 
            this.panelForm.BackColor = System.Drawing.Color.White;
            this.panelForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelForm.Controls.Add(this.lblTitulo);
            this.panelForm.Controls.Add(this.lblInfo);
            this.panelForm.Controls.Add(this.lblSenhaAtual);
            this.panelForm.Controls.Add(this.txtSenhaAtual);
            this.panelForm.Controls.Add(this.lblNovaSenha);
            this.panelForm.Controls.Add(this.txtNovaSenha);
            this.panelForm.Controls.Add(this.lblConfirmarNovaSenha);
            this.panelForm.Controls.Add(this.txtConfirmarNovaSenha);
            this.panelForm.Controls.Add(this.lblDicas);
            this.panelForm.Controls.Add(this.btnSalvar);
            this.panelForm.Controls.Add(this.btnCancelar);
            this.panelForm.Location = new System.Drawing.Point(25, 25);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(450, 500);
            this.panelForm.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(410, 35);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "🔑 ALTERAR SENHA";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.lblInfo.Location = new System.Drawing.Point(30, 65);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(390, 30);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "Usuário: {UsuarioLogado}";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSenhaAtual
            // 
            this.lblSenhaAtual.AutoSize = true;
            this.lblSenhaAtual.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSenhaAtual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblSenhaAtual.Location = new System.Drawing.Point(30, 110);
            this.lblSenhaAtual.Name = "lblSenhaAtual";
            this.lblSenhaAtual.Size = new System.Drawing.Size(118, 20);
            this.lblSenhaAtual.TabIndex = 2;
            this.lblSenhaAtual.Text = "SENHA ATUAL:";
            // 
            // txtSenhaAtual
            // 
            this.txtSenhaAtual.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtSenhaAtual.Location = new System.Drawing.Point(30, 135);
            this.txtSenhaAtual.Name = "txtSenhaAtual";
            this.txtSenhaAtual.PasswordChar = '●';
            this.txtSenhaAtual.Size = new System.Drawing.Size(390, 29);
            this.txtSenhaAtual.TabIndex = 0;
            // 
            // lblNovaSenha
            // 
            this.lblNovaSenha.AutoSize = true;
            this.lblNovaSenha.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblNovaSenha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblNovaSenha.Location = new System.Drawing.Point(30, 185);
            this.lblNovaSenha.Name = "lblNovaSenha";
            this.lblNovaSenha.Size = new System.Drawing.Size(113, 20);
            this.lblNovaSenha.TabIndex = 4;
            this.lblNovaSenha.Text = "NOVA SENHA:";
            // 
            // txtNovaSenha
            // 
            this.txtNovaSenha.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtNovaSenha.Location = new System.Drawing.Point(30, 210);
            this.txtNovaSenha.Name = "txtNovaSenha";
            this.txtNovaSenha.PasswordChar = '●';
            this.txtNovaSenha.Size = new System.Drawing.Size(390, 29);
            this.txtNovaSenha.TabIndex = 1;
            // 
            // lblConfirmarNovaSenha
            // 
            this.lblConfirmarNovaSenha.AutoSize = true;
            this.lblConfirmarNovaSenha.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblConfirmarNovaSenha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblConfirmarNovaSenha.Location = new System.Drawing.Point(30, 260);
            this.lblConfirmarNovaSenha.Name = "lblConfirmarNovaSenha";
            this.lblConfirmarNovaSenha.Size = new System.Drawing.Size(208, 20);
            this.lblConfirmarNovaSenha.TabIndex = 6;
            this.lblConfirmarNovaSenha.Text = "CONFIRMAR NOVA SENHA:";
            // 
            // txtConfirmarNovaSenha
            // 
            this.txtConfirmarNovaSenha.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtConfirmarNovaSenha.Location = new System.Drawing.Point(30, 285);
            this.txtConfirmarNovaSenha.Name = "txtConfirmarNovaSenha";
            this.txtConfirmarNovaSenha.PasswordChar = '●';
            this.txtConfirmarNovaSenha.Size = new System.Drawing.Size(390, 29);
            this.txtConfirmarNovaSenha.TabIndex = 2;
            // 
            // lblDicas
            // 
            this.lblDicas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblDicas.ForeColor = System.Drawing.Color.Gray;
            this.lblDicas.Location = new System.Drawing.Point(30, 325);
            this.lblDicas.Name = "lblDicas";
            this.lblDicas.Size = new System.Drawing.Size(390, 50);
            this.lblDicas.TabIndex = 8;
            this.lblDicas.Text = "💡 A nova senha deve ter no mínimo 6 caracteres,\ncom pelo menos uma letra e um n" +
    "úmero.\nExemplo: NovaSenha123";
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(30, 395);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(185, 45);
            this.btnSalvar.TabIndex = 3;
            this.btnSalvar.Text = "✔ SALVAR";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.BtnSalvar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(235, 395);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(185, 45);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "✖ CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // FormAlterarSenha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 550);
            this.Controls.Add(this.panelContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAlterarSenha";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Alterar Senha";
            this.panelContainer.ResumeLayout(false);
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblSenhaAtual;
        private System.Windows.Forms.TextBox txtSenhaAtual;
        private System.Windows.Forms.Label lblNovaSenha;
        private System.Windows.Forms.TextBox txtNovaSenha;
        private System.Windows.Forms.Label lblConfirmarNovaSenha;
        private System.Windows.Forms.TextBox txtConfirmarNovaSenha;
        private System.Windows.Forms.Label lblDicas;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
    }
}