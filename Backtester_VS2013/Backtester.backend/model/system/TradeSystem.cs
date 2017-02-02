
using Backtester.backend.DataManager;
using Backtester.backend.model.ativos;
using System.Runtime.Serialization;
namespace Backtester.backend.model.system
{
    [DataContract]
    public class TradeSystem
    {
        /*A diferença entre stopInicial e stopMovel: 
     a formula stopInicial será usada para determinar o Stop Inicial (dã) e o stopMovel é usado para determinar o stop daquele dia
     E caso o valor do stopMovel seja menor que o stopInicial então o valor retornado será o stopInicial
     */

        public TradeSystem()
        {
            stopMovelC = stopInicialC;
            stopMovelV = stopInicialV;
            stopInicialC = "SUBTRACT(L,MULTIPLY(" + FormulaManager.STDDEV + "(C,10),2))";
            stopInicialV = "SUM(H,MULTIPLY(" + FormulaManager.STDDEV + "(C,10),2))";//"HV(H,6)";
            stopGapPerc = 0f; //Usado para dar algum espaçamento entre a formula e o valor realmente usado
            condicaoEntradaC = new Condicao();
            condicaoEntradaV = new Condicao();
            condicaoSaidaC = new Condicao();
            condicaoSaidaV = new Condicao();
        }



        public string GetStopMovel(int sentido)
        {
            if (sentido > 0) return stopMovelC;
            return stopMovelV;
        }

        public string GetStopInicial(int sentido)
        {
            if (sentido > 0) return stopInicialC;
            return stopInicialV;
        }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string stopInicialC { get; set; }
        [DataMember]
        public string stopInicialV { get; set; }
        [DataMember]
        public float stopGapPerc { get; set; }
        [DataMember]
        public string stopMovelC { get; set; }
        [DataMember]
        public string stopMovelV { get; set; }


        [DataMember]
        public Condicao condicaoEntradaC { get; set; }

        [DataMember]
        public Condicao condicaoSaidaC { get; set; }
        [DataMember]
        public Condicao condicaoEntradaV { get; set; }

        [DataMember]
        public Condicao condicaoSaidaV { get; set; }

        /*
         * Esse método verifica se a entrada foi ativada para o candle atual
         * O metodo retorna onde deve ficar o stop, e se for positivo é compra e negativo é venda
         */
        public float checaCondicaoEntrada(Candle candle, Config config)
        {
            if ((config.flagCompra) && (condicaoEntradaC != null))
                if (condicaoEntradaC.VerificaCondicao(candle))
                    return candle.GetValor(stopInicialC) * (1f - stopGapPerc / 100f);

            if ((config.flagVenda) && (condicaoEntradaV != null))
                if (condicaoEntradaV.VerificaCondicao(candle))
                    return -candle.GetValor(stopInicialV) * (1f + stopGapPerc / 100f);

            return 0;
        }

        public float checaCondicaoSaida(Candle candle, int direcao)
        {
            //SE for compra e ativou a condicao de saida
            if (direcao > 0) if (condicaoSaidaC.VerificaCondicao(candle)) return 1;
            if (direcao < 0) if (condicaoSaidaV.VerificaCondicao(candle)) return 1;
            return 0;
        }


    }
}
