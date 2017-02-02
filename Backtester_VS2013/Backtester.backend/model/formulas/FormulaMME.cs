using Backtester.backend.model.ativos;
using System;

namespace Backtester.backend.model.formulas
{
    public class FormulaMME : FormulaMMS
    {
        public FormulaMME(FacadeBacktester facade, String name, String campo, int per)
            : base(facade, name, campo, per)
        {
        }

        public FormulaMME(FacadeBacktester facade, String name, Formula campo, int per)
            : base(facade, name, campo, per)
        {
        }

        public override float Calc(Candle candle)
        {
            Candle c = candle;
            float K = 2f / (per + 1f);
            Candle cp = c.candleAnterior;
            float P = 0;
            float prevMM = cp.GetValor(GetCode());
            if (prevMM == 0) { P = cp.GetValor(campo); } else { P = prevMM; }
            float C = c.GetValor(campo);

            float EMA = (K * (C - P)) + P;
            //System.out.println("tot:"+tot);
            return EMA;
        }
    }
}
