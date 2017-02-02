using Backtester.backend.DataManager;
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

        public void LoadAtivos()
        {
            LoadAtivoDiario("abev3");
            LoadAtivoDiario("bbas3");
            LoadAtivoDiario("bbdc4");
            LoadAtivoDiario("bvmf3");
            LoadAtivoDiario("ccro3");
            LoadAtivoDiario("ciel3");
            LoadAtivoDiario("ggbr4");
            LoadAtivoDiario("natu3");
            LoadAtivoDiario("petr4");
            LoadAtivoDiario("vale5");
        }

        private void LoadAtivoDiario(string papel)
        {
            LoadAtivo(papel.ToUpper(), 100, Consts.PERIODO_ACAO.DIARIO, "dados/" + papel.ToLower() + "-diario.js");
        }

        public void LoadAtivo(string papel, int loteMin, Consts.PERIODO_ACAO periodo, string fileName)
        {
            Ativo ativo = dh.GetOrCreateAtivo(papel, loteMin);
            dm.LoadAtivo(ativo, periodo, fileName);
        }



        internal Periodo GetPeriodo(string data)
        {
            return dh.GetPeriodo(data);
        }



    }
}
