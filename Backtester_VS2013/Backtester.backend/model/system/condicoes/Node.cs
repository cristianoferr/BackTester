
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Backtester.backend.model.system.condicoes
{
    [DataContract]
    public class Node : ICondicao
    {

        [DataMember]
        public Consts.NODE_TYPE nodeType { get; set; }
        [DataMember]
        public IList<ICondicao> condicoes { get; set; }

        [DataMember]
        public string formula { get; set; }

        public Node(Consts.NODE_TYPE nodeType)
        {
            this.nodeType = nodeType;
            condicoes = new List<ICondicao>();
        }

        public Node(Config config, string formula)
        {
            condicoes = new List<ICondicao>();
            formula = formula.Trim();
            this.formula = formula;
            if (!IsComplexFormula(formula))
            {
                if (formula.StartsWith("!"))
                {
                    nodeType = Consts.NODE_TYPE.NOT;
                }
                else
                {
                    nodeType = Consts.NODE_TYPE.AND;
                }
                AddCondicao(new Condicao(config, formula));
            }
            else
            {
                string[] elementos = Util.SeparaEmElementos(formula);
                if (elementos[1] == "&&") nodeType = Consts.NODE_TYPE.AND;
                if (elementos[1] == "||") nodeType = Consts.NODE_TYPE.AND;
                AddCondicao(new Node(config, elementos[0]));
                AddCondicao(new Node(config, elementos[2]));
            }
        }

        public override string ToString()
        {
            return formula;
        }

        public void AddCondicao(ICondicao condicao)
        {
            condicoes.Add(condicao);

        }

        private bool IsComplexFormula(string formula)
        {
            return formula.Contains("||") || formula.Contains("&&");
        }

        public bool VerificaCondicao(ativos.Candle candle, TradeSystem ts)
        {
            bool ret = true;
            bool firstLoop = true;

            foreach (ICondicao cond in condicoes)
            {
                bool b = cond.VerificaCondicao(candle, ts);
                if (firstLoop)
                {
                    ret = b;
                    firstLoop = false;
                }
                if (nodeType == Consts.NODE_TYPE.AND || nodeType == Consts.NODE_TYPE.NOT)
                {
                    ret = ret && b;
                }
                if (nodeType == Consts.NODE_TYPE.OR)
                {
                    ret = ret || b;
                }
            }

            if (nodeType == Consts.NODE_TYPE.NOT)
            {
                ret = !ret;
            }

            return ret;
        }
    }
}
