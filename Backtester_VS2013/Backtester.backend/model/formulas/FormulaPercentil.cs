
using Backtester.backend.DataManager;
using Backtester.backend.model.ativos;
namespace Backtester.backend.model.formulas
{
    /*
 * Esse método retorna o percentil do campo, ou seja:
 * Seja H=1 e L=0, qual o valor correspondente ao "campo"
 */
    public class FormulaPercentil : Formula
    {
        Formula campo;

        public FormulaPercentil(FacadeBacktester facade, string name, Formula campo)
            : base(facade, name)
        {
            this.campo = campo;
        }



        public override string GetCode()
        {
            return name + "(" + campo.GetCode() + ")";
        }




        public override float Calc(Candle candle)
        {
            float x = candle.GetValor(campo);
            float h = candle.GetValor(FormulaManager.HIGH);
            float l = candle.GetValor(FormulaManager.LOW);

            float dif = h - l;
            x = x - l;
            if (dif == 0) return 0;
            return x / dif;
        }
    }
}
