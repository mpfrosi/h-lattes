using System;
using System.Collections.Generic;
using System.Text;
using Model;
using System.Web;
using System.Text.RegularExpressions;
using log4net;
using System.Net;
using System.Threading;
using Common;
using System.IO;
using System.Windows.Forms;

namespace CurriculoParser
{
    /// <summary>
    /// Delegate para evento disparado ao iniciar as buscas no google scholar.
    /// </summary>
    /// <param name="_intCountArtigos">Numero de artigos a serem pesquisados.</param>
    public delegate void StartScholarSearchHandler(int _intCountArtigos);

    /// <summary>
    /// Delegate para evento disparado ao se terminar a busca de um artigo.
    /// </summary>
    /// <param name="_udtPublicacao">Publicacao pesquisada.</param>
    public delegate void ScholarSearchStepHandler(PublicacaoInfo _udtPublicacao);

    /// <summary>
    /// Delegate para evento disparado ao se terinar a busca no google scholar.
    /// </summary>
    public delegate void EndScholarSearchHandler();


    public delegate void UnautorisedAccess(Uri _uriAddress);

    /// <summary>
    /// Parser para o Google scholar.
    /// </summary>
    public class ScholarParser
    {
        /// <summary>
        /// Logger.
        /// </summary>
        ILog m_log;

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public ScholarParser()
        {
            m_log = LogManager.GetLogger(this.GetType());
        }

        private const string m_strNoResultPartern =
            "</b> - did not match any articles.<br>";

        /// <summary>
        /// 
        /// </summary>
        public event StartScholarSearchHandler StartScholarSearch;

        /// <summary>
        /// 
        /// </summary>
        public event ScholarSearchStepHandler ScholarSearchStep;

        /// <summary>
        /// 
        /// </summary>
        public event EndScholarSearchHandler EndScholarSearch;

        public event UnautorisedAccess GoogleDenyAccess;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_udtAutor"></param>
        /// <returns></returns>
        public AutorInfo PopulaCitacoes(AutorInfo _udtAutor)
        {
            //verifica se argumento não eh null.
            if (_udtAutor == null)
                throw new ArgumentNullException("_udtAutor");

            //percorre lista de publicações do autor, populando as citações
            if (_udtAutor.Publicacoes != null)

                //dispara evento de inicioda busca
                OnStartScholarSearch(_udtAutor.Publicacoes.Count);

            foreach (PublicacaoInfo udtPub in _udtAutor.Publicacoes)
            {
                Common.FlowHandler.VerifyCancel();

                //dispara evento
                OnScholarSearchStep(udtPub);

                m_log.Info("----------------------------------------------------------------");
                m_log.Info("Publicação pesquisada: " + udtPub.Titulo);


                List<PublicacaoInfo> lstTemp = GetArtigos(udtPub, _udtAutor);

                if (MyConstants.SchloarTimeout > 0)
                    Thread.Sleep(MyConstants.SchloarTimeout);

                if (lstTemp != null && lstTemp.Count > 0)
                {
                    m_log.Info("Publicação encontrada: " + lstTemp[0].Titulo);
                    m_log.Info("Citações: " + lstTemp[0].Citacoes);
                    udtPub.Citacoes = lstTemp[0].Citacoes;
                }
                else
                {
                    m_log.Info("Nenhum resultado para o artigo.");
                }

                m_log.Info("----------------------------------------------------------------");

                Common.FlowHandler.VerifyCancel();
            }

            //dispara evento de fim
            OnEndScholarSearch();

            return _udtAutor;
        }

        #region "EventHandlers"

        private void OnStartScholarSearch(int _intCountArtigos)
        {
            if (StartScholarSearch != null)
                StartScholarSearch(_intCountArtigos);
        }

        private void OnScholarSearchStep(PublicacaoInfo _udtPublicacao)
        {
            if (ScholarSearchStep != null)
                ScholarSearchStep(_udtPublicacao);
        }

        private void OnEndScholarSearch()
        {
            if (EndScholarSearch != null)
                EndScholarSearch();
        }

        #endregion "EventHandlers"

        /// <summary>
        /// Busca no google scholar artigos com base nas informações 
        /// do artigo recebido como parametro.
        /// </summary>
        /// <param name="_udtPublicacao">Artigo base para a busca.</param>
        /// <returns>Lista de artigos retornados pelo google scholar.</returns>
        public List<PublicacaoInfo> GetArtigos(PublicacaoInfo _udtPublicacao, AutorInfo _udtAutor)
        {
            bool blnTinhaAcentos;
            List<string> lstAlternareQuery;
            string strQuery = MontaQuery(_udtPublicacao, _udtAutor, out lstAlternareQuery);
            //string strQuerySemAcentos = Parser.TirarAcentos(strQuery,out blnTinhaAcentos);

            m_log.Info("Query: " + strQuery);

            List<PublicacaoInfo> lstReturn = this.DoSearch(strQuery);

            ////se tinha acentos e nao retornou resultados tenta sem acento.
            //if (blnTinhaAcentos && (lstReturn == null || lstReturn.Count == 0))
            //{
            //    m_log.Info("Sem resultados e com acentos na busca. Tentando sem acentos.");
            //    m_log.Info("Query: " + strQuerySemAcentos);
            //    lstReturn = this.DoSearch(strQuerySemAcentos);
            //}

            //se ainda nao tem resulatado tenta query alternativa
            if (lstReturn == null || lstReturn.Count == 0)
            {
                if (lstAlternareQuery.Count > 0)
                {
                    string strAlternateQuery = Parser.TirarAcentos(lstAlternareQuery[0],out blnTinhaAcentos);
                    m_log.Info("Sem resultados. Tentando query alternativa.");
                    m_log.Info("Query: " + strAlternateQuery);
                    lstReturn = this.DoSearch(strAlternateQuery);
                }
            }

            return lstReturn;
        }

        /// <summary>
        /// Monta query a ser pesquisada no scholar.
        /// </summary>
        /// <param name="_udtPublicacao"></param>
        /// <param name="_udtAutor"></param>
        /// <returns></returns>
        private string MontaQuery(PublicacaoInfo _udtPublicacao, AutorInfo _udtAutor, out List<string> _lstAlternativeQuery)
        {
            _lstAlternativeQuery = new List<string>();

            String strQuery = "allintitle:" + _udtPublicacao.Titulo;

            string[] arrTitleWords = _udtPublicacao.Titulo.Split(new char[] { ' ', '-' },
                StringSplitOptions.RemoveEmptyEntries);

            //tira palavras pequenas do começo do artigo
            while (arrTitleWords.Length > 0 && arrTitleWords[0].Length < 3)
            {
                List<string> lstTemp = new List<string>(arrTitleWords);
                lstTemp.RemoveAt(0);
                arrTitleWords = lstTemp.ToArray();
            }

            String strQuery2 = "intitle:\"{0}\" OR intitle:\"{1}\"";
            StringBuilder sblSubQuery1 = new StringBuilder();
            StringBuilder sblSubQuery2 = new StringBuilder();

            for (int i = 0; i < arrTitleWords.Length; i++)
            {
                if (i == 0 || i == (arrTitleWords.Length - 1))
                {
                    sblSubQuery1.Append(arrTitleWords[i] + (i == 0 ? " " : ""));
                    sblSubQuery2.Append("*" + (i == 0 ? " " : ""));
                }
                else if (i == arrTitleWords.Length / 2)
                {
                    sblSubQuery1.Append(arrTitleWords[i] + " ");
                    sblSubQuery2.Append("*" + " ");
                }
                else
                {
                    sblSubQuery2.Append(arrTitleWords[i] + " ");
                    sblSubQuery1.Append("* ");
                }
            }

            strQuery2 = String.Format(strQuery2, sblSubQuery1, sblSubQuery2);

            string[] arrAutor = _udtAutor.NomeCitacoes.Split(new char[] { ';' },
                StringSplitOptions.RemoveEmptyEntries);

            string[] arrPublicacao = _udtPublicacao.Autores.Split(new char[] { ';' },
                StringSplitOptions.RemoveEmptyEntries);

            List<string> lstAuthorWords = new List<string>();

            //se tem nome do autor nas citações 
            if (arrAutor.Length > 0)
            {
                for (int i = 0; i < arrAutor.Length; i++)
                {
                    arrAutor[i] = string.Format(" author:\"{0}\"", arrAutor[i]);

                    bool blnHaveAcents;
                    string strTmp = Parser.TirarAcentos(arrAutor[i], out blnHaveAcents);

                    if (blnHaveAcents)
                    {
                        lstAuthorWords.Add(strTmp);
                    }
                }

                lstAuthorWords.AddRange(arrAutor);
            }

            //se não conseguiu pegar nome nas citações do autor, tenta as da publicação
            else if (arrPublicacao.Length > 0)
            {
                for (int i = 0; i < arrPublicacao.Length; i++)
                {
                    arrPublicacao[i] = string.Format(" author:\"{0}\"", arrPublicacao[i]);

                    bool blnHaveAcents;
                    string strTmp = Parser.TirarAcentos(arrPublicacao[i], out blnHaveAcents);
                    if (blnHaveAcents)
                    {
                        lstAuthorWords.Add(strTmp);
                    }
                }

                lstAuthorWords.AddRange(arrPublicacao);
            }

            //adiciona ultimo sobrenome do autor
            arrAutor = _udtAutor.Nome.Split(new char[] { ' ', ',', '.', ';' },
                StringSplitOptions.RemoveEmptyEntries);

            if (arrAutor.Length > 0)
            {
                bool tt;
                string strTemp = " author:" + arrAutor[arrAutor.Length - 1];
                string strTemp2 = Parser.TirarAcentos(strTemp, out tt);
                lstAuthorWords.Add(strTemp);
                if (tt) lstAuthorWords.Add(strTemp2);
            }

            string strAutorQuery = string.Join(" OR ", lstAuthorWords.ToArray());
            strAutorQuery = strAutorQuery.Replace("  ", " ");

            strQuery += strAutorQuery;
            strQuery2 += strAutorQuery;

            if (arrTitleWords.Length >= 5)
                _lstAlternativeQuery.Add(strQuery2);

            return strQuery;
        }

        /// <summary>
        /// Realiza a busca no site do google scholar.
        /// </summary>
        /// <param name="_strQuery">Query a ser pesquisada.</param>
        /// <returns></returns>
        private List<PublicacaoInfo> DoSearch(string _strQuery)
        {
            _strQuery = HttpUtility.UrlEncode(_strQuery);

            Uri uriUrl = new Uri(string.Format("http://scholar.google.com/scholar?q={0}&hl=en&lr=&btnG=Search",
                _strQuery));

            Parser udtParser = new Parser();
            udtParser.Inicializa_WebBrowser();

            string strContent = null;

            do
            {
                if (strContent != null)
                {
                    if (GoogleDenyAccess != null)
                    {
                        udtParser.Dispose_WebBrowser();
                        GoogleDenyAccess(uriUrl);
                        udtParser.Inicializa_WebBrowser();
                    }
                }

                FlowHandler.VerifyCancel();

                strContent = udtParser.Use_WebBrowser(uriUrl.ToString());

            } while (IsSorryPage(strContent));

            return this.GetArtigos(strContent);
        }

        /// <summary>
        /// Realiza parse da pagina de resultados do google scholar.
        /// </summary>
        /// <param name="_strHtml">Html a ser processado.</param>
        /// <returns>Lista de artigos da pagina. NULL se ocorreu erro de parser.</returns>
        public List<PublicacaoInfo> GetArtigos(string _strHtml)
        {
            try
            {
                List<PublicacaoInfo> lstReturn = new List<PublicacaoInfo>();

                //verifica se tem resultados                
                if (_strHtml.IndexOf(m_strNoResultPartern) > 0)
                {
                    m_log.Info("Scholar não encontrou nenhum resultado.");
                    return lstReturn;
                }

                int intPos = _strHtml.IndexOf("<h3");
                intPos = intPos >= 0 ? intPos : _strHtml.IndexOf("<H3");
                _strHtml = _strHtml.Substring(intPos);                               

                //recorta soh os resultados
                intPos = _strHtml.IndexOf("<table");
                intPos = intPos >= 0 ? intPos : _strHtml.IndexOf("<TABLE");
                string strResultados = _strHtml.Substring(0, intPos);

                //quabra os resultados: cada paragrafo eh um resultados diferente               
                string[] arrResultados = strResultados.Split(new string[] { "<p>", "</p>", "<P>", "</P>" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string strResultado in arrResultados)
                {
                    if (strResultado.IndexOf("Did you mean:") >= 0)
                        continue;

                    //remove tags inuteis
                    string strTemp = Parser.RemoveTag("b", strResultado);
                    strTemp = Parser.RemoveTag("span", strTemp);
                    strTemp = Parser.RemoveTag("font", strTemp);

                    //remove as primeiras tags e chega ateh o titulo
                    while (strTemp[0] == '<')
                    {
                        strTemp = strTemp.Substring(strTemp.IndexOf('>') + 1);

                        if (strTemp.StartsWith("[PDF]"))
                            strTemp = strTemp.Substring(strTemp.IndexOf('<'));
                    }

                    //tituto eh a string ateh a primeira tag
                    string strTitulo = strTemp.Substring(0, strTemp.IndexOf('<'));
                    strTitulo = HttpUtility.HtmlDecode(strTitulo);

                    //pega quantidade de citacoes
                    Match matPos = Regex.Match(strTemp, @">Cited by \d*</a>");

                    int intCitacoes = 0;
                    if (matPos.Success)
                    {
                        intCitacoes = Convert.ToInt32(matPos.Value.Substring(10, matPos.Length - 14));
                    }
                    else
                        intCitacoes = 0;

                    PublicacaoInfo udtTemp = new PublicacaoInfo();
                    udtTemp.Titulo = strTitulo;
                    udtTemp.Citacoes = intCitacoes;
                    lstReturn.Add(udtTemp);
                }               

                return lstReturn;
            }
            catch (Exception ex)
            {
                //m_log.Error("Erro no parser do google Scholar.", ex);
                m_log.Warn("Erro no parser do google Scholar.", ex);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool IsSorryPage(string _strPage)
        {
            return Regex.IsMatch(_strPage, "<H1>We're sorry...</H1>", RegexOptions.IgnoreCase)
                || Regex.IsMatch(_strPage, "<h1>Infelizmente...</h1>", RegexOptions.IgnoreCase);
        }
    }
}
