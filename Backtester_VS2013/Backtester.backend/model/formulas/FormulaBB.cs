
using Backtester.backend.DataManager;
using Backtester.backend.model.ativos;
using System;
namespace Backtester.backend.model.formulas
{
    public class FormulaBB : Formula
    {
        Formula per;
        string banda;
        Formula desvio;
        Formula campo;

        public FormulaBB(FacadeBacktester facade, string name, Formula campo, string banda, Formula per, Formula desvio)
            : base(facade, name)
        {
            this.per = per;
            if (banda != "M" && banda != "U" && banda != "L")
                banda = "M";
            banda = banda.ToUpper();
            this.desvio = desvio;
            this.banda = banda;
            this.campo = campo;
        }



        public override string GetCode()
        {
            return name + "(" + campo.GetCode() + "," + per.GetCode() + "," + desvio.GetCode() + ")";
        }

        public override float Calc(Candle candle)
        {
            float vPer = per.Calc(candle);
            vPer = LimitPeriodo(vPer);
            float v = 0;
            float avg = candle.GetValor(facade.formulaManager.GetFormula(FormulaManager.MMS, campo.GetCode() + "," + vPer));
            float stdDev = candle.GetValor(facade.formulaManager.GetFormula(FormulaManager.STDDEV, campo.GetCode() + "," + vPer));
            if (banda == "U") v = avg + desvio.Calc(candle) * stdDev;
            if (banda == "M") v = avg;
            if (banda == "L") v = avg - desvio.Calc(candle) * stdDev;

            return v;
        }
    }
}
