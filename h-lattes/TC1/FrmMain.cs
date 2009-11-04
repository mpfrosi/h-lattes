using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace HLattes
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }       

        private void porNomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBuscaAutor frmBusca = new FrmBuscaAutor();
            frmBusca.MdiParent = this;
            frmBusca.Show();

            while (!frmBusca.IsDisposed)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }

            if (frmBusca.AutorSelecionado != null)
            {
                m_strLink = frmBusca.AutorSelecionado.Link;
                Thread oThread = new Thread(new ThreadStart(p1));
                //m_oThread.IsBackground = false;                                      
                oThread.Start();
            }
        }

        string m_strLink = null;

        private void porURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEnterURL frmEnterURL = new FrmEnterURL();            
            frmEnterURL.MdiParent = this;
            frmEnterURL.URLVisible = true;
            frmEnterURL.Show();
        }

        private void porArquivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofdSelectFile.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(ofdSelectFile.FileName))
                {
                    Thread oThread = new Thread(new ThreadStart(p2));
                    //m_oThread.IsBackground = false;                                      
                    oThread.Start();
                }
            }
        }

        private void opçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmConfig frmConfig = new FrmConfig();
            frmConfig.MdiParent = this;
            frmConfig.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_strValue"></param>
        public void p1()
        {
            if (this.InvokeRequired)
            {
                ThreadStart d = new ThreadStart(p1);
                this.Invoke(d, new object[] { });
            }
            else
            {
                FrmEnterURL frmEnterURL = new FrmEnterURL();
                frmEnterURL.URL = m_strLink;
                frmEnterURL.URLVisible = false;
                frmEnterURL.MdiParent = this;
                frmEnterURL.Show();
                frmEnterURL.GO();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_strValue"></param>
        public void p2()
        {
            if (this.InvokeRequired)
            {
                ThreadStart d = new ThreadStart(p2);
                this.Invoke(d, new object[] { });
            }
            else
            {
                FrmEnterURL frmEnterURL = new FrmEnterURL();
                frmEnterURL.FileName = ofdSelectFile.FileName;
                frmEnterURL.URLVisible = false;
                frmEnterURL.MdiParent = this;
                frmEnterURL.Show();
                frmEnterURL.GO();
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

    }
}