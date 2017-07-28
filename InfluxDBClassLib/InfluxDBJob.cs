using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net.Http;
using System.Security.Authentication;
using System.Net;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
namespace InfluxDBClassLib
{


    public class InfluxDBJob
    {

        //string = each influxdb environment, value = list of teams and their respective database storages in given environment
        Dictionary<string, EnvData> influxStorage;

        List<EnvTeamDataStorage> flatInfluxStorage;

        InfluxDBClient client;

        public InfluxDBJob(InfluxDBClient _client)
        {

            client = _client;

        }

        public List<EnvTeamDataStorage> GetDataIntoList()
        {

            var influxdbShardList = client.GetInfluxdbList();

            InfluxdbShardParser influxParser = new InfluxdbShardParser();

            foreach (InfluxdbShardTeamData shard in influxdbShardList)
            {
                flatInfluxStorage.Add(influxParser.ParseShardDataIntoEnvTeamDataStorage(shard));
            }

            return flatInfluxStorage;
        }


        /*
        public Dictionary<string, EnvData> GetData()
        {
            var influxdbShardList = client.GetInfluxdbList();

            InfluxdbShardParser influxParser = new InfluxdbShardParser();

            foreach (InfluxdbShardTeamData shard in influxdbShardList)
            {
                influxStorage = influxParser.ParseShardDataIntoDict(influxStorage, shard);
            }


            return null;
        }
        */

    }

}
