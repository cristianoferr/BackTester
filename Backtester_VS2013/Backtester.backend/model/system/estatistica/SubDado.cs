using Backtester.backend.model.system.condicoes;
using System;
using UsoComum;

namespace Backtester.backend.model.system.estatistica
{
    public class SubDado
    {
        int nTradesStopados;

        public SubSubDado todosTrades { get; private set; }
        public SubSubDado tradesGanhos { get; private set; }
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
                return (Math.Abs((float)tradesGanhos.getnTrades() / (float)todosTrades.getnTrades())) * 100;
            }
        }

        public float winLossRatio
        {
            get
            {
                return Math.Abs(tradesGanhos.getTotal() / tradesPerdidos.getTotal());
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
