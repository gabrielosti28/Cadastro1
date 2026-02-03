namespace Cadastro1
{
    partial class FormSelecionarCidade
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lstCidades = new System.Windows.Forms.ListBox();
            this.lblContagem = new System.Windows.Forms.Label();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnVerTodas = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTitulo.Location = new System.Drawing.Point(150, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(273, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "🏙️ SELECIONE A CIDADE";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(50, 60);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(489, 20);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Escolha qual cidade você deseja visualizar os clientes ou clique em VER TODAS:";
            this.lblSubtitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstCidades
            // 
            this.lstCidades.BackColor = System.Drawing.Color.White;
            this.lstCidades.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstCidades.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lstCidades.ItemHeight = 21;
            this.lstCidades.Location = new System.Drawing.Point(50, 100);
            this.lstCidades.Name = "lstCidades";
            this.lstCidades.Size = new System.Drawing.Size(500, 296);
            this.lstCidades.TabIndex = 2;
            this.lstCidades.SelectionMode = System.Windows.Forms.SelectionMode.One;
            // 
            // lblContagem
            // 
            this.lblContagem.AutoSize = true;
            this.lblContagem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblContagem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblContagem.Location = new System.Drawing.Point(50, 410);
            this.lblContagem.Name = "lblContagem";
            this.lblContagem.Size = new System.Drawing.Size(181, 19);
            this.lblContagem.TabIndex = 3;
            this.lblContagem.Text = "Total de cidades cadastradas:";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnConfirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmar.FlatAppearance.BorderSize = 0;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnConfirmar.ForeColor = System.Drawing.Color.White;
            this.btnConfirmar.Location = new System.Drawing.Point(50, 450);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(160, 45);
            this.btnConfirmar.TabIndex = 4;
            this.btnConfirmar.Text = "✓ CONFIRMAR";
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Enabled = false;
            // 
            // btnVerTodas
            // 
            this.btnVerTodas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnVerTodas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerTodas.FlatAppearance.BorderSize = 0;
            this.btnVerTodas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerTodas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnVerTodas.ForeColor = System.Drawing.Color.White;
            this.btnVerTodas.Location = new System.Drawing.Point(220, 450);
            this.btnVerTodas.Name = "btnVerTodas";
            this.btnVerTodas.Size = new System.Drawing.Size(160, 45);
            this.btnVerTodas.TabIndex = 5;
            this.btnVerTodas.Text = "🌍 VER TODAS";
            this.btnVerTodas.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(390, 450);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(160, 45);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "✖ CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // FormSelecionarCidade
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(600, 520);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnVerTodas);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.lblContagem);
            this.Controls.Add(this.lstCidades);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSelecionarCidade";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Selecionar Cidade";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.ListBox lstCidades;
        private System.Windows.Forms.Label lblContagem;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnVerTodas;
        private System.Windows.Forms.Button btnCancelar;
    }
}