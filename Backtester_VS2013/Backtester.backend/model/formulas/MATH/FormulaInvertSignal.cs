
using Backtester.backend.model.ativos;
using System;

namespace Backtester.backend.model.formulas
{
    public class FormulaInvertSignal : FormulaSUM
    {

        public FormulaInvertSignal(FacadeBacktester facade, string name, Formula campo1, Formula campo2)
            : base(facade, name,campo1,campo2)
        {
        }
        public FormulaInvertSignal(FacadeBacktester facade, string name, Formula campo1, float v2)
            : base(facade, name,campo1,v2)
        {
        }

        public override float Calc(Candle candle)
        {
            float val = base.Calc(candle);
            return -val;
        }


    }
}
