using Backtester.backend.model.ativos;
using Backtester.backend.model.formulas;
using System.Collections.Generic;
using System.Linq;
using System;
using UsoComum;
using Backtester.backend.DataManager;

namespace Backtester.backend.model
{
    public class DataHolder
    {

        public Dictionary<string, Ativo> ativos { get; set; }
        public IList<Periodo> periodos { get; set; }
        FacadeBacktester facade;

        public DataHolder(FacadeBacktester facade)
        {
            this.facade = facade;
            ativos = new Dictionary<string, Ativo>();
            periodos = new List<Periodo>();
        }

        internal void AddAtivo(Ativo ativo)
        {
            ativos.Add(ativo.name, ativo);
        }

        internal void AddFormula(string name, string par)
        {

            Formula f = facade.formulaManager.GetFormula(name, par);
            AddFormula(f);
        }


        public void AddFormula(Formula f)
        {
            if (!f.gravar) return;
            //Utils.Info("Carregando formula : " + f.GetCode());

            /*foreach (Ativo ativo in ativos.Values)
            {
                Candle candle = ativo.firstCandle;

                if (!candle.ContainsFormula(f.GetCode()))
                {
                    while (candle.proximoCandle != candle)
                    {
                        candle.SetValor(f, f.Calc(candle));
                        candle = candle.proximoCandle;
                    }
                }
            }*/
        }


        public Ativo GetAtivo(string name)
        {
            return ativos[name];
        }

        public Ativo GetOrCreateAtivo(string papel, int loteMin)
        {
            Ativo ativo;
            if (!ativos.ContainsKey(papel))
            {
                ativo = new Ativo(facade, papel, loteMin);
                AddAtivo(ativo);
            }
            else
            {
                ativo = GetAtivo(papel);
            }
            return ativo;
        }


        internal Periodo GetPeriodo(string data)
        {
            Periodo periodo = periodos.Where(v => v.data == data).FirstOrDefault();
            if (periodo == null)
            {
                periodo = new Periodo(data);
                periodos.Add(periodo);
                periodo.idPeriodo = periodos.Count;
            }
            return periodo;
        }

        internal IList<Ativo> GetAtivos()
        {
            IList<Ativo> lista = new List<Ativo>();
            foreach (Ativo ativo in ativos.Values)
            {
                lista.Add(ativo);
            }
            return lista;
        }

        internal void RemoveAtivo(Ativo ativo)
        {
            ativos.Remove(ativo.name);
        }

        internal void ClearAtivos()
        {
            ativos.Clear();
            periodos.Clear();
        }

        internal void ClearData()
        {
            foreach (string key in ativos.Keys)
            {
                ativos[key].ClearData();
            }
        }

        internal Ativo GenerateAtivoMock(int seed)
        {
            Ativo ativo = GetOrCreateAtivo("Mock" + seed,100);
            Random rnd = new Random(seed);
            
            float vlr= 10 + rnd.Next(120);
            float vol = 100000 + rnd.Next(100000);
            float percGap = rnd.Next(5);
            float animosity = 0;
            for (int i = 0; i < Consts.QTD_ATIVOS_MOCK; i++)
            {
                Periodo periodo = GetPeriodo(DateTime.Now.AddDays(i).ToShortDateString());
                Candle candle = new Candle(periodo, ativo);
                candle.SetValor(FormulaManager.OPEN, vlr);
                candle.SetValor(FormulaManager.HIGH, vlr);
                candle.SetValor(FormulaManager.LOW, vlr);
                for (int j = 0; j < 10; j++)
                {
                    vlr=GenerateMockData(rnd,candle,vlr, animosity);
                }
                float gapNext = Utils.Random(rnd, -2f, 2f);
                vlr *= (1 + gapNext / 100);

                gapNext = Utils.Random(rnd, -1f, 1f);
                animosity += gapNext;
                if (animosity > 5) animosity = 5;
                if (animosity < -5) animosity = -5;

                ativo.AddCandle(candle);

            }

            return ativo;
        }

        private float GenerateMockData(Random rnd, Candle candle, float vlr,float animosity)
        {
            float gapNext = Utils.Random(rnd, -0.5f+(animosity<0?animosity:0), 0.5f + (animosity > 0 ? animosity : 0));
            candle.AddData(vlr, vlr, vlr, vlr, 100000 * (1 + Math.Abs(gapNext) / 100));
            vlr *= (1 + gapNext / 100);
            if (vlr<10)vlr=10;
            return vlr;
        }
    }
}
