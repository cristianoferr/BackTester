using GeneticProgramming.nodes;
using GeneticProgramming.semantica;
using GeneticProgramming.solution;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticProgramming.tests
{
    [TestClass]
    public class GeneticProgrammingTest
    {

        [TestMethod]
        public void TestGPTemplate()
        {
            GPConfig config = new GPConfig();
            GPHolder holder = new GPHolder();

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


        }

        private static GPTemplate CreateTestTemplate(GPConfig config, SemanticaList listaFormulas, SemanticaList listaNumerica)
        {
            GPTemplate template = new GPTemplate(config);
            template.AddProperty("prop1", listaFormulas);
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
            GPConfig config = new GPConfig();
            GPHolder holder = new GPHolder();
            Assert.IsNotNull(holder.GetSemantica(GPHolder.BOOL_AND));
            Assert.IsTrue(holder.GetSemantica(GPHolder.BOOL_AND).nodeType == GPConsts.GPNODE_TYPE.NODE_BOOLEAN);
            Assert.IsNotNull(holder.GetSemantica(GPHolder.COMP_EQUAL));
            Assert.IsTrue(holder.GetSemantica(GPHolder.COMP_EQUAL).nodeType == GPConsts.GPNODE_TYPE.NODE_COMPARER);
            Assert.IsNotNull(holder.GetSemantica(GPHolder.OPER_MULTIPLY));
            Assert.IsTrue(holder.GetSemantica(GPHolder.OPER_MULTIPLY).nodeType == GPConsts.GPNODE_TYPE.NODE_OPERATOR);
        }

        [TestMethod]
        public void TestaGPSolutionDefinition()
        {
            GPConfig config = new GPConfig();
            GPHolder holder = new GPHolder();


            string listDefault = "default";
            GPSolutionDefinition definition = CreateSampleDefinition(listDefault, holder);
            SemanticaList listSemantic = definition.GetListByName(listDefault);
        }

        private GPSolutionDefinition CreateSampleDefinition(string listName, GPHolder holder)
        {
            GPSolutionDefinition definition = new GPSolutionDefinition(holder);
            definition.AddSemantica(listName, holder.GetSemantica(GPHolder.BOOL_AND));
            SemanticaList listSemantic = definition.GetListByName(listName);
            Assert.IsNotNull(listSemantic);
            Assert.IsTrue(listSemantic.Contains(holder.GetSemantica(GPHolder.BOOL_AND)));
            Assert.IsFalse(listSemantic.Contains(holder.GetSemantica(GPHolder.BOOL_NOT)));

            definition.AddSemantica(listName, holder.GetSemantica(GPHolder.BOOL_NOT));
            Assert.IsTrue(listSemantic.Contains(holder.GetSemantica(GPHolder.BOOL_NOT)));
            definition.AddSemantica(listName, holder.GetSemantica(GPHolder.BOOL_OR));

            definition.AddSemantica(listName, holder.GetSemantica(GPHolder.COMP_DIF));
            definition.AddSemantica(listName, holder.GetSemantica(GPHolder.COMP_EQUAL));
            definition.AddSemantica(listName, holder.GetSemantica(GPHolder.COMP_GREATER));
            definition.AddSemantica(listName, holder.GetSemantica(GPHolder.COMP_GREATER_EQ));
            definition.AddSemantica(listName, holder.GetSemantica(GPHolder.COMP_LOWER));
            definition.AddSemantica(listName, holder.GetSemantica(GPHolder.COMP_LOWER_EQ));

            definition.AddSemantica(listName, holder.GetSemantica(GPHolder.OPER_ADD));
            definition.AddSemantica(listName, holder.GetSemantica(GPHolder.OPER_DIVIDE));
            definition.AddSemantica(listName, holder.GetSemantica(GPHolder.OPER_MULTIPLY));
            definition.AddSemantica(listName, holder.GetSemantica(GPHolder.OPER_SUBTRACT));

            definition.AddSemantica(listName, holder.GetSemantica(GPHolder.NUMBER_DEFAULT));


            return definition;
        }

        [TestMethod]
        public void TestaGPCriacaoPelaLista()
        {
            GPConfig config = new GPConfig();
            GPHolder holder = new GPHolder();
            GPPool pool = new GPPool(config,holder);
            SemanticaList listaFormulas = CriaListaDefault(holder);

            string listDefault = "default";
            GPSolutionDefinition definition = CreateSampleDefinition(listDefault, holder);
            SemanticaList listSemantic = definition.GetListByName(listDefault);

            config.maxLevels = 3;
            config.minLevels = 2;
            GPAbstractNode nodeRandom = listSemantic.CreateRandomNode(config, false);
            Assert.IsNotNull(nodeRandom);
            string toString = nodeRandom.ToString();
            Assert.IsTrue(nodeRandom.SizeLevel() >= config.minLevels && nodeRandom.SizeLevel() <= config.maxLevels, "SizeLevel:" + nodeRandom.SizeLevel());
        }

        private SemanticaList CriaListaNumerica(GPHolder holder)
        {
            string listName = "numericList";
            GPSemanticaNumber semanticaN1 = new GPSemanticaNumber("0 a 100", 0, 100);
            GPSolutionDefinition sh = new GPSolutionDefinition(holder);
            sh.AddSemantica(listName, semanticaN1);

            SemanticaList lista = sh.GetListByName(listName);
            Assert.IsTrue(lista.Contains(semanticaN1));
            return lista;
        }

        private static SemanticaList CriaListaDefault(GPHolder holder)
        {
            string listName = "default";

            GPSemantica semanticaF1 = new GPSemanticaFormula("f1", 2, 3);
            GPSemantica semanticaF2 = new GPSemanticaFormula("f2", 2, 2);
            GPSemantica semanticaF3 = new GPSemanticaFormula("f3", 2, 3);
            GPSemantica semanticaF4 = new GPSemanticaFormula("f4", 1, 1);
            GPSemantica semanticaF5 = new GPSemanticaFormula("f5", 0, 0);
            GPSemanticaNumber semanticaN1 = new GPSemanticaNumber("0 a 100", 0, 100);

            GPSolutionDefinition sh = new GPSolutionDefinition(holder);
            sh.AddSemantica(listName, semanticaF1);
            sh.AddSemantica(listName, semanticaF2);
            sh.AddSemantica(listName, semanticaF3);
            sh.AddSemantica(listName, semanticaF5);
            sh.AddSemantica(listName, semanticaN1);


            SemanticaList lista = sh.GetListByName(listName);
            Assert.IsTrue(lista.Contains(semanticaF1));
            Assert.IsTrue(lista.Contains(semanticaF2));
            Assert.IsTrue(lista.Contains(semanticaF3));
            Assert.IsFalse(lista.Contains(semanticaF4));
            sh.AddSemantica(listName, semanticaF4);
            Assert.IsTrue(lista.Contains(semanticaF4));
            return lista;
        }

        [TestMethod]
        public void TestGPInicial()
        {
            GPHolder holder = new GPHolder();
            SemanticaList listaFormulas = CriaListaDefault(holder);
            SemanticaList listaNumerica = CriaListaNumerica(holder);

            GPConfig config = new GPConfig();
            GPTemplate template = CreateTestTemplate(config, listaFormulas, listaNumerica);
            GPPool pool = new GPPool(config, holder);

            Assert.IsTrue(config.poolQtd > 0);
            Assert.IsTrue(config.elitismPercent > 0);
            Assert.IsTrue(config.mutationRatePerc > 0);
            Assert.IsTrue(config.poolQtd > 0);

            pool.InitPool(template);
            Assert.IsTrue(pool.solutions.Count > 0);

        }

        [TestMethod]
        public void TestGPNode()
        {

            //pai: PAI(FILHO,NUMBER)
            GPSemantica semanticaPai = new GPSemanticaFormula("PAI", 2, 2);
            GPSemantica semanticaPai2 = new GPSemanticaFormula("PAI2", 2, 3);
            Assert.IsFalse(semanticaPai.IsTerminal);

            //filho: TERMINAL

            GPSemantica semanticaNumber = new GPSemanticaNumber("Numero de 0 a 10", 0, 10);

            GPSemantica semanticaFilho = new GPSemanticaFormula("FILHO", 0, 0);
            //neto: NETO(NUMBER)
            GPSemantica semanticaNeto = new GPSemanticaFormula("NETO", 1, 2);
            // GPSemantica semanticaNumber2 = new GPSemantica("NETO");
            Assert.IsTrue(semanticaFilho.IsTerminal);

            GPAbstractNode nodePai = new GPNodeFormula(semanticaPai);
            GPAbstractNode nodePai2 = new GPNodeFormula(semanticaPai2);
            GPAbstractNode nodeFilho = new GPNodeFormula(semanticaFilho);
            GPAbstractNode nodeNeto = new GPNodeFormula(semanticaNeto);
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
