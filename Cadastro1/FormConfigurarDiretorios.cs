// =============================================
// FORMULÁRIO DE CONFIGURAÇÃO DE DIRETÓRIOS
// Arquivo: FormConfigurarDiretorios.cs
// Permite configurar visualmente todas as pastas do sistema
// =============================================
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Cadastro1
{
    public partial class FormConfigurarDiretorios : Form
    {
        private bool alteracoesFeitas = false;

        public FormConfigurarDiretorios()
        {
            InitializeComponent();
        }

        private void FormConfigurarDiretorios_Load(object sender, EventArgs e)
        {
            CarregarConfiguracoes();
        }

        /// <summary>
        /// Carrega as configurações atuais nos campos
        /// </summary>
        private void CarregarConfiguracoes()
        {
            try
            {
                txtBackups.Text = ConfiguracaoPastas.PastaBackups;
                txtAnexos.Text = ConfiguracaoPastas.PastaAnexos;
                txtTemplates.Text = ConfiguracaoPastas.PastaTemplates;
                txtPDFs.Text = ConfiguracaoPastas.PastaPDFs;
                txtLogs.Text = ConfiguracaoPastas.PastaLogs;

                alteracoesFeitas = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao carregar configurações:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ========== BOTÕES DE ESCOLHER PASTA ==========

        private void BtnEscolherBackups_Click(object sender, EventArgs e)
        {
            string pasta = EscolherPasta(
                "Escolha a pasta para salvar os BACKUPS do banco de dados",
                txtBackups.Text);

            if (!string.IsNullOrEmpty(pasta))
            {
                txtBackups.Text = pasta;
                alteracoesFeitas = true;
            }
        }

        private void BtnEscolherAnexos_Click(object sender, EventArgs e)
        {
            string pasta = EscolherPasta(
                "Escolha a pasta para salvar os ANEXOS dos clientes",
                txtAnexos.Text);

            if (!string.IsNullOrEmpty(pasta))
            {
                txtAnexos.Text = pasta;
                alteracoesFeitas = true;
            }
        }

        private void BtnEscolherTemplates_Click(object sender, EventArgs e)
        {
            string pasta = EscolherPasta(
                "Escolha a pasta para salvar os TEMPLATES de mala direta",
                txtTemplates.Text);

            if (!string.IsNullOrEmpty(pasta))
            {
                txtTemplates.Text = pasta;
                alteracoesFeitas = true;
            }
        }

        private void BtnEscolherPDFs_Click(object sender, EventArgs e)
        {
            string pasta = EscolherPasta(
                "Escolha a pasta para salvar os PDFs de mala direta",
                txtPDFs.Text);

            if (!string.IsNullOrEmpty(pasta))
            {
                txtPDFs.Text = pasta;
                alteracoesFeitas = true;
            }
        }

        private void BtnEscolherLogs_Click(object sender, EventArgs e)
        {
            string pasta = EscolherPasta(
                "Escolha a pasta para salvar os LOGS do sistema",
                txtLogs.Text);

            if (!string.IsNullOrEmpty(pasta))
            {
                txtLogs.Text = pasta;
                alteracoesFeitas = true;
            }
        }

        /// <summary>
        /// Método auxiliar para escolher pasta
        /// </summary>
        private string EscolherPasta(string descricao, string pastaAtual)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = descricao;
                dialog.ShowNewFolderButton = true;

                // Se já existe uma pasta configurada, começar por ela
                if (!string.IsNullOrEmpty(pastaAtual) && Directory.Exists(pastaAtual))
                {
                    dialog.SelectedPath = pastaAtual;
                }

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.SelectedPath;
                }
            }

            return null;
        }

        // ========== BOTÕES DE ABRIR PASTA ==========

        private void BtnAbrirBackups_Click(object sender, EventArgs e)
        {
            AbrirPastaNoExplorador(txtBackups.Text, "Backups");
        }

        private void BtnAbrirAnexos_Click(object sender, EventArgs e)
        {
            AbrirPastaNoExplorador(txtAnexos.Text, "Anexos");
        }

        private void BtnAbrirTemplates_Click(object sender, EventArgs e)
        {
            AbrirPastaNoExplorador(txtTemplates.Text, "Templates");
        }

        private void BtnAbrirPDFs_Click(object sender, EventArgs e)
        {
            AbrirPastaNoExplorador(txtPDFs.Text, "PDFs");
        }

        private void BtnAbrirLogs_Click(object sender, EventArgs e)
        {
            AbrirPastaNoExplorador(txtLogs.Text, "Logs");
        }

        /// <summary>
        /// Abre a pasta no Windows Explorer
        /// </summary>
        private void AbrirPastaNoExplorador(string caminho, string nomePasta)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(caminho))
                {
                    MessageBox.Show(
                        $"A pasta de {nomePasta} ainda não foi configurada.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Criar a pasta se não existir
                if (!Directory.Exists(caminho))
                {
                    Directory.CreateDirectory(caminho);
                }

                // Abrir no explorer
                Process.Start("explorer.exe", caminho);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao abrir pasta:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ========== BOTÕES DE AÇÃO ==========

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
                    MessageBox.Show(
                        "⚠️ ATENÇÃO\n\n" +
                        "Todas as pastas devem ser configuradas.\n" +
                        "Configure cada pasta antes de salvar.",
                        "Configuração Incompleta",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Testar permissões antes de salvar
                if (!TestarTodasPermissoes())
                {
                    DialogResult result = MessageBox.Show(
                        "⚠️ PROBLEMA DE PERMISSÕES DETECTADO\n\n" +
                        "Algumas pastas não têm permissão de escrita.\n\n" +
                        "Deseja salvar mesmo assim?\n" +
                        "(Não recomendado - pode causar erros no sistema)",
                        "Aviso de Permissões",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                        return;
                }

                // Salvar configurações
                ConfiguracaoPastas.PastaBackups = txtBackups.Text;
                ConfiguracaoPastas.PastaAnexos = txtAnexos.Text;
                ConfiguracaoPastas.PastaTemplates = txtTemplates.Text;
                ConfiguracaoPastas.PastaPDFs = txtPDFs.Text;
                ConfiguracaoPastas.PastaLogs = txtLogs.Text;

                // Criar todas as pastas
                ConfiguracaoPastas.GarantirPastasExistem();

                MessageBox.Show(
                    "✅ CONFIGURAÇÃO SALVA COM SUCESSO!\n\n" +
                    "As pastas foram configuradas e criadas.\n\n" +
                    "📁 Backups: " + Path.GetFileName(txtBackups.Text) + "\n" +
                    "📎 Anexos: " + Path.GetFileName(txtAnexos.Text) + "\n" +
                    "📄 Templates: " + Path.GetFileName(txtTemplates.Text) + "\n" +
                    "📮 PDFs: " + Path.GetFileName(txtPDFs.Text) + "\n" +
                    "📝 Logs: " + Path.GetFileName(txtLogs.Text),
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                alteracoesFeitas = false;
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ ERRO AO SALVAR CONFIGURAÇÕES\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnRestaurar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "🔄 RESTAURAR CONFIGURAÇÕES PADRÃO\n\n" +
                "Isso irá redefinir todas as pastas para os locais padrão:\n" +
                "• Documentos/SistemaCadastroClientes/...\n\n" +
                "Deseja continuar?",
                "Confirmar Restauração",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    ConfiguracaoPastas.ResetarParaPadrao();
                    CarregarConfiguracoes();

                    MessageBox.Show(
                        "✅ Configurações restauradas para o padrão!\n\n" +
                        "Clique em SALVAR para confirmar as alterações.",
                        "Restaurado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    alteracoesFeitas = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Erro ao restaurar configurações:\n\n{ex.Message}",
                        "Erro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            if (alteracoesFeitas)
            {
                DialogResult result = MessageBox.Show(
                    "⚠️ ALTERAÇÕES NÃO SALVAS\n\n" +
                    "Você fez alterações que não foram salvas.\n\n" +
                    "Deseja sair sem salvar?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;
            }

            this.Close();
        }

        // ========== MÉTODOS AUXILIARES ==========

        /// <summary>
        /// Testa permissões de escrita em todas as pastas
        /// </summary>
        private bool TestarTodasPermissoes()
        {
            bool todasOK = true;

            todasOK &= TestarPermissao(txtBackups.Text);
            todasOK &= TestarPermissao(txtAnexos.Text);
            todasOK &= TestarPermissao(txtTemplates.Text);
            todasOK &= TestarPermissao(txtPDFs.Text);
            todasOK &= TestarPermissao(txtLogs.Text);

            return todasOK;
        }

        /// <summary>
        /// Testa permissão de escrita em uma pasta
        /// </summary>
        private bool TestarPermissao(string caminho)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(caminho))
                    return false;

                // Criar pasta se não existir
                if (!Directory.Exists(caminho))
                {
                    Directory.CreateDirectory(caminho);
                }

                // Tentar criar e deletar arquivo de teste
                string arquivoTeste = Path.Combine(caminho, $"_teste_{Guid.NewGuid()}.tmp");
                File.WriteAllText(arquivoTeste, "teste");
                File.Delete(arquivoTeste);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Obtém o ícone de status da pasta
        /// </summary>
        private string ObterIconeStatus(string caminho)
        {
            if (string.IsNullOrWhiteSpace(caminho))
                return "❌";

            if (!Directory.Exists(caminho))
                return "⚠️";

            if (!TestarPermissao(caminho))
                return "🔒";

            return "✅";
        }

        /// <summary>
        /// Formata o caminho para exibição
        /// </summary>
        private string FormatarCaminho(string caminho)
        {
            if (string.IsNullOrWhiteSpace(caminho))
                return "[Não configurado]";

            try
            {
                return Path.GetFullPath(caminho);
            }
            catch
            {
                return caminho;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormConfiguracaoPastas form = new FormConfiguracaoPastas();
            form.ShowDialog();
        }
    }
}