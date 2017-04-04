using Backtester.backend.model.ativos;
using System.Runtime.Serialization;
using UsoComum;

namespace Backtester.backend.model.system.condicoes
{
    [DataContract]
    public class Condicao : ICondicao
    {

        [DataMember]
        public string cond1 { get; set; }


        [DataMember]
        public string cond2 { get; set; }

        [DataMember]
        public ConstsComuns.Operador operador { get; set; }

        public Config config { get; set; }

        public Condicao()
        {
        }

        public Condicao(Config config)
            : this()
        {
            this.config = config;
        }

        public Condicao(Config config, string formula)
            : this(config)
        {
            cond1 = "";
            cond2 = "";
            string oper = "";
            int t = 1;
            for (int i = 0; i < formula.Length; i++)
            {
                if (formula[i] == '>' || formula[i] == '<' || formula[i] == '!' || formula[i] == '=')
                {
                    t = 2;
                    oper = oper + formula[i];
                }
                else
                {
                    if (t == 1)
                    {
                        cond1 = cond1 + formula[i];
                    }
                    if (t == 2)
                    {
                        cond2 = cond2 + formula[i];
                    }
                }
            }
            operador = Utils.ConverteOperador(oper);
        }







        public virtual bool VerificaCondicao(Candle candle, TradeSystem ts)
        {
            //Se cond1 e cond2 forem null é porque é uma condição pai, logo não há o que verificar (apenas os filhos) 
            if ((cond1 == null) && (cond2 == null)) return true;

            float c1 = candle.GetValor(ts.vm.ReplaceVariavel(cond1));
            float c2 = candle.GetValor(ts.vm.ReplaceVariavel(cond2));
            return VerificaCondicao(c1, c2);
        }

        private bool VerificaCondicao(float c1, float c2)
        {
            if (operador == ConstsComuns.Operador.DIFFERENT) return c1 != c2;
            if (operador == ConstsComuns.Operador.EQUAL) return c1 == c2;
            if (operador == ConstsComuns.Operador.GREATER) return c1 > c2;
            if (operador == ConstsComuns.Operador.GREATER_EQ) return c1 >= c2;
            if (operador == ConstsComuns.Operador.LOWER) return c1 < c2;
            if (operador == ConstsComuns.Operador.LOWER_EQ) return c1 <= c2;
            return false;
        }



    }
}
