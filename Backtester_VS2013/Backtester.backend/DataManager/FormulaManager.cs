
using Backtester.backend.model.formulas;
using System.Collections.Generic;
namespace Backtester.backend.DataManager
{
    public class FormulaManager
    {

        public List<string> formulasDisp = new List<string>();
        public Dictionary<string, Formula> formulasInst = new Dictionary<string, Formula>();

        public static string CLOSE = "C";
        public static string OPEN = "O";
        public static string HIGH = "H";
        public static string LOW = "L";
        public static string VOL = "V";
        public static string DIFOC = "DIFOC";
        public static string DIFHL = "DIFHL";
        public static string MMS = "MMS";
        public static string MME = "MME";
        public static string IFR = "RSI";
        public static string HV = "HV";
        public static string LV = "LV";
        public static string REF = "REF";
        public static string AVGGAIN = "AVGGAIN";
        public static string AVGLOSS = "AVGLOSS";
        public static string STDDEV = "STD";
        public static string UPPERBB = "UPPERBB";
        public static string MIDDLEBB = "MIDDLEBB";
        public static string LOWERBB = "LOWERBB";
        public static string BB = "BB";
        public static string SUM = "SUM";
        public static string SUBTRACT = "SUBTRACT";
        public static string MULTIPLY = "MULTIPLY";
        public static string DAYOFWEEK = "DAYOFWEEK";
        public static string TICK = "TICK";
        public static string STOCH = "STOCH";
        public static string PERCENTIL = "PERCENTIL";
        private FacadeBacktester facade;

        public FormulaManager(FacadeBacktester facadeBacktester)
        {
            this.facade = facadeBacktester;
            formulasDisp.Add(CLOSE);
            formulasDisp.Add(OPEN);
            formulasDisp.Add(HIGH);
            formulasDisp.Add(LOW);
            formulasDisp.Add(VOL);
            formulasDisp.Add(DIFOC);
            formulasDisp.Add(DIFHL);
            formulasDisp.Add(MMS);
            formulasDisp.Add(MME);
            formulasDisp.Add(IFR);
            formulasDisp.Add(HV);
            formulasDisp.Add(LV);
            formulasDisp.Add(REF);
            formulasDisp.Add(AVGLOSS);
            formulasDisp.Add(AVGGAIN);
            formulasDisp.Add(STDDEV);
            formulasDisp.Add(UPPERBB);
            formulasDisp.Add(MIDDLEBB);
            formulasDisp.Add(LOWERBB);
            formulasDisp.Add(BB);
            formulasDisp.Add(SUM);
            formulasDisp.Add(SUBTRACT);
            formulasDisp.Add(DAYOFWEEK);
            formulasDisp.Add(MULTIPLY);
            formulasDisp.Add(TICK);
            formulasDisp.Add(STOCH);
            formulasDisp.Add(PERCENTIL);
        }

        internal void CreateFormula(string formula)
        {
            CreateFormula(formula, "");
        }

        public string GetCode(string formula, string par)
        {
            if (par == "") return formula;
            return formula + "(" + par + ")";
        }

        public Formula GetFormula(string name, string par)
        {
            return GetFormula(GetCode(name, par));
        }

        public Formula GetFormula(string code)
        {
            code = code.ToUpper();
            if (formulasInst.ContainsKey(code))
                return formulasInst[code];
            string name = getFormulaNameFromCode(code);
            string par = getFormulaParFromCode(code);

            Formula formula = CreateFormula(name, par);
            facade.dh.AddFormula(name, par);
            return formula;

        }

        public string getFormulaNameFromCode(string code)
        {
            if (!code.Contains("(")) return code;
            return code.Substring(0, code.IndexOf("("));
        }

        //Esse metodo retira o parametro do codigo
        public string getFormulaParFromCode(string code)
        {
            string name = getFormulaNameFromCode(code);
            if (code == name) return "";
            string par = code.Replace(name + "(", "");
            par = par.Substring(0, par.Length - 1);
            return par;
        }

        private Formula CreateFormula(string name, string par)
        {

            if (formulasInst.ContainsKey(GetCode(name, par))) return formulasInst[GetCode(name, par)];

            string[] pars = new string[5];
            int p = 0;
            int flagParenteses = 0;
            string s = "";
            for (int i = 0; i < par.Length; i++)
            {
                if (par[i] == '(') flagParenteses++;
                if (par[i] == ')')
                {
                    flagParenteses--;
                }
                if ((flagParenteses == 0) && (par[i] == ','))
                {
                    pars[p] = s;
                    p++;
                    s = "";
                }
                else
                    s = s + par[i];
            }
            pars[p] = s;
            for (int i = 0; i < pars.Length; i++)
            {
                if (pars[i] != null) if (pars[i].EndsWith(".0")) pars[i] = pars[i].Replace(".0", "");
            }

            Formula f = null;

            //Esses operadores aceitam tanto fórmula quanto número no segundo parametro
            if (Util.IsNumber(pars[1]))
            {
                if (name == SUBTRACT) f = new FormulaSUBTRACT(facade, name, GetFormula(pars[0]), float.Parse(pars[1]));
                if (name == MULTIPLY) f = new FormulaMultiply(facade, name, GetFormula(pars[0]), float.Parse(pars[1]));
                if (name == SUM) f = new FormulaSUM(facade, name, GetFormula(pars[0]), float.Parse(pars[1]));
            }
            else
            {
                if (name == SUBTRACT) f = new FormulaSUBTRACT(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
                if (name == MULTIPLY) f = new FormulaMultiply(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
                if (name == SUM) f = new FormulaSUM(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
            }


            if (name == STOCH) f = new FormulaStoch(facade, name, int.Parse(pars[0]), int.Parse(pars[1]));
            if (name == PERCENTIL) f = new FormulaPercentil(facade, name, GetFormula(pars[0]));
            //if (name == TICK) f = new FormulaTick(facade, name);
            // if (name == DAYOFWEEK) f = new FormulaDayOfWeek(facade, name);
            if (name == REF) f = new FormulaREF(facade, name, GetFormula(pars[0]), int.Parse(pars[1]));
            if (name == IFR) f = new FormulaRSI(facade, name, GetFormula(pars[0]), int.Parse(pars[1]));
            if (name == HV) f = new FormulaHV(facade, name, GetFormula(pars[0]), int.Parse(pars[1]));
            if (name == LV) f = new FormulaLV(facade, name, GetFormula(pars[0]), int.Parse(pars[1]));
            if (name == AVGGAIN) f = new FormulaAvgGain(facade, name, GetFormula(pars[0]), int.Parse(pars[1]));
            if (name == AVGLOSS) f = new FormulaAvgLoss(facade, name, GetFormula(pars[0]), int.Parse(pars[1]));
            if (name == STDDEV) f = new FormulaStdDev(facade, name, GetFormula(pars[0]), int.Parse(pars[1]));
            if (name == UPPERBB) f = new FormulaBB(facade, BB, GetFormula(pars[0]), "U", int.Parse(pars[1]), float.Parse(pars[2]));
            if (name == MIDDLEBB) f = new FormulaBB(facade, BB, GetFormula(pars[0]), "M", int.Parse(pars[1]), float.Parse(pars[2]));
            if (name == LOWERBB) f = new FormulaBB(facade, BB, GetFormula(pars[0]), "L", int.Parse(pars[1]), float.Parse(pars[2]));
            if (name == BB) f = new FormulaBB(facade, BB, GetFormula(pars[0]), pars[1], int.Parse(pars[2]), float.Parse(pars[3]));

            if (name == MME) f = new FormulaMME(facade, name, GetFormula(pars[0]), int.Parse(pars[1]));
            if (name == MMS) f = new FormulaMMS(facade, name, GetFormula(pars[0]), int.Parse(pars[1]));
            if (f == null) f = new Formula(facade, name);
            formulasInst.Add(GetCode(name, par), f);

            return f;
        }
    }
}
