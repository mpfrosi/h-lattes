using System;
using System.Collections.Generic;
using System.Text;
using Model.Exceptions;

namespace Common
{
    public static class FlowHandler
    {
        private static bool m_blnCancel = false;
        
        /// <summary>
        /// Cancela execução.
        /// </summary>
        public static void Cancel()
        {
            m_blnCancel = true;
        }

        public static void VerifyCancel()
        {            
            if (m_blnCancel)
            {
                m_blnCancel = false;
                throw new CancelException();
            }
        }
    }
}
