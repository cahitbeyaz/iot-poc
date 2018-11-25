using LockCommons.Models.Proto;
using LockCommons.Mq;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LockCommons.Utilities;

namespace LockEventGateway.Network
{
    // State object for reading client data asynchronously  
    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 128 * 1024;
        public int bufferIdx = 0;
        public byte[] buffer = new byte[BufferSize];
    }

    public class AsynchronousSocketListener
    {
        // Thread signal.  
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public AsynchronousSocketListener()
        {
        }

        public static void StartListening()
        {
            // Establish the local endpoint for the socket.  
            // The DNS name of the computer  
            // running the listener is "host.contoso.com".  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Parse("0.0.0.0");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 12345);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(1024);

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("Waiting for a connection...on 0.0.0.0:12345");
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                // Signal the main thread to continue.  
                allDone.Set();

                // Get the socket that handles the client request.  
                Socket listener = (Socket)ar.AsyncState;
                Socket handler = listener.EndAccept(ar);
                StateObject state = new StateObject();

                Console.WriteLine($"Connection accepted {handler.RemoteEndPoint}");
                //maybe lack message framing
                //state.networkStream = new NetworkStream(state.workSocket);
                //LockEvent.Parser.ParseDelimitedFrom()

                state.workSocket = handler;
                handler.BeginReceive(state.buffer, state.bufferIdx, 4, 0, new AsyncCallback(ReadCallback), state);//read length first
            }
            catch (Exception e)
            {
                Console.WriteLine($"unexpected error client err AcceptCallback {e}");
            }
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            try
            {
                int bytesRead = handler.EndReceive(ar);
                if (bytesRead > 0)
                {
                    state.bufferIdx += bytesRead;
                    if (state.bufferIdx >= 4)//len of msg read
                    {
                        int lenOfMsg = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(state.buffer, 0));
                        if (state.bufferIdx >= lenOfMsg)//msg read fully
                        {
                            byte[] objectDataSlice = state.buffer.Slice(4, lenOfMsg);
                            //reset buffer
                            state.bufferIdx = 0;
                            Array.Clear(state.buffer, 0, StateObject.BufferSize);
                            Task.Run(() => EventMqBroker.Queue(handler.RemoteEndPoint.ToString(), objectDataSlice));
                            handler.BeginReceive(state.buffer, state.bufferIdx, 4, 0, new AsyncCallback(ReadCallback), state);//start reading again
                        }
                        else
                            handler.BeginReceive(state.buffer, state.bufferIdx, lenOfMsg + 4, 0, new AsyncCallback(ReadCallback), state);//read remaining of msg
                    }
                    else//read remaining len of msg
                        handler.BeginReceive(state.buffer, state.bufferIdx, 4 - state.bufferIdx, 0, new AsyncCallback(ReadCallback), state);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"unexpected error client {handler.RemoteEndPoint} err {e}");
                try
                {
                    handler?.Close();
                }
                catch (Exception)
                {//discard error
                }
            }
        }

        private static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
