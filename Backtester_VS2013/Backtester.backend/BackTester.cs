using Backtester.backend.interfaces;
using Backtester.backend.model;
using Backtester.backend.model.ativos;
using Backtester.backend.model.system;
using Backtester.backend.model.system.condicoes;
using Backtester.backend.model.system.estatistica;
using System;
using System.Collections.Generic;

namespace Backtester.backend
{
    public class BackTester
    {
        public Carteira carteira { get; private set; }
        Config config;
        TradeSystem tradeSystem;
        Periodo periodoInicial;

        //   IList<MonteCarlo> monteCarlo;
        bool useVars = true;
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

            init();

        }



        public void init()
        {
            carteira = new Carteira(facade, config.capitalInicial, config, tradeSystem);
        }

        public void runBackTest(ICaller caller)
        {
            if (tradeSystem.vm.Count == 0)
            {
                runSingleBackTest(caller);
                return;
            }
            if (!useVars)
            {
                return;
            }

            totalLoops_ = 1;
            countLoops_ = 0;
            foreach (Variavel v in tradeSystem.vm.variaveis)
            {
                totalLoops_ *= v.steps;
            }
            if (useVars) loopVariavel(caller, 0); else runMonteCarlo(caller, "MC Run");

        }

        private int totalLoops_ = 0;
        private int countLoops_ = 0;

        public void loopVariavel(ICaller caller, int id)
        {
            Util.println("init " + id);
            Variavel v = tradeSystem.vm.GetVariavel(id);
            v.reset();
            while (!v.hasEnded())
            {
                //System.out.println("loop "+id+" atual:"+v.getAtual());

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

        public void runMonteCarlo(ICaller caller, String name)
        {
            MonteCarlo mC = new MonteCarlo(name);
            Util.println("runMonteCarlo:" + name);
            Estatistica stat = runSingleBackTest(caller);
            stat.setDesc(getVarsValues());
            mC.setEstatistica(stat);
            mC.update();
            mC.printPerformance("");
            caller.UpdateApplication(carteira, mC, countLoops_, totalLoops_);
            //monteCarlo.Add(mC);

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
        public int[] GetRandomOrder()
        {
            Random rnd = new Random();
            int[] rd = new int[ativos.Count];
            for (int i = 0; i < ativos.Count; i++)
                rd[i] = i;

            for (int i = 0; i < ativos.Count - 1; i++)
                for (int j = i; j < ativos.Count; j++)
                    if (rnd.Next(1) > 0.5f)
                    {
                        int x = rd[i];
                        rd[i] = rd[j];
                        rd[j] = x;
                    }

            return rd;
        }

        /*
         * Método principal que vai verificar as condições e fazer entradas (um integrador por assim dizer)
         */
        public Estatistica runSingleBackTest(ICaller caller)
        {
            init();
            Periodo periodo = periodoInicial;
            string mesA = "";
            int[] rd = GetRandomOrder();
            while (periodo.proximoPeriodo != null)
            {
                caller.SimpleUpdate();
                //System.out.println("Periodo:"+periodo.getPeriodo());
                carteira.EndTurn(periodo, !mesA.Equals(periodo.GetMes()));
                if (!mesA.Equals(periodo.GetMes())) mesA = periodo.GetMes() + "";

                for (int i = 0; i < ativos.Count; i++)
                {

                    Ativo ativo = ativos[rd[i]];
                    Candle candle = ativo.GetCandle(periodo);

                    if (candle != null)
                    {
                        int qtdAcoes = carteira.PossuiAtivo(ativo);
                        float direcao = tradeSystem.checaCondicaoEntrada(candle, config);


                        //Não tenho posicao em aberto E direcao de entrada foi ativada (por hora só vou permitir 1 posicao do ativo por vez

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
                }
                periodo = periodo.proximoPeriodo;
            }
            carteira.FechaPosicoes(periodo);
            carteira.EndTurn(periodo, !mesA.Equals(periodo.GetMes()));
            Util.println("Saldo final:" + carteira.GetCapital());
            carteira.PrintEstatistica();
            return carteira.estatistica;
        }



        public void setUseVars(bool b)
        {
            useVars = b;
        }


        public FacadeBacktester facade { get; set; }

        public DataHolder ativoManager { get; set; }
    }
}
