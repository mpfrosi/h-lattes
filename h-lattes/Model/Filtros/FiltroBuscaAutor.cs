using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Filtros
{
    public class FiltroBuscaAutor
    {
        private string m_strNome;
        private bool m_blnDoutor;
        private bool m_blnOutrosPesquisadores;
        private string m_strCaptcha;
        
        public string Nome
        {
            get { return m_strNome; }
            set { m_strNome = value; }
        }

        public bool Doutor
        {
            get { return m_blnDoutor; }
            set { m_blnDoutor = value; }
        }

        public bool OutrosPesquisadores
        {
            get { return m_blnOutrosPesquisadores; }
            set { m_blnOutrosPesquisadores = value; }
        }

        public string Captcha
        {
            get { return m_strCaptcha; }
            set { m_strCaptcha = value; }
        }
    }
}
