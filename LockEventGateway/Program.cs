using LockEventGateway.Network;
using System;

namespace LockEventGateway
{
    class Program
    {
        static void Main(string[] args)
        {
            AsynchronousSocketListener.StartListening();
        }
    }
}
