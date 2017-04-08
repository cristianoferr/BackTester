
using Backtester.backend.DataManager;
using Backtester.backend.model.ativos;
namespace Backtester.backend.model.formulas
{
    public class FormulaStoch : Formula
    {
        Formula per, smooth;

        public FormulaStoch(FacadeBacktester facade, string name, Formula per, Formula smooth)
            : base(facade, name)
        {
            this.per = per;
            this.smooth = smooth;
        }



        public override string GetCode()
        {
            return name + "(" + per.ToString() + "," + smooth.ToString() + ")";
        }


        public override float Calc(Candle candle)
        {
            float k = 0;
            float max=smooth.Calc(candle);
            for (int i = 0; i < max; i++)
            {
                float ll = candle.GetValor(facade.formulaManager.GetFormula(FormulaManager.LV, "L," + per));
                float hh = candle.GetValor(facade.formulaManager.GetFormula(FormulaManager.HV, "H," + per));
                float close = candle.GetValor("C");
                k += (close - ll) / (hh - ll) * 100;
                candle = candle.candleAnterior;
                if (candle == null) return 0;
            }
            k /= smooth.Calc(candle);

            return k;
        }
    }
}
