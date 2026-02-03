// =============================================
// FORMULÁRIO - SELEÇÃO DE CIDADE
// Arquivo: FormSelecionarCidade.cs
// Permite escolher qual cidade visualizar
// =============================================
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Cadastro1
{
    public partial class FormSelecionarCidade : Form
    {
        public string CidadeSelecionada { get; private set; }
        public bool VerTodasCidades { get; private set; }

        private List<string> cidades;

        public FormSelecionarCidade(List<string> listaCidades)
        {
            InitializeComponent();
            this.cidades = listaCidades.OrderBy(c => c).ToList();
            CidadeSelecionada = null;
            VerTodasCidades = false;

            PreencherListaCidades();
            ConfigurarEventos();
        }

        private void PreencherListaCidades()
        {
            // Limpar lista existente
            lstCidades.Items.Clear();

            // Preencher lista de cidades
            foreach (var cidade in cidades)
            {
                lstCidades.Items.Add(cidade);
            }

            // Atualizar contagem
            lblContagem.Text = $"Total de cidades cadastradas: {cidades.Count}";
        }

        private void ConfigurarEventos()
        {
            // Conectar eventos
            lstCidades.SelectedIndexChanged += LstCidades_SelectedIndexChanged;
            lstCidades.DoubleClick += LstCidades_DoubleClick;
            btnConfirmar.Click += BtnConfirmar_Click;
            btnVerTodas.Click += BtnVerTodas_Click;
            btnCancelar.Click += BtnCancelar_Click;

            // Configurar efeitos hover nos botões
            btnConfirmar.MouseEnter += Botao_MouseEnter;
            btnConfirmar.MouseLeave += Botao_MouseLeave;
            btnVerTodas.MouseEnter += Botao_MouseEnter;
            btnVerTodas.MouseLeave += Botao_MouseLeave;
            btnCancelar.MouseEnter += Botao_MouseEnter;
            btnCancelar.MouseLeave += Botao_MouseLeave;
        }

        private void LstCidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnConfirmar.Enabled = lstCidades.SelectedIndex >= 0;
        }

        private void LstCidades_DoubleClick(object sender, EventArgs e)
        {
            if (lstCidades.SelectedIndex >= 0)
            {
                BtnConfirmar_Click(sender, e);
            }
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (lstCidades.SelectedIndex < 0)
            {
                MessageBox.Show(
                    "⚠ Selecione uma cidade primeiro!",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            CidadeSelecionada = lstCidades.SelectedItem.ToString();
            VerTodasCidades = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnVerTodas_Click(object sender, EventArgs e)
        {
            CidadeSelecionada = null;
            VerTodasCidades = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
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
                if (btn == btnConfirmar)
                    btn.BackColor = Color.FromArgb(46, 204, 113);
                else if (btn == btnVerTodas)
                    btn.BackColor = Color.FromArgb(52, 152, 219);
                else if (btn == btnCancelar)
                    btn.BackColor = Color.FromArgb(149, 165, 166);
            }
        }
    }
}