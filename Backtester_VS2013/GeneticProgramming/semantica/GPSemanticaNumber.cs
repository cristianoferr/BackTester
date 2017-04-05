
using GeneticProgramming.nodes;
using System;
using System.Runtime.Serialization;
namespace GeneticProgramming.semantica
{
    [DataContract]
    public class GPSemanticaNumber : GPSemantica
    {
        static Random randomizer = new Random();
        public GPSemanticaNumber(string name, int minValue, int maxValue)
            : base(name, GPConsts.GPNODE_TYPE.NODE_NUMBER)
        {
            this.name = name;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        internal override nodes.GPAbstractNode InstantiateEmpty()
        {
            return new GPNodeNumber(this, randomizer.Next(minValue, maxValue));
        }

        public override bool IsTerminal
        {
            get
            {
                return true;
            }
        }

        internal override bool CanAddNode(int index, nodes.GPAbstractNode nodeFilho)
        {
            return false;
        }

        [DataMember]
        public int minValue { get; set; }

        [DataMember]
        public int maxValue { get; set; }
    }
}
