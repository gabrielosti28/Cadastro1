// =============================================
// PROGRAMA PRINCIPAL - ATUALIZADO
// Arquivo: Program.cs
// Sistema Profissional de Cadastro com Login
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
                // Verificar se é o primeiro acesso
                UsuarioDAL usuarioDAL = new UsuarioDAL();

                if (usuarioDAL.VerificarPrimeiroAcesso())
                {
                    // Primeiro acesso - mostrar tela de configuração
                    MessageBox.Show(
                        "🔧 BEM-VINDO AO SISTEMA!\n\n" +
                        "Este é o primeiro acesso.\n" +
                        "Configure o usuário administrador para começar.",
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
                        // Login bem-sucedido - abrir menu principal
                        Application.Run(new FormMenuPrincipal());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "❌ ERRO CRÍTICO:\n\n" + ex.Message +
                    "\n\nVerifique:\n" +
                    "1. Se o SQL Server está rodando\n" +
                    "2. Se o banco 'projeto1' existe\n" +
                    "3. Se executou os scripts SQL",
                    "Erro ao Iniciar Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}