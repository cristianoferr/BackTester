using Backtester.backend.model.formulas;
using Backtester.backend.model.system;
using Backtester.backend.model.system.estatistica;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        internal bool VerificaFormulaViciada(FormulaManager fm,bool flagOriginal)
        {
            foreach(ClarifyNode node in children )
            {
                if (node.VerificaFormulaViciada(fm, flagOriginal)) return true;
            }
            Formula f=fm.GetFormula(original);
            return flagOriginal || f.CheckFormulaViciada();
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


        public bool VerificaFormulaViciada(FormulaManager fm,string formula)
        {
            ClarifyNode node = new ClarifyNode(this, formula);
            return node.VerificaFormulaViciada(fm,false);
        }

        internal ClarifyDict GetDictFor(string formula)
        {
            return dict.Where(x => x.code == formula).FirstOrDefault();
        }

#region Describe

        public void Describe(RichTextBox txt ,CandidatoData candidatoData,Config config)
        {
            
            txt.Text = "";
            TradeSystem ts = candidatoData.tradeSystem;
            AddTitle(txt, ts.name);
            AddQuebraLinha(txt);
            Clarify clarify = new Clarify();

            DescreveCaracteristicas(txt, ts);
            AddQuebraLinha(txt);
            DescreveFormulas(txt, ts, config);
            AddQuebraLinha(txt);
            AddQuebraLinha(txt);

            AddTitle(txt, "ESTATÍSTICAS");
            Estatistica stat = candidatoData.estatistica;
            AddText(txt, "Max.Capital", Utils.FormatCurrency(stat.maxCapital));
            AddText(txt, "Min.Capital", Utils.FormatCurrency(stat.minCapital));
            AddQuebraLinha(txt);
            txt.SelectionIndent += 10;
            if (config.flagCompra)
            {
                AddTitle(txt, "Ponta Comprada");
                SubDado dado = stat.geral.getCompras();
                DescreveDado(txt, dado);
            }
            if (config.flagVenda)
            {
                AddTitle(txt, "Ponta Vendida");
                SubDado dado = stat.geral.getVendas();
                DescreveDado(txt, dado);
            }
            txt.SelectionIndent -= 10;

            AddQuebraLinha(txt);
            AddTitle(txt, "MENSAGENS");
            foreach (string msg in candidatoData.tradeSystem.mensagens)
            {
                AddText(txt, msg);
            }


        }


        private void DescreveDado(RichTextBox txt, SubDado dado)
        {

            txt.SelectionBullet = true;
            AddText(txt, "% Acerto", Utils.FormatCurrency(dado.percAcerto) + "%");
            AddText(txt, "$Win/$Loss", Utils.FormatCurrency(dado.winLossRatio));
            AddText(txt, "Total Ganho", Utils.FormatCurrency(dado.totalGanho));
            AddText(txt, "Total Perdido", Utils.FormatCurrency(dado.totalPerdido));
            AddText(txt, "Trades Stopados", "" + dado.getnTradesStopados());

            txt.SelectionIndent += 10;
            SubSubDado ssd = dado.getTodosTrades();
            DescreveSubDado(txt, "TODOS TRADES:", ssd);
            ssd = dado.getTradesGanhos();
            DescreveSubDado(txt, "TRADES GANHOS:", ssd);
            ssd = dado.getTradesPerdidos();
            DescreveSubDado(txt, "TRADES PERDIDOS:", ssd);
            txt.SelectionIndent -= 10;

        }

        private void DescreveSubDado(RichTextBox txt, string titulo, SubSubDado ssd)
        {
            txt.SelectionBullet = false;
            AddQuebraLinha(txt);
            AddTitle(txt, titulo);
            txt.SelectionBullet = true;
            AddText(txt, "Qtd Trades", "" + ssd.getnTrades());

            AddText(txt, "Duração Média", "" + ssd.getAvgDias());
            AddText(txt, "Duração Máxima", "" + ssd.getMaxDias());
            AddText(txt, "Duração Mínima", "" + ssd.getMinDias());
            AddText(txt, "Maior Variação", "" + Utils.FormatCurrency(ssd.getMaxTrade()));
            AddText(txt, "Menor Variação", "" + Utils.FormatCurrency(ssd.getMinTrade()));
            AddText(txt, "Variação Total", "" + Utils.FormatCurrency(ssd.getTotal()));
            txt.SelectionBullet = false;
        }

        private void DescreveFormulas(RichTextBox txt, TradeSystem ts, Config config)
        {
            if (config.flagCompra)
            {
                AddTitle(txt, "CONDIÇÕES COMPRA");
                AddText(txt, "Cond.Compra", ClarificaFormula(ts.condicaoEntradaC));
                AddText(txt, "Sizing Compra", ClarificaFormula(ts.sizingCompra));
                AddText(txt, "Stop Inicial Compra", ClarificaFormula(ts.stopInicialC));
                if (ts.usaStopMovel)
                {
                    AddText(txt, "Stop Movel Compra", ClarificaFormula(ts.stopMovelC));
                }
            }
            if (config.flagVenda)
            {
                AddTitle(txt, "CONDIÇÕES VENDA");
                AddText(txt, "Cond.Venda", ClarificaFormula(ts.condicaoEntradaC));
                AddText(txt, "Sizing Venda", ClarificaFormula(ts.sizingCompra));
                AddText(txt, "Stop Inicial Venda", ClarificaFormula(ts.stopInicialC));
                if (ts.usaStopMovel)
                {
                    AddText(txt, "Stop Movel Venda", ClarificaFormula(ts.stopMovelC));
                }
            }
        }

        private void DescreveCaracteristicas(RichTextBox txt, TradeSystem ts)
        {
            AddTitle(txt, "CARACTERÍSTICAS");
            txt.SelectionBullet = true;
            AddText(txt, ts.usaMultiplasEntradas ? "Usa Múltiplas Entradas" : "Não usa Múltiplas Entradas");
            AddText(txt, ts.usaStopMovel ? "Usa Stop Móvel" : "Não Usa Stop Móvel");
            AddText(txt, "Risco por trade", Utils.FormatCurrency(ts.riscoTrade) + "%");
            AddText(txt, "Risco Global", Utils.FormatCurrency(ts.riscoGlobal) + "%");
            AddText(txt, "Gap Stop Inicial", Utils.FormatCurrency(ts.stopGapPerc) + "%");
            AddText(txt, "% capital máximo por trade", Utils.FormatCurrency(ts.percTrade) + "%");
            AddText(txt, "Capital máximo por trade", "$ " + Utils.FormatCurrency(ts.maxCapitalTrade));
            AddText(txt, "Stop Mensal", Utils.FormatCurrency(ts.stopMensal) + "%");
            txt.SelectionBullet = false;
        }

        private void AddText(RichTextBox txt, string v)
        {
            txt.AppendText(v);
            AddQuebraLinha(txt);
        }

        private void AddText(RichTextBox txt, string v1, string v2)
        {
            txt.SelectionFont = new Font(txt.Font, FontStyle.Bold);
            txt.AppendText(v1 + ": ");
            txt.SelectionFont = new Font(txt.Font, FontStyle.Regular);
            txt.AppendText(v2);
            AddQuebraLinha(txt);
        }

        private void AddQuebraLinha(RichTextBox txt)
        {
            txt.AppendText("\r\n");
        }

        private void AddTitle(RichTextBox txt, string name)
        {
            txt.SelectionFont = new Font(txt.Font, FontStyle.Bold);
            txt.AppendText(name);
            txt.SelectionFont = new Font(txt.Font, FontStyle.Regular);
            AddQuebraLinha(txt);
        }

#endregion
    }
}
