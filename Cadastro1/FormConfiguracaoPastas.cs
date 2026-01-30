using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace Cadastro1
{
    public partial class FormConfiguracaoPastas : Form
    {
        public FormConfiguracaoPastas()
        {
            InitializeComponent();
        }

        private void FormConfiguracaoPastas_Load(object sender, EventArgs e)
        {
            CarregarConfiguracoes();
            AtualizarStatus("Pronto", Color.Green);
        }

        private void CarregarConfiguracoes()
        {
            txtBackups.Text = ConfiguracaoPastas.PastaBackups;
            txtAnexos.Text = ConfiguracaoPastas.PastaAnexos;
            txtTemplates.Text = ConfiguracaoPastas.PastaTemplates;
            txtPDFs.Text = ConfiguracaoPastas.PastaPDFs;
            txtLogs.Text = ConfiguracaoPastas.PastaLogs;
        }

        private void BtnBackups_Click(object sender, EventArgs e) =>
            EscolherPasta("Selecione a pasta para BACKUPS", txtBackups);

        private void BtnAnexos_Click(object sender, EventArgs e) =>
            EscolherPasta("Selecione a pasta para ANEXOS", txtAnexos);

        private void BtnTemplates_Click(object sender, EventArgs e) =>
            EscolherPasta("Selecione a pasta para TEMPLATES", txtTemplates);

        private void BtnPDFs_Click(object sender, EventArgs e) =>
            EscolherPasta("Selecione a pasta para PDFs", txtPDFs);

        private void BtnLogs_Click(object sender, EventArgs e) =>
            EscolherPasta("Selecione a pasta para LOGS", txtLogs);

        private void EscolherPasta(string descricao, TextBox textBox)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = descricao;
                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textBox.Text = dialog.SelectedPath;
                    AtualizarStatus("Pasta atualizada. Clique em SALVAR.", Color.Blue);
                }
            }
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                ConfiguracaoPastas.PastaBackups = txtBackups.Text;
                ConfiguracaoPastas.PastaAnexos = txtAnexos.Text;
                ConfiguracaoPastas.PastaTemplates = txtTemplates.Text;
                ConfiguracaoPastas.PastaPDFs = txtPDFs.Text;
                ConfiguracaoPastas.PastaLogs = txtLogs.Text;

                CriarPastas();

                MessageBox.Show("✓ Configurações salvas!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                AtualizarStatus("Salvo com sucesso!", Color.Green);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                AtualizarStatus("Erro ao salvar.", Color.Red);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtBackups.Text) ||
                string.IsNullOrWhiteSpace(txtAnexos.Text) ||
                string.IsNullOrWhiteSpace(txtTemplates.Text) ||
                string.IsNullOrWhiteSpace(txtPDFs.Text) ||
                string.IsNullOrWhiteSpace(txtLogs.Text))
            {
                MessageBox.Show("Configure todas as pastas!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void CriarPastas()
        {
            CriarPasta(txtBackups.Text);
            CriarPasta(txtAnexos.Text);
            CriarPasta(txtTemplates.Text);
            CriarPasta(txtPDFs.Text);
            CriarPasta(txtLogs.Text);
        }

        private void CriarPasta(string caminho)
        {
            if (!Directory.Exists(caminho))
                Directory.CreateDirectory(caminho);
        }

        private void BtnRestaurarPadrao_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Restaurar valores padrão?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ConfiguracaoPastas.ResetarParaPadrao();
                CarregarConfiguracoes();
                MessageBox.Show("Valores restaurados. Clique em SALVAR.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                AtualizarStatus("Restaurado para padrão.", Color.Orange);
            }
        }

        private void BtnTestarPermissoes_Click(object sender, EventArgs e)
        {
            var resultado = new System.Text.StringBuilder("TESTE DE PERMISSÕES:\n\n");

            bool todasOk = true;
            todasOk &= TestarPasta(txtBackups.Text, "Backups", resultado);
            todasOk &= TestarPasta(txtAnexos.Text, "Anexos", resultado);
            todasOk &= TestarPasta(txtTemplates.Text, "Templates", resultado);
            todasOk &= TestarPasta(txtPDFs.Text, "PDFs", resultado);
            todasOk &= TestarPasta(txtLogs.Text, "Logs", resultado);

            resultado.AppendLine(todasOk ? "\n✓ TODAS OK!" : "\n✖ HÁ PROBLEMAS!");

            MessageBox.Show(resultado.ToString(), "Teste de Permissões",
                MessageBoxButtons.OK, todasOk ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            AtualizarStatus(todasOk ? "Todas OK!" : "Há problemas.", todasOk ? Color.Green : Color.Red);
        }

        private bool TestarPasta(string caminho, string nome, System.Text.StringBuilder resultado)
        {
            try
            {
                if (!Directory.Exists(caminho))
                    Directory.CreateDirectory(caminho);

                string teste = Path.Combine(caminho, "_teste.tmp");
                File.WriteAllText(teste, "teste");
                File.Delete(teste);

                resultado.AppendLine($"✓ {nome}: OK");
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                resultado.AppendLine($"✖ {nome}: SEM PERMISSÃO");
                return false;
            }
            catch (Exception ex)
            {
                resultado.AppendLine($"✖ {nome}: {ex.Message}");
                return false;
            }
        }

        private void AtualizarStatus(string mensagem, Color cor)
        {
            lblStatus.Text = mensagem;
            lblStatus.ForeColor = cor;
        }

        private void BtnFechar_Click(object sender, EventArgs e) => Close();
    }
}