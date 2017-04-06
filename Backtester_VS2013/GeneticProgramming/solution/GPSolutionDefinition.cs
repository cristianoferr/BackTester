
using System.Collections.Generic;
namespace GeneticProgramming.semantica
{
    public class GPSolutionDefinition
    {
        private GPHolder holder;

        public Dictionary<string, SemanticaList> semantics { get; set; }


        public GPSolutionDefinition(GPHolder holder)
        {
            semantics = new Dictionary<string, SemanticaList>();
            this.holder = holder;
            holder.semanticsHolder = this;
        }


        public void AddSemantica(string listName, string holderName)
        {
            GPSemantica semantica = holder.GetSemantica(holderName);
            AddSemantica(listName, semantica);
        }

        public void AddSemantica(string listName, GPSemantica semantica)
        {
            SemanticaList list = GetListByName(listName);
            list.Add(semantica);
        }


        public SemanticaList GetListByName(string listName)
        {
            if (!semantics.ContainsKey(listName))
            {
                SemanticaList list = new SemanticaList(listName);
                semantics.Add(listName, list);
            }
            return semantics[listName];
        }
    }
}
