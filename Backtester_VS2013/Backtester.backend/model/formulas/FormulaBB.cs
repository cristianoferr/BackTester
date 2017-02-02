
using Backtester.backend.DataManager;
using Backtester.backend.model.ativos;
namespace Backtester.backend.model.formulas
{
    public class FormulaBB : Formula
    {
        int per;
        string banda;
        float desvio;
        Formula campo;

        public FormulaBB(FacadeBacktester facade, string name, Formula campo, string banda, int per, float desvio)
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
            return name + "(" + campo.GetCode() + "," + banda + "," + per + "," + desvio + ")";
        }

        public override float Calc(Candle candle)
        {
            float v = 0;
            float avg = candle.GetValor(facade.formulaManager.GetFormula(FormulaManager.MMS, campo.GetCode() + "," + per));
            float stdDev = candle.GetValor(facade.formulaManager.GetFormula(FormulaManager.STDDEV, campo.GetCode() + "," + per));
            if (banda == "U") v = avg + desvio * stdDev;
            if (banda == "M") v = avg;
            if (banda == "L") v = avg - desvio * stdDev;

            return v;
        }
    }
}
