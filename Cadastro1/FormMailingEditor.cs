// =============================================
// FORMULARIO - EDITOR DE MALA DIRETA
// Arquivo: FormMailingEditor.cs
// LÓGICA E EVENTOS
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

        private string campoAtual = "";
        private int contadorCliques = 0;
        private string[] camposOrdem = { "Endereco", "Cidade", "CEP" };

        public FormMailingEditor()
        {
            InitializeComponent();

            templateDAL = new MailingTemplateDAL();
            clienteDAL = new ClienteDAL();
            todosClientes = new List<Cliente>();
            templateAtual = new MailingTemplate();

            ConfigurarEventos();
            CarregarClientes();
        }

        private void ConfigurarEventos()
        {
            // Configurar eventos dos controles
            picPreview.Click += PicPreview_Click;
            picPreview.Paint += PicPreview_Paint;

            // Encontrar botões pelo índice ou nome (na prática, você daria nomes aos controles)
            // Como estamos criando dinamicamente, precisamos encontrar os controles
            // Vou assumir que você adicionou os eventos diretamente na criação

            // Para simplificar, mantenha os eventos no arquivo principal
            // Na prática ideal, você usaria o Designer do VS e daria nomes aos controles
        }

        private void CarregarClientes()
        {
            try
            {
                todosClientes = clienteDAL.ListarTodosClientes();
                AtualizarLista(todosClientes);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar clientes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtualizarLista(List<Cliente> clientes)
        {
            chkClientes.Items.Clear();
            foreach (var c in clientes)
            {
                string item = c.CPF.PadRight(15) + " | " + c.NomeCompleto.PadRight(40) + " | " + c.Cidade;
                chkClientes.Items.Add(item, false);
            }
            AtualizarContador();
        }

        private void TxtBusca_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscaCPF.Text.Replace("-", "").Replace(".", "").ToUpper();
            if (string.IsNullOrWhiteSpace(filtro))
            {
                AtualizarLista(todosClientes);
                return;
            }
            var filtrados = todosClientes.Where(c =>
                c.CPF.Replace("-", "").Replace(".", "").Contains(filtro) ||
                c.NomeCompleto.ToUpper().Contains(filtro) ||
                c.Cidade.ToUpper().Contains(filtro)
            ).ToList();
            AtualizarLista(filtrados);
        }

        private void BtnLimparBusca_Click(object sender, EventArgs e)
        {
            txtBuscaCPF.Clear();
            AtualizarLista(todosClientes);
        }

        private void ChkClientes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke(new Action(() => AtualizarContador()));
        }

        private void AtualizarContador()
        {
            int total = chkClientes.CheckedItems.Count;
            lblTotalSelecionados.Text = total + " cliente" + (total != 1 ? "s" : "") + " selecionado" + (total != 1 ? "s" : "");
            lblTotalSelecionados.ForeColor = total > 0 ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60);
        }

        private void BtnCarregar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Selecione a imagem do papel A4";
                ofd.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        picPreview.Image = Image.FromFile(ofd.FileName);
                        templateAtual.CaminhoImagemFundo = ofd.FileName;
                        MessageBox.Show("Imagem carregada!\n\nAgora clique em POSICIONAR TUDO.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao carregar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnPosicionar_Click(object sender, EventArgs e)
        {
            if (picPreview.Image == null)
            {
                MessageBox.Show("Carregue uma imagem primeiro!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            contadorCliques = 0;
            campoAtual = "MODO_MULTIPLO";
            lblInfoPosicoes.Text = "Clique 3 vezes na imagem: (1) Endereco, (2) Cidade, (3) CEP";
            lblInfoPosicoes.ForeColor = Color.FromArgb(46, 204, 113);
            picPreview.Cursor = Cursors.Cross;
        }

        private void PicPreview_Click(object sender, EventArgs e)
        {
            if (picPreview.Image == null || campoAtual != "MODO_MULTIPLO") return;

            MouseEventArgs me = (MouseEventArgs)e;
            float xMm = (me.X / (float)picPreview.Width) * 210;
            float yMm = (me.Y / (float)picPreview.Height) * 297;

            string campo = camposOrdem[contadorCliques];
            templateAtual.Campos.RemoveAll(c => c.Nome == campo);
            templateAtual.AdicionarCampoPadrao(campo, xMm, yMm);

            contadorCliques++;

            if (contadorCliques < camposOrdem.Length)
            {
                lblInfoPosicoes.Text = "Clique " + (contadorCliques + 1) + "/3: " + camposOrdem[contadorCliques];
            }
            else
            {
                lblInfoPosicoes.Text = "Todas as posicoes definidas!";
                lblInfoPosicoes.ForeColor = Color.FromArgb(46, 204, 113);
                campoAtual = "";
                contadorCliques = 0;
                picPreview.Cursor = Cursors.Default;
                MessageBox.Show("Posicoes definidas com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            picPreview.Invalidate();
            AtualizarStatus();
        }

        private void PicPreview_Paint(object sender, PaintEventArgs e)
        {
            if (templateAtual == null || templateAtual.Campos.Count == 0) return;

            Graphics g = e.Graphics;
            foreach (var campo in templateAtual.Campos)
            {
                float xPx = (campo.PosicaoX / 210f) * picPreview.Width;
                float yPx = (campo.PosicaoY / 297f) * picPreview.Height;

                Color cor = campo.Nome == "Endereco" ? Color.FromArgb(155, 89, 182) :
                           campo.Nome == "Cidade" ? Color.FromArgb(230, 126, 34) :
                           Color.FromArgb(231, 76, 60);

                using (Brush brush = new SolidBrush(cor))
                using (Pen pen = new Pen(cor, 2))
                {
                    g.FillEllipse(brush, xPx - 5, yPx - 5, 10, 10);
                    g.DrawLine(pen, xPx - 10, yPx, xPx + 10, yPx);
                    g.DrawLine(pen, xPx, yPx - 10, xPx, yPx + 10);
                    using (Font font = new Font("Segoe UI", 8, FontStyle.Bold))
                    {
                        g.DrawString(campo.Nome, font, brush, xPx + 15, yPx - 8);
                    }
                }
            }
        }

        private void AtualizarStatus()
        {
            var end = templateAtual.Campos.Any(c => c.Nome == "Endereco");
            var cid = templateAtual.Campos.Any(c => c.Nome == "Cidade");
            var cep = templateAtual.Campos.Any(c => c.Nome == "CEP");
            lblStatusPosicoes.Text = "Endereco: " + (end ? "Definido" : "Nao definido") + "\n" +
                                     "Cidade: " + (cid ? "Definido" : "Nao definido") + "\n" +
                                     "CEP: " + (cep ? "Definido" : "Nao definido");
        }

        private void BtnLimpar_Click(object sender, EventArgs e)
        {
            templateAtual.Campos.Clear();
            picPreview.Invalidate();
            AtualizarStatus();
            MessageBox.Show("Posicoes limpas!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNomeTemplate.Text))
                {
                    MessageBox.Show("Digite um nome!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (templateAtual.Campos.Count == 0)
                {
                    MessageBox.Show("Defina as posicoes primeiro!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                templateAtual.Nome = txtNomeTemplate.Text;
                templateDAL.SalvarTemplate(templateAtual);
                MessageBox.Show("Template salvo!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    MessageBox.Show("Selecione clientes!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (templateAtual.Campos.Count == 0)
                {
                    MessageBox.Show("Defina as posicoes!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<Cliente> selecionados = new List<Cliente>();
                string filtro = txtBuscaCPF.Text.Replace("-", "").Replace(".", "").ToUpper();
                List<Cliente> visiveis = string.IsNullOrWhiteSpace(filtro) ? todosClientes :
                    todosClientes.Where(c => c.CPF.Replace("-", "").Replace(".", "").Contains(filtro) ||
                    c.NomeCompleto.ToUpper().Contains(filtro) || c.Cidade.ToUpper().Contains(filtro)).ToList();

                foreach (int i in chkClientes.CheckedIndices)
                {
                    if (i < visiveis.Count) selecionados.Add(visiveis[i]);
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "PDF|*.pdf";
                    sfd.FileName = "MailaDireta_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        MailingPdfGenerator gerador = new MailingPdfGenerator();
                        string arquivo = gerador.GerarPDF(templateAtual, selecionados, Path.GetDirectoryName(sfd.FileName));

                        if (MessageBox.Show("PDF gerado!\n\nTotal: " + selecionados.Count + " paginas\n\nAbrir?", "Sucesso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(arquivo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}