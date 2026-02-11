// =============================================
// FORMULÁRIO - SELEÇÃO DE CIDADES PARA IMPORTAÇÃO
// Arquivo: FormSelecionarCidade.Designer.cs
// DESIGNER - CONTROLES VISUAIS
// =============================================
namespace Cadastro1
{
    partial class FormSelecionarCidadeImportacao
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
            this.panelPrincipal = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblInstrucoes = new System.Windows.Forms.Label();
            this.txtBusca = new System.Windows.Forms.TextBox();
            this.btnLimparBusca = new System.Windows.Forms.Button();
            this.lblContador = new System.Windows.Forms.Label();
            this.chkCidades = new System.Windows.Forms.CheckedListBox();
            this.panelAcoesRapidas = new System.Windows.Forms.Panel();
            this.btnMarcarTodas = new System.Windows.Forms.Button();
            this.btnDesmarcarTodas = new System.Windows.Forms.Button();
            this.btnInverter = new System.Windows.Forms.Button();
            this.panelBotoes = new System.Windows.Forms.Panel();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panelPrincipal.SuspendLayout();
            this.panelAcoesRapidas.SuspendLayout();
            this.panelBotoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelPrincipal
            // 
            this.panelPrincipal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelPrincipal.Controls.Add(this.lblTitulo);
            this.panelPrincipal.Controls.Add(this.lblInstrucoes);
            this.panelPrincipal.Controls.Add(this.txtBusca);
            this.panelPrincipal.Controls.Add(this.btnLimparBusca);
            this.panelPrincipal.Controls.Add(this.lblContador);
            this.panelPrincipal.Controls.Add(this.chkCidades);
            this.panelPrincipal.Controls.Add(this.panelAcoesRapidas);
            this.panelPrincipal.Controls.Add(this.panelBotoes);
            this.panelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipal.Location = new System.Drawing.Point(0, 0);
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.Padding = new System.Windows.Forms.Padding(20);
            this.panelPrincipal.Size = new System.Drawing.Size(600, 700);
            this.panelPrincipal.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(491, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "🏙️ SELECIONE AS CIDADES PARA IMPORTAR";
            // 
            // lblInstrucoes
            // 
            this.lblInstrucoes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblInstrucoes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblInstrucoes.Location = new System.Drawing.Point(20, 60);
            this.lblInstrucoes.Name = "lblInstrucoes";
            this.lblInstrucoes.Size = new System.Drawing.Size(540, 40);
            this.lblInstrucoes.TabIndex = 1;
            this.lblInstrucoes.Text = "Marque as cidades cujos clientes você deseja importar.\r\nApenas clientes das cidad" +
    "es selecionadas serão cadastrados.";
            // 
            // txtBusca
            // 
            this.txtBusca.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtBusca.Location = new System.Drawing.Point(20, 110);
            this.txtBusca.Name = "txtBusca";
            this.txtBusca.Size = new System.Drawing.Size(400, 27);
            this.txtBusca.TabIndex = 2;
            // 
            // btnLimparBusca
            // 
            this.btnLimparBusca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnLimparBusca.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimparBusca.FlatAppearance.BorderSize = 0;
            this.btnLimparBusca.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimparBusca.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLimparBusca.ForeColor = System.Drawing.Color.White;
            this.btnLimparBusca.Location = new System.Drawing.Point(430, 110);
            this.btnLimparBusca.Name = "btnLimparBusca";
            this.btnLimparBusca.Size = new System.Drawing.Size(100, 30);
            this.btnLimparBusca.TabIndex = 3;
            this.btnLimparBusca.Text = "✖ Limpar";
            this.btnLimparBusca.UseVisualStyleBackColor = false;
            // 
            // lblContador
            // 
            this.lblContador.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblContador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblContador.Location = new System.Drawing.Point(20, 150);
            this.lblContador.Name = "lblContador";
            this.lblContador.Size = new System.Drawing.Size(540, 25);
            this.lblContador.TabIndex = 4;
            this.lblContador.Text = "📊 0 cidades listadas | 0 selecionadas";
            // 
            // chkCidades
            // 
            this.chkCidades.BackColor = System.Drawing.Color.White;
            this.chkCidades.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chkCidades.CheckOnClick = true;
            this.chkCidades.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkCidades.FormattingEnabled = true;
            this.chkCidades.Location = new System.Drawing.Point(20, 185);
            this.chkCidades.Name = "chkCidades";
            this.chkCidades.Size = new System.Drawing.Size(540, 350);
            this.chkCidades.TabIndex = 5;
            // 
            // panelAcoesRapidas
            // 
            this.panelAcoesRapidas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelAcoesRapidas.Controls.Add(this.btnMarcarTodas);
            this.panelAcoesRapidas.Controls.Add(this.btnDesmarcarTodas);
            this.panelAcoesRapidas.Controls.Add(this.btnInverter);
            this.panelAcoesRapidas.Location = new System.Drawing.Point(20, 545);
            this.panelAcoesRapidas.Name = "panelAcoesRapidas";
            this.panelAcoesRapidas.Size = new System.Drawing.Size(540, 40);
            this.panelAcoesRapidas.TabIndex = 6;
            // 
            // btnMarcarTodas
            // 
            this.btnMarcarTodas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnMarcarTodas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMarcarTodas.FlatAppearance.BorderSize = 0;
            this.btnMarcarTodas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarcarTodas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnMarcarTodas.ForeColor = System.Drawing.Color.White;
            this.btnMarcarTodas.Location = new System.Drawing.Point(10, 5);
            this.btnMarcarTodas.Name = "btnMarcarTodas";
            this.btnMarcarTodas.Size = new System.Drawing.Size(160, 30);
            this.btnMarcarTodas.TabIndex = 0;
            this.btnMarcarTodas.Text = "✓ Marcar Todas";
            this.btnMarcarTodas.UseVisualStyleBackColor = false;
            // 
            // btnDesmarcarTodas
            // 
            this.btnDesmarcarTodas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDesmarcarTodas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDesmarcarTodas.FlatAppearance.BorderSize = 0;
            this.btnDesmarcarTodas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDesmarcarTodas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDesmarcarTodas.ForeColor = System.Drawing.Color.White;
            this.btnDesmarcarTodas.Location = new System.Drawing.Point(180, 5);
            this.btnDesmarcarTodas.Name = "btnDesmarcarTodas";
            this.btnDesmarcarTodas.Size = new System.Drawing.Size(160, 30);
            this.btnDesmarcarTodas.TabIndex = 1;
            this.btnDesmarcarTodas.Text = "✖ Desmarcar Todas";
            this.btnDesmarcarTodas.UseVisualStyleBackColor = false;
            // 
            // btnInverter
            // 
            this.btnInverter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnInverter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInverter.FlatAppearance.BorderSize = 0;
            this.btnInverter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInverter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnInverter.ForeColor = System.Drawing.Color.White;
            this.btnInverter.Location = new System.Drawing.Point(350, 5);
            this.btnInverter.Name = "btnInverter";
            this.btnInverter.Size = new System.Drawing.Size(160, 30);
            this.btnInverter.TabIndex = 2;
            this.btnInverter.Text = "⇄ Inverter Seleção";
            this.btnInverter.UseVisualStyleBackColor = false;
            // 
            // panelBotoes
            // 
            this.panelBotoes.BackColor = System.Drawing.Color.Transparent;
            this.panelBotoes.Controls.Add(this.btnConfirmar);
            this.panelBotoes.Controls.Add(this.btnCancelar);
            this.panelBotoes.Location = new System.Drawing.Point(20, 600);
            this.panelBotoes.Name = "panelBotoes";
            this.panelBotoes.Size = new System.Drawing.Size(540, 50);
            this.panelBotoes.TabIndex = 7;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnConfirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmar.FlatAppearance.BorderSize = 0;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnConfirmar.ForeColor = System.Drawing.Color.White;
            this.btnConfirmar.Location = new System.Drawing.Point(50, 10);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(200, 40);
            this.btnConfirmar.TabIndex = 0;
            this.btnConfirmar.Text = "✓ CONFIRMAR SELEÇÃO";
            this.btnConfirmar.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(290, 10);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(200, 40);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "✖ CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // FormSelecionarCidade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 700);
            this.Controls.Add(this.panelPrincipal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSelecionarCidade";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Selecionar Cidades para Importação";
            this.panelPrincipal.ResumeLayout(false);
            this.panelPrincipal.PerformLayout();
            this.panelAcoesRapidas.ResumeLayout(false);
            this.panelBotoes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelPrincipal;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblInstrucoes;
        private System.Windows.Forms.TextBox txtBusca;
        private System.Windows.Forms.Button btnLimparBusca;
        private System.Windows.Forms.Label lblContador;
        private System.Windows.Forms.CheckedListBox chkCidades;
        private System.Windows.Forms.Panel panelAcoesRapidas;
        private System.Windows.Forms.Button btnMarcarTodas;
        private System.Windows.Forms.Button btnDesmarcarTodas;
        private System.Windows.Forms.Button btnInverter;
        private System.Windows.Forms.Panel panelBotoes;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnCancelar;
    }
}