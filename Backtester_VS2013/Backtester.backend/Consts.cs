
using System.ComponentModel;
namespace Backtester.backend
{
    public class Consts
    {

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
