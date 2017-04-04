
using System.Collections.Generic;
namespace GeneticProgramming.semantica
{
    public class GPSemanticaHolder
    {
        private GPHolder holder;

        public Dictionary<string, SemanticaList> semantics { get; set; }


        public GPSemanticaHolder(GPHolder holder)
        {
            semantics = new Dictionary<string, SemanticaList>();
            this.holder = holder;
            holder.semanticsHolder = this;
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
                return list;
            }
            return semantics[listName];
        }
    }
}
