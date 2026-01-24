using System;
using System.Collections.Generic;
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

                // CORREÇÃO: Verificar se dgvBackups foi inicializado
                if (dgvBackups != null)
                {
                    dgvBackups.AutoGenerateColumns = true;
                    dgvBackups.AllowUserToAddRows = false;
                    dgvBackups.AllowUserToDeleteRows = false;
                    dgvBackups.ReadOnly = true;
                    dgvBackups.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvBackups.MultiSelect = false;
                }

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
            try
            {
                // CORREÇÃO: Verificar se panelBotoes existe antes de adicionar botão
                if (panelBotoes == null)
                {
                    MessageBox.Show(
                        "Erro: Panel de botões não foi inicializado corretamente.",
                        "Erro de Interface",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

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
                btnConfigurarPasta.MouseEnter += Botao_MouseEnter;
                btnConfigurarPasta.MouseLeave += Botao_MouseLeave;

                panelBotoes.Controls.Add(btnConfigurarPasta);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao adicionar botão Configurar Pasta:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void BtnConfigurarPasta_Click(object sender, EventArgs e)
        {
            try
            {
                if (backupManager.EscolherDiretorioBackup())
                {
                    if (lblDiretorio != null)
                    {
                        lblDiretorio.Text = $"📁 Backups salvos em: {backupManager.ObterDiretorioBackup()}";
                    }
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
                // CORREÇÃO: Verificar se os controles existem
                if (dgvBackups == null)
                {
                    MessageBox.Show(
                        "Erro: DataGridView não foi inicializado.",
                        "Erro de Interface",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                AtualizarStatus("Carregando backups...", Color.FromArgb(52, 73, 94));

                BackupInfo[] backups = backupManager.ListarBackups();

                dgvBackups.DataSource = null;
                dgvBackups.DataSource = backups;

                Application.DoEvents();

                if (dgvBackups.Columns != null && dgvBackups.Columns.Count > 0)
                {
                    //ConfigurarColunasDataGridView();
                }

                AtualizarStatus($"✓ Pronto - {backups.Length} backup(s) encontrado(s)", Color.FromArgb(46, 204, 113));
            }
            catch (Exception ex)
            {
                AtualizarStatus("✖ Erro ao carregar backups", Color.FromArgb(231, 76, 60));
                MessageBox.Show(
                    $"Erro ao carregar backups:\n\n{ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //private void ConfigurarColunasDataGridView()
        //{
        //    try
        //    {
        //        // CORREÇÃO: Verificação adicional
        //        if (dgvBackups == null || dgvBackups.Columns == null)
        //            return;

        //        // Colunas a ocultar
        //        string[] colunasOcultas = { "CaminhoCompleto", "TamanhoBytes" };

        //        // Configurações de colunas visíveis
        //        var configColunas = new Dictionary<string, (string header, int? width, string format, DataGridViewContentAlignment? align)>
        //        {
        //            { "NomeArquivo", ("ARQUIVO", null, null, null) },
        //            { "DataCriacao", ("DATA/HORA", 180, "dd/MM/yyyy HH:mm:ss", DataGridViewContentAlignment.MiddleCenter) },
        //            { "TamanhoFormatado", ("TAMANHO", 100, null, DataGridViewContentAlignment.MiddleCenter) },
        //            { "Tipo", ("TIPO", 120, null, DataGridViewContentAlignment.MiddleCenter) }
        //        };

        //        foreach (string coluna in colunasOcultas)
        //        {
        //            if (dgvBackups.Columns.Contains(coluna))
        //                dgvBackups.Columns[coluna].Visible = false;
        //        }

        //        foreach (var config in configColunas)
        //        {
        //            if (dgvBackups.Columns.Contains(config.Key))
        //            {
        //                var col = dgvBackups.Columns[config.Key];
        //                col.HeaderText = config.Value.header;

        //                if (config.Value.width.HasValue)
        //                    col.Width = config.Value.width.Value;
        //                else
        //                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        //                if (!string.IsNullOrEmpty(config.Value.format))
        //                    col.DefaultCellStyle.Format = config.Value.format;

        //                if (config.Value.align.HasValue)
        //                    col.DefaultCellStyle.Alignment = config.Value.align.Value;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(
        //            $"Erro ao configurar colunas:\n\n{ex.Message}",
        //            "Erro",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Warning);
        //    }
        //}

        private void AtualizarStatus(string mensagem, Color cor)
        {
            if (lblStatus != null)
            {
                lblStatus.Text = mensagem;
                lblStatus.ForeColor = cor;
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
                "• Se der erro, use o botão 'Configurar Pasta' para escolher outra pasta\n\n" +
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

                AtualizarStatus("⏳ Realizando backup... Aguarde, não feche!", Color.FromArgb(230, 126, 34));

                string caminho = await Task.Run(() => backupManager.RealizarBackup(automatico));

                AuditLogger.RegistrarBackup(caminho, true);

                AtualizarStatus("✓ Backup realizado com sucesso!", Color.FromArgb(46, 204, 113));

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
                AtualizarStatus("✖ Erro ao realizar backup", Color.FromArgb(231, 76, 60));
                AuditLogger.RegistrarErro("Backup", ex.Message);

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
            // CORREÇÃO: Verificar se dgvBackups existe
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

                AtualizarStatus("⏳ Restaurando... NÃO FECHE O PROGRAMA!", Color.FromArgb(231, 76, 60));

                await Task.Run(() => backupManager.RestaurarBackup(backup.CaminhoCompleto));

                AuditLogger.RegistrarRestauracao(backup.CaminhoCompleto, true);

                AtualizarStatus("✓ Backup restaurado!", Color.FromArgb(46, 204, 113));

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
                AtualizarStatus("✖ Erro ao restaurar", Color.FromArgb(231, 76, 60));
                AuditLogger.RegistrarErro("Restore", ex.Message);

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

                AtualizarStatus("Verificando integridade...", Color.FromArgb(230, 126, 34));

                bool integro = await Task.Run(() =>
                    backupManager.VerificarIntegridadeBackup(backup.CaminhoCompleto));

                if (integro)
                {
                    AtualizarStatus("✓ Backup verificado - Íntegro", Color.FromArgb(46, 204, 113));

                    MessageBox.Show(
                        "✓ BACKUP ÍNTEGRO!\n\n" +
                        "O arquivo está OK e pode ser restaurado.",
                        "Verificação",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    AtualizarStatus("✖ Backup corrompido", Color.FromArgb(231, 76, 60));

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
                AtualizarStatus("✖ Erro na verificação", Color.FromArgb(231, 76, 60));

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
                    AtualizarStatus("✓ Backup excluído", Color.FromArgb(46, 204, 113));
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
                if (btn == btnConfigurarPasta)
                    btn.BackColor = Color.FromArgb(41, 128, 185);
            }
        }
    }
}