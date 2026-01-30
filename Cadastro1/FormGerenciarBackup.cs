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
            InitializeComponent();
            backupManager = BackupManager.Instance;

            ConfigurarDataGridView();
            AdicionarBotaoConfigurarPasta();
            AtualizarDiretorioAtual();
            CarregarBackups();
        }

        private void ConfigurarDataGridView()
        {
            dgvBackups.AutoGenerateColumns = true;
            dgvBackups.AllowUserToAddRows = false;
            dgvBackups.ReadOnly = true;
            dgvBackups.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void AdicionarBotaoConfigurarPasta()
        {
            btnConfigurarPasta = new Button
            {
                BackColor = Color.FromArgb(41, 128, 185),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, 500),
                Size = new Size(180, 40),
                Text = "📂 Configurar Pasta",
                Cursor = Cursors.Hand
            };
            btnConfigurarPasta.FlatAppearance.BorderSize = 0;
            btnConfigurarPasta.Click += BtnConfigurarPasta_Click;
            panelBotoes.Controls.Add(btnConfigurarPasta);
        }

        private void AtualizarDiretorioAtual()
        {
            lblDiretorio.Text = $"📁 Backups salvos em: {backupManager.ObterDiretorioBackupAtual()}";
        }

        private void BtnConfigurarPasta_Click(object sender, EventArgs e)
        {
            using (FormConfiguracaoPastas form = new FormConfiguracaoPastas())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    AtualizarDiretorioAtual();
                    CarregarBackups();
                    MessageBox.Show("✓ Pasta de backups atualizada!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void CarregarBackups()
        {
            try
            {
                AtualizarStatus("Carregando backups...", Color.FromArgb(52, 73, 94));

                BackupInfo[] backups = backupManager.ListarBackups();
                dgvBackups.DataSource = backups;

                if (dgvBackups.Columns.Count > 0)
                    ConfigurarColunas();

                AtualizarStatus($"✓ Pronto - {backups.Length} backup(s) encontrado(s)",
                    Color.FromArgb(46, 204, 113));
            }
            catch (Exception ex)
            {
                AtualizarStatus("✖ Erro ao carregar backups", Color.FromArgb(231, 76, 60));
                MessageBox.Show($"Erro ao carregar backups:\n\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColunas()
        {
            // Ocultar colunas
            OcultarColuna("CaminhoCompleto");
            OcultarColuna("TamanhoBytes");

            //// Configurar colunas visíveis - CORRIGIDO
            //ConfigurarColuna("NomeArquivo", "ARQUIVO", 0, null, null); // 0 = AutoSize
            //ConfigurarColuna("DataCriacao", "DATA/HORA", 180, "dd/MM/yyyy HH:mm:ss",
            //    DataGridViewContentAlignment.MiddleCenter);
            //ConfigurarColuna("TamanhoFormatado", "TAMANHO", 100, null,
            //    DataGridViewContentAlignment.MiddleCenter);
            //ConfigurarColuna("Tipo", "TIPO", 120, null,
            //    DataGridViewContentAlignment.MiddleCenter);
        }

        private void OcultarColuna(string nome)
        {
            if (dgvBackups.Columns.Contains(nome))
                dgvBackups.Columns[nome].Visible = false;
        }

        //private void ConfigurarColuna(string nome, string header, int width,
        //    string format = null, DataGridViewContentAlignment? align = null)
        //{
        //    if (!dgvBackups.Columns.Contains(nome)) return;

        //    var col = dgvBackups.Columns[nome];
        //    col.HeaderText = header;

        //    // CORREÇÃO: Tratar width corretamente
        //    if (width > 0)
        //    {
        //        col.Width = width;
        //        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        //    }
        //    else
        //    {
        //        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        //    }

        //    if (!string.IsNullOrEmpty(format))
        //        col.DefaultCellStyle.Format = format;

        //    if (align.HasValue)
        //        col.DefaultCellStyle.Alignment = align.Value;
        //}

        private void AtualizarStatus(string mensagem, Color cor)
        {
            lblStatus.Text = mensagem;
            lblStatus.ForeColor = cor;
        }

        private async void BtnBackupManual_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show(
                "📌 CRIAR BACKUP MANUAL?\n\n" +
                $"Local: {backupManager.ObterDiretorioBackupAtual()}\n\n" +
                "Deseja continuar?",
                "Confirmar Backup",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
                await RealizarBackupAsync(false);
        }

        private async Task RealizarBackupAsync(bool automatico)
        {
            HabilitarBotoes(false);
            progressBar.Visible = true;
            progressBar.Style = ProgressBarStyle.Marquee;
            AtualizarStatus("⏳ Realizando backup...", Color.FromArgb(230, 126, 34));

            try
            {
                string caminho = await Task.Run(() => backupManager.RealizarBackup(automatico));
                AuditLogger.RegistrarBackup(caminho, true);
                AtualizarStatus("✓ Backup realizado!", Color.FromArgb(46, 204, 113));

                MessageBox.Show($"✓ BACKUP REALIZADO!\n\nArquivo: {System.IO.Path.GetFileName(caminho)}",
                    "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CarregarBackups();
            }
            catch (Exception ex)
            {
                AtualizarStatus("✖ Erro ao realizar backup", Color.FromArgb(231, 76, 60));
                AuditLogger.RegistrarErro("Backup", ex.Message);
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar.Visible = false;
                HabilitarBotoes(true);
            }
        }

        private async void BtnRestaurar_Click(object sender, EventArgs e)
        {
            if (dgvBackups.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um backup!", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BackupInfo backup = (BackupInfo)dgvBackups.SelectedRows[0].DataBoundItem;

            var resultado = MessageBox.Show(
                $"⚠️ RESTAURAR BANCO?\n\n" +
                $"Arquivo: {backup.NomeArquivo}\n" +
                $"Data: {backup.DataCriacao:dd/MM/yyyy HH:mm:ss}\n\n" +
                "TODOS OS DADOS ATUAIS SERÃO SUBSTITUÍDOS!\n\n" +
                "Tem certeza?",
                "CONFIRMAR RESTAURAÇÃO",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (resultado == DialogResult.Yes)
                await RestaurarBackupAsync(backup);
        }

        private async Task RestaurarBackupAsync(BackupInfo backup)
        {
            HabilitarBotoes(false);
            progressBar.Visible = true;
            progressBar.Style = ProgressBarStyle.Marquee;
            AtualizarStatus("⏳ Restaurando...", Color.FromArgb(231, 76, 60));

            try
            {
                await Task.Run(() => backupManager.RestaurarBackup(backup.CaminhoCompleto));
                AuditLogger.RegistrarRestauracao(backup.CaminhoCompleto, true);

                MessageBox.Show("✓ Backup restaurado! O sistema será reiniciado.",
                    "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Application.Restart();
            }
            catch (Exception ex)
            {
                AtualizarStatus("✖ Erro ao restaurar", Color.FromArgb(231, 76, 60));
                AuditLogger.RegistrarErro("Restore", ex.Message);
                MessageBox.Show($"✖ ERRO: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar.Visible = false;
                HabilitarBotoes(true);
            }
        }

        private async void BtnVerificar_Click(object sender, EventArgs e)
        {
            if (dgvBackups.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um backup!", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BackupInfo backup = (BackupInfo)dgvBackups.SelectedRows[0].DataBoundItem;

            HabilitarBotoes(false);
            progressBar.Visible = true;
            AtualizarStatus("Verificando...", Color.FromArgb(230, 126, 34));

            try
            {
                bool integro = await Task.Run(() =>
                    backupManager.VerificarIntegridadeBackup(backup.CaminhoCompleto));

                if (integro)
                {
                    AtualizarStatus("✓ Backup íntegro", Color.FromArgb(46, 204, 113));
                    MessageBox.Show("✓ Backup OK!", "Verificação",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    AtualizarStatus("✖ Backup corrompido", Color.FromArgb(231, 76, 60));
                    MessageBox.Show("✖ Backup corrompido!", "Verificação",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                AtualizarStatus("✖ Erro na verificação", Color.FromArgb(231, 76, 60));
                MessageBox.Show($"Erro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar.Visible = false;
                HabilitarBotoes(true);
            }
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvBackups.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um backup!", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BackupInfo backup = (BackupInfo)dgvBackups.SelectedRows[0].DataBoundItem;

            var resultado = MessageBox.Show(
                $"Excluir backup?\n\n{backup.NomeArquivo}\n\nIsto não pode ser desfeito!",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    System.IO.File.Delete(backup.CaminhoCompleto);
                    CarregarBackups();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro: {ex.Message}", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnAtualizar_Click(object sender, EventArgs e) => CarregarBackups();

        private void BtnAbrirPasta_Click(object sender, EventArgs e)
        {
            try
            {
                string dir = backupManager.ObterDiretorioBackupAtual();
                if (System.IO.Directory.Exists(dir))
                    System.Diagnostics.Process.Start("explorer.exe", dir);
                else
                    MessageBox.Show($"Diretório não encontrado:\n{dir}", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFechar_Click(object sender, EventArgs e) => Close();

        private void HabilitarBotoes(bool habilitar)
        {
            btnBackupManual.Enabled = habilitar;
            btnRestaurar.Enabled = habilitar;
            btnVerificar.Enabled = habilitar;
            btnExcluir.Enabled = habilitar;
            btnAtualizar.Enabled = habilitar;
            btnAbrirPasta.Enabled = habilitar;
            btnConfigurarPasta.Enabled = habilitar;
        }
    }
}