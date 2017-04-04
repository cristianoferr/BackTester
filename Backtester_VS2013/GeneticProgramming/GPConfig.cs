using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace GeneticProgramming
{
    /*
     Função do Config: manter dados de configuração, usados principalmente na inicialização sem lógica de negócio, praticamente um DTO
     */
    [DataContract]
    public class GPConfig
    {

        #region Constructors
        public GPConfig()
        {
            poolQtd = 100;
            elitismPercent = 5;
            mutationRatePerc = 3;
        }

        #region IO
        public static GPConfig LoadSaved()
        {
            try
            {
                var fileStream = File.Open("GPConfig.js", FileMode.Open);
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(GPConfig));
                GPConfig config = (GPConfig)ser.ReadObject(fileStream);
                fileStream.Close();
                return config;
            }
            catch (System.Exception e)
            {

            }
            return new GPConfig();
        }
        #endregion
        #endregion
        #region Properties

        [DataMember]
        public int poolQtd { get; set; }

        [DataMember]
        public float elitismPercent { get; set; }

        [DataMember]
        public float mutationRatePerc { get; set; }
        #endregion

    }
}
