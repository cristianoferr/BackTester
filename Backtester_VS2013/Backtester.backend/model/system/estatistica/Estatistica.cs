using Backtester.backend.model.ativos;
using Backtester.backend.model.system.condicoes;
using System;
using System.Collections.Generic;

namespace Backtester.backend.model.system.estatistica
{
    public class Estatistica
    {
        private DadoEstatistico geral;
        float capitalInicial, maxCapital, minCapital;
        private float capitalFinal;
        public Dictionary<Periodo, DadoEstatistico> estatisticasDiarias { get; private set; }
        String desc = "";//Descricao do que foi feito

        public Estatistica(float capitalInicial)
        {
            this.capitalInicial = capitalInicial;
            maxCapital = capitalInicial;
            estatisticasDiarias = new Dictionary<Periodo, DadoEstatistico>();
            minCapital = capitalInicial;
            setGeral(new DadoEstatistico());
        }

        public void AddTrade(Operacao oper, Periodo periodo)
        {
            //DadoEstatistico stat=getEstatisticaDia(periodo);
            //stat.addOperacao(oper);
            geral.addOperacao(oper);

        }

        private DadoEstatistico GetEstatisticaDia(Periodo periodo)
        {
            if (estatisticasDiarias.ContainsKey(periodo)) return estatisticasDiarias[periodo];
            DadoEstatistico stat = new DadoEstatistico();
            estatisticasDiarias.Add(periodo, stat);
            return stat;
        }

        public void AtualizaPeriodo(Periodo periodo, float capital)
        {
            setCapitalFinal(capital);
            if (capital > maxCapital) maxCapital = capital;
            if (capital < minCapital) minCapital = capital;
            DadoEstatistico stat = GetEstatisticaDia(periodo);
            stat.atualizaDia(capital);

        }

        public void Print()
        {

            Util.println("-------------------------------------------------------------");
            getGeral().print();
            Util.println("------------------Estatistica--------------------------------");
            Util.println(getPerformance());
            Util.println("Max Capital:" + Util.FormatCurrency(maxCapital) + " Menor Capital:" + Util.FormatCurrency(minCapital));
            /*float wl=0;
            if (geral.getTodosTrades().nTradesWin!=0) wl=((float)geral.getTodosTrades().nTradesWin/(float)geral.getTodosTrades().nTrades);
            System.out.println("Trades:"+geral.getTodosTrades().nTrades+" Winners:"+geral.getTodosTrades().nTradesWin+" Losers:"+geral.getTodosTrades().nTradesLoss+" W/L:"+formatCurrency((wl)*100)+"% Stops:"+geral.getTodosTrades().nTradesStopados);
            System.out.println("AvgWin:"+formatCurrency(geral.getTodosTrades().totalGanho/geral.getTodosTrades().nTradesWin)+" AvgLoss:"+formatCurrency(geral.getTodosTrades().totalPerdido/geral.getTodosTrades().nTradesLoss)+" AvgTrade:"+formatCurrency(geral.getTodosTrades().total/geral.getTodosTrades().nTrades));
            System.out.println("Ponta Comprada:"+geral.getCompras().nTrades+" Ponta Vendida:"+geral.getVendas().nTrades);*/
        }

        public String getPerformance()
        {
            return "CapitalFinal:" + Util.FormatCurrency(getCapitalFinal()) + " Dif:" + Util.FormatCurrency(getGeral().getAmbasPontas().todosTrades.getTotal()) + " GrossWin:" + Util.FormatCurrency(getGeral().getAmbasPontas().tradesGanhos.getTotal()) + " GrossLoss:" + Util.FormatCurrency(getGeral().getAmbasPontas().tradesPerdidos.getTotal());
        }

        public String getMaxMinCapital()
        {
            return "CapitalFinal:" + Util.FormatCurrency(getCapitalFinal()) + " Max:" + Util.FormatCurrency(maxCapital) + " Min:" + Util.FormatCurrency(minCapital);
        }

        public String getDesc()
        {
            return desc;
        }

        public void setDesc(String desc)
        {
            this.desc = desc;
        }

        public void setGeral(DadoEstatistico geral)
        {
            this.geral = geral;
        }

        public DadoEstatistico getGeral()
        {
            return geral;
        }

        public void setCapitalFinal(float capitalFinal)
        {
            this.capitalFinal = capitalFinal;
        }

        public float getCapitalFinal()
        {
            return capitalFinal;
        }

        public float getMaxCapital()
        {
            return maxCapital;
        }

        public float getMinCapital()
        {
            return minCapital;
        }



        public void setMaxCapital(float maxCapital)
        {
            this.maxCapital = maxCapital;
        }

        public void setMinCapital(float minCapital)
        {
            this.minCapital = minCapital;
        }


    }
}
