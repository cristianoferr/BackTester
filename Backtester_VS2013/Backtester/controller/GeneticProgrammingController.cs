using Backtester.backend.DataManager;
using Backtester.backend.interfaces;
using Backtester.backend.model;
using Backtester.backend.model.system;
using Backtester.GeneticProgramming;
using Backtester.interfaces;
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
        private IReferView frmPrincipal;
        private ConfigController configController;
        public BTGPRunner gpRunner;

        static GeneticProgrammingController singleton; 

        public GeneticProgrammingController(IReferView frmPrincipal, ConfigController configController)
        {
            singleton = this;
            this.frmPrincipal = frmPrincipal;
            this.configController = configController;
            gpRunner = new BTGPRunner(configController.config, this);
        }

        public void InitPool()
        {
            gpRunner.InitPool();
        }
        public void UpdateValuesFromUI()
        {

        }
        public void UpdateUI()
        {

        }

        public Carteira RunBackTester(TradeSystem ts, string name)
        {
            return configController.facade.RunSingle(name, this, configController.config, ts);
        }


        internal void Run()
        {
            frmPrincipal.SetTitle("Gerando Candidatos...");
            configController.facade.LoadAtivos(configController.config.papeis, configController.config.tipoPeriodo,false);
            while (true)
            {
                StartSingleRun();
            }
        }

        internal void RunSingle()
        {
            configController.facade.LoadAtivos(configController.config.papeis,configController.config.tipoPeriodo, false);
            StartSingleRun();
        }

        private void StartSingleRun()
        {
            GC.Collect();
            configController.facade.ClearData();
            configController.facade.ClearFormulas();

            frmPrincipal.ClearRows("dataGridRuns");
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
            singleton.gpRunner.SingleRun();
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




        internal void ValidaCandidatos(bool deleteFile=false)
        {

            while (true)
            {
                Thread.Sleep(1000);
                Application.DoEvents();
                string file = Utils.GetFirstFile(GPConsts.DIRECTORY_TO_CHECK + configController.config.tipoPeriodo.ToString());
                if (file != null)
                {
                    GPSolutionProxy solution = GPSolutionProxy.LoadFromFile(file);
                    if (solution.solution != null)
                    {
                        frmPrincipal.SetTitle("Validando: " + solution.solution.name);
                        ValidaCandidato(solution);
                    }
                    if (deleteFile) Utils.DeleteFile(file);
                } else
                {
                    frmPrincipal.SetTitle("NENHUM CANDIDATO A VALIDAR: "+ configController.config.tipoPeriodo.ToString());
                }
            }

        }

        public GPSolutionProxy solutionToTest;
        private void ValidaCandidato(GPSolutionProxy proxy)
        {
            solutionToTest = proxy;
            //frmPrincipal.SetStatus("Validando solution " + proxy.solution.name);
            GC.Collect();
            configController.facade.ClearData();
            configController.facade.ClearFormulas();

            frmPrincipal.ClearRows("dataGridRuns");
            Thread t = new Thread(staticSingleRunValidaSolution);
            t.Name = "BacktestRunner";
            t.Start();
            int runs = 0;
            frmPrincipal.ClearRows("dataGridRuns");
            while (t.IsAlive)
            {
                Thread.Sleep(100);
                runs = UpdateThreadTick(runs);
            }
          
        }

        public static void staticSingleRunValidaSolution()
        {
            singleton.SingleRunValidaSolution();
        }


        public  Carteira SingleRunValidaSolution(string name="Single Run Validate")
        {
            //gpRunner.SingleRun();
            TradeSystem ts=solutionToTest.tradeSystem;
            Carteira carteira=configController.RunSingle(ts,name,true);
            CandidatoManager cm = CandidatoManager.LoadSaved();
            cm.AddTradeSystem(ts,carteira.estatistica);
            return carteira;
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
