using Backtester.backend.model.ativos;
using System.Runtime.Serialization;

namespace Backtester.backend.model.system.condicoes
{
    [DataContract]
    public class CondicaoComplexa : ICondicao
    {

        string formula_ = "";
        [DataMember]
        public string formula
        {
            get
            {
                return formula_;
            }
            set
            {
                formula_ = value;
                ChangeCondicao(value);
            }
        }
        [DataMember]
        public string descricao { get; set; }

        public Config config { get; set; }

        public Node topNode { get; set; }

        public CondicaoComplexa()
        {
        }

        public CondicaoComplexa(Config config)
            : this()
        {
            this.config = config;
        }

        public CondicaoComplexa(Config config, string formula)
            : this(config)
        {
            ChangeCondicao(formula);
        }

        public void ChangeCondicao(string formula)
        {
            this.formula_ = formula;
            topNode = new Node(config, formula);
        }


        public override string ToString()
        {
            return formula;
        }


        public bool VerificaCondicao(Candle candle, TradeSystem ts)
        {
            return topNode.VerificaCondicao(candle, ts);
        }


    }
}
