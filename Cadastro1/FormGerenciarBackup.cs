using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Cadastro1
{
    public partial class FormGerenciarBackup : Form
    {
        private BackupManager backupManager;
        private Button btnConfigurarPasta;

        public FormGerenciarBackup()
        {
            try
            {
                InitializeComponent();
                backupManager = BackupManager.Instance;

                // Adicionar botão para configurar pasta
                AdicionarBotaoConfigurarPasta();

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

        private void AdicionarBotaoConfigurarPasta()
        {
            // Criar botão para configurar pasta
            btnConfigurarPasta = new Button
            {
                BackColor = Color.FromArgb(41, 128, 185),
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, 500),
                Name = "btnConfigurarPasta",
                Size = new Size(180, 40),
                TabIndex = 7,
                Text = "📂 Configurar Pasta",
                UseVisualStyleBackColor = false
            };

            btnConfigurarPasta.FlatAppearance.BorderSize = 0;
            btnConfigurarPasta.Click += BtnConfigurarPasta_Click;

            // Adicionar ao painel de botões se existir, senão adicionar direto no form
            if (panelBotoes != null)
            {
                panelBotoes.Controls.Add(btnConfigurarPasta);
            }
            else if (panelContainer != null)
            {
                panelContainer.Controls.Add(btnConfigurarPasta);
                btnConfigurarPasta.BringToFront();
            }
        }

        private void BtnConfigurarPasta_Click(object sender, EventArgs e)
        {
            try
            {
                if (backupManager.EscolherDiretorioBackup())
                {
                    // Atualizar label com novo diretório
                    if (lblDiretorio != null)
                    {
                        lblDiretorio.Text = $"📁 Backups salvos em: {backupManager.ObterDiretorioBackup()}";
                    }

                    // Recarregar lista de backups
                    CarregarBackups();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao configurar pasta:\n\n{ex.Message}",
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

                if (dgvBackups == null)
                {
                    MessageBox.Show("Erro: Grade de backups não foi inicializada.",
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
                    {
                        dgvBackups.Columns["NomeArquivo"].HeaderText = "ARQUIVO";
                        dgvBackups.Columns["NomeArquivo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }

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
                    $"Erro ao carregar backups:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void BtnBackupManual_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "📌 CRIAR BACKUP MANUAL?\n\n" +
                "O backup será salvo na pasta configurada.\n\n" +
                $"Local atual: {backupManager.ObterDiretorioBackup()}\n\n" +
                "⚠ IMPORTANTE:\n" +
                "• O SQL Server precisa ter permissão para criar arquivos\n" +
                "• Se der erro, use o botão 'Configurar Pasta' para escolher outra pasta\n" +
                "• Ou execute o SQL Server como Administrador\n\n" +
                "Deseja continuar?",
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
                HabilitarBotoes(false);

                if (progressBar != null)
                {
                    progressBar.Visible = true;
                    progressBar.Style = ProgressBarStyle.Marquee;
                }

                if (lblStatus != null)
                {
                    lblStatus.Text = "⏳ Realizando backup... Aguarde, não feche!";
                    lblStatus.ForeColor = Color.FromArgb(230, 126, 34);
                }

                string caminho = await Task.Run(() =>
                {
                    return backupManager.RealizarBackup(automatico);
                });

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
                    $"Arquivo: {System.IO.Path.GetFileName(caminho)}\n" +
                    $"Local: {System.IO.Path.GetDirectoryName(caminho)}\n\n" +
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
                    ex.Message,
                    "Erro no Backup",
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
                $"Restaurar o banco para:\n" +
                $"📄 Arquivo: {backup.NomeArquivo}\n" +
                $"📅 Data: {backup.DataCriacao:dd/MM/yyyy HH:mm:ss}\n\n" +
                "🚨 TODOS OS DADOS ATUAIS SERÃO SUBSTITUÍDOS!\n" +
                "Esta operação NÃO pode ser desfeita!\n\n" +
                "Tem certeza?",
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
                    lblStatus.Text = "⏳ Restaurando... NÃO FECHE O PROGRAMA!";
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
                    lblStatus.Text = "✓ Backup restaurado!";
                    lblStatus.ForeColor = Color.FromArgb(46, 204, 113);
                }

                MessageBox.Show(
                    "✓ BACKUP RESTAURADO COM SUCESSO!\n\n" +
                    "O sistema será reiniciado.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Application.Restart();
            }
            catch (Exception ex)
            {
                if (lblStatus != null)
                {
                    lblStatus.Text = "✖ Erro ao restaurar";
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
                        "✓ BACKUP ÍNTEGRO!\n\n" +
                        "O arquivo está OK e pode ser restaurado.",
                        "Verificação",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    if (lblStatus != null)
                    {
                        lblStatus.Text = "✖ Backup corrompido";
                        lblStatus.ForeColor = Color.FromArgb(231, 76, 60);
                    }

                    MessageBox.Show(
                        "✖ BACKUP CORROMPIDO!\n\n" +
                        "O arquivo está danificado e não pode ser restaurado.",
                        "Verificação",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                if (lblStatus != null)
                {
                    lblStatus.Text = "✖ Erro na verificação";
                    lblStatus.ForeColor = Color.FromArgb(231, 76, 60);
                }

                MessageBox.Show(
                    $"Erro ao verificar:\n\n{ex.Message}",
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
                $"Excluir este backup?\n\n" +
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
                    MessageBox.Show(
                        $"Erro ao excluir:\n\n{ex.Message}",
                        "Erro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
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
            if (btnConfigurarPasta != null) btnConfigurarPasta.Enabled = habilitar;
        }
    }
}