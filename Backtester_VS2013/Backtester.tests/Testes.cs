using Backtester.backend;
using Backtester.backend.DataManager;
using Backtester.backend.interfaces;
using Backtester.backend.model;
using Backtester.backend.model.ativos;
using Backtester.backend.model.formulas;
using Backtester.backend.model.system;
using Backtester.backend.model.system.condicoes;
using Backtester.backend.model.system.estatistica;
using Backtester.controller;
using Backtester.GeneticProgramming;
using Backtester.interfaces;
using GeneticProgramming.nodes;
using GeneticProgramming.solution;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UsoComum;

namespace Backtester.tests
{
    class MockCaller : ICaller
    {
        public Carteira RunBackTester(TradeSystem ts, string name)
        {
            return null;
        }

        public void SimpleUpdate()
        {
        }

        public void UpdateApplication(Carteira carteira, MonteCarlo mc, int countLoops, int totalLoops)
        {
        }
    }
    
    [TestClass]
    public class Testes
    {

        static FacadeBacktester facade = new FacadeBacktester();


        [TestMethod]
        public void TestData()
        {
            int year = 2017;
            int month = 10;
            int day = 15;
            DateTime data = new DateTime(year, month, day,15,0,0);
            double dataUnix=Utils.DateTimeToUnixTimestamp(data);
            DateTime dataFromUnix = Utils.UnixTimeStampToDateTime(dataUnix);
            Assert.IsTrue(dataFromUnix.Year == data.Year);
            Assert.IsTrue(dataFromUnix.Month == data.Month);
            Assert.IsTrue(dataFromUnix.Day == data.Day);
        }

        [TestMethod]
        public void TestMockAtivo()
        {
            Ativo ativo = facade.GenerateAtivoMock(500);
            Assert.IsNotNull(ativo);
            Assert.IsTrue(ativo.candles.Count == Consts.QTD_ATIVOS_MOCK);

        }


        [TestMethod]
        public void TestBacktester()
        {

            MockCaller mockCaller = new MockCaller();
            Config config = new Config();
            TradeSystem tradeSystem = new TradeSystem(config);
            MonteCarlo mc = new MonteCarlo("Teste");
            config.custoOperacao = 0f;
            config.flagCompra = true;
            config.flagVenda = false;
            config.capitalInicial = 100000;


            facade.LoadAtivo("PETR4", 100, Consts.PERIODO_ACAO.SEMANAL, "dados/petr4-diario.js");
            Ativo ativo = facade.GetAtivo("PETR4");
            Assert.IsNotNull(ativo);
            Assert.IsTrue(ativo.candles.Count > 0);
            Candle candle = ativo.firstCandle;
            for (int i = 0; i < 30; i++)
            {
                candle = candle.proximoCandle;
            }
            Periodo periodo = candle.periodo;


            Assert.IsTrue(Stop.CalcValorStop(10, 1, 10)==9);
            Assert.IsTrue(Stop.CalcValorStop(10, -1, 10) == 11);

            BackTester backtester = new BackTester(facade, periodo, config, tradeSystem);
            Carteira carteira = backtester.carteira;
            Assert.IsTrue(carteira.PossuiAtivo(ativo) == 0);

            tradeSystem.condicaoEntradaC = "GREATER(MME(C,9),MME(C,6))";
            tradeSystem.condicaoSaidaC = "LOWER(MME(C,9),MME(C,6))";
            float mmec9=candle.GetValor("MME(C,9)");
            float mmec6 = candle.GetValor("MME(C,6)");
            Assert.IsTrue(mmec9 > 0);
            Assert.IsTrue(mmec6 > 0);
            Assert.IsTrue(candle.GetValor(tradeSystem.condicaoEntradaC) == 1);

            tradeSystem.vm.SetVariavel(Consts.VAR_STOP_GAP, 2);
            tradeSystem.vm.SetVariavel(Consts.VAR_USA_STOP_MOVEL, 1);

            tradeSystem.stopInicialC = "LV(L,5)";
            tradeSystem.stopMovelC = tradeSystem.stopInicialC;
            float stop = candle.GetValor(tradeSystem.stopInicialC);
            stop = stop * (1f - tradeSystem.stopGapPerc / 100f);

            //realiza 1a entrada
            backtester.BackTestCandle(periodo, ativo, candle);
            int qtdAcoes = carteira.PossuiAtivo(ativo);
            Assert.IsTrue(qtdAcoes  == 500,qtdAcoes+" <> 500");

            Posicao posicao=carteira.GetPosicaoDoAtivo(ativo);
            Assert.IsNotNull(posicao);
            Assert.IsTrue(posicao.saldo == 500, posicao.saldo + "<>"+ 500);
            Operacao oper = posicao.operacoesAbertas[0];
            Assert.IsTrue(oper.stop.CalcStop(candle) == stop);
            Assert.IsTrue(carteira.capitalLiq == 100000 - 500 * candle.proximoCandle.GetValor(FormulaManager.OPEN));
            float capitalAtual = carteira.capitalLiq;

            //simulando um stop
            candle = candle.proximoCandle;
            carteira.periodoAtual = candle.periodo;
            float vlrStopado = stop / 2;
            candle.SetValor(FormulaManager.OPEN, vlrStopado);
            candle.SetValor(FormulaManager.LOW, vlrStopado);
            posicao.VerificaStops(candle);

            qtdAcoes = carteira.PossuiAtivo(ativo);
            Assert.IsTrue(qtdAcoes == 0, qtdAcoes + " <> 0");
            Assert.IsTrue(carteira.capitalLiq == capitalAtual + 500 * vlrStopado);
            capitalAtual = carteira.capitalLiq;

            //mais uma entrada
            candle = candle.proximoCandle;
            carteira.periodoAtual = candle.periodo;
            backtester.BackTestCandle(periodo, ativo, candle);
            qtdAcoes = carteira.PossuiAtivo(ativo);
            Assert.IsTrue(qtdAcoes == 500, qtdAcoes + " <> 500");
            Assert.IsTrue(carteira.capitalLiq == capitalAtual - 500 * candle.proximoCandle.GetValor(FormulaManager.OPEN));
            capitalAtual = carteira.capitalLiq;

            candle = candle.proximoCandle;
            carteira.periodoAtual = candle.periodo;
            posicao.VerificaStops(candle);
            qtdAcoes = carteira.PossuiAtivo(ativo);
            //não foi stopado...
            Assert.IsTrue(qtdAcoes == 500, qtdAcoes + " <> 500");

            /*  candle.SetValor(tradeSystem.stopInicialC, vlrStopado);
              candle.SetValor(FormulaManager.OPEN, vlrStopado);
              candle.SetValor(FormulaManager.LOW, vlrStopado-1);
              posicao.VerificaStops(candle);
              qtdAcoes = carteira.PossuiAtivo(ativo);
              //foi stopado...
              Assert.IsTrue(qtdAcoes == 0, qtdAcoes + " <> 0");*/

            //MME(C,9)<MME(C,6)
            Assert.IsTrue(tradeSystem.checaCondicaoSaida(candle, 1)==1);

        }

        [TestMethod]
        public void TestClarificarTradeSystem()
        {
            Config config = new Config();
            FormulaManager fm = facade.formulaManager;
            Clarify clarify = new Clarify();
            //TradeSystem tradeSystem = new TradeSystem(config);
            ValidaClarify(clarify, "SUM(H,MULTIPLY(STD(C,10),2))","H + (STD(C,10) * 2)");
            ValidaClarify(clarify, "GREATER(MME(C, 9), MME(C, 6))", "MME(C,9) > MME(C,6)");
            ValidaClarify(clarify, "AND(LV(C, L), C)", "LV(C,L) && C");
            


        }

        private void ValidaClarify(Clarify clarify, string original, string esperado)
        {
            string[] elementos = Utils.SeparaEmElementos(original);
            string resultado = clarify.ClarificaFormula(original);
            Assert.IsTrue(resultado == esperado, "Em '" + original + "': '" + resultado + "'<>'" + esperado + "'");

            Assert.IsNotNull(elementos);
        }

        [TestMethod]
        public void TestValidaCandidato()
        {
            MockReferView rv = new MockReferView();
            ConfigController configController = new ConfigController(rv);

            GeneticProgrammingController gpc = new GeneticProgrammingController(rv, configController);
            gpc.gpRunner.gpConfig.poolSize = 2;
            gpc.InitPool();

            gpc.solutionToTest = new GPSolutionProxy();
            gpc.solutionToTest.tradeSystem = new TradeSystem(configController.config);
            Assert.IsNotNull(gpc.solutionToTest.tradeSystem);
            Carteira carteira=gpc.SingleRunValidaSolution("teste Validation");
            Assert.IsTrue(carteira.estatistica.capitalFinal != carteira.estatistica.capitalInicial);

            Assert.IsNotNull(carteira);
            Assert.IsTrue(carteira.posicoesAbertas.Count == 0);
            Assert.IsTrue(carteira.posicoesFechadas.Count > 0);

            Assert.IsTrue(configController.facadeValidation.dh.ativos.Count > 0);

            //adicionar o resultado da carteira em algum objeto persistido, ordenando pelo resultado final
            CandidatoManager cm=CandidatoManager.LoadSaved();
            CandidatoManager cm2 = CandidatoManager.LoadSaved();
            Assert.IsTrue(cm == cm2);

            Estatistica stat = new Estatistica(10000);

            CandidatoManager cmTest = new CandidatoManager();
            TradeSystem ts1 = new TradeSystem(configController.config);
            TradeSystem ts2 = new TradeSystem(configController.config);
            TradeSystem ts3 = new TradeSystem(configController.config);
            stat.capitalFinal = 1000;
            cmTest.AddTradeSystem(ts1,stat);
            Assert.IsTrue(cmTest.GetRanking(ts1) == 0);
            stat.capitalFinal = 2000;
            cmTest.AddTradeSystem(ts2, stat);
            Assert.IsTrue(cmTest.GetRanking(ts1) == 1);
            Assert.IsTrue(cmTest.GetRanking(ts2) == 0);
            stat.capitalFinal = 3000;
            cmTest.AddTradeSystem(ts3, stat);
            Assert.IsTrue(cmTest.GetRanking(ts1) == 2);
            Assert.IsTrue(cmTest.GetRanking(ts2) == 1);
            Assert.IsTrue(cmTest.GetRanking(ts3) == 0);

        }

        [TestMethod]
        public void TestRandomSolution()
        {
            MockReferView rv = new MockReferView();
            ConfigController configController = new ConfigController(rv);

            GeneticProgrammingController gpc = new GeneticProgrammingController(rv, configController);
            gpc.gpRunner.gpConfig.poolSize = 2;
            gpc.InitPool();
            GPSolution solution = gpc.gpRunner.pool.template.CreateRandomSolution();
            Assert.IsNotNull(solution);
        }

        [TestMethod]
        public void TestConfigGPVars()
        {
            Config config = new Config();
            config.AddGPVar("VAR1", 10);
            config.AddGPVar("//VAR2", 10);
            Assert.IsTrue(config.IsGPVarDefined("VAR1"));
            Assert.IsFalse(config.IsGPVarDefined("VAR2"));
            Assert.IsTrue(config.GetGPVarValue("VAR1") == 10);
        }

        [TestMethod]
        public void TestBacktesterVars()
        {
            Config config = new Config();
            config.capitalInicial = 100000;
            TradeSystem tradeSystem = new TradeSystem(config);

            facade.LoadAtivo("PETR4", 100, Consts.PERIODO_ACAO.DIARIO, "dados/petr4-diario.js", backend.Consts.TIPO_CARGA_ATIVOS.GERA_CANDIDATOS);
            Ativo ativo = facade.GetAtivo("PETR4");
            Assert.IsNotNull(ativo);
            Assert.IsTrue(ativo.candles.Count > 0);

            Candle candle = ativo.firstCandle;
            Periodo periodo = candle.periodo;

            BackTester bt = new BackTester(facade, periodo, config, tradeSystem);

            Carteira carteira = bt.carteira;
            Assert.IsNotNull(carteira);
            Assert.IsTrue(carteira.posicoesFechadas.Count == 0);
            Assert.IsTrue(carteira.posicoesAbertas.Count == 0);
            float riscoTotal = carteira.GetRiscoCarteiraPercent(periodo);
            Assert.IsTrue(riscoTotal == 0);
            // carteira.EfetuaEntrada(ativo, periodo, 1, 10, 9, 1);

            //entro com um valor fixo (direto na carteira)
            Posicao posicao = new Posicao(carteira, ativo, 0);
            carteira.posicoesAbertas.Add(ativo, posicao);

            int qtd = 1000;
            posicao.saldo += qtd;
            Operacao oper1 = new Operacao(carteira, candle, 10, 9, qtd, 1, null);
            posicao.operacoesAbertas.Add(oper1);
            float capitalSobRisco = ((10 - 9) * qtd);
            float riscoEsperado = capitalSobRisco / carteira.GetCapital() * 100;
            carteira.capitalLiq -= oper1.vlrEntrada * oper1.qtd;

            candle.SetValor(FormulaManager.CLOSE, 10);
            carteira.periodoAtual = periodo;
            carteira.AtualizaPosicao();
            Assert.IsTrue(carteira.GetCapital() == 100000, carteira.GetCapital() + "<>" + 100000);
            Assert.IsTrue(carteira.capitalPosicao == 10000, carteira.GetCapital() + "<>" + 10000);
            riscoTotal = carteira.GetRiscoCarteiraPercent(periodo);
            Assert.IsTrue(riscoTotal == riscoEsperado, riscoTotal + "<>" + riscoEsperado);

            //2a operacao
            Operacao oper2 = new Operacao(carteira, candle, 10, 9, qtd, 1, null);
            posicao.operacoesAbertas.Add(oper2);
            posicao.saldo += qtd;
            carteira.AtualizaPosicao();
            carteira.capitalLiq -= oper1.vlrEntrada * oper1.qtd;
            capitalSobRisco *= 2;
            riscoEsperado = capitalSobRisco / carteira.GetCapital() * 100;

            Assert.IsTrue(carteira.GetCapital() == 100000, carteira.GetCapital() + "<>" + 100000);
            Assert.IsTrue(carteira.capitalPosicao == 20000, carteira.GetCapital() + "<>" + 20000);
            riscoTotal = carteira.GetRiscoCarteiraPercent(periodo);
            Assert.IsTrue(riscoTotal == riscoEsperado, riscoTotal + "<>" + riscoEsperado);

            tradeSystem.vm.GetVariavel(Consts.VAR_RISCO_TRADE).vlrAtual = 2;
            tradeSystem.vm.GetVariavel(Consts.VAR_RISCO_GLOBAL).vlrAtual = 5;

            float qtdTrade = carteira.CalculaQtdTrade(100000, 10, 12, periodo);
            Assert.IsTrue(qtdTrade == 980, qtdTrade + "<>" + 980);
        }

        [TestMethod]
        public void TestWebRequest()
        {
            /* string saida = AcaoService.LoadFromWeb("PETR4");
             Assert.IsNotNull(saida);
             Assert.IsTrue(saida.Contains("\"s\":\"ok\""), saida);*/
        }

        [AssemblyInitialize]
        public static void Configure(TestContext tc)
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        [TestMethod]
        public void TestLogs()
        {
            Utils.Info("log de info");
            Utils.Error("log de erro");
            Utils.Info("log de info");

            log.Info("log direto");
        }

        [TestMethod]
        public void TestInicial()
        {
            string ativoName = "ABCD";
            Ativo ativo = new Ativo(facade, ativoName, 100);

            facade.AddAtivo(ativo);
            Ativo ativoGet = facade.GetAtivo(ativoName);
            Assert.IsNotNull(ativoGet);
            Assert.IsTrue(ativoGet == ativo);
            Assert.IsTrue(ativo.loteMin == 100);
            Assert.IsTrue(ativo.name == ativoName);

            Periodo periodo1 = new Periodo("2015-06-05 00:00");
            Periodo periodo2 = new Periodo("2015-06-06 00:00");

            float open = 2;
            float close = 3;
            float high = 3.5f;
            float low = 1.5f;
            float vol = 100;

            Candle candle = new Candle(periodo1, ativo);
            ativo.AddCandle(candle);
            candle.SetValor(FormulaManager.CLOSE, close);
            candle.SetValor(FormulaManager.OPEN, open);
            candle.SetValor(FormulaManager.HIGH, high);
            candle.SetValor(FormulaManager.LOW, low);
            candle.SetValor(FormulaManager.VOL, vol);


            Candle candleDefinido = new Candle(periodo2, ativo, open, close, high, low, vol);
            ativo.AddCandle(candleDefinido);

            Assert.IsTrue(candleDefinido.GetValor(FormulaManager.CLOSE) == candle.GetValor(FormulaManager.CLOSE) && candle.GetValor(FormulaManager.CLOSE) == close);
            Assert.IsTrue(candleDefinido.GetValor(FormulaManager.OPEN) == candle.GetValor(FormulaManager.OPEN) && candle.GetValor(FormulaManager.OPEN) == open);
            Assert.IsTrue(candleDefinido.GetValor(FormulaManager.HIGH) == candle.GetValor(FormulaManager.HIGH) && candle.GetValor(FormulaManager.HIGH) == high);
            Assert.IsTrue(candleDefinido.GetValor(FormulaManager.LOW) == candle.GetValor(FormulaManager.LOW) && candle.GetValor(FormulaManager.LOW) == low);
            Assert.IsTrue(candleDefinido.GetValor(FormulaManager.VOL) == candle.GetValor(FormulaManager.VOL) && candle.GetValor(FormulaManager.VOL) == vol);

            Assert.IsTrue(ativo.GetCandle(periodo2) == candleDefinido);
            Assert.IsTrue(ativo.GetCandle(periodo1) == candle);
        }

        [TestMethod]
        public void TestVariaveis()
        {
            Config config = new Config();
            TradeSystem tradeSystem = new TradeSystem(config);
            VariavelManager vm = tradeSystem.vm;
            string na = "VAR1";
            string nb = "NOME_QUALQUER";
            Variavel va = vm.GetVariavel(na, "", 1, 2, 3);
            Variavel vb = vm.GetVariavel(nb, "", 10, 3, 30);

            string text = "%" + na + "%>%" + nb + "%";
            Assert.IsTrue(vm.ReplaceVariavel(text) == "1>10", "Erro: " + vm.ReplaceVariavel(text));
            va.next();
            vb.next();
            float vna = va.vlrAtual;
            float vnb = vb.vlrAtual;
            Assert.IsTrue(vm.ReplaceVariavel(text) == vna + ">" + vnb, "Erro: " + vm.ReplaceVariavel(text));

            text = "%" + na + "%%" + nb + "%";
            Assert.IsTrue(vm.ReplaceVariavel(text) == vna + "" + vnb, "Erro: " + vm.ReplaceVariavel(text));
        }

        [TestMethod]
        public void TestCarteira()
        {
            Config config = new Config();
            TradeSystem tradeSystem = new TradeSystem(config);
            MonteCarlo mc = new MonteCarlo("Teste");

            float valorInicial = 100000;
            Carteira carteira = new Carteira(facade, 100000, config, tradeSystem,mc);
            config.custoOperacao = 20f;

            Assert.IsTrue(carteira.capitalInicial == valorInicial);
            Assert.IsTrue(carteira.capitalLiq == valorInicial);

            Ativo ativoInex = new Ativo(facade, "ERR", 100);
            Ativo ativo = new Ativo(facade, "PETR4", 100);
            Periodo periodo1 = new Periodo("2015-06-05 00:00");
            Periodo periodo2 = new Periodo("2015-06-06 00:00");

            float closeInicial = 3;
            float closeFinal = 2.7f;
            Candle candleInicial = new Candle(periodo1, ativo, closeInicial * 0.9f, closeInicial, closeInicial * 1.2f, closeInicial * 0.9f, 1000);
            Candle candleFinal = new Candle(periodo2, ativo, closeFinal * 0.9f, closeFinal, closeFinal * 1.2f, closeFinal * 0.8f, 1000);
            ativo.AddCandle(candleInicial);
            ativo.AddCandle(candleFinal);

            Assert.IsTrue(carteira.capitalLiq == valorInicial);

            carteira.EfetuaEntrada(ativo, periodo1, 1, closeInicial, closeInicial * 0.8f, 1);

            Assert.IsTrue(carteira.capitalLiq < valorInicial, carteira.capitalLiq + ">=" + valorInicial);
            Posicao posicao = carteira.GetPosicaoDoAtivo(ativo);
            Assert.IsNotNull(posicao);
            Assert.IsTrue(posicao.ativo == ativo);
            Assert.IsTrue(posicao.operacoesAbertas.Count == 1);
            Operacao oper = posicao.operacoesAbertas[0];
            Assert.IsTrue(oper.qtd > 0);
            Assert.IsTrue(carteira.capitalLiq + oper.qtd * oper.vlrEntrada + config.custoOperacao == valorInicial);
            carteira.periodoAtual = periodo1;
            carteira.AtualizaPosicao();
            Assert.IsTrue(carteira.capitalLiq + carteira.capitalPosicao == valorInicial - config.custoOperacao, "Posicao: " + carteira.capitalPosicao);

            carteira.periodoAtual = periodo2;
            carteira.AtualizaPosicao();
            Assert.IsTrue(carteira.capitalPosicao == 0, "vlr posicao:" + carteira.capitalPosicao);
            //  cartAssert.IsTrue(oper.qtd>0);eira.

            /*      int qtdAcoesOrdem = 350; int qtdAcoesEsperado = 300;
                  carteira.EfetuaCompra(candleInicial, qtdAcoesOrdem);
                  float capitalEsperado = valorInicial - config.custoOperacao - qtdAcoesEsperado * closeInicial;
                  Assert.IsTrue(carteira.capitalLiq == capitalEsperado, "Capital atual:" + carteira.capitalLiq + " <> " + capitalEsperado);

                  Posicao posicao = carteira.GetPosicaoDoAtivo(ativo);
                  Assert.IsNotNull(posicao);
                  Assert.IsTrue(posicao.saldo == qtdAcoesEsperado);

                  carteira.EfetuaCompra(candleInicial, qtdAcoesOrdem);
                  capitalEsperado = valorInicial - 2 * (config.custoOperacao + qtdAcoesEsperado * closeInicial);
                  Assert.IsTrue(carteira.capitalLiq == capitalEsperado, "Capital atual:" + carteira.capitalLiq + " <> " + capitalEsperado);
                  Assert.IsTrue(posicao.saldo == qtdAcoesEsperado * 2);

                  int maxQtd = (int)(capitalEsperado / closeInicial);
                  maxQtd -= maxQtd % ativo.loteMin;
                  carteira.EfetuaCompra(candleInicial, maxQtd * 2);
                  qtdAcoesEsperado = qtdAcoesEsperado * 2 + maxQtd;
                  Assert.IsTrue(posicao.saldo == qtdAcoesEsperado);
                  capitalEsperado -= maxQtd * closeInicial + config.custoOperacao;
                  Assert.IsTrue(carteira.capitalLiq == capitalEsperado, "Capital atual:" + carteira.capitalLiq + " <> " + capitalEsperado);

                  //Venda
                  carteira.EfetuaVenda(candleFinal, 150);
                  capitalEsperado = capitalEsperado + closeFinal * 100 - config.custoOperacao;
                  Assert.IsTrue(carteira.capitalLiq == capitalEsperado, "Capital atual:" + carteira.capitalLiq + " <> " + capitalEsperado);
                  qtdAcoesEsperado -= 100;
                  Assert.IsTrue(posicao.saldo == qtdAcoesEsperado);


                  carteira.EfetuaVenda(candleFinal, qtdAcoesEsperado * 100);
                  capitalEsperado = capitalEsperado + qtdAcoesEsperado * closeFinal - config.custoOperacao;
                  Assert.IsTrue(carteira.capitalLiq == capitalEsperado, "Capital atual:" + carteira.capitalLiq + " <> " + capitalEsperado);
                  Assert.IsTrue(posicao.saldo == 0);*/
        }
        [TestMethod]
        public void TestCarga()
        {
            facade.LoadAtivo("PETR4", 100, Consts.PERIODO_ACAO.DIARIO, "dados/petr4-diario.js");
            Ativo ativo = facade.GetAtivo("PETR4");
            Assert.IsNotNull(ativo);
            Assert.IsTrue(ativo.candles.Count > 0);

            Assert.IsTrue(facade.dh.periodos.Count > 0);
            Assert.IsTrue(facade.dh.periodos[0].candles[0] != null);
            Assert.IsTrue(facade.dh.periodos[0].candles[0].periodo == facade.dh.periodos[0]);
            Assert.IsTrue(facade.dh.periodos[1].candles[0].candleAnterior == facade.dh.periodos[0].candles[0]);

        }

        [TestMethod]
        public void TestaFormulasDiretamente()
        {
            facade = new FacadeBacktester();
            FormulaManager fm = facade.formulaManager;
            facade.LoadAtivo("PETR4", 100, Consts.PERIODO_ACAO.DIARIO, "dados/petr4-diario.js");
            Ativo ativo = facade.GetAtivo("PETR4");
            Formula fTeste = new Formula(facade, "TESTE");
            Formula fTeste2 = new Formula(facade, "TESTE2");

            Formula f = new FormulaPercentil(facade, "PERCENTIL", fTeste);
            Candle candle = ativo.firstCandle;
            candle.SetValor("TESTE", candle.GetValor(FormulaManager.LOW));
            Assert.IsTrue(f.Calc(candle) == 0, f.Calc(candle) +" <> "+ 0);
            candle.SetValor("TESTE", candle.GetValor(FormulaManager.HIGH));
            Assert.IsTrue(f.Calc(candle) == 1, f.Calc(candle) + " <> " + 1);

            f = new FormulaMultiply(facade, "MULT", fTeste, 10);
            candle.SetValor(fTeste,10);
            Assert.IsTrue(f.Calc(candle) == 100, f.Calc(candle) + " <> " + 100);
            candle.SetValor(fTeste, 5);
            Assert.IsTrue(f.Calc(candle) == 50, f.Calc(candle) + " <> " + 50);

            f = new FormulaSUM(facade, "MULT", fTeste, fTeste2);
            candle.SetValor(fTeste2, 10);
            Assert.IsTrue(f.Calc(candle) == 15, f.Calc(candle) + " <> " + 15);

            //HV e LV
            Candle candle2 = candle.proximoCandle;
            Candle candle3 = candle2.proximoCandle;
            Candle candle4 = candle3.proximoCandle;
            Formula fHV = new FormulaHV(facade, "HV", fm.GetFormula(FormulaManager.HIGH), fTeste2);
            Formula fLV = new FormulaLV(facade, "LV", fm.GetFormula(FormulaManager.HIGH), fTeste2);

            candle4.SetValor(fTeste2, 3);
            float max = candle4.GetValor(fm.GetFormula(FormulaManager.HIGH));
            float low = candle4.GetValor(fm.GetFormula(FormulaManager.HIGH));
            Candle c = candle4;
            for (int i = 0; i < 3; i++)
            {
                float value = c.GetValor(fm.GetFormula(FormulaManager.HIGH));
                if (value > max) max = value;
                if (value < low) low = value;
                c = c.candleAnterior;
            }
            Assert.IsTrue(max == fHV.Calc(candle4), max + "<>" + fHV.Calc(candle4));
            Assert.IsTrue(low == fLV.Calc(candle4), max + "<>" + fLV.Calc(candle4));

            //REF
            f = new FormulaREF(facade, "REF", fTeste, fTeste2);
            candle4.SetValor(fTeste, 10);
            candle3.SetValor(fTeste, 20);
            candle2.SetValor(fTeste, 30);
            candle4.SetValor(fTeste2, 1);
            Assert.IsTrue(20 == f.Calc(candle4));
            candle4.SetValor(fTeste2, 2);
            Assert.IsTrue(30 == f.Calc(candle4));

        }

        [TestMethod]
        public void TestFormulas()
        {
            facade = new FacadeBacktester();
            facade.LoadAtivo("PETR4", 100, Consts.PERIODO_ACAO.DIARIO, "dados/petr4-diario.js");
            Ativo ativo = facade.GetAtivo("PETR4");

            FormulaManager fm = facade.formulaManager;

            Assert.IsNotNull(fm.GetFormula("3"));

            Formula f = fm.GetFormula("RSI(HV(SUBTRACT(O,-9),L),C)");
            Assert.IsNotNull(f);

            f = fm.GetFormula("PERCENTIL(SUBTRACT(PERCENTIL(C),C))");
            Assert.IsNotNull(f);

            f = fm.GetFormula("RSI(HV(SUBTRACT(O,-9),L),C)");
            Assert.IsNotNull(f);

            Assert.IsNotNull(fm.GetFormula("MME(C,3)"));

            string fonte = "C";
            int periodos = 3;

            validaFormulaMMS(ativo, fm, fonte, periodos);

            periodos = 6;
            fonte = "MME(O,6)";
            validaFormulaMMS(ativo, fm, fonte, periodos);
        }

        private static void validaFormulaMMS(Ativo ativo, FormulaManager fm, string fonte, int periodos)
        {
            string codigoFormula = "MMS(" + fonte + "," + periodos + ")";
            Formula formulaMMS = fm.GetFormula(codigoFormula);
            Assert.IsNotNull(formulaMMS);
            Assert.IsTrue(formulaMMS.GetCode() == codigoFormula, formulaMMS.GetCode() + "<>" + codigoFormula);

            float soma = 0;
            Candle candle = ativo.firstCandle;
            for (int i = 0; i < 10; i++)
            {
                candle = candle.proximoCandle;
            }
            for (int i = 0; i < periodos; i++)
            {
                soma += candle.GetValor(fonte);
                candle = candle.proximoCandle;
            }
            soma = soma / periodos;
            candle = candle.candleAnterior;
            Assert.IsTrue(Math.Abs(candle.GetValor(codigoFormula) - soma) < 0.1f, candle.GetValor(codigoFormula) + "<>" + soma);
        }

        [TestMethod]
        public void TestNode()
        {
            facade.LoadAtivo("PETR4", 100, Consts.PERIODO_ACAO.DIARIO, "dados/petr4-diario.js");
            Ativo ativo = facade.GetAtivo("PETR4");

            Config config = new Config();
            TradeSystem ts = new TradeSystem(config);
            Candle candle = ativo.firstCandle;
            candle.SetValor("A", 10);
            candle.SetValor("B", 20);
            candle.SetValor("C", 10);

            Condicao cond1 = new Condicao(config, "A>B");
            Condicao cond2 = new Condicao(config, "B>C");



            Node nodeOR = new Node(ConstsComuns.BOOLEAN_TYPE.OR);
            Node nodeAND = new Node(ConstsComuns.BOOLEAN_TYPE.AND);
            Node nodeNOT = new Node(ConstsComuns.BOOLEAN_TYPE.NOT);
            nodeOR.AddCondicao(cond1);
            nodeAND.AddCondicao(cond1);
            nodeNOT.AddCondicao(cond1);

            Assert.IsFalse(nodeOR.VerificaCondicao(candle, ts));
            Assert.IsFalse(nodeAND.VerificaCondicao(candle, ts));
            Assert.IsTrue(nodeNOT.VerificaCondicao(candle, ts));
            candle.SetValor("A", 30);
            Assert.IsTrue(nodeOR.VerificaCondicao(candle, ts));
            Assert.IsTrue(nodeAND.VerificaCondicao(candle, ts));
            Assert.IsFalse(nodeNOT.VerificaCondicao(candle, ts));

            candle.SetValor("A", 10);
            nodeOR.AddCondicao(cond2);
            nodeAND.AddCondicao(cond2);
            nodeNOT.AddCondicao(cond2);
            Assert.IsTrue(nodeOR.VerificaCondicao(candle, ts));
            Assert.IsFalse(nodeAND.VerificaCondicao(candle, ts));
            Assert.IsTrue(nodeNOT.VerificaCondicao(candle, ts));
        }

        [TestMethod]
        public void TestSeparaFormulaEmElementos()
        {
            string formula = "A>B && B>C";
            string[] elementos = Utils.SeparaEmElementos(formula);
            Assert.IsTrue(elementos.Length == 3);
            Assert.IsTrue(elementos[0] == "A>B");
            Assert.IsTrue(elementos[1] == "&&");
            Assert.IsTrue(elementos[2] == "B>C");

            formula = "(A>B && B>C) || A<S";
            elementos = Utils.SeparaEmElementos(formula);
            Assert.IsTrue(elementos.Length == 3);
            Assert.IsTrue(elementos[0] == "(A>B && B>C)", elementos[0]);
            Assert.IsTrue(elementos[1] == "||");
            Assert.IsTrue(elementos[2] == "A<S");

            formula = "((A>B && B>C) || A<S) && (A(a)>d(d&&c(-1)) && D>A(GH))";
            elementos = Utils.SeparaEmElementos(formula);
            Assert.IsTrue(elementos.Length == 3);
            Assert.IsTrue(elementos[0] == "((A>B && B>C) || A<S)", elementos[0]);
            Assert.IsTrue(elementos[1] == "&&");
            Assert.IsTrue(elementos[2] == "(A(a)>d(d&&c(-1)) && D>A(GH))");

        }

        [TestMethod]
        public void TestCondicaoComplexa()
        {
            facade.LoadAtivo("PETR4", 100, Consts.PERIODO_ACAO.DIARIO, "dados/petr4-diario.js");
            Ativo ativo = facade.GetAtivo("PETR4");

            Config config = new Config();
            TradeSystem ts = new TradeSystem(config);
            /*
             * Exemplos:
             * C>O
             * 
             * !C>O
             * 
             * C>O||C<REF(C,1)
             * 
             * (C>O||C<REF(C,1))&&RSI(C,9)>7
             * 
             *
             */

            string strFormulaA = "MME(C,9)";
            string strFormulaB = "MMS(C,9)";
            string strFormulaC = "MMS(C,3)";

            ICondicao cond1 = new CondicaoComplexa(config, strFormulaA + ">" + strFormulaB);
            FormulaManager fm = facade.formulaManager;
            Candle candle = ativo.firstCandle;
            candle.SetValor(strFormulaA, 10);
            candle.SetValor(strFormulaB, 20);
            candle.SetValor(strFormulaC, 5);
            Assert.IsFalse(cond1.VerificaCondicao(candle, ts));
            candle.SetValor(strFormulaA, 30);
            Assert.IsTrue(cond1.VerificaCondicao(candle, ts));

            ICondicao cond2 = new CondicaoComplexa(config, strFormulaA + ">" + strFormulaB + "&&" + strFormulaB + ">" + strFormulaC);
            Assert.IsTrue(cond2.VerificaCondicao(candle, ts));
            candle.SetValor(strFormulaC, 30);
            Assert.IsFalse(cond2.VerificaCondicao(candle, ts));
            candle.SetValor(strFormulaC, 5);
            Assert.IsTrue(cond2.VerificaCondicao(candle, ts));
            candle.SetValor(strFormulaA, 10);
            Assert.IsFalse(cond2.VerificaCondicao(candle, ts));
        }

        [TestMethod]
        public void TestCondicao()
        {
            Config config = new Config();
            Condicao cond = new Condicao(config, "MME(C,9)>MME(C,3)");
            Assert.IsTrue(cond.cond1 == "MME(C,9)");
            Assert.IsTrue(cond.cond2 == "MME(C,3)");
            Assert.IsTrue(cond.operador == ConstsComuns.Operador.GREATER);

            cond = new Condicao(config, "MME(C,19)<=MME(C,31)");
            Assert.IsTrue(cond.cond1 == "MME(C,19)");
            Assert.IsTrue(cond.cond2 == "MME(C,31)");
            Assert.IsTrue(cond.operador == ConstsComuns.Operador.LOWER_EQ);
        }

        [TestMethod]
        public void TestTradeSystem()
        {
            facade = new FacadeBacktester();
            Config config = new Config();
            TradeSystem tradeSystem = new TradeSystem(config);
            MonteCarlo mc = new MonteCarlo("teste");

            float valorInicial = 100000;
            Carteira carteira = new Carteira(facade, valorInicial, config, tradeSystem,mc);
            config.custoOperacao = 20f;

            tradeSystem.condicaoEntradaC = "GREATER(MME(C,9),MME(C,3))";
            tradeSystem.condicaoSaidaC = "LOWER(MME(C,9),MME(C,3))";
            tradeSystem.condicaoEntradaV = "LOWER(MME(C,9),MME(C,3))";

            Periodo periodo = new Periodo("01-01-2017");
            Ativo ativo = new Ativo(facade, "TESTE", 100);
            Candle candle = new Candle(periodo, ativo);
            ativo.firstCandle = candle;
            candle.SetValor("MME(C,9)", 10);
            candle.SetValor("MME(C,3)", 5);
            candle.SetValor("SUBTRACT(L,MULTIPLY(STD(C,10),2))", 2);
            candle.SetValor("H", 10);
            candle.SetValor(tradeSystem.stopInicialC, 5);

            Formula formulaGreater = facade.formulaManager.GetFormula(tradeSystem.condicaoEntradaC);
            float value = formulaGreater.Calc(candle);
            Assert.IsTrue(value > 0, value + "<=0");

            Formula formulaLower = facade.formulaManager.GetFormula(tradeSystem.condicaoEntradaV);
            value = formulaLower.Calc(candle);
            Assert.IsTrue(value == 0, value + ">0");

            float result = tradeSystem.checaCondicaoEntrada(candle, config);
            Assert.IsTrue(result > 0, "result:" + result);

            candle.SetValor("MME(C,9)", 2);
            candle.SetValor("C", 2);
            candle.RemoveValor(tradeSystem.condicaoEntradaC);

            result = tradeSystem.checaCondicaoEntrada(candle, config);
            Assert.IsTrue(result == 0, "result:" + result);
        }

    }
}
