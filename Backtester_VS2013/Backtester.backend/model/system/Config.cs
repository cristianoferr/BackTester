using Backtester.backend.DataManager;
using Backtester.backend.model.system.condicoes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
namespace Backtester.backend.model.system
{
    [DataContract]
    public class Config
    {

        public Config()
        {
            flagCompra = true;
            flagVenda = true;
            variaveis = new List<Variavel>();
            capitalInicial = 100000;
            custoOperacao = 20;
            campoVenda = FormulaManager.CLOSE;
            campoCompra = FormulaManager.CLOSE;
            riscoTrade = 1.5f;
            stopMensal = 10;
            maxCapitalTrade = 100000;
            percTrade = 25; //Isso informa quanto do capital eu posso ter por ativo
        }

        [DataMember]
        public float riscoTrade { get; set; }
        IList<Variavel> variaveis;
        internal float GetVariavelValue(string n)
        {
            return GetVariavelValue(int.Parse(n));
        }
        public float GetVariavelValue(int id)
        {
            return GetVariavel(id).getAtual();
        }

        [DataMember]
        public bool flagCompra { get; set; }

        [DataMember]
        public bool flagVenda { get; set; }

        [DataMember]
        public float capitalInicial { get; set; }

        internal void LimpaVariaveis()
        {
            variaveis.Clear();
        }

        internal void AddVariavel(Variavel variavel)
        {
            variaveis.Add(variavel);
        }



        internal int GetVariaveisSize()
        {
            return variaveis.Count;
        }

        internal Variavel GetVariavel(int id)
        {
            return variaveis.Where(x => x.id == id).FirstOrDefault();
        }

        [DataMember]
        public float custoOperacao { get; set; }

        [DataMember]
        public string campoVenda { get; set; }

        [DataMember]
        public float stopMensal { get; set; }

        [DataMember]
        public string campoCompra { get; set; }

        [DataMember]
        public float maxCapitalTrade { get; set; }

        [DataMember]
        public float percTrade { get; set; }

        public static Config LoadSaved()
        {
            try
            {
                var fileStream = File.Open("saved-config.js", FileMode.Open);
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Config));
                Config config = (Config)ser.ReadObject(fileStream);
                fileStream.Close();
                return config;
            }
            catch (System.Exception e)
            {

            }
            return new Config();
        }

        public void SaveToFile()
        {

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Config));
            var fileStream = File.Create("saved-config.js");
            ser.WriteObject(fileStream, this);
            fileStream.Close();

        }
    }
}
