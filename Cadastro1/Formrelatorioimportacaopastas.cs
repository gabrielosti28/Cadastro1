// =============================================
// FORMULÁRIO - RELATÓRIO DE IMPORTAÇÃO DE PASTAS
// Arquivo: FormRelatorioImportacaoPastas.cs
// =============================================
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Cadastro1
{
    public class FormRelatorioImportacaoPastas : Form
    {
        private ResultadoImportacaoPastas resultado;
        private TextBox txtRelatorio;
        private Button btnSalvar;
        private Button btnFechar;
        private Label lblTitulo;

        public FormRelatorioImportacaoPastas(ResultadoImportacaoPastas resultadoImportacao)
        {
            this.resultado = resultadoImportacao;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new Label();
            this.txtRelatorio = new TextBox();
            this.btnSalvar = new Button();
            this.btnFechar = new Button();

            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(0, 102, 204);
            this.lblTitulo.Location = new Point(20, 15);
            this.lblTitulo.Text = "📊 RELATÓRIO DE IMPORTAÇÃO DE PASTAS";

            // txtRelatorio
            this.txtRelatorio.BackColor = Color.White;
            this.txtRelatorio.Font = new Font("Consolas", 10F);
            this.txtRelatorio.Location = new Point(20, 55);
            this.txtRelatorio.Multiline = true;
            this.txtRelatorio.ReadOnly = true;
            this.txtRelatorio.ScrollBars = ScrollBars.Both;
            this.txtRelatorio.Size = new Size(960, 520);
            this.txtRelatorio.WordWrap = false;

            // btnSalvar
            this.btnSalvar.BackColor = Color.FromArgb(52, 152, 219);
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatStyle = FlatStyle.Flat;
            this.btnSalvar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnSalvar.ForeColor = Color.White;
            this.btnSalvar.Location = new Point(300, 590);
            this.btnSalvar.Size = new Size(180, 40);
            this.btnSalvar.Text = "💾 SALVAR";
            this.btnSalvar.Click += BtnSalvar_Click;

            // btnFechar
            this.btnFechar.BackColor = Color.FromArgb(149, 165, 166);
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = FlatStyle.Flat;
            this.btnFechar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnFechar.ForeColor = Color.White;
            this.btnFechar.Location = new Point(510, 590);
            this.btnFechar.Size = new Size(180, 40);
            this.btnFechar.Text = "✖ FECHAR";
            this.btnFechar.Click += (s, e) => this.Close();

            // Form
            this.BackColor = Color.FromArgb(240, 248, 255);
            this.ClientSize = new Size(1000, 645);
            this.Controls.Add(lblTitulo);
            this.Controls.Add(txtRelatorio);
            this.Controls.Add(btnSalvar);
            this.Controls.Add(btnFechar);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Relatório de Importação";

            this.Load += (s, e) => txtRelatorio.Text = resultado.GerarRelatorio();
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Arquivo de Texto|*.txt";
                    sfd.FileName = $"RelatorioImportacaoPastas_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(sfd.FileName, txtRelatorio.Text, Encoding.UTF8);
                        MessageBox.Show($"✅ Relatório salvo!\n\n{sfd.FileName}",
                            "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}