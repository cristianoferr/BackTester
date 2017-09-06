using Backtester.backend.model.ativos;
using Backtester.backend.model.system.condicoes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UsoComum;

namespace Backtester.backend.model.system.estatistica
{
    [DataContract]
    public class Estatistica
    {
        [DataMember]
        public DadoEstatistico geral;
        [DataMember]
        public float capitalInicial, maxCapital, minCapital;
        [DataMember]
        public float capitalFinal { get; set; }
        public Dictionary<Periodo, DadoEstatistico> estatisticasDiarias { get; private set; }
        [DataMember]
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
            capitalFinal=capital;
            if (capital > maxCapital) maxCapital = capital;
            if (capital < minCapital) minCapital = capital;
            DadoEstatistico stat = GetEstatisticaDia(periodo);
            stat.atualizaDia(capital);

        }

        public void Print()
        {

            Utils.println("-------------------------------------------------------------");
            getGeral().print();
            Utils.println("------------------Estatistica--------------------------------");
            Utils.println(getPerformance());
            Utils.println("Max Capital:" + Utils.FormatCurrency(maxCapital) + " Menor Capital:" + Utils.FormatCurrency(minCapital));
            /*float wl=0;
            if (geral.getTodosTrades().nTradesWin!=0) wl=((float)geral.getTodosTrades().nTradesWin/(float)geral.getTodosTrades().nTrades);
            System.out.println("Trades:"+geral.getTodosTrades().nTrades+" Winners:"+geral.getTodosTrades().nTradesWin+" Losers:"+geral.getTodosTrades().nTradesLoss+" W/L:"+formatCurrency((wl)*100)+"% Stops:"+geral.getTodosTrades().nTradesStopados);
            System.out.println("AvgWin:"+formatCurrency(geral.getTodosTrades().totalGanho/geral.getTodosTrades().nTradesWin)+" AvgLoss:"+formatCurrency(geral.getTodosTrades().totalPerdido/geral.getTodosTrades().nTradesLoss)+" AvgTrade:"+formatCurrency(geral.getTodosTrades().total/geral.getTodosTrades().nTrades));
            System.out.println("Ponta Comprada:"+geral.getCompras().nTrades+" Ponta Vendida:"+geral.getVendas().nTrades);*/
        }

        public String getPerformance()
        {
            return "CapitalFinal:" + Utils.FormatCurrency(capitalFinal) + " Dif:" + Utils.FormatCurrency(getGeral().getAmbasPontas().todosTrades.getTotal()) + " GrossWin:" + Utils.FormatCurrency(getGeral().getAmbasPontas().tradesGanhos.getTotal()) + " GrossLoss:" + Utils.FormatCurrency(getGeral().getAmbasPontas().tradesPerdidos.getTotal());
        }

        public String getMaxMinCapital()
        {
            return "CapitalFinal:" + Utils.FormatCurrency(capitalFinal) + " Max:" + Utils.FormatCurrency(maxCapital) + " Min:" + Utils.FormatCurrency(minCapital);
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
