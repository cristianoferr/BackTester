using Backtester.backend;
using Backtester.backend.DataManager;
using Backtester.backend.model;
using Backtester.backend.model.ativos;
using Backtester.backend.model.formulas;
using Backtester.backend.model.system;
using Backtester.backend.model.system.condicoes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UsoComum;

namespace Backtester.tests
{
    [TestClass]
    public class Testes
    {

        static FacadeBacktester facade = new FacadeBacktester();


        [TestMethod]
        public void TestBacktesterVars()
        {
            Config config = new Config();
            config.capitalInicial = 100000;
            TradeSystem tradeSystem = new TradeSystem(config);

            facade.LoadAtivo("PETR4", 100, Consts.PERIODO_ACAO.DIARIO, "dados/petr4-diario.js");
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
            Posicao posicao = new Posicao(carteira, ativo);
            carteira.posicoesAbertas.Add(ativo, posicao);

            int qtd = 1000;
            posicao.saldo += qtd;
            Operacao oper1 = new Operacao(carteira, candle, 10, 9, qtd, 1, null);
            posicao.operacoesAbertas.Add(oper1);
            float capitalSobRisco = ((10 - 9) * qtd);
            float riscoEsperado = capitalSobRisco / carteira.GetCapital() * 100;
            carteira.capitalLiq -= oper1.vlrEntrada * oper1.qtd;

            candle.SetValor(config.campoVenda, 10);
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

            Ativo ativo = new Ativo(facade, "PETR4", 100);

            facade.AddAtivo(ativo);
            Ativo ativoGet = facade.GetAtivo("PETR4");
            Assert.IsNotNull(ativoGet);
            Assert.IsTrue(ativoGet == ativo);
            Assert.IsTrue(ativo.loteMin == 100);
            Assert.IsTrue(ativo.name == "PETR4");

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

            float valorInicial = 100000;
            Carteira carteira = new Carteira(facade, 100000, config, tradeSystem);
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
        public void TestFormulas()
        {
            facade.LoadAtivo("PETR4", 100, Consts.PERIODO_ACAO.DIARIO, "dados/petr4-diario.js");
            Ativo ativo = facade.GetAtivo("PETR4");

            FormulaManager fm = facade.formulaManager;


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
            Assert.IsTrue(formulaMMS.GetCode() == codigoFormula);

            float soma = 0;
            Candle candle = ativo.firstCandle;
            for (int i = 0; i < periodos; i++)
            {
                soma += candle.GetValor(fonte);
                candle = candle.proximoCandle;
            }
            candle = candle.candleAnterior;
            soma = soma / periodos;
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



            Node nodeOR = new Node(Consts.NODE_TYPE.OR);
            Node nodeAND = new Node(Consts.NODE_TYPE.AND);
            Node nodeNOT = new Node(Consts.NODE_TYPE.NOT);
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
            Config config = new Config();
            TradeSystem tradeSystem = new TradeSystem(config);

            float valorInicial = 100000;
            Carteira carteira = new Carteira(facade, valorInicial, config, tradeSystem);
            config.custoOperacao = 20f;

            tradeSystem.condicaoEntradaC.formula = "MME(C,9)>MME(C,3)";
            tradeSystem.condicaoSaidaC.formula = "MME(C,9)<MME(C,3)";
            tradeSystem.condicaoEntradaV.formula = "MME(C,9)<MME(C,3)";

            Periodo periodo = new Periodo("01-01-2017");
            Ativo ativo = new Ativo(facade, "TESTE", 100);
            Candle candle = new Candle(periodo, ativo);
            candle.SetValor("MME(C,9)", 10);
            candle.SetValor("MME(C,3)", 5);
            candle.SetValor("SUBTRACT(L,MULTIPLY(STD(C,10),2))", 2);

            float result = tradeSystem.checaCondicaoEntrada(candle, config);
            Assert.IsTrue(result > 0, "result:" + result);

            candle.SetValor("MME(C,9)", 2);

            result = tradeSystem.checaCondicaoEntrada(candle, config);
            Assert.IsTrue(result == 0, "result:" + result);
        }

    }
}
