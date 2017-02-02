using Backtester.backend.model.ativos;
using Backtester.backend.model.formulas;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Console.WriteLine("Carregando formula : " + f.GetCode());

            foreach (Ativo ativo in ativos.Values)
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
            }
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
    }
}
