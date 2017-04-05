using GeneticProgramming.solution;
using System.Collections.Generic;

namespace GeneticProgramming.semantica
{
    /*
     Template que serve de base para formar os programas (representam os campos evolutivos que o programa final terá)
     */
    public class GPTemplate
    {
        public Dictionary<string, SemanticaList> properties { get; private set; }
        public GPTemplate(GPConfig config)
        {
            properties = new Dictionary<string, SemanticaList>();
            this.config = config;
        }

        public void AddProperty(string name, SemanticaList lista)
        {
            properties.Add(name, lista);
        }

        public solution.GPSolution CreateRandomSolution()
        {
            GPSolution solution = new GPSolution();
            foreach (string key in properties.Keys)
            {
                SemanticaList lista=properties[key];
                solution.SetValue(key,lista.CreateRandomNode(config,GPConsts.GPNODE_TYPE.NODE,false));
            }
            return solution;
        }

        public GPConfig config { get; set; }
    }
}
