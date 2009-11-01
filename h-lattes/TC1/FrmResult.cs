using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Model;
using System.IO;

namespace HLattes
{
    public partial class FrmResult : Form
    {
        private AutorInfo m_udtAutor;

        public FrmResult(AutorInfo _udtAutor)
        {
            InitializeComponent();
            m_udtAutor = _udtAutor;
            lblAutor.Text = m_udtAutor.Nome;
            
            
            int intHindex = m_udtAutor.Hindex();
            int intGindex = m_udtAutor.Gindex();
            int intCountCitacoes = m_udtAutor.TotalCitacoes();
            int intCountCitacoesH = m_udtAutor.TotalCitacoesHindex();
            int intCountCitacoesG = m_udtAutor.TotalCitacoesGindex();
            

            foreach (PublicacaoInfo udtPublicacao in m_udtAutor.Publicacoes)
            {              
                datCurriculos.PublicacaoRow rowPub = this.datCurriculos1.Publicacao.NewPublicacaoRow();
                rowPub.Titulo = udtPublicacao.Titulo;
                rowPub.Citacoes = udtPublicacao.Citacoes;
                this.datCurriculos1.Publicacao.AddPublicacaoRow(rowPub);                
            }

            lblCitTotal.Text = intCountCitacoes.ToString();
            lblCitGindex.Text = intCountCitacoesG.ToString();
            lblCitHindex.Text = intCountCitacoesH.ToString();

            lblHindex.Text = intHindex.ToString();
            lblGindex.Text = intGindex.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim() != string.Empty)
            {
                string[] arr = this.textBox1.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                List<string> lstFilter = new List<string>();

                foreach (string str in arr)
                {
                    lstFilter.Add(string.Format("Titulo like '%{0}%' ", str));
                }

                bindingSource.Filter = string.Join(" AND ", lstFilter.ToArray());
            }
            else
                bindingSource.Filter = string.Empty;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string strFileName = m_udtAutor.Nome;
            strFileName = strFileName.Replace(".", String.Empty);
            strFileName = strFileName.Replace(" ", "_");

            sfdSaveFile.FileName = strFileName + ".csv";
            if (sfdSaveFile.ShowDialog() == DialogResult.OK)
            {
                SaveFile(sfdSaveFile.FileName);
            }
        }

        private void SaveFile(string _strFileName)
        {            
            using (TextWriter tw = new StreamWriter(_strFileName, false, Encoding.Default))
            {
                tw.WriteLine("{0};{1}","Título", "Citações");

                foreach (datCurriculos.PublicacaoRow row in datCurriculos1.Publicacao)
                {
                    tw.WriteLine("{0};{1}", row.Titulo, row.Citacoes);
                }

                tw.Close();
            }
        }
    }
}