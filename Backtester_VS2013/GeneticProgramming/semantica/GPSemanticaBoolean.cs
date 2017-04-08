
using GeneticProgramming.nodes;
using System.Runtime.Serialization;
namespace GeneticProgramming.semantica
{
    [DataContract]
    public class GPSemanticaBoolean : GPSemantica
    {
        public GPSemanticaBoolean(string name, int minParams = 2, int maxParams = 5)
            : base(name, GPConsts.GPNODE_TYPE.NODE_BOOLEAN, minParams, maxParams)
        {
        }


        internal override nodes.GPAbstractNode InstantiateEmpty()
        {
            return new GPNode(this);
        }

    }
}
