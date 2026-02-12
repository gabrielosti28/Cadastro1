// =============================================
// FORMULÁRIO EDITOR DE MALA DIRETA (ATUALIZADO)
// Arquivo: FormMailingEditor.cs
// ATUALIZAÇÃO: Suporte a múltiplos formatos de planilha
// =============================================
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace Cadastro1
{
    public partial class FormMailingEditor : Form
    {
        private MailingTemplateDAL templateDAL;
        private ClienteDAL clienteDAL;
        private MailingTemplate templateAtual;
        private List<Cliente> todosClientes;
        private List<Cliente> clientesFiltrados;

        // Controle de posicionamento
        private bool modoPositionar = false;
        private int contadorCliques = 0;
        private readonly string[] camposOrdem = { "Endereco", "Cidade", "CEP" };

        // =============================================
        // PROPRIEDADES PARA CONVERSÃO PRECISA
        // =============================================
        private float RazaoX => picPreview.Width / 210f;  // Pixels por mm (largura)
        private float RazaoY => picPreview.Height / 297f; // Pixels por mm (altura)

        public FormMailingEditor()
        {
            InitializeComponent();
            InicializarDados();
        }

        private void InicializarDados()
        {
            try
            {
                templateDAL = new MailingTemplateDAL();
                clienteDAL = new ClienteDAL();
                templateAtual = new MailingTemplate();
                todosClientes = new List<Cliente>();
                clientesFiltrados = new List<Cliente>();

                CarregarClientes();
                AtualizarStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao inicializar:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CarregarClientes()
        {
            try
            {
                lblInstrucoes.Text = "✏️ Digite no mínimo 3 caracteres no campo de busca para localizar clientes";
                chkClientes.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        // =============================================
        // BUSCA DE CLIENTES
        // =============================================
        private void txtBuscaCPF_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscaCPF.Text.Trim();

            if (filtro.Length < 3)
            {
                chkClientes.Items.Clear();
                lblInstrucoes.Text = "✏️ Digite pelo menos 3 caracteres para buscar (Nome, CPF ou Cidade)";
                lblTotalSelecionados.Text = "0 clientes encontrados";
                return;
            }

            try
            {
                clientesFiltrados = clienteDAL.BuscarClientesPorFiltro(filtro, 500);
                AtualizarListaClientes();

                lblInstrucoes.Text = $"✅ Encontrados {clientesFiltrados.Count} cliente(s) - Marque os desejados";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro na busca: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtualizarListaClientes()
        {
            chkClientes.Items.Clear();

            foreach (var cliente in clientesFiltrados)
            {
                string item = $"{cliente.CPF.PadRight(15)} | {cliente.NomeCompleto.PadRight(40)} | {cliente.Cidade}";
                chkClientes.Items.Add(item, false);
            }

            AtualizarContador();
        }

        private void AtualizarContador()
        {
            int total = chkClientes.CheckedItems.Count;
            lblTotalSelecionados.Text = $"{total} cliente{(total != 1 ? "s" : "")} selecionado{(total != 1 ? "s" : "")}";
            lblTotalSelecionados.ForeColor = total > 0 ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60);
        }

        private void AtualizarStatus()
        {
            var endereco = templateAtual.Campos.Any(c => c.Nome == "Endereco");
            var cidade = templateAtual.Campos.Any(c => c.Nome == "Cidade");
            var cep = templateAtual.Campos.Any(c => c.Nome == "CEP");

            lblStatusPosicoes.Text =
                $"Endereço: {(endereco ? "✓ Definido" : "✗ Não definido")}\n" +
                $"Cidade: {(cidade ? "✓ Definido" : "✗ Não definido")}\n" +
                $"CEP: {(cep ? "✓ Definido" : "✗ Não definido")}";
        }

        private void BtnCarregar_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Title = "Selecione a imagem do papel A4";
                    ofd.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        picPreview.Image = Image.FromFile(ofd.FileName);
                        templateAtual.CaminhoImagemFundo = ofd.FileName;

                        MessageBox.Show(
                            "✓ Imagem carregada!\n\n" +
                            "📐 O sistema agora conhece o tamanho exato da folha A4.\n\n" +
                            "Próximo passo: Clique em 'POSICIONAR TUDO'",
                            "Sucesso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao carregar imagem:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnPosicionar_Click(object sender, EventArgs e)
        {
            if (picPreview.Image == null)
            {
                MessageBox.Show(
                    "⚠ Carregue uma imagem primeiro!",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            modoPositionar = true;
            contadorCliques = 0;
            picPreview.Cursor = Cursors.Cross;

            MessageBox.Show(
                "📍 MODO DE POSICIONAMENTO ATIVADO\n\n" +
                "Você fará 3 cliques na imagem:\n\n" +
                "1️⃣ ENDEREÇO - Clique onde o endereço deve aparecer\n" +
                "2️⃣ CIDADE - Clique onde a cidade deve aparecer\n" +
                "3️⃣ CEP - Clique onde o CEP deve aparecer\n\n" +
                "💡 Dica: Clique exatamente no início de cada linha!\n" +
                "As coordenadas serão convertidas para milímetros precisos.",
                "Instruções",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            lblInfoPosicoes.Text = "👆 CLIQUE 1/3: Posicione o ENDEREÇO";
            lblInfoPosicoes.ForeColor = Color.FromArgb(46, 204, 113);
        }

        private void PicPreview_Click(object sender, EventArgs e)
        {
            if (!modoPositionar || picPreview.Image == null) return;

            MouseEventArgs me = e as MouseEventArgs;
            if (me == null) return;

            float xMm = (me.X / RazaoX);
            float yMm = (me.Y / RazaoY);

            string campo = camposOrdem[contadorCliques];

            templateAtual.Campos.RemoveAll(c => c.Nome == campo);
            templateAtual.AdicionarCampoPadrao(campo, xMm, yMm);

            contadorCliques++;

            if (contadorCliques < camposOrdem.Length)
            {
                lblInfoPosicoes.Text = $"👆 CLIQUE {contadorCliques + 1}/3: Posicione {camposOrdem[contadorCliques].ToUpper()}";
            }
            else
            {
                modoPositionar = false;
                picPreview.Cursor = Cursors.Default;
                lblInfoPosicoes.Text = "✓ Todas as posições definidas com precisão!";
                lblInfoPosicoes.ForeColor = Color.FromArgb(46, 204, 113);

                MessageBox.Show(
                    "✓ POSIÇÕES DEFINIDAS COM SUCESSO!\n\n" +
                    "📐 Coordenadas precisas salvas:\n" +
                    $"   • Endereço: {templateAtual.Campos[0].PosicaoX:F2}mm, {templateAtual.Campos[0].PosicaoY:F2}mm\n" +
                    $"   • Cidade: {templateAtual.Campos[1].PosicaoX:F2}mm, {templateAtual.Campos[1].PosicaoY:F2}mm\n" +
                    $"   • CEP: {templateAtual.Campos[2].PosicaoX:F2}mm, {templateAtual.Campos[2].PosicaoY:F2}mm\n\n" +
                    "✅ O PDF será gerado EXATAMENTE nestas posições!",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            picPreview.Invalidate();
            AtualizarStatus();
        }

        private void PicPreview_Paint(object sender, PaintEventArgs e)
        {
            if (templateAtual == null || templateAtual.Campos.Count == 0) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            foreach (var campo in templateAtual.Campos)
            {
                float xPx = campo.PosicaoX * RazaoX;
                float yPx = campo.PosicaoY * RazaoY;

                Color cor = campo.Nome == "Endereco" ? Color.FromArgb(155, 89, 182) :
                           campo.Nome == "Cidade" ? Color.FromArgb(230, 126, 34) :
                           Color.FromArgb(231, 76, 60);

                using (Brush brush = new SolidBrush(cor))
                using (Pen pen = new Pen(cor, 2))
                {
                    g.DrawLine(pen, xPx - 10, yPx, xPx + 10, yPx);
                    g.DrawLine(pen, xPx, yPx - 10, xPx, yPx + 10);
                    g.FillEllipse(brush, xPx - 4, yPx - 4, 8, 8);

                    using (Font font = new Font("Segoe UI", 9, FontStyle.Bold))
                    {
                        string label = $"{campo.Nome}\n({campo.PosicaoX:F1}mm, {campo.PosicaoY:F1}mm)";
                        SizeF tamanhoTexto = g.MeasureString(label, font);

                        using (Brush fundoBrush = new SolidBrush(Color.FromArgb(200, Color.White)))
                        {
                            g.FillRectangle(fundoBrush, xPx + 15, yPx - 10, tamanhoTexto.Width + 4, tamanhoTexto.Height + 4);
                        }

                        g.DrawString(label, font, brush, xPx + 17, yPx - 8);
                    }
                }
            }
        }

        private void BtnLimpar_Click(object sender, EventArgs e)
        {
            templateAtual.Campos.Clear();
            picPreview.Invalidate();
            AtualizarStatus();

            MessageBox.Show(
                "✓ Posições limpas!\n\nVocê pode marcar novamente.",
                "OK",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNomeTemplate.Text))
                {
                    MessageBox.Show(
                        "⚠ Digite um nome para o template!",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtNomeTemplate.Focus();
                    return;
                }

                if (templateAtual.Campos.Count == 0)
                {
                    MessageBox.Show(
                        "⚠ Defina as posições primeiro!",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                templateAtual.Nome = txtNomeTemplate.Text.Trim();
                templateDAL.SalvarTemplate(templateAtual);

                MessageBox.Show(
                    $"✓ Template '{templateAtual.Nome}' salvo com sucesso!\n\n" +
                    "As coordenadas precisas foram armazenadas.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao salvar template:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void TxtBusca_TextChanged(object sender, EventArgs e)
        {
            txtBuscaCPF_TextChanged(sender, e);
        }

        private void BtnLimparBusca_Click(object sender, EventArgs e)
        {
            txtBuscaCPF.Clear();
            clientesFiltrados = new List<Cliente>();
            AtualizarListaClientes();
        }

        private void ChkClientes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke(new Action(() => AtualizarContador()));
        }

        private void BtnMarcar_Click(object sender, EventArgs e)
        {
            bool marcar = chkClientes.CheckedItems.Count < chkClientes.Items.Count / 2;

            for (int i = 0; i < chkClientes.Items.Count; i++)
            {
                chkClientes.SetItemChecked(i, marcar);
            }

            AtualizarContador();
        }

        private void BtnGerar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkClientes.CheckedItems.Count == 0)
                {
                    MessageBox.Show(
                        "⚠ Selecione pelo menos um cliente!",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (templateAtual.Campos.Count == 0)
                {
                    MessageBox.Show(
                        "⚠ Defina as posições dos campos primeiro!",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(templateAtual.CaminhoImagemFundo))
                {
                    MessageBox.Show(
                        "⚠ Carregue uma imagem de fundo primeiro!",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                List<Cliente> clientesSelecionados = new List<Cliente>();

                foreach (int index in chkClientes.CheckedIndices)
                {
                    if (index < clientesFiltrados.Count)
                    {
                        clientesSelecionados.Add(clientesFiltrados[index]);
                    }
                }

                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    fbd.Description = "Selecione onde salvar os PDFs da mala direta";
                    fbd.ShowNewFolderButton = true;

                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        btnGerar.Enabled = false;
                        btnGerar.Text = "⏳ GERANDO PDFs...";
                        Application.DoEvents();

                        MailingPdfGenerator gerador = new MailingPdfGenerator();
                        ResultadoGeracaoPDF resultado = gerador.GerarPDF(templateAtual, clientesSelecionados, fbd.SelectedPath);

                        btnGerar.Enabled = true;
                        btnGerar.Text = "GERAR PDF";

                        MessageBox.Show(
                            resultado.GerarRelatorio(),
                            "Geração Concluída",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        DialogResult abrirPasta = MessageBox.Show(
                            "Deseja abrir a pasta com os arquivos gerados?",
                            "Abrir Pasta",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (abrirPasta == DialogResult.Yes)
                        {
                            Process.Start("explorer.exe", fbd.SelectedPath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                btnGerar.Enabled = true;
                btnGerar.Text = "GERAR PDF";

                MessageBox.Show(
                    $"Erro ao gerar PDF:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // =============================================
        // IMPORTAÇÃO DE PLANILHA - ATUALIZADO
        // Agora suporta: XLSX, XLS, CSV, TXT, TSV
        // =============================================
        private void BtnImportarPlanilha_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Title = "Selecione a planilha com CPFs";

                    // =============================================
                    // FILTROS ATUALIZADOS - MÚLTIPLOS FORMATOS
                    // =============================================
                    ofd.Filter =
                        "Todos os formatos suportados|*.csv;*.xlsx;*.xls;*.txt;*.tsv|" +
                        "Arquivos Excel (*.xlsx)|*.xlsx|" +
                        "Arquivos Excel Antigo (*.xls)|*.xls|" +
                        "Arquivos CSV (*.csv)|*.csv|" +
                        "Arquivos de Texto (*.txt)|*.txt|" +
                        "Arquivos TSV (*.tsv)|*.tsv|" +
                        "Todos os arquivos (*.*)|*.*";

                    ofd.FilterIndex = 1; // "Todos os formatos suportados" como padrão

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        btnImportarPlanilha.Enabled = false;
                        btnImportarPlanilha.Text = "⏳ Importando...";
                        Application.DoEvents();

                        // Verificar extensão do arquivo
                        string extensao = Path.GetExtension(ofd.FileName).ToLower();
                        string[] extensoesSuportadas = { ".csv", ".xlsx", ".xls", ".txt", ".tsv" };

                        if (!Array.Exists(extensoesSuportadas, ext => ext == extensao))
                        {
                            MessageBox.Show(
                                $"❌ FORMATO NÃO SUPORTADO: {extensao}\n\n" +
                                "Formatos aceitos:\n" +
                                "✓ .XLSX (Excel moderno)\n" +
                                "✓ .XLS (Excel antigo)\n" +
                                "✓ .CSV (separado por vírgula ou ponto-e-vírgula)\n" +
                                "✓ .TXT (texto delimitado)\n" +
                                "✓ .TSV (separado por tabulação)\n\n" +
                                "Por favor, converta seu arquivo para um destes formatos.",
                                "Formato Não Suportado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            btnImportarPlanilha.Enabled = true;
                            btnImportarPlanilha.Text = "📊 IMPORTAR CPFs DA PLANILHA (Excel/CSV/TXT)";
                            return;
                        }

                        ExcelImporter importer = new ExcelImporter();
                        ResultadoImportacao resultado = importer.ImportarPlanilha(ofd.FileName);

                        MarcarClientesDaPlanilha(resultado.ClientesEncontrados);
                        AtualizarStatusImportacao(resultado);

                        MessageBox.Show(
                            resultado.GerarRelatorio(),
                            "Importação Concluída",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        btnImportarPlanilha.Enabled = true;
                        btnImportarPlanilha.Text = "📊 IMPORTAR CPFs DA PLANILHA (Excel/CSV/TXT)";
                    }
                    else
                    {
                        btnImportarPlanilha.Enabled = true;
                        btnImportarPlanilha.Text = "📊 IMPORTAR CPFs DA PLANILHA (Excel/CSV/TXT)";
                    }
                }
            }
            catch (Exception ex)
            {
                btnImportarPlanilha.Enabled = true;
                btnImportarPlanilha.Text = "📊 IMPORTAR CPFs DA PLANILHA (Excel/CSV/TXT)";

                MessageBox.Show(
                    ex.Message,
                    "Erro na Importação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void MarcarClientesDaPlanilha(List<Cliente> clientesEncontrados)
        {
            for (int i = 0; i < chkClientes.Items.Count; i++)
            {
                chkClientes.SetItemChecked(i, false);
            }

            foreach (var cliente in clientesEncontrados)
            {
                int index = clientesFiltrados.FindIndex(c => c.ClienteID == cliente.ClienteID);

                if (index >= 0 && index < chkClientes.Items.Count)
                {
                    chkClientes.SetItemChecked(index, true);
                }
            }

            AtualizarContador();
        }

        private void AtualizarStatusImportacao(ResultadoImportacao resultado)
        {
            panelStatusImportacao.Visible = true;

            string status = $"📊 RESULTADO DA IMPORTAÇÃO\n";
            status += $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n";
            status += $"📄 Total na planilha: {resultado.TotalCPFs} CPFs  |  ";
            status += $"🗑️ Duplicatas: {resultado.DuplicatasRemovidas}  |  ";
            status += $"✅ Únicos: {resultado.CPFsValidos.Count}\n";
            status += $"✓ Encontrados: {resultado.CPFsEncontrados} clientes marcados  |  ";
            status += $"✗ Não encontrados: {resultado.CPFsNaoEncontrados} CPFs";

            lblStatusImportacao.Text = status;

            if (resultado.CPFsNaoEncontrados == 0)
            {
                panelStatusImportacao.BackColor = Color.FromArgb(212, 237, 218);
                lblStatusImportacao.ForeColor = Color.FromArgb(21, 87, 36);
            }
            else if (resultado.CPFsEncontrados > 0)
            {
                panelStatusImportacao.BackColor = Color.FromArgb(255, 243, 205);
                lblStatusImportacao.ForeColor = Color.FromArgb(133, 100, 4);
            }
            else
            {
                panelStatusImportacao.BackColor = Color.FromArgb(248, 215, 218);
                lblStatusImportacao.ForeColor = Color.FromArgb(114, 28, 36);
            }
        }
    }
}