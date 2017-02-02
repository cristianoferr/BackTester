
using Backtester.backend.DataManager;
using Backtester.backend.model.ativos;
namespace Backtester.backend.model.formulas
{
    public class FormulaStoch : Formula
    {
        int per, d, smooth;

        public FormulaStoch(FacadeBacktester facade, string name, int per, int smooth)
            : base(facade, name)
        {
            this.per = per;
            this.smooth = smooth;
        }



        public override string GetCode()
        {
            return name + "(" + per + "," + smooth + ")";
        }


        /*
 * (non-Javadoc)
 * @see com.cristiano.backtester.formulas.Formula#calc(com.cristiano.backtester.ativos.Candle)
 * 
 * %K = (Current Close - Lowest Low)/(Highest High - Lowest Low) * 100
%D = 3-day SMA of %K

Lowest Low = lowest low for the look-back period
Highest High = highest high for the look-back period
%K is multiplied by 100 to move the decimal point two places
 */


        public override float Calc(Candle candle)
        {
            float k = 0;
            for (int i = 0; i < smooth; i++)
            {
                float ll = candle.GetValor(facade.formulaManager.GetFormula(FormulaManager.LV, "L," + per));
                float hh = candle.GetValor(facade.formulaManager.GetFormula(FormulaManager.HV, "H," + per));
                float close = candle.GetValor("C");
                k += (close - ll) / (hh - ll) * 100;
                candle = candle.candleAnterior;
            }
            k /= smooth;


            //	System.out.println("candle:"+candle.getPeriodo()+" rsi:"+rsi+" ag:"+ag+" al:"+al);
            return (k);
        }
    }
}
