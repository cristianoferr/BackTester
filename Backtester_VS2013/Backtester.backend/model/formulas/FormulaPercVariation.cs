
using Backtester.backend.DataManager;
using Backtester.backend.model.ativos;
using System;
namespace Backtester.backend.model.formulas
{
    /*
 Essa formula retorna a variação média
 */
    public class FormulaPercVariation : Formula
    {
        Formula per;
        Formula campo;

        public FormulaPercVariation(FacadeBacktester facade, string name, Formula campo, Formula per)
            : base(facade, name)
        {
            this.campo = campo;
            this.per = per;
        }



        public override string GetCode()
        {
            return name + "(" + campo.GetCode() + "," + per.GetCode() + ")";
        }




        public override float Calc(Candle candle)
        {
            Candle cp = candle;

            float soma = 0; 
            float vPer = per.Calc(candle);
            vPer = LimitPeriodo(vPer);
            float vlrFinal = cp.GetValor(campo);
            if (vPer == 0) return 0;
            for (int i = 0; i < vPer; i++)
            {
                cp = cp.candleAnterior;
                if (cp == null) return 0;

            }
            float vlrInicial = cp.GetValor(campo);


            return (vlrFinal/vlrInicial-1)*100;
        }
    }
}
