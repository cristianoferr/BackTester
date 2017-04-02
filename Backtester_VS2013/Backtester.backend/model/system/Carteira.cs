
using Backtester.backend.model.ativos;
using Backtester.backend.model.system.condicoes;
using Backtester.backend.model.system.estatistica;
using System;
using System.Collections.Generic;
namespace Backtester.backend.model
{
    public class Carteira
    {

        Dictionary<Ativo, Posicao> posicoesAbertas;
        private int p;
        public system.Config config { get; private set; }
        public IList<Posicao> posicoesFechadas {get;private set;}
        public Estatistica estatistica { get; private set; }
        public Carteira(FacadeBacktester facade, float capitalInicial, system.Config config, system.TradeSystem tradeSystem)
        {
            this.facade = facade;
            this.capitalInicial = capitalInicial;
            this.capitalLiq = capitalInicial;
            posicoesAbertas = new Dictionary<Ativo, Posicao>();
            this.config = config;
            posicoesFechadas = new List<Posicao>();
            capitalPosicao = 0;
            this.tradeSystem = tradeSystem;
            estatistica = new Estatistica(capitalInicial);
            capitalMes = capitalInicial;
        }
        public system.TradeSystem tradeSystem { get; set; }

        public float capitalInicial { get; set; }
        public float capitalMes { get; private set; }
        public float capitalPosicao { get; private set; }

        public float capitalLiq { get; set; }

        public Posicao GetPosicaoDoAtivo(Ativo ativo)
        {
            if (posicoesAbertas.ContainsKey(ativo))
            {
                return posicoesAbertas[ativo];
            }
            Posicao posicao = new Posicao(this, ativo);
            posicoesAbertas.Add(ativo, posicao);
            return posicao;
        }


        public FacadeBacktester facade { get; set; }


        /*
 * Efetua a entrada no ativo e deduz do saldo o capital do trade 
 * não importando se é compra ou venda
 */
        public void EfetuaEntrada(Ativo ativo, Periodo periodo, float perc, float vlrEntrada, float vlrStop, int direcao)
        {
            Posicao posicao = null;
            if (!posicoesAbertas.ContainsKey(ativo))
            {
                posicao = new Posicao(this, ativo);
                posicoesAbertas.Add(ativo, posicao);
            }
            else
            {
                posicao = posicoesAbertas[ativo];
            }
            MovimentaSaldo(-posicao.EfetuaEntrada(periodo, perc, direcao, vlrEntrada, vlrStop));
        }

        /*
     * Efetuo a saida no ativo no periodo determinado usando o vlrSaida caso determinado, se não, pego o vlrSaida padrao
     * vlrSaida será informado quando for stopado
     */
        public void EfetuaSaida(Ativo ativo, Periodo periodo, float vlrSaida)
        {
            Posicao posicao = posicoesAbertas[ativo];
            Candle candle = posicao.ativo.GetCandle(periodo);
            if (vlrSaida == 0) vlrSaida = candle.GetValor(config.campoVenda);
            FechaPosicao(posicao, candle, vlrSaida);

        }

        List<Posicao> posicoesARemover_ = new List<Posicao>();
        public void FechaPosicoes(Periodo periodo)
        {
            posicoesARemover_.Clear();
            foreach (Ativo key in posicoesAbertas.Keys)
            {
                Posicao p = posicoesAbertas[key];
                Candle candle = p.ativo.GetCandle(periodo);
                float vlrSaida = candle.GetValor(config.campoVenda);
                posicoesARemover_.Add(FechaPosicao(p, candle, vlrSaida,false));
            }

            foreach (Posicao p in posicoesARemover_)
            {
                posicoesAbertas.Remove(p.ativo);
            }
        }

        public Posicao FechaPosicao(Posicao posicao, Candle candle, float vlrSaida,bool okToRemove=true)
        {


            for (int i = posicao.operacoesAbertas.Count - 1; i >= 0; i--)
            {
                Operacao oper = posicao.operacoesAbertas[i];
                //posicao.fechaOperacao(oper,candle, vlrSaida);
                FechaOperacao(posicao, candle, oper, vlrSaida);

            }
            if (okToRemove)
                 posicoesAbertas.Remove(posicao.ativo);
            posicoesFechadas.Add(posicao);
            return posicao;
        }


        public void FechaOperacao(Posicao posicao, Candle candle, Operacao oper, float vlrSaida)
        {
            float vlr = oper.qtd * vlrSaida;

            posicao.FechaOperacao(oper, candle, vlrSaida);
            estatistica.AddTrade(oper, candle.periodo);

            //movimentaSaldo(vlr);
            float dif = vlr - oper.vlrEntrada * oper.qtd;
            dif *= posicao.direcao;
            if (oper.direcao > 0)
            {
                MovimentaSaldo(vlr);
            }
            else
            {
                MovimentaSaldo(oper.vlrEntrada * oper.qtd + dif);
            }
            //System.out.println("Fechando posicao em "+posicao.ativo+" em "+candle.getPeriodo()+ " a "+formatCurrency(vlrSaida)+"x"+oper.qtd+"="+formatCurrency(vlrSaida*oper.qtd)+" dif:"+formatCurrency(dif)+" Stop? "+oper.isStopado()+" direcao:"+posicao.direcao+" saldo:"+getSaldo());

        }

        public void MovimentaSaldo(float valor)
        {
            if (valor == 0) return;
            capitalLiq += valor - config.custoOperacao;
            //		System.out.println("Saldo movimentado: "+valor+" liguido:"+capitalLiq);
        }

        //Essa função retorna o quanto de capital eu posso usar no trade com base nos parametros de configuração
        //Não posso por exemplo comprar mais que o risco permite ou mais capital do que eu tenho líquido.
        public float CalculaCapitalTrade(float percentualOperacao)
        {
            float capitalTrade = GetCapital() * tradeSystem.percTrade/100 * percentualOperacao;

            float maxCapitalTrade = tradeSystem.maxCapitalTrade;
            //Caso o capital seja maior que o maximo capital então limita o capital
            if ((capitalTrade > maxCapitalTrade) && (maxCapitalTrade > 0))
                capitalTrade = maxCapitalTrade;

            if (capitalTrade > capitalLiq) capitalTrade = capitalLiq;
            return capitalTrade;
        }

        internal int QueryQtdAcoes(float vlrEntrada, float vlrStop, float perc)
        {
            float capitalTrade = CalculaCapitalTrade(perc);
            float qtd = capitalTrade / vlrEntrada;

            qtd = CalculaQtdTrade(qtd, vlrStop, vlrEntrada);

            float qtdN = (float)(Math.Round(qtd / 100) * 100);
            if (qtdN > qtd) qtdN = qtdN - 100;
            qtd = qtdN;
            return (int)qtd;
        }


        /*
     * Essa função serve para limitar baseado no risco do trade... se for muito arriscado então a quantidade é menor.
     * O Stop não é alterado apenas a quantidade é reduzida.
     */
        public float CalculaQtdTrade(float qtd, float vlrStop, float vlrEntrada)
        {
            //Limita pelo risco por trade (limita quanto posso arriscar baseado no stop inicial e no percentual do capital)
            float riscoTrade = tradeSystem.riscoTrade;
            if (riscoTrade > 0)
            {
                float vlrRisco = riscoTrade * GetCapital()/100;
                float qtdR = Math.Abs((vlrRisco - 2 * config.custoOperacao) / (vlrEntrada - vlrStop));
                if (qtdR < qtd) qtd = qtdR;
            }

            //Calculo quanto poderia arriscar para não passar o stop mensal
            if (tradeSystem.stopMensal > 0)
            {
                float vlrRisco = GetCapital() - capitalMes * (1 - tradeSystem.stopMensal/100);
                float qtdR = Math.Abs((vlrRisco - 2 * config.custoOperacao) / (vlrEntrada - vlrStop));
                if (vlrRisco < 0) qtdR = 0;
                if (qtdR < qtd)
                    qtd = qtdR;
            }
            if (qtd < 100) return 0;
            return qtd;
        }

        public float GetCapital()
        {
            return capitalLiq + capitalPosicao;

        }

        public void EndTurn(Periodo periodo, bool flagEndMes)
        {
            periodoAtual = periodo;
            AtualizaPosicao();
            estatistica.AtualizaPeriodo(periodo, GetCapital());
            if (flagEndMes) capitalMes = GetCapital();
        }

        public void AtualizaPosicao()
        {
            capitalPosicao = 0;
            foreach (Posicao p in posicoesAbertas.Values)
            {
                Candle candle = p.ativo.GetCandle(periodoAtual);
                if (candle != null) p.VerificaStops(candle);
                capitalPosicao += p.GetCapital(periodoAtual);

            }
        }


        public Periodo periodoAtual { get; set; }

        internal int PossuiAtivo(Ativo ativo)
        {

            if (!posicoesAbertas.ContainsKey(ativo)) return 0;
            Posicao p = posicoesAbertas[ativo];
            return p.saldo * p.direcao;
        }

        internal void PrintEstatistica()
        {
            estatistica.Print();
        }
    }
}
