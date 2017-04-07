using GeneticProgramming.nodes;
using GeneticProgramming.semantica;
using GeneticProgramming.solution;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GeneticProgramming
{
    /*
     Função do Pool: manter todos os nodes e fazendo operações em cima destes.
     */
    [DataContract]
    public class GPPool
    {

        public GPPool(GPConfig config, GPHolder holder)
        {
            this.config = config;
            this.holder = holder;
            

        }
        [DataMember]
        public IList<GPSolution> solutions { get; private set; }

        public void InitPool(GPTemplate template)
        {
            solutions = new List<GPSolution>();
            for (int i = 0; i < config.poolQtd; i++)
            {
                solutions.Add(template.CreateRandomSolution());
            }
        }

        public GPConfig config { get; set; }
        public GPHolder holder { get; set; }

    }
}
