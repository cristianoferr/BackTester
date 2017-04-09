using Backtester.backend.DataManager;
using Backtester.backend.interfaces;
using Backtester.backend.model;
using Backtester.backend.model.ativos;

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

        public void LoadAtivos(System.Collections.Generic.IList<string> list)
        {
            foreach (string papel in list)
            {
                LoadAtivoDiario(papel);
            }
        }

        private void LoadAtivoDiario(string papel)
        {
            LoadAtivo(papel.ToUpper(), 100, Consts.PERIODO_ACAO.DIARIO, "dados/" + papel.ToLower() + "-diario.js");
        }

        public void LoadAtivo(string papel, int loteMin, Consts.PERIODO_ACAO periodo, string fileName)
        {
            Ativo ativo = dh.GetOrCreateAtivo(papel, loteMin);
            if (ativo.candles.Count > 0)
            {
                return;
            }
            if (!dm.LoadAtivo(ativo, periodo, fileName))
            {
                dh.RemoveAtivo(ativo);
            }
        }

        internal Periodo GetPeriodo(string data)
        {
            return dh.GetPeriodo(data);
        }

        BackTester backTester;

        public void Run(ICaller caller,model.system.Config config, model.system.TradeSystem ts)
        {

            backTester = new BackTester(this, dh.periodos[0],config,ts);
            backTester.runBackTest(caller);
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
    }
}
