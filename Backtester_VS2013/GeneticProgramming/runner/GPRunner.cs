
using GeneticProgramming.semantica;
using GeneticProgramming.solution;
namespace GeneticProgramming
{
    public abstract class GPRunner
    {

        public GPRunner(GPConfig config)
        {
            this.gpConfig = config;

            this.pool = new GPPool(config);
            this.definitions = CreateSolutionDefinition();

            pool.InitPool(CreateTemplate());
        }

        public abstract GPSolutionDefinition CreateSolutionDefinition();

        public abstract GPTemplate CreateTemplate();


        public virtual void SingleRun()
        {
            pool.InitTurn();
            foreach (GPSolution solution in pool.solutions)
            {
                PrepareSolution(solution);
                RunSolution(solution);
            }

            pool.EndTurn();
            pool.Mutate();
        }

        public abstract void RunSolution(GPSolution solution);

        public abstract void PrepareSolution(GPSolution solution);


        public GPSolutionDefinition definitions { get; set; }
        public GPTemplate template { get; set; }

        public GPConfig gpConfig { get; set; }


        public GPPool pool { get; set; }
    }
}
