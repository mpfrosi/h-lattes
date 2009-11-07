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
    public partial class FrmBathResult : Form
    {
        private List<AutorInfo> m_lstAutores;

        public FrmBathResult(List<AutorInfo> _lstAutores)
        {
            InitializeComponent();
            m_lstAutores = _lstAutores;

            foreach (AutorInfo udtAutor in m_lstAutores)
            {
                datCurriculos.AutorRow rowAutor = this.datCurriculos1.Autor.NewAutorRow();
                rowAutor.Nome = udtAutor.Nome;
                rowAutor.H_Index = udtAutor.Hindex();
                rowAutor.G_Index = udtAutor.Gindex();
                this.datCurriculos1.Autor.AddAutorRow(rowAutor);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {          
            sfdSaveFile.FileName = "SearchResult.csv";
            if (sfdSaveFile.ShowDialog() == DialogResult.OK)
            {
                SaveFile(sfdSaveFile.FileName);
            }
        }

        private void SaveFile(string _strFileName)
        {
            using (TextWriter tw = new StreamWriter(_strFileName, false, Encoding.Default))
            {
                tw.WriteLine("{0};{1};{2}", "Autor", "H-Index", "G-Index");

                foreach (datCurriculos.AutorRow row in datCurriculos1.Autor)
                {
                    tw.WriteLine("{0};{1};{2}", row.Nome, row.H_Index, row.G_Index);
                }

                tw.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                foreach (AutorInfo udtAutor in m_lstAutores)
                {
                    if (udtAutor.Nome == (string)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                    {
                        FrmResult frmResult = new FrmResult(udtAutor);
                        frmResult.MdiParent = this.MdiParent;
                        frmResult.Show();
                    }
                }
            }
        }
    }
}