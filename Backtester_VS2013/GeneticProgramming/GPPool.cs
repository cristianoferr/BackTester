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
        public const float PENALTY = 100000;

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
            //penalizando solucoes com mesmo fitness (parece que a mesma solução se alastra, ficando todos iguais)

            for (int i = 0; i < solutions.Count - 1; i++)
            {
                GPSolution solI=solutions[i];
                for (int j = i + 1; j < solutions.Count; j++)
                {
                    GPSolution solJ=solutions[j];
                    if (solI.fitnessResult == solJ.fitnessResult)
                    {
                        solJ.fitnessResult -= Utils.Random(PENALTY,PENALTY*2);
                    }
                }
            }
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
            IList<GPSolution> nextSolutions = new List<GPSolution>();

            for (int i = 0; i < solutions.Count; i++)
            {
                if (i<=config.elitismRange * solutions.Count/100){
                    nextSolutions.Add(solutions[i]);
                }
                else if (i <= config.mutationRange * solutions.Count / 100)
                {
                    GPSolution solution = solutions[i];
                    solution.Mutate();
                    nextSolutions.Add(solutions[i]);
                }
                else if (i <= config.generateChildRange * solutions.Count / 100)
                {
                    GPSolution solution1 = solutions[Utils.Random(0, config.generateChildRange/ 100f * solutions.Count) ];
                    GPSolution solution2 = solutions[Utils.Random(0, config.generateChildRange/ 100f * solutions.Count)];
                    GPSolution child2 = null;
                    GPSolution child = solution1.CreateChildWith(solution2, out child2);
                    nextSolutions.Add(child);
                    nextSolutions.Add(child2);
                    i += 1;
                }
                else
                {
                    nextSolutions.Add(template.CreateRandomSolution());
                }

            }
            solutions.Clear();
            solutions = nextSolutions;

            /*

            for (int i = config.elitismRange * solutions.Count/100; i < solutions.Count; i++)
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
            }*/
        }

        public GPTemplate template { get; set; }

        
    }
}
