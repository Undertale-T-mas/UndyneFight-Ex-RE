using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UndyneFight_Ex.Server
{ 
    public class Client
    {  
        public Client(string v, Socket socket)
        {
            this.UserName = v;
            this.ConnectSocket = socket;
        }

        public string UserName { get; set; }
        public Socket ConnectSocket { get; set; }

        public void Reply(string message)
        {
            Console.WriteLine("Reply >> " + message);
            byte[] b = Encoding.UTF8.GetBytes(message);
            ConnectSocket.Send(b);
        }
    }
}