using Backtester.backend.DataManager;
using Backtester.backend.model.ativos;
using System;

namespace Backtester.backend.model.system.condicoes
{
    public class Operacao
    {

        public Operacao(Carteira carteira, Candle candle, float vlrEntrada, float vlrStop, int qtd, int direcao, String formulaStop)
        {
            direcao = 1;//1=compra / -1=venda
            this.carteira = carteira;
            this.candleInicial = candle;
            stopado = false;
            this.vlrEntrada = vlrEntrada;
            if (formulaStop == "")
                stop = new Stop(carteira.tradeSystem, direcao, vlrStop);
            else
                stop = new Stop(carteira.tradeSystem, direcao, vlrStop, carteira.facade.formulaManager.GetFormula(formulaStop));
            this.qtd = qtd;
            this.direcao = direcao;

        }
        /*
         * Retorna a quantidade de "candles" da operação
         */
        public int getQtdPeriodosOper()
        {
            return candleInicial.periodo.GetDiferenca(candleFinal.periodo);
        }

        public float GetDif()
        {
            return (GetTotalSaida() - GetTotalEntrada()) * direcao;
        }
        /*
         * Essa função verifica se atingiu o stop definido, tanto na compra quanto na venda
         * Se sim, então retorna o valor do stop, lembrando que não 
         * necessariamente pode ser o stop definido pois a ação pode ter aberto em gap.
         * 
         */
        public float atingiuStop(Periodo periodo)
        {
            Candle candle = candleInicial.ativo.GetCandle(periodo);

            float vlrStop = stop.CalcStop(candle);
            //Compra
            if (direcao == 1)
            {
                if (candle.GetValor(FormulaManager.LOW) <= vlrStop)
                {
                    //Se foi gap, então o stop será igual ao preço de abertura
                    if (candle.GetValor(FormulaManager.OPEN) <= vlrStop)
                    {
                        return candle.GetValor(FormulaManager.OPEN);
                    }
                    else
                    {
                        return vlrStop;
                    }
                }
            }
            else
            {
                //Venda
                if (candle.GetValor(FormulaManager.HIGH) >= vlrStop)
                {
                    //Se foi gap, então o stop será igual ao preço de abertura
                    if (candle.GetValor(FormulaManager.OPEN) >= vlrStop)
                    {
                        return candle.GetValor(FormulaManager.OPEN);
                    }
                    else
                    {
                        return vlrStop;
                    }
                }
            }

            return -1;
        }

        //Retorna o valor total 
        public float GetTotalSaida()
        {
            return vlrSaida * qtd;
        }

        public float GetTotalEntrada()
        {
            return vlrEntrada * qtd;
        }

        public float vlrSaida { get; set; }

        public Candle candleFinal { get; set; }

        public float vlrEntrada { get; set; }

        public bool stopado { get; set; }


        public Candle candleInicial { get; set; }

        public Stop stop { get; set; }

        public int direcao { get; set; }

        public int qtd { get; set; }

        public Carteira carteira { get; set; }

    }
}
