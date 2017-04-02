using Backtester.backend.model.system.estatistica;
using System.Collections.Generic;

namespace Backtester.backend.model.system
{
    public class MonteCarlo
    {
        string name;
        Estatistica global;
        public MonteCarlo(string name)
        {
            this.name = name;
            global = new Estatistica(0);
        }

        public void setEstatistica(Estatistica stat)
        {
            global = stat;

        }


        public override string ToString()
        {
            return name;
        }
      

        public void printEstatisticas()
        {
                Util.println(global.getDesc() + "=>" + global.getPerformance());
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

          /*  for (int i = 0; i < estatisticas.Count; i++)
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

            }*/

           /* global.setCapitalFinal(capitalFinal);
            global.setMaxCapital(maxCapital);
            global.setMinCapital(minCapital);*/


        }

        public int qtdTrades { get {
            return global.getGeral().getAmbasPontas().getTodosTrades().getnTrades(); 
        } 
        }

        public float winLossRatio
        {
            get
            {
                return global.getGeral().getAmbasPontas().winLossRatio;
            }
        }

        public float percAcerto
        {
            get
            {
                return global.getGeral().getAmbasPontas().percAcerto;
            }
        }


        public float totalGanho
        {
            get
            {
                return global.getGeral().getAmbasPontas().totalGanho;
            }
        }
        public float totalPerdido
        {
            get
            {
                return global.getGeral().getAmbasPontas().totalPerdido;
            }
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

        public Estatistica getGlobal()
        {
            return global;
        }
    }
}
