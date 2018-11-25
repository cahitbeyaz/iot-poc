/*
 * Author: Nikola Živković
 * Blog: rubikscode.net
 * Company: Vega IT Sourcing
 * Year: 2017
 */

using LockCommons.Models;
using LockCommons.Models.Api;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace LockCommons.DB
{
    /// <summary>
    /// Class used to access Mongo DB.
    /// </summary>
    public class LockEventDbRepository
    {
        private IMongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<LockEventBson> _LockEventBsonsCollection;
        private IMongoCollection<LockDeviceBson> _LockDeviceBsonsCollection;


        public LockEventDbRepository(string connectionString)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("LockEventDb");
            _LockEventBsonsCollection = _database.GetCollection<LockEventBson>("LockEvents");
            _LockDeviceBsonsCollection = _database.GetCollection<LockDeviceBson>("LockDevices");
        }

        /// <summary>
        /// Checking is connection to the database established.
        /// </summary>
        public bool CheckConnection()
        {
            try
            {
                _database.ListCollections();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returning all data from 'LockEventBsons' collection.
        /// </summary>
        public IEnumerable<LockEventBson> GetAllLockEventBsons()
        {
            return _LockEventBsonsCollection.Find(new BsonDocument()).ToEnumerable();
        }

        /// <summary>
        /// Returning all LockEventBsons with the defined value of defined field.
        /// </summary>
        public async Task<List<LockEventBson>> GetLockEventBsonsByField(string fieldName, string fieldValue)
        {
            var filter = Builders<LockEventBson>.Filter.Eq(fieldName, fieldValue);
            var result = await _LockEventBsonsCollection.Find(filter).ToListAsync();

            return result;
        }

        /// <summary>
        /// Returning defined number of LockEventBsons.
        /// </summary>
        public async Task<List<LockEventBson>> GetLockEventBsons(int startingFrom, int count)
        {
            var result = await _LockEventBsonsCollection.Find(new BsonDocument()).Skip(startingFrom).Limit(count).ToListAsync();

            return result;
        }

        /// <summary>
        /// Inserting passed LockEventBson into the database.
        /// </summary>
        public async Task InsertLockEventBson(LockEventBson LockEventBson)
        {
            await _LockEventBsonsCollection.InsertOneAsync(LockEventBson);
        }


        /// <summary>
        /// Returning all LockEventBsons with the defined value of defined field.
        /// </summary>
        public async Task<List<LockDeviceBson>> GetLockDeviceBsonsByField(string fieldName, string fieldValue)
        {
            var filter = Builders<LockDeviceBson>.Filter.Eq(fieldName, fieldValue);
            var result = await _LockDeviceBsonsCollection.Find(filter).ToListAsync();

            return result;
        }


        /// <summary>
        /// Inserting passed LockDeviceBson into the database.
        /// </summary>
        public async Task UpsertLockDeviceBson(LockDeviceBson lockDevice)
        {
            var result = _LockDeviceBsonsCollection.ReplaceOne(
                filter: new BsonDocument("lockDeviceId", lockDevice.LockDeviceId),
                options: new UpdateOptions { IsUpsert = true, },
                replacement: lockDevice);

        }


        /// <summary>
        /// Updating LockEventBson.
        /// </summary>
        /// <param name="_id">LockEventBson id.</param>
        /// <param name="udateFieldName">Field that should be updated.</param>
        /// <param name="updateFieldValue">New value for the field.</param>
        /// <returns>
        /// True - If LockEventBson is updated.
        /// False - If LockEventBson is not updated.
        /// </returns>
        public async Task UpdateLockDeviceBson(string lockDeviceId, string udateFieldName, string updateFieldValue)
        {
            var filter = Builders<LockDeviceBson>.Filter.Where(a => a.LockDeviceId == lockDeviceId);
            var update = Builders<LockDeviceBson>.Update.Set(udateFieldName, updateFieldValue);

            var result = await _LockDeviceBsonsCollection.UpdateOneAsync(filter, update);

            if (result.ModifiedCount == 0)
            {
                throw new Exception($"lock device status could not be update {lockDeviceId}");
            }

        }

        /// <summary>
        /// Creates index on Name field.
        /// </summary>
        public async Task CreateIndexOnDeviceIdField()
        {
            var keys = Builders<LockEventBson>.IndexKeys.Ascending(x => x.Id);
            await _LockEventBsonsCollection.Indexes.CreateOneAsync(keys);
        }

        /// <summary>
        /// Creates index on defined field.
        /// </summary>
        public async Task CreateIndexOnCollection(IMongoCollection<BsonDocument> collection, string field)
        {
            var keys = Builders<BsonDocument>.IndexKeys.Ascending(field);
            await collection.Indexes.CreateOneAsync(keys);
        }

        public List<LockDeviceEventReportModel> GetLockDevicesEvents()
        {
            var lockDevicesSorted = _LockDeviceBsonsCollection.AsQueryable().ToList();
            var lockDeviceEvents = _LockEventBsonsCollection.AsQueryable().ToList();
            List<LockDeviceEventReportModel> lockDeviceEventReportModel = new List<LockDeviceEventReportModel>();

            foreach (var device in lockDevicesSorted)
            {
                LockDeviceEventReportModel deviceWithEvents = new LockDeviceEventReportModel();
                deviceWithEvents.LockDevice = new ApiLockDevice()
                {
                    LockDeviceId = device.LockDeviceId,
                    LastActiveTime = device.LastActiveTime,
                    IsActive = device.IsActive
                };
                deviceWithEvents.LockDeviceEvents = lockDeviceEvents.Where(e => e.LockDeviceBson.LockDeviceId == device.LockDeviceId).Select(p => new ApiDeviceEventModel() { DeviceEvent = p.DeviceEvent.ToString(), EventTime = p.EventTime, RequestReferenceNumber = p.RequestReferenceNumber });
                lockDeviceEventReportModel.Add(deviceWithEvents);
            }
            return lockDeviceEventReportModel;

        }

    }
}
