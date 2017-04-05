
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace GeneticProgramming.semantica
{
    [DataContract]
    public abstract class GPSemantica
    {
        public GPSemantica(string name, GPConsts.GPNODE_TYPE nodeType)
        {
            this.name = name;
            this.nodeType = nodeType;
            propriedades = new List<int>();
        }

        [DataMember]
        public IList<int> propriedades { get; private set; }

        [DataMember]
        public string name { get; set; }

        public virtual bool IsTerminal
        {
            get
            {
                if (propriedades.Count == 0) return true;
                return false;
            }
        }


        public virtual void AddPropriedade(int nodesAllowed)
        {
            propriedades.Add(nodesAllowed);
        }

        internal virtual bool CanAddNode(int index, nodes.GPAbstractNode nodeFilho)
        {
            if (index >= propriedades.Count)
            {
                return false;
            }
            return (propriedades[index] & (int)nodeFilho.nodeType) > 0;
        }

        internal abstract nodes.GPAbstractNode InstantiateEmpty();

        [DataMember]
        public GPConsts.GPNODE_TYPE nodeType { get; private set; }


        internal int NextNodeType(int i)
        {
            return propriedades[i];
        }

        public void AddPropriedade(GPConsts.GPNODE_TYPE nodeType)
        {
            propriedades.Add((int)nodeType);
        }
    }
}
