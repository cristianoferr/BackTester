using System;
using System.Runtime.Serialization;

namespace Backtester.backend.model.system.estatistica
{
    [DataContract]
    public class SubSubDado
    {
        [DataMember]
        public int nTrades {get;private set;}
        [DataMember]
        public int nTradesStopados {get;private set;}
        [DataMember]
        public int maxDias {get;private set;}
        [DataMember]
        public int minDias{get;private set;} 
        [DataMember]
        public int totDias {get;private set;}

        [DataMember]
        public float avgDias { get; private set; }
        [DataMember]
        public float total {get;private set;}
        [DataMember]
        public float maxTrade { get; private set; }
        [DataMember]
        public float minTrade  {get;private set;}

        public SubSubDado()
        {
            nTrades = 0;
            nTradesStopados = 0;
            maxDias = 0;
            minDias = 0;
            totDias = 0;
            avgDias=0;
            total =0;
            maxTrade = 0;
            minTrade = 0;
        }

        internal void MergeWith(SubSubDado dado)
        {
            nTrades = (nTrades + dado.nTrades) / 2;
            nTradesStopados = (nTradesStopados + dado.nTradesStopados) / 2;
            maxDias = (maxDias + dado.maxDias) / 2;
            minDias = (minDias + dado.minDias) / 2;
            totDias = (totDias + dado.totDias) / 2;
            avgDias = (avgDias + dado.avgDias) / 2;
            total = (total + dado.total) / 2;
            maxTrade = (maxTrade + dado.maxTrade) / 2;
            minTrade = (minTrade + dado.minTrade) / 2;
        }

        public void addDado(int dias, float dif, bool isStopado)
        {
            nTrades++;
            if (isStopado)
            {
                nTradesStopados++;
            }

            if (dif > maxTrade) maxTrade = dif;
            if (minTrade == 0 || dif < minTrade) minTrade = dif;

            totDias += dias;
            if (dias > maxDias) maxDias = dias;
            if (dias < minDias || minDias==0) minDias = dias;
            avgDias = totDias / nTrades;
            total += dif;
        }

        public void print(String tx)
        {
            //  UsoComum.Utils.println(tx + ":  Trades:" + nTrades + " Total:" + UsoComum.Utils.FormatCurrency(total) + " Média:" + UsoComum.Utils.FormatCurrency(total / nTrades));
            //   UsoComum.Utils.println(tx + ":  Maior Resultado: " + UsoComum.Utils.FormatCurrency(maxTrade) + " Menor Resultado: " + UsoComum.Utils.FormatCurrency(minTrade));
            //  UsoComum.Utils.println(tx + ":  Tempo Médio: " + UsoComum.Utils.FormatCurrency(avgDias) + " Maior Tempo:" + maxDias + " Menor Tempo: " + minDias);
        }

    }
}
