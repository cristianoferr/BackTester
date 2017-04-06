
using System.Runtime.Serialization;
namespace GeneticProgramming.nodes
{
    [DataContract]
    public class GPNodeFormula : GPAbstractNode
    {

        public GPNodeFormula(semantica.GPSemantica semantica)
            : base(semantica, GPConsts.GPNODE_TYPE.NODE_FORMULA)
        {
        }


      
    }
}
