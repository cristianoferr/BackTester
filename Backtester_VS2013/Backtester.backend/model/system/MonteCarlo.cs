using Backtester.backend.model.system.estatistica;
using System.Collections.Generic;

namespace Backtester.backend.model.system
{
    public class MonteCarlo
    {
        IList<Estatistica> estatisticas;
        string name;
        Estatistica global;
        public MonteCarlo(string name)
        {
            this.name = name;
            estatisticas = new List<Estatistica>();
            global = new Estatistica(0);
        }

        public void addEstatistica(Estatistica stat)
        {
            estatisticas.Add(stat);

        }

        /*
         * Um método para ordenar as estatísticas de acordo com um parâmetro
         */
        public void ordernaEstatisticas(Consts.OrdemEstatistica ordem)
        {
            for (int i = 0; i < estatisticas.Count - 1; i++)
            {

                for (int j = i + 1; j < estatisticas.Count; j++)
                {
                    Estatistica statI = estatisticas[i];
                    float vI = 0;
                    Estatistica statJ = estatisticas[j];
                    float vJ = 0;
                    if (ordem == Consts.OrdemEstatistica.CAPITAL_FINAL) vI = statI.getCapitalFinal();
                    if (ordem == Consts.OrdemEstatistica.CAPITAL_FINAL) vJ = statJ.getCapitalFinal();
                    if (vI > vJ)
                    {
                        estatisticas[j] = statI;
                        estatisticas[i] = statJ;
                    }
                }

            }
        }

        public void printEstatisticas()
        {
            for (int i = 0; i < estatisticas.Count; i++)
            {
                Estatistica stat = estatisticas[i];
                Util.println(stat.getDesc() + "=>" + stat.getPerformance());
            }
        }

        public void printPerformance(string s)
        {
            Util.println(s + ": " + name + "=>" + global.getMaxMinCapital() + " Trades:" + global.getGeral().getAmbasPontas().getTodosTrades().getnTrades());
        }

        public void update()
        {
            float capitalFinal = 0;
            float maxCapital = 0;
            float minCapital = -1;
            float nTrades = 0;
            float nDias = 0;
            float dif = 0;

            for (int i = 0; i < estatisticas.Count; i++)
            {
                Estatistica stat = estatisticas[i];

                capitalFinal += stat.getCapitalFinal();
                if (stat.getCapitalFinal() > maxCapital)
                    maxCapital = stat.getCapitalFinal();
                if ((minCapital == -1) || (stat.getCapitalFinal() < minCapital))
                    minCapital = stat.getCapitalFinal();
                nTrades += stat.getGeral().getAmbasPontas().getTodosTrades().getnTrades();
                dif += stat.getGeral().getAmbasPontas().getTodosTrades().getTotal();

                global.getGeral().getAmbasPontas().addOperacao(stat.getGeral().getAmbasPontas().getTodosTrades().getTotal(), stat.getGeral().getAmbasPontas().getTodosTrades().getnTrades(), false);

            }

            nTrades /= estatisticas.Count;
            dif /= estatisticas.Count;
            capitalFinal /= estatisticas.Count;
            global.setCapitalFinal(capitalFinal);
            global.setMaxCapital(maxCapital);
            global.setMinCapital(minCapital);


        }

        public float getCapitalFinal()
        {
            return global.getCapitalFinal();
        }

        public float getMaxCapital()
        {
            return global.getMaxCapital();
        }

        public float getMinCapital()
        {
            return global.getMinCapital();
        }
    }
}
