
using Backtester.backend.model.ativos;
namespace Backtester.backend.model.formulas
{
    public class FormulaHV : Formula
    {
        int per;
        Formula campo;

        public FormulaHV(FacadeBacktester facade, string name, Formula campo, int per)
            : base(facade, name)
        {
            this.per = per;
            this.campo = campo;
        }



        public override string GetCode()
        {
            return name + "(" + campo.GetCode() + "," + per + ")";
        }




        public override float Calc(Candle candle)
        {
            Candle c = candle;

            float v = candle.GetValor(campo);
            Candle cp = c;
            for (int i = 0; i < per; i++)
            {
                float x = cp.GetValor(campo);
                if (x > v) v = x;
                cp = cp.candleAnterior;
            }

            return (v);
        }
    }
}
