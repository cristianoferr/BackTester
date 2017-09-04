using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backtester.controller;
using Backtester.backend.DataManager;
using Backtester.interfaces;

namespace FrontTester.frontend.controller
{
    public class FrontController
    {
        private IReferView frmFront;
        private ConfigController configController;
        CandidatoManager candidatoManager;


        public FrontController(IReferView frmFront, ConfigController configController)
        {
            this.frmFront = frmFront;
            this.configController = configController;
            LoadCandidatos();

        }

        private void LoadCandidatos()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("FrontTester.frontend", "Backtester");
            candidatoManager = CandidatoManager.LoadSaved(path);
            for (int i = 0; i < candidatoManager.ranking.Count; i++)
            {
                CandidatoData data = candidatoManager.ranking[i];
                frmFront.AddItem("cbTradeSystem", data);
            }
        }
    }
}
