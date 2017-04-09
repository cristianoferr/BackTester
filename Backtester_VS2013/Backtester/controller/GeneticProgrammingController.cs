using Backtester.backend.interfaces;
using Backtester.backend.model;
using Backtester.backend.model.system;
using Backtester.GeneticProgramming;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backtester.controller
{

    public class GeneticProgrammingController:IController,ICaller
    {
        private FrmPrincipal frmPrincipal;
        private ConfigController configController;
        static BTGPRunner runner;


        public GeneticProgrammingController(FrmPrincipal frmPrincipal, ConfigController configController)
        {
            this.frmPrincipal = frmPrincipal;
            this.configController = configController;
            runner = new BTGPRunner(ConfigController.config,this);
            runner.Init();
        }
        public void UpdateValuesFromUI()
        {

        }
        public void UpdateUI()
        {

        }

        public Carteira RunBackTester(TradeSystem ts,string name)
        {
            return configController.facade.RunSingle(name,this, ConfigController.config, ts);
        }

        int loops = 0;

        internal void Run()
        {
            configController.facade.LoadAtivos(ConfigController.config.papeis);
            loops = 0;
            while (true)
            {
                loops++;
                StartSingleRun();
            }
        }

        internal void RunSingle()
        {
            configController.facade.LoadAtivos(ConfigController.config.papeis);
            configController.facade.LoadAtivos(ConfigController.config.papeis);
            StartSingleRun();
        }

        private void StartSingleRun()
        {
            GC.Collect();
            configController.facade.ClearData();
            configController.facade.ClearFormulas();
            
            frmPrincipal.dataGridRuns.Rows.Clear();
            Thread t = new Thread(staticSingleRun);
            t.Start();
            int runs = 0;
            while (t.IsAlive)
            {
                Thread.Sleep(100);
                Application.DoEvents();
                if (updatesToAdd.Count > 0)
                {
                    int count = updatesToAdd.Count;
                    for (int i = 0; i < count; i++)
                    {
                        UpdatesToAdd updt = updatesToAdd[i];
                        runs++;
                        configController.UpdateApplication(updt.carteira, updt.mc, runs,loops);
                    }
                    updatesToAdd.Clear();
                }
            }
        }

        static void staticSingleRun()
        {
            runner.SingleRun();
            
        }

         public void SimpleUpdate(){
             configController.SimpleUpdate();
         }

         List<UpdatesToAdd> updatesToAdd = new List<UpdatesToAdd>();
         public void UpdateApplication(Carteira carteira, MonteCarlo mc, int countLoops, int totalLoops)
         {
             UpdatesToAdd updt = new UpdatesToAdd();
             updt.carteira = carteira;
             updt.mc = mc;
             updt.countLoops=countLoops;
             updt.totalLoops = totalLoops;
             updatesToAdd.Add(updt);
             
         }


         
    }

    class UpdatesToAdd
    {

        public Carteira carteira { get; set; }

        public MonteCarlo mc { get; set; }

        public int countLoops { get; set; }

        public int totalLoops { get; set; }
    }
}
