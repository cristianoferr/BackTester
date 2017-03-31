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
        Carteira carteira;
        Config config;
        TradeSystem tradeSystem;
        String dataInicial;
        IList<MonteCarlo> monteCarlo;
        int runs = 1;
        bool useVars = true;
        IList<Ativo> ativos = null;


        public BackTester(FacadeBacktester facade, String dataInicial, Config config, TradeSystem tradeSystem)
        {
            this.facade = facade;
            this.ativoManager = facade.dh;
            this.ativos = facade.dh.GetAtivos();
            this.config = config;
            this.tradeSystem = tradeSystem;
            this.dataInicial = dataInicial;
            monteCarlo = new List<MonteCarlo>();

            init();

        }



        public void init()
        {
            carteira = new Carteira(facade, config.capitalInicial, config, tradeSystem);

        }

        public void runBackTest()
        {
            if (tradeSystem.vm.Count == 0)
            {
                runSingleBackTest();
                return;
            }
            if (!useVars)
            {
                return;
            }

            if (useVars) loopVariavel(0); else runMonteCarlo("MC Run");
        }
        public void loopVariavel(int id)
        {
            Util.println("init " + id);
            Variavel v = tradeSystem.vm.GetVariavel(id);
            v.reset();
            while (!v.hasEnded())
            {
                //System.out.println("loop "+id+" atual:"+v.getAtual());

                if (id + 1 < tradeSystem.vm.Count)
                    loopVariavel(id + 1);
                else
                {
                    runMonteCarlo(getVarsValues());
                }

                v.next();


                //runSingleBackTest();
            }
        }

        public void printMonteCarlo()
        {
            for (int i = 0; i < monteCarlo.Count; i++)
            {
                MonteCarlo mC = monteCarlo[i];
                mC.printPerformance("MC(" + i + ")");
            }
        }

        public void ordernaMonteCarlo(Consts.OrdemEstatistica ordem)
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

        }

        public void runMonteCarlo(String name)
        {
            MonteCarlo mC = new MonteCarlo(name);
            Util.println("runMonteCarlo:" + name);
            for (int i = 0; i < runs; i++)
            {
                Estatistica stat = runSingleBackTest();
                stat.setDesc(getVarsValues());
                mC.addEstatistica(stat);
            }
            mC.update();
            mC.printPerformance("");
            monteCarlo.Add(mC);

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
            int[] rd = new int[ativos.Count];
            for (int i = 0; i < ativos.Count; i++)
                rd[i] = i;

            for (int i = 0; i < ativos.Count - 1; i++)
                for (int j = i; j < ativos.Count; j++)
                    if (new Random().Next(1) > 0.5f)
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
        public Estatistica runSingleBackTest()
        {
            init();
            Periodo periodo = ativoManager.GetPeriodo(dataInicial);
            string mesA = "";
            while (periodo.proximoPeriodo != null)
            {
                //System.out.println("Periodo:"+periodo.getPeriodo());
                carteira.EndTurn(periodo, !mesA.Equals(periodo.GetMes()));
                if (!mesA.Equals(periodo.GetMes())) mesA = periodo.GetMes() + "";

                int[] rd = GetRandomOrder();
                for (int i = 0; i < ativos.Count; i++)
                {

                    Ativo ativo = ativos[rd[i]];

                    Candle candle = ativo.GetCandle(periodo);


                    if (candle != null)
                    {
                        int qtdAcoes = carteira.PossuiAtivo(ativo);
                        float direcao = tradeSystem.checaCondicaoEntrada(candle, config);
                        //System.out.println("Capital:"+carteira.getCapital()+" Líquido:"+carteira.getCapitalLiq()+" QtdAcoes:"+qtdAcoes+" C:"+candle.getValor("C")+" MME(C,9):"+candle.getValor("MME(C,9)")+" direcao:"+direcao);

                        //Não tenho posicao em aberto E direcao de entrada foi ativada (por hora só vou permitir 1 posicao do ativo por vez
                        if ((direcao != 0))
                        {
                            float valorAcao = candle.GetValor(carteira.config.campoCompra);
                            carteira.EfetuaEntrada(ativo, periodo, 1, valorAcao, Math.Abs(direcao), (direcao > 0 ? 1 : -1));
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



        public int getRuns()
        {
            return runs;
        }



        public void setRuns(int runs)
        {
            this.runs = runs;
        }



        public void setUseVars(bool b)
        {
            useVars = b;
        }




        public FacadeBacktester facade { get; set; }

        public DataHolder ativoManager { get; set; }
    }
}
