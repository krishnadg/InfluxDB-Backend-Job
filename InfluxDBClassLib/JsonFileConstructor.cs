using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
namespace InfluxDBClassLib
{
    public class JsonFileConstructor
    {

        FinalJsonRoot rootObj;

        List<EnvTeamDataStorage> teamData = new List<EnvTeamDataStorage>();

        EnvTeamDataStorage envTeamStorage = new EnvTeamDataStorage();
        MetaData _meta;       

        //Could add in new constructor here to alter meta/teamdata field names
        public JsonFileConstructor()
        {
          

            _meta = new MetaData
            {
                storageType = "database_storage",
                period = "current_storage",
                unit = "bytes",
                name = "InfluxDB",
                dateEvaluated = DateTime.Now
            };

            rootObj = new FinalJsonRoot()
            {
                meta = _meta
            };
        }


        public FinalJsonRoot GetFinalJsonStructure(List<EnvTeamDataStorage> teamsStorage)
        {
            rootObj.results = teamsStorage.ToArray();

            return rootObj;

        }

        /*
        public FinalJsonRoot GetFinalJsonStructure2(Dictionary<string, EnvData> teamsStorage)
        {
            ConvertDictToJsonObject(teamsStorage);

            return rootObj;

        }
        //Sort dictionary and convert into array of TeamData for json root object's results field
        private void ConvertDictToJsonObject(Dictionary<string, EnvData> teamsStorage)
        {

            foreach (KeyValuePair<string, EnvData> teamAndStorage in teamsStorage)
            {
                foreach (KeyValuePair<string, long> teamAndStorageInEnv in teamAndStorage.Value.databaseStorage)
                {
                    
                }
            }

            //Add team data array/list to json root obj for serialization
            rootObj.results = teamData.ToArray();
            
        }

        //Determine the root team name if there is one, otherwise team and sub_team values will be the same
        private string DetermineRootTeamName(string fullBucketName)
        {
            string rootTeam = fullBucketName;
            if (fullBucketName.Contains("-"))
            {
                var index = fullBucketName.IndexOf("-");
                rootTeam = fullBucketName.Substring(0, index);
            }

            return rootTeam;
        }

        */
    }
}
