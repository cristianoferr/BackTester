
using System;
using System.Runtime.Serialization;
namespace Backtester.backend.model.system.condicoes
{
    [DataContract]
    public class Variavel
    {

        [DataMember]
        public float vlrInicial { get;  set; }
        [DataMember]
        public int steps { get; set; }
        [DataMember]
        public float vlrFinal { get;  set; }
        [DataMember]
        public float vlrAtual { get;  set; }

        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string descricao { get;  set; }
        [DataMember]
        public int uniqueID { get; private set; }

        public static int countVars = 0;

        public Variavel(string name, float vlrInicial, int steps, float vlrFinal)
        {
            if (steps == 0) steps = 1;
            this.steps = steps;
            //step = Math.Abs(vlrFinal - vlrInicial) / steps;
            countVars++;
            uniqueID = countVars;
            this.vlrFinal = vlrFinal;
            this.name = name;
            //if (vlrFinal < vlrInicial) step = -step;
            //if (step == 0) step = 1;
            //this.step = step;
            this.vlrInicial = vlrInicial;
            this.vlrAtual = this.vlrInicial;
        }
        public void reset()
        {
            this.vlrAtual = this.vlrInicial;
        }


        private float step{
            get{
                return Math.Abs(vlrFinal - vlrInicial) / steps;
            }
        }

        public void next()
        {
            vlrAtual += step;
            if (vlrAtual > vlrFinal) vlrAtual = vlrFinal;
        }

        public bool hasEnded()
        {
            return (vlrAtual == vlrFinal);
        }

        public override string ToString()
        {
            if (name == null)
            {
                return "UNDEF.";
            }
            return name;
        }

    }
}
