using LockCommons.LockApi;
using LockCommons.Models;
using LockCommons.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LockCommons.Models.Proto;
using Google.Protobuf.WellKnownTypes;

namespace LockSimulator
{
    public class CommandExecuter
    {

        TcpDataSendReceiver tcpDataSendReceiver;

        private CommandExecuter()
        {

        }

        public CommandExecuter(TcpDataSendReceiver tcpDataSendReceiver)
        {
            this.tcpDataSendReceiver = tcpDataSendReceiver;
        }

        public void Execute(string cmd)
        {
            switch (cmd.ToLower().Trim())
            {
                case "open":
                case "close":
                    SendEvent(cmd);
                    break;

                case "quit":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine($"invalid command {cmd}");
                    break;
            }
        }

        private void SendEvent(string cmd)
        {
            LockEvent lockEvent = new LockEvent();

            lockEvent.EventTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc).ToTimestamp();
            lockEvent.DeviceEvent = ModelUtils.StringEvent2Enum(cmd);
            lockEvent.LockDeviceId = LockDevice.DeviceId;
            lockEvent.RequestReferenceNumber = Utils.GetRandomRequestReferenceNumber();
            tcpDataSendReceiver.SendEvent(lockEvent);
        }

    }
}
