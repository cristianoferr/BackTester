
using Backtester.backend.model.ativos;
using System;

namespace Backtester.backend.model.formulas
{
    public class FormulaMod : Formula
    {
        private Formula campo1;
        private Formula campo2;

        public FormulaMod(FacadeBacktester facade, string name, Formula campo1, Formula campo2)
            : base(facade, name)
        {
            this.campo1 = campo1;
            this.campo2 = campo2;
        }

        public override string GetCode()
        {
            return name + "(" + campo1.ToString() + "," + campo2.ToString() + ")";
        }


        public override float Calc(Candle candle)
        {
            float v1 = candle.GetValor(campo1);
            float v2 = candle.GetValor(campo1);

            if (v2 == 0) return 0;
            return v1 % v2;
            
        }


    }
}
