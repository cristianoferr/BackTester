
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
    }
}
