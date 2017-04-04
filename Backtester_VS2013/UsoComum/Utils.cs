using System;
using System.ComponentModel;
using System.Reflection;
namespace UsoComum
{
    public class Utils
    {




        static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Info(string msg)
        {
            log.Info(msg);
        }

        public static void Error(string msg)
        {
            log.Error(msg);
        }

        public static string FormatCurrency(float valor)
        {
            return valor.ToString("0.00");
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

        public static ConstsComuns.Operador ConverteOperador(string oper)
        {
            if (oper == "=") return ConstsComuns.Operador.EQUAL;
            if (oper == ">") return ConstsComuns.Operador.GREATER;
            if (oper == "<") return ConstsComuns.Operador.LOWER;
            if (oper == ">=") return ConstsComuns.Operador.GREATER_EQ;
            if (oper == "<=") return ConstsComuns.Operador.LOWER_EQ;
            return ConstsComuns.Operador.DIFFERENT;
        }

        public static void println(string p)
        {
            Utils.Info(p);

        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string[] SeparaEmElementos(string par)
        {
            string[] pars = new string[3];
            int p = 0;
            int flagParenteses = 0;
            string s = "";
            for (int i = 0; i < par.Length; i++)
            {
                if (par[i] == '(') flagParenteses++;
                if (par[i] == ')')
                {
                    flagParenteses--;
                }
                if (i < par.Length - 1 && flagParenteses == 0 && ((par[i] == '|' && par[i + 1] == '|') || (par[i] == '&' && par[i + 1] == '&')))
                {
                    pars[p] = s;
                    p++;
                    pars[p] = "" + par[i] + par[i + 1];
                    p++;
                    s = "";
                    i++;
                }
                else
                    s = s + par[i];
            }
            pars[p] = s;
            for (int i = 0; i < pars.Length; i++)
            {
                if (pars[i] != null)
                {
                    if (pars[i].EndsWith(".0")) pars[i] = pars[i].Replace(".0", "");
                    pars[i] = pars[i].Trim();
                }
            }
            return pars;
        }

        public static DateTime UnixTimeStampToDateTime(Int64 unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }

}
