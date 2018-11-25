using System;
using System.Net;

namespace LockSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                LockDevice.DeviceId = args[0];
            }
            else
            {
                Console.WriteLine("usage dotnet LockSimulator.dll [LockDeviceId]\nexample: dotnet LockSimulator.dll VIkvffGFUoNd0P0B");
            }


            Console.WriteLine("Lock Simulator\n" +
                "This app simulates a lock\n" +
                "You can enter following commands for simulation\n" +
                "open : turn locked state on\n" +
                "close : turn unlocked state off\n" +
                "quit : quit application"
                );
            string currentCommand = "";
            TcpConnector tcpConnector = TcpConnector.GetInstance(IPAddress.Parse("127.0.0.1"), 12345);
            Console.WriteLine("Initializing server connection to 127.0.0.1:12345");
            tcpConnector.StartTcpConnection();
            Console.WriteLine("Connection made");
            TcpDataSendReceiver tcpDataSendReceiver = new TcpDataSendReceiver(tcpConnector);
            CommandExecuter commandExecuter = new CommandExecuter(tcpDataSendReceiver);

            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter a command:\t");
                    currentCommand = Console.ReadLine()?.ToLower();
                    commandExecuter.Execute(currentCommand);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An unexpected exception occured {e.Message}, \n{e}");
                }
            }
        }
    }
}
