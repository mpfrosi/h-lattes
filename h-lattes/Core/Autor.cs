using System;
using System.Collections.Generic;
using System.Text;
using Model;
using Model.Filtros;
using CurriculoParser;
using System.IO;
using log4net;

namespace Core
{
    /// <summary>
    /// Classe que encapsula a busca de autores.
    /// </summary>
    public class Autor
    {
        LattesParser m_udtLattesParser = null;
        ILog m_loger;
       
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// Construtor padão.
        /// </summary>
        public Autor()
        {            
            m_udtLattesParser = new LattesParser();
            m_loger = LogManager.GetLogger(GetType());
        }

        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// Busca a imagem de CAPTCHA para busca de autores.
        /// </summary>
        /// <returns>Stream que contêm a imagem.</returns>
        public Stream GetSearchImage()
        {
            try
            {
                return m_udtLattesParser.GetImage();
            }
            catch(Exception ex)
            {
                m_loger.Error("Erro na obtenção da imagem.", ex);
                throw;
            }
        }

        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// Busca autores com base em um filtro.
        /// </summary>
        /// <param name="_udtFiltro">Filtro para busca.</param>
        /// <returns>Lista de autores emcontrados.</returns>
        public List<AutorInfo> BuscaAutores(FiltroBuscaAutor _udtFiltro)
        {
            try
            {
                return this.m_udtLattesParser.RealizaBusca(_udtFiltro);
            }
            catch (Exception ex)
            {
                m_loger.Error("Erro na busca de autores.", ex);
                throw;
            }
        }

        //-------------------------------------------------------------------------------------------------
    }
}
