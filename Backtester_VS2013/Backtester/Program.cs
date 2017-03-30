using Backtester.backend;
using System;
using System.Windows.Forms;

namespace Backtester
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmPrincipal());
            Util.Info("Log em Program");
        }
    }
}
