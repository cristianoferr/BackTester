using GeneticProgramming.nodes;
using GeneticProgramming.semantica;
using GeneticProgramming.solution;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GeneticProgramming.tests
{
    [TestClass]
    public class GeneticProgrammingTest
    {

        static GPConfig config = new GPConfig();

        

        [TestMethod]
        public void TestGPTemplate()
        {
            GPSolutionDefinition holder = new GPSolutionDefinition(config);

            /*
             vou simular um template com 4 formulas(nodes) e 2 variaveis numéricas
             * f1(NODE,NUMBER)
             * F2(NODE,NODE)
             * F3
             * F4(NUMBER)
             */
            SemanticaList listaFormulas = CriaListaDefault(holder);

            //Lista Numerica
            SemanticaList listaNumerica = CriaListaNumerica(holder);

            //Templates: possuem propriedades evolutivas, cada uma apontando para uma lista de semantica
            GPTemplate template = CreateTestTemplate(config, listaFormulas, listaNumerica);

            GPSolution solution = template.CreateRandomSolution();
            Assert.IsNotNull(solution);
            GPAbstractNode p1 = solution.GetValue("prop1");
            Assert.IsNotNull(p1);
            GPAbstractNode p2 = solution.GetValue("prop2");
            Assert.IsNotNull(p2);
            GPAbstractNode p3 = solution.GetValue("prop3");
            Assert.IsNotNull(p3);
            GPAbstractNode p4 = solution.GetValue("prop4");
            Assert.IsNotNull(p4);

            GPAbstractNode n1 = solution.GetValue("number1");
            Assert.IsNotNull(n1);
            GPAbstractNode n2 = solution.GetValue("number2");
            Assert.IsNotNull(n2);


            GPAbstractNode clonep1 = p1.Clone();
            Assert.IsNotNull(clonep1);
            Assert.IsFalse(clonep1 == p1);
            Assert.IsTrue(clonep1.ToString() == p1.ToString());

            GPSolution cloneSolution = solution.Clone();
            GPSolution solution2 = template.CreateRandomSolution();

            GPSolution childSolution2=null;
            GPSolution childSolution = solution.CreateChildWith(solution2,out childSolution2);
            Assert.IsNotNull(childSolution);
            Assert.IsNotNull(childSolution2);

        }

        private static GPTemplate CreateTestTemplate(GPConfig config, SemanticaList listaFormulas, SemanticaList listaNumerica)
        {
            GPTemplate template = new GPTemplate(config);
            template.AddProperty("prop1", listaFormulas);
            Assert.IsTrue(template.GetProperty("prop1") == listaFormulas);
            template.AddProperty("prop2", listaFormulas);
            template.AddProperty("prop3", listaFormulas);
            template.AddProperty("prop4", listaFormulas);

            template.AddProperty("number1", listaNumerica);
            template.AddProperty("number2", listaNumerica);
            return template;
        }

        [TestMethod]
        public void TestaGPHolder()
        {
            GPSolutionDefinition holder = new GPSolutionDefinition(config);
            Assert.IsNotNull(holder.GetSemantica(GPConsts.BOOL_AND));
            Assert.IsTrue(holder.GetSemantica(GPConsts.BOOL_AND).nodeType == GPConsts.GPNODE_TYPE.NODE_BOOLEAN);
            Assert.IsNotNull(holder.GetSemantica(GPConsts.COMP_EQUAL));
            Assert.IsTrue(holder.GetSemantica(GPConsts.COMP_EQUAL).nodeType == GPConsts.GPNODE_TYPE.NODE_COMPARER);
            Assert.IsNotNull(holder.GetSemantica(GPConsts.OPER_MULTIPLY));
            Assert.IsTrue(holder.GetSemantica(GPConsts.OPER_MULTIPLY).nodeType == GPConsts.GPNODE_TYPE.NODE_OPERATOR);
        }

        [TestMethod]
        public void TestaGPSolutionDefinition()
        {



            string listDefault = "default";
            GPSolutionDefinition definition = CreateSampleDefinition(listDefault);
            SemanticaList listSemantic = definition.GetListByName(listDefault);
        }

        private GPSolutionDefinition CreateSampleDefinition(string listName)
        {

            GPSolutionDefinition definition = new GPSolutionDefinition(config);
            definition.CreateListByName(listName, config.minLevels, config.maxLevels);
            definition.AddSemanticaToList(listName, definition.GetSemantica(GPConsts.BOOL_AND));
            SemanticaList listSemantic = definition.GetListByName(listName);
            Assert.IsNotNull(listSemantic);
            Assert.IsTrue(listSemantic.Contains(definition.GetSemantica(GPConsts.BOOL_AND)));
            Assert.IsFalse(listSemantic.Contains(definition.GetSemantica(GPConsts.BOOL_NOT)));

            definition.AddSemanticaToList(listName, definition.GetSemantica(GPConsts.BOOL_NOT));
            Assert.IsTrue(listSemantic.Contains(definition.GetSemantica(GPConsts.BOOL_NOT)));
            definition.AddSemanticaToList(listName, definition.GetSemantica(GPConsts.BOOL_OR));

            definition.AddSemanticaToList(listName, definition.GetSemantica(GPConsts.COMP_DIF));
            definition.AddSemanticaToList(listName, definition.GetSemantica(GPConsts.COMP_EQUAL));
            definition.AddSemanticaToList(listName, definition.GetSemantica(GPConsts.COMP_GREATER));
            definition.AddSemanticaToList(listName, definition.GetSemantica(GPConsts.COMP_GREATER_EQ));
            definition.AddSemanticaToList(listName, definition.GetSemantica(GPConsts.COMP_LOWER));
            definition.AddSemanticaToList(listName, definition.GetSemantica(GPConsts.COMP_LOWER_EQ));

            definition.AddSemanticaToList(listName, definition.GetSemantica(GPConsts.OPER_ADD));
            definition.AddSemanticaToList(listName, definition.GetSemantica(GPConsts.OPER_DIVIDE));
            definition.AddSemanticaToList(listName, definition.GetSemantica(GPConsts.OPER_MULTIPLY));
            definition.AddSemanticaToList(listName, definition.GetSemantica(GPConsts.OPER_SUBTRACT));

            definition.AddSemanticaToList(listName, definition.GetSemantica(GPConsts.NUMBER_DEFAULT));


            return definition;
        }

        [TestMethod]
        public void TestaGPCriacaoPelaLista()
        {
            GPPool pool = new GPPool(config);

            string listDefault = "default";
            GPSolutionDefinition definition = CreateSampleDefinition(listDefault);
            SemanticaList listaFormulas = CriaListaDefault(definition);
            SemanticaList listSemantic = definition.GetListByName(listDefault);

            listSemantic.maxLevels = 3;
            listSemantic.minLevels = 2;
            GPAbstractNode nodeRandom = listSemantic.CreateRandomNode(config, false);
            Assert.IsNotNull(nodeRandom);
            string toString = nodeRandom.ToString();
            Assert.IsTrue(nodeRandom.SizeLevel() >= listSemantic.minLevels && nodeRandom.SizeLevel() <= listSemantic.maxLevels, "SizeLevel:" + nodeRandom.SizeLevel());
        }

        private SemanticaList CriaListaNumerica(GPSolutionDefinition holder)
        {
            string listName = "numericList";
            GPSemanticaNumber semanticaN1 = new GPSemanticaNumber("0 a 100", 0, 100);
            GPSolutionDefinition sh = new GPSolutionDefinition(config);
            sh.CreateListByName(listName, 0, 0);
            sh.AddSemanticaToList(listName, semanticaN1);

            SemanticaList lista = sh.GetListByName(listName);
            Assert.IsTrue(lista.Contains(semanticaN1));
            return lista;
        }

        private static SemanticaList CriaListaDefault(GPSolutionDefinition sh)
        {
            string listName = "default";
            sh.CreateListByName(listName, config.minLevels, config.maxLevels);
            GPSemantica semanticaF1 = new GPSemanticaFormula("f1", 2, 3);
            GPSemantica semanticaF2 = new GPSemanticaFormula("f2", 2, 2);
            GPSemantica semanticaF3 = new GPSemanticaFormula("f3", 2, 3);
            GPSemantica semanticaF4 = new GPSemanticaFormula("f4", 1, 1);
            GPSemantica semanticaF5 = new GPSemanticaFormula("f5", 0, 0);
            GPSemanticaNumber semanticaN1 = new GPSemanticaNumber("0 a 100", 0, 100);

            sh.AddSemanticaToList(listName, semanticaF1);
            sh.AddSemanticaToList(listName, semanticaF2);
            sh.AddSemanticaToList(listName, semanticaF3);
            sh.AddSemanticaToList(listName, semanticaF5);
            sh.AddSemanticaToList(listName, semanticaN1);


            SemanticaList lista = sh.GetListByName(listName);
            lista.minLevels = config.minLevels;
            lista.maxLevels = config.maxLevels;
            Assert.IsTrue(lista.Contains(semanticaF1));
            Assert.IsTrue(lista.Contains(semanticaF2));
            Assert.IsTrue(lista.Contains(semanticaF3));
            Assert.IsFalse(lista.Contains(semanticaF4));
            sh.AddSemanticaToList(listName, semanticaF4);
            Assert.IsTrue(lista.Contains(semanticaF4));
            return lista;
        }

        [TestMethod]
        public void TestGPInicial()
        {
            GPSolutionDefinition sh = new GPSolutionDefinition(config);
            SemanticaList listaFormulas = CriaListaDefault(sh);
            SemanticaList listaNumerica = CriaListaNumerica(sh);

            GPTemplate template = CreateTestTemplate(config, listaFormulas, listaNumerica);
            GPPool pool = new GPPool(config);

            Assert.IsTrue(config.poolSize > 0);
            Assert.IsTrue(config.elitismRange > 0);
            Assert.IsTrue(config.mutationRatePerc > 0);
            Assert.IsTrue(config.poolSize > 0);

            pool.InitPool(template);


            Assert.IsTrue(pool.solutions.Count > 0);

            //randomizo valores iniciais para fazer ordenação
            Random rnd = new Random();
            for (int i = 0; i < pool.solutions.Count; i++)
            {
                GPSolution solution = pool.solutions[i];
                solution.fitnessResult = rnd.Next(1, 10000);
            }


            pool.SortFitness();
            for (int i = 1; i < pool.solutions.Count; i++)
            {
                Assert.IsTrue(pool.solutions[i - 1].fitnessResult >= pool.solutions[i].fitnessResult);
            }

            GPSolution sol1 = pool.solutions[1];
            GPSolution sol2 = pool.solutions[2];
            
            string splicedKey = sol1.SpliceWith(sol2);
            Assert.IsNotNull(splicedKey);

            string strOriginal=sol1.GetValue(splicedKey).ToString();
            sol1.Mutate(sol1.GetValue(splicedKey), splicedKey);
            string strMutated= sol1.GetValue(splicedKey).ToString();
            //Assert.IsFalse(strOriginal == strMutated,"Não mutou: "+strOriginal);

            GPSolution badSolution=pool.solutions[pool.solutions.Count - 1];
            pool.Mutate();
            Assert.IsFalse(pool.solutions.Contains(badSolution));
            Assert.IsTrue(pool.solutions.Count == config.poolSize, pool.solutions.Count +"<>"+ config.poolSize);

        }

        [TestMethod]
        public void TestGPNode()
        {

            //pai: PAI(FILHO,NUMBER)
            GPSemantica semanticaPai = new GPSemanticaFormula("PAI", 2, 2);
            GPSemantica semanticaPai2 = new GPSemanticaFormula("PAI2", 2, 3);
            Assert.IsFalse(semanticaPai.IsTerminal);

            //filho: TERMINAL

            GPSemantica semanticaNumber = new GPSemanticaNumber("Numero de 0 a 10", 0, 10000);

            GPSemantica semanticaFilho = new GPSemanticaFormula("FILHO", 0, 0);
            //neto: NETO(NUMBER)
            GPSemantica semanticaNeto = new GPSemanticaFormula("NETO", 1, 2);
            // GPSemantica semanticaNumber2 = new GPSemantica("NETO");
            Assert.IsTrue(semanticaFilho.IsTerminal);

            GPAbstractNode nodePai = new GPNode(semanticaPai);
            GPAbstractNode nodePai2 = new GPNode(semanticaPai2);
            GPAbstractNode nodeFilho = new GPNode(semanticaFilho);
            GPAbstractNode nodeNeto = new GPNode(semanticaNeto);
            GPAbstractNode nodeNumber = new GPNodeNumber(semanticaNumber, 10);
            GPAbstractNode nodeNumber2 = new GPNodeNumber(semanticaNumber, 1050.123f);

            Assert.IsTrue(nodePai.CanAddNode(nodeFilho));
            Assert.IsTrue(nodePai.CanAddNode(nodeNumber));
            nodePai.AddNode(nodeFilho);
            Assert.IsFalse(nodePai.CanAddNode(nodeFilho));//já foi adicionado
            Assert.IsTrue(nodePai.CanAddNode(nodeNumber));
            nodePai.AddNode(nodeNumber);
            Assert.IsFalse(nodePai.CanAddNode(nodeFilho));
            Assert.IsFalse(nodePai.CanAddNode(nodeNumber));

            Assert.IsFalse(nodeNumber.CanAddNode(nodeFilho));
            Assert.IsFalse(nodeNumber.CanAddNode(nodeNumber));
            Assert.IsFalse(nodeFilho.CanAddNode(nodeNumber));
            Assert.IsFalse(nodeFilho.CanAddNode(nodeFilho));
            Assert.IsFalse(nodeFilho.CanAddNode(nodeNeto));

            Assert.IsTrue(nodePai.ToString() == "PAI(FILHO,10)", nodePai.ToString() + "<>" + "PAI(FILHO,10)");

            Assert.IsFalse(nodeNeto.CanAddNode(nodeFilho));

            Assert.IsTrue(nodePai.ContainsNode(nodeFilho));
            Assert.IsFalse(nodePai.ContainsNode(nodeNeto));

            nodePai2.AddNode(nodeNeto);
            Assert.IsTrue(nodePai2.ContainsNode(nodeNeto));
            Assert.IsTrue(nodeNeto.nodePai == nodePai2);

            Assert.IsTrue(nodePai.TransferNode(nodeFilho, nodeNeto));
            Assert.IsFalse(nodePai.ContainsNode(nodeFilho));
            Assert.IsTrue(nodePai.ContainsNode(nodeNeto));
            Assert.IsTrue(nodePai2.ContainsNode(nodeFilho));

            Assert.IsTrue(nodeNeto.nodePai == nodePai, nodeNeto.nodePai + "<>" + nodePai);
            Assert.IsTrue(nodeFilho.nodePai == nodePai2, nodeFilho.nodePai.ToString());
            nodeNeto.AddNode(nodeNumber2);

            Assert.IsTrue(nodePai.ToString() == "PAI(NETO(1050.123),10)", nodePai.ToString() + "<>" + "PAI(NETO(1050.123),10)");
            Assert.IsTrue(nodePai.Size() == 4, nodePai.Size() + "<>" + 4);
            Assert.IsTrue(nodeNumber.Size() == 1, nodeNumber.Size() + "<>" + 1);

            Assert.IsTrue(nodePai.SizeLevel() == 3, nodePai.SizeLevel() + "<>" + 3);

        }
    }
}
