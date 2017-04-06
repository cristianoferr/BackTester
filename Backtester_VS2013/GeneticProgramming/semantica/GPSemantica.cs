
using System.Runtime.Serialization;
namespace GeneticProgramming.semantica
{
    [DataContract]
    public abstract class GPSemantica
    {
        public GPSemantica(string name, GPConsts.GPNODE_TYPE nodeType, int minParams, int maxParams)
        {
            this.name = name;
            this.nodeType = nodeType;
            this.minParams = minParams;
            this.maxParams = maxParams;
        }

        public override string ToString()
        {
            string ret = name;

            if (minParams > 0)
            {
                ret += "(" + minParams + "..." + maxParams + ")";
            }
            return ret;
        }



        [DataMember]
        public string name { get; set; }

        public virtual bool IsTerminal
        {
            get
            {
                return maxParams == 0;
            }
        }



        internal virtual bool CanAddNode(int index, nodes.GPAbstractNode nodeFilho)
        {
            return (maxParams > index);
        }

        internal abstract nodes.GPAbstractNode InstantiateEmpty();

        [DataMember]
        public GPConsts.GPNODE_TYPE nodeType { get; private set; }


        public int minParams { get; set; }

        public int maxParams { get; set; }
    }
}
