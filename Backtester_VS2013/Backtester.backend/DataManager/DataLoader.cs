
using Backtester.backend.model.ativos;
using Newtonsoft.Json;
using System;
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


        internal void LoadAtivo(Ativo ativo, Consts.PERIODO_ACAO periodoAcao, string fileName)
        {
            if (!File.Exists(fileName))
            {
                Util.Info("Ativo " + ativo.name + " não encontrado... carregando via serviço...");
                AcaoService.RequestData(ativo.name, fileName);
            }
            using (StreamReader r = new StreamReader(fileName))
            {
                ativo.candles.Clear();
                string json = "[" + r.ReadToEnd() + "]";
                List<CargaADVFN> items = JsonConvert.DeserializeObject<List<CargaADVFN>>(json);
                foreach (CargaADVFN item in items)
                {
                    Candle candleAnterior = null;
                    Periodo periodoAnterior = null;
                    //i=0 está bugado...
                    for (int i = item.t.Count - 1; i > 0; i--)
                    {
                        //DataDTO data = item.data[i];
                        DateTime data = Util.UnixTimeStampToDateTime(item.t[i]);

                        Periodo periodo = facade.GetPeriodo(data.ToShortDateString());
                        if (ativo.GetCandle(periodo) == null)
                        {

                            Candle candle = new Candle(periodo, ativo, item.o[i], item.c[i], item.h[i], item.l[i], item.v[i]);
                            candle.candleAnterior = candleAnterior;
                            candle.periodo.AddCandle(candle);
                            if (candle.periodo.periodoAnterior == null && periodoAnterior != null)
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
}
