using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluxDBClassLib
{

    public class InfluxdbShardParser
    {

        public InfluxdbShardParser()
        {

        }


        public EnvTeamDataStorage ParseShardDataIntoEnvTeamDataStorage(InfluxdbShardTeamData data)
        {
            // Console.WriteLine("Storage index 0: " + Decimal.ToInt64(data.values.ElementAtOrDefault(0).ElementAtOrDefault(0)));
            // Console.WriteLine("Storage index 1: " + Decimal.ToInt64(data.values.ElementAtOrDefault(0).ElementAtOrDefault(1)));

            var envTeamData  = new EnvTeamDataStorage
            {
                team = data.tags.database,
                env = data.tags.env,
                value = Decimal.ToInt64(data.values.ElementAtOrDefault(0).ElementAtOrDefault(1))//This is the location of the storage field
            };

            return envTeamData;
        }


        //This method may be unnecessary due to how the client side will be able to group by env anyways...
        public Dictionary<string, EnvData> ParseShardDataIntoDict(Dictionary<string, EnvData> envDataDict, InfluxdbShardTeamData data)
        {
            //prod7, prod8, etc..
            var envName = data.tags.env;

            if (envDataDict.ContainsKey(envName))
            {
                var envDataToUpdate = envDataDict[envName];

                envDataToUpdate.databaseStorage.Add(data.tags.database, Decimal.ToInt64(data.values.ElementAtOrDefault(0).ElementAtOrDefault(1)));

                envDataDict[envName] = envDataToUpdate;
            }
            else
            {
                var newEnvData = new EnvData
                {
                    databaseStorage = new Dictionary<string, long>
                    {
                        {data.tags.database, Decimal.ToInt64( data.values.ElementAtOrDefault(0).ElementAtOrDefault(1))}

                    }
                    
                };
                envDataDict.Add(envName, newEnvData);

            }

            return envDataDict;
        }

    }

}