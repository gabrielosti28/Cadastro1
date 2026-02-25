// =============================================
// FORMULÁRIO - IMPORTAÇÃO DE PASTAS DE CLIENTES
// Arquivo: FormImportarPastas.cs
// =============================================
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Cadastro1
{
    public partial class FormImportarPastas : Form
    {
        private ImportadorPastas importador;
        private ResultadoImportacaoPastas ultimoResultado;
        private BackgroundWorker worker;

        public FormImportarPastas()
        {
            InitializeComponent();
            importador = new ImportadorPastas();
            ConfigurarWorker();
        }

        private void ConfigurarWorker()
        {
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        // =============================================
        // SELECIONAR DIRETÓRIO
        // =============================================
        private void btnSelecionarDiretorio_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Selecione a pasta que contém as pastas dos clientes\n(ex: D:\\A 1 ACLIENTES\\AAA CLIENTES)";
                fbd.ShowNewFolderButton = false;

                // Tentar abrir direto no caminho padrão
                string caminhoDefault = @"D:\A 1 ACLIENTES\AAA CLIENTES";
                if (Directory.Exists(caminhoDefault))
                    fbd.SelectedPath = caminhoDefault;

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtDiretorio.Text = fbd.SelectedPath;
                    CarregarPreview(fbd.SelectedPath);
                }
            }
        }

        // =============================================
        // CARREGAR PREVIEW DAS PASTAS
        // =============================================
        private void CarregarPreview(string diretorio)
        {
            try
            {
                var pastas = importador.ListarPastas(diretorio);

                dgvPreview.Rows.Clear();
                dgvPreview.Columns.Clear();

                dgvPreview.Columns.Add("Nome", "NOME DO CLIENTE");
                dgvPreview.Columns.Add("Arquivos", "ARQUIVOS");
                dgvPreview.Columns["Nome"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvPreview.Columns["Arquivos"].Width = 100;

                foreach (string nomePasta in pastas)
                {
                    string caminhoPasta = Path.Combine(diretorio, nomePasta);
                    int qtdArquivos = importador.ContarArquivosPermitidos(caminhoPasta);

                    int rowIdx = dgvPreview.Rows.Add();
                    dgvPreview.Rows[rowIdx].Cells["Nome"].Value = nomePasta;
                    dgvPreview.Rows[rowIdx].Cells["Arquivos"].Value = $"{qtdArquivos} arquivo(s)";
                }

                lblPreviewInfo.Text = $"✅ {pastas.Count} pasta(s) encontrada(s) — prontas para importar";
                lblPreviewInfo.ForeColor = Color.FromArgb(46, 204, 113);
                btnIniciar.Enabled = pastas.Count > 0;
            }
            catch (Exception ex)
            {
                lblPreviewInfo.Text = $"❌ Erro: {ex.Message}";
                lblPreviewInfo.ForeColor = Color.FromArgb(231, 76, 60);
            }
        }

        // =============================================
        // INICIAR IMPORTAÇÃO
        // =============================================
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDiretorio.Text))
            {
                MessageBox.Show("⚠ Selecione o diretório primeiro!",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(txtDiretorio.Text))
            {
                MessageBox.Show("❌ Diretório não encontrado!",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int totalPastas = dgvPreview.Rows.Count;

            string confirmacao = $"🚀 CONFIRMAR IMPORTAÇÃO EM LOTE\n\n" +
                $"📁 Diretório: {txtDiretorio.Text}\n" +
                $"👥 Total de pastas (clientes): {totalPastas}\n\n" +
                "O sistema irá:\n" +
                "✓ Cadastrar cada pasta como um cliente\n" +
                "✓ Importar todos os arquivos como anexos\n" +
                "✓ Usar o nome da pasta como nome do cliente\n\n" +
                "⚠️ ATENÇÃO: Os dados cadastrais (CPF, endereço, etc.)\n" +
                "serão preenchidos com valores temporários.\n" +
                "Você deverá completar cada cliente depois.\n\n" +
                "Deseja continuar?";

            if (MessageBox.Show(confirmacao, "Confirmar Importação",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // Iniciar importação em background
            btnIniciar.Enabled = false;
            btnSelecionarDiretorio.Enabled = false;
            btnCancelar.Enabled = true;
            progressBar.Value = 0;
            progressBar.Visible = true;
            lblStatus.Text = "⏳ Iniciando importação...";
            lblStatus.ForeColor = Color.FromArgb(52, 152, 219);
            txtLog.Clear();

            worker.RunWorkerAsync(txtDiretorio.Text);
        }

        // =============================================
        // BACKGROUND WORKER - EXECUTA A IMPORTAÇÃO
        // =============================================
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string diretorio = (string)e.Argument;

            var resultado = importador.ImportarPastas(
                diretorio,
                (atual, total, nomePasta) =>
                {
                    int percentual = (int)((double)atual / total * 100);
                    worker.ReportProgress(percentual,
                        new object[] { atual, total, nomePasta });
                }
            );

            e.Result = resultado;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var dados = (object[])e.UserState;
            int atual = (int)dados[0];
            int total = (int)dados[1];
            string nomePasta = (string)dados[2];

            progressBar.Value = Math.Min(e.ProgressPercentage, 100);
            lblStatus.Text = $"⏳ Processando {atual} de {total}: {nomePasta}";
            txtLog.AppendText($"[{atual}/{total}] {nomePasta}\r\n");
            txtLog.ScrollToCaret();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnSelecionarDiretorio.Enabled = true;
            btnCancelar.Enabled = false;
            progressBar.Value = 100;

            if (e.Error != null)
            {
                lblStatus.Text = "❌ Erro durante a importação";
                lblStatus.ForeColor = Color.FromArgb(231, 76, 60);
                MessageBox.Show($"Erro:\n\n{e.Error.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIniciar.Enabled = true;
                return;
            }

            ultimoResultado = (ResultadoImportacaoPastas)e.Result;

            lblStatus.Text = $"✅ Concluído! {ultimoResultado.PastasSucesso} clientes cadastrados, " +
                             $"{ultimoResultado.TotalArquivosImportados} arquivos importados";
            lblStatus.ForeColor = Color.FromArgb(46, 204, 113);

            btnVerRelatorio.Enabled = true;

            string mensagemFinal =
                $"🎉 IMPORTAÇÃO CONCLUÍDA!\n\n" +
                $"✅ Clientes cadastrados: {ultimoResultado.PastasSucesso}\n" +
                $"🔄 Já existiam no sistema: {ultimoResultado.PastasJaCadastradas}\n" +
                $"❌ Falhas: {ultimoResultado.PastasFalha}\n" +
                $"📎 Arquivos importados: {ultimoResultado.TotalArquivosImportados}\n\n" +
                "⚠️ PRÓXIMO PASSO:\n" +
                "Abra cada cliente e complete os dados:\n" +
                "CPF, Data de Nascimento, Endereço e INSS.\n\n" +
                "Deseja ver o relatório completo?";

            if (MessageBox.Show(mensagemFinal, "Importação Concluída",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                MostrarRelatorio();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (worker.IsBusy)
            {
                worker.CancelAsync();
                lblStatus.Text = "⏳ Cancelando...";
            }
        }

        private void btnVerRelatorio_Click(object sender, EventArgs e)
        {
            MostrarRelatorio();
        }

        private void MostrarRelatorio()
        {
            if (ultimoResultado == null) return;

            using (FormRelatorioImportacaoPastas formRel =
                new FormRelatorioImportacaoPastas(ultimoResultado))
            {
                formRel.ShowDialog();
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            if (worker.IsBusy)
            {
                if (MessageBox.Show("A importação ainda está em andamento.\nDeseja cancelar e fechar?",
                    "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
                worker.CancelAsync();
            }
            this.Close();
        }
    }
}