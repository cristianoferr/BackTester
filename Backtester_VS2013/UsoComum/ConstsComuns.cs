
using System.ComponentModel;
namespace UsoComum
{
    public class ConstsComuns
    {

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
            XOR=3
        }
    }
}
