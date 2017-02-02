
using Backtester.backend.model.ativos;
using System;
namespace Backtester.backend.model.formulas
{
    public class FormulaREF : Formula
    {
        int per;
        Formula campo;

        public FormulaREF(FacadeBacktester facade, string name, Formula campo, int per)
            : base(facade, name)
        {
            this.per = per;
            this.campo = campo;
            this.per = Math.Abs(per);
            gravar = false;
        }



        public override string GetCode()
        {
            return name + "(" + campo.GetCode() + "," + per + ")";
        }




        public override float Calc(Candle candle)
        {
            Candle c = candle;

            for (int i = 0; i < per; i++)
            {
                c = c.candleAnterior;
            }
            return c.GetValor(campo);
        }
    }
}
