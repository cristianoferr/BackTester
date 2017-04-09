
using Backtester.backend.model.ativos;
using System;
using System.Globalization;
namespace Backtester.backend.model.formulas
{
    public class FormulaNumber : Formula
    {

        public FormulaNumber(FacadeBacktester facade, string value)
            : base(facade, "NUMBER")
        {
            this.value = float.Parse(value);
            if (this.value > 100) this.value = 100;
            if (this.value < -100) this.value = -100;
            gravar = false;
        }



        public override string GetCode()
        {
            return value.ToString("0.###", CultureInfo.CreateSpecificCulture("en-US"));
        }


        public override float Calc(Candle candle)
        {
            return value;
        }

        public float value { get; set; }
    }
}
