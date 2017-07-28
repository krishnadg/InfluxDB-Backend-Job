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


    public class InfluxDBClient
    {

        string uri;
        string username;
        string password;


        //curl -G 'http://localhost:8086/query?pretty=true' --data-urlencode "db=mydb" --data-urlencode "q=SELECT \"value\" FROM \"cpu_load_short\" WHERE \"region\"='us-west'"
        static string REQUEST_QUERY = "/query?pretty=true&db=DataLens&epoch=ns&q=select+sum%28%22foo%22%29+from+%28select+max%28diskBytes%29+as+%22foo%22+from+influxdb_shard+where+time+%3E+now%28%29+-+1m+group+by+app_name%2C+%22database%22%2C+engine%2C+env%2C+env_class%2C+host%2C+id%2C+path%2C+retentionPolicy%2C+url%2C+walPath%29+group+by+%22database%22%2C+%22env%22";
        //static string SQL_QUERY = "select sum(\"foo\") from (select max(diskBytes) as \"foo\" from influxdb_shard where time > now() - 1m group by app_name, \"database\", engine, env, env_class, host, id, path, retentionPolicy, url, walPath) group by \"database\", \"env\"";
        //static string BASIC_QUERY = "SELECT value FROM cpu_load_short WHERE region='us-west'";

        HttpClientHandler clientHandler = new HttpClientHandler();
        HttpClient client;

        public InfluxDBClient(string _uri, string _username, string _password)
        {
            uri = _uri;
            uri = "http://metrics-data-admin.monitoring.nonprod.aws.cloud.nordstrom.net:8086";
            
            var builder = new UriBuilder(uri);
            builder.Port = -1;


            username = _username;
            password = _password;
           

            clientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
            //clientHandler.SslProtocols = SslProtocols.Tls12;
            clientHandler.ServerCertificateCustomValidationCallback = delegate {return true;}; //Needs to accomodate the CACERT AT SOME POINT!!! - shouldn't always return true
            clientHandler.Credentials = new NetworkCredential(username, password);


            client = new HttpClient(clientHandler);
        }

        public List<InfluxdbShardTeamData> GetInfluxdbList()
        {
            string json = GetJson();

            var rootObject = new RootObject();

            var listTeamData = new List<InfluxdbShardTeamData>();

            // var teamData = new InfluxdbShardTeamData();
            // teamData = JsonConvert.DeserializeObject<InfluxdbShardTeamData>(rawJson);

            rootObject = JsonConvert.DeserializeObject<RootObject>(json);

            listTeamData = rootObject.results.ElementAtOrDefault(0).series.ToList();

            return listTeamData;
        }


        public string GetJson()
        {
            var resp = CreateHttpRequestAndGetMessage();

            return resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();


        }


        private HttpResponseMessage CreateHttpRequestAndGetMessage()
        {

            Console.WriteLine("Creating Message");

            var message = new HttpRequestMessage();
            //message.Method = System.Net.Http.HttpMethod.;
            message.RequestUri = new Uri(uri + REQUEST_QUERY);
            

            var resp = client.SendAsync(message).GetAwaiter().GetResult();
            resp.EnsureSuccessStatusCode();

            return resp;
        }

    }

}

