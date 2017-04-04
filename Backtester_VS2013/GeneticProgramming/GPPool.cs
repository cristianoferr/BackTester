using GeneticProgramming.nodes;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GeneticProgramming
{
    /*
     Função do Pool: manter todos os nodes e fazendo operações em cima destes.
     */
    [DataContract]
    public class GPPool
    {

        [DataMember]
        public IList<GPAbstractNode> nodes { get; private set; }

        public void InitPool(GPConfig config, GPHolder holder)
        {
            nodes = new List<GPAbstractNode>();
        }
    }
}
