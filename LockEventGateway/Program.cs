using LockCommons.Mq;
using LockEventGateway.Network;
using System;
using System.Threading.Tasks;

namespace LockEventGateway
{
    public delegate void ProcessMsg(string logData,byte[] data);
    class Program
    {
        static MqBroker mqBroker = new MqBroker("event_que");
        static void Main(string[] args)
        {
            AsynchronousSocketListener.ProcessMsgEvent += ProcessMsgs;
            AsynchronousSocketListener.StartListening();
        }

        private static void ProcessMsgs(string logData,byte[] data)
        {
            Task.Run(() => mqBroker.Queue(logData, data));
        }
    }
}
