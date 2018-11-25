using LockCommons.DB;
using LockCommons.Mq;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace LockEventProcesser
{
    class Program
    {
        static void Main(string[] args)
        {

            MongoDriver.ConfigureDriver();

            LockEventManager lockEventManager = new LockEventManager();
            lockEventManager.StartHandling();

            Console.WriteLine("LockEventProcesser LockEvent subscriber started");

            Console.ReadLine();
        }
    }
}
