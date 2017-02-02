
using Backtester.backend.model.ativos;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
namespace Backtester.backend.DataManager
{
    public class DataLoader
    {
        private FacadeBacktester facade;

        public DataLoader(FacadeBacktester facadeBacktester)
        {
            this.facade = facadeBacktester;
        }

      
        internal void LoadAtivo(Ativo ativo, Consts.PERIODO_ACAO periodo, string fileName)
        {

            using (StreamReader r = new StreamReader(fileName))
            {
                ativo.candles.Clear();
                string json = "[" + r.ReadToEnd() + "]";
                List<CargaDTO> items = JsonConvert.DeserializeObject<List<CargaDTO>>(json);
                foreach (CargaDTO item in items)
                {
                    Candle candleAnterior = null;
                    Periodo periodoAnterior = null;
                    for (int i = 0; i < item.data.Count; i++)
                    {
                        DataDTO data = item.data[i];
                        Candle candle = new Candle(facade.GetPeriodo(data.date), ativo, data.open, data.close, data.high, data.low, data.vol);
                        candle.candleAnterior = candleAnterior;
                        candle.periodo.AddCandle(candle);
                        if (candle.periodo.periodoAnterior == null)
                        {
                            candle.periodo.periodoAnterior = periodoAnterior;
                            periodoAnterior.proximoPeriodo = candle.periodo;
                        }
                        periodoAnterior = candle.periodo;
                        ativo.AddCandle(candle);
                        if (candleAnterior == null) ativo.firstCandle = candle;
                        if (candleAnterior != null) candleAnterior.proximoCandle = candle;
                        candleAnterior = candle;
                    }
                }
            }

        }
    }
}
