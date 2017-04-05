
using GeneticProgramming.nodes;
using System.Runtime.Serialization;
namespace GeneticProgramming.semantica
{
    [DataContract]
    public class GPSemanticaFormula : GPSemantica
    {
        public GPSemanticaFormula(string name)
            : base(name, GPConsts.GPNODE_TYPE.NODE_FORMULA)
        {
        }




        internal override nodes.GPAbstractNode InstantiateEmpty()
        {
            return new GPNode(this);
        }

    }
}
