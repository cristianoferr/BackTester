
namespace Backtester.backend
{
    public class Consts
    {
        public enum PERIODO_ACAO
        {
            DIARIO, SEMANAL
        }

        public enum Operador
        {
            LOWER, LOWER_EQ, GREATER, GREATER_EQ, EQUAL, DIFFERENT
        }

        public enum OrdemEstatistica
        {
            CAPITAL_FINAL,
            TRADES,
            PERC_ACERTO
        }
    }
}
