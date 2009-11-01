using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// Model para a entidade Autor.
    /// </summary>
    public class AutorInfo
    {
        /// <summary>
        /// Nome do autor.
        /// </summary>
        private string m_strNome;

        /// <summary>
        /// Nome do autor em citações bibliográficas.
        /// </summary>
        private string m_strNomeCitacoes;

        /// <summary>
        /// Lista de publicações.
        /// </summary>
        private List<PublicacaoInfo> m_lstPublicacoes;

        /// <summary>
        /// Chave de acesso no capes.
        /// </summary>
        private string m_strKey;

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public AutorInfo()
        {
            m_lstPublicacoes = new List<PublicacaoInfo>();
        }

        /// <summary>
        /// Chave de acesso no capes.
        /// </summary>
        public string Key
        {
            get { return m_strKey; }
            set { m_strKey = value; }
        }

        /// <summary>
        /// Link para curriculo.
        /// </summary>
        public string Link
        {
            get
            {
                if (Key != null)
                    return "http://buscatextual.cnpq.br/buscatextual/visualizacv.jsp?id=" + m_strKey;
                else
                    return null;
            }
        }

        /// <summary>
        /// Nome do autor.
        /// </summary>
        public string Nome
        {
            get { return m_strNome; }
            set { m_strNome = value; }
        }

        /// <summary>
        /// Nome do autor em citações bibliográficas.
        /// </summary>
        public string NomeCitacoes
        {
            get { return m_strNomeCitacoes; }
            set { m_strNomeCitacoes = value; }
        }

        /// <summary>
        /// Lista de publicações.
        /// </summary>
        public List<PublicacaoInfo> Publicacoes
        {
            get { return m_lstPublicacoes; }
            set { m_lstPublicacoes = value; }
        }

        /// <summary>
        /// Calculo do h-index do autor.
        /// </summary>
        /// <remarks>Calculado com base na lista de publicações da model.</remarks>
        /// <returns>H-index para o autor.</returns>
        public int Hindex()
        {
            List<int> lstCitacoes = new List<int>();

            foreach (PublicacaoInfo udtPubTemp in Publicacoes)
                lstCitacoes.Add(udtPubTemp.Citacoes);

            lstCitacoes.Sort();
            lstCitacoes.Reverse();

            int intHindex = 0;

            while ((intHindex < lstCitacoes.Count) && (intHindex < lstCitacoes[intHindex]))
                intHindex++;

            return intHindex;
        }

        /// <summary>
        /// Total de citações
        /// </summary>
        /// <returns></returns>
        public int TotalCitacoes()
        {
            int intCount = 0;
            foreach (PublicacaoInfo udtPubTemp in Publicacoes)
                intCount += udtPubTemp.Citacoes;
            
            return intCount;
        }

        /// <summary>
        /// Total de citações do hindex
        /// </summary>
        /// <returns></returns>
        public int TotalCitacoesHindex()
        {
            List<int> lstCitacoes = new List<int>();

            foreach (PublicacaoInfo udtPubTemp in Publicacoes)
                lstCitacoes.Add(udtPubTemp.Citacoes);

            lstCitacoes.Sort();
            lstCitacoes.Reverse();

            int intCount = 0;
            int intHindex = Hindex();
            for (int i = 0; i < lstCitacoes.Count && i < intHindex; i++)
                intCount += lstCitacoes[i];

            return intCount;
        }

        /// <summary>
        /// Total de citações do gindex
        /// </summary>
        /// <returns></returns>
        public int TotalCitacoesGindex()
        {
            List<int> lstCitacoes = new List<int>();

            foreach (PublicacaoInfo udtPubTemp in Publicacoes)
                lstCitacoes.Add(udtPubTemp.Citacoes);

            lstCitacoes.Sort();
            lstCitacoes.Reverse();

            int intCount = 0;
            int intGindex = Gindex();
            for (int i = 0; i < lstCitacoes.Count && i < intGindex; i++)
                intCount += lstCitacoes[i];
            
            return intCount;
        }

        /// <summary>
        /// Calculo do g-index do autor.
        /// </summary>
        /// <returns></returns>
        public int Gindex()
        {
            List<int> lstCitacoes = new List<int>();

            foreach (PublicacaoInfo udtPubTemp in Publicacoes)
                lstCitacoes.Add(udtPubTemp.Citacoes);

            lstCitacoes.Sort();
            lstCitacoes.Reverse();

            return Gindex(lstCitacoes, 1);
        }        

        private int Gindex(List<int> _lstCitacoes, int _intGindex)
        {
            if (_intGindex < _lstCitacoes.Count)
            {
                int intCount = 0;

                for (int i = 0; i < _intGindex; i++)                
                    intCount += _lstCitacoes[i];

                if (intCount >= (_intGindex * _intGindex))
                    return Gindex(_lstCitacoes, ++_intGindex);
                else
                    return --_intGindex;
            }
            else
                return --_intGindex;
        }

    }
}
