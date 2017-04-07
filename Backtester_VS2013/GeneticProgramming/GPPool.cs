using GeneticProgramming.semantica;
using GeneticProgramming.solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

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
            solutions = new List<GPSolution>();
            for (int i = 0; i < config.poolQtd; i++)
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

        internal void Mutate()
        {
            Random rnd = new Random();
            for (int i = config.elitismPercent * solutions.Count; i < solutions.Count; i++)
            {
                GPSolution solution = solutions[i];

                if (rnd.Next(0, 100) < config.spliceChancePerc)
                {
                    //Pego um dos top 85%
                    GPSolution mateWith = solutions[rnd.Next(0, 85) / 100 * solutions.Count];
                    solution.SpliceWith(mateWith);
                }
                if (rnd.Next(0, 100) <= config.mutationRatePerc)
                {
                    solution.Mutate();
                }
                //TODO: mutate, splice, etc
            }
        }
    }
}
