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
            flagVenda = false;

            capitalInicial = 10000;
            custoOperacao = 20;
            campoVenda = FormulaManager.CLOSE;
            campoCompra = FormulaManager.CLOSE;
            papeis = new List<string>();
            maxTestes = 20;
            qtdPercPapeis = 50;//se eu tenho 100 papeis a testar então isso fará com que seja retornado uma lsita com x perc de 100
            InitDefaultPapeis();
        }

        private void InitDefaultPapeis()
        {
            papeis.Add("ABEV3");
            papeis.Add("BBAS3");
            papeis.Add("BBDC4");
            papeis.Add("BRAP4");
            papeis.Add("BRFS3");
            papeis.Add("BRKM5");
            papeis.Add("BVMF3");
            papeis.Add("CCRO3");
            papeis.Add("CIEL3");
            papeis.Add("CMIG4");
            papeis.Add("CPFE3");
            papeis.Add("CSNA3");
            papeis.Add("CYRE3");
            papeis.Add("EMBR3");
            papeis.Add("FIBR3");
            papeis.Add("GGBR4");
            papeis.Add("ITSA4");
            papeis.Add("ITUB4");
            papeis.Add("JBSS3");
            papeis.Add("LAME4");
            papeis.Add("LREN3");
            papeis.Add("MRFG3");
            papeis.Add("NATU3");
            papeis.Add("PCAR4");
            papeis.Add("PETR4");
            papeis.Add("SUZB5");
            papeis.Add("VALE5");
            papeis.Add("USIM5");
            papeis.Add("WEGE3");
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

        [DataMember]
        public int qtdPercPapeis { get; set; }
    }
}
