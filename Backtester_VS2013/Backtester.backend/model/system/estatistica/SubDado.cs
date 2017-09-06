using Backtester.backend.model.system.condicoes;
using System;
using System.Runtime.Serialization;
using UsoComum;

namespace Backtester.backend.model.system.estatistica
{
    [DataContract]
    public class SubDado
    {
        [DataMember]
        int nTradesStopados;

        [DataMember]
        public SubSubDado todosTrades { get; private set; }
        [DataMember]
        public SubSubDado tradesGanhos { get; private set; }
        [DataMember]
        public SubSubDado tradesPerdidos { get; private set; }

        public SubDado()
        {
            todosTrades = new SubSubDado();
            tradesGanhos = new SubSubDado();
            tradesPerdidos = new SubSubDado();
        }

        public void addOperacao(Operacao oper)
        {
            addOperacao(oper.GetDif(), oper.getQtdPeriodosOper(), oper.stopado);
        }

        public void addOperacao(float dif, int dias, bool isStopado)
        {
            todosTrades.addDado(dias, dif);

            if (dif > 0)
            {
                tradesGanhos.addDado(dias, dif);
            }
            else
            {
                tradesPerdidos.addDado(dias, dif);

            }
            if (isStopado) nTradesStopados++;
        }

        public void print(String txt)
        {
            Utils.println("------------------" + txt + "--------------------------------");
            string percAcerto = Utils.FormatCurrency(this.percAcerto);
            string winLossRatio = Utils.FormatCurrency(this.winLossRatio);
            Utils.println("DIF: " + Utils.FormatCurrency(todosTrades.getTotal()) + " %Acerto:" + percAcerto + "%   $W/$L:" + winLossRatio + " Stops:" + nTradesStopados);
            todosTrades.print("TODOS:     ");
            tradesGanhos.print("GANHADORES:");
            tradesPerdidos.print("PERDEDORES:");
        }

        public int getnTradesStopados()
        {
            return nTradesStopados;
        }

        public float percAcerto
        {
            get
            {
                float nTrades = todosTrades.getnTrades();
                if (nTrades == 0) return 0;
                return (Math.Abs((float)tradesGanhos.getnTrades() / nTrades)) * 100;
            }
        }

        public float winLossRatio
        {
            get
            {
                float total = tradesPerdidos.getTotal();
                if (total == 0) return 0;
                return Math.Abs(tradesGanhos.getTotal() / total);
            }
        }

        public float totalGanho
        {
            get
            {
                return tradesGanhos.getTotal();
            }
        }
        public float totalPerdido
        {
            get
            {
                return tradesPerdidos.getTotal();
            }
        }

        public SubSubDado getTodosTrades()
        {
            return todosTrades;
        }

        public SubSubDado getTradesGanhos()
        {
            return tradesGanhos;
        }

        public SubSubDado getTradesPerdidos()
        {
            return tradesPerdidos;
        }

    }
}
