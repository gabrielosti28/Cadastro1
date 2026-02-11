// =============================================
// FORMULÁRIO - SELEÇÃO DE CIDADES PARA IMPORTAÇÃO
// Arquivo: FormSelecionarCidade.cs
// LÓGICA - EVENTOS E FUNCIONALIDADES
// =============================================
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Cadastro1
{
    public partial class FormSelecionarCidadeImportacao: Form
    {
        public List<string> CidadesSelecionadas { get; private set; }
        private List<string> todasCidades;

        public FormSelecionarCidadeImportacao(List<string> cidadesDisponiveis)
        {
            InitializeComponent();
            CidadesSelecionadas = new List<string>();
            todasCidades = cidadesDisponiveis.OrderBy(c => c).ToList();

            ConfigureEvents();
            CarregarCidades();
        }

        private void ConfigureEvents()
        {
            // Eventos de busca
            txtBusca.TextChanged += TxtBusca_TextChanged;
            btnLimparBusca.Click += (s, e) => txtBusca.Clear();

            // Eventos de seleção em massa
            btnMarcarTodas.Click += BtnMarcarTodas_Click;
            btnDesmarcarTodas.Click += BtnDesmarcarTodas_Click;
            btnInverter.Click += BtnInverter_Click;

            // Eventos de confirmação
            btnConfirmar.Click += BtnConfirmar_Click;
            btnCancelar.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            // Evento de check do CheckedListBox
            chkCidades.ItemCheck += (s, e) =>
            {
                // Atualizar contador após o check (BeginInvoke para pegar o estado novo)
                this.BeginInvoke(new Action(() => AtualizarContador()));
            };

            // Atualizar contador inicial
            AtualizarContador();
        }

        private void CarregarCidades()
        {
            chkCidades.Items.Clear();

            foreach (string cidade in todasCidades)
            {
                chkCidades.Items.Add(cidade, false);
            }

            AtualizarContador();
        }

        private void TxtBusca_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBusca.Text.ToUpper();

            // Guardar seleções atuais
            HashSet<string> selecionadas = new HashSet<string>();
            foreach (int index in chkCidades.CheckedIndices)
            {
                selecionadas.Add(chkCidades.Items[index].ToString());
            }

            chkCidades.Items.Clear();

            if (string.IsNullOrWhiteSpace(filtro))
            {
                // Mostrar todas
                foreach (string cidade in todasCidades)
                {
                    chkCidades.Items.Add(cidade, selecionadas.Contains(cidade));
                }
            }
            else
            {
                // Filtrar
                var cidadesFiltradas = todasCidades
                    .Where(c => c.ToUpper().Contains(filtro))
                    .ToList();

                foreach (string cidade in cidadesFiltradas)
                {
                    chkCidades.Items.Add(cidade, selecionadas.Contains(cidade));
                }
            }

            AtualizarContador();
        }

        private void BtnMarcarTodas_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkCidades.Items.Count; i++)
            {
                chkCidades.SetItemChecked(i, true);
            }
            AtualizarContador();
        }

        private void BtnDesmarcarTodas_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkCidades.Items.Count; i++)
            {
                chkCidades.SetItemChecked(i, false);
            }
            AtualizarContador();
        }

        private void BtnInverter_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkCidades.Items.Count; i++)
            {
                chkCidades.SetItemChecked(i, !chkCidades.GetItemChecked(i));
            }
            AtualizarContador();
        }

        private void AtualizarContador()
        {
            int total = chkCidades.Items.Count;
            int selecionadas = chkCidades.CheckedItems.Count;

            lblContador.Text = $"📊 {total} cidades listadas | {selecionadas} selecionadas";

            if (selecionadas == 0)
            {
                lblContador.ForeColor = Color.FromArgb(231, 76, 60);
            }
            else if (selecionadas == total)
            {
                lblContador.ForeColor = Color.FromArgb(46, 204, 113);
            }
            else
            {
                lblContador.ForeColor = Color.FromArgb(52, 152, 219);
            }
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (chkCidades.CheckedItems.Count == 0)
            {
                MessageBox.Show(
                    "⚠ Nenhuma cidade foi selecionada!\n\n" +
                    "Selecione pelo menos uma cidade para continuar a importação.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Coletar cidades selecionadas
            CidadesSelecionadas.Clear();

            foreach (var item in chkCidades.CheckedItems)
            {
                CidadesSelecionadas.Add(item.ToString());
            }

            // Confirmar seleção
            string mensagem = $"📋 CONFIRMAÇÃO DE IMPORTAÇÃO\n\n";

            if (CidadesSelecionadas.Count == todasCidades.Count)
            {
                mensagem += "✅ Você selecionou TODAS as cidades.\n";
                mensagem += $"   Total: {CidadesSelecionadas.Count} cidades\n\n";
            }
            else if (CidadesSelecionadas.Count <= 5)
            {
                mensagem += $"✅ Você selecionou {CidadesSelecionadas.Count} cidade(s):\n\n";
                foreach (var cidade in CidadesSelecionadas)
                {
                    mensagem += $"   • {cidade}\n";
                }
                mensagem += "\n";
            }
            else
            {
                mensagem += $"✅ Você selecionou {CidadesSelecionadas.Count} cidades:\n\n";
                for (int i = 0; i < 3; i++)
                {
                    mensagem += $"   • {CidadesSelecionadas[i]}\n";
                }
                mensagem += $"   • ... e mais {CidadesSelecionadas.Count - 3} cidades\n\n";
            }

            mensagem += "⚠ IMPORTANTE:\n" +
                       "Apenas clientes das cidades selecionadas serão importados.\n" +
                       "Clientes de outras cidades serão ignorados.\n\n" +
                       "Deseja continuar?";

            DialogResult confirmacao = MessageBox.Show(
                mensagem,
                "Confirmar Seleção",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacao == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}