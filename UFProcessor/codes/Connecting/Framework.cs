using System.Net;
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
            Console.WriteLine("@" + DateTime.Now + ": " + (source == null ? "Unknown user" : source.UserName) + " >> ran command:" + str);

            string[] args = str.Split('\\'); 
            
            Command runner = Command.GetCommand(args[0]);

            runner.Processor(args[1..], source);
        }

        private void ReceiveMessage(Client client)
        {
            byte[] buffer = new byte[1024];
            int index = -1;
            while (true) {
                int len = client.ConnectSocket.Receive(buffer);
                index += len;
                if (index < 0) continue;
                if (buffer[index] == 1)
                {
                    // Message end!
                    string str = Encoding.ASCII.GetString(buffer, 0, index + 1);
                    DoCommand(client, str);
                    index = -1;
                }
            }
        }
        private void ReceiveClient()
        {
            Task.Run(() => {
                while (true)
                {
                    Socket socket = listener.Accept();
                    socket.ReceiveTimeout = -1; socket.SendTimeout = 3000;
                    Console.WriteLine(DateTime.Now + ": " + "New user have logged in. IP: " + socket.RemoteEndPoint?.Serialize().ToString());
                    Task.Run(() => ReceiveMessage(new Client("unknown", socket)));
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