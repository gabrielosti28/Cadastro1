// =============================================
// FORMULÁRIO DE CONFIGURAÇÃO DE PASTAS
// Arquivo: FormConfiguracaoPastas.cs
// Lógica completa para configurar todas as pastas do sistema
// =============================================
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

        // Carrega as configurações atuais ao abrir o formulário
        private void FormConfiguracaoPastas_Load(object sender, EventArgs e)
        {
            CarregarConfiguracoes();
            AtualizarStatus("Configurações carregadas com sucesso.", Color.Green);
        }

        // Carrega as pastas configuradas nos TextBox
        private void CarregarConfiguracoes()
        {
            txtBackups.Text = ConfiguracaoPastas.PastaBackups;
            txtAnexos.Text = ConfiguracaoPastas.PastaAnexos;
            txtTemplates.Text = ConfiguracaoPastas.PastaTemplates;
            txtPDFs.Text = ConfiguracaoPastas.PastaPDFs;
            txtLogs.Text = ConfiguracaoPastas.PastaLogs;
        }

        // Escolher pasta de Backups
        private void BtnBackups_Click(object sender, EventArgs e)
        {
            string pastaEscolhida = EscolherPasta("Selecione a pasta para BACKUPS do banco de dados");
            if (!string.IsNullOrEmpty(pastaEscolhida))
            {
                txtBackups.Text = pastaEscolhida;
                AtualizarStatus("Pasta de backups atualizada. Clique em SALVAR para confirmar.", Color.Blue);
            }
        }

        // Escolher pasta de Anexos
        private void BtnAnexos_Click(object sender, EventArgs e)
        {
            string pastaEscolhida = EscolherPasta("Selecione a pasta para ANEXOS dos clientes");
            if (!string.IsNullOrEmpty(pastaEscolhida))
            {
                txtAnexos.Text = pastaEscolhida;
                AtualizarStatus("Pasta de anexos atualizada. Clique em SALVAR para confirmar.", Color.Blue);
            }
        }

        // Escolher pasta de Templates
        private void BtnTemplates_Click(object sender, EventArgs e)
        {
            string pastaEscolhida = EscolherPasta("Selecione a pasta para TEMPLATES de mala direta");
            if (!string.IsNullOrEmpty(pastaEscolhida))
            {
                txtTemplates.Text = pastaEscolhida;
                AtualizarStatus("Pasta de templates atualizada. Clique em SALVAR para confirmar.", Color.Blue);
            }
        }

        // Escolher pasta de PDFs
        private void BtnPDFs_Click(object sender, EventArgs e)
        {
            string pastaEscolhida = EscolherPasta("Selecione a pasta para PDFs de mala direta");
            if (!string.IsNullOrEmpty(pastaEscolhida))
            {
                txtPDFs.Text = pastaEscolhida;
                AtualizarStatus("Pasta de PDFs atualizada. Clique em SALVAR para confirmar.", Color.Blue);
            }
        }

        // Escolher pasta de Logs
        private void BtnLogs_Click(object sender, EventArgs e)
        {
            string pastaEscolhida = EscolherPasta("Selecione a pasta para LOGS do sistema");
            if (!string.IsNullOrEmpty(pastaEscolhida))
            {
                txtLogs.Text = pastaEscolhida;
                AtualizarStatus("Pasta de logs atualizada. Clique em SALVAR para confirmar.", Color.Blue);
            }
        }

        // Método auxiliar para abrir o FolderBrowserDialog
        private string EscolherPasta(string descricao)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = descricao;
                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.SelectedPath;
                }
            }
            return null;
        }

        // Salvar as configurações
        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar se todas as pastas foram preenchidas
                if (string.IsNullOrWhiteSpace(txtBackups.Text) ||
                    string.IsNullOrWhiteSpace(txtAnexos.Text) ||
                    string.IsNullOrWhiteSpace(txtTemplates.Text) ||
                    string.IsNullOrWhiteSpace(txtPDFs.Text) ||
                    string.IsNullOrWhiteSpace(txtLogs.Text))
                {
                    MessageBox.Show("Por favor, configure todas as pastas antes de salvar.",
                        "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Salvar cada pasta
                ConfiguracaoPastas.PastaBackups = txtBackups.Text;
                ConfiguracaoPastas.PastaAnexos = txtAnexos.Text;
                ConfiguracaoPastas.PastaTemplates = txtTemplates.Text;
                ConfiguracaoPastas.PastaPDFs = txtPDFs.Text;
                ConfiguracaoPastas.PastaLogs = txtLogs.Text;

                // Criar as pastas se não existirem
                CriarPastasSeNecessario();

                MessageBox.Show("✓ Configurações salvas com sucesso!\n\n" +
                    "As pastas foram criadas e estão prontas para uso.",
                    "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                AtualizarStatus("Configurações salvas com sucesso!", Color.Green);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar configurações:\n{ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AtualizarStatus("Erro ao salvar configurações.", Color.Red);
            }
        }

        // Criar todas as pastas se não existirem
        private void CriarPastasSeNecessario()
        {
            CriarPasta(txtBackups.Text, "Backups");
            CriarPasta(txtAnexos.Text, "Anexos");
            CriarPasta(txtTemplates.Text, "Templates");
            CriarPasta(txtPDFs.Text, "PDFs");
            CriarPasta(txtLogs.Text, "Logs");
        }

        // Criar uma pasta específica
        private void CriarPasta(string caminho, string nome)
        {
            try
            {
                if (!Directory.Exists(caminho))
                {
                    Directory.CreateDirectory(caminho);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar pasta de {nome}:\n{ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Restaurar configurações padrão
        private void BtnRestaurarPadrao_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Deseja realmente restaurar as configurações padrão?\n\n" +
                "Esta ação irá redefinir todas as pastas para os locais padrão do sistema.",
                "Confirmar Restauração",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Resetar para valores padrão
                    ConfiguracaoPastas.ResetarParaPadrao();

                    // Recarregar as configurações
                    CarregarConfiguracoes();

                    MessageBox.Show("Configurações restauradas para os valores padrão.\n\n" +
                        "Não esqueça de clicar em SALVAR se desejar manter estas configurações.",
                        "Restaurado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    AtualizarStatus("Configurações restauradas para padrão.", Color.Orange);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao restaurar configurações:\n{ex.Message}",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Testar permissões de escrita em todas as pastas
        private void BtnTestarPermissoes_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder resultado = new System.Text.StringBuilder();
            resultado.AppendLine("TESTE DE PERMISSÕES:\n");

            bool todasOk = true;

            // Testar cada pasta
            todasOk &= TestarPasta(txtBackups.Text, "Backups", resultado);
            todasOk &= TestarPasta(txtAnexos.Text, "Anexos", resultado);
            todasOk &= TestarPasta(txtTemplates.Text, "Templates", resultado);
            todasOk &= TestarPasta(txtPDFs.Text, "PDFs", resultado);
            todasOk &= TestarPasta(txtLogs.Text, "Logs", resultado);

            if (todasOk)
            {
                resultado.AppendLine("\n✓ TODAS AS PASTAS ESTÃO OK!");
                MessageBox.Show(resultado.ToString(), "Teste de Permissões",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                AtualizarStatus("Teste concluído: Todas as pastas OK!", Color.Green);
            }
            else
            {
                resultado.AppendLine("\n✖ ALGUMAS PASTAS APRESENTARAM PROBLEMAS!");
                resultado.AppendLine("Verifique as permissões ou escolha outras pastas.");
                MessageBox.Show(resultado.ToString(), "Teste de Permissões",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AtualizarStatus("Teste concluído: Há problemas com permissões.", Color.Red);
            }
        }

        // Testar uma pasta específica
        private bool TestarPasta(string caminho, string nome, System.Text.StringBuilder resultado)
        {
            try
            {
                // Verificar se a pasta existe ou pode ser criada
                if (!Directory.Exists(caminho))
                {
                    Directory.CreateDirectory(caminho);
                }

                // Testar escrita
                string arquivoTeste = Path.Combine(caminho, "_teste_permissao.tmp");
                File.WriteAllText(arquivoTeste, "teste");
                File.Delete(arquivoTeste);

                resultado.AppendLine($"✓ {nome}: OK - Leitura e escrita funcionando");
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                resultado.AppendLine($"✖ {nome}: SEM PERMISSÃO - Escolha outra pasta");
                return false;
            }
            catch (Exception ex)
            {
                resultado.AppendLine($"✖ {nome}: ERRO - {ex.Message}");
                return false;
            }
        }

        // Atualizar mensagem de status
        private void AtualizarStatus(string mensagem, Color cor)
        {
            lblStatus.Text = mensagem;
            lblStatus.ForeColor = cor;
        }

        // Fechar o formulário
        private void BtnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}