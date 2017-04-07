using Backtester.backend.DataManager;
using GeneticProgramming;
using GeneticProgramming.semantica;

namespace Backtester.GeneticProgramming
{

    public class BackTesterSemantica : GPSolutionDefinition
    {


        public BackTesterSemantica(GPConfig config)
            : base(config)
        {

            InicializaFormulasFinanceiras();

        }

        private void InicializaFormulasFinanceiras()
        {
            allSemantics.Remove(GPConsts.OPER_ADD);
            allSemantics.Remove(GPConsts.OPER_DIVIDE);
            allSemantics.Remove(GPConsts.OPER_MULTIPLY);
            allSemantics.Remove(GPConsts.OPER_SUBTRACT);


            AddSemanticaFormula(FormulaManager.CLOSE, 0, 0);
            AddSemanticaFormula(FormulaManager.OPEN, 0, 0);
            AddSemanticaFormula(FormulaManager.HIGH, 0, 0);
            AddSemanticaFormula(FormulaManager.LOW, 0, 0);
            AddSemanticaFormula(FormulaManager.VOL, 0, 0);
            // AddSemanticaFormula(FormulaManager.DIFOC, 0, 0);
            //AddSemanticaFormula(FormulaManager.DIFHL, 0, 0);
            AddSemanticaFormula(FormulaManager.MMS, 2, 2);
            AddSemanticaFormula(FormulaManager.MME, 2, 2);
            AddSemanticaFormula(FormulaManager.IFR, 2, 2);
            AddSemanticaFormula(FormulaManager.HV, 1, 1);
            AddSemanticaFormula(FormulaManager.LV, 1, 1);
            AddSemanticaFormula(FormulaManager.AVGGAIN, 1, 1);
            AddSemanticaFormula(FormulaManager.AVGLOSS, 1, 1);
            AddSemanticaFormula(FormulaManager.STDDEV, 1, 1);
            AddSemanticaFormula(FormulaManager.UPPERBB, 2, 2);
            AddSemanticaFormula(FormulaManager.MIDDLEBB, 2, 2);
            AddSemanticaFormula(FormulaManager.LOWERBB, 2, 2);
            AddSemanticaFormula(FormulaManager.BB, 3, 3);
            AddSemanticaFormula(FormulaManager.SUBTRACT, 2, 2);
            AddSemanticaFormula(FormulaManager.MULTIPLY, 2, 2);
            AddSemanticaFormula(FormulaManager.SUM, 2, 2);
            AddSemanticaFormula(FormulaManager.DIVIDE, 2, 2);
            AddSemanticaFormula(FormulaManager.STOCH, 1, 1);
            AddSemanticaFormula(FormulaManager.PERCENTIL, 0, 0);
        }

        private void AddSemanticaFormula(string name, int min, int max)
        {
            GPSemantica semantica = new GPSemanticaFormula(name, min, max);
            base.AddSemanticaFormula(name, semantica);
            AddSemanticaToList(GPConsts.LISTA_FORMULA, semantica);

        }
    }
}
