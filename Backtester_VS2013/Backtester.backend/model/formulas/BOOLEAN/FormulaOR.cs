
using Backtester.backend.model.ativos;
namespace Backtester.backend.model.formulas
{
    public class FormulaOR : FormulaLogical
    {

        public FormulaOR(FacadeBacktester facade, string name, Formula campo1, Formula campo2)
            : base(facade, name,campo1,campo2)
        {
        }

        public override float Calc(Candle candle)
        {
            Candle c = candle;

            return (c.GetValor(campo1) > 0 || c.GetValor(campo2) > 0) ? 1 : 0;
        }


    }
}
