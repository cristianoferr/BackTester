
using Backtester.backend.model.ativos;
using System;
namespace Backtester.backend.model.formulas
{
    public class FormulaAvgLoss : Formula
    {
        int per;
        Formula campo;

        public FormulaAvgLoss(FacadeBacktester facade, string name, Formula campo, int per)
            : base(facade, name)
        {
            this.per = per;
            this.campo = campo;
        }



        public override string GetCode()
        {
            return name + "(" + campo.GetCode() + "," + per + ")";
        }


        public float GetFirstRSI(Candle candle)
        {
            Candle cp = candle;
            float ag = 0;
            for (int i = 0; i < per; i++)
            {
                float dif = cp.GetValor(campo) - cp.candleAnterior.GetValor(campo);
                if (dif < 0) ag += dif;
                if (cp.candleAnterior == null) return 0;
                cp = cp.candleAnterior;

            }
            float rs = 0;

            //1o candle após "per" periodos
            if (cp.candleAnterior == null)
            {
                ag = ag / per;
                rs = ag;
            }
            else
            {
                ag = candle.GetValor(campo) - candle.candleAnterior.GetValor(campo);
                if (ag > 0) ag = 0;
                ag = Math.Abs(ag);
                rs = candle.candleAnterior.GetValor(facade.formulaManager.GetFormula(GetCode())) * (per - 1) + ag;
                rs = rs / per;
            }


            return rs;
        }


        public override float Calc(Candle candle)
        {
            float r = GetFirstRSI(candle);

            return r;
        }
    }
}
