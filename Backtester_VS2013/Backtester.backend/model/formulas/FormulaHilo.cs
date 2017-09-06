
using Backtester.backend.DataManager;
using Backtester.backend.model.ativos;
using System;
namespace Backtester.backend.model.formulas
{
    public class FormulaHilo : Formula
    {
        Formula campoH;
        Formula campoL,campo;
        Formula periodo;

        public FormulaHilo(FacadeBacktester facade, string name, Formula campo, Formula campoH,Formula campoL,Formula periodo)
            : base(facade, name)
        {
            this.periodo = periodo;
            this.campoH = campoH;
            this.campoL = campoL;
            this.campo = campo;
        }



        public override string GetCode()
        {
            return name + "(" + campo.GetCode() + "," + campoH.GetCode() + "," + campoL.GetCode() + "," + periodo.GetCode() + ")";
        }

        public override float Calc(Candle candle)
        {
            string fHiloH = FormulaManager.MMS + "(" + campoH.GetCode() + "," + periodo.GetCode() + ")";
            string fHiloL = FormulaManager.MMS + "(" + campoL.GetCode() + "," + periodo.GetCode() + ")";
            float hiloH = candle.GetValor(FormulaManager.REF+"("+fHiloH+",1)");
            float hiloL = candle.GetValor(FormulaManager.REF + "(" + fHiloL + ",1)");

            float C = candle.GetValor(campo);

            if (C > hiloH) return hiloH;
            if (C < hiloL) return hiloL;
            return 0;

        }
    }
}
