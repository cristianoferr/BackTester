
using Backtester.backend.model.system.condicoes;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System;

namespace Backtester.backend.model
{
    [DataContract]
    public class VariavelManager
    {
        [DataMember]
        public IList<Variavel> variaveis { get; private set; }

        public VariavelManager()
        {
            variaveis = new List<Variavel>();
        }


        /*  internal float GetVariavelValue(string n)
          {
              return GetVariavelValue(int.Parse(n));
          }
          public float GetVariavelValue(int id)
          {
              return GetVariavel(id).vlrAtual;
          }*/
        public Variavel GetVariavel(string name)
        {
            name = name.ToUpper();
            return variaveis.Where(x => x.name == name).FirstOrDefault();
        }

        public Variavel GetVariavel(string name, string descricao, float vlrInicial, int steps, float vlrFinal)
        {
            name = name.ToUpper();
            Variavel var = GetVariavel(name);
            if (var == null)
            {
                var = new Variavel(name, vlrInicial, steps, vlrFinal);
                var.descricao = descricao;
                variaveis.Add(var);
            }
            return var;
        }

        public int Count { get { return variaveis.Count; } }

        internal Variavel GetVariavel(int i)
        {
            return variaveis[i];
        }

        public string ReplaceVariavel(string txt)
        {
            txt = txt.ToUpper() + " ";
            while (txt.Contains("%"))
            {
                int posInicial = txt.IndexOf("%");
                int posFinal = txt.IndexOf("%", posInicial + 1);
                string var = txt.Substring(posInicial + 1, posFinal - posInicial - 1);


                string varValue = GetVariavel(var).vlrAtual.ToString();
                if (varValue.EndsWith(".0")) varValue = varValue.Replace(".0", "");

                txt = txt.Replace("%" + var + "%", varValue + "");
            }
            return txt.Trim();
        }

        public void Clear()
        {
            variaveis.Clear();
        }

        public void LoadVars(string vars)
        {
            string[] arrVars = vars.Split('(');
            foreach (string v in arrVars)
            {
                if (v.Contains(":"))
                {
                    string v1 = v.Replace("(", "").Replace(")", "");
                    string[] arrVar = v1.Split(':');
                    if (arrVar[1].Contains(" v"))
                    {
                        arrVar[1] = arrVar[1].Substring(0, arrVar[1].IndexOf(" v"));
                    }
                    GetVariavel(arrVar[0]).vlrAtual = float.Parse(arrVar[1]);
                }
            }
        }

        public void SetVariavel(string var, float value)
        {
            GetVariavel(var).vlrAtual = value;
        }
    }
}
