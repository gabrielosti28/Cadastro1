// =============================================
// FORMULÁRIO DE CONFIGURAÇÃO DE PASTAS
// Arquivo: FormConfiguracaoPastas.cs
// VERSÃO CORRIGIDA - Com validação de permissões SQL
// =============================================
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Cadastro1
{
    public partial class FormConfiguracaoPastas : Form
    {
        public FormConfiguracaoPastas()
        {
            InitializeComponent();
            ConfigurarFormulario();
            CarregarConfiguracoesAtuais();
        }

        private void ConfigurarFormulario()
        {
            // Aplicar efeitos hover nos botões
            btnEscolherBackups.MouseEnter += Botao_MouseEnter;
            btnEscolherBackups.MouseLeave += Botao_MouseLeave;
            btnEscolherAnexos.MouseEnter += Botao_MouseEnter;
            btnEscolherAnexos.MouseLeave += Botao_MouseLeave;
            btnTestarPermissoes.MouseEnter += Botao_MouseEnter;
            btnTestarPermissoes.MouseLeave += Botao_MouseLeave;
            btnRestaurarPadrao.MouseEnter += Botao_MouseEnter;
            btnRestaurarPadrao.MouseLeave += Botao_MouseLeave;
            btnSalvar.MouseEnter += Botao_MouseEnter;
            btnSalvar.MouseLeave += Botao_MouseLeave;
            btnCancelar.MouseEnter += Botao_MouseEnter;
            btnCancelar.MouseLeave += Botao_MouseLeave;
        }

        private void CarregarConfiguracoesAtuais()
        {
            try
            {
                txtPastaBackups.Text = ConfiguracaoPastas.PastaBackups;
                txtPastaAnexos.Text = ConfiguracaoPastas.PastaAnexos;

                VerificarPermissoes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar configurações:\n\n{ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEscolherBackups_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Selecione a pasta para salvar BACKUPS";
                fbd.ShowNewFolderButton = true;

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtPastaBackups.Text = fbd.SelectedPath;
                    VerificarPermissaoPasta(fbd.SelectedPath, lblStatusBackups);
                }
            }
        }

        private void BtnEscolherAnexos_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Selecione a pasta para salvar ANEXOS";
                fbd.ShowNewFolderButton = true;

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtPastaAnexos.Text = fbd.SelectedPath;
                    VerificarPermissaoPasta(fbd.SelectedPath, lblStatusAnexos);
                }
            }
        }

        private void BtnTestarPermissoes_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPastaBackups.Text))
            {
                MessageBox.Show("Selecione a pasta de backups primeiro!",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Cursor = Cursors.WaitCursor;
            btnTestarPermissoes.Enabled = false;
            btnTestarPermissoes.Text = "⏳ Testando...";

            try
            {
                // Testar permissões do SQL Server
                TestarPermissoesSQLServer(txtPastaBackups.Text);
            }
            finally
            {
                btnTestarPermissoes.Enabled = true;
                btnTestarPermissoes.Text = "🔍 TESTAR PERMISSÕES SQL";
                Cursor = Cursors.Default;
            }
        }

        private void TestarPermissoesSQLServer(string caminho)
        {
            try
            {
                // Garantir que a pasta existe
                if (!Directory.Exists(caminho))
                {
                    Directory.CreateDirectory(caminho);
                }

                // Criar arquivo de teste
                string arquivoTeste = Path.Combine(caminho, $"teste_sql_{Guid.NewGuid()}.tmp");

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Tentar criar um backup de teste
                    string sqlTeste = $@"
                        BACKUP DATABASE [projeto1]
                        TO DISK = @CaminhoTeste
                        WITH FORMAT, INIT, SKIP, NOREWIND, NOUNLOAD";

                    using (SqlCommand cmd = new SqlCommand(sqlTeste, conn))
                    {
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@CaminhoTeste", arquivoTeste);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Limpar arquivo de teste
                if (File.Exists(arquivoTeste))
                {
                    File.Delete(arquivoTeste);
                }

                lblStatusBackups.Text = "✅ SQL Server TEM permissão para gravar nesta pasta!";
                lblStatusBackups.ForeColor = Color.FromArgb(46, 204, 113);

                MessageBox.Show(
                    "✅ TESTE BEM-SUCEDIDO!\n\n" +
                    "O SQL Server conseguiu gravar um backup de teste nesta pasta.\n" +
                    "A configuração está correta!",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                lblStatusBackups.Text = "❌ SQL Server NÃO tem permissão nesta pasta!";
                lblStatusBackups.ForeColor = Color.FromArgb(231, 76, 60);

                string mensagemErro = "❌ ERRO DE PERMISSÃO SQL SERVER\n\n";

                if (ex.Message.Contains("Operating system error 5"))
                {
                    mensagemErro +=
                        "O SQL Server não consegue gravar nesta pasta.\n\n" +
                        "SOLUÇÕES:\n\n" +
                        "1️⃣ USE A PASTA PADRÃO (Recomendado)\n" +
                        "   Clique em 'Restaurar Padrão' e teste novamente\n\n" +
                        "2️⃣ CONFIGURE PERMISSÕES MANUALMENTE:\n" +
                        "   • Botão direito na pasta > Propriedades\n" +
                        "   • Guia Segurança > Editar > Adicionar\n" +
                        "   • Digite: NT SERVICE\\MSSQLSERVER\n" +
                        "   • Marque 'Controle Total'\n" +
                        "   • Aplique e teste novamente\n\n" +
                        $"Pasta testada:\n{caminho}";
                }
                else
                {
                    mensagemErro += $"Erro SQL:\n{ex.Message}";
                }

                MessageBox.Show(mensagemErro, "Erro de Permissão",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao testar permissões:\n\n{ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VerificarPermissoes()
        {
            VerificarPermissaoPasta(txtPastaBackups.Text, lblStatusBackups);
            VerificarPermissaoPasta(txtPastaAnexos.Text, lblStatusAnexos);
        }

        private void VerificarPermissaoPasta(string caminho, Label labelStatus)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(caminho))
                {
                    labelStatus.Text = "⚠️ Pasta não configurada";
                    labelStatus.ForeColor = Color.Gray;
                    return;
                }

                // Testar permissão de escrita do Windows
                if (!Directory.Exists(caminho))
                {
                    Directory.CreateDirectory(caminho);
                }

                string arquivoTeste = Path.Combine(caminho, $"test_{Guid.NewGuid()}.tmp");
                File.WriteAllText(arquivoTeste, "teste");
                File.Delete(arquivoTeste);

                labelStatus.Text = $"✓ Windows OK: {caminho}";
                labelStatus.ForeColor = Color.FromArgb(46, 204, 113);
            }
            catch (Exception)
            {
                labelStatus.Text = "❌ Sem permissão de escrita do Windows";
                labelStatus.ForeColor = Color.FromArgb(231, 76, 60);
            }
        }

        private void BtnRestaurarPadrao_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "Restaurar configurações padrão?\n\n" +
                "As pastas serão configuradas para:\n" +
                "Documentos/SistemaCadastroClientes/...",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                ConfiguracaoPastas.ResetarParaPadrao();
                CarregarConfiguracoesAtuais();

                MessageBox.Show(
                    "✓ Configurações restauradas!\n\n" +
                    "Agora clique em 'Testar Permissões SQL' para verificar.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPastaBackups.Text) ||
                    string.IsNullOrWhiteSpace(txtPastaAnexos.Text))
                {
                    MessageBox.Show(
                        "Configure todas as pastas antes de salvar!",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Salvar configurações
                ConfiguracaoPastas.PastaBackups = txtPastaBackups.Text;
                ConfiguracaoPastas.PastaAnexos = txtPastaAnexos.Text;

                // Garantir que as pastas existem
                ConfiguracaoPastas.GarantirPastasExistem();

                MessageBox.Show(
                    "✅ Configurações salvas com sucesso!\n\n" +
                    "IMPORTANTE: Teste os backups antes de usar em produção.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao salvar configurações:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Botao_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.BackColor = ControlPaint.Dark(btn.BackColor, 0.1f);
            }
        }

        private void Botao_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn == btnSalvar)
                    btn.BackColor = Color.FromArgb(46, 204, 113);
                else if (btn == btnCancelar)
                    btn.BackColor = Color.FromArgb(231, 76, 60);
                else if (btn == btnEscolherBackups)
                    btn.BackColor = Color.FromArgb(41, 128, 185);
                else if (btn == btnEscolherAnexos)
                    btn.BackColor = Color.FromArgb(155, 89, 182);
                else if (btn == btnTestarPermissoes)
                    btn.BackColor = Color.FromArgb(230, 126, 34);
                else if (btn == btnRestaurarPadrao)
                    btn.BackColor = Color.FromArgb(149, 165, 166);
            }
        }
    }
}