using System.Net;
using System.Net.Sockets;
using System.Text;
using UndyneFight_Ex.UserService;

namespace UndyneFight_Ex.Server
{ 
    public class Client
    {  
        public Client(string v, Socket socket)
        {
            this.UserName = v;
            this.ConnectSocket = socket;
        }

        public string? RSABuffer { get; set; }
        public string UserName { get; set; }
        public Socket ConnectSocket { get; set; }
        public User? BindUser { get; set; }
        public bool Disposed { get; private set; }

        public void Update()
        {
            if (updatePending)
            {
                updatePending = false;
            }
            else return;
            if (BindUser == null) return;
            if (BindUser.IsDead())
            {
                this.Dispose();
            }
        }

        private void Dispose()
        {
            if (BindUser == null) return;
            UserLibrary.UserExit(BindUser);
            Console.WriteLine(DateTime.Now + ": " + "user " + UserName + " is killed because of no response for too long. IP: " + ConnectSocket.RemoteEndPoint?.Serialize().ToString());
            ConnectSocket.Dispose();
            this.BindUser.Save();
            this.Disposed = true;
        }

        public void Reply(string message)
        {
            UFConsole.WriteLine("Reply >> " + message);
            byte[] b = Encoding.UTF8.GetBytes(message);
            ConnectSocket.Send(b);

            BindUser?.Refresh();
        }

        private bool updatePending = false;
        internal void PendUpdate()
        {
            updatePending = true;
        }
    }
}