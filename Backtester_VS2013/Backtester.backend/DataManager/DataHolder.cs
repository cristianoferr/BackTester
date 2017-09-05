using Backtester.backend.model.ativos;
using Backtester.backend.model.formulas;
using System.Collections.Generic;
using System.Linq;
using System;
using UsoComum;

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
            
            float vlr= 10 + rnd.Next(90);
            float vol = 100000 + rnd.Next(100000);
            float percGap = rnd.Next(5);
            for (int i = 0; i < Consts.QTD_ATIVOS_MOCK; i++)
            {
                Periodo periodo = GetPeriodo(DateTime.Now.AddDays(i).ToShortDateString());
                float open = vlr;
                float difClose = Utils.Random(rnd,-percGap,percGap);
                vlr *= (1 + difClose / 100);
                float close = vlr;

                float vlrAlta = Utils.Random(0,2);
                float high = (open > close ? open : close) * (1 + vlrAlta / 100);
                float vlrBaixa = Utils.Random(0, 2);
                float low = (open < close ? open : close) * (1 - vlrBaixa / 100);

                vol = (100000 + rnd.Next(100000))*(1+Math.Abs(percGap) /100);
                Candle candle = new Candle(periodo, ativo,open,close,high,low,vol);
                ativo.AddCandle(candle);

                percGap += Utils.Random(rnd, -1,1);
                if (percGap < 1) percGap = 1;
                if (percGap >5) percGap = 5;

                float gapNext = Utils.Random(rnd,-2, 2);
                vlr *= (1 + gapNext / 100);

            }

            return ativo;
        }
    }
}
