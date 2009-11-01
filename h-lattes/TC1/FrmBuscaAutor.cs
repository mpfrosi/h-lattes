using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Net;
using Model.Filtros;
using Model;
using Core;

namespace HLattes
{
    public partial class FrmBuscaAutor : Form
    {
        private Autor m_udtAutor;

        private AutorInfo m_udtAutorSelecionado = null;

        public AutorInfo AutorSelecionado
        {
            get { return m_udtAutorSelecionado; }
        }

        public FrmBuscaAutor()
        {
            InitializeComponent();
            m_udtAutor = new Autor();
        }       

        private void FrmBuscaAutor_Load(object sender, EventArgs e)
        {
            RefreshImage();
            ckbDoutores.Checked = true;            
        }

        private void RefreshImage()
        {
            this.pictureBox1.Image = Image.FromStream(m_udtAutor.GetSearchImage());
        }        

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string msge;
            this.Valida(out msge);

            if (!string.IsNullOrEmpty(msge))
            {
                MessageBox.Show(msge,"ATENÇÂO");
                return;
            }

            FiltroBuscaAutor udtFiltro = new FiltroBuscaAutor();

            udtFiltro.Nome = tbxAutor.Text;
            udtFiltro.Doutor = ckbDoutores.Checked;
            udtFiltro.OutrosPesquisadores = ckbOutrosPesquisadores.Checked;
            udtFiltro.Captcha = tbxCaptcha.Text;

            List<AutorInfo> lstAutores = this.m_udtAutor.BuscaAutores(udtFiltro);

            if(lstAutores == null)
            {
                MessageBox.Show("Erro na busca por autores.\nVerifique o valor do captcha digitado.", "ATENÇÂO");
                return;
            }

            if (lstAutores.Count == 0)
            {
                MessageBox.Show("Busca não retornou nenhum resultado.", "ATENÇÂO");
                return;
            }

            DataTable Tabela = new DataTable();
            Tabela.Columns.Add("Autor");
            Tabela.Columns.Add("Model", typeof(object));

            foreach (AutorInfo udtAutor in lstAutores)
            {
                DataRow row = Tabela.NewRow();

                row["Autor"] = udtAutor.Nome;
                row["Model"] = udtAutor;

                Tabela.Rows.Add(row);
            }

            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = Tabela;

        }

        private void Valida(out string msge)
        {
            msge = null;
            if (string.IsNullOrEmpty(this.tbxCaptcha.Text))
                msge = "Campo Autor não pode estar em branco.";
            if (string.IsNullOrEmpty(this.tbxCaptcha.Text))
                msge = "Campo Captcha não pode estar em branco.";
        }       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {            
            DataTable Tabela = (DataTable)((DataGridView)sender).DataSource;
            m_udtAutorSelecionado = (AutorInfo)Tabela.Rows[e.RowIndex]["Model"];
            this.Close();
        }

        private void ckbOutrosPesquisadores_CheckedChanged(object sender, EventArgs e)
        {
            ckbDoutores.Checked = !ckbOutrosPesquisadores.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.ckbOutrosPesquisadores.Checked = !this.ckbDoutores.Checked;
        }

        private void lnkMudarImagem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RefreshImage();
        }
    }
}