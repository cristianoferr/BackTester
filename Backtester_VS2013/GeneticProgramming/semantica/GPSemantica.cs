
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace GeneticProgramming.semantica
{
    [DataContract]
    public class GPSemantica
    {
        public GPSemantica(string name)
        {
            this.name = name;
            propriedades = new List<GPConsts.GPNODE_TYPE>();
        }

        [DataMember]
        public IList<GPConsts.GPNODE_TYPE> propriedades { get; private set; }

        [DataMember]
        public string name { get; set; }

        public virtual bool IsTerminal
        {
            get
            {
                return propriedades.Count == 0;
            }
        }

        public virtual void AddPropriedade(GPConsts.GPNODE_TYPE nodeType)
        {
            propriedades.Add(nodeType);
        }

        internal virtual bool CanAddNode(int index, nodes.GPAbstractNode nodeFilho)
        {
            if (index >= propriedades.Count)
            {
                return false;
            }
            return propriedades[index] == nodeFilho.nodeType;
        }
    }
}
