﻿using Backtester.backend.model.system.estatistica;
using System;
using UsoComum;

namespace Backtester.backend.model.system
{
    public class MonteCarlo
    {
        string name;
        Estatistica global;
        public float fitness = 0;
        public MonteCarlo(string name)
        {
            this.name = name;
            global = new Estatistica(0);
            fitness = 0;
        }

        public void setEstatistica(Estatistica stat)
        {
            global = stat;

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

        internal void AnalisaEntrada(int direcao, float valorAcao, float vlrStop)
        {
            if (vlrStop == 0)
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

            float difPerc = Math.Abs(valorAcao / vlrStop*100);
            if (difPerc > MAX_DISTANCE_VLR_ENTRADA_VLR_STOP)
            {
                ERROR_DISTANCIA_SUPERADA = true;
            }
        }

        bool ERROR_STOP_0=false;
        bool ERROR_VLR_STOP_ERRADO=false;
        bool ERROR_DISTANCIA_SUPERADA=false;

        public void FinishStats()
        {
            if (ERROR_STOP_0) fitness -= PENALTY;
            if (ERROR_VLR_STOP_ERRADO) fitness -= PENALTY;
            if (ERROR_DISTANCIA_SUPERADA) fitness -= PENALTY;

            if (qtdTrades == 0)
            {
                fitness -= PENALTY*100;
            }

            fitness += BONUS * percAcerto;

            int difTrades = QTD_MINIMA_TRADES - qtdTrades;
            if (difTrades<0)difTrades=0;
            fitness -= difTrades * PENALTY / QTD_MINIMA_TRADES*10;


            float avgDias = global.getGeral().getAmbasPontas().getTodosTrades().getAvgDias();
            //bonus até 50 dias...
            if (avgDias < AVG_DIAS_GOAL)
            {
                fitness += BONUS * avgDias;
            }

            fitness /= 10;
        }

        public const int AVG_DIAS_GOAL = 50;
        public const int MAX_DISTANCE_VLR_ENTRADA_VLR_STOP = 20;

        public const int BONUS = 1000;
        public const int PENALTY = 100000;
        public const int PERC_MINIMA_ACERTO = 50;
        public const int QTD_MINIMA_TRADES = 100;

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
            return global.getCapitalFinal();
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

       
    }
}
