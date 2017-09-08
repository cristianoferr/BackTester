using Backtester.backend.DataManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace FrontTester.frontend.model
{
    [DataContract]
    public class FrontData
    {
        [DataMember]
        internal float capitalAtual;

        [DataMember]
        public CandidatoData tradeSystemEmUso { get; set; }

        public static FrontData LoadSaved()
        {
            try
            {
                var fileStream = File.Open("frontdata.js", FileMode.Open);
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(FrontData));
                FrontData config = (FrontData)ser.ReadObject(fileStream);
                fileStream.Close();
                return config;
            }
            catch (System.Exception e)
            {

            }
            return new FrontData();
        }

        public void SaveToFile()
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(FrontData));
            var fileStream = File.Create("frontdata.js");
            ser.WriteObject(fileStream, this);
            fileStream.Close();
        }

    }
}
