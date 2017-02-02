
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
        public IList<TradeSystem> tradeSystems { get; set; }

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
                return config;
            }
            catch (System.Exception e)
            {

            }
            return new TradeSystemHolder();
        }

        public void SaveToFile()
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(TradeSystemHolder));
            var fileStream = File.Create("TradeSystems.js");
            ser.WriteObject(fileStream, this);
            fileStream.Close();
        }

        public TradeSystem GetTS(int index)
        {
            return tradeSystems[index];
        }
    }
}
