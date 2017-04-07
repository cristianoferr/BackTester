
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

        public const string BOOL_AND = "AND";
        public const string BOOL_OR = "OR";
        public const string BOOL_NOT = "NOT";

        public const string NUMBER_DEFAULT = "NUMBER_DEFAULT";

        public const string LISTA_FORMULA = "formulas";
        public const string LISTA_NUMEROS = "numeros";

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
