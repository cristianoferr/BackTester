
using Backtester.backend.DataManager;
using Backtester.backend.model.ativos;
using System;
namespace Backtester.backend.model.formulas
{
    /*
 * Essa classe retorna o desvio padrão (standard deviation) de uma determinada ação em
 * um determinado período em um determinado cmapo 
 */
    public class FormulaStdDev : Formula
    {
        Formula per;
        Formula campo;

        public FormulaStdDev(FacadeBacktester facade, string name, Formula campo, Formula per)
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
            Candle cp = candle;
            float avg = candle.GetValor(facade.formulaManager.GetFormula(FormulaManager.MMS, campo.GetCode() + "," + per));

            float soma = 0; 
            float vPer = per.Calc(candle);
            vPer = LimitPeriodo(vPer);
            if (vPer == 0) return 0;
            for (int i = 0; i < vPer; i++)
            {
                //System.out.println(candle.getPeriodo()+" "+cp.getValor(FormulaManager.close));
                float dif = cp.GetValor(campo) - avg;
                dif = dif * dif;
                soma += dif;
                cp = cp.candleAnterior;
                if (cp == null) return 0;

            }
            soma = soma / vPer;


            return (float)Math.Sqrt(soma);
        }
    }
}
