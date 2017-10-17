using System;
using Backtester.backend.model.formulas;

namespace Backtester.backend.model.system.condicoes
{
    public class Stop
    {
        private TradeSystem tradeSystem;
        private float vlrStop;
        private formulas.Formula stopCalc;
        public float stopInicial { get; private set; }
        public float stopAtual {get;set;}


        public Stop(TradeSystem tradeSystem, int sentido, float stopInicial)
        {
            this.tradeSystem = tradeSystem;
            this.sentido = sentido;
            stopAtual = 0;
            this.stopInicial = stopInicial;
            stopAtual = stopInicial;
        }

        public Stop(TradeSystem tradeSystem, int sentido, float stopInicial, Formula stopCalc)
            : this(tradeSystem, sentido, stopInicial)
        {
            this.stopCalc = stopCalc;
            stopAtual = stopInicial;
        }

        public float CalcStopCandle(ativos.Candle candle)
        {
            if (stopCalc == null) return stopInicial;
            float v = candle.GetValor(stopCalc);
            v = CalcValorStop(v, sentido, tradeSystem.stopGapPerc);
            return v;
        }

            public float CalcStop(ativos.Candle candle)
        {
            if (stopCalc == null) return stopInicial;
            float v = CalcStopCandle(candle);
            //	System.out.println("StopInicial: "+stopInicial+" stopMovel:"+v+" "+(v>stopInicial));
            //Caso o valor da formula seja maior que o stopInicial então retorna o valor
            if (sentido > 0)
            {
                if (v > stopAtual) stopAtual = v;
            }
            else
            {
                if (v < stopAtual) stopAtual = v;
            }

            return stopAtual;
        }

        public int sentido { get; set; }

        public static float CalcValorStop(float valor, int direcao, float stopGapPerc)
        {
            if (direcao == 1)
            {
                valor *= (1f - stopGapPerc / 100f);
            } else
            {
                valor*= (1f + stopGapPerc / 100f);

            }
            if (float.IsNaN(valor)) valor = 0;
            return valor;
        }
    }
}
