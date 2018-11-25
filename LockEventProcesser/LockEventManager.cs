using LockCommons.DB;
using LockCommons.Models;
using LockCommons.Models.Proto;
using LockCommons.Mq;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LockEventProcesser
{

    public class LockEventManager
    {
        DeliveryEventHandler eventHandler;
        static MqBroker mqBroker = new MqBroker("event_que");

        ushort nmrOfConcurrentWorkers = 10;
        public LockEventManager()
        {
            eventHandler = new DeliveryEventHandler();
            eventHandler.LockEventProcessHandler += ProcessAEvent;
        }

        private async Task<bool> ProcessAEvent(LockEvent lockEvent)
        {
            try
            {
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

        public void StartHandling()
        {
            mqBroker.InitializeConsumer(eventHandler, nmrOfConcurrentWorkers);
        }
    }


}
