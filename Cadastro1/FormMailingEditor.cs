// =============================================
// FORMULARIO - EDITOR DE MALA DIRETA
// Arquivo: FormMailingEditor.cs
// VERSÃO CORRIGIDA E SIMPLIFICADA
// =============================================
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
                todosClientes = clienteDAL.ListarTodosClientes();
                clientesFiltrados = new List<Cliente>(todosClientes);
                AtualizarListaClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao carregar clientes:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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

        // ===== EVENTOS DOS BOTÕES =====

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
                            "✓ Imagem carregada!\n\nAgora clique em 'POSICIONAR TUDO'.",
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

            lblInfoPosicoes.Text = "👆 Clique 3 vezes na imagem: (1) Endereço, (2) Cidade, (3) CEP";
            lblInfoPosicoes.ForeColor = Color.FromArgb(46, 204, 113);
        }

        private void PicPreview_Click(object sender, EventArgs e)
        {
            if (!modoPositionar || picPreview.Image == null) return;

            MouseEventArgs me = e as MouseEventArgs;
            if (me == null) return;

            // Converter posição do clique para milímetros (A4 = 210x297mm)
            float xMm = (me.X / (float)picPreview.Width) * 210;
            float yMm = (me.Y / (float)picPreview.Height) * 297;

            string campo = camposOrdem[contadorCliques];

            // Remover campo anterior se existir
            templateAtual.Campos.RemoveAll(c => c.Nome == campo);

            // Adicionar novo campo
            templateAtual.AdicionarCampoPadrao(campo, xMm, yMm);

            contadorCliques++;

            if (contadorCliques < camposOrdem.Length)
            {
                lblInfoPosicoes.Text = $"👆 Clique {contadorCliques + 1}/3: {camposOrdem[contadorCliques]}";
            }
            else
            {
                modoPositionar = false;
                picPreview.Cursor = Cursors.Default;
                lblInfoPosicoes.Text = "✓ Todas as posições definidas!";
                lblInfoPosicoes.ForeColor = Color.FromArgb(46, 204, 113);

                MessageBox.Show(
                    "✓ Posições definidas com sucesso!",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            picPreview.Invalidate(); // Força redesenho
            AtualizarStatus();
        }

        private void PicPreview_Paint(object sender, PaintEventArgs e)
        {
            if (templateAtual == null || templateAtual.Campos.Count == 0) return;

            Graphics g = e.Graphics;

            foreach (var campo in templateAtual.Campos)
            {
                // Converter mm para pixels
                float xPx = (campo.PosicaoX / 210f) * picPreview.Width;
                float yPx = (campo.PosicaoY / 297f) * picPreview.Height;

                // Cor por campo
                Color cor = campo.Nome == "Endereco" ? Color.FromArgb(155, 89, 182) :
                           campo.Nome == "Cidade" ? Color.FromArgb(230, 126, 34) :
                           Color.FromArgb(231, 76, 60);

                using (Brush brush = new SolidBrush(cor))
                using (Pen pen = new Pen(cor, 2))
                {
                    // Desenhar marcador
                    g.FillEllipse(brush, xPx - 5, yPx - 5, 10, 10);
                    g.DrawLine(pen, xPx - 10, yPx, xPx + 10, yPx);
                    g.DrawLine(pen, xPx, yPx - 10, xPx, yPx + 10);

                    // Desenhar label
                    using (Font font = new Font("Segoe UI", 8, FontStyle.Bold))
                    {
                        g.DrawString(campo.Nome, font, brush, xPx + 15, yPx - 8);
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
                "✓ Posições limpas!",
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
                    $"✓ Template '{templateAtual.Nome}' salvo com sucesso!",
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
            string filtro = txtBuscaCPF.Text.Replace("-", "").Replace(".", "").ToUpper().Trim();

            if (string.IsNullOrWhiteSpace(filtro))
            {
                clientesFiltrados = new List<Cliente>(todosClientes);
            }
            else
            {
                clientesFiltrados = todosClientes.Where(c =>
                    c.CPF.Replace("-", "").Replace(".", "").Contains(filtro) ||
                    c.NomeCompleto.ToUpper().Contains(filtro) ||
                    c.Cidade.ToUpper().Contains(filtro)
                ).ToList();
            }

            AtualizarListaClientes();
        }

        private void BtnLimparBusca_Click(object sender, EventArgs e)
        {
            txtBuscaCPF.Clear();
            clientesFiltrados = new List<Cliente>(todosClientes);
            AtualizarListaClientes();
        }

        private void ChkClientes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Usar BeginInvoke para atualizar após o check ser aplicado
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
                // Validações
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

                // Obter clientes selecionados
                List<Cliente> clientesSelecionados = new List<Cliente>();

                foreach (int index in chkClientes.CheckedIndices)
                {
                    if (index < clientesFiltrados.Count)
                    {
                        clientesSelecionados.Add(clientesFiltrados[index]);
                    }
                }

                // Escolher onde salvar
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "PDF|*.pdf";
                    sfd.FileName = $"MailaDireta_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                    sfd.Title = "Salvar Mala Direta";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        // Gerar PDF
                        MailingPdfGenerator gerador = new MailingPdfGenerator();
                        string diretorio = Path.GetDirectoryName(sfd.FileName);
                        string arquivo = gerador.GerarPDF(templateAtual, clientesSelecionados, diretorio);

                        // Perguntar se quer abrir
                        DialogResult resultado = MessageBox.Show(
                            $"✓ PDF gerado com sucesso!\n\n" +
                            $"Total de páginas: {clientesSelecionados.Count}\n" +
                            $"Arquivo: {Path.GetFileName(arquivo)}\n\n" +
                            "Deseja abrir o arquivo?",
                            "Sucesso",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);

                        if (resultado == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(arquivo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
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

        // ===== IMPORTAÇÃO DE PLANILHA =====

        private void BtnImportarPlanilha_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Title = "Selecione a planilha com CPFs";
                    ofd.Filter = "Arquivos CSV|*.csv|Arquivos Excel|*.xlsx;*.xls|Todos os arquivos|*.*";
                    ofd.FilterIndex = 1;

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        // Mostrar progresso
                        btnImportarPlanilha.Enabled = false;
                        btnImportarPlanilha.Text = "⏳ Importando...";
                        Application.DoEvents();

                        // Importar planilha
                        ExcelImporter importer = new ExcelImporter();
                        ResultadoImportacao resultado = importer.ImportarPlanilha(ofd.FileName);

                        // Marcar clientes encontrados
                        MarcarClientesDaPlanilha(resultado.ClientesEncontrados);

                        // Atualizar status
                        AtualizarStatusImportacao(resultado);

                        // Mostrar relatório
                        MessageBox.Show(
                            resultado.GerarRelatorio(),
                            "Importação Concluída",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // Restaurar botão
                        btnImportarPlanilha.Enabled = true;
                        btnImportarPlanilha.Text = "📊 IMPORTAR CPFs DA PLANILHA (Excel/CSV)";
                    }
                }
            }
            catch (Exception ex)
            {
                btnImportarPlanilha.Enabled = true;
                btnImportarPlanilha.Text = "📊 IMPORTAR CPFs DA PLANILHA (Excel/CSV)";

                MessageBox.Show(
                    ex.Message,
                    "Erro na Importação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void MarcarClientesDaPlanilha(List<Cliente> clientesEncontrados)
        {
            // Desmarcar todos primeiro
            for (int i = 0; i < chkClientes.Items.Count; i++)
            {
                chkClientes.SetItemChecked(i, false);
            }

            // Marcar apenas os clientes da planilha
            foreach (var cliente in clientesEncontrados)
            {
                // Encontrar índice do cliente na lista filtrada
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

            // Montar texto detalhado
            string status = $"📊 RESULTADO DA IMPORTAÇÃO\n";
            status += $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n";
            status += $"📄 Total na planilha: {resultado.TotalCPFs} CPFs  |  ";
            status += $"🗑️ Duplicatas: {resultado.DuplicatasRemovidas}  |  ";
            status += $"✅ Únicos: {resultado.CPFsValidos.Count}\n";
            status += $"✓ Encontrados: {resultado.CPFsEncontrados} clientes marcados  |  ";
            status += $"✗ Não encontrados: {resultado.CPFsNaoEncontrados} CPFs";

            lblStatusImportacao.Text = status;

            // Colorir conforme resultado
            if (resultado.CPFsNaoEncontrados == 0)
            {
                panelStatusImportacao.BackColor = Color.FromArgb(212, 237, 218); // Verde claro
                lblStatusImportacao.ForeColor = Color.FromArgb(21, 87, 36); // Verde escuro
            }
            else if (resultado.CPFsEncontrados > 0)
            {
                panelStatusImportacao.BackColor = Color.FromArgb(255, 243, 205); // Amarelo claro
                lblStatusImportacao.ForeColor = Color.FromArgb(133, 100, 4); // Amarelo escuro
            }
            else
            {
                panelStatusImportacao.BackColor = Color.FromArgb(248, 215, 218); // Vermelho claro
                lblStatusImportacao.ForeColor = Color.FromArgb(114, 28, 36); // Vermelho escuro
            }
        }
    }
}