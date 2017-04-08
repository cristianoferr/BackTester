
using Backtester.backend.model.ativos;
namespace Backtester.backend.model.formulas
{
    public abstract class FormulaLogical : Formula
    {

        public FormulaLogical(FacadeBacktester facade, string name, Formula campo1, Formula campo2)
            : base(facade, name)
        {
            this.campo1 = campo1;
            this.campo2 = campo2;
            gravar = false;
        }

      

        public override string GetCode()
        {
            return name + "(" + campo1.ToString() + "," + campo2.ToString() + ")";
        }



        public Formula campo1 { get; set; }
        public Formula campo2 { get; set; }

    }
}
