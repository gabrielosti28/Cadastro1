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
        private List<Cliente> clientesSelecionados;

        public FormMailingEditor()
        {
            // Inicializar variáveis primeiro
            templateDAL = new MailingTemplateDAL();
            clienteDAL = new ClienteDAL();
            clientesSelecionados = new List<Cliente>();

            // Agora configurar interface
            InitializeComponent();

            // Criar template padrão
            templateAtual = MailingTemplate.CriarTemplatePadrao();
            CarregarClientes();
        }

        private void CarregarClientes()
        {
            try
            {
                var clientes = clienteDAL.ListarTodosClientes();
                chkClientes.Items.Clear();

                foreach (var cliente in clientes)
                {
                    string item = $"{cliente.NomeCompleto} - {cliente.CPF} - {cliente.Cidade}";
                    chkClientes.Items.Add(item, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar clientes:\n\n{ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCarregarImagem_Click(object sender, EventArgs e)
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

                        MessageBox.Show(
                            "✓ Imagem carregada!\n\n" +
                            "Agora clique na imagem onde deseja colocar:\n" +
                            "• Nome do cliente\n" +
                            "• Endereço\n" +
                            "• Cidade\n" +
                            "• CEP",
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
                MessageBox.Show("Carregue uma imagem primeiro!",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MouseEventArgs me = (MouseEventArgs)e;

            // Converter coordenadas de pixel para mm
            float xMm = (me.X / (float)picPreview.Width) * 210; // A4 = 210mm largura
            float yMm = (me.Y / (float)picPreview.Height) * 297; // A4 = 297mm altura

            MessageBox.Show(
                $"Posição clicada:\n\nX: {xMm:F1}mm\nY: {yMm:F1}mm\n\n" +
                $"Anote essas coordenadas para usar no template!",
                "Coordenadas",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnSalvarTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNomeTemplate.Text))
                {
                    MessageBox.Show("Digite um nome para o template!",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                templateAtual.Nome = txtNomeTemplate.Text;
                templateDAL.SalvarTemplate(templateAtual);

                MessageBox.Show(
                    "✓ Template salvo com sucesso!\n\n" +
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

        private void BtnSelecionarClientes_Click(object sender, EventArgs e)
        {
            bool marcarTodos = chkClientes.CheckedItems.Count == 0;

            for (int i = 0; i < chkClientes.Items.Count; i++)
            {
                chkClientes.SetItemChecked(i, marcarTodos);
            }

            btnSelecionarClientes.Text = marcarTodos ? "✗ Desmarcar Todos" : "✓ Marcar Todos";
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

                // Obter clientes selecionados
                List<Cliente> clientesSelecionados = new List<Cliente>();
                var todosClientes = clienteDAL.ListarTodosClientes();

                foreach (int index in chkClientes.CheckedIndices)
                {
                    clientesSelecionados.Add(todosClientes[index]);
                }

                // Escolher onde salvar
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "PDF|*.pdf";
                    sfd.FileName = $"MailaDireta_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        MailingPdfGenerator gerador = new MailingPdfGenerator();
                        string caminhoSaida = Path.GetDirectoryName(sfd.FileName);

                        string arquivoGerado = gerador.GerarPDF(templateAtual, clientesSelecionados, caminhoSaida);

                        DialogResult resultado = MessageBox.Show(
                            $"✓ PDF gerado com sucesso!\n\n" +
                            $"Total de páginas: {clientesSelecionados.Count}\n" +
                            $"Arquivo: {Path.GetFileName(arquivoGerado)}\n\n" +
                            "Deseja abrir o arquivo?",
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
                MessageBox.Show($"Erro ao gerar PDF:\n\n{ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}