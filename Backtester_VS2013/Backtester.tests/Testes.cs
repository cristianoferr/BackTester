using Backtester.backend;
using Backtester.backend.DataManager;
using Backtester.backend.model;
using Backtester.backend.model.ativos;
using Backtester.backend.model.formulas;
using Backtester.backend.model.system;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Backtester.tests
{
    [TestClass]
    public class Testes
    {

        static FacadeBacktester facade = new FacadeBacktester();

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
        public void TestCarteira()
        {
            Config config = new Config();
            TradeSystem tradeSystem = new TradeSystem();

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
            float closeFinal = 3.2f;
            Candle candleInicial = new Candle(periodo1, ativo, 2, closeInicial, 3.5f, 1.5f, 1000);
            Candle candleFinal = new Candle(periodo1, ativo, 2, closeFinal, 3.5f, 1.5f, 1000);

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

            string codigoFormula = "MMS(C,3)";
            Formula formulaMMS = fm.GetFormula(codigoFormula);
            Assert.IsNotNull(formulaMMS);
            Assert.IsNotNull(fm.GetFormula("MME(C,3)"));
            Assert.IsTrue(formulaMMS.GetCode() == codigoFormula);

            float soma = 0;
            int valor = 3;
            string fonte = "C";
            Candle candle = ativo.firstCandle;
            for (int i = 0; i < valor; i++)
            {
                soma += candle.GetValor(fonte);
                candle = candle.proximoCandle;
            }
            soma = soma / valor;
            Assert.IsTrue(Math.Abs(candle.GetValor(codigoFormula) - soma) > 0.1f, candle.candleAnterior.GetValor(codigoFormula) + "<>" + soma);



        }

        [TestMethod]
        public void TestCondicao()
        {
            Config config = new Config();
            Condicao cond = new Condicao(config, "MME(C,9)>MME(C,3)");
            Assert.IsTrue(cond.cond1 == "MME(C,9)");
            Assert.IsTrue(cond.cond2 == "MME(C,3)");
            Assert.IsTrue(cond.operador == Consts.Operador.GREATER);

            cond = new Condicao(config, "MME(C,19)<=MME(C,31)");
            Assert.IsTrue(cond.cond1 == "MME(C,19)");
            Assert.IsTrue(cond.cond2 == "MME(C,31)");
            Assert.IsTrue(cond.operador == Consts.Operador.LOWER_EQ);
        }

        [TestMethod]
        public void TestTradeSystem()
        {
            Config config = new Config();
            TradeSystem tradeSystem = new TradeSystem();

            float valorInicial = 100000;
            Carteira carteira = new Carteira(facade, valorInicial, config, tradeSystem);
            config.custoOperacao = 20f;

            tradeSystem.condicaoEntradaC = new Condicao(config, "MME(C,9)>MME(C,3)");
            tradeSystem.condicaoSaidaC = new Condicao(config, "MME(C,9)<MME(C,3)");

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
