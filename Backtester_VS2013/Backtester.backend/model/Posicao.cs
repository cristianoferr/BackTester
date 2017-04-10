
using Backtester.backend.model.ativos;
using Backtester.backend.model.system.condicoes;
using System.Collections.Generic;
using System.Linq;
namespace Backtester.backend.model
{
    public class Posicao
    {
        private Carteira carteira;
        public Ativo ativo { get; private set; }
        public IList<Operacao> operacoesAbertas { get; private set; }
        public IList<Operacao> operacoesFechadas { get; private set; }
        public int direcao { get; set; }

        public Posicao(Carteira carteira, Ativo ativo)
        {
            this.carteira = carteira;
            this.ativo = ativo;
            operacoesAbertas = new List<Operacao>();
            operacoesFechadas = new List<Operacao>();
            saldo = 0;
            direcao = 0;
        }

        public int countAcoes
        {
            get
            {
                return operacoesFechadas.Sum(x => x.qtd);
            }

        }

        public int saldo { get; set; }

        /*
     * Esse método vai efetuar a compra de X ações do Ativo no período.
     * Perc=percentual do capital que eu posso comprar, usado para fazer multiplas entradas:
     * 1=1 entrada
     * 0.5=2 entradas
     * 0.33= 3 entradas
     * 
     * direcao: 1 = compra
     * 			2 = venda 
    */
        public float EfetuaEntrada(Periodo periodo, float perc, int direcao, float valorAcao, float vlrStop)
        {
            Candle candle = ativo.GetCandle(periodo);
            if (candle == null)
            {
                return 0;
            }
            if (this.direcao == 0) this.direcao = direcao;

            //Se estou na venda não posso operar na compra e vice-versa
            if (this.direcao != direcao)
            {
                return 0;
            }

            int qtd = (int)carteira.QueryQtdAcoes(valorAcao, vlrStop, perc, periodo);
            //cancelando trades lixos... 
            //TODO: adicionar um erro critico no tradesystem

            carteira.monteCarlo.AnalisaEntrada(direcao, valorAcao, vlrStop, qtd > 0 ? qtd : 0);

            if (direcao > 0 && vlrStop >= valorAcao)
            {
                return 0;
            }
            if (direcao < 0 && vlrStop <= valorAcao)
            {
                return 0;
            }

            if (qtd <= 0) return 0;


            saldo += qtd;
            //System.out.println("Efetuando entrada no "+ativo.getName()+" em "+periodo+" a "+formatCurrency(valorAcao)+"x"+qtd+"="+formatCurrency(valorAcao*qtd)+" stop em: "+formatCurrency(vlrStop)+" saldo:"+saldo+" direcao:"+direcao);
            Operacao oper = new Operacao(carteira, candle, valorAcao, vlrStop, qtd, direcao, carteira.tradeSystem.GetStopMovel(direcao));
            operacoesAbertas.Add(oper);
            return qtd * valorAcao;
        }

        public float GetCapital(Periodo periodo)
        {
            Candle c = ativo.GetCandle(periodo);

            //Caso não tenha candle, tenta pegar o anterior... 
            if (c == null) c = ativo.GetCandle(periodo.periodoAnterior);

            return saldo * c.GetValor(carteira.config.campoVenda);
        }

        public void VerificaStops(Candle candle)
        {
            for (int i = operacoesAbertas.Count - 1; i >= 0; i--)
            {
                Operacao oper = operacoesAbertas[i];
                float vlr = oper.atingiuStop(candle.periodo);

                //Se vlr > 0 então o stop foi atingido
                if (vlr > 0)
                {
                    //	System.out.println("Stop atingido em "+ativo.getName()+" no dia "+candle.getPeriodo()+" no valor de "+vlr);
                    oper.stopado = true;
                    carteira.FechaOperacao(this, candle, oper, vlr);
                }
            }

        }

        public void FechaOperacao(Operacao oper, Candle candle, float vlrSaida)
        {
            saldo -= oper.qtd;
            oper.vlrSaida = vlrSaida;
            oper.candleFinal = candle;
            operacoesAbertas.Remove(oper);
            operacoesFechadas.Add(oper);

        }


        internal float GetCapitalSobRisco(Periodo periodo)
        {
            float capitalSobRisco = 0;

            foreach (Operacao oper in operacoesAbertas)
            {
                capitalSobRisco += oper.GetCapitalSobRisco(periodo);
            }
            return capitalSobRisco;
        }
    }
}
