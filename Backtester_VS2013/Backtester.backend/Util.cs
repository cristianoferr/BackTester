using System;
namespace Backtester.backend
{
    public class Util
    {


        internal static string FormatCurrency(float valor)
        {
            return valor.ToString();
        }


        public static bool IsNumber(string input)
        {
            try
            {
                float n;
                return float.TryParse(input, out n);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static Backtester.backend.Consts.Operador ConverteOperador(string oper)
        {
            if (oper == "=") return Backtester.backend.Consts.Operador.EQUAL;
            if (oper == ">") return Backtester.backend.Consts.Operador.GREATER;
            if (oper == "<") return Backtester.backend.Consts.Operador.LOWER;
            if (oper == ">=") return Backtester.backend.Consts.Operador.GREATER_EQ;
            if (oper == "<=") return Backtester.backend.Consts.Operador.LOWER_EQ;
            return Backtester.backend.Consts.Operador.DIFFERENT;
        }

        internal static void println(string p)
        {
            Console.WriteLine(p);

        }


    }

}
