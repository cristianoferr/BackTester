using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backtester.backend.DataManager
{
    class ClarifyDict
    {
        string code;
        string resultado;

        public ClarifyDict(string code, string resultado)
        {
            this.code = code;
            this.resultado = resultado;
        }
    }
    class ClarifyNode
    {
        IList<ClarifyNode> children = new List<ClarifyNode>();;
    }
    public class Clarify
    {
        private FormulaManager fm;
        IList<ClarifyDict> dict;

        public Clarify(FormulaManager fm)
        {
            this.fm = fm;
            dict = new List<ClarifyDict>();
            AddDict(FormulaManager.SUM, "+");
            AddDict(FormulaManager.MULTIPLY, "*");
            AddDict(FormulaManager.DIVIDE, "/");
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
            throw new NotImplementedException();
        }
    }
}
