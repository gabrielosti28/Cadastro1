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

        // Controle de qual campo está sendo posicionado
        private string campoAtual = "";

        public FormMailingEditor()
        {
            templateDAL = new MailingTemplateDAL();
            clienteDAL = new ClienteDAL();
            todosClientes = new List<Cliente>();

            InitializeComponent();
        }

        private void CarregarClientes()
        {
            try
            {
                todosClientes = clienteDAL.ListarTodosClientes();
                AtualizarListaClientes(todosClientes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar clientes:\n\n{ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtualizarListaClientes(List<Cliente> clientes)
        {
            chkClientes.Items.Clear();

            foreach (var cliente in clientes)
            {
                string item = $"{cliente.CPF.PadRight(15)} | {cliente.NomeCompleto.PadRight(40)} | {cliente.Cidade}";
                chkClientes.Items.Add(item, false);
            }

            AtualizarContador();
        }

        private void TxtBuscaCPF_TextChanged(object sender, EventArgs e)
        {
            FiltrarClientes(txtBuscaCPF.Text);
        }

        private void FiltrarClientes(string filtro)
        {
            if (string.IsNullOrWhiteSpace(filtro))
            {
                AtualizarListaClientes(todosClientes);
                return;
            }

            filtro = filtro.ToUpper();
            var clientesFiltrados = todosClientes.Where(c =>
                c.CPF.Contains(filtro) ||
                c.NomeCompleto.ToUpper().Contains(filtro) ||
                c.Cidade.ToUpper().Contains(filtro)
            ).ToList();

            AtualizarListaClientes(clientesFiltrados);
        }

        private void AtualizarContador()
        {
            int total = chkClientes.CheckedItems.Count;
            lblTotalSelecionados.Text = $"{total} cliente{(total != 1 ? "s" : "")} selecionado{(total != 1 ? "s" : "")}";
            lblTotalSelecionados.ForeColor = total > 0 ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60);
        }

        private void IniciarPosicionamento(string campo)
        {
            campoAtual = campo;
            lblInfoPosicoes.Text = $"🎯 Agora clique na IMAGEM onde quer mostrar: {campo}";
            lblInfoPosicoes.ForeColor = Color.FromArgb(230, 126, 34);
            lblInfoPosicoes.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            picPreview.Cursor = Cursors.Cross;
        }

        private void BtnCarregarImagem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Selecione a imagem do papel A4";
                ofd.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp|Todos os arquivos|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        picPreview.Image = Image.FromFile(ofd.FileName);
                        templateAtual.CaminhoImagemFundo = ofd.FileName;

                        MessageBox.Show(
                            "✓ Imagem carregada com sucesso!\n\n" +
                            "Agora clique nos botões azuis (Nome, Endereço, etc)\n" +
                            "e depois clique na imagem onde quer cada informação.",
                            "Próximo Passo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao carregar imagem:\n\n{ex.Message}",
                            "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void PicPreview_Click(object sender, EventArgs e)
        {
            if (picPreview.Image == null)
            {
                MessageBox.Show("⚠ Carregue uma imagem primeiro!",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(campoAtual))
            {
                MessageBox.Show(
                    "⚠ Clique primeiro em um dos botões:\n\n" +
                    "• 👤 Nome\n• 🏠 Endereço\n• 🌆 Cidade\n• 📮 CEP",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            MouseEventArgs me = (MouseEventArgs)e;

            // Converter coordenadas de pixel para mm
            float xMm = (me.X / (float)picPreview.Width) * 210;
            float yMm = (me.Y / (float)picPreview.Height) * 297;

            // Remover campo anterior se existir
            templateAtual.Campos.RemoveAll(c => c.Nome == campoAtual);

            // Adicionar novo campo
            templateAtual.AdicionarCampoPadrao(campoAtual, xMm, yMm);

            // Atualizar display
            picPreview.Invalidate();
            AtualizarStatusPosicoes();

            MessageBox.Show(
                $"✓ Posição definida!\n\n" +
                $"Campo: {campoAtual}\n" +
                $"X: {xMm:F1}mm\n" +
                $"Y: {yMm:F1}mm",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // Resetar
            campoAtual = "";
            lblInfoPosicoes.Text = "Clique nos botões abaixo e depois clique na imagem:";
            lblInfoPosicoes.ForeColor = Color.Gray;
            lblInfoPosicoes.Font = new Font("Segoe UI", 9, FontStyle.Italic);
        }

        private void PicPreview_Paint(object sender, PaintEventArgs e)
        {
            if (templateAtual == null || templateAtual.Campos.Count == 0)
                return;

            Graphics g = e.Graphics;

            foreach (var campo in templateAtual.Campos)
            {
                // Converter mm para pixels
                float xPx = (campo.PosicaoX / 210f) * picPreview.Width;
                float yPx = (campo.PosicaoY / 297f) * picPreview.Height;

                // Desenhar marcador
                Color cor = ObterCorCampo(campo.Nome);
                using (Brush brush = new SolidBrush(cor))
                using (Pen pen = new Pen(cor, 2))
                {
                    // Círculo
                    g.FillEllipse(brush, xPx - 5, yPx - 5, 10, 10);

                    // Cruz
                    g.DrawLine(pen, xPx - 10, yPx, xPx + 10, yPx);
                    g.DrawLine(pen, xPx, yPx - 10, xPx, yPx + 10);

                    // Label
                    using (Font font = new Font("Segoe UI", 8, FontStyle.Bold))
                    {
                        g.DrawString(campo.Nome, font, brush, xPx + 15, yPx - 8);
                    }
                }
            }
        }

        private Color ObterCorCampo(string nomeCampo)
        {
            switch (nomeCampo)
            {
                case "NomeCompleto": return Color.FromArgb(46, 204, 113);
                case "Endereco": return Color.FromArgb(155, 89, 182);
                case "Cidade": return Color.FromArgb(230, 126, 34);
                case "CEP": return Color.FromArgb(231, 76, 60);
                default: return Color.Blue;
            }
        }

        private void AtualizarStatusPosicoes()
        {
            Label lbl = this.Controls.Find("lblStatusPosicoes", true).FirstOrDefault() as Label;
            if (lbl == null) return;

            var nome = templateAtual.Campos.Any(c => c.Nome == "NomeCompleto");
            var end = templateAtual.Campos.Any(c => c.Nome == "Endereco");
            var cid = templateAtual.Campos.Any(c => c.Nome == "Cidade");
            var cep = templateAtual.Campos.Any(c => c.Nome == "CEP");

            lbl.Text = $"{(nome ? "✓" : "✗")} Nome: {(nome ? "Definido" : "Não definido")}\n" +
                       $"{(end ? "✓" : "✗")} Endereço: {(end ? "Definido" : "Não definido")}\n" +
                       $"{(cid ? "✓" : "✗")} Cidade: {(cid ? "Definido" : "Não definido")}\n" +
                       $"{(cep ? "✓" : "✗")} CEP: {(cep ? "Definido" : "Não definido")}";
        }

        private void BtnSalvarTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNomeTemplate.Text))
                {
                    MessageBox.Show("⚠ Digite um nome para o template!",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (templateAtual.Campos.Count == 0)
                {
                    MessageBox.Show("⚠ Defina pelo menos uma posição antes de salvar!",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                templateAtual.Nome = txtNomeTemplate.Text;
                templateDAL.SalvarTemplate(templateAtual);

                MessageBox.Show(
                    "✓ Template salvo com sucesso!\n\n" +
                    $"Nome: {templateAtual.Nome}\n" +
                    $"Campos definidos: {templateAtual.Campos.Count}\n\n" +
                    $"Local: {templateDAL.ObterDiretorioTemplates()}",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar template:\n\n{ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnMarcarTodos_Click(object sender, EventArgs e)
        {
            bool marcarTodos = chkClientes.CheckedItems.Count == 0;

            for (int i = 0; i < chkClientes.Items.Count; i++)
            {
                chkClientes.SetItemChecked(i, marcarTodos);
            }

            btnMarcarTodos.Text = marcarTodos ? "✗ Desmarcar Todos" : "✓ Marcar Todos";
            AtualizarContador();
        }

        private void BtnGerarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkClientes.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Selecione pelo menos um cliente!",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (templateAtual.Campos.Count == 0)
                {
                    MessageBox.Show("Defina as posições dos campos antes de gerar o PDF!",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<Cliente> clientesSelecionados = new List<Cliente>();
                var clientesExibidos = txtBuscaCPF.Text.Length > 0
                    ? todosClientes.Where(c => c.CPF.Contains(txtBuscaCPF.Text.ToUpper())).ToList()
                    : todosClientes;

                foreach (int index in chkClientes.CheckedIndices)
                {
                    if (index < clientesExibidos.Count)
                    {
                        clientesSelecionados.Add(clientesExibidos[index]);
                    }
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "PDF|*.pdf";
                    sfd.FileName = $"MailaDireta_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                    sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        btnGerarPDF.Enabled = false;
                        btnGerarPDF.Text = "Gerando PDF...";
                        Application.DoEvents();

                        MailingPdfGenerator gerador = new MailingPdfGenerator();
                        string caminhoSaida = Path.GetDirectoryName(sfd.FileName);

                        string arquivoGerado = gerador.GerarPDF(templateAtual, clientesSelecionados, caminhoSaida);

                        btnGerarPDF.Enabled = true;
                        btnGerarPDF.Text = "GERAR PDF PARA IMPRESSÃO";

                        DialogResult resultado = MessageBox.Show(
                            "PDF GERADO COM SUCESSO!\n\n" +
                            $"Total de páginas: {clientesSelecionados.Count}\n" +
                            $"Arquivo: {Path.GetFileName(arquivoGerado)}\n" +
                            $"Local: {Path.GetDirectoryName(arquivoGerado)}\n\n" +
                            "Deseja abrir o arquivo agora?",
                            "Sucesso",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);

                        if (resultado == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(arquivoGerado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                btnGerarPDF.Enabled = true;
                btnGerarPDF.Text = "GERAR PDF PARA IMPRESSÃO";

                MessageBox.Show($"Erro ao gerar PDF:\n\n{ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "Deseja realmente fechar?\n\n" +
                "Certifique-se de ter salvo o template se fez alterações.",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}