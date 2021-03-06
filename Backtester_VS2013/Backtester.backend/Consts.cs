﻿
namespace Backtester.backend
{
    public class Consts
    {

        public enum TIPO_CARGA_ATIVOS
        {
            GERA_CANDIDATOS=1,
            VALIDA_CANDIDATO=2,
            DADOS_ATUAIS=3
        }


        public const int MAX_CANDIDATOS = 100;

        public const string VAR_STOP_GAP = "STOP_GAP";
        public const string VAR_RISCO_TRADE = "RISCO_TRADE";
        public const string VAR_STOP_MENSAL = "STOP_MENSAL";
        public const string VAR_MAX_CAPITAL_TRADE = "MAX_CAPITAL_TRADE";
        public const string VAR_PERC_TRADE = "PERC_TRADE";
        public const string VAR_USA_STOP_MOVEL = "USA_STOP_MOVEL";
        public const string VAR_RISCO_GLOBAL = "RISCO_GLOBAL";
        public const string VAR_MULTIPLAS_ENTRADAS = "MULTIPLAS_ENTRADAS";
        public const string VAR_MULTIPLAS_SAIDAS = "MULTIPLAS_SAIDAS ";
        


        public const int QTD_MAXIMA_FORMULAS_CACHE = 30000;
        public const int QTD_ATIVOS_MOCK=200;

        public enum TIPO_OPERACAO
        {
            UNDEF = 0,
            COMPRA = 1,
            VENDA = -1
        }


        public enum PERIODO_ACAO
        {
            DIARIO, SEMANAL
        }



        public enum OrdemEstatistica
        {
            CAPITAL_FINAL,
            TRADES,
            PERC_ACERTO
        }



        
    }
}
