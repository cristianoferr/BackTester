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

        internal void MergeWith(SubDado subDado)
        {
            nTradesStopados = (nTradesStopados + subDado.nTradesStopados) / 2;
            todosTrades.MergeWith(subDado.todosTrades);
            tradesGanhos.MergeWith(subDado.tradesGanhos);
            tradesPerdidos.MergeWith(subDado.tradesPerdidos);
        }

        public void addOperacao(Operacao oper)
        {
            addOperacao(oper.GetDif(), oper.getQtdPeriodosOper(), oper.stopado);
        }

        public void addOperacao(float dif, int dias, bool isStopado)
        {
            todosTrades.addDado(dias, dif, isStopado);

            if (dif > 0)
            {
                tradesGanhos.addDado(dias, dif, isStopado);
            }
            else
            {
                tradesPerdidos.addDado(dias, dif, isStopado);

            }
            if (isStopado) nTradesStopados++;
        }

        public void print(String txt)
        {
            Utils.println("------------------" + txt + "--------------------------------");
            string percAcerto = Utils.FormatCurrency(this.percAcerto);
            string winLossRatio = Utils.FormatCurrency(this.winLossRatio);
            Utils.println("DIF: " + Utils.FormatCurrency(todosTrades.total) + " %Acerto:" + percAcerto + "%   $W/$L:" + winLossRatio + " Stops:" + nTradesStopados);
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
                float nTrades = todosTrades.nTrades;
                if (nTrades == 0) return 0;
                return (Math.Abs((float)tradesGanhos.nTrades / nTrades)) * 100;
            }
        }

        public float winLossRatio
        {
            get
            {
                float total = tradesPerdidos.total;
                if (total == 0) return 0;
                return Math.Abs(tradesGanhos.total / total);
            }
        }

        public float totalGanho
        {
            get
            {
                return tradesGanhos.total;
            }
        }
        public float totalPerdido
        {
            get
            {
                return tradesPerdidos.total;
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
