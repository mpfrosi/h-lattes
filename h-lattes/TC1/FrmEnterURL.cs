using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Core;
using System.Threading;
using Model;
using Model.Exceptions;
using CurriculoParser;
using System.Net;
using Common;

namespace HLattes
{
    public partial class FrmEnterURL : Form
    {
        Curriculo m_coreCurriculo = null;
        
        /// <summary>
        /// Autor pesquisado.
        /// </summary>
        AutorInfo m_udtAutor;

        /// <summary>
        /// Lista de autores pesquisados.
        /// </summary>
        List<AutorInfo> m_lstAutores;

        /// <summary>
        /// Thread para busca ser realizada asincronamente e não trancara GUI.
        /// </summary>
        Thread m_oThread = null;

        /// <summary>
        /// Se aposa busca terminar o form de resultado deve ser aberto.
        /// </summary>
        bool m_blnGoToResultForm = false;

        /// <summary>
        /// Nome do arquivo para caso de pesquisa em lote.
        /// </summary>
        private string m_strFileName;

        public FrmEnterURL()
        {
            InitializeComponent();
            ToolTips.SetToolTip(btnOk, "Processa o curriculo.");            
            
            m_coreCurriculo = new Curriculo();

            URL = null;
            FileName = null;
            lblAutor.Text = string.Empty;
                
            //Tratadores de evento
            m_coreCurriculo.StartScholarSearch += new CoreStartScholarSearchHandler(m_udtCurriculo_StartScholarSearch);
            m_coreCurriculo.ScholarSearchStep += new CoreScholarSearchStepHandler(m_udtCurriculo_ScholarSearchStep);
            m_coreCurriculo.EndScholarSearch += new CoreEndScholarSearchHandler(m_udtCurriculo_EndScholarSearch);
            m_coreCurriculo.GoogleDenyAccess += new CoreUnautorisedAccess(m_udtCurriculo_GoogleDenyAccess);
            m_coreCurriculo.SearchGetAutor += new CoreScholarSearchGetAutorHandler(m_coreCurriculo_SearchGetAutor);
        }               

        /// <summary>
        /// URL para busca do autor.
        /// </summary>
        public string URL
        {
            get { return this.tbxUrl.Text; }
            set { this.tbxUrl.Text = value; }
        }

        /// <summary>
        /// Se form deve exibur a url pesquisada.
        /// </summary>
        public bool URLVisible
        {
            get { return pnlURL.Visible; }
            set
            {               
                pnlURL.Visible = value;
                this.lblArtigo.Visible =!value;
                this.lblAutor.Visible = !value;
            }
        }       

        /// <summary>
        /// Nome do arquivo para caso de pesquisa em lote.
        /// </summary>
        public string FileName
        {
            get { return m_strFileName; }
            set { m_strFileName = value; }
        }

        /// <summary>
        /// Realiza a pesquisa asincronamente.
        /// </summary>
        private void DoSearch()
        {                       
            btnCancel.Enabled = true;           
            btnOk.Enabled = false;           
            //PerformSearch();

            m_oThread = new Thread(new ThreadStart(PerformSearch));
            //m_oThread.IsBackground = false;
            m_oThread.SetApartmentState(ApartmentState.STA);
            m_oThread.Name = "SEARCH";
            m_oThread.Start();
            
            while (!m_oThread.Join(100))
            {                 
                Application.DoEvents(); 
            }          
        }
              
        /// <summary>
        /// 
        /// </summary>     
        [STAThread]
        private void PerformSearch()
        {
            try
            {
                if (!string.IsNullOrEmpty(FileName))
                {
                    //Thread.Sleep(10000);
                    m_lstAutores = m_coreCurriculo.GetCurriculosByFile(this.FileName);
                }
                else
                {
                    m_udtAutor = m_coreCurriculo.GetCurriculo(this.URL);
                }
                
                m_blnGoToResultForm = true;
            }
            catch (CancelException)
            {
                m_blnGoToResultForm = false;
            }
            catch (UriFormatException ex)
            {
                MessageBox.Show("O endereco não é válido!", "ATENÇÂO");
            }
            catch (ThreadAbortException ex)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRO");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnOk_Click(object sender, EventArgs e)
        {
            GO();
        }

        /// <summary>
        /// 
        /// </summary>
        public void GO()
        {
            if (this.InvokeRequired)
            {
                ExecuteHandler d = new ExecuteHandler(GO);
                this.Invoke(d, new object[] { });
            }
            else
            {
                this.Select();
                this.Focus();

                if (!string.IsNullOrEmpty(this.URL) ||
                    !string.IsNullOrEmpty(this.FileName))
                {
                    this.Text = "Processando Curriculo";
                    DoSearch();
                }
                else
                {
                    MessageBox.Show("O endereco não é válido!", "ATENÇÂO");
                }

                if (m_blnGoToResultForm)
                {
                    if (m_udtAutor != null)
                    {
                        FrmResult frmResult = new FrmResult(m_udtAutor);
                        frmResult.MdiParent = this.MdiParent;
                        frmResult.Show();
                    }
                    else if (m_lstAutores != null)
                    {
                        FrmBathResult frmResult = new FrmBathResult(m_lstAutores);
                        frmResult.MdiParent = this.MdiParent;
                        frmResult.Show();
                    }

                    this.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.URLVisible)
            {
                this.Text = "Endereço do Currículo Lattes";
                Common.FlowHandler.Cancel();
                progressBar.Value = 0;
                btnCancel.Enabled = false;                
                btnOk.Enabled = true;
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmEnterURL_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_oThread != null && m_oThread.IsAlive)
                m_oThread.Abort();
        }

        //-------------------------------------------------------------------------------------------------

        void m_udtCurriculo_StartScholarSearch(int _intCountArtigos)
        {
            int intStep = _intCountArtigos != 0 ? (100000 / _intCountArtigos) : 100000;
            this.SetStepValue(intStep);
            this.SetProgressBarValue(0);
            
            Application.DoEvents();

        }
        
        void m_udtCurriculo_ScholarSearchStep(PublicacaoInfo _udtPublicacao)
        {
            this.PerformStep();
            this.SetProgressBarTextArtigo(_udtPublicacao.Titulo);                   
            Application.DoEvents();
        }        

        void m_udtCurriculo_EndScholarSearch()
        {
            this.SetProgressBarTextArtigo("Buscando curriculo");
            this.SetProgressBarTextAutor("");
            Application.DoEvents();
        }

        void m_coreCurriculo_SearchGetAutor(AutorInfo _udtAutor)
        {
            this.SetProgressBarTextAutor(_udtAutor.Nome);          
            Application.DoEvents();
        } 

        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// Executa um passo do progressBar.
        /// </summary>
        public void PerformStep()
        {
            if (this.progressBar.InvokeRequired)
            {
                ExecuteHandler d = new ExecuteHandler(PerformStep);
                this.Invoke(d, new object[] { });
            }
            else
            {
                this.progressBar.PerformStep();
            }
        }

        /// <summary>
        /// Seta o valor maximo da progressBar.
        /// </summary>
        /// <param name="_intValue">valor.</param>
        public void SetProgressBarValue(int _intValue)
        {
            if (this.progressBar.InvokeRequired)
            {
                SetIntValueHandler d = new SetIntValueHandler(SetProgressBarValue);
                this.Invoke(d, new object[] { _intValue });
            }
            else
            {
                this.progressBar.Value = _intValue;
            }
        }

        /// <summary>
        /// Seta o valor de cada passo da progressBar.
        /// </summary>
        /// <param name="_intValue">valor do passo.</param>
        public void SetStepValue(int _intValue)
        {
            if (this.progressBar.InvokeRequired)
            {
                SetIntValueHandler d = new SetIntValueHandler(SetStepValue);
                this.Invoke(d, new object[] { _intValue });
            }
            else
            {
                this.progressBar.Step = _intValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_strValue"></param>
        public void SetProgressBarTextArtigo(string _strValue)
        {
            if (this.lblArtigo.InvokeRequired)
            {
                SetStringValueHandler d = new SetStringValueHandler(SetProgressBarTextArtigo);
                this.Invoke(d, new object[] { _strValue });
            }
            else
            {
                this.lblArtigo.Text = _strValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_strValue"></param>
        public void SetProgressBarTextAutor(string _strValue)
        {
            if (this.lblAutor.InvokeRequired)
            {
                SetStringValueHandler d = new SetStringValueHandler(SetProgressBarTextAutor);
                this.Invoke(d, new object[] { _strValue });
            }
            else
            {
                this.lblAutor.Text = _strValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uriUrl"></param>
        void m_udtCurriculo_GoogleDenyAccess(Uri _uriUrl)
        {
            if (this.MdiParent.InvokeRequired)
            {
                SetUriValueHandler d = new SetUriValueHandler(m_udtCurriculo_GoogleDenyAccess);
                this.Invoke(d, new object[] { _uriUrl });
            }
            else
            {
                FrmNavegator frmNavegator = new FrmNavegator();
                frmNavegator.MdiParent = this.MdiParent;
                frmNavegator.SetURL(_uriUrl.ToString());
                frmNavegator.Show();

                while (!frmNavegator.IsDisposed)
                {
                    Application.DoEvents();
                    Thread.Sleep(100);
                }
            }
        }        
    }
}