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

        public int getnTrades()
        {
            return nTrades;
        }

        public void setnTrades(int nTrades)
        {
            this.nTrades = nTrades;
        }

        public int getMaxDias()
        {
            return maxDias;
        }

        public void setMaxDias(int maxDias)
        {
            this.maxDias = maxDias;
        }

        public int getMinDias()
        {
            return minDias;
        }

        public void setMinDias(int minDias)
        {
            this.minDias = minDias;
        }

        public int getTotDias()
        {
            return totDias;
        }

        public void setTotDias(int totDias)
        {
            this.totDias = totDias;
        }

        public float getAvgDias()
        {
            return avgDias;
        }

        public void setAvgDias(float avgDias)
        {
            this.avgDias = avgDias;
        }

        public float getTotal()
        {
            return total;
        }

        public void setTotal(float total)
        {
            this.total = total;
        }

        public float getMaxTrade()
        {
            return maxTrade;
        }

        public float getMinTrade()
        {
            return minTrade;
        }

    }
}
