using LockCommons.DB;
using LockCommons.Models.Mq;
using LockCommons.Models.Proto;
using LockCommons.Mq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LockCommons.Models
{

    public class DeliveryEventHandler : IEventHandler
    {
        public event ProcessEventDelegate LockEventProcessHandler;
        MqBroker checkEventMqBroker;
        public DeliveryEventHandler(MqBroker checkEventMqBroker)
        {
            this.checkEventMqBroker = checkEventMqBroker;
            LockEventProcessHandler += ProcessEvent;
        }

        private async Task<bool> ProcessEvent(object lockEventpbj)
        {
            try
            {
                LockEvent lockEvent = (LockEvent)lockEventpbj;
                LockDeviceBson lockDeviceBson = new LockDeviceBson()
                {
                    IsActive = true,
                    LastActiveTime = lockEvent.EventTime.ToDateTime(),
                    LockDeviceId = lockEvent.LockDeviceId
                };

                await MongoDriver.MongoDbRepo.UpsertLockDeviceBson(lockDeviceBson);
                LockEventBson lockEventBson = new LockEventBson()
                {
                    DeviceEvent = lockEvent.DeviceEvent,
                    EventTime = lockEvent.EventTime.ToDateTime(),
                    LockDeviceBson = lockDeviceBson,
                    RequestReferenceNumber = lockEvent.RequestReferenceNumber
                };
                await MongoDriver.MongoDbRepo.InsertLockEventBson(lockEventBson);

                if (lockEvent.DeviceEvent == LockEvent.Types.DeviceEventEnum.Open)
                {
                    //publish a msg for another queu to set lock to passive if spesified amount time is passed since last event
                    LockActivityCheck lockActivityCheck = new LockActivityCheck();
                    lockActivityCheck.LockDeviceId = lockDeviceBson.LockDeviceId;
                    lockActivityCheck.EventDate = lockEventBson.EventTime;
                    string lockActivityCheckJson = JsonConvert.SerializeObject(lockActivityCheck);
                    byte[] binaryObject = Encoding.UTF8.GetBytes(lockActivityCheckJson);
                    checkEventMqBroker.DelayedQueu($"Activity check event {lockDeviceBson.LockDeviceId}", binaryObject, 30 * 1000);
                }



                Console.WriteLine($"Event processed to be processed {lockEvent}");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"err {e}");
            }
            return false;

        }

        public async Task<bool> ProcessEvent(byte[] data)
        {
            LockEvent lockEvent= LockEvent.Parser.ParseFrom(data);
            return await LockEventProcessHandler.Invoke(lockEvent);
        }
    }
}
