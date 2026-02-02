// =============================================
// FORMULÁRIO - RELATÓRIO DE IMPORTAÇÃO
// Arquivo: FormRelatorioImportacao.cs
// =============================================
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Cadastro1
{
    public partial class FormRelatorioImportacao : Form
    {
        private ResultadoImportacaoLote resultado;

        public FormRelatorioImportacao(ResultadoImportacaoLote resultadoImportacao)
        {
            InitializeComponent();
            this.resultado = resultadoImportacao;
        }

        private void FormRelatorioImportacao_Load(object sender, EventArgs e)
        {
            CarregarRelatorio();
        }

        private void CarregarRelatorio()
        {
            string relatorio = resultado.GerarRelatorioDetalhado();
            txtRelatorio.Text = relatorio;
        }

        private void btnSalvarRelatorio_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Arquivo de Texto|*.txt";
                    sfd.FileName = $"RelatorioImportacao_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                    sfd.Title = "Salvar Relatório";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(sfd.FileName, txtRelatorio.Text, Encoding.UTF8);

                        MessageBox.Show(
                            $"✅ Relatório salvo com sucesso!\n\n{sfd.FileName}",
                            "Sucesso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao salvar relatório:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCopiar_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txtRelatorio.Text);
                MessageBox.Show(
                    "✅ Relatório copiado para a área de transferência!",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao copiar:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}