using LockCommons.DB;
using LockCommons.Models;
using LockCommons.Models.Proto;
using LockCommons.Mq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LockEventProcesser
{

    public class LockEventManager
    {
        DeliveryEventHandler eventHandler;
        ushort nmrOfConcurrentWorkers = 10;
        public LockEventManager()
        {
            eventHandler = new DeliveryEventHandler();
            eventHandler.LockEventProcessHandler += ProcessAEvent;
        }

        private async Task ProcessAEvent(LockEvent lockEvent)
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
                LockDeviceBson = new LockDeviceBson() { LockDeviceId = lockDeviceBson.LockDeviceId },
                RequestReferenceNumber = lockEvent.RequestReferenceNumber
            };

            await MongoDriver.MongoDbRepo.InsertLockEventBson(lockEventBson);
            Console.WriteLine($"Event processed to be processed {lockEvent}");

        }

        public void StartHandling()
        {
            EventMqBroker.InitializeConsumer(eventHandler, nmrOfConcurrentWorkers);
        }
    }


}
