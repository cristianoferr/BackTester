using Backtester.backend.model.system.condicoes;
using System;

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
            Util.println("------------------" + txt + "--------------------------------");
            Util.println("DIF: " + Util.FormatCurrency(todosTrades.getTotal()) + " %Acerto:" + Util.FormatCurrency((Math.Abs((float)tradesGanhos.getnTrades() / (float)todosTrades.getnTrades())) * 100) + "%   $W/$L:" + Util.FormatCurrency((Math.Abs(tradesGanhos.getTotal() / tradesPerdidos.getTotal()) - 1) * 100) + "% Stops:" + nTradesStopados);
            todosTrades.print("TODOS:     ");
            tradesGanhos.print("GANHADORES:");
            tradesPerdidos.print("PERDEDORES:");
        }

        public int getnTradesStopados()
        {
            return nTradesStopados;
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
