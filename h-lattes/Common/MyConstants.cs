using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Configuration;

namespace Common
{
    /// <summary>
    /// 
    /// </summary>
    public class MyConstants
    {
        /// <summary>
        /// 
        /// </summary>
        public static CookieContainer MyCookies = new CookieContainer();
        
        /// <summary>
        /// 
        /// </summary>
        private static int? s_intScholarTimeout = null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static int ReadScholarTimeout()
        {
            try
            {
                s_intScholarTimeout = Convert.ToInt32(ConfigurationSettings.AppSettings["Scholar.Timeout"]);
            }
            catch
            {
                s_intScholarTimeout = 0;
            }

            return s_intScholarTimeout.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        public static int SchloarTimeout
        {
            get
            {
                if (s_intScholarTimeout == null)
                    ReadScholarTimeout();

                return s_intScholarTimeout.Value;
            }
            set
            { 
                s_intScholarTimeout = value; 
            }
        }
    }
}
