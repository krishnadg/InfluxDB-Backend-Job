using System;
using System.Linq;
using System.Collections.Generic;
namespace InfluxDBClassLib
{

    public class RootObject
    {

        public ICollection<SubRootObject> results;


    }

     public class SubRootObject
    {

        public string statement_id {get;set;}



        //This is the thing we care about...
        public ICollection<InfluxdbShardTeamData> series;


    }


    public class InfluxdbShardTeamData
    {
        public string name {get; set;}

        public Tag tags {get; set;}

        public ICollection<string> columns {get;set;}
        public ICollection<ICollection<decimal>> values;



        public override string ToString()
        {
            return "name: " + name + " database: " + tags.database + " env: " + tags.env + " value: " + values.ToList().ElementAtOrDefault(0).ToList().ElementAtOrDefault(1);
        }

    }


    public partial class Tag
    {
        public string database{get; set;}

        public string env {get;set;}

    }

    public partial class Columns
    {
        public string time {get;set;}

        public string sum {get;set;}

    }

    public partial class Values
    {
        public ICollection<decimal> values;

    }

    


}





/*
{
                    "name": "influxdb_shard",
                    "tags": {
                        "database": "mri_audit",
                        "env": "prod-3"
                    },
                    "columns": [
                        "time",
                        "sum"
                    ],
                    "values": [
                        [
                            0,
                            3.13829e+07
                        ]
                    ]
                }
                */