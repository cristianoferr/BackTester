﻿
using System.ComponentModel;
namespace UsoComum
{
    public class ConstsComuns
    {

        public const string OBJ_TRADESYSTEM = "TRADESYSTEM";
        public const string OBJ_MONTECARLO = "MONTERCARLO";
        public const string OBJ_TOTAL_PROFIT = "TOTAL_PROFIT";
        public const string OBJ_TOTAL_LOSS = "TOTAL_LOSS";
        public const string OBJ_ITERATIONS = "ITERATIONS";

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

        public enum BOOLEAN_TYPE
        {
            [Description("&&")]
            AND = 0,
            [Description("||")]
            OR = 1,
            [Description("!")]
            NOT = 2,
            [Description("^")]
            XOR = 3
        }


    }
}
