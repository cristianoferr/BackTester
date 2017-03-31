
using System;
namespace Backtester.backend.model.system.condicoes
{
    public class Variavel
    {

        public float vlrInicial { get; private set; }
        public float step { get; private set; }
        public float vlrFinal { get; private set; }
        public float vlrAtual { get; private set; }

        public string name { get; private set; }

        public Variavel(string name, float vlrInicial, float step, float vlrFinal)
        {
            this.vlrFinal = vlrFinal;
            this.name = name;
            step = Math.Abs(step);
            if (vlrFinal < vlrInicial) step = -step;
            if (step == 0) step = 1;
            this.step = step;
            this.vlrInicial = vlrInicial;
            this.vlrAtual = this.vlrInicial;
        }
        public void reset()
        {
            this.vlrAtual = this.vlrInicial;
        }

        public int getSteps()
        {
            return (int)(Math.Abs(vlrFinal - vlrInicial) / step);
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


    }
}
