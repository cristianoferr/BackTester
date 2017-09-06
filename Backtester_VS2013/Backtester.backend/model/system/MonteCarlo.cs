using Backtester.backend.model.system.condicoes;
using Backtester.backend.model.system.estatistica;
using System;
using System.Collections.Generic;
using UsoComum;
using UsoComum.interfaces;

namespace Backtester.backend.model.system
{
    public class MonteCarlo
    {
        string name;
        Estatistica global;
        public float fitness = 0;
        public IList<Operacao> operacoes { get; set; }
        public MonteCarlo(string name)
        {
            this.name = name;
            global = new Estatistica(0);
            fitness = 0;
            operacoes = new List<Operacao>();
        }

        public void setEstatistica(Estatistica stat)
        {
            global = stat;

        }


        internal void AddOperacao(condicoes.Operacao oper)
        {
            operacoes.Add(oper);
        }

        public override string ToString()
        {
            return name;
        }


        public void printEstatisticas()
        {
            Utils.println(global.getDesc() + "=>" + global.getPerformance());
        }

        public void printPerformance(string s)
        {
            Utils.println(s + ": " + name + "=>" + global.getMaxMinCapital() + " Trades:" + global.getGeral().getAmbasPontas().getTodosTrades().getnTrades());
        }

        int totalQtdAcoes = 0;

        float totalCarteiraLiquido = 0;
        float totalCarteiraEmPosicao = 0;
        int totalPeriodos = 0;

        internal void AnalizaPeriodo(Carteira carteira)
        {
            totalCarteiraLiquido += carteira.capitalLiq;
            totalCarteiraEmPosicao += carteira.capitalPosicao;
            totalPeriodos++;
        }

        internal void AnalisaEntrada(int direcao, float valorAcao, float vlrStop, int qtd)
        {
            if (vlrStop == 0 || float.IsNaN(vlrStop))
            {
                ERROR_STOP_0 = true;
                return;
            }
            if (direcao > 0 && vlrStop >= valorAcao)
            {
                ERROR_VLR_STOP_ERRADO = true;
                return;
            }
            if (direcao < 0 && vlrStop <= valorAcao)
            {
                ERROR_VLR_STOP_ERRADO = true;
                return;
            }

            float difPerc = Math.Abs((valorAcao / vlrStop - 1) * 100);
            if (difPerc > MAX_DISTANCE_VLR_ENTRADA_VLR_STOP)
            {
                if (difPerc > ERROR_DISTANCIA_SUPERADA) ERROR_DISTANCIA_SUPERADA = difPerc;
            }
            totalQtdAcoes += qtd;
        }

        bool ERROR_STOP_0 = false;
        bool ERROR_VLR_STOP_ERRADO = false;
        float ERROR_DISTANCIA_SUPERADA = 0;

        public void FinishStats(Carteira carteira)
        {
            if (ERROR_STOP_0) fitness -= PENALTY * 100;
            if (ERROR_VLR_STOP_ERRADO) fitness -= PENALTY * 100;
            if (ERROR_DISTANCIA_SUPERADA > 0) fitness -= PENALTY * ERROR_DISTANCIA_SUPERADA * 100;

            if (qtdTrades == 0)
            {
                fitness -= PENALTY * 100;
            }

            //
            if (global.maxCapital > carteira.capitalInicial)
            {
                float difMaxCapital = (global.maxCapital - carteira.GetCapital()) * 10;
                fitness -= difMaxCapital;
            }

            //Conta a variedade nas ações ganhadoras: acerta 2 ações é melhor que 1 ação
            fitness += CalcBonusVariedade(carteira);

            fitness += percAcerto * BONUS*10;

            float difCapital = carteira.GetCapital() - carteira.capitalInicial;
            fitness += BONUS * difCapital / 100;
            if (difCapital == 0) fitness -= PENALTY * 10;


            int difTrades = QTD_MINIMA_TRADES - qtdTrades;
            if (difTrades > 0) fitness -= PENALTY * difTrades * 100;
            /*//só dou bonus de acerto para os que estiverem acima do objetivo de qtd minima de trades
            if (difTrades == 0)
            {
                FitnessQtdAcoes();
                FitnessPercUsoCapital();
                fitness += BONUS * percAcerto / 10;
                float avgDias = global.getGeral().getAmbasPontas().getTodosTrades().getAvgDias();
                //bonus até 50 dias...
                if (avgDias < AVG_DIAS_GOAL)
                {
                    fitness += BONUS;
                }
            }
            */




            fitness /= 10;
        }

        private float CalcBonusVariedade(Carteira carteira)
        {
            //carteira.posicoesFechadas.
            float totalGanho=0;
            int countGanho = 0;
            List<string> ativosVencedores = new List<string>();
            foreach (Posicao pos in carteira.posicoesFechadas)
            {
                foreach (Operacao oper in pos.operacoesFechadas)
                {
                    if (oper.GetDif() > 0)
                    {
                        if (!ativosVencedores.Contains(pos.ativo.name))
                        {
                            ativosVencedores.Add(pos.ativo.name);
                        }
                        countGanho++;
                        totalGanho += oper.GetDif();
                    }
                }
            }
            float bonusWinAvg = 0;
            float avgGanho = 0;
            if (countGanho > 0)
            {
                avgGanho = totalGanho / countGanho;
                bonusWinAvg = avgGanho*10;
            }
            

            return ativosVencedores.Count*BONUS*10+bonusWinAvg;
        }

        private void FitnessPercUsoCapital()
        {
            //quanto maior o percentual de uso, melhor (significa que estou usando melhor os fundos)
            fitness += capitalUsePercent * BONUS;
        }

        private void FitnessQtdAcoes()
        {
            //Bonus para quantidade de acoes...
            if (qtdTrades > 0)
            {
                float avgAcoes = totalQtdAcoes / qtdTrades / 100;
                if (avgAcoes <= 2)
                {
                    fitness -= PENALTY;
                }
                else
                {
                    fitness += BONUS * avgAcoes;
                }
            }
        }

        public float capitalUsePercent
        {
            get
            {
                return totalCarteiraEmPosicao / (totalCarteiraEmPosicao + totalCarteiraLiquido) * 100;
            }
        }

        public const int AVG_DIAS_GOAL = 50;
        public const int MAX_DISTANCE_VLR_ENTRADA_VLR_STOP = 20;

        public const int BONUS = 10000;
        public const int PENALTY = 10000;
        public const int PERC_MINIMA_ACERTO = 50;
        public const int QTD_MINIMA_TRADES = 25;

        public int qtdTrades
        {
            get
            {
                return global.getGeral().getAmbasPontas().getTodosTrades().getnTrades();
            }
        }

        public float winLossRatio
        {
            get
            {
                return global.getGeral().getAmbasPontas().winLossRatio;
            }
        }

        public float percAcerto
        {
            get
            {
                return global.getGeral().getAmbasPontas().percAcerto;
            }
        }


        public float totalGanho
        {
            get
            {
                return global.getGeral().getAmbasPontas().totalGanho;
            }
        }
        public float totalPerdido
        {
            get
            {
                return global.getGeral().getAmbasPontas().totalPerdido;
            }
        }

        public float getCapitalFinal()
        {
            return global.capitalFinal;
        }

        public float getMaxCapital()
        {
            return global.getMaxCapital();
        }

        public float getMinCapital()
        {
            return global.getMinCapital();
        }

        public Estatistica getGlobal()
        {
            return global;
        }

        public float CalcFitness()
        {
            return getCapitalFinal() + fitness;
        }






        public IStoreProperties properties { get; set; }
    }
}
