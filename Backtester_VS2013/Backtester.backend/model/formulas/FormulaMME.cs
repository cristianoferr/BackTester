using Backtester.backend.model.ativos;
using System;

namespace Backtester.backend.model.formulas
{
    public class FormulaMME : FormulaMMS
    {
  

        public FormulaMME(FacadeBacktester facade, String name, Formula campo, Formula per)
            : base(facade, name, campo, per)
        {
        }

        public override float Calc(Candle candle)
        {
            Candle c = candle;
            float vPer = per.Calc(candle);
            vPer = LimitPeriodo(vPer);
            if (vPer <= 0) return 0;
            float K = 2f / (vPer + 1f);
            Candle cp = c.candleAnterior;
            if (cp == null) return 0;
            float P = 0;
            
            float prevMM = cp==c?0:cp.GetValor(GetCode());
            P = prevMM; 
            float C = c.GetValor(campo);

            float EMA = (K * (C - P)) + P;
            //System.out.println("tot:"+tot);
            return EMA;
        }
    }
}
