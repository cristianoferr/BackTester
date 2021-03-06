﻿
using GeneticProgramming.nodes;
using GeneticProgramming.semantica;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using UsoComum;
using UsoComum.interfaces;
namespace GeneticProgramming.solution
{
    [DataContract]

    public class GPSolution : IStoreProperties
    {
        static int countSolutions = 1;

        public GPSolution(GPTemplate template)
        {
            valores = new Dictionary<string, GPAbstractNode>();
            propriedadesDeNegocio = new Dictionary<string, object>();
            this.template = template;
            idSolution = countSolutions++;
            name = "Solution " + idSolution;
            iterations = 0;

        }

        public override string ToString()
        {
            return name;
        }

        public GPSolution Clone()
        {
            GPSolution result = new GPSolution(template);

            foreach (string key in valores.Keys)
            {
                GPAbstractNode node = valores[key].Clone();
                result.valores.Add(key, node);
            }

            return result;
        }

        #region Properties
        [DataMember]
        public int idSolution { get; set; }
        [DataMember]
        private Dictionary<string, object> propriedadesDeNegocio { get; set; }


        [DataMember]
        public int iterations { get; set; }
        [DataMember]
        private Dictionary<string, GPAbstractNode> valores { get; set; }
        [DataMember]
        public string name { get; set; }

        //Será usado como base para o processo de ordenação de resultados e deve ser calculado pelo programa mestre...
        public double fitnessResult { get; set; }

        public GPTemplate template { get; set; }
        #endregion


        public object GetPropriedade(string key)
        {
            if (!propriedadesDeNegocio.ContainsKey(key)) return null;
            return propriedadesDeNegocio[key];
        }

        public float GetPropriedadeAsFloat(string propriedade)
        {
            object vlr = GetPropriedade(propriedade);
            return vlr == null ? 0 : (float)vlr;
        }

        public void SetPropriedade(string key, object node)
        {
            if (propriedadesDeNegocio.ContainsKey(key))
            {
                propriedadesDeNegocio.Remove(key);
            }
            propriedadesDeNegocio.Add(key, node);
        }

        public void RemovePropriedade(string key)
        {
            if (!propriedadesDeNegocio.ContainsKey(key)) return;
            propriedadesDeNegocio.Remove(key);

        }

        public GPAbstractNode GetValue(string key)
        {
            if (!valores.ContainsKey(key)) return null;
            return valores[key];
        }

        internal void SetValue(string key, GPAbstractNode node)
        {
            valores.Add(key, node);
        }

        public float GetValueAsNumber(string var)
        {
            if (GetValue(var) == null) return 0;
            return float.Parse(GetValue(var).ToString().Replace(".",","));
        }

        public string GetValueAsString(string var)
        {
            GPAbstractNode node = GetValue(var);
            if (node == null) return "";
            return node.ToString();
        }

        public void Mutate(int chance)
        {
            foreach (string key in valores.Keys)
            {
                int rnd = Utils.RandomInt(0, 100);
                if (rnd < chance) 
                {
                    Mutate(valores[key], key);
                }
            }

        }

        public void Mutate(GPAbstractNode node, string property)
        {
            node.Mutate(template.GetProperty(property));
        }


        public string SpliceWith(GPSolution mateWith)
        {
            int minSize = 2;
            List<string> validKeys = new List<string>();
            foreach (string key in valores.Keys)
            {
                if (valores[key].Size() > minSize && mateWith.valores[key].Size() > minSize)
                {
                    validKeys.Add(key);
                }
            }

            if (validKeys.Count > 0)
            {
                string key = validKeys[Utils.RandomInt(0, validKeys.Count)];
                return SpliceWith(mateWith, key);
            }
            else
            {
                Utils.Error("Nenhuma chave válida com tamanho minimo de " + minSize);
                return null;
            }

        }

        private string SpliceWith(GPSolution mateWith, string key)
        {
            if (!valores.ContainsKey(key)) return null;
            if (!mateWith.valores.ContainsKey(key)) return null;
            GPAbstractNode rootNode1 = valores[key];
            GPAbstractNode rootNode2 = mateWith.valores[key];

            if (rootNode1.Size() > 1 && rootNode2.Size() > 1)
            {
                int count = 0;
                GPAbstractNode rndNode1 = rootNode1.GetNthChild(Utils.RandomInt(2, rootNode1.Size()), ref count);
                count = 0;
                GPAbstractNode rndNode2 = rootNode2.GetNthChild(Utils.RandomInt(2, rootNode2.Size()), ref count);
                if (!rndNode1.nodePai.TransferNode(rndNode1, rndNode2))
                {
                    Utils.Error("Erro fazendo transferNode!");
                    return null;
                }
            }
            else
            {
                valores.Remove(key);
                mateWith.valores.Remove(key);
                valores.Add(key, rootNode2);
                mateWith.valores.Add(key, rootNode1);
            }
            return key;
        }



        public GPSolution CreateChildWith(GPSolution solution2, out GPSolution child2)
        {
            child2 = solution2.Clone();
            GPSolution child = this.Clone();

            foreach (string key in valores.Keys)
            {
                /* GPAbstractNode node1 = child.valores[key];
                 GPAbstractNode node2 = child2.valores[key];*/
                child.SpliceWith(child2, key);

            }

            return child;
        }



        internal void FinishLoading(GPSolutionDefinition definition)
        {
            if (idSolution > countSolutions)
            {
                countSolutions = idSolution + 1;
            }
            propriedadesDeNegocio = new Dictionary<string, object>();
            foreach (string key in valores.Keys)
            {
                GPAbstractNode node = valores[key];
                node.FinishLoading(definition);
            }
        }








    }
}
