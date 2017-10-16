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
            geral=new DadoEstatistico();
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
            geral.print();
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
            return "CapitalFinal:" + Utils.FormatCurrency(capitalFinal) + " Dif:" + Utils.FormatCurrency(geral.getAmbasPontas().todosTrades.total) + " GrossWin:" + Utils.FormatCurrency(geral.getAmbasPontas().tradesGanhos.total) + " GrossLoss:" + Utils.FormatCurrency(geral.getAmbasPontas().tradesPerdidos.total);
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



        public void MergeWith(Estatistica estatistica)
        {
            maxCapital = (maxCapital + estatistica.maxCapital) / 2;
            minCapital = (minCapital + estatistica.minCapital) / 2;
            capitalFinal = (capitalFinal + estatistica.capitalFinal) / 2;
            geral.MergeWith(estatistica.geral);

        }
        

        
    }
}
