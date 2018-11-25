using LockCommons.Models.Proto;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using static LockCommons.Models.Proto.LockEvent.Types;

namespace LockCommons.Models
{
    public class LockEventBson
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("eventTime")]
        public DateTime EventTime { get; set; }
        [BsonElement("lockDevice")]
        public LockDeviceBson LockDeviceBson { get; set; }
        [BsonElement("requestReferenceNumber")]
        public string RequestReferenceNumber { get; set; }
        [BsonElement("deviceEvent")]
        public DeviceEventEnum DeviceEvent { get; set; }
    }
}
