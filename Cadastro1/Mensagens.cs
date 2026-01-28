using System.Windows.Forms;

namespace Cadastro1.Utilidades
{
    /// <summary>
    /// Centralizador de mensagens do sistema
    /// </summary>
    public static class Mensagens
    {
        // ========== MENSAGENS DE SUCESSO ==========
        public const string CLIENTE_SALVO = "Cliente cadastrado com sucesso!";
        public const string CLIENTE_ATUALIZADO = "Cliente atualizado com sucesso!";
        public const string CLIENTE_EXCLUIDO = "Cliente excluído com sucesso!";
        public const string PDF_GERADO = "PDF gerado com sucesso!\n\nLocalização: {0}";
        public const string PLANILHA_IMPORTADA = "Planilha importada com sucesso!\n\n{0} clientes marcados.";
        public const string TEMPLATE_SALVO = "Template salvo com sucesso!";

        // ========== MENSAGENS DE VALIDAÇÃO ==========
        public const string CAMPO_OBRIGATORIO = "O campo '{0}' é obrigatório.";
        public const string CPF_INVALIDO = "CPF inválido.\n\nFormato esperado: 000.000.000-00";
        public const string CEP_INVALIDO = "CEP inválido.\n\nFormato esperado: 00000-000";
        public const string EMAIL_INVALIDO = "E-mail inválido.";
        public const string TELEFONE_INVALIDO = "Telefone inválido.";
        public const string CLIENTE_JA_CADASTRADO = "Já existe um cliente cadastrado com este CPF.";

        // ========== MENSAGENS DE ERRO ==========
        public const string ERRO_BANCO_DADOS = "Erro ao acessar banco de dados.\n\n{0}";
        public const string ERRO_ARQUIVO = "Erro ao processar arquivo.\n\n{0}";
        public const string ERRO_CARREGAR_IMAGEM = "Erro ao carregar imagem.\n\n{0}";
        public const string ERRO_GERAR_PDF = "Erro ao gerar PDF.\n\n{0}";
        public const string ERRO_IMPORTAR_PLANILHA = "Erro ao importar planilha.\n\n{0}";
        public const string ERRO_INESPERADO = "Ocorreu um erro inesperado.\n\nContate o suporte técnico.";

        // ========== MENSAGENS DE CONFIRMAÇÃO ==========
        public const string CONFIRMAR_EXCLUSAO = "Tem certeza que deseja excluir o cliente '{0}'?\n\nEsta ação não pode ser desfeita.";
        public const string CONFIRMAR_SOBRESCREVER = "Já existe um arquivo com este nome.\n\nDeseja sobrescrever?";

        // ========== MENSAGENS INFORMATIVAS ==========
        public const string NENHUM_CLIENTE_SELECIONADO = "Nenhum cliente foi selecionado.\n\nSelecione pelo menos um cliente para continuar.";
        public const string NENHUM_RESULTADO_BUSCA = "Nenhum cliente encontrado com os critérios informados.";
        public const string TEMPLATE_NAO_DEFINIDO = "Template não foi definido.\n\nDefina as posições dos campos antes de gerar o PDF.";

        // ========== MÉTODOS DE EXIBIÇÃO ==========

        public static void ExibirSucesso(string mensagem)
        {
            MessageBox.Show(
                mensagem,
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        public static void ExibirErro(string mensagem)
        {
            Logger.Error($"Erro exibido ao usuário: {mensagem}");
            MessageBox.Show(
                mensagem,
                "Erro",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        public static void ExibirAviso(string mensagem)
        {
            MessageBox.Show(
                mensagem,
                "Atenção",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        public static void ExibirInfo(string mensagem)
        {
            MessageBox.Show(
                mensagem,
                "Informação",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        public static DialogResult ConfirmarAcao(string mensagem)
        {
            return MessageBox.Show(
                mensagem,
                "Confirmação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
        }
    }
}