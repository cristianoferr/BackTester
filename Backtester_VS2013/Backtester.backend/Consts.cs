﻿
using System.ComponentModel;
namespace Backtester.backend
{
    public class Consts
    {

        public const string VAR_STOP_GAP = "STOP_GAP";
        public const string VAR_RISCO_TRADE = "RISCO_TRADE";
        public const string VAR_STOP_MENSAL = "STOP_MENSAL";
        public const string VAR_MAX_CAPITAL_TRADE = "MAX_CAPITAL_TRADE";
        public const string VAR_PERC_TRADE = "PERC_TRADE";
        public const string VAR_USA_STOP_MOVEL = "USA_STOP_MENSAL";

        public enum TIPO_OPERACAO{
            UNDEF=0,
            COMPRA=1,
            VENDA=-1
        }

        public enum NODE_TYPE
        {
            [Description("&&")]
            AND = 0,
            [Description("||")]
            OR = 1,
            [Description("!")]
            NOT = 2
        }
        public enum PERIODO_ACAO
        {
            DIARIO, SEMANAL
        }

        public enum Operador
        {
            [Description("=")]
            EQUAL = 0,
            [Description("<")]
            LOWER = 1,
            [Description("<=")]
            LOWER_EQ = 2,
            [Description(">")]
            GREATER = 3,
            [Description(">=")]
            GREATER_EQ = 4,
            [Description("!=")]
            DIFFERENT = 5
        }

        public enum OrdemEstatistica
        {
            CAPITAL_FINAL,
            TRADES,
            PERC_ACERTO
        }

        
    }
}
