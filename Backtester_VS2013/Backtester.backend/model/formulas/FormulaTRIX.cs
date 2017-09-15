
using Backtester.backend.DataManager;
using Backtester.backend.model.ativos;
namespace Backtester.backend.model.formulas
{
    public class FormulaTRIX : Formula
    {
        Formula per;
        Formula campo;

        public FormulaTRIX(FacadeBacktester facade, string name, Formula campo, Formula per)
            : base(facade, name)
        {
            this.per = per;
            this.campo = campo;
        }



        public override string GetCode()
        {
            return name + "(" + campo.GetCode() + "," + per.GetCode() + ")";
        }
        public override bool CheckFormulaViciada()
        {
            return campo.IsNumber() || campo.CheckFormulaViciada();
        }



        public override float Calc(Candle candle)
        {
            Candle c = candle;
            Candle candleAnterior = candle.candleAnterior;
            if (candleAnterior == null) return 0;
            float vPer = per.Calc(candle);
            vPer = LimitPeriodo(vPer);
            string m1 = FormulaManager.MME + "(" + campo.GetCode() + "," + vPer + ")";
            string m2 = FormulaManager.MME + "(" + m1 + "," + vPer + ")";
            string m3 = FormulaManager.MME + "(" + m2 + "," + vPer + ")";

            float vAtual = candle.GetValor(m3);
            float vAnterior = candleAnterior.GetValor(m3);
            if (vAnterior == 0) return 0;

            return vAtual / vAnterior;
        }
    }
}
