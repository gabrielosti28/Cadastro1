using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Cadastro1
{
    public partial class FormGerenciarBackup : Form
    {
        private BackupManager backupManager;

        public FormGerenciarBackup()
        {
            try
            {
                InitializeComponent();
                backupManager = BackupManager.Instance;

                // CORRIGIDO: Mostrar diretório de backup
                if (lblDiretorio != null)
                {
                    lblDiretorio.Text = $"📁 Backups salvos em: {backupManager.ObterDiretorioBackup()}";
                }

                CarregarBackups();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao iniciar formulário de backup:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CarregarBackups()
        {
            try
            {
                if (lblStatus != null)
                {
                    lblStatus.Text = "Carregando backups...";
                    lblStatus.ForeColor = Color.FromArgb(52, 73, 94);
                }

                BackupInfo[] backups = backupManager.ListarBackups();

                // CORRIGIDO: Verificar se dgvBackups não é nulo
                if (dgvBackups == null)
                {
                    MessageBox.Show("Erro: Grade de backups não foi inicializada corretamente.",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dgvBackups.DataSource = null;
                dgvBackups.DataSource = backups;

                if (dgvBackups.Columns != null && dgvBackups.Columns.Count > 0)
                {
                    if (dgvBackups.Columns.Contains("CaminhoCompleto"))
                        dgvBackups.Columns["CaminhoCompleto"].Visible = false;

                    if (dgvBackups.Columns.Contains("NomeArquivo"))
                        dgvBackups.Columns["NomeArquivo"].HeaderText = "ARQUIVO";

                    if (dgvBackups.Columns.Contains("DataCriacao"))
                    {
                        dgvBackups.Columns["DataCriacao"].HeaderText = "DATA/HORA";
                        dgvBackups.Columns["DataCriacao"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvBackups.Columns["DataCriacao"].Width = 180;
                    }

                    if (dgvBackups.Columns.Contains("TamanhoFormatado"))
                    {
                        dgvBackups.Columns["TamanhoFormatado"].HeaderText = "TAMANHO";
                        dgvBackups.Columns["TamanhoFormatado"].Width = 100;
                    }

                    if (dgvBackups.Columns.Contains("Tipo"))
                    {
                        dgvBackups.Columns["Tipo"].HeaderText = "TIPO";
                        dgvBackups.Columns["Tipo"].Width = 120;
                    }

                    if (dgvBackups.Columns.Contains("TamanhoBytes"))
                        dgvBackups.Columns["TamanhoBytes"].Visible = false;
                }

                if (lblStatus != null)
                {
                    lblStatus.Text = $"✓ Pronto - {backups.Length} backup(s) encontrado(s)";
                    lblStatus.ForeColor = Color.FromArgb(46, 204, 113);
                }
            }
            catch (Exception ex)
            {
                if (lblStatus != null)
                {
                    lblStatus.Text = "✖ Erro ao carregar backups";
                    lblStatus.ForeColor = Color.FromArgb(231, 76, 60);
                }

                MessageBox.Show(
                    $"Erro ao carregar backups:\n\n{ex.Message}\n\n" +
                    $"Verifique o diretório:\n{backupManager.ObterDiretorioBackup()}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void BtnBackupManual_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "Deseja criar um backup MANUAL do banco de dados?\n\n" +
                "O backup será salvo e poderá ser restaurado posteriormente.\n\n" +
                "⚠️ IMPORTANTE: O SQL Server precisa de permissões para criar o arquivo.\n" +
                "Se der erro, verifique as permissões no SQL Server.",
                "Confirmar Backup",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                await RealizarBackupAsync(false);
            }
        }

        private async Task RealizarBackupAsync(bool automatico)
        {
            try
            {
                // Desabilitar botões
                HabilitarBotoes(false);

                if (progressBar != null)
                {
                    progressBar.Visible = true;
                    progressBar.Style = ProgressBarStyle.Marquee;
                }

                if (lblStatus != null)
                {
                    lblStatus.Text = "Realizando backup... Por favor, aguarde.";
                    lblStatus.ForeColor = Color.FromArgb(230, 126, 34);
                }

                string caminho = await Task.Run(() =>
                {
                    return backupManager.RealizarBackup(automatico);
                });

                // Registrar no audit log se disponível
                try
                {
                    AuditLogger.RegistrarBackup(caminho, true);
                }
                catch { }

                if (lblStatus != null)
                {
                    lblStatus.Text = "✓ Backup realizado com sucesso!";
                    lblStatus.ForeColor = Color.FromArgb(46, 204, 113);
                }

                MessageBox.Show(
                    "✓ BACKUP REALIZADO COM SUCESSO!\n\n" +
                    $"Arquivo criado:\n{System.IO.Path.GetFileName(caminho)}\n\n" +
                    "Seus dados estão protegidos.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CarregarBackups();
            }
            catch (Exception ex)
            {
                if (lblStatus != null)
                {
                    lblStatus.Text = "✖ Erro ao realizar backup";
                    lblStatus.ForeColor = Color.FromArgb(231, 76, 60);
                }

                try
                {
                    AuditLogger.RegistrarErro("Backup", ex.Message);
                }
                catch { }

                MessageBox.Show(
                    $"✖ ERRO ao realizar backup:\n\n{ex.Message}\n\n" +
                    "SOLUÇÕES:\n" +
                    "1. Execute o SQL Server como Administrador\n" +
                    "2. Verifique permissões de escrita no disco\n" +
                    "3. Verifique espaço em disco disponível\n" +
                    $"4. Consulte o log em:\n{backupManager.ObterDiretorioBackup()}\\backup_log.txt",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                if (progressBar != null)
                    progressBar.Visible = false;
                HabilitarBotoes(true);
            }
        }

        private async void BtnRestaurar_Click(object sender, EventArgs e)
        {
            if (dgvBackups == null || dgvBackups.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um backup para restaurar!",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BackupInfo backup = (BackupInfo)dgvBackups.SelectedRows[0].DataBoundItem;

            DialogResult resultado = MessageBox.Show(
                "⚠️ ATENÇÃO - OPERAÇÃO CRÍTICA!\n\n" +
                $"Você está prestes a RESTAURAR o banco de dados para:\n" +
                $"Arquivo: {backup.NomeArquivo}\n" +
                $"Data: {backup.DataCriacao:dd/MM/yyyy HH:mm:ss}\n\n" +
                "TODOS OS DADOS ATUAIS SERÃO SUBSTITUÍDOS!\n" +
                "Esta operação não pode ser desfeita!\n\n" +
                "Deseja continuar?",
                "CONFIRMAR RESTAURAÇÃO",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (resultado == DialogResult.Yes)
            {
                await RestaurarBackupAsync(backup);
            }
        }

        private async Task RestaurarBackupAsync(BackupInfo backup)
        {
            try
            {
                HabilitarBotoes(false);

                if (progressBar != null)
                {
                    progressBar.Visible = true;
                    progressBar.Style = ProgressBarStyle.Marquee;
                }

                if (lblStatus != null)
                {
                    lblStatus.Text = "Restaurando backup... NÃO FECHE O PROGRAMA!";
                    lblStatus.ForeColor = Color.FromArgb(231, 76, 60);
                }

                await Task.Run(() =>
                {
                    backupManager.RestaurarBackup(backup.CaminhoCompleto);
                });

                try
                {
                    AuditLogger.RegistrarRestauracao(backup.CaminhoCompleto, true);
                }
                catch { }

                if (lblStatus != null)
                {
                    lblStatus.Text = "✓ Backup restaurado com sucesso!";
                    lblStatus.ForeColor = Color.FromArgb(46, 204, 113);
                }

                MessageBox.Show(
                    "✓ BACKUP RESTAURADO COM SUCESSO!\n\n" +
                    "O sistema será reiniciado.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Reiniciar aplicação
                Application.Restart();
            }
            catch (Exception ex)
            {
                if (lblStatus != null)
                {
                    lblStatus.Text = "✖ Erro ao restaurar backup";
                    lblStatus.ForeColor = Color.FromArgb(231, 76, 60);
                }

                try
                {
                    AuditLogger.RegistrarErro("Restore", ex.Message);
                }
                catch { }

                MessageBox.Show(
                    $"✖ ERRO ao restaurar backup:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                if (progressBar != null)
                    progressBar.Visible = false;
                HabilitarBotoes(true);
            }
        }

        private async void BtnVerificar_Click(object sender, EventArgs e)
        {
            if (dgvBackups == null || dgvBackups.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um backup para verificar!",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BackupInfo backup = (BackupInfo)dgvBackups.SelectedRows[0].DataBoundItem;

            try
            {
                HabilitarBotoes(false);

                if (progressBar != null)
                {
                    progressBar.Visible = true;
                    progressBar.Style = ProgressBarStyle.Marquee;
                }

                if (lblStatus != null)
                {
                    lblStatus.Text = "Verificando integridade...";
                    lblStatus.ForeColor = Color.FromArgb(230, 126, 34);
                }

                bool integro = await Task.Run(() =>
                    backupManager.VerificarIntegridadeBackup(backup.CaminhoCompleto));

                if (integro)
                {
                    if (lblStatus != null)
                    {
                        lblStatus.Text = "✓ Backup verificado - Íntegro";
                        lblStatus.ForeColor = Color.FromArgb(46, 204, 113);
                    }

                    MessageBox.Show(
                        "✓ BACKUP ÍNTEGRO!\n\nO arquivo de backup está OK e pode ser restaurado.",
                        "Verificação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (lblStatus != null)
                    {
                        lblStatus.Text = "✖ Backup corrompido";
                        lblStatus.ForeColor = Color.FromArgb(231, 76, 60);
                    }

                    MessageBox.Show(
                        "✖ BACKUP CORROMPIDO!\n\nO arquivo está danificado e não pode ser restaurado.",
                        "Verificação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                if (lblStatus != null)
                {
                    lblStatus.Text = "✖ Erro na verificação";
                    lblStatus.ForeColor = Color.FromArgb(231, 76, 60);
                }

                MessageBox.Show($"Erro ao verificar backup:\n\n{ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (progressBar != null)
                    progressBar.Visible = false;
                HabilitarBotoes(true);
            }
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvBackups == null || dgvBackups.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um backup para excluir!",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BackupInfo backup = (BackupInfo)dgvBackups.SelectedRows[0].DataBoundItem;

            DialogResult resultado = MessageBox.Show(
                $"Deseja realmente EXCLUIR este backup?\n\n" +
                $"Arquivo: {backup.NomeArquivo}\n" +
                $"Data: {backup.DataCriacao:dd/MM/yyyy HH:mm:ss}\n\n" +
                "Esta ação não pode ser desfeita!",
                "Confirmar Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    System.IO.File.Delete(backup.CaminhoCompleto);

                    if (lblStatus != null)
                    {
                        lblStatus.Text = "✓ Backup excluído";
                        lblStatus.ForeColor = Color.FromArgb(46, 204, 113);
                    }

                    CarregarBackups();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao excluir backup:\n\n{ex.Message}",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnAtualizar_Click(object sender, EventArgs e)
        {
            CarregarBackups();
        }

        private void BtnAbrirPasta_Click(object sender, EventArgs e)
        {
            try
            {
                string diretorio = backupManager.ObterDiretorioBackup();

                if (System.IO.Directory.Exists(diretorio))
                {
                    System.Diagnostics.Process.Start("explorer.exe", diretorio);
                }
                else
                {
                    MessageBox.Show(
                        $"Diretório não encontrado:\n{diretorio}",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
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

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HabilitarBotoes(bool habilitar)
        {
            if (btnBackupManual != null) btnBackupManual.Enabled = habilitar;
            if (btnRestaurar != null) btnRestaurar.Enabled = habilitar;
            if (btnVerificar != null) btnVerificar.Enabled = habilitar;
            if (btnExcluir != null) btnExcluir.Enabled = habilitar;
            if (btnAtualizar != null) btnAtualizar.Enabled = habilitar;
            if (btnAbrirPasta != null) btnAbrirPasta.Enabled = habilitar;
        }
    }
}