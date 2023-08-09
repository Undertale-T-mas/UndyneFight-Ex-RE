﻿using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UndyneFight_Ex.Server
{
    public class SocketReceiver
    {
        Socket listener;

        internal void DoCommand(Client? source, string? str)
        {
            if (string.IsNullOrEmpty(str)) return;

            string[] args = str.Split('\\');

            Command runner = Command.GetCommand(args[0]);
            if (runner.Log)
                Console.WriteLine("[ @ ]" + DateTime.Now + ": " + (source == null ? "Unknown user" : source.UserName) + " >> ran command:" + str);
            else Console.WriteLine("[ @ ]" + DateTime.Now + ": " + (source == null ? "Unknown user" : source.UserName) + " >> ran hidden command.");

            try {
                runner.Processor(args[1..], source);
            }
            catch(Exception ex)
            {
                Console.WriteLine("An exception was thrown when running the command: " + ex.Message);
            }
        }

        private void ReceiveMessage(Client client)
        {
            byte[] buffer = new byte[1024];
            int index = -1;
            while (true) {
                client.Update();
                if (client.Disposed) break;
                int len = client.ConnectSocket.Receive(buffer);
                index += len;
                if (index < 0) continue;
                if (buffer[index] == 1)
                {
                    // Message end!
                    string str = Encoding.ASCII.GetString(buffer, 0, index);
                    DoCommand(client, str);
                    index = -1;
                }
            }
        }
        List<Client> allClients = new();
        private void ReceiveClient()
        {
            Task.Run(() => {
                while (true)
                {
                    Client client;
                    Socket socket = listener.Accept();
                    socket.ReceiveTimeout = -1; socket.SendTimeout = 3000;
                    Console.WriteLine(DateTime.Now + ": " + "New user have logged in. IP: " + socket.RemoteEndPoint?.Serialize().ToString());
                    client = new Client("unknown", socket);
                    Task.Run(() => ReceiveMessage(client));
                    allClients.Add(client);
                    allClients.ForEach(s => s.PendUpdate());
                }
            });
        }

        internal bool Start()
        {
            try {
                listener = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Any;
                IPEndPoint point = new IPEndPoint(ip, 9982);
                listener.Bind(point);
                listener.Listen(200);

                ReceiveClient(); 

                Console.WriteLine("UFProcessor started successfully"); return true;
            }
            catch  (Exception ex) 
            {
                Console.WriteLine("Start failed. Exception:" );
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    } 
}