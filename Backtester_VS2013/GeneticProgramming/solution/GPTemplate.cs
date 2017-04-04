using System.Collections.Generic;

namespace GeneticProgramming.semantica
{
    /*
     Template que serve de base para formar os programas (representam os campos evolutivos que o programa final terá)
     */
    public class GPTemplate
    {
        public Dictionary<string, SemanticaList> properties { get; private set; }
        public GPTemplate()
        {
            properties = new Dictionary<string, SemanticaList>();
        }

        public void AddProperty(string name, SemanticaList lista)
        {
            properties.Add(name, lista);
        }

        public solution.GPSolution CreateRandomSolution()
        {
            throw new System.NotImplementedException();
        }
    }
}
