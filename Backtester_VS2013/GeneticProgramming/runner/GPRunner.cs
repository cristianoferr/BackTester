
using GeneticProgramming.semantica;
using GeneticProgramming.solution;
namespace GeneticProgramming
{
    public abstract class GPRunner
    {
        private string periodoAcao;

        public GPRunner(GPConfig config,string periodoAcao)
        {
            this.gpConfig = config;

            this.definitions = CreateSolutionDefinition();
            this.periodoAcao = periodoAcao;
            this.pool = GPPool.LoadSaved(config, definitions, periodoAcao);

        }
        public void InitPool()
        {

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
                EndSolution(solution);
            }

            pool.EndTurn();
            pool.Mutate();
            pool.SaveState(periodoAcao);
        }

        public abstract void RunSolution(GPSolution solution);
        public abstract void EndSolution(GPSolution solution);

        public abstract void PrepareSolution(GPSolution solution);


        public GPSolutionDefinition definitions { get; set; }
        public GPTemplate template { get; set; }

        public GPConfig gpConfig { get; set; }


        public GPPool pool { get; set; }
    }
}
