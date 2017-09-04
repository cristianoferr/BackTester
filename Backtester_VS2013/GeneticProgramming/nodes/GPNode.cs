
using GeneticProgramming.semantica;
using System.Runtime.Serialization;
namespace GeneticProgramming.nodes
{
    [DataContract]
    public class GPNode : GPAbstractNode
    {

        public GPNode(semantica.GPSemantica semantica=null)
            : base(semantica)
        {
        }

        public override void Mutate(SemanticaList semanticaList)
        {
            semantica = semanticaList.GetEquivalent(semantica, children.Count);
        }

    }
}
