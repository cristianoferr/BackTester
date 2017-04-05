
using System;
using System.Runtime.Serialization;
namespace GeneticProgramming.nodes
{
    [DataContract]
    public class GPNodeComparer : GPAbstractNode
    {

        public GPNodeComparer(semantica.GPSemantica semantica)
            : base(semantica, GPConsts.GPNODE_TYPE.NODE_COMPARER)
        {
            
        }
    }
}
