
using GeneticProgramming.semantica;
using System.Runtime.Serialization;
using UsoComum;

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
            if (children.Count == 0 || Utils.RandomInt(0, 100) < 50)
            {
                semantica = semanticaList.GetEquivalent(semantica, children.Count);
            }
            else
            {
                int rnd = Utils.RandomInt(0, children.Count);
                children[rnd].Mutate(semanticaList);
            }
            
        }

    }
}
