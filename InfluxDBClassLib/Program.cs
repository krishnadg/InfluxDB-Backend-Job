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


        Console.WriteLine("Collected Args: " + args.Length);

        string uri = args[0];

        string username = args[1];

        string password = args[2];


        InfluxDBClient client = new InfluxDBClient("", "admin", "admin");

        InfluxDBJob jobDoer = new InfluxDBJob(client);
        List<EnvTeamDataStorage> dataList = jobDoer.GetDataIntoList();

        jobDoer.PrintData();

        S3Client.AddJsonFileToS3(dataList);
    
        }
    }
 }

