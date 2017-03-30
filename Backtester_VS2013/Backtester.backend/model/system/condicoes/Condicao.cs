using Backtester.backend.model.ativos;
using System.Runtime.Serialization;

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
        public Consts.Operador operador { get; set; }

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
            operador = Util.ConverteOperador(oper);
        }





        public string ReplaceVariavel(string txt)
        {
            txt = txt.ToUpper() + " ";
            while (txt.Contains("VAR"))
            {
                int pos = txt.IndexOf("VAR") + 3;
                string n = txt.Substring(pos, pos + 1);
                while (Util.IsNumber(n))
                {
                    pos++;
                    n = n + txt.Substring(pos, pos + 1);
                }
                n = n.Substring(0, n.Length - 1);

                string varValue = config.GetVariavelValue(n) + "";
                //System.out.println("VAR:"+n+"="+varValue);
                if (varValue.EndsWith(".0")) varValue = varValue.Replace(".0", "");

                txt = txt.Replace("VAR" + n, varValue + "");
            }
            return txt.Trim();
        }

        public virtual bool VerificaCondicao(Candle candle)
        {
            //Se cond1 e cond2 forem null é porque é uma condição pai, logo não há o que verificar (apenas os filhos) 
            if ((cond1 == null) && (cond2 == null)) return true;

            float c1 = candle.GetValor(ReplaceVariavel(cond1));
            float c2 = candle.GetValor(ReplaceVariavel(cond2));
            return VerificaCondicao(c1, c2);
        }

        private bool VerificaCondicao(float c1, float c2)
        {
            if (operador == Consts.Operador.DIFFERENT) return c1 != c2;
            if (operador == Consts.Operador.EQUAL) return c1 == c2;
            if (operador == Consts.Operador.GREATER) return c1 > c2;
            if (operador == Consts.Operador.GREATER_EQ) return c1 >= c2;
            if (operador == Consts.Operador.LOWER) return c1 < c2;
            if (operador == Consts.Operador.LOWER_EQ) return c1 <= c2;
            return false;
        }



    }
}
