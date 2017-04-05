
using System;
using System.Runtime.Serialization;
namespace GeneticProgramming.nodes
{
    [DataContract]
    public class GPNode : GPAbstractNode
    {

        public GPNode(semantica.GPSemantica semantica)
            : base(semantica, GPConsts.GPNODE_TYPE.NODE_FORMULA)
        {
            if (semantica == null)
            {
                throw new Exception("Semantica nula!");
            }
        }
    }
}
