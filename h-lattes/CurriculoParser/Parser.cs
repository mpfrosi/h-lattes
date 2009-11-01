using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Net;
using System.Xml;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;
using System.Security;
using System.Windows.Forms;
using System.Threading;
using log4net;

namespace CurriculoParser
{
    /// <summary>
    /// 
    /// </summary>
    public class Parser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uriUrl"></param>
        /// <returns></returns>
        public static string GetHtml(Uri _uriUrl)
        {
            WebClient wc = new WebClient();
            UTF8Encoding objUTF8 = new UTF8Encoding();
            byte[] reqHTML = wc.DownloadData(_uriUrl);
            return objUTF8.GetString(reqHTML);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uriUrl"></param>
        /// <returns></returns>
        public static string GetHtml(Uri _uriUrl, CookieContainer _ckcCookies)
        {
            HttpWebRequest objRequest;
            HttpWebResponse objResponse;
            
            objRequest = (HttpWebRequest)WebRequest.Create(_uriUrl);
            objRequest.Method = "GET";
            objRequest.UserAgent = @"User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
            objRequest.Accept = "/";
            objRequest.Headers["Accept-Language"] = "pt-br";

            objRequest.CookieContainer = _ckcCookies;
            objRequest.Timeout = 10000;

            objResponse = (HttpWebResponse)objRequest.GetResponse();
            StreamReader stmReader = new StreamReader(objResponse.GetResponseStream());

            return stmReader.ReadToEnd();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_strTag"></param>
        /// <param name="_strHtml"></param>
        /// <returns></returns>
        public static string RemoveTag(string _strTag, string _strHtml)
        {
            //string strTagRemover = "</?(img|a)(.|\n)*?>";
            string strTagRemover = string.Format("</?{0}(.|\n)*?>", _strTag);
            return Regex.Replace(_strHtml, strTagRemover, string.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Remove todas as ocorrências de uma determinada propriedade HTML.
        /// </summary>
        /// <param name="_strPropriedade"></param>
        /// <param name="_strHtml"></param>
        /// <returns></returns>
        public static string RemovePropriedade(string _strPropriedade, string _strHtml)
        {
            // _strHtml = Regex.Replace(_strHtml, @"<([^>]*)(?:src|href|id|onclick|onload|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>",  RegexOptions.IgnoreCase);
            string strRemover = string.Format(@"<([^>]*)(?{0}|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", _strPropriedade);
            return Regex.Replace(_strHtml, strRemover, "<$1$2>", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Retirar todos os acentos do texto informado.
        /// </summary> 
        /// <param name="_strTexto">String para retirar acentos</param>        
        /// <param name="_blnTinhaAcento">Se texto tinha algum acento.</param>
        /// <returns>String do texto sem acentos</returns>
        public static string TirarAcentos(string _strTexto, out bool _blnTinhaAcento)
        {
            StringBuilder strReturn = new StringBuilder();
            _blnTinhaAcento = false;

            for (int i = 0; i < _strTexto.Length; i++)
            {
                switch (_strTexto[i])
                {

                    case 'ã':
                    case 'â':
                    case 'à':
                    case 'á':
                    case 'ä':
                        strReturn.Append("a");
                        _blnTinhaAcento = true;
                        break;
                    case 'é':
                    case 'è':
                    case 'ë':
                    case 'ê':
                        strReturn.Append("e");
                        _blnTinhaAcento = true;
                        break;
                    case 'í':
                    case 'ì':
                    case 'ï':
                        strReturn.Append("i");
                        _blnTinhaAcento = true;
                        break;
                    case 'õ':
                    case 'ó':
                    case 'ò':
                    case 'ö':
                        strReturn.Append("o");
                        _blnTinhaAcento = true;
                        break;
                    case 'ú':
                    case 'ù':
                    case 'ü':
                        strReturn.Append("u");
                        _blnTinhaAcento = true;
                        break;
                    case 'ç':
                        strReturn.Append("c");
                        _blnTinhaAcento = true;
                        break;
                    case 'Ã':
                    case 'Á':
                    case 'À':
                    case 'Â':
                    case 'Ä':
                        strReturn.Append("A");
                        _blnTinhaAcento = true;
                        break;
                    case 'É':
                    case 'È':
                    case 'Ê':
                    case 'Ë':
                        strReturn.Append("E");
                        _blnTinhaAcento = true;
                        break;
                    case 'Í':
                    case 'Ì':
                    case 'Ï':
                        strReturn.Append("I");
                        _blnTinhaAcento = true;
                        break;
                    case 'Õ':
                    case 'Ó':
                    case 'Ò':
                    case 'Ö':
                        strReturn.Append("O");
                        _blnTinhaAcento = true;
                        break;
                    case 'Ú':
                    case 'Ù':
                    case 'Ü':
                        strReturn.Append("U");
                        _blnTinhaAcento = true;
                        break;
                    case 'Ç':
                        strReturn.Append("C");
                        _blnTinhaAcento = true;
                        break;
                    default:
                        strReturn.Append(_strTexto[i]);                        
                        break;
                }               
            }
            
            return strReturn.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_strTag"></param>
        /// <param name="_strHtml"></param>
        /// <param name="_intQuantidade"></param>
        /// <returns></returns>
        public static string RemoveTag(string _strTag, string _strHtml, int _intQuantidade)
        {
            string strTagRemover = string.Format("</?{0}(.|\n)*?>", _strTag);
            int intRemovidos = 0;

            Match mch = Regex.Match(_strHtml, strTagRemover);

            while (mch.Success)
            {
                if (intRemovidos >= _intQuantidade)
                    break;

                _strHtml = _strHtml.Remove(mch.Index, mch.Length);

                intRemovidos++;

                mch = Regex.Match(_strHtml, strTagRemover);
            }

            return _strHtml;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_strInicio"></param>
        /// <param name="_strFim"></param>
        /// <param name="_strHtml"></param>
        /// <returns></returns>
        public static string RecortaHtml(string _strInicio, string _strFim, string _strHtml)
        {
            int intComeco = _strHtml.IndexOf(_strInicio) + _strInicio.Length;
            int intTamanho = _strHtml.IndexOf(_strFim) - intComeco;
            return _strHtml.Substring(intComeco, intTamanho);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uriEndereco"></param>
        /// <param name="_dctParmns"></param>
        /// <param name="_ckcCookies"></param>
        /// <returns></returns>
        public static string DoPost(Uri _uriEndereco, Dictionary<string, string> _dctParmns, CookieContainer _ckcCookies)
        {
            HttpWebRequest objRequest;
            HttpWebResponse objResponse;

            objRequest = (HttpWebRequest)WebRequest.Create(_uriEndereco);
            objRequest.Method = "POST";
            objRequest.ContentType = "application/x-www-form-urlencoded";
            objRequest.CookieContainer = _ckcCookies;
            objRequest.Timeout = 10000;

            string postData = null;

            foreach (KeyValuePair<string, string> kvpTemp in _dctParmns)
            {
                postData += string.Format("{0}={1}&", kvpTemp.Key, kvpTemp.Value);
            }

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(postData);

            objRequest.GetRequestStream().Write(data, 0, data.Length);
            objRequest.GetRequestStream().Close();

            objResponse = (HttpWebResponse)objRequest.GetResponse();
            StreamReader stmReader = new StreamReader(objResponse.GetResponseStream());

            return stmReader.ReadToEnd();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_strEndereco"></param>
        /// <param name="_cl"></param>
        /// <returns></returns>
        public static Stream GetImage(string _strEndereco, CookieContainer _cl)
        {
            HttpWebRequest objRequest;
            HttpWebResponse objResponse;

            objRequest = (HttpWebRequest)WebRequest.Create(_strEndereco);
            objRequest.Timeout = 10000;
            objRequest.CookieContainer = _cl;
            objResponse = (HttpWebResponse)objRequest.GetResponse();

            return objResponse.GetResponseStream();
        }

        /// <summary>
        /// 
        /// </summary>
        public static string CoockieHeader = "";        

        WebBrowser m_wb = null;
        HtmlDocument tt;
        ILog m_loger;

        public void Inicializa_WebBrowser()
        {
            m_wb = new WebBrowser();
            m_loger = LogManager.GetLogger(GetType());
        }

        public void Dispose_WebBrowser()
        {
            m_wb.Dispose();
        }

        public string Use_WebBrowser(string _strPath)
        {
            if (m_wb.Document != null)
                m_wb.Document.Cookie = CoockieHeader;

            tt = null;

            m_wb.Navigate(_strPath);

            m_wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);

            DateTime dtmInicio = DateTime.Now;

            while (tt == null)
            {
                if (((TimeSpan)(DateTime.Now - dtmInicio)).TotalMilliseconds > 10000)
                {
                    m_wb.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);                    
                    m_loger.Warn("Tempo de espera por resposta de requisição estourou.");
                    return string.Empty;
                }

                Application.DoEvents();
                Thread.Sleep(100);
            }

            m_wb.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);

            CoockieHeader = m_wb.Document.Cookie;
            
            try
            {
                using (StreamReader stmReader = new StreamReader(m_wb.DocumentStream))
                {
                    m_wb.DocumentStream.Seek(0, SeekOrigin.Begin);
                    return stmReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            { 
                return tt.Body.InnerHtml;
            }
        }

        void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            tt = m_wb.Document;            
        }
    }
}
