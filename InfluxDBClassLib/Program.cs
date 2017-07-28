using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

 namespace InfluxDBClassLib {
    static class Program {
        
        //@Author Krishna Ganesan
        //Runs entire Elastic Search backend job, ending with the output data structure

        // Args in format []
        static void Main(string[] args) {

        InfluxDBClient client = new InfluxDBClient("", "", "");

        var respString = client.GetJson();

        Console.WriteLine("Response: " + respString);
        
        // string rawJson = "{\"name\": \"influxdb_shard\",\"tags\": {\"database\": \"wse\",\"env\": \"prod-7\"},\"columns\": [\"time\",\"sum\"],\"values\": [[0,1.61270024305e+11]] }";
        // string rawJson2 = "{ \"results\": [ { \"name\": \"influxdb_shard\",\"tags\": {\"database\": \"wse\",\"env\": \"prod-7\"},\"columns\": [\"time\",\"sum\"],\"values\": [[0,1.61270024305e+11]]  },   \"name\": \"influxdb_shard\",\"tags\": {\"database\": \"wse\",\"env\": \"prod-7\"},\"columns\": [\"time\",\"sum\"],\"values\": [[0,1.61270024305e+11]] } ] }  ";

/*
        var rootObject = new RootObject();

        var listTeamData = new List<InfluxdbShardTeamData>();

        // var teamData = new InfluxdbShardTeamData();
        // teamData = JsonConvert.DeserializeObject<InfluxdbShardTeamData>(rawJson);

        rootObject = JsonConvert.DeserializeObject<RootObject>(respString);

        //Console.WriteLine(teamData.ToString());
        Console.WriteLine(rootObject.results.ToList().ElementAtOrDefault(0).series.ElementAtOrDefault(0).ToString());//
  */    
        }
    }
 }

