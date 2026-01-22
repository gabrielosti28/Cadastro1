// =============================================
// PROGRAMA PRINCIPAL - COM BACKUP AUTOMÁTICO
// Arquivo: Program.cs (ATUALIZADO)
// Sistema Profissional de Cadastro com Login e Backup
// =============================================
using System;
using System.Windows.Forms;

namespace Cadastro1
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // =============================================
                // INICIAR SISTEMA DE BACKUP AUTOMÁTICO
                // =============================================
                try
                {
                    BackupManager.Instance.IniciarBackupAutomatico();
                    AuditLogger.RegistrarOperacao(
                        AuditLogger.TipoOperacao.BACKUP,
                        "Sistema",
                        "Sistema de backup automático iniciado",
                        null
                    );
                }
                catch (Exception exBackup)
                {
                    // Não bloquear o sistema se backup falhar
                    MessageBox.Show(
                        "⚠ AVISO: Sistema de backup não pôde ser iniciado.\n\n" +
                        $"Erro: {exBackup.Message}\n\n" +
                        "O sistema continuará funcionando, mas é recomendado fazer backups manuais.",
                        "Aviso - Backup",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }

                // Verificar se é o primeiro acesso
                UsuarioDAL usuarioDAL = new UsuarioDAL();

                if (usuarioDAL.VerificarPrimeiroAcesso())
                {
                    // Primeiro acesso - mostrar tela de configuração
                    MessageBox.Show(
                        "🔧 BEM-VINDO AO SISTEMA!\n\n" +
                        "Este é o primeiro acesso.\n" +
                        "Configure o usuário administrador para começar.\n\n" +
                        "IMPORTANTE: O sistema possui backup automático diário.\n" +
                        "Os backups serão salvos em 'Documentos/SistemaCadastroClientes/Backups'",
                        "Configuração Inicial",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    using (FormPrimeiroAcesso formConfig = new FormPrimeiroAcesso())
                    {
                        if (formConfig.ShowDialog() != DialogResult.OK)
                        {
                            MessageBox.Show(
                                "Sistema não configurado.\nO programa será encerrado.",
                                "Cancelado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                            return;
                        }
                    }
                }

                // Mostrar tela de login
                using (FormLogin formLogin = new FormLogin())
                {
                    if (formLogin.ShowDialog() == DialogResult.OK)
                    {
                        // Registrar login no audit log
                        AuditLogger.RegistrarLogin(Usuario.UsuarioLogado.Login, true);

                        // Login bem-sucedido - abrir menu principal
                        Application.Run(new FormMenuPrincipal());

                        // Registrar logout
                        AuditLogger.RegistrarLogout();
                    }
                }

                // =============================================
                // PARAR SISTEMA DE BACKUP AUTOMÁTICO
                // =============================================
                try
                {
                    BackupManager.Instance.PararBackupAutomatico();
                }
                catch
                {
                    // Ignorar erros ao parar backup
                }
            }
            catch (Exception ex)
            {
                // Registrar erro crítico
                try
                {
                    AuditLogger.RegistrarErro("Sistema", ex.Message);
                }
                catch
                {
                    // Ignorar se não conseguir registrar
                }

                MessageBox.Show(
                    "❌ ERRO CRÍTICO:\n\n" + ex.Message +
                    "\n\nVerifique:\n" +
                    "1. Se o SQL Server está rodando\n" +
                    "2. Se o banco 'projeto1' existe\n" +
                    "3. Se executou os scripts SQL\n\n" +
                    "Para mais detalhes, consulte os logs em:\n" +
                    "Documentos/SistemaCadastroClientes/Backups/backup_log.txt",
                    "Erro ao Iniciar Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}