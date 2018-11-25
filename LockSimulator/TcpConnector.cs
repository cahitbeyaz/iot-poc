using Google.Protobuf;
using LockCommons.Models;
using LockCommons.Models.Proto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LockSimulator
{
    public class TcpConnector
    {

        public TcpClient Client;
        static TcpConnector tcpConnectorInstance;
        IPAddress ipAddress;
        int port;
        IPEndPoint ipEndPoint;
        private TcpConnector()
        {

        }

        private TcpConnector(IPAddress ipAddress, int port)
        {
            Client = new TcpClient();
            this.ipAddress = ipAddress;
            this.port = port;
        }

        public static TcpConnector GetInstance(IPAddress ipAddress, int port)
        {
            if (tcpConnectorInstance == null)
                tcpConnectorInstance = new TcpConnector(ipAddress, port);
            return tcpConnectorInstance;
        }


        public void StartTcpConnection()
        {
            if (Client != null && Client.Connected)
            {
                Console.WriteLine("client already initalized discarding call");
                return;
            }

            ipEndPoint = new IPEndPoint(this.ipAddress, this.port);
            Client.Connect(ipEndPoint);
        }

    }
}
