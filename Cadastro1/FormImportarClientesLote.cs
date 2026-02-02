// =============================================
// FORMULÁRIO - IMPORTAÇÃO EM LOTE
// Arquivo: FormImportarClientesLote.cs
// =============================================
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Org.BouncyCastle.Asn1.Cmp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Cadastro1
{
    public partial class FormImportarClientesLote : Form
    {
        private ImportadorClientesLote importador;
        private ResultadoImportacaoLote ultimoResultado;

        public FormImportarClientesLote()
        {
            InitializeComponent();
            importador = new ImportadorClientesLote();
        }

        private void btnSelecionarArquivo_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Title = "Selecione a planilha com os clientes";
                    ofd.Filter = "Arquivos CSV|*.csv|Todos os arquivos|*.*";
                    ofd.FilterIndex = 1;

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        txtCaminhoArquivo.Text = ofd.FileName;
                        btnIniciarImportacao.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao selecionar arquivo:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnIniciarImportacao_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCaminhoArquivo.Text))
            {
                MessageBox.Show(
                    "⚠ Selecione um arquivo primeiro!",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!File.Exists(txtCaminhoArquivo.Text))
            {
                MessageBox.Show(
                    "❌ Arquivo não encontrado!",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // Confirmar
            DialogResult confirmacao = MessageBox.Show(
                "🚀 CONFIRMAR IMPORTAÇÃO EM LOTE\n\n" +
                "Esta operação irá:\n" +
                "• Ler todos os clientes da planilha\n" +
                "• Cadastrar automaticamente os que tiverem dados completos\n" +
                "• Salvar informações extras como anexos\n" +
                "• Pular CPFs duplicados\n\n" +
                "Deseja continuar?",
                "Confirmar Importação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacao != DialogResult.Yes)
                return;

            RealizarImportacao();
        }

        private void RealizarImportacao()
        {
            try
            {
                // Desabilitar controles
                btnSelecionarArquivo.Enabled = false;
                btnIniciarImportacao.Enabled = false;
                progressBar.Visible = true;
                progressBar.Style = ProgressBarStyle.Marquee;
                lblStatus.Text = "⏳ Processando planilha...";
                lblStatus.ForeColor = Color.FromArgb(52, 152, 219);
                Application.DoEvents();

                // Executar importação
                ultimoResultado = importador.ImportarCSV(txtCaminhoArquivo.Text);

                // Atualizar interface
                progressBar.Visible = false;
                lblStatus.Text = $"✅ Importação concluída! {ultimoResultado.Sucessos} cadastros realizados.";
                lblStatus.ForeColor = Color.FromArgb(46, 204, 113);

                // Mostrar resultados
                MostrarResultados();

                // Habilitar botões
                btnVerRelatorio.Enabled = true;
                btnFechar.Enabled = true;
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                lblStatus.Text = "❌ Erro na importação";
                lblStatus.ForeColor = Color.FromArgb(231, 76, 60);

                MessageBox.Show(
                    $"Erro durante a importação:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                // Reabilitar controles
                btnSelecionarArquivo.Enabled = true;
                btnIniciarImportacao.Enabled = true;
            }
        }

        private void MostrarResultados()
        {
            if (ultimoResultado == null) return;

            // Limpar grid
            dgvResultados.Rows.Clear();
            dgvResultados.Columns.Clear();

            // Configurar colunas
            dgvResultados.Columns.Add("Status", "Status");
            dgvResultados.Columns.Add("Nome", "Nome");
            dgvResultados.Columns.Add("CPF", "CPF");
            dgvResultados.Columns.Add("Detalhes", "Detalhes");

            dgvResultados.Columns["Status"].Width = 80;
            dgvResultados.Columns["Nome"].Width = 250;
            dgvResultados.Columns["CPF"].Width = 120;
            dgvResultados.Columns["Detalhes"].Width = 300;
            dgvResultados.Columns["Detalhes"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Preencher dados
            foreach (var resultado in ultimoResultado.Resultados)
            {
                int rowIndex = dgvResultados.Rows.Add();
                DataGridViewRow row = dgvResultados.Rows[rowIndex];

                // Status
                if (resultado.Sucesso)
                {
                    row.Cells["Status"].Value = "✅ SUCESSO";
                    row.Cells["Status"].Style.BackColor = Color.FromArgb(212, 237, 218);
                    row.Cells["Status"].Style.ForeColor = Color.FromArgb(21, 87, 36);
                }
                else
                {
                    row.Cells["Status"].Value = "❌ FALHA";
                    row.Cells["Status"].Style.BackColor = Color.FromArgb(248, 215, 218);
                    row.Cells["Status"].Style.ForeColor = Color.FromArgb(114, 28, 36);
                }

                // Nome
                row.Cells["Nome"].Value = resultado.Nome ?? "Não encontrado";

                // CPF
                row.Cells["CPF"].Value = FormatarCPF(resultado.CPF);

                // Detalhes
                string detalhes = "";
                if (resultado.Sucesso)
                {
                    if (resultado.DadosExcedentes.Count > 0)
                    {
                        detalhes = $"📎 {resultado.DadosExcedentes.Count} campo(s) salvos em anexo";
                    }
                    else
                    {
                        detalhes = "Cadastrado sem dados extras";
                    }
                }
                else
                {
                    if (resultado.CamposFaltantes.Count > 0)
                    {
                        detalhes = $"Faltam: {string.Join(", ", resultado.CamposFaltantes)}";
                    }
                    else if (!string.IsNullOrEmpty(resultado.MensagemErro))
                    {
                        detalhes = resultado.MensagemErro;
                    }
                }

                row.Cells["Detalhes"].Value = detalhes;
            }

            // Atualizar resumo
            txtResumo.Text =
                $"Total processado: {ultimoResultado.TotalLinhas}\r\n" +
                $"✅ Sucessos: {ultimoResultado.Sucessos}\r\n" +
                $"❌ Falhas: {ultimoResultado.Falhas}\r\n" +
                $"🔄 CPFs duplicados: {ultimoResultado.CPFsDuplicados}";
        }

        private string FormatarCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) return "";
            cpf = cpf.Replace("-", "").Replace(".", "").Trim();

            if (cpf.Length == 10)
                cpf = "0" + cpf;

            if (cpf.Length == 11)
                return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";

            return cpf;
        }

        private void btnVerRelatorio_Click(object sender, EventArgs e)
        {
            if (ultimoResultado == null)
            {
                MessageBox.Show(
                    "Nenhuma importação foi realizada ainda.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            // Mostrar relatório detalhado
            FormRelatorioImportacao formRelatorio = new FormRelatorioImportacao(ultimoResultado);
            formRelatorio.ShowDialog();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            if (ultimoResultado != null && ultimoResultado.Sucessos > 0)
            {
                this.DialogResult = DialogResult.OK;
            }

            this.Close();
        }
    }
}