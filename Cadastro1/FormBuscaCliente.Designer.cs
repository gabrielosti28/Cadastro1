namespace Cadastro1
{
    partial class FormBuscaCliente
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
            this.panelBusca = new System.Windows.Forms.Panel();
            this.lblCPF = new System.Windows.Forms.Label();
            this.txtCPFBusca = new System.Windows.Forms.MaskedTextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.panelResultado = new System.Windows.Forms.Panel();
            this.btnFechar = new System.Windows.Forms.Button();
            this.panelBusca.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTitulo.Location = new System.Drawing.Point(230, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(280, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "🔍 BUSCAR CLIENTE";
            // 
            // panelBusca
            // 
            this.panelBusca.BackColor = System.Drawing.Color.White;
            this.panelBusca.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBusca.Controls.Add(this.lblCPF);
            this.panelBusca.Controls.Add(this.txtCPFBusca);
            this.panelBusca.Controls.Add(this.btnBuscar);
            this.panelBusca.Location = new System.Drawing.Point(50, 80);
            this.panelBusca.Name = "panelBusca";
            this.panelBusca.Size = new System.Drawing.Size(650, 120);
            this.panelBusca.TabIndex = 1;
            // 
            // lblCPF
            // 
            this.lblCPF.AutoSize = true;
            this.lblCPF.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCPF.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblCPF.Location = new System.Drawing.Point(30, 20);
            this.lblCPF.Name = "lblCPF";
            this.lblCPF.Size = new System.Drawing.Size(212, 21);
            this.lblCPF.TabIndex = 0;
            this.lblCPF.Text = "Digite o CPF (11 números):";
            // 
            // txtCPFBusca
            // 
            this.txtCPFBusca.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.txtCPFBusca.Location = new System.Drawing.Point(30, 50);
            this.txtCPFBusca.Mask = "00000000000";
            this.txtCPFBusca.Name = "txtCPFBusca";
            this.txtCPFBusca.Size = new System.Drawing.Size(300, 36);
            this.txtCPFBusca.TabIndex = 1;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(350, 45);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(180, 45);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "🔍 BUSCAR";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            // 
            // panelResultado
            // 
            this.panelResultado.BackColor = System.Drawing.Color.White;
            this.panelResultado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelResultado.Location = new System.Drawing.Point(50, 220);
            this.panelResultado.Name = "panelResultado";
            this.panelResultado.Size = new System.Drawing.Size(650, 426);
            this.panelResultado.TabIndex = 2;
            this.panelResultado.Visible = false;
            // 
            // btnFechar
            // 
            this.btnFechar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(265, 652);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(180, 45);
            this.btnFechar.TabIndex = 3;
            this.btnFechar.Text = "✖ FECHAR";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.BtnFechar_Click);
            // 
            // FormBuscaCliente
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(823, 709);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.panelResultado);
            this.Controls.Add(this.panelBusca);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormBuscaCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buscar Cliente por CPF";
            this.panelBusca.ResumeLayout(false);
            this.panelBusca.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelBusca;
        private System.Windows.Forms.Label lblCPF;
        private System.Windows.Forms.MaskedTextBox txtCPFBusca;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Panel panelResultado;
        private System.Windows.Forms.Button btnFechar;
    }
}