
using Backtester.backend.model.ativos;
namespace Backtester.backend.model.formulas
{
    public class FormulaMMS : Formula
    {
        public int per { get; set; }
        public Formula campo { get; set; }

        public FormulaMMS(FacadeBacktester facade, string name, string campo, int per)
            : base(facade, name)
        {
            this.campo = facade.formulaManager.GetFormula(campo);
            this.per = per;
        }

        public FormulaMMS(FacadeBacktester facade, string name, Formula campo, int per)
            : base(facade, name)
        {
            this.campo = campo;
            this.per = per;
        }

        public override string GetCode()
        {
            return name + "(" + campo.GetCode() + "," + per + ")";
        }

        public override float Calc(Candle candle)
        {
            float tot = 0;
            Candle c = candle;
            for (int i = 0; i < per; i++)
            {
                //System.out.println("i:"+i+" "+tot);
                tot = tot + c.GetValor(campo);
                c = c.candleAnterior;
            }
            tot = tot / per;
            //System.out.println("tot:"+tot);
            return tot;
        }
    }
}
