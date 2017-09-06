
using Backtester.backend.model.ativos;
using System;
namespace Backtester.backend.model.formulas
{
    public class FormulaREF : Formula
    {
        Formula per;
        Formula campo;

        public FormulaREF(FacadeBacktester facade, string name, Formula campo, Formula per)
            : base(facade, name)
        {
            this.per = per;
            this.campo = campo;
            gravar = false;
        }



        public override string GetCode()
        {
            return name + "(" + campo.GetCode() + "," + per.GetCode() + ")";
        }




        public override float Calc(Candle candle)
        {
            Candle c = candle;

            float vPer = per.Calc(candle);
            vPer = LimitPeriodo(vPer);
            for (int i = 0; i < vPer; i++)
            {
                if (c.candleAnterior!=null)
                    c = c.candleAnterior;
            }
            return c.GetValor(campo);
        }
    }
}
