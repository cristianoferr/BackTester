using Backtester.backend.model.system;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Backtester.backend.model.system.estatistica;
using System.Runtime.CompilerServices;

namespace Backtester.backend.DataManager
{
    [DataContract]
    public class CandidatoData
    {
        [DataMember]
        public TradeSystem tradeSystem;
        [DataMember]
        public Estatistica estatistica;
        [DataMember]
        public float finalScore;

        public CandidatoData(TradeSystem ts, Estatistica stat, float capitalFinal)
        {
            this.tradeSystem = ts;
            this.estatistica = stat;
            this.finalScore = capitalFinal;
        }
    }

    [DataContract]
    public class CandidatoManager
    {

        [DataMember]
        public IList<CandidatoData> ranking;

        static CandidatoManager singleton = null;

        public CandidatoManager()
        {
            ranking = new List<CandidatoData>();
        }

        public static CandidatoManager LoadSaved()
        {
            if (singleton != null) return singleton;
            try
            {
                var fileStream = File.Open("saved-candidatos.js", FileMode.Open);
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(CandidatoManager));
                singleton = (CandidatoManager)ser.ReadObject(fileStream);
                fileStream.Close();
                singleton.ranking = singleton.ranking.ToList();

                return singleton;
            }
            catch (System.Exception e)
            {

            }
            singleton=new CandidatoManager();
            return singleton;
        }

        public void SaveToFile()
        {
            //objeto de teste
            if (this != singleton) return;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(CandidatoManager));
            var fileStream = File.Create("saved-candidatos.js");
            ser.WriteObject(fileStream, this);
            fileStream.Close();

        }

        public int AddTradeSystem(TradeSystem ts,Estatistica stat)
        {
            if (!RankingContains(ts))
            {
                ranking.Add(new CandidatoData(ts,stat,stat.capitalFinal));
            }
            SortRanking();
            SaveToFile();
            return GetRanking(ts);
        }

        private bool RankingContains(TradeSystem ts)
        {
            return ranking.FirstOrDefault(x => x.tradeSystem == ts) != null;
        }

        public int GetRanking(TradeSystem ts)
        {
            for (int i = 0; i < ranking.Count; i++)
            {
                if (ranking[i].tradeSystem == ts) return i;
            }
            return ranking.Count;
        }

        public void SortRanking()
        {
            ranking = ranking.OrderByDescending(x => x.finalScore).ToList();
            while (ranking.Count > Consts.MAX_CANDIDATOS)
            {
                ranking.RemoveAt(ranking.Count() - 1);
            }
        }
    }
}
