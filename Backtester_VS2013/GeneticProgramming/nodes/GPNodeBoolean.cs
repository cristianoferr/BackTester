
using System.Runtime.Serialization;
namespace GeneticProgramming.nodes
{
    [DataContract]
    public class GPNodeBoolean : GPAbstractNode
    {

        public GPNodeBoolean(semantica.GPSemantica semantica)
            : base(semantica, GPConsts.GPNODE_TYPE.NODE_BOOLEAN)
        {
        }


    


    }
}
