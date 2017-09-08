using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsoComum;

namespace Backtester.backend.DataManager
{
    class ClarifyDict
    {
        public string code;
        public string resultado;

        public ClarifyDict(string code, string resultado)
        {
            this.code = code;
            this.resultado = resultado;
        }
    }
    class ClarifyNode
    {
        IList<ClarifyNode> children = new List<ClarifyNode>();
        private Clarify clarify;
        private string original;
        private string formula;
        private string[] pars;
        private ClarifyDict dict;

        public ClarifyNode(Clarify clarify, string original)
        {
            this.clarify = clarify;
            this.original = original;
            this.formula = Utils.getFormulaNameFromCode(original);
            this.dict = clarify.GetDictFor(formula);
            string param= Utils.getFormulaParFromCode(original);
            this.pars = Utils.SplitParameters(param);
            for (int i = 0; i < pars.Length; i++)
            {
                if (pars[i] != null && pars[i] != "")
                {
                    children.Add(new ClarifyNode(clarify, pars[i].Trim()));
                }
            }
        }

        public override string ToString()
        {
            if (children.Count == 0) return ReplaceDict();
            if (dict == null || children.Count == 1)
            {
                string ret = "";
                for (int i = 0; i < children.Count; i++)
                {
                    ret = ret + children[i].ToString() + ",";
                }
                ret = ret.Substring(0, ret.Length - 1);
                return formula + "(" + ret + ")";
            } else
            {

                string suffix = " " + dict.resultado + " ";
                string ret = "";
                for (int i = 0; i < children.Count; i++)
                {
                    ret = ret + children[i].ToString() + suffix;
                }
                ret = ret.Substring(0, ret.LastIndexOf(suffix));
                return "(" +ret + ")";
            }
        }

        private string ReplaceDict()
        {
            if (dict == null) return formula;
            return dict.resultado;
        }
    }
    public class Clarify
    {
        IList<ClarifyDict> dict;

        public Clarify()
        {
            dict = new List<ClarifyDict>();
            AddDict(FormulaManager.SUM, "+");
            AddDict(FormulaManager.MULTIPLY, "*");
            AddDict(FormulaManager.DIVIDE, "/");
            AddDict(FormulaManager.MOD, "%");
            AddDict(FormulaManager.BOOL_AND, "&&");
            AddDict(FormulaManager.BOOL_OR, "||");
            AddDict(FormulaManager.BOOL_XOR, "^^");
            AddDict(FormulaManager.COMP_GREATER, ">");
            AddDict(FormulaManager.COMP_GREATER_EQ, ">=");
            AddDict(FormulaManager.COMP_LOWER, "<");
            AddDict(FormulaManager.COMP_LOWER_EQ, "<=");
            AddDict(FormulaManager.COMP_EQUAL, "==");
            AddDict(FormulaManager.COMP_DIF, "<>");
        }

        private void AddDict(string code, string resultado)
        {
            dict.Add(new ClarifyDict(code, resultado));
        }

        public string ClarificaFormula(string original)
        {
            ClarifyNode node = new ClarifyNode(this, original);
            string result= node.ToString();
            if (result[0] == '(')
            {
                result = result.Substring(1, result.Length - 2);
            }
            return result;
        }

        internal ClarifyDict GetDictFor(string formula)
        {
            return dict.Where(x => x.code == formula).FirstOrDefault();
        }
    }
}
