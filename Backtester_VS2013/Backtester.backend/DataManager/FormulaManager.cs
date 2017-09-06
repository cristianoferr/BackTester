
using Backtester.backend.model.formulas;
using System.Collections.Generic;
using UsoComum;
namespace Backtester.backend.DataManager
{
    public class FormulaManager
    {



        public List<string> formulasDisp = new List<string>();
        public Dictionary<string, Formula> formulasInst = new Dictionary<string, Formula>();
        public List<string> formulasCache = new List<string>();

        public static string CLOSE = "C";
        public static string OPEN = "O";
        public static string HIGH = "H";
        public static string LOW = "L";
        public static string VOL = "V";
        // public static string DIFOC = "DIFOC";//TODO: IMPLEMENTAR ESSAS 2 FUNÇÕES
        //public static string DIFHL = "DIFHL";
        public static string MMS = "MMS";
        public static string MME = "MME";
        public static string IFR = "RSI";
        public static string HV = "HV";
        public static string LV = "LV";
        public static string TRIX = "TRIX";

        public static string REF = "REF";
        public static string AVGGAIN = "AVGGAIN";
        public static string HILO = "HILO";
        public static string AVGLOSS = "AVGLOSS";
        public static string STDDEV = "STD";
        public static string UPPERBB = "UPPERBB";
        public static string MIDDLEBB = "MIDDLEBB";
        public static string LOWERBB = "LOWERBB";
        //  public static string BB = "BB";
        public static string SUM = "SUM";
        public static string ABS = "ABS";
        public static string DIF = "DIF";
        public static string INVERT_SIGNAL = "INVERT_SIGNAL";
        
        public static string SUBTRACT = "SUBTRACT";
        public static string MULTIPLY = "MULTIPLY";
        public static string DIVIDE = "DIVIDE";

        // public static string DAYOFWEEK = "DAYOFWEEK";
        // public static string TICK = "TICK";
        public static string STOCH = "STOCH";
        public static string PERCENTIL = "PERCENTIL";

        //LOGICAL
        public static string COMP_DIF = UsoComum.ConstsComuns.Operador.DIFFERENT.ToString();
        public static string COMP_EQUAL = UsoComum.ConstsComuns.Operador.EQUAL.ToString();
        public static string COMP_GREATER = UsoComum.ConstsComuns.Operador.GREATER.ToString();
        public static string COMP_GREATER_EQ = UsoComum.ConstsComuns.Operador.GREATER_EQ.ToString();
        public static string COMP_LOWER = UsoComum.ConstsComuns.Operador.LOWER.ToString();
        public static string COMP_LOWER_EQ = UsoComum.ConstsComuns.Operador.LOWER_EQ.ToString();

        //BOOLEAN
        public static string BOOL_AND = UsoComum.ConstsComuns.BOOLEAN_TYPE.AND.ToString();
        public static string BOOL_OR = UsoComum.ConstsComuns.BOOLEAN_TYPE.OR.ToString();
        public static string BOOL_XOR = UsoComum.ConstsComuns.BOOLEAN_TYPE.XOR.ToString();


        private FacadeBacktester facade;

        public FormulaManager(FacadeBacktester facadeBacktester)
        {
            this.facade = facadeBacktester;
            formulasDisp.Add(BOOL_AND);
            formulasDisp.Add(BOOL_OR);
            formulasDisp.Add(BOOL_XOR);

            formulasDisp.Add(COMP_DIF);
            formulasDisp.Add(COMP_EQUAL);
            formulasDisp.Add(COMP_GREATER);
            formulasDisp.Add(COMP_GREATER_EQ);
            formulasDisp.Add(COMP_LOWER);
            formulasDisp.Add(COMP_LOWER_EQ);

            formulasDisp.Add(CLOSE);
            formulasDisp.Add(OPEN);
            formulasDisp.Add(HIGH);
            formulasDisp.Add(LOW);
            formulasDisp.Add(VOL);
            //formulasDisp.Add(DIFOC);
            //formulasDisp.Add(DIFHL);
            formulasDisp.Add(MMS);
            formulasDisp.Add(MME);
            formulasDisp.Add(TRIX);
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
            formulasDisp.Add(HILO);
            //  formulasDisp.Add(BB);
            formulasDisp.Add(SUM);
            formulasDisp.Add(ABS);
            formulasDisp.Add(INVERT_SIGNAL);
            
            formulasDisp.Add(DIF);
            formulasDisp.Add(SUBTRACT);
            // formulasDisp.Add(DAYOFWEEK);
            formulasDisp.Add(MULTIPLY);
            // formulasDisp.Add(TICK);
            formulasDisp.Add(STOCH);
            formulasDisp.Add(PERCENTIL);


        }

        public void ClearFormulas()
        {
            /* while (formulasInst.Count > Consts.QTD_MAXIMA_FORMULAS_CACHE)
             {
                 string key=formulasCache[0];
                 formulasInst.Remove(key);
                 formulasCache.RemoveAt(0);
             }*/
            formulasInst.Clear();
            formulasCache.Clear();
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
            string name = Utils.getFormulaNameFromCode(code);
            string par = Utils.getFormulaParFromCode(code);

            Formula formula = CreateFormula(name, par);
            //  facade.dh.AddFormula(name, par);
            facade.dh.AddFormula(formula);
            return formula;

        }

       

        private Formula CreateFormula(string name, string par)
        {

            if (formulasInst.ContainsKey(GetCode(name, par))) return formulasInst[GetCode(name, par)];

            string[] pars = Utils.SplitParameters(par);

            Formula f = null;

            if (Utils.IsNumber(name))
            {
                f = new FormulaNumber(facade, name);
            }

            //Esses operadores aceitam tanto fórmula quanto número no segundo parametro
            if (Utils.IsNumber(pars[1]))
            {
                if (name == SUBTRACT) f = new FormulaSUBTRACT(facade, name, GetFormula(pars[0]), float.Parse(pars[1]));
                if (name == MULTIPLY) f = new FormulaMultiply(facade, name, GetFormula(pars[0]), float.Parse(pars[1]));
                if (name == DIVIDE) f = new FormulaDivide(facade, name, GetFormula(pars[0]), float.Parse(pars[1]));
                if (name == SUM) f = new FormulaSUM(facade, name, GetFormula(pars[0]), float.Parse(pars[1]));
                if (name == ABS) f = new FormulaABS(facade, name, GetFormula(pars[0]), float.Parse(pars[1]));
                if (name == DIF) f = new FormulaDif(facade, name, GetFormula(pars[0]), float.Parse(pars[1]));
                if (name == INVERT_SIGNAL) f = new FormulaInvertSignal(facade, name, GetFormula(pars[0]), float.Parse(pars[1]));
            }
            else
            {
                if (name == SUBTRACT) f = new FormulaSUBTRACT(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
                if (name == MULTIPLY) f = new FormulaMultiply(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
                if (name == DIVIDE) f = new FormulaDivide(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
                if (name == SUM) f = new FormulaSUM(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
                if (name == ABS) f = new FormulaABS(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
                if (name == DIF) f = new FormulaDif(facade, name, GetFormula(pars[0]), float.Parse(pars[1]));
                if (name == INVERT_SIGNAL) f = new FormulaInvertSignal(facade, name, GetFormula(pars[0]), float.Parse(pars[1]));
                
            }

            if (f == null)
            {
                f = CreateLogicalFormulas(name, pars);
            }

            if (f == null)
            {
                f = CreateBooleanFormulas(name, pars);
            }

            if (name == STOCH) f = new FormulaStoch(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
            if (name == PERCENTIL) f = new FormulaPercentil(facade, name, GetFormula(pars[0]));
            //if (name == TICK) f = new FormulaTick(facade, name);
            // if (name == DAYOFWEEK) f = new FormulaDayOfWeek(facade, name);
            if (name == REF) f = new FormulaREF(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
            if (name == IFR) f = new FormulaRSI(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
            if (name == HV) f = new FormulaHV(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
            if (name == TRIX) f = new FormulaTRIX(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
            if (name == LV) f = new FormulaLV(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
            if (name == HILO) f = new FormulaHilo(facade, name, GetFormula(pars[0]), GetFormula(pars[1]), GetFormula(pars[2]), GetFormula(pars[3]));
            if (name == AVGGAIN) f = new FormulaAvgGain(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
            if (name == AVGLOSS) f = new FormulaAvgLoss(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
            if (name == STDDEV) f = new FormulaStdDev(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
            if (name == UPPERBB) f = new FormulaBB(facade, UPPERBB, GetFormula(pars[0]), "U", GetFormula(pars[1]), GetFormula(pars[2]));
            if (name == MIDDLEBB) f = new FormulaBB(facade, MIDDLEBB, GetFormula(pars[0]), "M", GetFormula(pars[1]), GetFormula(pars[2]));
            if (name == LOWERBB) f = new FormulaBB(facade, LOWERBB, GetFormula(pars[0]), "L", GetFormula(pars[1]), GetFormula(pars[2]));
            // if (name == BB) f = new FormulaBB(facade, BB, GetFormula(pars[0]), pars[1], int.Parse(pars[2]), float.Parse(pars[3]));

            if (name == MME) f = new FormulaMME(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
            if (name == MMS) f = new FormulaMMS(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
            if (f == null) f = new Formula(facade, name);
            string code = GetCode(name, par);
            formulasInst.Add(code, f);
            formulasCache.Add(code);

            return f;
        }

        private Formula CreateBooleanFormulas(string name, string[] pars)
        {
            Formula f = null;
            if (name == BOOL_AND) f = new FormulaAND(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
            if (name == BOOL_OR) f = new FormulaOR(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
            if (name == BOOL_XOR) f = new FormulaXOR(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
            return f;
        }

        
        private Formula CreateLogicalFormulas(string name, string[] pars)
        {
            Formula f = null;
            if (name == COMP_DIF) f = new FormulaDIF(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
            if (name == COMP_EQUAL) f = new FormulaEQUAL(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
            if (name == COMP_GREATER) f = new FormulaGREATER(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
            if (name == COMP_GREATER_EQ) f = new FormulaGREATER_EQ(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
            if (name == COMP_LOWER) f = new FormulaLOWER(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
            if (name == COMP_LOWER_EQ) f = new FormulaLOWER_EQ(facade, name, GetFormula(pars[0]), GetFormula(pars[1]));
            return f;
        }


    }
}
