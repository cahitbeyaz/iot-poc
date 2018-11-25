using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace LockCommons.Models
{
    
    public class LockDeviceBson
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }

        [BsonElement("lockDeviceId")]
        public string LockDeviceId { get; set; }

        [BsonElement("isActive")]
        public bool IsActive { get; set; }

        [BsonElement("lastActiveTime")]
        public DateTime LastActiveTime { get; set; }

    }
}
