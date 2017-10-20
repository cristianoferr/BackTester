
using Backtester.backend.model.ativos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UsoComum;
namespace Backtester.backend.DataManager
{
    public class DataLoader
    {
        private FacadeBacktester facade;

        public DataLoader(FacadeBacktester facadeBacktester)
        {
            this.facade = facadeBacktester;
        }
        bool minDateSet = false;
        DateTime minDate;


        internal bool LoadAtivo(Ativo ativo, Consts.PERIODO_ACAO periodoAcao, string fileName, Consts.TIPO_CARGA_ATIVOS tipoCarga)
        {
            fileName = fileName.Replace("%5e", "_");
            CarregaDadosWebSeNaoExisteArquivo(ativo, fileName, tipoCarga);

            using (StreamReader r = new StreamReader(fileName))
            {
                ativo.candles.Clear();
                string json = "[" + r.ReadToEnd() + "]";
                List<CargaADVFN> items = JsonConvert.DeserializeObject<List<CargaADVFN>>(json);
                foreach (CargaADVFN item in items)
                {
                    if (item.s != "ok")
                    {
                        return false;
                    }
                    ProcessaDia(ativo, periodoAcao, item);
                }
                return true;
            }

        }

        private void ProcessaDia(Ativo ativo, Consts.PERIODO_ACAO periodoAcao, CargaADVFN item)
        {
            Candle candleAnterior = null;
            Periodo periodoAnterior = null;
            //i=0 está bugado...
            
            for (int i = item.t.Count - 1; i > 0; i--)
            {
                //DataDTO data = item.data[i];
                DateTime data = Utils.UnixTimeStampToDateTime(item.t[i]);
                if (!minDateSet)
                {
                    minDate = data;
                    minDateSet = true;
                }
                if (data.CompareTo(minDate)>=0)
                {
                    if (ativo.name.Contains("USIM5") && data.Month == 3 && data.Year == 2016 && data.Day>27)
                    {
                        Console.WriteLine("beep");
                    }

                        if (periodoAcao == Consts.PERIODO_ACAO.DIARIO)
                    {
                        ProcessaPeriodoDiario(ativo, item, ref candleAnterior, ref periodoAnterior, i, data);
                    }
                    if (periodoAcao == Consts.PERIODO_ACAO.SEMANAL)
                    {
                        ProcessaPeriodoSemanal(ativo, item, ref candleAnterior, ref periodoAnterior, i, data);
                    }
                }
            }
        }

        private static void CarregaDadosWebSeNaoExisteArquivo(Ativo ativo, string fileName, Consts.TIPO_CARGA_ATIVOS tipoCarga)
        {
            //sempre carrego quando é dado atual...
            if (!File.Exists(fileName) || tipoCarga == Consts.TIPO_CARGA_ATIVOS.DADOS_ATUAIS)
            {
                Utils.Info("Ativo " + ativo.name + " não encontrado... carregando via serviço...");
                AcaoService.RequestData(ativo.name, fileName, tipoCarga);
            }
        }

        private void ProcessaPeriodoSemanal(Ativo ativo, CargaADVFN item, ref Candle candleAnterior, ref Periodo periodoAnterior, int i, DateTime data)
        {
            Periodo periodo = facade.GetPeriodo(data, Consts.PERIODO_ACAO.SEMANAL);
            Candle candle = ativo.GetCandle(periodo);
            if (candle == null)
            {

                candle = new Candle(periodo, ativo, item.o[i], item.c[i], item.h[i], item.l[i], item.v[i]);
                ProcessaCandle(ativo, ref candleAnterior, ref periodoAnterior, candle);
            }
            else
            {
                candle.AddData(item.o[i], item.c[i], item.h[i], item.l[i], item.v[i]);
            }
        }

        private void ProcessaPeriodoDiario(Ativo ativo, CargaADVFN item, ref Candle candleAnterior, ref Periodo periodoAnterior, int i, DateTime data)
        {
            Periodo periodo = facade.GetPeriodo(data);
            if (ativo.GetCandle(periodo) == null)
            {

                Candle candle = new Candle(periodo, ativo, item.o[i], item.c[i], item.h[i], item.l[i], item.v[i]);
                ProcessaCandle(ativo, ref candleAnterior, ref periodoAnterior, candle);
            }
        }

        private void ProcessaCandle(Ativo ativo, ref Candle candleAnterior, ref Periodo periodoAnterior, Candle candle)
        {
            candle.candleAnterior = candleAnterior;
            //LimitaCandle(candle, 50);
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

        //tem alguns dados que parecem com problema... 
        private void LimitaCandle(Candle candle, int percent)
        {
            Candle candleAnterior = candle.candleAnterior;
            if (candleAnterior != null)
            {
                if (PercentDif(candle.GetValor(FormulaManager.LOW), candleAnterior.GetValor(FormulaManager.LOW)) > percent ||
                    PercentDif(candle.GetValor(FormulaManager.HIGH), candleAnterior.GetValor(FormulaManager.HIGH)) > percent ||
                    PercentDif(candle.GetValor(FormulaManager.CLOSE), candleAnterior.GetValor(FormulaManager.CLOSE)) > percent ||
                        PercentDif(candle.GetValor(FormulaManager.OPEN), candleAnterior.GetValor(FormulaManager.OPEN)) > percent)
                {
                    candle.SetValor(FormulaManager.LOW, candleAnterior.GetValor(FormulaManager.LOW));
                    candle.SetValor(FormulaManager.HIGH, candleAnterior.GetValor(FormulaManager.HIGH));
                    candle.SetValor(FormulaManager.CLOSE, candleAnterior.GetValor(FormulaManager.CLOSE));
                    candle.SetValor(FormulaManager.OPEN, candleAnterior.GetValor(FormulaManager.OPEN));
                }

            }
        }

        private float PercentDif(float p1, float p2)
        {
            float ratio = (1 - p1 / p2) * 100f;
            return Math.Abs(ratio);
        }
    }
}
