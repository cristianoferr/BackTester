
using Backtester.backend.DataManager;
using Backtester.backend.model.formulas;
using System.Collections.Generic;
namespace Backtester.backend.model.ativos
{
    public class Candle
    {
        Dictionary<string, float> valores;

        public Candle(Periodo periodo, Ativo ativo)
        {
            this.periodo = periodo;
            this.ativo = ativo;
            valores = new Dictionary<string, float>();

        }

        public Candle(Periodo periodo, Ativo ativo, float open, float close, float high, float low, float vol)
            : this(periodo, ativo)
        {
            SetValor(FormulaManager.CLOSE, close);
            SetValor(FormulaManager.OPEN, open);
            SetValor(FormulaManager.HIGH, high);
            SetValor(FormulaManager.LOW, low);
            SetValor(FormulaManager.VOL, vol);
        }

        public Periodo periodo { get; set; }

        public Ativo ativo { get; set; }

        public void SetValor(string formula, float valor)
        {
            ativo.facade.CreateFormula(formula);
            if (valores.ContainsKey(formula))
            {
                valores.Remove(formula);
            }
            valores.Add(formula, valor);
        }


        public string ToString()
        {
            string s = "Ativo:" + ativo.name + " Dia: " + periodo;
            foreach (string key in valores.Keys)
            {
                s = s + " " + ativo.facade.formulaManager.GetFormula(key).ToString(GetValor(key));
            }
            return s;
        }

        public void SetValor(Formula formula, float valor)
        {
            SetValor(formula.GetCode(), valor);
        }

        public float GetValor(string name)
        {
            Formula f = ativo.facade.formulaManager.GetFormula(name);
            if (!f.gravar) return f.Calc(this);
            if (!valores.ContainsKey(name)) return 0;
            return valores[name];
        }

        public float GetValor(Formula f)
        {
            if (!valores.ContainsKey(f.GetCode())) return f.Calc(this);
            return valores[f.GetCode()];
        }

        Candle candleAnterior_;
        public Candle candleAnterior
        {
            get { if (candleAnterior_ == null) { return this; } return candleAnterior_; }
            set { candleAnterior_ = value; }
        }



        internal bool ContainsFormula(string p)
        {
            return valores.ContainsKey(p);
        }

        Candle proximoCandle_;
        public Candle proximoCandle
        {
            get
            {
                if (proximoCandle_ == null) return this;
                return proximoCandle_;
            }
            set { proximoCandle_ = value; }
        }


    }
}
