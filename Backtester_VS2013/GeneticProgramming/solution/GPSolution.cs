
using GeneticProgramming.nodes;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace GeneticProgramming.solution
{
    [DataContract]
    public class GPSolution
    {
        public GPSolution()
        {
            valores = new Dictionary<string, GPAbstractNode>();
        }

        #region Properties
        private Dictionary<string,GPAbstractNode> valores{ get; set; }

        //Será usado como base para o processo de ordenação de resultados e deve ser calculado pelo programa mestre...
        public float fitnessResult { get; set; }
        #endregion

        public GPAbstractNode GetValue(string key)
        {
            if (!valores.ContainsKey(key)) return null;
            return valores[key];
        }

        internal void SetValue(string key, GPAbstractNode node)
        {
            valores.Add(key, node);
        }
    }
}
