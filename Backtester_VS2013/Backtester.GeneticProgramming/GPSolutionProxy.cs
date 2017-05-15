using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using GeneticProgramming.solution;
using Backtester.backend.model.system;
using UsoComum;
using System.IO;

namespace Backtester.GeneticProgramming
{
    [DataContract]
 public  class GPSolutionProxy
    {
        [DataMember]
        public GPSolution solution { get; set; }
        [DataMember]
        public TradeSystem tradeSystem { get; set; }


        public void SaveToFile(string fileName)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(GPSolutionProxy));
                var fileStream = File.Create(fileName);
                ser.WriteObject(fileStream, this);
                fileStream.Close();
            }
            catch (System.Exception e)
            {
                Utils.Error("Erro ao salvar gpsolution: " + e.ToString());


            }

        }

        public static GPSolutionProxy LoadFromFile(string file)
        {
            try
            {
                var fileStream = File.Open(file, FileMode.Open);
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(GPSolutionProxy));
                GPSolutionProxy config = (GPSolutionProxy)ser.ReadObject(fileStream);
                fileStream.Close();
                return config;
            }
            catch (System.Exception e)
            {
                Utils.Error("Erro ao carregar solution: " + file + ": " + e.Message);
            }
            return null;
        }
    }
}
