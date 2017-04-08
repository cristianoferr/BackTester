
using Backtester.backend.DataManager;
using Backtester.backend.model.ativos;
using System;
namespace Backtester.backend.model.formulas
{
    public class FormulaRSI : Formula
    {
        Formula per;
        Formula campo;

        public FormulaRSI(FacadeBacktester facade, string name, Formula campo, Formula per)
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

            float ag = candle.GetValor(facade.formulaManager.GetFormula(FormulaManager.AVGGAIN, campo.GetCode() + "," + per.GetCode()));
            float al = candle.GetValor(facade.formulaManager.GetFormula(FormulaManager.AVGLOSS, campo.GetCode() + "," + per.GetCode()));

            if (al == 0) return 0;
            float rs = ag / al;
            float rsi = 100 - (100 / (1 + rs));

            return rsi;
        }
    }
}
