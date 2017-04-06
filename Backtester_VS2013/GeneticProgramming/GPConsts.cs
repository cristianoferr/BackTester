
using System;
namespace GeneticProgramming
{
    public class GPConsts
    {

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
