using Backtester.backend.interfaces;
using Backtester.backend.model;
using Backtester.backend.model.system;
using Backtester.GeneticProgramming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backtester.controller
{
    public class GeneticProgrammingController:IController,ICaller
    {
        private FrmPrincipal frmPrincipal;
        private ConfigController configController;
        BTGPRunner runner;


        public GeneticProgrammingController(FrmPrincipal frmPrincipal, ConfigController configController)
        {
            this.frmPrincipal = frmPrincipal;
            this.configController = configController;
            runner = new BTGPRunner(ConfigController.config,this);
        }
        public void UpdateValuesFromUI()
        {

        }
        public void UpdateUI()
        {

        }

        public void RunBackTester(TradeSystem ts)
        {
            configController.facade.RunSingle(this, ConfigController.config, ts);
        }

        internal void Run()
        {
            configController.facade.LoadAtivos(ConfigController.config.papeis);
            frmPrincipal.dataGridRuns.Rows.Clear();
            runner.SingleRun();
        }

         public void SimpleUpdate(){
             configController.SimpleUpdate();
         }

         public void UpdateApplication(Carteira carteira, MonteCarlo mc, int countLoops, int totalLoops)
         {
             configController.UpdateApplication(carteira, mc, countLoops, totalLoops);
         }

    }
}
