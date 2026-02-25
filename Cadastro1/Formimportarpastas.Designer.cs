// =============================================
// DESIGNER - FORMULÁRIO IMPORTAÇÃO DE PASTAS
// Arquivo: FormImportarPastas.Designer.cs
// =============================================
namespace Cadastro1
{
    partial class FormImportarPastas
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTopo = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();

            this.groupDiretorio = new System.Windows.Forms.GroupBox();
            this.txtDiretorio = new System.Windows.Forms.TextBox();
            this.btnSelecionarDiretorio = new System.Windows.Forms.Button();
            this.lblInstrucao = new System.Windows.Forms.Label();

            this.groupPreview = new System.Windows.Forms.GroupBox();
            this.dgvPreview = new System.Windows.Forms.DataGridView();
            this.lblPreviewInfo = new System.Windows.Forms.Label();

            this.groupProgresso = new System.Windows.Forms.GroupBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();

            this.panelBotoes = new System.Windows.Forms.Panel();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnVerRelatorio = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();

            // ── panelTopo ──────────────────────────────
            this.panelTopo.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.panelTopo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopo.Height = 80;
            this.panelTopo.Controls.Add(this.lblTitulo);
            this.panelTopo.Controls.Add(this.lblSubtitulo);

            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 10);
            this.lblTitulo.Text = "📁 IMPORTAR CLIENTES DAS PASTAS";

            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(200, 230, 255);
            this.lblSubtitulo.Location = new System.Drawing.Point(22, 50);
            this.lblSubtitulo.Text = "Cada pasta vira um cliente. Todos os arquivos são importados como anexos automaticamente.";

            // ── groupDiretorio ─────────────────────────
            this.groupDiretorio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupDiretorio.Location = new System.Drawing.Point(15, 95);
            this.groupDiretorio.Size = new System.Drawing.Size(1070, 90);
            this.groupDiretorio.Text = "1. Selecione a pasta raiz dos clientes";

            this.lblInstrucao.AutoSize = true;
            this.lblInstrucao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblInstrucao.ForeColor = System.Drawing.Color.Gray;
            this.lblInstrucao.Location = new System.Drawing.Point(15, 22);
            this.lblInstrucao.Text = "Exemplo: D:\\A 1 ACLIENTES\\AAA CLIENTES  (a pasta que contém as subpastas de cada cliente)";

            this.txtDiretorio.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDiretorio.Location = new System.Drawing.Point(15, 48);
            this.txtDiretorio.ReadOnly = true;
            this.txtDiretorio.Size = new System.Drawing.Size(830, 27);
            this.txtDiretorio.Text = @"D:\A 1 ACLIENTES\AAA CLIENTES";

            this.btnSelecionarDiretorio.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnSelecionarDiretorio.FlatAppearance.BorderSize = 0;
            this.btnSelecionarDiretorio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecionarDiretorio.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSelecionarDiretorio.ForeColor = System.Drawing.Color.White;
            this.btnSelecionarDiretorio.Location = new System.Drawing.Point(860, 44);
            this.btnSelecionarDiretorio.Size = new System.Drawing.Size(190, 35);
            this.btnSelecionarDiretorio.Text = "📂 SELECIONAR";
            this.btnSelecionarDiretorio.UseVisualStyleBackColor = false;
            this.btnSelecionarDiretorio.Click += new System.EventHandler(this.btnSelecionarDiretorio_Click);

            this.groupDiretorio.Controls.Add(this.lblInstrucao);
            this.groupDiretorio.Controls.Add(this.txtDiretorio);
            this.groupDiretorio.Controls.Add(this.btnSelecionarDiretorio);

            // ── groupPreview ───────────────────────────
            this.groupPreview.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupPreview.Location = new System.Drawing.Point(15, 195);
            this.groupPreview.Size = new System.Drawing.Size(1070, 230);
            this.groupPreview.Text = "2. Prévia das pastas encontradas";

            this.lblPreviewInfo.AutoSize = true;
            this.lblPreviewInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPreviewInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblPreviewInfo.Location = new System.Drawing.Point(15, 25);
            this.lblPreviewInfo.Text = "Selecione um diretório para ver as pastas...";

            this.dgvPreview.AllowUserToAddRows = false;
            this.dgvPreview.AllowUserToDeleteRows = false;
            this.dgvPreview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPreview.BackgroundColor = System.Drawing.Color.White;
            this.dgvPreview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPreview.ColumnHeadersHeight = 35;
            this.dgvPreview.Location = new System.Drawing.Point(15, 50);
            this.dgvPreview.ReadOnly = true;
            this.dgvPreview.RowHeadersVisible = false;
            this.dgvPreview.RowTemplate.Height = 28;
            this.dgvPreview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPreview.Size = new System.Drawing.Size(1040, 165);
            this.dgvPreview.TabIndex = 0;

            this.groupPreview.Controls.Add(this.lblPreviewInfo);
            this.groupPreview.Controls.Add(this.dgvPreview);

            // ── groupProgresso ─────────────────────────
            this.groupProgresso.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupProgresso.Location = new System.Drawing.Point(15, 435);
            this.groupProgresso.Size = new System.Drawing.Size(1070, 175);
            this.groupProgresso.Text = "3. Progresso da importação";

            this.progressBar.Location = new System.Drawing.Point(15, 30);
            this.progressBar.Size = new System.Drawing.Size(1040, 22);
            this.progressBar.Visible = false;

            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(15, 60);
            this.lblStatus.Text = "Aguardando início...";

            this.txtLog.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtLog.ForeColor = System.Drawing.Color.FromArgb(0, 230, 118);
            this.txtLog.Location = new System.Drawing.Point(15, 88);
            this.txtLog.Multiline = true;
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(1040, 75);

            this.groupProgresso.Controls.Add(this.progressBar);
            this.groupProgresso.Controls.Add(this.lblStatus);
            this.groupProgresso.Controls.Add(this.txtLog);

            // ── panelBotoes ────────────────────────────
            this.panelBotoes.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.panelBotoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotoes.Height = 60;
            this.panelBotoes.Controls.Add(this.btnIniciar);
            this.panelBotoes.Controls.Add(this.btnCancelar);
            this.panelBotoes.Controls.Add(this.btnVerRelatorio);
            this.panelBotoes.Controls.Add(this.btnFechar);

            // btnIniciar
            this.btnIniciar.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnIniciar.Enabled = false;
            this.btnIniciar.FlatAppearance.BorderSize = 0;
            this.btnIniciar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIniciar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnIniciar.ForeColor = System.Drawing.Color.White;
            this.btnIniciar.Location = new System.Drawing.Point(150, 10);
            this.btnIniciar.Size = new System.Drawing.Size(220, 40);
            this.btnIniciar.Text = "🚀 INICIAR IMPORTAÇÃO";
            this.btnIniciar.UseVisualStyleBackColor = false;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);

            // btnCancelar
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnCancelar.Enabled = false;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(390, 10);
            this.btnCancelar.Size = new System.Drawing.Size(180, 40);
            this.btnCancelar.Text = "⏹ CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // btnVerRelatorio
            this.btnVerRelatorio.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnVerRelatorio.Enabled = false;
            this.btnVerRelatorio.FlatAppearance.BorderSize = 0;
            this.btnVerRelatorio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerRelatorio.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnVerRelatorio.ForeColor = System.Drawing.Color.White;
            this.btnVerRelatorio.Location = new System.Drawing.Point(590, 10);
            this.btnVerRelatorio.Size = new System.Drawing.Size(200, 40);
            this.btnVerRelatorio.Text = "📊 VER RELATÓRIO";
            this.btnVerRelatorio.UseVisualStyleBackColor = false;
            this.btnVerRelatorio.Click += new System.EventHandler(this.btnVerRelatorio_Click);

            // btnFechar
            this.btnFechar.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(810, 10);
            this.btnFechar.Size = new System.Drawing.Size(160, 40);
            this.btnFechar.Text = "✖ FECHAR";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);

            // ── Form ───────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.panelTopo);
            this.Controls.Add(this.groupDiretorio);
            this.Controls.Add(this.groupPreview);
            this.Controls.Add(this.groupProgresso);
            this.Controls.Add(this.panelBotoes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormImportarPastas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importar Clientes das Pastas";

            // Carregar preview automaticamente se o caminho padrão existir
            this.Load += (s, e) => {
                string caminhoDefault = @"D:\A 1 ACLIENTES\AAA CLIENTES";
                if (System.IO.Directory.Exists(caminhoDefault))
                    CarregarPreview(caminhoDefault);
            };
        }

        // Declarações dos controles
        private System.Windows.Forms.Panel panelTopo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.GroupBox groupDiretorio;
        private System.Windows.Forms.Label lblInstrucao;
        private System.Windows.Forms.TextBox txtDiretorio;
        private System.Windows.Forms.Button btnSelecionarDiretorio;
        private System.Windows.Forms.GroupBox groupPreview;
        private System.Windows.Forms.DataGridView dgvPreview;
        private System.Windows.Forms.Label lblPreviewInfo;
        private System.Windows.Forms.GroupBox groupProgresso;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Panel panelBotoes;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnVerRelatorio;
        private System.Windows.Forms.Button btnFechar;
    }
}