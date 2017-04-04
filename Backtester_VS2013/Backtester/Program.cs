using System;
using System.Windows.Forms;
using UsoComum;

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
            Utils.Info("Inicializando...");
            Application.Run(new FrmPrincipal());
        }
    }
}
