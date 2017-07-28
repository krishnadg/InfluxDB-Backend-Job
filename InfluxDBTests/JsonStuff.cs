using System;
using System.Diagnostics;
using Xunit;
using Xunit.Sdk;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using InfluxDBClassLib;

namespace InfluxDBTests
{
    public class UnitTest1
    {
        [Fact]
        public void TestJson()
        {
            var rootDict = new Dictionary<string, EnvData>();

            var envData = new EnvData();

            var databaseStore = new Dictionary<string, long>();

            databaseStore.Add("Team1", 1000);
            databaseStore.Add("Team2", 2000);

            envData.databaseStorage = databaseStore;


            var envData2 = new EnvData();

            var databaseStore2 = new Dictionary<string, long>();

            databaseStore2.Add("Team3", 4000);
            databaseStore2.Add("Team4", 5000);

            envData2.databaseStorage = databaseStore;

            rootDict.Add("Total_influx_storage", envData);
            rootDict.Add("Second env", envData2);

            var json = JsonConvert.SerializeObject(rootDict);

            Assert.True(true, json);
        }
    }
}
