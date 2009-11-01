using System;
using System.Collections.Generic;
using System.Text;
using CurriculoParser;
using Model;
using Model.Exceptions;
using System.Net;
using Common;
using System.Windows.Forms;
using log4net;
using System.IO;

namespace Core
{
    /// <summary>
    /// Delegate para evento disparado ao iniciar as buscas no google scholar.
    /// </summary>
    /// <param name="_intCountArtigos">Numero de artigos a serem pesquisados.</param>
    public delegate void CoreStartScholarSearchHandler(int _intCountArtigos);

    /// <summary>
    /// Delegate para evento disparado ao se terminar a busca de um artigo.
    /// </summary>
    /// <param name="_udtPublicacao">Publicacao pesquisada.</param>
    public delegate void CoreScholarSearchStepHandler(PublicacaoInfo _udtPublicacao);

    /// <summary>
    /// Delegate para evento disparado ao se terminar a busca de um autor.
    /// </summary>
    /// <param name="_udtAutor"></param>
    public delegate void CoreScholarSearchGetAutorHandler(AutorInfo _udtAutor);

    /// <summary>
    /// Delegate para evento disparado ao se terinar a busca no google scholar.
    /// </summary>
    public delegate void CoreEndScholarSearchHandler();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_uriAddress"></param>
    public delegate void CoreUnautorisedAccess(Uri _uriAddress);

    /// <summary>
    /// Classe que encapsula o acesso à um curriculo.
    /// </summary>
    public class Curriculo
    {
        /// <summary>
        /// 
        /// </summary>
        ILog m_loger;

        /// <summary>
        /// 
        /// </summary>       
        LattesParser m_udtParser = null;

        /// <summary>
        /// 
        /// </summary>
        ScholarParser m_udtScholarParser = null;        
        
        #region Events 

        /// <summary>
        /// 
        /// </summary>
        public event CoreStartScholarSearchHandler StartScholarSearch;

        /// <summary>
        /// 
        /// </summary>
        public event CoreScholarSearchStepHandler ScholarSearchStep;

        /// <summary>
        /// 
        /// </summary>
        public event CoreEndScholarSearchHandler EndScholarSearch;

        /// <summary>
        /// 
        /// </summary>
        public event CoreUnautorisedAccess GoogleDenyAccess;
        
        /// <summary>
        /// 
        /// </summary>
        public event CoreScholarSearchGetAutorHandler SearchGetAutor;

        #endregion Events         

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public Curriculo()
        {
            m_udtParser = new LattesParser();
            m_udtScholarParser = new ScholarParser();
            m_loger = LogManager.GetLogger(GetType());

            m_udtScholarParser.StartScholarSearch += new StartScholarSearchHandler(m_udtScholarParser_StartScholarSearch);
            m_udtScholarParser.ScholarSearchStep += new ScholarSearchStepHandler(m_udtScholarParser_ScholarSearchStep);
            m_udtScholarParser.EndScholarSearch += new EndScholarSearchHandler(m_udtScholarParser_EndScholarSearch);
            m_udtScholarParser.GoogleDenyAccess += new UnautorisedAccess(m_udtScholarParser_GoogleDenyAccess);
        }               

        /// <summary>
        /// Busca o curriculo de um autor.
        /// </summary>
        /// <param name="_strAddress">Endereço do curriculo lattes.</param>
        /// <returns>Model de autor com seus dados preenchidos, bem como suas publicações e as citações.</returns>
        public AutorInfo GetCurriculo(string _strAddress)
        {
            AutorInfo udtAutor = null;

            try
            {
                m_loger.DebugFormat("Inicio do processamento da url {0}", _strAddress);

                udtAutor = m_udtParser.GetAutor(new Uri(_strAddress));

                if (SearchGetAutor != null)
                    SearchGetAutor(udtAutor);

                udtAutor = m_udtScholarParser.PopulaCitacoes(udtAutor);
            }
            catch (CancelException)
            {
                m_loger.Debug("Atividadade cancelada pelo usuário.");
                throw;
            }
            catch (WebException ex)
            {              
                m_loger.Warn("Erro de requisição WEB.", ex);                              
                MessageBox.Show("O google scholar deixou de aceitar pesquisas automatizadas à partir deste computador.\nTente mais tarde novamente.",
                    "ATENÇÂO");
                throw;
            }
            catch (Exception ex)
            {
                m_loger.Error("Erro desconhecido na busca por autor.", ex);
                throw;
            }

            return udtAutor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_strFilePath"></param>
        /// <returns></returns>
        public List<AutorInfo> GetCurriculosByFile(string _strFilePath)
        {
            try
            {
                List<AutorInfo> lstReturn = new List<AutorInfo>();

                using (StreamReader stmReader = new StreamReader(_strFilePath))
                {
                    while (!stmReader.EndOfStream)
                    {
                        string strURL = stmReader.ReadLine();
                        strURL = strURL.Trim();

                        try
                        {                            
                            if (!string.IsNullOrEmpty(strURL))
                                lstReturn.Add(GetCurriculo(strURL));
                        }
                        catch (Exception ex)
                        {
                            m_loger.ErrorFormat("Erro desconhecido na busca do endereço {0}.", strURL);
                            m_loger.Error("Exception: ", ex);
                        }
                    }
                }
                return lstReturn;
            }
            catch (Exception ex)
            {
                m_loger.Error("Erro desconhecido na busca por arquivo.", ex);
                throw;
            }
        }


        #region Event Handlers        

        private void m_udtScholarParser_StartScholarSearch(int _intCountArtigos)
        {
            if (StartScholarSearch != null)
                StartScholarSearch(_intCountArtigos);
        }        

        private void m_udtScholarParser_ScholarSearchStep(PublicacaoInfo _udtPublicacao)
        {
            if (ScholarSearchStep != null)
                ScholarSearchStep(_udtPublicacao);
        }        

        private void m_udtScholarParser_EndScholarSearch()
        {
            if (EndScholarSearch != null)
                EndScholarSearch();
        }

        void m_udtScholarParser_GoogleDenyAccess(Uri _uriAddress)
        {
            if (GoogleDenyAccess != null)
                GoogleDenyAccess(_uriAddress);
        } 

        #endregion Event Handlers        
    }
}
