﻿using System;
using System.Collections.Generic;

namespace Backtester.backend.model.ativos
{
    public class Ativo
    {
        public Dictionary<Periodo, Candle> candles { get; set; }

        public Ativo(FacadeBacktester facade, string name, int loteMin)
        {
            this.name = name;
            this.facade = facade;
            candles = new Dictionary<Periodo, Candle>();
            this.loteMin = loteMin;
        }

        public string name { get; set; }

        public override string ToString()
        {
            return name;
        }

        public void AddCandle(Candle candle)
        {
            candles.Add(candle.periodo, candle);
        }

        public Candle GetCandle(Periodo periodo)
        {
            if (!candles.ContainsKey(periodo))
            {
                return null;
            }
            return candles[periodo];
        }

        public int loteMin { get; set; }


        public FacadeBacktester facade { get; set; }

        public Candle firstCandle { get; set; }


        internal void ClearData()
        {
            foreach (Periodo per in candles.Keys)
            {
                Candle candle = candles[per];
                candle.ClearData();
            }
        }

        public void DrawIn(System.Drawing.Graphics graphics)
        {
            throw new NotImplementedException();
        }
    }
}
