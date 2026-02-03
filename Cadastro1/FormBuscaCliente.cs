// =============================================
// FORMULÁRIO DE BUSCA - VERSÃO OTIMIZADA
// Arquivo: FormBuscaCliente.cs
// Busca simples e direta por CPF
// =============================================
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cadastro1
{
    public partial class FormBuscaCliente : Form
    {
        private ClienteDAL clienteDAL;
        private Cliente clienteEncontrado;

        public FormBuscaCliente()
        {
            InitializeComponent();
            clienteDAL = new ClienteDAL();
            ConfigurarInterface();
        }

        private void ConfigurarInterface()
        {
            // Focar automaticamente no campo de CPF
            txtCPFBusca.Focus();

            // Permitir busca com Enter
            txtCPFBusca.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    e.Handled = true;
                    BtnBuscar_Click(null, null);
                }
            };
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                // Limpar CPF (remover espaços e formatação)
                string cpf = txtCPFBusca.Text.Replace(" ", "").Replace(".", "").Replace("-", "");

                // Validar comprimento
                if (cpf.Length != 11)
                {
                    MessageBox.Show(
                        "⚠ Digite um CPF completo com 11 números!",
                        "CPF Incompleto",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtCPFBusca.Focus();
                    return;
                }

                // Buscar cliente
                clienteEncontrado = clienteDAL.ConsultarPorCPF(cpf);

                if (clienteEncontrado != null)
                {
                    MostrarResultado(clienteEncontrado);
                }
                else
                {
                    panelResultado.Visible = false;
                    clienteEncontrado = null;

                    DialogResult resultado = MessageBox.Show(
                        $"❌ Cliente não encontrado!\n\n" +
                        $"CPF: {FormatarCPF(cpf)}\n\n" +
                        "Deseja cadastrar este CPF agora?",
                        "Não Encontrado",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        AbrirCadastroNovoCliente(cpf);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao buscar cliente:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void MostrarResultado(Cliente cliente)
        {
            // Limpar painel
            panelResultado.Controls.Clear();

            // Calcular idade
            int idade = ClienteDAL.CalcularIdade(cliente.DataNascimento);

            // Criar layout de resultado
            int y = 15;
            int larguraLabel = 120;
            int larguraValor = 680;
            int alturaLinha = 35;

            // Título
            Label lblTitulo = CriarLabel("✓ CLIENTE ENCONTRADO", 0, y, 800, 30,
                new Font("Segoe UI", 14, FontStyle.Bold), Color.FromArgb(46, 204, 113));
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            panelResultado.Controls.Add(lblTitulo);

            y += 45;

            // Dados do cliente
            AdicionarCampo("Nome:", cliente.NomeCompleto, ref y, larguraLabel, larguraValor, alturaLinha);
            AdicionarCampo("CPF:", FormatarCPF(cliente.CPF), ref y, larguraLabel, larguraValor, alturaLinha);
            AdicionarCampo("Nascimento:", $"{cliente.DataNascimento:dd/MM/yyyy} ({idade} anos)", ref y, larguraLabel, larguraValor, alturaLinha);
            AdicionarCampo("Endereço:", cliente.Endereco, ref y, larguraLabel, larguraValor, alturaLinha);
            AdicionarCampo("Cidade:", cliente.Cidade, ref y, larguraLabel, larguraValor, alturaLinha);
            AdicionarCampo("CEP:", FormatarCEP(cliente.CEP), ref y, larguraLabel, larguraValor, alturaLinha);

            if (!string.IsNullOrEmpty(cliente.Telefone))
                AdicionarCampo("Telefone:", cliente.Telefone, ref y, larguraLabel, larguraValor, alturaLinha);

            AdicionarCampo("INSS:", cliente.BeneficioINSS, ref y, larguraLabel, larguraValor, alturaLinha);

            if (!string.IsNullOrEmpty(cliente.BeneficioINSS2))
                AdicionarCampo("2º INSS:", cliente.BeneficioINSS2, ref y, larguraLabel, larguraValor, alturaLinha);

            y += 15;

            // Botões de ação
            Panel panelBotoes = new Panel
            {
                Location = new Point(20, y),
                Size = new Size(795, 60),
                BackColor = Color.FromArgb(236, 240, 241)
            };

            Button btnEditar = CriarBotao("✏️ EDITAR DADOS", 20, 10, 240, 40,
                Color.FromArgb(230, 126, 34));
            btnEditar.Click += (s, evt) => EditarCliente();

            Button btnDocumentos = CriarBotao("📎 VER DOCUMENTOS", 280, 10, 240, 40,
                Color.FromArgb(52, 152, 219));
            btnDocumentos.Click += (s, evt) => VerDocumentos();

            Button btnNovaBusca = CriarBotao("🔍 NOVA BUSCA", 540, 10, 240, 40,
                Color.FromArgb(149, 165, 166));
            btnNovaBusca.Click += (s, evt) => NovaBusca();

            panelBotoes.Controls.AddRange(new Control[] { btnEditar, btnDocumentos, btnNovaBusca });
            panelResultado.Controls.Add(panelBotoes);

            panelResultado.Visible = true;
        }

        private void AdicionarCampo(string titulo, string valor, ref int y,
            int larguraLabel, int larguraValor, int altura)
        {
            Panel panelCampo = new Panel
            {
                Location = new Point(20, y),
                Size = new Size(larguraLabel + larguraValor, altura),
                BackColor = Color.FromArgb(236, 240, 241)
            };

            Label lblTitulo = CriarLabel(titulo, 10, 8, larguraLabel, 20,
                new Font("Segoe UI", 10, FontStyle.Bold), Color.FromArgb(52, 73, 94));

            Label lblValor = CriarLabel(valor, larguraLabel + 10, 8, larguraValor, 20,
                new Font("Segoe UI", 10), Color.FromArgb(44, 62, 80));

            panelCampo.Controls.AddRange(new Control[] { lblTitulo, lblValor });
            panelResultado.Controls.Add(panelCampo);

            y += altura + 5;
        }

        private Label CriarLabel(string texto, int x, int y, int largura, int altura,
            Font fonte, Color cor)
        {
            return new Label
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(largura, altura),
                Font = fonte,
                ForeColor = cor,
                AutoEllipsis = true
            };
        }

        private Button CriarBotao(string texto, int x, int y, int largura, int altura, Color cor)
        {
            Button btn = new Button
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(largura, altura),
                BackColor = cor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;

            // Efeito hover
            btn.MouseEnter += (s, e) => btn.BackColor = ControlPaint.Dark(cor, 0.1f);
            btn.MouseLeave += (s, e) => btn.BackColor = cor;

            return btn;
        }

        private void EditarCliente()
        {
            if (clienteEncontrado == null) return;

            try
            {
                using (FormEditarCliente formEditar = new FormEditarCliente(clienteEncontrado))
                {
                    if (formEditar.ShowDialog() == DialogResult.OK)
                    {
                        // Recarregar dados atualizados
                        clienteEncontrado = clienteDAL.ConsultarPorCPF(clienteEncontrado.CPF);
                        MostrarResultado(clienteEncontrado);

                        MessageBox.Show(
                            "✓ Dados atualizados com sucesso!",
                            "Sucesso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao editar cliente:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void VerDocumentos()
        {
            if (clienteEncontrado == null) return;

            try
            {
                using (FormAnexosCliente formAnexos = new FormAnexosCliente(clienteEncontrado))
                {
                    formAnexos.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao abrir documentos:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void NovaBusca()
        {
            panelResultado.Visible = false;
            clienteEncontrado = null;
            txtCPFBusca.Clear();
            txtCPFBusca.Focus();
        }

        private void AbrirCadastroNovoCliente(string cpf)
        {
            try
            {
                using (FormCadastroCliente formCadastro = new FormCadastroCliente())
                {
                    // Aqui você poderia pré-preencher o CPF se modificasse o FormCadastroCliente
                    formCadastro.ShowDialog();

                    // Tentar buscar novamente após cadastro
                    clienteEncontrado = clienteDAL.ConsultarPorCPF(cpf);
                    if (clienteEncontrado != null)
                    {
                        MostrarResultado(clienteEncontrado);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao abrir cadastro:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string FormatarCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) return "";
            cpf = cpf.Replace("-", "").Replace(".", "").Trim();

            if (cpf.Length == 10)
                cpf = "0" + cpf;

            if (cpf.Length == 11)
                return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";

            return cpf;
        }

        private string FormatarCEP(string cep)
        {
            if (string.IsNullOrEmpty(cep)) return "";
            cep = cep.Replace("-", "").Trim();

            if (cep.Length == 8)
                return $"{cep.Substring(0, 5)}-{cep.Substring(5, 3)}";

            return cep;
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}