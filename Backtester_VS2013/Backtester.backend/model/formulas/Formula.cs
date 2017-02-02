
using Backtester.backend.model.ativos;
namespace Backtester.backend.model.formulas
{
    public class Formula
    {
        public bool gravar { get; set; }
        public Formula(FacadeBacktester facade, string name)
        {
            this.name = name;
            this.facade = facade;
            gravar = true;
        }

        public FacadeBacktester facade { get; set; }

        public string name { get; set; }
        public string ToString(float valor)
        {
            return GetCode() + "=" + Util.FormatCurrency(valor);
        }
        public virtual string GetCode()
        {
            return name;
        }

        public virtual float Calc(Candle candle)
        {
            return candle.GetValor(name);
        }
    }
}
