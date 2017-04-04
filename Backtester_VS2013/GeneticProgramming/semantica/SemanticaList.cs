
using System.Collections.Generic;
namespace GeneticProgramming.semantica
{
    public class SemanticaList
    {
        public SemanticaList(string listName)
        {
            this.name = listName;
            lista = new List<GPSemantica>();
        }
        string name { get; set; }
        IList<GPSemantica> lista { get; set; }

        public bool Contains(GPSemantica semantica)
        {
            return lista.Contains(semantica);
        }

        internal void Add(GPSemantica semantica)
        {
            lista.Add(semantica);
        }
    }
}
