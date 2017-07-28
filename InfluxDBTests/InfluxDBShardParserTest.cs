using System;
using System.Diagnostics;
using Xunit;
using Xunit.Sdk;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using InfluxDBClassLib;

namespace InfluxDBTests
{

    public abstract class IndexDataParserTestBase : IDisposable    
    {   
        
        protected List<EnvTeamDataStorage> expectedTeamDataList;

        protected InfluxdbShardParser sut;
        protected IndexDataParserTestBase()
        {
            // Do "global" initialization here; Called before every test method.
            expectedTeamDataList = new List<EnvTeamDataStorage>();
        }

        public void Dispose()
        {
            // Do "global" teardown here; Called after every test method.

        }
    } 


    public class IndexRecordParserTest : IndexDataParserTestBase 
    {
        /*Test Helper method */
        public void AreSameLists(List<EnvTeamDataStorage> expected, List<EnvTeamDataStorage> result)
        {  
            var countMatches = expected.Count == result.Count;
            Assert.True(countMatches, String.Format("expected count {0}, got count {1}", expected.Count, result.Count ));

            for (int i =0 ; i < expected.Count; i++)
            {
                var envData = expected.ElementAt(i);
                var resEnvData = result.Find(x => x.team == envData.team);

                bool sameName = envData.team == resEnvData.team;
                Assert.True(sameName, "Didn't have same name");


                bool sameEnv = envData.env == resEnvData.env;
                Assert.True(sameEnv, "Didn't have same env");

                bool sameStorage = envData.value == resEnvData.value;
                Assert.True(sameStorage, "Didn't have same storage- expected storage " + envData.value +" got storage " + resEnvData.value);

            }
            
        }

        [Fact]
        public void ParseSingleShardIntoList_EmptyList_Return1TeamData()
        {
            sut = new InfluxdbShardParser();

            List<EnvTeamDataStorage> result = new List<EnvTeamDataStorage>();


            var listoflist = new List<ICollection<decimal>>();
            var listInList = new List<decimal>();
            decimal time = 0;
            decimal d = Decimal.Parse("3.4444973985e+11", System.Globalization.NumberStyles.Float);
        
            listInList.Add(time);
            listInList.Add(d);
            listoflist.Add(listInList);
           
            InfluxdbShardTeamData arg = new InfluxdbShardTeamData
            {
                tags = new Tag
                {
                    database = "Team1",
                    env = "Prod-1"
                    
                },
                values = listoflist
                
            };

            expectedTeamDataList = new List<EnvTeamDataStorage>
            {
               new EnvTeamDataStorage
               {
                   team = "Team1",
                   env = "Prod-1",
                   value = Decimal.ToInt64(d)
               }  
            };

            //ACT

            result.Add(sut.ParseShardDataIntoEnvTeamDataStorage(arg));


            //ASSERT
            //Assert.Equal(expectedTeamDataList, result);
            AreSameLists(expectedTeamDataList, result);


        }


         [Fact]
        public void ParseSingleShardIntoList_NonEmptyList_Return3TeamData()
        {
            //ARRANGE
            sut = new InfluxdbShardParser();

            List<EnvTeamDataStorage> result = new List<EnvTeamDataStorage>();

            /*Add preexisting entries */
            result.Add(new EnvTeamDataStorage{
                team = "Team2",
                env = "Prod-7",
                value = 23145
            });
            result.Add(new EnvTeamDataStorage{
                team = "Team3",
                env = "Prod-8",
                value = 4999999
            });



            var listoflist = new List<ICollection<decimal>>();
            var listInList = new List<decimal>();
            decimal time = 0;
            decimal d = Decimal.Parse("8.4444973985e+11", System.Globalization.NumberStyles.Float);
            listInList.Add(time);
            listInList.Add(d);
            listoflist.Add(listInList);
           
            InfluxdbShardTeamData arg = new InfluxdbShardTeamData
            {
                tags = new Tag
                {
                    database = "Team56",
                    env = "Prod-1"
                    
                },
                values = listoflist
                
            };

            expectedTeamDataList = new List<EnvTeamDataStorage>
            {
               new EnvTeamDataStorage
               {
                   team = "Team56",
                   env = "Prod-1",
                   value = Decimal.ToInt64(d)
               } ,
               new EnvTeamDataStorage
               {
                   team = "Team2",
                   env = "Prod-7",
                   value = 23145
               },
               new EnvTeamDataStorage
               {
                   team = "Team3",
                   env = "Prod-8",
                   value = 4999999
               }
             
            };

            //ACT

            result.Add(sut.ParseShardDataIntoEnvTeamDataStorage(arg));


            //ASSERT
            //Assert.Equal(expectedTeamDataList, result);
            AreSameLists(expectedTeamDataList, result);


        }



    }

}


