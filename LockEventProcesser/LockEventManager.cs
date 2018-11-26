using LockCommons.DB;
using LockCommons.Models;
using LockCommons.Models.Mq;
using LockCommons.Models.Proto;
using LockCommons.Mq;
using LockEventGateway;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LockEventProcesser
{

    public class LockEventManager
    {
        DeliveryEventHandler eventHandler;
        CheckDeviceActivityEventHandler checkDeviceActivityEventHandler;
        static MqBroker mqBroker = new MqBroker("event_que");
        static MqBroker activityCheckQue = new MqBroker("activity_que");

        ushort nmrOfConcurrentWorkers = 10;
        public LockEventManager()
        {
            eventHandler = new DeliveryEventHandler(activityCheckQue);
            checkDeviceActivityEventHandler = new CheckDeviceActivityEventHandler();
        }

        

        public void StartHandling()
        {
            mqBroker.InitializeConsumer(eventHandler, nmrOfConcurrentWorkers);

            activityCheckQue.InitializeConsumer(checkDeviceActivityEventHandler, nmrOfConcurrentWorkers);
        }
    }


}
