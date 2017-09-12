
using Backtester.backend.model.ativos;
namespace Backtester.backend.model.formulas
{
    public class FormulaHV : Formula
    {
        Formula per;
        Formula campo;

        public FormulaHV(FacadeBacktester facade, string name, Formula campo, Formula per)
            : base(facade, name)
        {
            this.per = per;
            this.campo = campo;
        }



        public override string GetCode()
        {
            return name + "(" + campo.GetCode() + "," + per.GetCode() + ")";
        }




        public override float Calc(Candle candle)
        {
            Candle c = candle;

            float v = candle.GetValor(campo);
            Candle cp = c;
            float vPer = per.Calc(candle);
            vPer = LimitPeriodo(vPer);
            for (int i = 0; i < vPer; i++)
            {
                float x = cp.GetValor(campo);
                if (x > v) v = x;
                cp = cp.candleAnterior;
                if (cp == null) return v;
            }

            return v;
        }
    }
}
