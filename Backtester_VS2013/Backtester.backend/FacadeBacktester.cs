using Backtester.backend.DataManager;
using Backtester.backend.interfaces;
using Backtester.backend.model;
using Backtester.backend.model.ativos;
using Backtester.backend.model.system;
using System;
using System.Collections.Generic;

namespace Backtester.backend
{
    public class FacadeBacktester
    {
        DataLoader dm;
        public DataHolder dh { get; set; }

        public FormulaManager formulaManager { get; set; }

        
        public FacadeBacktester()
        {
            dm = new DataLoader(this);
            dh = new DataHolder(this);
            formulaManager = new FormulaManager(this);
        }
        public void AddAtivo(Ativo ativo)
        {
            dh.AddAtivo(ativo);

        }

        public void CreateFormula(string formula)
        {
            formulaManager.CreateFormula(formula);
        }

        public Ativo GetAtivo(string name)
        {
            return dh.GetAtivo(name);

        }


        public void LoadAtivos(Config config,int loopNumber,Consts.PERIODO_ACAO periodo, Consts.TIPO_CARGA_ATIVOS tipoCarga)
        {
            dh.ClearAtivos();
            IList<string> list = config.GetLista(tipoCarga, loopNumber);

            foreach (string papel in list)
            {
                if (periodo == Consts.PERIODO_ACAO.DIARIO)
                {
                    LoadAtivoDiario(papel, tipoCarga);
                } else if (periodo == Consts.PERIODO_ACAO.SEMANAL)
                {
                    LoadAtivoSemanal(papel, tipoCarga);
                }
                else
                {
                    throw new Exception("Periodo não implementado:"+periodo);
                }
            }
        }

        private void LoadAtivoDiario(string papel, Consts.TIPO_CARGA_ATIVOS tipoCarga)
        {
            string suffix = tipoCarga.ToString();
            LoadAtivo(papel.ToUpper(), 100, Consts.PERIODO_ACAO.DIARIO, "dados/" + papel.ToLower() + "-diario"+ suffix + ".js", tipoCarga);
        }
        private void LoadAtivoSemanal(string papel, Consts.TIPO_CARGA_ATIVOS tipoCarga)
        {
            string suffix = tipoCarga.ToString();
            LoadAtivo(papel.ToUpper(), 100, Consts.PERIODO_ACAO.SEMANAL, "dados/" + papel.ToLower() + "-semanal"+ suffix + ".js", tipoCarga);
        }

        public void LoadAtivo(string papel, int loteMin, Consts.PERIODO_ACAO periodo, string fileName, Consts.TIPO_CARGA_ATIVOS tipoCarga= backend.Consts.TIPO_CARGA_ATIVOS.GERA_CANDIDATOS)
        {
            Ativo ativo = dh.GetOrCreateAtivo(papel, loteMin);
            if (ativo.candles.Count > 0)
            {
                return;
            }
            if (!dm.LoadAtivo(ativo, periodo, fileName, tipoCarga))
            {
                dh.RemoveAtivo(ativo);
            }
        }

        internal Periodo GetPeriodo(string data)
        {
            return dh.GetPeriodo(data);
        }

        internal Periodo GetPeriodo(DateTime data, Consts.PERIODO_ACAO tipoPeriodo=Consts.PERIODO_ACAO.DIARIO)
        {
            if (tipoPeriodo == Consts.PERIODO_ACAO.DIARIO)
            {
                return dh.GetPeriodo(data.ToShortDateString());
            } else if (tipoPeriodo == Consts.PERIODO_ACAO.SEMANAL){
                int v = (int)data.DayOfWeek;
                data = data.AddDays(-v);
                return dh.GetPeriodo(data.ToShortDateString());
            } else {
                throw new Exception("Periodo não implementado:"+tipoPeriodo);
            }
        }

        BackTester backTester;

        public Carteira Run(ICaller caller,model.system.Config config, model.system.TradeSystem ts, string name = "??")
        {
            backTester = new BackTester(this, dh.periodos[0],config,ts);
            backTester.runBackTest(caller,name);
            return backTester.carteira;
        }

        public Carteira RunValidation(ICaller caller, model.system.Config config, model.system.TradeSystem ts, string name = "??")
        {
            backTester = new BackTester(this, dh.periodos[0], config, ts);
            return backTester.runSingleBackTest(caller, new MonteCarlo(name));
        }

        

        public Carteira RunSingle(string name,ICaller caller, model.system.Config config, model.system.TradeSystem ts)
        {
            backTester = new BackTester(this, dh.periodos[0], config, ts);
            return backTester.runMonteCarlo(caller, name);
        }

        public void ClearAtivos()
        {
            dh.ClearAtivos();
        }

        public void ClearFormulas()
        {
            formulaManager.ClearFormulas();
        }

        public void ClearData()
        {
            dh.ClearData();
        }

        public Ativo GenerateAtivoMock(int seed)
        {
            return dh.GenerateAtivoMock(seed);
         
        }
    }
}
