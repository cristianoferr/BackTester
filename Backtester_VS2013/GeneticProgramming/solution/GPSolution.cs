
using GeneticProgramming.nodes;
using GeneticProgramming.semantica;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UsoComum;
namespace GeneticProgramming.solution
{
    [DataContract]
    public class GPSolution
    {
        public GPSolution(GPTemplate template)
        {
            valores = new Dictionary<string, GPAbstractNode>();
            propriedadesDeNegocio = new Dictionary<string, object>();
            this.template = template;
        }

        #region Properties
        private Dictionary<string, object> propriedadesDeNegocio { get; set; }
        private Dictionary<string, GPAbstractNode> valores { get; set; }

        //Será usado como base para o processo de ordenação de resultados e deve ser calculado pelo programa mestre...
        public float fitnessResult { get; set; }
        #endregion


        public object GetPropriedade(string key)
        {
            if (!propriedadesDeNegocio.ContainsKey(key)) return null;
            return propriedadesDeNegocio[key];
        }

        public void SetPropriedade(string key, object node)
        {
            propriedadesDeNegocio.Add(key, node);
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
            return float.Parse(GetValue(var).ToString());
        }

        internal void Mutate()
        {
            throw new System.NotImplementedException();
        }

        public GPTemplate template { get; set; }

        static Random rnd = new Random();
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
                string key = validKeys[rnd.Next(0, validKeys.Count)];
                GPAbstractNode rootNode1 = valores[key];
                GPAbstractNode rootNode2 = mateWith.valores[key];
                int count = 0;
                GPAbstractNode rndNode1 = rootNode1.GetNthChild(rnd.Next(2, rootNode1.Size()), ref count);
                count = 0;
                GPAbstractNode rndNode2 = rootNode2.GetNthChild(rnd.Next(2, rootNode2.Size()), ref count);
                if (!rndNode1.nodePai.TransferNode(rndNode1, rndNode2))
                {
                    Utils.Error("Erro fazendo transferNode!");
                    return null;
                }
                return key;
            }
            else
            {
                Utils.Error("Nenhuma chave válida com tamanho minimo de " + minSize);
                return null;
            }

        }

    }
}
