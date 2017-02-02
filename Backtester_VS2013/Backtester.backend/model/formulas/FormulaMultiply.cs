
using Backtester.backend.model.ativos;
namespace Backtester.backend.model.formulas
{
    public class FormulaMultiply : Formula
    {
        int per;

        public FormulaMultiply(FacadeBacktester facade, string name, Formula campo1, Formula campo2)
            : base(facade, name)
        {
            this.campo1 = campo1;
            this.campo2 = campo2;
            gravar = false;
        }

        public FormulaMultiply(FacadeBacktester facade, string name, Formula campo1, float v2)
            : base(facade, name)
        {
            this.campo1 = campo1;
            this.v2 = v2;
            gravar = false;
        }


        public override string GetCode()
        {
            if (campo2 == null) return name + "(" + campo1.GetCode() + "," + v2 + ")";
            return name + "(" + campo1.ToString() + "," + campo2.ToString() + ")";
        }




        public override float Calc(Candle candle)
        {
            Candle c = candle;

            if (campo2 == null) return c.GetValor(campo1) * v2;
            return c.GetValor(campo1) * c.GetValor(campo2);
        }

        public Formula campo1 { get; set; }
        public Formula campo2 { get; set; }

        public float v2 { get; set; }
    }
}
