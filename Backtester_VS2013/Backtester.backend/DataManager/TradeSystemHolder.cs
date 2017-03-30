
using Backtester.backend.model.system;
using System.Collections.Generic;
using System.IO;
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
            try
            {
                var fileStream = File.Open("TradeSystems.js", FileMode.Open);
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(TradeSystemHolder));
                TradeSystemHolder config = (TradeSystemHolder)ser.ReadObject(fileStream);
                fileStream.Close();
                if (config.Count == 0)
                {
                    return new TradeSystemHolder();
                }
                return config;
            }
            catch (System.Exception e)
            {
                Util.Error("Erro ao carregar TradeSystems: " + e.Message);
            }
            return new TradeSystemHolder();
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
            return tradeSystems[index];
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
