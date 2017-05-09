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
            qtdPercPapeis = 30;//se eu tenho 100 papeis a testar então isso fará com que seja retornado uma lsita com x perc de 100
            InitDefaultPapeis();
            tipoPeriodo = Consts.PERIODO_ACAO.DIARIO;

            gpVars = "";
            AddGPVar(Consts.VAR_MAX_CAPITAL_TRADE, 20000);
            AddGPVar(Consts.VAR_MULTIPLAS_ENTRADAS, 1);
            AddGPVar(Consts.VAR_PERC_TRADE, 20);
            AddGPVar(Consts.VAR_RISCO_GLOBAL, 6);
            AddGPVar(Consts.VAR_RISCO_TRADE, 2);
            AddGPVar(Consts.VAR_STOP_GAP, 3);
            AddGPVar(Consts.VAR_STOP_MENSAL, 10);
            AddGPVar(Consts.VAR_USA_STOP_MOVEL, 1);

        }

        public void AddGPVar(string var, int valor)
        {
            gpVars += var + "=" + valor + "\r\n";
        }

        private void InitDefaultPapeis()
        {
            papeis.Add("ABEV3");
            papeis.Add("BBAS3");
            papeis.Add("BBDC4");
            papeis.Add("BBSE3");
            papeis.Add("BRAP4");
            papeis.Add("BRFS3");
            papeis.Add("BRKM5");
            papeis.Add("BRML3");
            papeis.Add("BVMF3");
            papeis.Add("CCRO3");
            papeis.Add("CIEL3");
            papeis.Add("CMIG4");
            papeis.Add("CPFE3");
            papeis.Add("CPLE3");
            papeis.Add("CSAN3");
            papeis.Add("CSNA3");
            papeis.Add("CYRE3");
            papeis.Add("EMBR3");
            papeis.Add("ENBR3");
            papeis.Add("FIBR3");
            papeis.Add("GGBR4");
            papeis.Add("GOAU4");
            papeis.Add("HYPE3");
            papeis.Add("ITSA4");
            papeis.Add("ITUB4");
            papeis.Add("JBSS3");
            papeis.Add("KLBN11");
            papeis.Add("KROT3");
            papeis.Add("LAME4");
            papeis.Add("LREN3");
            papeis.Add("MRFG3");
            papeis.Add("MRVE3");
            papeis.Add("MULT3");
            papeis.Add("NATU3");
            papeis.Add("PCAR4");
            papeis.Add("PETR4");
            papeis.Add("QUAL3");
            papeis.Add("RENT3");
            papeis.Add("SBSP3");
            papeis.Add("SMLE3");
            papeis.Add("SUZB5");
            papeis.Add("TIMP3");
            papeis.Add("UGPA3");
            papeis.Add("USIM5");
            papeis.Add("VALE5");
            papeis.Add("VIVT4");
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

        [DataMember]
        public Consts.PERIODO_ACAO tipoPeriodo { get; set; }

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

        [DataMember]
        public string gpVars { get; set; }

        public bool IsGPVarDefined(string var)
        {
            string[] vars = gpVars.Split('\n');
            foreach (string v in vars)
            {
                if (v.StartsWith(var+"="))
                {
                    return true;
                }
            }
            return false;
        }

        public float GetGPVarValue(string var)
        {
            string[] vars = gpVars.Split('\n');
            foreach (string v in vars)
            {
                if (v.StartsWith(var + "="))
                {
                    string valor = v.Substring(v.IndexOf("=") + 1);
                    valor = valor.Substring(0, valor.IndexOf('\r'));
                    return int.Parse(valor);
                }
            }
            return 0;
        }

        
    }
}
