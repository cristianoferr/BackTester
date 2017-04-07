﻿
using System.Collections.Generic;
namespace GeneticProgramming.semantica
{
    public class GPSolutionDefinition
    {


        //lista categorizada de semantica
        public Dictionary<string, SemanticaList> listOfSemantics { get; set; }

        public Dictionary<string, GPSemantica> allSemantics { get; private set; }


        public GPSolutionDefinition(GPConfig config)
        {
            listOfSemantics = new Dictionary<string, SemanticaList>();
            allSemantics = new Dictionary<string, GPSemantica>();
            this.config = config;

            InitDefaultSemantics();

            CreateListByName(GPConsts.LISTA_FORMULA, config.minLevels, config.maxLevels);
            CreateListByName(GPConsts.LISTA_NUMEROS, 0, 0);
            AddSemanticaToList(GPConsts.LISTA_FORMULA, GetSemantica(GPConsts.NUMBER_DEFAULT));
            AddSemanticaToList(GPConsts.LISTA_NUMEROS, GetSemantica(GPConsts.NUMBER_DEFAULT));
        }
        #region AllSemantics

        public virtual void InitDefaultSemantics()
        {

            InitBooleans();

            InitComparers();

            InitFormulas();


            GPSemantica semantica = new GPSemanticaNumber(GPConsts.NUMBER_DEFAULT, -10, 10);
            AddSemanticaToDictionary(GPConsts.NUMBER_DEFAULT, semantica);

        }



        private void InitComparers()
        {
            AddSemanticaComparer(GPConsts.COMP_DIF, new GPSemanticaComparer(UsoComum.ConstsComuns.Operador.DIFFERENT));
            AddSemanticaComparer(GPConsts.COMP_EQUAL, new GPSemanticaComparer(UsoComum.ConstsComuns.Operador.EQUAL));
            AddSemanticaComparer(GPConsts.COMP_GREATER, new GPSemanticaComparer(UsoComum.ConstsComuns.Operador.GREATER));
            AddSemanticaComparer(GPConsts.COMP_GREATER_EQ, new GPSemanticaComparer(UsoComum.ConstsComuns.Operador.GREATER_EQ));
            AddSemanticaComparer(GPConsts.COMP_LOWER, new GPSemanticaComparer(UsoComum.ConstsComuns.Operador.LOWER));
            AddSemanticaComparer(GPConsts.COMP_LOWER_EQ, new GPSemanticaComparer(UsoComum.ConstsComuns.Operador.LOWER_EQ));
        }

        private void InitFormulas()
        {
            AddSemanticaFormula(GPConsts.OPER_ADD, new GPSemanticaFormula(GPConsts.OPER_ADD, 2, 2, GPConsts.GPNODE_TYPE.NODE_OPERATOR));
            AddSemanticaFormula(GPConsts.OPER_SUBTRACT, new GPSemanticaFormula(GPConsts.OPER_SUBTRACT, 2, 2, GPConsts.GPNODE_TYPE.NODE_OPERATOR));
            AddSemanticaFormula(GPConsts.OPER_MULTIPLY, new GPSemanticaFormula(GPConsts.OPER_MULTIPLY, 2, 2, GPConsts.GPNODE_TYPE.NODE_OPERATOR));
            AddSemanticaFormula(GPConsts.OPER_DIVIDE, new GPSemanticaFormula(GPConsts.OPER_DIVIDE, 2, 2, GPConsts.GPNODE_TYPE.NODE_OPERATOR));
        }

        private void InitBooleans()
        {
            AddSemanticaBoolean(GPConsts.BOOL_AND, new GPSemanticaBoolean(GPConsts.BOOL_AND));
            AddSemanticaBoolean(GPConsts.BOOL_OR, new GPSemanticaBoolean(GPConsts.BOOL_OR));
            AddSemanticaBoolean(GPConsts.BOOL_NOT, new GPSemanticaBoolean(GPConsts.BOOL_NOT, 1, 1));
        }

        internal void AddSemanticaBoolean(string name, GPSemanticaBoolean semantica)
        {
            // semantica.AddPropriedade(GPConsts.GPNODE_TYPE.NODE_COMPARER, GPConsts.GPNODE_TYPE.NODE_BOOLEAN);
            //semantica.AddPropriedade(GPConsts.GPNODE_TYPE.NODE_COMPARER, GPConsts.GPNODE_TYPE.NODE_BOOLEAN);
            AddSemanticaToDictionary(name, semantica);
        }

        public void AddSemanticaFormula(string name, GPSemantica semantica)
        {
            //semantica.AddPropriedade(GPConsts.GPNODE_TYPE.NODE_FORMULA, GPConsts.GPNODE_TYPE.NODE_NUMBER);
            //semantica.AddPropriedade(GPConsts.GPNODE_TYPE.NODE_FORMULA, GPConsts.GPNODE_TYPE.NODE_NUMBER);
            AddSemanticaToDictionary(name, semantica);
        }



        // Para comparers eu permito formulas, exemplo F1 > F2
        //TODO: como fazer para permitir formula e numero?
        public void AddSemanticaComparer(string name, GPSemantica semantica)
        {
            //semantica.AddPropriedade(GPConsts.GPNODE_TYPE.NODE_FORMULA, GPConsts.GPNODE_TYPE.NODE_NUMBER, GPConsts.GPNODE_TYPE.NODE_BOOLEAN, GPConsts.GPNODE_TYPE.NODE_COMPARER);
            // semantica.AddPropriedade(GPConsts.GPNODE_TYPE.NODE_FORMULA, GPConsts.GPNODE_TYPE.NODE_NUMBER, GPConsts.GPNODE_TYPE.NODE_BOOLEAN, GPConsts.GPNODE_TYPE.NODE_COMPARER);
            AddSemanticaToDictionary(name, semantica);
        }

        public void AddSemanticaToDictionary(string name, GPSemantica semantica)
        {
            allSemantics.Add(name, semantica);
        }


        public GPSemantica GetSemantica(string name)
        {
            return allSemantics[name];
        }
        #endregion

        #region listOfSemantics

        /*public void AddSemantica(string listName, string holderName)
        {
            GPSemantica semantica = holder.GetSemantica(holderName);
            AddSemantica(listName, semantica);
        }*/

        public void AddSemanticaToList(string listName, GPSemantica semantica)
        {
            SemanticaList list = GetListByName(listName);
            list.Add(semantica);
        }

        public void CreateListByName(string listName, int minLevels, int maxLevels)
        {
            if (!listOfSemantics.ContainsKey(listName))
            {
                SemanticaList list = new SemanticaList(listName, minLevels, maxLevels);
                listOfSemantics.Add(listName, list);
            }
        }

        public SemanticaList GetListByName(string listName)
        {
            return listOfSemantics[listName];
        }

        #endregion

        public GPConfig config { get; set; }
    }
}
