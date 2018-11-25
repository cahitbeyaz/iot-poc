using LockCommons.Models.Proto;
using System;
using System.Collections.Generic;
using System.Text;
using Google.Protobuf;
using LockCommons.Utilities;
using System.Net;

namespace LockSimulator
{
    public class TcpDataSendReceiver
    {
        TcpConnector tcpConnector;
        public TcpDataSendReceiver(TcpConnector tcpConnector)
        {
            this.tcpConnector = tcpConnector;
        }

        public void SendEvent(LockEvent lockEvent)
        {
            int sizeOfMsg = lockEvent.CalculateSize();
            tcpConnector.Client.GetStream().Write(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(sizeOfMsg)));
            lockEvent.WriteTo(tcpConnector.Client.GetStream());
        }
    }
}
