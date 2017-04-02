
using System;
using System.Collections.Generic;
namespace Backtester.backend.model.ativos
{
    public class Periodo
    {
        public IList<Candle> candles { get; set; }
        public Periodo(string data)
        {
            this.data = data;
            this.date = DateTime.Parse(data);
            candles = new List<Candle>();
        }


        public string data { get; set; }

        public void AddCandle(Candle candle)
        {
            candles.Add(candle);
        }

        public override string ToString()
        {
            return data;
        }

        internal int GetDiferenca(Periodo per)
        {
            return Math.Abs(per.idPeriodo - idPeriodo);
        }

        public int idPeriodo { get; set; }

        public Periodo periodoAnterior { get; set; }

        public Periodo proximoPeriodo { get; set; }


        internal string GetMes()
        {
            return date.Month.ToString();
        }

        public DateTime date { get; set; }
    }
}
