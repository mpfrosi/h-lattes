using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// Model para a entidade publicação.
    /// </summary>
    public class PublicacaoInfo
    {
        /// <summary>
        /// Título da publicação.
        /// </summary>
        private string m_strTitulo;

        /// <summary>
        /// Veículo onde o artigo foi publicado.
        /// </summary>
        private string m_lstVeiculo;

        /// <summary>
        /// Autores da publicação.
        /// </summary>
        private string m_strAutores;

        /// <summary>
        /// Número de citações do artigo.
        /// </summary>
        private int m_intCitacoes;

        /// <summary>
        /// Tipo de publicação.
        /// </summary>
        private ETipoPublicacao m_enmTipoPublicacao;

        /// <summary>
        /// Tipo de publicação.
        /// </summary>
        public ETipoPublicacao TipoPublicacao
        {
            get { return m_enmTipoPublicacao; }
            set { m_enmTipoPublicacao = value; }
        }
        
        /// <summary>
        /// Título da publicação.
        /// </summary>
        public string Titulo
        {
            get { return m_strTitulo; }
            set { m_strTitulo = value; }
        }

        /// <summary>
        /// Veículo onde o artigo foi publicado.
        /// </summary>
        public string Veiculo
        {
            get { return m_lstVeiculo; }
            set { m_lstVeiculo = value; }
        }

        /// <summary>
        /// Autores da publicação.
        /// </summary>
        public string Autores
        {
            get { return m_strAutores; }
            set { m_strAutores = value; }
        }

        /// <summary>
        /// Número de citações do artigo.
        /// </summary>
        public int Citacoes
        {
            get { return m_intCitacoes; }
            set { m_intCitacoes = value; }
        }
    }
}
