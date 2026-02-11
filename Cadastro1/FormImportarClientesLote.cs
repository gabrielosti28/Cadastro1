// =============================================
// FORMULÁRIO - IMPORTAÇÃO EM LOTE - COM FILTRO DE CIDADES
// Arquivo: FormImportarClientesLote.cs (ATUALIZADO)
// NOVO: Seleção de cidades antes da importação
// =============================================
using System;
using System.Collections.Generic;
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
        private List<string> cidadesSelecionadas; // NOVO

        public FormImportarClientesLote()
        {
            InitializeComponent();
            importador = new ImportadorClientesLote();
            cidadesSelecionadas = null;
        }

        private void btnSelecionarArquivo_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Title = "Selecione a planilha com os clientes";
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

                        // NOVO: Resetar seleção de cidades ao trocar arquivo
                        cidadesSelecionadas = null;

                        btnIniciarImportacao.Enabled = true;

                        string extensao = Path.GetExtension(ofd.FileName).ToUpper();
                        lblStatus.Text = $"✅ Arquivo {extensao} selecionado - Configure o filtro de cidades";
                        lblStatus.ForeColor = Color.FromArgb(52, 152, 219);
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

            // =============================================
            // NOVO: ETAPA DE SELEÇÃO DE CIDADES
            // =============================================
            try
            {
                lblStatus.Text = "⏳ Analisando cidades na planilha...";
                lblStatus.ForeColor = Color.FromArgb(230, 126, 34);
                Application.DoEvents();

                // Extrair cidades da planilha
                List<string> cidadesNaPlanilha = importador.ExtrairCidadesDaPlanilha(txtCaminhoArquivo.Text);

                if (cidadesNaPlanilha == null || cidadesNaPlanilha.Count == 0)
                {
                    DialogResult semCidades = MessageBox.Show(
                        "⚠ NENHUMA CIDADE ENCONTRADA\n\n" +
                        "A planilha não possui dados na coluna 'Cidade'.\n\n" +
                        "Deseja importar mesmo assim?\n" +
                        "(Todos os clientes terão cidade preenchida automaticamente)",
                        "Aviso",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (semCidades != DialogResult.Yes)
                        return;

                    cidadesSelecionadas = null;
                }
                else
                {
                    // Mostrar formulário de seleção de cidades
                    using (FormSelecionarCidadeImportacao formCidades = new FormSelecionarCidadeImportacao(cidadesNaPlanilha))
                    {
                        if (formCidades.ShowDialog() != DialogResult.OK)
                        {
                            lblStatus.Text = "❌ Importação cancelada - Nenhuma cidade selecionada";
                            lblStatus.ForeColor = Color.FromArgb(231, 76, 60);
                            return;
                        }

                        cidadesSelecionadas = formCidades.CidadesSelecionadas;
                    }

                    // Aplicar filtro de cidades ao importador
                    importador.DefinirFiltroCidades(cidadesSelecionadas);
                }

                // Confirmar importação
                string mensagemConfirmacao = "🚀 CONFIRMAR IMPORTAÇÃO EM LOTE\n\n";

                if (cidadesSelecionadas == null || cidadesSelecionadas.Count == 0)
                {
                    mensagemConfirmacao += "⚠️ SEM FILTRO DE CIDADE - Todos os clientes serão importados\n\n";
                }
                else if (cidadesSelecionadas.Count == cidadesNaPlanilha.Count)
                {
                    mensagemConfirmacao += $"✅ TODAS AS CIDADES ({cidadesSelecionadas.Count}) selecionadas\n\n";
                }
                else
                {
                    mensagemConfirmacao += $"📊 FILTRO ATIVO: {cidadesSelecionadas.Count} de {cidadesNaPlanilha.Count} cidades\n\n";

                    if (cidadesSelecionadas.Count <= 5)
                    {
                        mensagemConfirmacao += "Cidades selecionadas:\n";
                        foreach (var cidade in cidadesSelecionadas)
                        {
                            mensagemConfirmacao += $"  • {cidade}\n";
                        }
                    }
                    else
                    {
                        mensagemConfirmacao += "Primeiras cidades:\n";
                        for (int i = 0; i < 3; i++)
                        {
                            mensagemConfirmacao += $"  • {cidadesSelecionadas[i]}\n";
                        }
                        mensagemConfirmacao += $"  • ... e mais {cidadesSelecionadas.Count - 3}\n";
                    }

                    mensagemConfirmacao += "\n⚠️ Clientes de outras cidades serão IGNORADOS\n\n";
                }

                mensagemConfirmacao +=
                    "Esta operação irá:\n" +
                    "• Cadastrar clientes com dados completos\n" +
                    "• Preencher dados vazios com placeholders\n" +
                    "• Pular CPFs duplicados\n" +
                    "• Gerar CSV com falhas (se houver)\n\n" +
                    $"📁 Arquivo: {Path.GetFileName(txtCaminhoArquivo.Text)}\n\n" +
                    "Deseja continuar?";

                DialogResult confirmacao = MessageBox.Show(
                    mensagemConfirmacao,
                    "Confirmar Importação",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacao != DialogResult.Yes)
                    return;

                RealizarImportacao();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao processar cidades:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void RealizarImportacao()
        {
            try
            {
                btnSelecionarArquivo.Enabled = false;
                btnIniciarImportacao.Enabled = false;
                progressBar.Visible = true;
                progressBar.Style = ProgressBarStyle.Marquee;

                string extensao = Path.GetExtension(txtCaminhoArquivo.Text).ToUpper();

                if (cidadesSelecionadas != null && cidadesSelecionadas.Count > 0)
                {
                    lblStatus.Text = $"⏳ Importando {extensao} (Filtro: {cidadesSelecionadas.Count} cidades)...";
                }
                else
                {
                    lblStatus.Text = $"⏳ Importando {extensao} (Sem filtro de cidade)...";
                }

                lblStatus.ForeColor = Color.FromArgb(52, 152, 219);
                Application.DoEvents();

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
                            lblStatus.Text = $"✅ Importação concluída! {ultimoResultado.Sucessos} sucessos. 📄 CSV de falhas gerado!";
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

                progressBar.Visible = false;
                lblStatus.ForeColor = Color.FromArgb(46, 204, 113);

                MostrarResultados();

                // Mensagem de conclusão com informações sobre filtro
                string mensagemFinal = $"📊 IMPORTAÇÃO CONCLUÍDA\n\n";
                mensagemFinal += $"✅ Sucessos: {ultimoResultado.Sucessos}\n";
                mensagemFinal += $"❌ Falhas: {ultimoResultado.Falhas}\n";
                mensagemFinal += $"🔄 CPFs duplicados: {ultimoResultado.CPFsDuplicados}\n";

                if (ultimoResultado.ClientesIgnoradosPorCidade > 0)
                {
                    mensagemFinal += $"🏙️ Ignorados (filtro cidade): {ultimoResultado.ClientesIgnoradosPorCidade}\n";
                }

                mensagemFinal += "\n";

                if (!string.IsNullOrEmpty(arquivoFalhas))
                {
                    mensagemFinal += $"📄 CSV com falhas gerado:\n{Path.GetFileName(arquivoFalhas)}\n\n";
                    mensagemFinal += "💡 Você pode corrigir os erros no CSV e importar novamente!\n\n";
                    mensagemFinal += "Deseja abrir a pasta onde o arquivo foi salvo?";

                    DialogResult abrirCsv = MessageBox.Show(
                        mensagemFinal,
                        "CSV de Falhas Gerado",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (abrirCsv == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{arquivoFalhas}\"");
                    }
                }
                else if (ultimoResultado.Sucessos > 0)
                {
                    mensagemFinal += "🎉 Todos os clientes foram importados com sucesso!";

                    MessageBox.Show(
                        mensagemFinal,
                        "Sucesso Total",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                btnVerRelatorio.Enabled = true;
                btnFechar.Enabled = true;
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                lblStatus.Text = "❌ Erro na importação";
                lblStatus.ForeColor = Color.FromArgb(231, 76, 60);

                string mensagemErro = ex.Message;

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

                btnSelecionarArquivo.Enabled = true;
                btnIniciarImportacao.Enabled = true;
            }
        }

        private void MostrarResultados()
        {
            if (ultimoResultado == null) return;

            dgvResultados.Rows.Clear();
            dgvResultados.Columns.Clear();

            dgvResultados.Columns.Add("Status", "Status");
            dgvResultados.Columns.Add("Nome", "Nome");
            dgvResultados.Columns.Add("CPF", "CPF");
            dgvResultados.Columns.Add("Cidade", "Cidade"); // NOVO: Coluna Cidade
            dgvResultados.Columns.Add("Detalhes", "Detalhes");

            dgvResultados.Columns["Status"].Width = 80;
            dgvResultados.Columns["Nome"].Width = 200;
            dgvResultados.Columns["CPF"].Width = 120;
            dgvResultados.Columns["Cidade"].Width = 150; // NOVO
            dgvResultados.Columns["Detalhes"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            int maxMostrar = Math.Min(100, ultimoResultado.Resultados.Count);

            for (int i = 0; i < maxMostrar; i++)
            {
                var resultado = ultimoResultado.Resultados[i];

                int rowIndex = dgvResultados.Rows.Add();
                DataGridViewRow row = dgvResultados.Rows[rowIndex];

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

                row.Cells["Nome"].Value = resultado.Nome ?? "Não encontrado";
                row.Cells["CPF"].Value = FormatarCPF(resultado.CPF);
                row.Cells["Cidade"].Value = resultado.Cidade ?? "-"; // NOVO

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
                        // NOVO: Destacar filtro de cidade
                        if (resultado.MensagemErro.Contains("cidade não está no filtro"))
                        {
                            detalhes = "🏙️ " + resultado.MensagemErro;
                            row.Cells["Status"].Style.BackColor = Color.FromArgb(255, 243, 205);
                            row.Cells["Status"].Style.ForeColor = Color.FromArgb(133, 100, 4);
                        }
                        else
                        {
                            detalhes = resultado.MensagemErro;
                        }
                    }
                }

                row.Cells["Detalhes"].Value = detalhes;
            }

            if (ultimoResultado.Resultados.Count > maxMostrar)
            {
                lblStatus.Text += $" (Mostrando {maxMostrar} de {ultimoResultado.TotalLinhas} linhas)";
            }

            // Atualizar resumo
            string resumo = $"📊 RESUMO\r\n━━━━━━━━━━━━━━━━━━\r\n";
            resumo += $"Total: {ultimoResultado.TotalLinhas}\r\n";
            resumo += $"✅ Sucessos: {ultimoResultado.Sucessos}\r\n";
            resumo += $"❌ Falhas: {ultimoResultado.Falhas}\r\n";
            resumo += $"🔄 Duplicados: {ultimoResultado.CPFsDuplicados}\r\n";

            if (ultimoResultado.ClientesIgnoradosPorCidade > 0)
            {
                resumo += $"🏙️ Ignorados: {ultimoResultado.ClientesIgnoradosPorCidade}\r\n";
            }

            resumo += $"⚠️ Campos auto: {ultimoResultado.CamposPreenchidosAuto}\r\n\r\n";
            resumo += $"Taxa sucesso:\r\n{(ultimoResultado.TotalLinhas > 0 ? (double)ultimoResultado.Sucessos / ultimoResultado.TotalLinhas * 100 : 0):F1}%";

            txtResumo.Text = resumo;
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