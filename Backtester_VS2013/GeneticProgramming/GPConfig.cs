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
            poolSize = 100;
            elitismPercent = 5;
            mutationRatePerc = 3;
            maxLevels = 5;//Quantidade maxima que um nó pode ter
            minLevels = 2;

            spliceChancePerc = 60;//chance de misturar o código com outra solution
            this.maxSize = 30;//tamanhos maiores que esse serão rejeitados.
            unfitRemovalPercent = 85;//85% para cima serão removidos e substituidos por novos
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
        public int poolSize { get; set; }

        [DataMember]
        public int elitismPercent { get; set; }

        [DataMember]
        public float mutationRatePerc { get; set; }

        [DataMember]
        public int maxLevels { get; set; }
        [DataMember]
        public int minLevels { get; set; }
        #endregion




        public int maxSize { get; set; }

        public int spliceChancePerc { get; set; }

        public int unfitRemovalPercent { get; set; }
    }
}
