
using Backtester.backend.model.system.condicoes;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
namespace Backtester.backend.model
{
    [DataContract]
    public class VariavelManager
    {
        [DataMember]
        IList<Variavel> variaveis { get; private set; }

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

        public Variavel GetVariavel(string name, float vlrInicial, float step, float vlrFinal)
        {
            name = name.ToUpper();
            Variavel var = GetVariavel(name);
            if (var == null)
            {
                var = new Variavel(name, vlrInicial, step, vlrFinal);
                variaveis.Add(var);
            }
            return var;
        }

        public int Count { get { return variaveis.Count; } }

        internal Variavel GetVariavel(int i)
        {
            return variaveis[i];
        }
    }
}
