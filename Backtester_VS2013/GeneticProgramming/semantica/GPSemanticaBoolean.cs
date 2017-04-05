
using GeneticProgramming.nodes;
using System.Runtime.Serialization;
namespace GeneticProgramming.semantica
{
    [DataContract]
    public class GPSemanticaBoolean : GPSemantica
    {
        public GPSemanticaBoolean(string name)
            : base(name, GPConsts.GPNODE_TYPE.NODE_BOOLEAN)
        {
        }


        internal override nodes.GPAbstractNode InstantiateEmpty()
        {
            return new GPNode(this);
        }

    }
}
