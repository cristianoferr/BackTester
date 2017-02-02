using System;

namespace Backtester.backend.model.system.condicoes
{
    public class Variavel
    {

        float vlrInicial, step, vlrFinal, vlrAtual;
        public int id { get; private set; }

        public Variavel(int id, float vlrInicial, float step, float vlrFinal)
        {
            this.vlrFinal = vlrFinal;
            this.id = id;
            step = Math.Abs(step);
            if (vlrFinal < vlrInicial) step = -step;
            if (step == 0) step = 1;
            this.step = step;
            this.vlrInicial = vlrInicial;
            this.vlrAtual = this.vlrInicial - step;
        }
        public void reset()
        {
            this.vlrAtual = this.vlrInicial - step;
        }

        public int getSteps()
        {
            return (int)(Math.Abs(vlrFinal - vlrInicial) / step);
        }
        public float getAtual()
        {
            return vlrAtual;
        }

        public float getNext()
        {
            vlrAtual += step;
            if (vlrAtual > vlrFinal) vlrAtual = vlrFinal;
            return vlrAtual;
        }

        public bool hasEnded()
        {
            return (vlrAtual == vlrFinal);
        }
        public float getVlrInicial()
        {
            return vlrInicial;
        }
        public float getStep()
        {
            return step;
        }
        public float getVlrFinal()
        {
            return vlrFinal;
        }
        public float getVlrAtual()
        {
            return vlrAtual;
        }

    }
}
