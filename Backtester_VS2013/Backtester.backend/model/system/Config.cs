using Backtester.backend.DataManager;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System;

namespace Backtester.backend.model.system
{
    [DataContract]
    public class PapelList
    {
        [DataMember]
        public IList<string> listaPapeis { get; private set; }

        public PapelList()
        {
            listaPapeis = new List<string>();
        }

        internal void Add(string v)
        {
            if (!listaPapeis.Contains(v))
            listaPapeis.Add(v);
        }
    }

    [DataContract]
    public class ListaPapeis
    {
        Dictionary<string, IList<PapelList>> dictPapeis;
        public ListaPapeis()
        {
            dictPapeis = new Dictionary<string, IList<PapelList>>();
        }

        internal void AddLista(string nomeLista, int v)
        {
            IList<PapelList> lista = new List<PapelList>(v);
            dictPapeis.Add(nomeLista, lista);
            for (int i = 0; i < v; i++)
            {
                lista.Add(new PapelList());
            }
        }

        internal PapelList GetLista(string nomeLista, int loop)
        {
            IList<PapelList> lista = dictPapeis[nomeLista];
            return lista[loop%lista.Count];
        }
    }

        [DataContract]
    public class Config
    {
        public int MAX_LISTA_GERACAO = 2;
        public int MAX_LISTA_VALIDACAO = 2;
        public string NOME_LISTA_GERACAO = Consts.TIPO_CARGA_ATIVOS.GERA_CANDIDATOS.ToString();
        public string NOME_LISTA_VALIDACAO = Consts.TIPO_CARGA_ATIVOS.VALIDA_CANDIDATO.ToString();
        public string NOME_LISTA_FINAL = Consts.TIPO_CARGA_ATIVOS.DADOS_ATUAIS.ToString();

        public bool useVars=true;
        

        public Config()
        {
            flagCompra = true;
            flagVenda = false;

            capitalInicial = 10000;
            custoOperacao = 20;
            flagCompraMesmoDia = false;
            flagVendaMesmoDia = false;
            papeis = new ListaPapeis();
            papeis.AddLista(NOME_LISTA_GERACAO, MAX_LISTA_GERACAO);
            papeis.AddLista(NOME_LISTA_VALIDACAO, MAX_LISTA_VALIDACAO);
            papeis.AddLista(NOME_LISTA_FINAL, 1);
            maxTestes = 20;
            qtdPercPapeis = 60;//se eu tenho 100 papeis a testar então isso fará com que seja retornado uma lsita com x perc de 100
            InitDefaultPapeis();
            InitDefaultPapeisValidation();
            InitDefaultPapeisFinal();
            tipoPeriodo = Consts.PERIODO_ACAO.SEMANAL;

            //Quantidade minima de iterações que a solution sobreviveu que a qualifica como viavel
            saveMinIterations = 5;
            //Ratio minimo para salvar calculado na forma: totalWin/(totalWin+totalLoss)
            saveMinProfitRatio = 0.7f;


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

        internal IList<string> GetLista(Consts.TIPO_CARGA_ATIVOS tipoCarga, int loopNumber)
        {
            return papeis.GetLista(tipoCarga.ToString(), loopNumber).listaPapeis;
        }

        //TODO: separar a configuração dos papeis daqui: muito embolado
        private void InitDefaultPapeisFinal()
        {
            AddPapel(NOME_LISTA_FINAL,0,"BOV", "ABEV3");
            AddPapel(NOME_LISTA_FINAL , 0, "BOV", "BBAS3");
            AddPapel(NOME_LISTA_FINAL , 0, "BOV", "BBDC4");
            AddPapel(NOME_LISTA_FINAL , 0, "BOV", "BRAP4");
            AddPapel(NOME_LISTA_FINAL , 0, "BOV", "BRFS3");
            AddPapel(NOME_LISTA_FINAL , 0, "BOV", "BVMF3");
            AddPapel(NOME_LISTA_FINAL , 0, "BOV", "CMIG4");
            AddPapel(NOME_LISTA_FINAL , 0, "BOV", "CSNA3");
            AddPapel(NOME_LISTA_FINAL , 0, "BOV", "GGBR4");
            AddPapel(NOME_LISTA_FINAL , 0, "BOV", "ITUB4");
            AddPapel(NOME_LISTA_FINAL , 0, "BOV", "EMBR3");
            AddPapel(NOME_LISTA_FINAL , 0, "BOV", "VALE5");
            AddPapel(NOME_LISTA_FINAL , 0, "BOV", "PETR4");
            AddPapel(NOME_LISTA_FINAL , 0, "BOV", "CMIG4");
            AddPapel(NOME_LISTA_FINAL , 0, "BOV", "PETR4");
            AddPapel(NOME_LISTA_FINAL , 0, "BOV", "SUZB5");
            AddPapel(NOME_LISTA_FINAL , 0, "BOV", "NATU3");
            AddPapel(NOME_LISTA_FINAL , 0, "BOV", "WEGE3");
            AddPapel(NOME_LISTA_FINAL , 0,"BOV", "CCRO3");
        }

        public void AddGPVar(string var, int valor)
        {
            gpVars += var + "=" + valor + "\r\n";
        }
        private void InitDefaultPapeisValidation()
        {
            AddPapelValidacao(0,"BMV", "ALFAA");
            AddPapelValidacao(0, "BMV", "BIMBOA");
            AddPapelValidacao(0, "BMV", "GMEXICOB");
            AddPapelValidacao(0, "BMV", "CULTIBAB");
            AddPapelValidacao(0, "BMV", "MASECAB");
            AddPapelValidacao(0, "BMV", "LABB");
            AddPapelValidacao(0, "BMV", "MEGACPO");
            AddPapelValidacao(0, "BMV", "AXTELCPO");
            AddPapelValidacao(0, "BMV", "CIEB");
            AddPapelValidacao(0, "BMV", "POCHTECB");
            AddPapelValidacao(0, "BMV", "ASURB");
            AddPapelValidacao(0, "BMV", "TMMA");
            AddPapelValidacao(0, "BMV", "CEMEXCPO");
            AddPapelValidacao(0, "BMV", "KOFL");
            AddPapelValidacao(0, "BMV", "AMXL");
            AddPapelValidacao(0, "BMV", "FEMSAUBD");
            AddPapelValidacao(0, "BMV", "GFNORTEO");
            AddPapelValidacao(0, "BMV", "SORIANAB");
            AddPapelValidacao(0, "BMV", "ALPEKA");
            AddPapelValidacao(0, "BMV", "GCARSOA1");
            AddPapelValidacao(0, "BMV", "LIVEPOL1");


        }

        private void AddPapelValidacao(int lista,string bolsa, string acao)
        {
            AddPapel(NOME_LISTA_VALIDACAO, lista, bolsa, acao);
        }
        public void AddPapel(string lista,int index,string bolsa, string acao)
        {
            PapelList dict = GetDictionary(lista,index);
            dict.Add(bolsa + "_" + acao);
        }

        private PapelList GetDictionary(string lista,int loop)
        {
            PapelList list = papeis.GetLista(lista, loop);
            return list;
        }



        private void InitDefaultPapeis()
        {
            AddPapel(NOME_LISTA_GERACAO , 0,"NY", "BAC");
            AddPapel(NOME_LISTA_GERACAO , 0, "NY", "VALE");
            AddPapel(NOME_LISTA_GERACAO , 0, "NY", "F");
            AddPapel(NOME_LISTA_GERACAO , 0, "NY", "GE");
            AddPapel(NOME_LISTA_GERACAO , 0, "NY", "CHK");
            AddPapel(NOME_LISTA_GERACAO , 0, "NY", "AKS");
            AddPapel(NOME_LISTA_GERACAO , 0, "NY", "GGB");
            AddPapel(NOME_LISTA_GERACAO , 0, "NY", "KO");
            AddPapel(NOME_LISTA_GERACAO , 0, "NY", "HPQ");
            AddPapel(NOME_LISTA_GERACAO , 0, "NY", "DIS");
            AddPapel(NOME_LISTA_GERACAO , 0, "NY", "MCD");
            AddPapel(NOME_LISTA_GERACAO , 0, "NY", "WFC");
            AddPapel(NOME_LISTA_GERACAO , 0, "NY", "FCX");
            AddPapel(NOME_LISTA_GERACAO , 0, "NY", "TECK");
            AddPapel(NOME_LISTA_GERACAO , 0, "NY", "TEVA");
            AddPapel(NOME_LISTA_GERACAO , 0, "NY", "WLL");
            AddPapel(NOME_LISTA_GERACAO , 0, "NY", "AUY");
            AddPapel(NOME_LISTA_GERACAO , 0, "NY", "NOK");
            AddPapel(NOME_LISTA_GERACAO , 0, "NY", "HPE");
            AddPapel(NOME_LISTA_GERACAO , 0, "NY", "COL");
            AddPapel(NOME_LISTA_GERACAO , 0, "NY", "UTX");
            AddPapel(NOME_LISTA_GERACAO , 0, "NY", "JPM");

            AddPapel(NOME_LISTA_GERACAO ,1,"BOV", "ABEV3");
            AddPapel(NOME_LISTA_GERACAO ,1, "BOV", "BBAS3");
            AddPapel(NOME_LISTA_GERACAO ,1, "BOV", "BBDC4");
            AddPapel(NOME_LISTA_GERACAO ,1, "BOV", "BBSE3");
            AddPapel(NOME_LISTA_GERACAO ,1, "BOV", "BRAP4");
            AddPapel(NOME_LISTA_GERACAO ,1, "BOV", "BRFS3");
            AddPapel(NOME_LISTA_GERACAO ,1, "BOV", "BRKM5");
            AddPapel(NOME_LISTA_GERACAO ,1, "BOV", "BRML3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","BVMF3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","CCRO3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","CIEL3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","CMIG4");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","CPFE3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","CPLE3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","CSAN3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","CSNA3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","CYRE3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","EMBR3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","ENBR3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","FIBR3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","GGBR4");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","GOAU4");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","HYPE3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","ITSA4");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","ITUB4");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","JBSS3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","KROT3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","LAME4");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","LREN3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","MRFG3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","MRVE3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","MULT3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","NATU3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","PCAR4");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","PETR4");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","QUAL3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","RENT3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","SBSP3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","SMLE3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","SUZB5");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","TIMP3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","UGPA3");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","USIM5");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","VALE5");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","VIVT4");
            AddPapel(NOME_LISTA_GERACAO ,1,"BOV","WEGE3");
        }

        [DataMember]
        public bool flagCompraMesmoDia { get; set; }
        [DataMember]
        public bool flagVendaMesmoDia { get; set; }
        [DataMember]
        public string varsDebug { get; set; }



        [DataMember]
        public ListaPapeis papeis { get; set; }

        [DataMember]
        public bool flagCompra { get; set; }

        [DataMember]
        public bool flagVenda { get; set; }

        [DataMember]
        public float capitalInicial { get; set; }

        [DataMember]
        public float custoOperacao { get; set; }


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


      

        [DataMember]
        public float saveMinProfitRatio { get; set; }
        [DataMember]
        public int saveMinIterations { get; set; }
        [DataMember]
        public int maxTestes { get; set; }

        [DataMember]
        public int qtdPercPapeis { get; set; }

        [DataMember]
        public string gpVars { get; set; }

        public bool IsGPVarDefined(string var)
        {
          /*  string[] vars = gpVars.Split('\n');
            foreach (string v in vars)
            {
                if (v.StartsWith(var + "="))
                {
                    return true;
                }
            }*/
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
