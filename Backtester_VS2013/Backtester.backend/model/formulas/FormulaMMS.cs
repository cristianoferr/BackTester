
using Backtester.backend.model.ativos;
namespace Backtester.backend.model.formulas
{
    public class FormulaMMS : Formula
    {
        public Formula per { get; set; }
        public Formula campo { get; set; }

       

        public FormulaMMS(FacadeBacktester facade, string name, Formula campo, Formula per)
            : base(facade, name)
        {
            this.campo = campo;
            this.per = per;
        }

        public override bool CheckFormulaViciada()
        {
            return campo.IsNumber() || campo.CheckFormulaViciada();
        }


        public override string GetCode()
        {
            return name + "(" + campo.GetCode() + "," + per.GetCode() + ")";
        }

        public override float Calc(Candle candle)
        {
            float tot = 0;
            Candle c = candle;
            float vPer = per.Calc(candle);
            vPer = LimitPeriodo(vPer);
            if (vPer <= 0) return 0;
            for (int i = 0; i < vPer; i++)
            {
                //System.out.println("i:"+i+" "+tot);
                float vlr = 0;
                if (c == null) return 0;
                if (c.candleAnterior!=c)
                    vlr=c.GetValor(campo);
                tot = tot + vlr;
                c = c.candleAnterior;
            }
            tot = tot / vPer;
            //System.out.println("tot:"+tot);
            return tot;
        }
    }
}
