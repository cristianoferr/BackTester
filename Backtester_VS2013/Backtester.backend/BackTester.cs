using Backtester.backend.interfaces;
using Backtester.backend.model;
using Backtester.backend.model.ativos;
using Backtester.backend.model.system;
using Backtester.backend.model.system.condicoes;
using Backtester.backend.model.system.estatistica;
using System;
using System.Collections.Generic;
using UsoComum;

namespace Backtester.backend
{
    public class BackTester
    {
        public Carteira carteira { get; private set; }
        Config config;
        TradeSystem tradeSystem;
        Periodo periodoInicial;

        IList<Ativo> ativos = null;


        public BackTester(FacadeBacktester facade, Periodo periodoInicial, Config config, TradeSystem tradeSystem)
        {
            this.facade = facade;
            this.ativoManager = facade.dh;
            this.ativos = facade.dh.GetAtivos();
            this.config = config;
            this.tradeSystem = tradeSystem;
            this.periodoInicial = periodoInicial;
            // monteCarlo = new List<MonteCarlo>();

            Init(new MonteCarlo("single"));

        }



        public void Init(MonteCarlo mc)
        {
            carteira = new Carteira(facade, config.capitalInicial, config, tradeSystem,mc);
        }

        public bool runBackTest(ICaller caller,string name)
        {
            if (tradeSystem.vm.Count == 0)
            {
                runSingleBackTest(caller, new MonteCarlo(name));
                return true;
            }
            if (!config.useVars)
            {
                return false;
            }

            totalLoops_ = 1;
            countLoops_ = 0;
            foreach (Variavel v in tradeSystem.vm.variaveis)
            {
                totalLoops_ *= v.steps;
            }
            if (config.useVars) loopVariavel(caller, 0); else runMonteCarlo(caller, "MC Run");
            return true;
        }

        private int totalLoops_ = 0;
        private int countLoops_ = 0;

        public void loopVariavel(ICaller caller, int id)
        {
            Variavel v = tradeSystem.vm.GetVariavel(id);
            v.reset();
            while (!v.hasEnded())
            {
                Utils.Info("loop da variavel " + v.name + " com vlr:" + v.vlrAtual);

                if (id + 1 < tradeSystem.vm.Count)
                {
                    loopVariavel(caller, id + 1);
                }
                else
                {
                    countLoops_++;
                    runMonteCarlo(caller, getVarsValues());
                }

                v.next();


                //runSingleBackTest();
            }
        }

        /*   public void printMonteCarlo()
           {
               for (int i = 0; i < monteCarlo.Count; i++)
               {
                   MonteCarlo mC = monteCarlo[i];
                   mC.printPerformance("MC(" + i + ")");
               }
           }*/

        /*    public void ordernaMonteCarlo(Consts.OrdemEstatistica ordem)
            {
                for (int i = 0; i < monteCarlo.Count - 1; i++)
                {

                    for (int j = i + 1; j < monteCarlo.Count; j++)
                    {
                        MonteCarlo statI = monteCarlo[i];
                        float vI = 0;
                        MonteCarlo statJ = monteCarlo[j];
                        float vJ = 0;
                        if (ordem == Backtester.backend.Consts.OrdemEstatistica.CAPITAL_FINAL) vI = statI.getCapitalFinal();
                        if (ordem == Backtester.backend.Consts.OrdemEstatistica.CAPITAL_FINAL) vJ = statJ.getCapitalFinal();
                        if (vI > vJ)
                        {
                            monteCarlo[j] = statI;
                            monteCarlo[i] = statJ;
                        }
                    }

                }

            }*/

        public Carteira runMonteCarlo(ICaller caller, String name)
        {
            MonteCarlo mC = new MonteCarlo(name);
            Utils.println("runMonteCarlo:" + name);
            Estatistica stat = runSingleBackTest(caller, mC).estatistica;
            stat.setDesc(getVarsValues());
            mC.setEstatistica(stat);
            mC.FinishStats(carteira);
            mC.printPerformance("");
            caller.UpdateApplication(carteira, mC, countLoops_, totalLoops_);
            //monteCarlo.Add(mC);
            return carteira;
        }

        public String getVarsValues()
        {
            String s = "";
            for (int i = 0; i < tradeSystem.vm.Count; i++)
            {
                Variavel v = tradeSystem.vm.GetVariavel(i);
                s = s + " v(" + v.name + "):" + v.vlrAtual;
            }
            return s;
        }
        /*
         * Adiciona um ativo à lista de ativos que serão testados
         */
        private void AddAtivo(String ativo)
        {
            ativos.Add(ativoManager.GetAtivo(ativo));

        }
        public int[] GetRandomOrder(int percMax)
        {
            List<int> lista = new List<int>();
            int[] rd = new int[ativos.Count];
            for (int i = 0; i < ativos.Count; i++)
            {
                rd[i] = i;
                lista.Add(i);
            }

            for (int i = 0; i < ativos.Count - 1; i++)
                for (int j = i; j < ativos.Count; j++)
                    if (Utils.Random(0, 100) > 75)
                    {
                        int x = rd[i];
                        rd[i] = rd[j];
                        rd[j] = x;
                    }

            int[] ret = new int[rd.Length * percMax / 100];
            /*for (int i = 0; i < ret.Length; i++)
                ret[i] = rd[i];*/
            int cont = 0;
            while (cont < ret.Length)
            {
                int rndIndex = Utils.Random(0, lista.Count);
                ret[cont] = lista[rndIndex];
                lista.RemoveAt(rndIndex);
                cont++;
            }

            return ret;
        }

        /*
         * Método principal que vai verificar as condições e fazer entradas (um integrador por assim dizer)
         */
        public Carteira runSingleBackTest(ICaller caller, MonteCarlo mc)
        {
            Init(mc);
            Periodo periodo = periodoInicial;
            string mesA = "";
            int[] rd = GetRandomOrder(config.qtdPercPapeis);
            while (periodo.proximoPeriodo != null)
            {
                BackTestePeriodo(caller, mc, ref periodo, ref mesA, rd);
            }
            carteira.FechaPosicoes(periodo);
            carteira.EndTurn(periodo, !mesA.Equals(periodo.GetMes()));
            Utils.println("Saldo final:" + carteira.GetCapital());
            carteira.PrintEstatistica();
            return carteira;
        }

        public void BackTestePeriodo(ICaller caller, MonteCarlo mc, ref Periodo periodo, ref string mesA, int[] rd)
        {
            caller.SimpleUpdate();
            //System.out.println("Periodo:"+periodo.getPeriodo());
            carteira.EndTurn(periodo, !mesA.Equals(periodo.GetMes()));
            mc.AnalizaPeriodo(carteira);
            if (!mesA.Equals(periodo.GetMes())) mesA = periodo.GetMes() + "";

            for (int i = 0; i < rd.Length; i++)
            {

                Ativo ativo = ativos[rd[i]];
                Candle candle = ativo.GetCandle(periodo);

                if (candle != null)
                {
                    BackTestCandle(periodo, ativo, candle);

                }
            }

            periodo = periodo.proximoPeriodo;
        }

        public void BackTestCandle(Periodo periodo, Ativo ativo, Candle candle)
        {
            int qtdAcoes = carteira.PossuiAtivo(ativo);
            float direcao = tradeSystem.checaCondicaoEntrada(candle, config);

            //Não tenho posicao em aberto E direcao de entrada foi ativada
            if ((direcao != 0))
            {
                if (qtdAcoes == 0 || (qtdAcoes != 0 && tradeSystem.usaMultiplasEntradas))
                {
                    float valorAcao = candle.GetValor(carteira.config.campoCompra);
                    carteira.EfetuaEntrada(ativo, periodo, 1, valorAcao, Math.Abs(direcao), (direcao > 0 ? 1 : -1));
                }
            }

            //Se eu tiver posicao em aberto, verifico se foi ativado a saida, se sim, a saida será feita no vlrSaida padrao
            if (qtdAcoes != 0)
            {

                if (tradeSystem.checaCondicaoSaida(candle, (qtdAcoes > 0 ? 1 : -1)) != 0)
                {
                    carteira.EfetuaSaida(ativo, periodo, 0);
                }
            }
        }




        public FacadeBacktester facade { get; set; }

        public DataHolder ativoManager { get; set; }
    }
}
