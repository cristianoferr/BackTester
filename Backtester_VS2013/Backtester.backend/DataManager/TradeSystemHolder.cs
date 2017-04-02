
using Backtester.backend.model.system;
using Backtester.backend.model.system.condicoes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
namespace Backtester.backend.DataManager
{
    [DataContract]
    public class TradeSystemHolder
    {

        [DataMember]
        private IList<TradeSystem> tradeSystems { get; set; }

        public TradeSystemHolder()
        {
            tradeSystems = new List<TradeSystem>();
        }

        public static TradeSystemHolder LoadSaved()
        {
            FileStream fileStream=null;
            try
            {
                 fileStream = File.Open("TradeSystems.js", FileMode.Open);
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(TradeSystemHolder));
                TradeSystemHolder config = (TradeSystemHolder)ser.ReadObject(fileStream);
                fileStream.Close();
                if (config.Count == 0)
                {
                    return new TradeSystemHolder();
                }
                else
                {
                    config.UpdateVarID();
                }
                return config;
            }
            catch (System.Exception e)
            {
                Util.Error("Erro ao carregar TradeSystems: " + e.Message);
                if (fileStream!=null)
                    fileStream.Close();
            }
            return new TradeSystemHolder();
        }

        private void UpdateVarID()
        {
            int maxId = 0;
            foreach (TradeSystem ts in tradeSystems)
            {
                foreach (Variavel v in ts.vm.variaveis)
                {
                    if (v.uniqueID > maxId)
                    {
                        maxId = v.uniqueID;
                    }
                }
            }
            maxId++;
            Variavel.countVars = maxId;
        }

        public void SaveToFile()
        {
            Util.Info("Salvando TradeSystems... qtd:" + tradeSystems.Count);
            if (tradeSystems.Count == 0) return;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(TradeSystemHolder));
            var fileStream = File.Create("TradeSystems.js");
            ser.WriteObject(fileStream, this);
            fileStream.Close();
        }

        public TradeSystem GetTS(int index)
        {
            if (tradeSystems.Count > index && index>=0)
            {
                return tradeSystems[index];
            }
            return null;
        }

        public TradeSystem GetTS(string name)
        {
            return tradeSystems.Where(x => x.name == name).FirstOrDefault();
        }

        public void RemoveTradeSystem(int p)
        {
            tradeSystems.RemoveAt(p);
        }

        public int Count { get { return tradeSystems.Count; } }

        public void AddTS(TradeSystem ts)
        {
            tradeSystems.Add(ts);
        }
    }
}
