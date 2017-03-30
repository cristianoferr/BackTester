using Backtester.backend.model.ativos;
using System.Runtime.Serialization;

namespace Backtester.backend.model.system.condicoes
{
    [DataContract]
    public class CondicaoComplexa : ICondicao
    {

        [DataMember]
        public string formula { get; set; }
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
            this.formula = formula;
            topNode = new Node(config, formula);
        }



        public override string ToString()
        {
            return formula;
        }


        public bool VerificaCondicao(Candle candle)
        {
            return topNode.VerificaCondicao(candle);
        }


    }
}
