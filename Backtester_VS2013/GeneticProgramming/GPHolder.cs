
using GeneticProgramming.semantica;
using System.Collections.Generic;
namespace GeneticProgramming
{
    /*
     Função do holder: armazenar variaveis de uso comum (dados de semantica, etc)
     */
    public class GPHolder
    {
        public semantica.GPSolutionDefinition semanticsHolder { get; set; }

        private Dictionary<string, GPSemantica> allSemantics { get; set; }

        public GPHolder()
        {
            allSemantics = new Dictionary<string, GPSemantica>();

            InitDefaultSemantics();
        }

        public static string COMP_DIF = UsoComum.ConstsComuns.Operador.DIFFERENT.ToString();
        public static string COMP_EQUAL = UsoComum.ConstsComuns.Operador.EQUAL.ToString();
        public static string COMP_GREATER = UsoComum.ConstsComuns.Operador.GREATER.ToString();
        public static string COMP_GREATER_EQ = UsoComum.ConstsComuns.Operador.GREATER_EQ.ToString();
        public static string COMP_LOWER = UsoComum.ConstsComuns.Operador.LOWER.ToString();
        public static string COMP_LOWER_EQ = UsoComum.ConstsComuns.Operador.LOWER_EQ.ToString();

        public const string FORM_ADD = "ADD";
        public const string FORM_SUBTRACT = "SUBTRACT";
        public const string FORM_MULTIPLY = "MULTIPLY";
        public const string FORM_DIVIDE = "DIVIDE";

        public const string BOOL_AND = "AND";
        public const string BOOL_OR = "OR";
        public const string BOOL_NOT = "NOT";

        public virtual void InitDefaultSemantics()
        {

            InitBooleans();

            InitComparers();

            InitFormulas();


        }

        private void InitComparers()
        {
            AddSemanticaComparer(COMP_DIF, new GPSemanticaComparer(UsoComum.ConstsComuns.Operador.DIFFERENT));
            AddSemanticaComparer(COMP_EQUAL, new GPSemanticaComparer(UsoComum.ConstsComuns.Operador.EQUAL));
            AddSemanticaComparer(COMP_GREATER, new GPSemanticaComparer(UsoComum.ConstsComuns.Operador.GREATER));
            AddSemanticaComparer(COMP_GREATER_EQ, new GPSemanticaComparer(UsoComum.ConstsComuns.Operador.GREATER_EQ));
            AddSemanticaComparer(COMP_LOWER, new GPSemanticaComparer(UsoComum.ConstsComuns.Operador.LOWER));
            AddSemanticaComparer(COMP_LOWER_EQ, new GPSemanticaComparer(UsoComum.ConstsComuns.Operador.LOWER_EQ));
        }

        private void InitFormulas()
        {
            AddSemanticaFormula(FORM_ADD, new GPSemanticaFormula(FORM_ADD));
            AddSemanticaFormula(FORM_SUBTRACT, new GPSemanticaFormula(FORM_SUBTRACT));
            AddSemanticaFormula(FORM_MULTIPLY, new GPSemanticaFormula(FORM_MULTIPLY));
            AddSemanticaFormula(FORM_DIVIDE, new GPSemanticaFormula(FORM_DIVIDE));
        }

        private void InitBooleans()
        {
            AddSemanticaBoolean(BOOL_AND, new GPSemanticaBoolean(BOOL_AND));
            AddSemanticaBoolean(BOOL_OR, new GPSemanticaBoolean(BOOL_OR));
            AddSemanticaBoolean(BOOL_NOT, new GPSemanticaBoolean(BOOL_NOT));
        }

        private void AddSemanticaBoolean(string name, GPSemanticaBoolean semantica)
        {
            semantica.AddPropriedade(GPConsts.GPNODE_TYPE.NODE_COMPARER);
            semantica.AddPropriedade(GPConsts.GPNODE_TYPE.NODE_COMPARER);
            AddSemantica(name, semantica);
        }

        private void AddSemanticaFormula(string name, GPSemantica semantica)
        {
            semantica.AddPropriedade(GPConsts.GPNODE_TYPE.NODE_FORMULA);
            semantica.AddPropriedade(GPConsts.GPNODE_TYPE.NODE_FORMULA);
            AddSemantica(name, semantica);
        }



        // Para comparers eu permito formulas, exemplo F1 > F2
        //TODO: como fazer para permitir formula e numero?
        public void AddSemanticaComparer(string name, GPSemantica semantica)
        {
            semantica.AddPropriedade(GPConsts.GPNODE_TYPE.NODE_FORMULA);
            semantica.AddPropriedade(GPConsts.GPNODE_TYPE.NODE_FORMULA);
            AddSemantica(name, semantica);
        }

        public void AddSemantica(string name, GPSemantica semantica)
        {
            allSemantics.Add(name, semantica);
        }


        public GPSemantica GetSemantica(string name)
        {
            return allSemantics[name];
        }
    }
}
