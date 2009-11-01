using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EventArgs
{
    public class DenyAccessArg : System.EventArgs
    {
        private bool m_blnStop = false;

        public bool Stop
        {
            get { return m_blnStop; }
            set { m_blnStop = value; }
        }
    }
}
