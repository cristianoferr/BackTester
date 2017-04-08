using GeneticProgramming.semantica;
using GeneticProgramming.solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UsoComum;

namespace GeneticProgramming
{
    /*
     Função do Pool: manter todos os nodes e fazendo operações em cima destes.
     */
    [DataContract]
    public class GPPool
    {

        public GPPool(GPConfig config)
        {
            this.config = config;


        }
        [DataMember]
        public IList<GPSolution> solutions { get; private set; }

        public void InitPool(GPTemplate template)
        {
            this.template = template;
            solutions = new List<GPSolution>();
            for (int i = 0; i < config.poolSize; i++)
            {
                solutions.Add(template.CreateRandomSolution());
            }
        }

        public GPConfig config { get; set; }


        public void SortFitness()
        {
            //for (int i=0;i)
            solutions = solutions.OrderByDescending(x => x.fitnessResult).ToList();
        }
        public void EndTurn()
        {
            SortFitness();

        }

        internal void InitTurn()
        {
            foreach (GPSolution solution in solutions)
            {
                solution.fitnessResult = 0;
            }
        }

        public void Mutate()
        {
            for (int i = config.elitismPercent * solutions.Count/100; i < solutions.Count; i++)
            {
                GPSolution solution = solutions[i];

                if (Utils.Random(0, 100) < config.spliceChancePerc)
                {
                    //Pego um dos top 85%
                    GPSolution mateWith = solutions[Utils.Random(0, 85) / 100 * solutions.Count];
                    solution.SpliceWith(mateWith);
                }
                if (Utils.Random(0, 100) <= config.mutationRatePerc)
                {
                    solution.Mutate();
                }
                //TODO: mutate, splice, etc
            }

            for (int i = solutions.Count-1;i>=config.unfitRemovalPercent * config.poolSize / 100; i--)
            {
                solutions.RemoveAt(i);
            }
            for (int i = config.unfitRemovalPercent * config.poolSize / 100; i < config.poolSize; i++)
            {
                solutions.Add(template.CreateRandomSolution());
            }
        }

        public GPTemplate template { get; set; }
    }
}
