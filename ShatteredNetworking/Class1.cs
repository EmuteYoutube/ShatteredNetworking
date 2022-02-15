using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ShatteredNetworking
{
    public class Globals
    {
        public static readonly int Port = 25665;
        public static readonly string Host = "127.0.0.1";
    }
    public class Server
    {
        private TcpListener server;
        private List<TcpClient> clients = new List<TcpClient>();
        public void Host(int port, string address)
        {
            IPAddress addr = IPAddress.Parse(address);
            server = new TcpListener(addr, port);
            server.Start();
            while (true)
            {
                Console.WriteLine($@"Waiting for connection ...");
                TcpClient client = server.AcceptTcpClient();
                clients.Add(client);
                var stream = client.GetStream();
                while (true)
                {
                    Console.WriteLine($@"Waiting for message");
                    byte[] buffer = new byte[255];
                    var data = stream.Read(buffer, 0, buffer.Length);
                    var str = System.Text.Encoding.ASCII.GetString(buffer, 0, data);
                    Console.WriteLine($"R:{str}");
                    buffer = System.Text.Encoding.ASCII.GetBytes($@"C:{str}");
                    Console.WriteLine($"S:C:{str}");

                    stream.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
    public class Client
    {
        private TcpClient client;
        private byte[] buffer;
        private NetworkStream stream;

        public void Connect(int port, string address)
        {
            try
            {
                Console.WriteLine("Connecting to server");
                client = new TcpClient(address, port);
                buffer = new byte[4096];
                stream = client.GetStream();
                Console.WriteLine("Connected to server");
            }
            catch(SocketException e)
            {
                Console.WriteLine($@"Could not connect to server");
                throw;
            }
            catch(Exception e)
            {
                Console.WriteLine($"Unkown Error:{e.ToString()}");
                throw;

            }

        }
        public void SendMessage(string message)
        {
            try
            {
                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                int half = data.Length / 2;
                stream.Write(data, 0, data.Length);
                //Thread.Sleep(500);
                //stream.Write(data, half, data.Length-half);

                Console.WriteLine($"S:{message}");

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error sending message:{e.ToString()}");
                throw;
            }
        }
        public void WaitForMessage()
        {
         
                Console.WriteLine($@"Waiting for message");
                byte[] buffer = new byte[255];
                var data = stream.Read(buffer, 0, buffer.Length);
                var str = System.Text.Encoding.ASCII.GetString(buffer, 0, data);
                Console.WriteLine($"R:{str}");
                return;
            
        }
    }
}
