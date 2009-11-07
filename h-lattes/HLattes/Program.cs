using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HLattes
{
    public delegate void SetIntValueHandler(int _intValue);
    public delegate void SetStringValueHandler(string _strValue);
    public delegate void SetUriValueHandler(Uri _uriAddress);
    public delegate void ExecuteHandler();

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            log4net.Config.XmlConfigurator.Configure();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}