
using System.Runtime.Serialization;
namespace GeneticProgramming.semantica
{
    [DataContract]
    public class GPSemanticaNumber : GPSemantica
    {
        public GPSemanticaNumber(string name, float minValue, float maxValue)
            : base(name)
        {
            this.name = name;
            this.minValue = minValue;
            this.maxValue = maxValue;
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
        public float minValue { get; set; }

        [DataMember]
        public float maxValue { get; set; }
    }
}
