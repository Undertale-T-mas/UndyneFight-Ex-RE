using System.Net;
using System.Net.Sockets;
using System.Text;
using static UndyneFight_Ex.Server.UFConsole;

namespace UndyneFight_Ex.Server
{
    public class SocketReceiver
    {
        Socket listener;

        internal void DoCommand(Client? source, string? str)
        {
            if (string.IsNullOrEmpty(str)) return;

            string[] args = Command.Split(str);

            Command? runner = Command.GetCommand(args[0]);
            if (runner == null) {
                source.Reply($"E Running unknown command! ({str}, from client {source.UserName})");
                return; }
            WriteLine($"\0#Yellow][ @ ] \0#Green]{DateTime.Now}: \0#White]{(source == null ? "Unknown user" : source.UserName)} >> ran {(runner.Log ? $"command: {str}" : "hidden command.")}");

            try {
                runner.Processor(args[1..], source);
            }
            catch(Exception ex)
            {
                WriteLine($"\0#Red]An exception was thrown when running the command: {ex.Message}\n{ex}");
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
            string? ip;
            WriteLine($"Disconnected with {client.UserName}, ip: {ip = client.ConnectSocket.RemoteEndPoint?.Serialize().ToString()}");
            ipExists.Remove(ip);
        }
        List<Client> allClients = new();
        Dictionary<string, Client> ipExists = new();
        private void ReceiveClient()
        {
            Task.Run(() => {
                while (true)
                {
                    Client client;
                    Socket socket = listener.Accept();
                    string? ip = socket.RemoteEndPoint?.Serialize().ToString();
                    if (string.IsNullOrEmpty(ip)) continue; 
                    if (ipExists.ContainsKey(ip)) { 
                        ipExists[ip].ConnectSocket.Close();
                        ipExists[ip].ConnectSocket.Dispose();
                        ipExists[ip].ConnectSocket = socket;
                        continue;
                    }
                    socket.ReceiveTimeout = -1; socket.SendTimeout = 3000;
                    WriteLine($"\0#Green]{DateTime.Now}\0#White]: New user have logged in. IP: \0#Magenta]{socket.RemoteEndPoint?.Serialize().ToString()}");
                    client = new Client("unknown", socket);
                    Task.Run(() => ReceiveMessage(client));
                    allClients.Add(client);
                    ipExists.Add(ip, client);
                    allClients.ForEach(s => s.PendUpdate());
                }
            });
        }

        internal bool Start()
        {
            try {
                listener = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Any;
                IPEndPoint point = new(ip, 9982);
                listener.Bind(point);
                listener.Listen(200);

                ReceiveClient();

                WriteLine("\0#Green]UFProcessor started successfully"); return true;
            }
            catch  (Exception ex) 
            {
                WriteLine("\0#Red]Start failed. Exception:");
                WriteLine(ex.ToString());
                return false;
            }
        }
    } 
}