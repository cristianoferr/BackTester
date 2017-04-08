
using Backtester.backend.model.ativos;
namespace Backtester.backend.model.formulas
{
    public class FormulaGREATER : FormulaLogical
    {

        public FormulaGREATER(FacadeBacktester facade, string name, Formula campo1, Formula campo2)
            : base(facade, name,campo1,campo2)
        {
        }

      


        public override float Calc(Candle candle)
        {
            Candle c = candle;
            float v1 = c.GetValor(campo1);
            float v2 = c.GetValor(campo2);
            return v1 > v2?1:0;
        }


    }
}
