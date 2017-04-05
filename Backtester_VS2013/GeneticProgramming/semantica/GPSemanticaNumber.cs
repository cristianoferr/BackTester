
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
            : base(name,GPConsts.GPNODE_TYPE.NUMBER)
        {
            this.name = name;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        internal override nodes.GPAbstractNode InstantiateEmpty()
        {
            return new GPNumber(this,randomizer.Next(minValue,maxValue));
        }

        public override bool IsTerminal
        {
            get
            {
                return true;
            }
        }

        public override void AddPropriedade(GPConsts.GPNODE_TYPE nodeType)
        {

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
