using System;

namespace Backtester.backend.model.system.estatistica
{
    public class SubSubDado
    {
        int nTrades = 0, maxDias = 0, minDias = 9999999, totDias = 0;
        float avgDias, total;
        float maxTrade = 0, minTrade = -1;

        public void addDado(int dias, float dif)
        {
            nTrades++;

            if (Math.Abs(dif) > Math.Abs(maxTrade)) maxTrade = dif;
            if ((minTrade == -1) || (Math.Abs(dif) < Math.Abs(minTrade))) minTrade = dif;

            totDias += dias;
            if (dias > maxDias) maxDias = dias;
            if (dias < minDias) minDias = dias;
            avgDias = totDias / nTrades;
            total += dif;
        }

        public void print(String tx)
        {
            Util.println(tx + ":  Trades:" + nTrades + " Total:" + Util.FormatCurrency(total) + " Média:" + Util.FormatCurrency(total / nTrades));
            Util.println(tx + ":  Maior Resultado: " + Util.FormatCurrency(maxTrade) + " Menor Resultado: " + Util.FormatCurrency(minTrade));
            Util.println(tx + ":  Tempo Médio: " + Util.FormatCurrency(avgDias) + " Maior Tempo:" + maxDias + " Menor Tempo: " + minDias);
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
