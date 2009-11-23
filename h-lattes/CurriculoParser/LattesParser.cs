using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using Model;
using Model.Filtros;
using System.Web;
using System.Xml;
using log4net;
using System.Text.RegularExpressions;
using Common;

namespace CurriculoParser
{
    public class LattesParser
    {
        const string m_strCaptchaURL = "http://buscatextual.cnpq.br/buscatextual/simagem";

        const string m_strBuscaURL = "http://buscatextual.cnpq.br/buscatextual/busca.do?registros=0;100";              

        const string m_strArtigosCompletosPeriodicos = "Artigos  completos publicados em\n\t\t\t\t\t\tperiódicos";

        const string m_strLivrosPublicados = "Livros\n\t\t\t\t\t\tpublicados/organizados ou edições";

        const string m_strTrabalhosPublicadosAnais = "Trabalhos completos\n\t\t\t\t\t publicados em anais de congressos";

        const string m_strCapitulosLivros = "Capítulos de livros\n\t\t\t\t\t\tpublicados";

        const string m_strResumosExpandidos = "Resumos expandidos \n\t\t\t\t\t publicados em anais de congressos";

        const string m_strResumosPublicados = "Resumos \n\t\t\t\t\t publicados em anais de congressos";

        const string m_strDemaisTipos = "Demais tipos de\n\t\t\t\t\t\t\tprodução bibliográfica";

        const string m_strArtigosAceitos = "Artigos  aceitos para publicação";

         const string m_strTextosJornaisRevistas = "Textos em jornais de\n\t\t\t\t\t\tnotícias/revistas";
        

        /// <summary>
        /// Objeto Logger.
        /// </summary>
        ILog m_log;        

        /// <summary>
        /// Lista de publicações aceitas pelo parser.
        /// </summary>
        private List<ETipoPublicacao> m_lstTiposAceitos = null;

        /// <summary>
        /// 
        /// </summary>
        public LattesParser()
        {
            m_log = LogManager.GetLogger(GetType());

            m_lstTiposAceitos = new List<ETipoPublicacao>();
            m_lstTiposAceitos.Add(ETipoPublicacao.ArtigosCompletosPeriodicos);
            m_lstTiposAceitos.Add(ETipoPublicacao.LivrosPublicados);
            m_lstTiposAceitos.Add(ETipoPublicacao.TrabalhosPublicadosAnais);
            m_lstTiposAceitos.Add(ETipoPublicacao.CapitulosLivros);
            m_lstTiposAceitos.Add(ETipoPublicacao.ArtigosAceitosPublicacao);
            //m_lstTiposAceitos.Add(ETipoPublicacao.TextosJornaisRevistas);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uriUrl"></param>
        /// <returns></returns>
        public List<PublicacaoInfo> GetArtigos(Uri _uriUrl)
        {
            return this.GetArtigos(Parser.GetHtml(_uriUrl));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uriUrl"></param>
        /// <returns></returns>
        public AutorInfo GetAutor(Uri _uriUrl)
        { 
            string strPage = Parser.GetHtml(_uriUrl);
            AutorInfo udtReturn = GetAutor(strPage);
            udtReturn.Publicacoes = GetArtigos(strPage);

            return udtReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_strHtml"></param>
        /// <returns></returns>
        private AutorInfo GetAutor(string _strHtml)
        {
            string strInicio = "<em>Dados pessoais</em>";
            string strFim = "<em>Forma&ccedil;&atilde;o acad&ecirc;mica/Titula&ccedil;&atilde;o</em>";

            _strHtml = Parser.RecortaHtml(strInicio, strFim, _strHtml);
            _strHtml = Parser.RemoveTag("a", _strHtml);
            _strHtml = Parser.RemoveTag("img", _strHtml);
            _strHtml = Parser.RemoveTag("div", _strHtml);
            _strHtml = Parser.RemoveTag("br", _strHtml);
            _strHtml = Parser.RemoveTag("span", _strHtml);
            _strHtml = Parser.RemoveTag("table", _strHtml);
            _strHtml = Parser.RemoveTag("tr", _strHtml);

            //cria xml 
            StringBuilder tbTemp = new StringBuilder();

            XmlWriterSettings xws = new XmlWriterSettings();
            xws.Encoding = new UTF8Encoding();

            XmlWriter xmlWriter = XmlWriter.Create(tbTemp, xws);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("root");
            xmlWriter.WriteRaw(_strHtml);
            xmlWriter.WriteFullEndElement();
            xmlWriter.Close();

            //Realiza parse                
            String temp = HttpUtility.HtmlDecode(tbTemp.ToString());
            temp = temp.Replace("&", "&amp;");

            XmlReader xmlReader = XmlReader.Create(new StringReader(temp));

            AutorInfo udtReturn = new AutorInfo();

            int intStatus = 0;
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    if (xmlReader.GetAttribute("class") == "DadGerTabelaCelula26")
                    {
                        string strContent = xmlReader.ReadElementContentAsString();
                        
                        if (strContent.Trim() == "Nome")
                        { intStatus = 1; }                        
                        else if (strContent.Trim() == "Nome em citações bibliográficas")
                        { intStatus = 2; }
                    }

                    if (xmlReader.GetAttribute("class") == "DadGerTabelaCelula74")
                    {
                        if (intStatus == 1)
                        {
                            udtReturn.Nome = xmlReader.ReadElementContentAsString();                            
                        }
                        else if (intStatus == 2)
                        {
                            udtReturn.NomeCitacoes = xmlReader.ReadElementContentAsString();
                            break;
                        }
                    }
                }
            }

            return udtReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_strHtml"></param>
        /// <returns></returns>
        private List<PublicacaoInfo> GetArtigos(String _strHtml)
        {
            try
            {
                string strInicio = "<a name=\"Producaobibliografica\"></a>";
                string strFim = "<a name=\"Producaotecnica\"></a>"; 

                try
                {
                    //descarta inicio e fim                    
                    _strHtml = Parser.RecortaHtml(strInicio, strFim, _strHtml);
                }
                catch
                {
                    try
                    {
                         strFim = "<a name=\"Bancas\"></a>";
                        _strHtml = Parser.RecortaHtml(strInicio, strFim, _strHtml);
                    }
                    catch
                    {
                        try
                        {
                            strFim = "<a name=\"Participacaoemeventos\"></a>";
                            _strHtml = Parser.RecortaHtml(strInicio, strFim, _strHtml);
                        }
                        catch
                        {
                            strFim = "<a name=\"Orientacoes\"></a>";
                            _strHtml = Parser.RecortaHtml(strInicio, strFim, _strHtml);
                        }

                    }
                }

                //_strHtml = Parser.RemovePropriedade("src", _strHtml);
                //_strHtml = Parser.RemovePropriedade("href", _strHtml);
                //_strHtml = Parser.RemovePropriedade("id", _strHtml);
                //_strHtml = Parser.RemovePropriedade("onclick", _strHtml);
                //_strHtml = Parser.RemovePropriedade("onload", _strHtml);

                _strHtml = Regex.Replace(_strHtml, @"<([^>]*)(?:src|href|id|onclick|onload|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase);
                _strHtml = Regex.Replace(_strHtml, @"<([^>]*)(?:src|href|id|onclick|onload|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase);
                _strHtml = Regex.Replace(_strHtml, @"<([^>]*)(?:src|href|id|onclick|onload|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase);

                //remove tags sem fechamento ou inuteis
                _strHtml = Parser.RemoveTag("a", _strHtml);
                _strHtml = Parser.RemoveTag("img", _strHtml);
                _strHtml = Parser.RemoveTag("div", _strHtml);
                _strHtml = Parser.RemoveTag("br", _strHtml);
                _strHtml = Parser.RemoveTag("span", _strHtml);
                _strHtml = Parser.RemoveTag("table", _strHtml);
                _strHtml = Parser.RemoveTag("tr", _strHtml);
                _strHtml = Parser.RemoveTag("sup", _strHtml);

                //cria xml 
                StringBuilder tbTemp = new StringBuilder();

                XmlWriterSettings xws = new XmlWriterSettings();
                xws.Encoding = new UTF8Encoding();

                XmlWriter xmlWriter = XmlWriter.Create(tbTemp, xws);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("root");
                xmlWriter.WriteRaw(_strHtml);
                xmlWriter.WriteFullEndElement();
                xmlWriter.Close();

                //Realiza parse   
                String temp = tbTemp.ToString();
                temp = temp.Replace("&gt;", "#pua1##");
                temp = temp.Replace("&lt;", "#pua2##");
                temp = HttpUtility.HtmlDecode(temp);
                temp = temp.Replace("&", "&amp;");
                temp = temp.Replace("#pua2##", "&lt;");
                temp = temp.Replace("#pua1##", "&gt;");
                //temp = SecurityElement.Escape(temp);

                XmlReader xmlReader = XmlReader.Create(new StringReader(temp));

                ETipoPublicacao enmStatus = ETipoPublicacao.ArtigosCompletosPeriodicos;

                List<string> lstPublicacoes = new List<string>();
                List<ETipoPublicacao> lstTipos = new List<ETipoPublicacao>();

                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        if (xmlReader.GetAttribute("class") == "agrupadorsub")
                        {
                            string strContent = xmlReader.ReadElementContentAsString();

                            if (strContent.IndexOf(m_strArtigosCompletosPeriodicos) >= 0)
                                enmStatus = ETipoPublicacao.ArtigosCompletosPeriodicos;
                            else if (strContent.IndexOf(m_strLivrosPublicados) >= 0)
                                enmStatus = ETipoPublicacao.LivrosPublicados;
                            else if (strContent.IndexOf(m_strTrabalhosPublicadosAnais) >= 0)
                                enmStatus = ETipoPublicacao.TrabalhosPublicadosAnais;
                            else if (strContent.IndexOf(m_strCapitulosLivros) >= 0)
                                enmStatus = ETipoPublicacao.CapitulosLivros;
                            else if (strContent.IndexOf(m_strResumosExpandidos) >= 0)
                                enmStatus = ETipoPublicacao.ResumosExpandidos;
                            else if (strContent.IndexOf(m_strResumosPublicados) >= 0)
                                enmStatus = ETipoPublicacao.ResumosPublicados;
                            else if (strContent.IndexOf(m_strDemaisTipos) >= 0)
                                enmStatus = ETipoPublicacao.DemaisTipos;
                            else if (strContent.IndexOf(m_strArtigosAceitos) >= 0)
                                enmStatus = ETipoPublicacao.ArtigosAceitosPublicacao;
                            else if (strContent.IndexOf(m_strTextosJornaisRevistas) >= 0)
                                enmStatus = ETipoPublicacao.TextosJornaisRevistas;
                        }

                        if (xmlReader.GetAttribute("class") == "textoProducao")
                        {
                            string strTemp = HttpUtility.HtmlDecode(xmlReader.ReadElementContentAsString());

                            if (strTemp.IndexOf("<![CDATA[") > 0)
                            {
                                strTemp = strTemp.Replace("<![CDATA[", string.Empty);
                                strTemp = strTemp.Replace("]]>", string.Empty);
                            }

                            lstPublicacoes.Add(strTemp);
                            lstTipos.Add(enmStatus);
                        }
                    }
                }

                List<PublicacaoInfo> lstRetorno = new List<PublicacaoInfo>();
                for (int i = 0; i < lstPublicacoes.Count; i++)
                {
                    if (m_lstTiposAceitos.Contains(lstTipos[i]))
                    {
                        PublicacaoInfo udtPublicacao = new PublicacaoInfo();
                        udtPublicacao.TipoPublicacao = lstTipos[i];
                        string strTemp = lstPublicacoes[i];

                        string strT = strTemp.Replace("\t", String.Empty);
                        strT = strT.Replace("\n", String.Empty);

                        String[] arrString = strT.Split(new string[] { " . " }, StringSplitOptions.None);
                        udtPublicacao.Autores = arrString[0].Trim();

                        arrString = arrString[1].Split(new string[] { "In:" }, StringSplitOptions.None);
                        udtPublicacao.Titulo = arrString[0].Trim();

                        if (arrString.Length == 2)
                        {
                            udtPublicacao.Veiculo = arrString[1].Trim();
                        }

                        else if (udtPublicacao.TipoPublicacao == ETipoPublicacao.ArtigosCompletosPeriodicos)
                        {
                            arrString = arrString[0].Split(new string[] { "." },
                                StringSplitOptions.RemoveEmptyEntries);

                            if (arrString.Length > 1)
                            {
                                udtPublicacao.Titulo = arrString[0].Trim();
                                udtPublicacao.Veiculo = string.Join(".", arrString, 1, arrString.Length-1);
                            }
                        }

                        else if (udtPublicacao.TipoPublicacao == ETipoPublicacao.ArtigosAceitosPublicacao)
                        {
                            arrString = arrString[0].Split(new string[] { "." },
                               StringSplitOptions.RemoveEmptyEntries);

                            if (arrString.Length > 1)
                            {
                                udtPublicacao.Titulo = arrString[0].Trim();
                                udtPublicacao.Veiculo = string.Join(".", arrString, 1, arrString.Length - 1);
                            }
                        }
                        else if (udtPublicacao.TipoPublicacao == ETipoPublicacao.LivrosPublicados)
                        {
                            arrString = arrString[0].Split(new string[] { "." },
                                StringSplitOptions.RemoveEmptyEntries);
                           
                            udtPublicacao.Titulo = arrString[0].Trim();
                            udtPublicacao.Veiculo = string.Empty;
                        }

                        lstRetorno.Add(udtPublicacao);
                    }
                }

                //Tira repetidos

                List<string> lstJahTem = new List<string>();

                for (int i = 0; i < lstRetorno.Count; i++)
                {
                    lstRetorno[i].Titulo = lstRetorno[i].Titulo.Trim(".".ToCharArray());
                }

                for (int i = 0; i < lstRetorno.Count; i++)
                {
                    if (lstJahTem.Contains(lstRetorno[i].Titulo))
                    {
                        lstRetorno.RemoveAt(i);
                        i--;
                    }
                    else
                        lstJahTem.Add(lstRetorno[i].Titulo);
                }

                return lstRetorno;
            }
            catch (Exception ex)
            {
                m_log.Error("Erro de parse.", ex);
                List<PublicacaoInfo> lstRetorno = new List<PublicacaoInfo>();
                return lstRetorno;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Stream GetImage()
        {
            return Parser.GetImage(m_strCaptchaURL, MyConstants.MyCookies);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_udtFiltro"></param>
        /// <remarks>
        /// - Retorna nullo caso nao tenha avancado de pagina (captcha incorreto).
        /// - Retorna lista vazia caso não tenha resultados.
        /// - Lista preenchida caso tenha.        
        /// </remarks>
        /// <exception cref="">ParserException</exception>
        /// <returns></returns>
        public List<AutorInfo> RealisaBusca(FiltroBuscaAutor _udtFiltro)
        {
            try
            {
                Dictionary<string, string> dctData = new Dictionary<string, string>();                              

                dctData["metodo"] = "buscar";
                dctData["acao"] = "";
                dctData["resumoFormacao"] = "";
                dctData["resumoAtividade"] = "";
                dctData["resumoAtuacao"] = "";
                dctData["resumoProducao"] = "";
                dctData["resumoPesquisador"] = "";
                dctData["resumoIdioma"] = "";
                dctData["resumoPresencaDGP"] = "";
                dctData["resumoModalidade"] = "";
                dctData["buscaAvancada"] = "0";
                dctData["filtros.buscaNome"] = "true";
                dctData["textoBusca"] = HttpUtility.UrlEncode(_udtFiltro.Nome);
                dctData["buscarDoutores"] = _udtFiltro.Doutor ? "true" : "false";
                dctData["textoBuscaTodas"] = "";
                dctData["textoBuscaFrase"] = "";
                dctData["textoBuscaQualquer"] = "";
                dctData["textoBuscaNenhuma"] = "";
                dctData["textoExpressao"] = "";
                dctData["buscarDoutoresAvancada"] = "true";
                dctData["filtros.atualizacaoCurriculo"] = "18";
                dctData["quantidadeRegistros"] = "10";
                dctData["filtros.radioPeriodoProducao"] = "1";
                dctData["filtros.visualizaPeriodoProducaoCV="] = "";
                dctData["filtros.categoriaNivelBolsa"] = "";
                dctData["filtros.modalidadeBolsa"] = "0";
                dctData["filtros.nivelFormacao"] = "0";
                dctData["filtros.paisFormacao"] = "0";
                dctData["filtros.regiaoFormacao"] = "0";
                dctData["filtros.ufFormacao"] = "0";
                dctData["filtros.nomeInstFormacao"] = "";
                dctData["filtros.conceitoCurso"] = "";
                dctData["filtros.buscaAtuacao"] = "false";
                dctData["filtros.codigoGrandeAreaAtuacao"] = "0";
                dctData["filtros.codigoAreaAtuacao"] = "0";
                dctData["filtros.codigoSubareaAtuacao"] = "0";
                dctData["filtros.codigoEspecialidadeAtuacao"] = "0";
                dctData["filtros.orientadorCNPq"] = "";
                dctData["filtros.idioma"] = "0";
                dctData["filtros.grandeAreaProducao"] = "0";
                dctData["filtros.areaProducao"] = "0";
                dctData["filtros.setorProducao"] = "0";
                dctData["filtros.naturezaAtividade"] = "0";
                dctData["filtros.paisAtividade"] = "0";
                dctData["filtros.regiaoAtividade"] = "0";
                dctData["filtros.ufAtividade"] = "0";
                dctData["filtros.nomeInstAtividade"] = "";
                dctData["palavra"] = _udtFiltro.Captcha;
               
                string strHtml = Parser.DoPost(new Uri(m_strBuscaURL), dctData, MyConstants.MyCookies);

                //se tiver constante abaixo é pq o captcha não foi digitado corretamente.                
                if (strHtml.IndexOf("<label>Buscar por: </label>") >= 0)
                    return null;

                //strHtml = Parser.RecortaHtml("<ol type='1' start=\"1\" >", "</ol>", strHtml);
                strHtml = Parser.RecortaHtml("<ol", "</ol>", strHtml);
                strHtml = strHtml.Substring(strHtml.IndexOf('>') + 1);
                strHtml = Parser.RemoveTag("img", strHtml);
                strHtml = Parser.RemoveTag("b", strHtml);

                strHtml = HttpUtility.HtmlDecode(strHtml);
                strHtml = strHtml.Trim();
                strHtml = strHtml.Replace("\t", string.Empty);
                strHtml = strHtml.Replace("\n", string.Empty);
                strHtml = strHtml.Replace("\r", string.Empty);

                List<AutorInfo> lstReturn = new List<AutorInfo>();

                string[] arrAutores = strHtml.Split(new string[] { "<li>", "<LI>", "</li>", "</LI>" }, 
                    StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < arrAutores.Length; i++)
                {
                    string strLink = "<a href=\"javascript:abreDetalhe('";
                    int pos = arrAutores[i].IndexOf(strLink);
                    string strKey = arrAutores[i].Substring(pos + strLink.Length, 10);

                    arrAutores[i] = arrAutores[i].Substring(arrAutores[i].IndexOf('>') + 1);
                    string strAutor = arrAutores[i].Substring(0, arrAutores[i].IndexOf('<')).Trim();

                    AutorInfo udtAutor = new AutorInfo();
                    udtAutor.Key = strKey;
                    udtAutor.Nome = strAutor;

                    lstReturn.Add(udtAutor);
                }

                return lstReturn;
            }
            catch(Exception ex)
            {
                m_log.Error("Erro de parser.", ex);
                throw new Model.Exceptions.ParserException(ex);
            }
        }      
    }
}
