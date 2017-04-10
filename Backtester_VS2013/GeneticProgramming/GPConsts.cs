
using System;
namespace GeneticProgramming
{
    public class GPConsts
    {

        public static string COMP_DIF = UsoComum.ConstsComuns.Operador.DIFFERENT.ToString();
        public static string COMP_EQUAL = UsoComum.ConstsComuns.Operador.EQUAL.ToString();
        public static string COMP_GREATER = UsoComum.ConstsComuns.Operador.GREATER.ToString();
        public static string COMP_GREATER_EQ = UsoComum.ConstsComuns.Operador.GREATER_EQ.ToString();
        public static string COMP_LOWER = UsoComum.ConstsComuns.Operador.LOWER.ToString();
        public static string COMP_LOWER_EQ = UsoComum.ConstsComuns.Operador.LOWER_EQ.ToString();

        public const string OPER_ADD = "ADD";
        public const string OPER_SUBTRACT = "SUBTRACT";
        public const string OPER_MULTIPLY = "MULTIPLY";
        public const string OPER_DIVIDE = "DIVIDE";

        public static string BOOL_AND = UsoComum.ConstsComuns.BOOLEAN_TYPE.AND.ToString();
        public static string BOOL_OR = UsoComum.ConstsComuns.BOOLEAN_TYPE.OR.ToString();
        public static string BOOL_XOR = UsoComum.ConstsComuns.BOOLEAN_TYPE.XOR.ToString();
        public static string BOOL_NOT = UsoComum.ConstsComuns.BOOLEAN_TYPE.NOT.ToString();//NYI

        public const string NUMBER_DEFAULT = "NUMBER_DEFAULT";
        public const string NUMBER_BOOLEAN = "NUMBER_BOOLEAN";
        public const string NUMBER_PERCENTUAL = "NUMBER_PERCENTUAL";
        public const string NUMBER_GRANDE = "NUMBER_GRANDE";

        public const string LISTA_FORMULA = "formulas";
        public const string LISTA_NUMEROS = "numeros";
        public const string LISTA_NUMEROS_BOOLEANOS = "LISTA_NUMEROS_BOOLEANOS";
        public const string LISTA_NUMEROS_GRANDES = "LISTA_NUMEROS_GRANDES";
        public const string LISTA_NUMEROS_PERCENTUAIS = "LISTA_NUMEROS_PERCENTUAIS";



        [Flags]
        public enum GPNODE_TYPE
        {
            NONE = 0,
            NODE_FORMULA = 1,
            NODE_NUMBER = 2,
            NODE_COMPARER = 4,
            NODE_BOOLEAN = 8,
            NODE_OPERATOR = 16
        }
    }
}
