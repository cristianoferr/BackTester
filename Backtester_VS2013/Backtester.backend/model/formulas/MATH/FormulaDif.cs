
using Backtester.backend.model.ativos;
using System;

namespace Backtester.backend.model.formulas
{
    public class FormulaDif : FormulaSUM
    {

        public FormulaDif(FacadeBacktester facade, string name, Formula campo1, Formula campo2)
            : base(facade, name,campo1,campo2)
        {
        }
        public FormulaDif(FacadeBacktester facade, string name, Formula campo1, float v2)
            : base(facade, name,campo1,v2)
        {
        }

        public override float Calc(Candle candle)
        {
            Candle c = candle;
            float val1 = c.GetValor(campo1);
            float val2 = (campo2 == null) ? v2 : c.GetValor(campo2);
            return val1 - val2;
        }


    }
}
