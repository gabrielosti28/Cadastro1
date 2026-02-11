// =============================================
// FORMULÁRIO - IMPORTAÇÃO EM LOTE - UNIVERSAL
// Arquivo: FormImportarClientesLote.cs (ATUALIZADO)
// SUPORTA: CSV, XLSX, XLS, TXT, TSV
// =============================================
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Cadastro1
{
    public partial class FormImportarClientesLote : Form
    {
        private ImportadorClientesLote importador;
        private ResultadoImportacaoLote ultimoResultado;
        private string caminhoArquivoImportado;

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

                    // =============================================
                    // ATUALIZADO: Aceitar múltiplos formatos
                    // =============================================
                    ofd.Filter = "Todos os formatos suportados|*.csv;*.xlsx;*.xls;*.txt;*.tsv|" +
                                "Arquivos CSV|*.csv|" +
                                "Arquivos Excel|*.xlsx;*.xls|" +
                                "Arquivos de Texto|*.txt;*.tsv|" +
                                "Todos os arquivos|*.*";
                    ofd.FilterIndex = 1;

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        txtCaminhoArquivo.Text = ofd.FileName;
                        caminhoArquivoImportado = ofd.FileName;
                        btnIniciarImportacao.Enabled = true;

                        // Mostrar tipo de arquivo detectado
                        string extensao = Path.GetExtension(ofd.FileName).ToUpper();
                        lblStatus.Text = $"✅ Arquivo {extensao} selecionado - Pronto para importar";
                        lblStatus.ForeColor = Color.FromArgb(46, 204, 113);
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
            string extensao = Path.GetExtension(txtCaminhoArquivo.Text).ToUpper();
            DialogResult confirmacao = MessageBox.Show(
                $"🚀 CONFIRMAR IMPORTAÇÃO EM LOTE ({extensao})\n\n" +
                "Esta operação irá:\n" +
                "• Ler todos os clientes da planilha\n" +
                "• Cadastrar automaticamente os que tiverem dados completos\n" +
                "• Preencher dados vazios com placeholders\n" +
                "• Pular CPFs duplicados\n" +
                "• GERAR CSV COM AS FALHAS (se houver)\n\n" +
                $"📁 Arquivo: {Path.GetFileName(txtCaminhoArquivo.Text)}\n\n" +
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

                string extensao = Path.GetExtension(txtCaminhoArquivo.Text).ToUpper();
                lblStatus.Text = $"⏳ Processando arquivo {extensao}...";
                lblStatus.ForeColor = Color.FromArgb(52, 152, 219);
                Application.DoEvents();

                // =============================================
                // ATUALIZADO: Usar método universal
                // =============================================
                ultimoResultado = importador.ImportarArquivo(txtCaminhoArquivo.Text);

                // Exportar falhas para CSV
                string arquivoFalhas = null;
                if (ultimoResultado.Falhas > 0)
                {
                    try
                    {
                        arquivoFalhas = ultimoResultado.ExportarFalhasParaCSV(caminhoArquivoImportado);

                        if (!string.IsNullOrEmpty(arquivoFalhas))
                        {
                            lblStatus.Text = $"✅ Importação concluída! {ultimoResultado.Sucessos} sucessos. " +
                                           $"📄 CSV de falhas gerado!";
                        }
                    }
                    catch (Exception exCsv)
                    {
                        System.Diagnostics.Debug.WriteLine($"Erro ao gerar CSV de falhas: {exCsv.Message}");
                    }
                }
                else
                {
                    lblStatus.Text = $"✅ Importação 100% concluída! {ultimoResultado.Sucessos} cadastros realizados.";
                }

                // Atualizar interface
                progressBar.Visible = false;
                lblStatus.ForeColor = Color.FromArgb(46, 204, 113);

                // Mostrar resultados
                MostrarResultados();

                // Avisar sobre CSV de falhas
                if (!string.IsNullOrEmpty(arquivoFalhas))
                {
                    DialogResult abrirCsv = MessageBox.Show(
                        $"📊 IMPORTAÇÃO CONCLUÍDA\n\n" +
                        $"✅ Sucessos: {ultimoResultado.Sucessos}\n" +
                        $"❌ Falhas: {ultimoResultado.Falhas}\n" +
                        $"🔄 CPFs duplicados: {ultimoResultado.CPFsDuplicados}\n\n" +
                        $"📄 Foi gerado um arquivo CSV com as falhas:\n" +
                        $"{Path.GetFileName(arquivoFalhas)}\n\n" +
                        $"💡 Você pode corrigir os erros no CSV e importar novamente!\n\n" +
                        $"Deseja abrir a pasta onde o arquivo foi salvo?",
                        "CSV de Falhas Gerado",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (abrirCsv == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start("explorer.exe",
                            $"/select,\"{arquivoFalhas}\"");
                    }
                }
                else if (ultimoResultado.Sucessos > 0)
                {
                    MessageBox.Show(
                        $"🎉 IMPORTAÇÃO 100% CONCLUÍDA!\n\n" +
                        $"✅ {ultimoResultado.Sucessos} clientes cadastrados com sucesso\n" +
                        $"❌ {ultimoResultado.Falhas} falhas\n" +
                        $"🔄 {ultimoResultado.CPFsDuplicados} CPFs já existentes (pulados)\n\n" +
                        "Todos os clientes foram importados!",
                        "Sucesso Total",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                // Habilitar botões
                btnVerRelatorio.Enabled = true;
                btnFechar.Enabled = true;
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                lblStatus.Text = "❌ Erro na importação";
                lblStatus.ForeColor = Color.FromArgb(231, 76, 60);

                // Mensagem de erro detalhada
                string mensagemErro = ex.Message;

                // Se for erro de driver Excel, mostrar solução
                if (mensagemErro.Contains("Driver") || mensagemErro.Contains("driver") ||
                    mensagemErro.Contains("OleDb") || mensagemErro.Contains("OLEDB"))
                {
                    MessageBox.Show(
                        mensagemErro,
                        "Driver Excel Não Disponível",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(
                        $"❌ Erro durante a importação:\n\n{mensagemErro}",
                        "Erro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

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

            // Mostrar apenas primeiros 100 resultados (performance)
            int maxMostrar = Math.Min(100, ultimoResultado.Resultados.Count);

            for (int i = 0; i < maxMostrar; i++)
            {
                var resultado = ultimoResultado.Resultados[i];

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
                    if (resultado.CamposPreenchidosAutomaticamente.Count > 0)
                    {
                        detalhes = $"⚠️ Campos auto: {string.Join(", ", resultado.CamposPreenchidosAutomaticamente)}";
                    }
                    else if (resultado.DadosExcedentes.Count > 0)
                    {
                        detalhes = $"📎 {resultado.DadosExcedentes.Count} campo(s) salvos em anexo";
                    }
                    else
                    {
                        detalhes = "Cadastrado com todos os dados";
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

            // Se tiver mais resultados, mostrar aviso
            if (ultimoResultado.Resultados.Count > maxMostrar)
            {
                lblStatus.Text += $" (Mostrando {maxMostrar} de {ultimoResultado.TotalLinhas} linhas)";
            }

            // Atualizar resumo
            txtResumo.Text =
                $"📊 RESUMO\r\n" +
                $"━━━━━━━━━━━━━━━━━━\r\n" +
                $"Total: {ultimoResultado.TotalLinhas}\r\n" +
                $"✅ Sucessos: {ultimoResultado.Sucessos}\r\n" +
                $"❌ Falhas: {ultimoResultado.Falhas}\r\n" +
                $"🔄 Duplicados: {ultimoResultado.CPFsDuplicados}\r\n" +
                $"⚠️  Campos auto: {ultimoResultado.CamposPreenchidosAuto}\r\n\r\n" +
                $"Taxa de sucesso:\r\n" +
                $"{(ultimoResultado.TotalLinhas > 0 ? (double)ultimoResultado.Sucessos / ultimoResultado.TotalLinhas * 100 : 0):F1}%";
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