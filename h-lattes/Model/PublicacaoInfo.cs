using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// Model para a entidade publica��o.
    /// </summary>
    public class PublicacaoInfo
    {
        /// <summary>
        /// T�tulo da publica��o.
        /// </summary>
        private string m_strTitulo;

        /// <summary>
        /// Ve�culo onde o artigo foi publicado.
        /// </summary>
        private string m_lstVeiculo;

        /// <summary>
        /// Autores da publica��o.
        /// </summary>
        private string m_strAutores;

        /// <summary>
        /// N�mero de cita��es do artigo.
        /// </summary>
        private int m_intCitacoes;

        /// <summary>
        /// Tipo de publica��o.
        /// </summary>
        private ETipoPublicacao m_enmTipoPublicacao;

        /// <summary>
        /// Tipo de publica��o.
        /// </summary>
        public ETipoPublicacao TipoPublicacao
        {
            get { return m_enmTipoPublicacao; }
            set { m_enmTipoPublicacao = value; }
        }
        
        /// <summary>
        /// T�tulo da publica��o.
        /// </summary>
        public string Titulo
        {
            get { return m_strTitulo; }
            set { m_strTitulo = value; }
        }

        /// <summary>
        /// Ve�culo onde o artigo foi publicado.
        /// </summary>
        public string Veiculo
        {
            get { return m_lstVeiculo; }
            set { m_lstVeiculo = value; }
        }

        /// <summary>
        /// Autores da publica��o.
        /// </summary>
        public string Autores
        {
            get { return m_strAutores; }
            set { m_strAutores = value; }
        }

        /// <summary>
        /// N�mero de cita��es do artigo.
        /// </summary>
        public int Citacoes
        {
            get { return m_intCitacoes; }
            set { m_intCitacoes = value; }
        }
    }
}
