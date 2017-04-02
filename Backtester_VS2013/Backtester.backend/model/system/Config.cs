using Backtester.backend.DataManager;
using System.Collections.Generic;
using System.IO;
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

            capitalInicial = 100000;
            custoOperacao = 20;
            campoVenda = FormulaManager.CLOSE;
            campoCompra = FormulaManager.CLOSE;
            papeis = new List<string>();
            maxTestes = 100;
        }

        [DataMember]
        public string varsDebug { get; set; }
        [DataMember]
        public IList<string> papeis { get; set; }

        [DataMember]
        public bool flagCompra { get; set; }

        [DataMember]
        public bool flagVenda { get; set; }

        [DataMember]
        public float capitalInicial { get; set; }

        [DataMember]
        public float custoOperacao { get; set; }

        [DataMember]
        public string campoVenda { get; set; }

        [DataMember]
        public string campoCompra { get; set; }

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



        public void AddPapel(string papel)
        {
            if (!papeis.Contains(papel))
                papeis.Add(papel);
        }

        public void RemovePapel(string papel)
        {
            papeis.Remove(papel);
        }


        [DataMember]
        public int maxTestes { get; set; }
    }
}
