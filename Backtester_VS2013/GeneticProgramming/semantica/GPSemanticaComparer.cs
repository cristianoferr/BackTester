
using GeneticProgramming.nodes;
using System;
using System.Runtime.Serialization;
namespace GeneticProgramming.semantica
{
    [DataContract]
    public class GPSemanticaComparer : GPSemantica
    {
        static Random randomizer = new Random();
        public GPSemanticaComparer(UsoComum.ConstsComuns.Operador operador)
            : base(operador.ToString(), GPConsts.GPNODE_TYPE.NODE_COMPARER, 2, 2)
        {
            this.operador = operador;
        }

        internal override nodes.GPAbstractNode InstantiateEmpty()
        {
            return new GPNode(this);
        }

        public UsoComum.ConstsComuns.Operador operador { get; set; }
    }
}
