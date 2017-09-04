
using Backtester.backend.DataManager;
using Backtester.backend.model.formulas;
using System.Collections.Generic;
using System;

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

        internal void AddData(float open, float close, float high, float low, float vol)
        {
            SetValor(FormulaManager.CLOSE, close);
            if (high > GetValor(FormulaManager.HIGH))
            {
                SetValor(FormulaManager.HIGH, high);
            }

            if (low < GetValor(FormulaManager.LOW))
            {
                SetValor(FormulaManager.LOW, low);
            }
            SetValor(FormulaManager.VOL, vol + GetValor(FormulaManager.VOL));
        }

        List<string> keysToRemove = new List<string>();
        internal void ClearData()
        {

            keysToRemove.Clear();
            foreach (string key in valores.Keys)
            {
                if (key!=FormulaManager.CLOSE &&
                    key!=FormulaManager.OPEN &&
                    key!=FormulaManager.HIGH &&
                    key!=FormulaManager.LOW &&
                    key != FormulaManager.VOL)
                {
                    keysToRemove.Add(key);
                }

            }

            foreach (string key in keysToRemove)
            {
                valores.Remove(key);
            }
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

        public void RemoveValor(string key)
        {
            valores.Remove(key);
        }

        public override string ToString()
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
            if (valores.ContainsKey(name))
            {
                return valores[name];
            }
            Formula f = ativo.facade.formulaManager.GetFormula(name);
            if (valores.ContainsKey(name))
            {
                return valores[name];
            }
            float calc= f.Calc(this);
            SetValor(name, calc);
            return calc;
        }

        public float GetValor(Formula f)
        {
            if (!valores.ContainsKey(f.GetCode()))
            {
                float calc = f.Calc(this);
                SetValor(f, calc);
                return calc;
            }
            return valores[f.GetCode()];
        }

        public float GetValorIfExists(string name)
        {
            if (valores.ContainsKey(name))
                return valores[name];
            return 0;
        }

        public Candle candleAnterior { get; set; }



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
