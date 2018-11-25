using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace LockCommons.DB
{
    public class MongoDriver
    {

        public static LockEventDbRepository MongoDbRepo = null;

        public static void ConfigureDriver()
        {
            // Set up MongoDB conventions
            var pack = new ConventionPack { new EnumRepresentationConvention(BsonType.String) };
            ConventionRegistry.Register("EnumStringConvention", pack, t => true);
            MongoDbRepo = new LockEventDbRepository("mongodb://localhost:27017");
        }
    }
}
