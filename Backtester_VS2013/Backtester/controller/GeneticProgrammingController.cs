using Backtester.backend.interfaces;
using Backtester.backend.model;
using Backtester.backend.model.system;
using Backtester.GeneticProgramming;
using GeneticProgramming;
using GeneticProgramming.solution;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using UsoComum;

namespace Backtester.controller
{

    public class GeneticProgrammingController : IController, ICaller
    {
        private FrmPrincipal frmPrincipal;
        static ConfigController configController;
        static BTGPRunner gpRunner;

        public GeneticProgrammingController(FrmPrincipal frmPrincipal, ConfigController configController)
        {
            this.frmPrincipal = frmPrincipal;
            GeneticProgrammingController.configController = configController;
            gpRunner = new BTGPRunner(ConfigController.config, this);
            gpRunner.Init();
        }
        public void UpdateValuesFromUI()
        {

        }
        public void UpdateUI()
        {

        }

        public Carteira RunBackTester(TradeSystem ts, string name)
        {
            return configController.facade.RunSingle(name, this, ConfigController.config, ts);
        }


        internal void Run()
        {
            configController.facade.LoadAtivos(ConfigController.config.papeis, ConfigController.config.tipoPeriodo);
            while (true)
            {
                StartSingleRun();
            }
        }

        internal void RunSingle()
        {
            configController.facade.LoadAtivos(ConfigController.config.papeis,ConfigController.config.tipoPeriodo);
            configController.facade.LoadAtivos(ConfigController.config.papeis, ConfigController.config.tipoPeriodo);
            StartSingleRun();
        }

        private void StartSingleRun()
        {
            GC.Collect();
            configController.facade.ClearData();
            configController.facade.ClearFormulas();

            frmPrincipal.dataGridRuns.Rows.Clear();
            Thread t = new Thread(staticSingleRunGP);
            t.Name = "BacktestRunner";
            t.Start();
            int runs = 0;
            while (t.IsAlive)
            {
                Thread.Sleep(100);
                runs = UpdateThreadTick(runs);
            }
        }

        private int UpdateThreadTick(int runs)
        {
            Application.DoEvents();
            if (updatesToAdd.Count > 0)
            {
                int count = updatesToAdd.Count;
                for (int i = 0; i < count; i++)
                {
                    UpdatesToAdd updt = updatesToAdd[i];
                    runs++;
                    configController.UpdateApplication(updt.carteira, updt.mc, runs, updt.totalLoops);
                }
                updatesToAdd.Clear();
            }
            return runs;
        }

        static void staticSingleRunGP()
        {
            gpRunner.SingleRun();
        }

       

        public void SimpleUpdate()
        {
            configController.SimpleUpdate();
        }

        List<UpdatesToAdd> updatesToAdd = new List<UpdatesToAdd>();
        public void UpdateApplication(Carteira carteira, MonteCarlo mc, int countLoops, int totalLoops)
        {
            UpdatesToAdd updt = new UpdatesToAdd();
            updt.carteira = carteira;
            updt.mc = mc;
            updt.countLoops = countLoops;
            updt.totalLoops = gpRunner.pool.iterationNumber;
            updatesToAdd.Add(updt);

        }




        internal void ValidaCandidatos()
        {

            while (true)
            {
                Thread.Sleep(100);
                Application.DoEvents();
                string file = Utils.GetFirstFile(GPConsts.DIRECTORY_TO_CHECK + ConfigController.config.tipoPeriodo.ToString());
                if (file != null)
                {
                    GPSolutionProxy solution = GPSolutionProxy.LoadFromFile(file);
                    ValidaCandidato(solution);
                }
            }
            
        }

        static GPSolutionProxy solutionToTest;
        private void ValidaCandidato(GPSolutionProxy proxy)
        {
            solutionToTest = proxy;
            frmPrincipal.txtStatus.Text = "Validando solution " + proxy.solution.name;
            GC.Collect();
            configController.facade.ClearData();
            configController.facade.ClearFormulas();

            frmPrincipal.dataGridRuns.Rows.Clear();
            Thread t = new Thread(staticSingleRunValidaSolution);
            t.Name = "BacktestRunner";
            t.Start();
            int runs = 0;
            while (t.IsAlive)
            {
                Thread.Sleep(100);
                runs = UpdateThreadTick(runs);
            }
          //  Utils.DeleteFile(file);
        }

        static void staticSingleRunValidaSolution()
        {
            //gpRunner.SingleRun();
            TradeSystem ts=solutionToTest.tradeSystem;
            configController.Run(ts);
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
