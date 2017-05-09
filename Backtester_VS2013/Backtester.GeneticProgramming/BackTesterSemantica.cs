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

            AddSemanticaToList(GPConsts.LISTA_FORMULA, GetSemantica(GPConsts.BOOL_AND));
            AddSemanticaToList(GPConsts.LISTA_FORMULA, GetSemantica(GPConsts.BOOL_OR));
            AddSemanticaToList(GPConsts.LISTA_FORMULA, GetSemantica(GPConsts.BOOL_XOR));
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
            AddSemanticaFormula(FormulaManager.HV, 2, 2);
            AddSemanticaFormula(FormulaManager.LV, 2, 2);
            AddSemanticaFormula(FormulaManager.TRIX, 2, 2);
            AddSemanticaFormula(FormulaManager.AVGGAIN, 2, 2);
            AddSemanticaFormula(FormulaManager.AVGLOSS, 2, 2);
            AddSemanticaFormula(FormulaManager.STDDEV, 2, 2);
            AddSemanticaFormula(FormulaManager.UPPERBB, 3, 3);
            AddSemanticaFormula(FormulaManager.MIDDLEBB, 3, 3);
            AddSemanticaFormula(FormulaManager.LOWERBB, 3, 3);
            //AddSemanticaFormula(FormulaManager.BB, 3, 3);
            AddSemanticaFormula(FormulaManager.SUBTRACT, 2, 2);
            AddSemanticaFormula(FormulaManager.MULTIPLY, 2, 2);
            AddSemanticaFormula(FormulaManager.SUM, 2, 2);
            AddSemanticaFormula(FormulaManager.DIVIDE, 2, 2);
            AddSemanticaFormula(FormulaManager.STOCH, 2, 2);
            AddSemanticaFormula(FormulaManager.PERCENTIL, 1, 1);


        }

        private void AddSemanticaFormula(string name, int min, int max)
        {
            GPSemantica semantica = new GPSemanticaFormula(name, min, max);
            base.AddSemanticaFormula(name, semantica);
            AddSemanticaToList(GPConsts.LISTA_FORMULA, semantica);

        }
    }
}
