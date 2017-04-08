
using GeneticProgramming.nodes;
using System.Runtime.Serialization;
namespace GeneticProgramming.semantica
{
    [DataContract]
    public class GPSemanticaFormula : GPSemantica
    {
        public GPSemanticaFormula(string name, int minParams, int maxParams, GPConsts.GPNODE_TYPE nodeType = GPConsts.GPNODE_TYPE.NODE_FORMULA)
            : base(name, nodeType, minParams, maxParams)
        {
        }




        internal override nodes.GPAbstractNode InstantiateEmpty()
        {
            return new GPNode(this);
        }

    }
}
