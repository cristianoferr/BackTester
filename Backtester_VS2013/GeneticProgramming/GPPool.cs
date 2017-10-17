using Backtester.backend.model.system;
using GeneticProgramming.semantica;
using GeneticProgramming.solution;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using UsoComum;

namespace GeneticProgramming
{
    /*
     Função do Pool: manter todos os nodes e fazendo operações em cima destes.
     */
    [DataContract]
    public class GPPool
    {
        public const float PENALTY = 1000;

        #region IO
        internal static GPPool LoadSaved(GPConfig config, GPSolutionDefinition definition, string periodoAcao)
        {
            try
            {
                var lista = new List<Type>();
                lista.Add(typeof(TradeSystem));
                var fileStream = File.Open("saved-pool-"+ periodoAcao + ".js", FileMode.Open);
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(GPPool), lista);
                fileStream.Flush();
                fileStream.Position = 0;
                GPPool pool = (GPPool)ser.ReadObject(fileStream);
                pool.config = config;
                pool.FinishLoading(definition);
                fileStream.Close();
                return pool;
            }
            catch (System.Exception e)
            {

            }
            return new GPPool(config);
        }

        //Necessário para religar o nodePai (não está sendo serializado para evitar loops)
        private void FinishLoading(GPSolutionDefinition definition)
        {
            foreach (GPSolution solution in solutions)
            {

                solution.FinishLoading(definition);
            }
        }
        internal void SaveState(string periodoAcao)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(GPPool));
            var fileStream = File.Create("saved-pool-" + periodoAcao + ".js");
            ser.WriteObject(fileStream, this);
            fileStream.Close();
        }


        #endregion


        public GPPool(GPConfig config)
        {
            this.config = config;
            iterationNumber = 0;
            solutions = new List<GPSolution>();
        }

        #region properties
        [DataMember]
        public int iterationNumber { get; set; }
        [DataMember]
        public IList<GPSolution> solutions { get; private set; }
        public GPConfig config { get; set; }
        public GPTemplate template { get; set; }
        #endregion

        public void InitPool(GPTemplate template)
        {
            this.template = template;

            while (solutions.Count < config.poolSize)
            {
                solutions.Add(template.CreateRandomSolution());
            }

            for (int i = 0; i < solutions.Count; i++)
            {
                solutions[i].template = template;
            }
        }

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
                GPSolution solI = solutions[i];
                
                for (int j = i + 1; j < solutions.Count; j++)
                {
                    GPSolution solJ = solutions[j];
                    if (solI.fitnessResult == solJ.fitnessResult)
                    {
                        solJ.fitnessResult -= Utils.Random(PENALTY, PENALTY * 2);
                    }
                }
            }
            SortFitness();

        }

        internal void InitTurn()
        {
            iterationNumber++;
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
                if (i <= config.elitismRange * solutions.Count / 100)
                {
                    nextSolutions.Add(solutions[i]);
                }
                else if (i <= config.mutationRange * solutions.Count / 100)
                {
                    GPSolution solution = solutions[i];
                    solution.Mutate(20);
                    nextSolutions.Add(solutions[i]);
                }
                else if (i <= config.generateChildRange * solutions.Count / 100)
                {
                    GPSolution solution1 = solutions[Utils.RandomInt(0, config.generateChildRange / 100f * solutions.Count)];
                    GPSolution solution2 = solutions[Utils.RandomInt(0, config.generateChildRange / 100f * solutions.Count)];
                    GPSolution child2 = null;
                    GPSolution child = solution1.CreateChildWith(solution2, out child2);
                    nextSolutions.Add(child);
                    child.Mutate(20);
                    nextSolutions.Add(child2);
                    child2.Mutate(20);
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








    }
}
