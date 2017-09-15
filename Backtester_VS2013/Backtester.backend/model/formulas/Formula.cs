
using Backtester.backend.model.ativos;
using System;
namespace Backtester.backend.model.formulas
{
    public class Formula
    {
        public bool gravar { get; set; }
        public Formula(FacadeBacktester facade, string name)
        {
            this.name = name;
            this.facade = facade;
            gravar = true;
        }

        public FacadeBacktester facade { get; set; }

        public string name { get; set; }
        public string ToString(float valor)
        {
            return GetCode() + "=" + UsoComum.Utils.FormatCurrency(valor);
        }
        public virtual string GetCode()
        {
            return name;
        }

        public virtual float Calc(Candle candle)
        {
            return candle.GetValorIfExists(name);
        }

        internal float LimitPeriodo(float per)
        {
            per = Math.Abs(per);
            if (per > 40)
            {
                per = 40;
            }
            if (per <= 0) per = 0;
            return per;
        }

        public override string ToString()
        {
            return GetCode();
        }

        public virtual bool CheckFormulaViciada()
        {
            return false;
        }

        public virtual bool IsNumber()
        {
            return false;
        }
    }

}
