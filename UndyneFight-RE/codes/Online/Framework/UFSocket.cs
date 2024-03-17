using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Remake.UI.DEBUG;
using static UndyneFight_Ex.GameInterface.UFEXSettings;
using static UndyneFight_Ex.Remake.Network.UFSocketData;

namespace UndyneFight_Ex.Remake.Network
{
    internal static class UFSocketData
    {
        [DllImport("winInet.dll")]
        private static extern bool InternetGetConnectedState(
            ref int dwFlag,
            int dwReserved
        );
        public static bool IsAbleConnect()
        {
            int s = 0;
            return InternetGetConnectedState(ref s, 0);
        }

        public static bool _isConnected = false;
        private static Socket _socketClient = null;
        public static Socket Socket => _socketClient;
        private static IPAddress _ipAddress = null;
        public static Exception TryConnect()
        {
            Socket socketClient; IPAddress ipAddress;
            if (_isConnected)
            {
                if (_socketClient.Connected) return null;
                else
                {
                    ipAddress = _ipAddress;
                    socketClient = KeepAliver.IsAlive ? _socketClient : new(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                }
            }
            else
            {
                // try connect
                string url = MainServerURL;
                try
                {
                    IPHostEntry ipHostInfo = Dns.GetHostEntry(url);
                    ipAddress = ipHostInfo.AddressList[0];
                    socketClient = new(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                }
                catch (Exception ex) { return ex; }
            }
            try
            {
                socketClient.ReceiveTimeout = 3000;
                socketClient.SendTimeout = 3000;

                socketClient.Connect(new[] { ipAddress }, MainServerPort);
                _isConnected = true;
                _socketClient = socketClient;
                _ipAddress = ipAddress;
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static bool sending = false;

    }
    public class UFSocket<T> where T : IMessageResult, new()
    {
        Action<Message<T>> _onReceive;
        byte[] buffer = new byte[1024 * 2];
        public UFSocket(Action<Message<T>> OnReceive) { this._onReceive = OnReceive; }

        private static string _last;
        private static DateTime _lastTime = DateTime.Now;

        public void SendRequest(string info)
        {
            DateTime time = DateTime.Now;
            if (info == _last && time.Ticks - _lastTime.Ticks < 2000) {
                return;
            }
            _last = info;
            _lastTime = time;
            PromptLine.Memories.Enqueue("Local >> " + info);
            Task.Run(() => {
                Exception ex = TryConnect();
                if (ex != null)
                {
                    ex = TryConnect();
                    if (ex != null)
                    {
                        bool t = IsAbleConnect();
                        string info = "Cannot connect to server!";
                        if (!t) info = "Please check your connect!";
                        var scene = GameStates.CurrentScene as GameMenuScene;
                        scene?.InstanceCreate(new WarningShower(info));
                        _onReceive.Invoke(new(DateTime.Now.Ticks - time.Ticks, false, ex.Message, 'D'));
                        _isConnected = false;
                        return;
                    }
                }
                byte[] infobyte = new byte[info.Length + 1];
                Encoding.ASCII.GetBytes(info, 0, info.Length, infobyte, 0);
                infobyte[info.Length] = 1;

                try
                {
                    while (sending)
                    {
                        Thread.Sleep(10);
                    }
                    sending = true;
                    UFSocketData.Socket.Send(infobyte);

                    int len = UFSocketData.Socket.Receive(buffer);
                    sending = false;
                    string state = Encoding.ASCII.GetString(buffer, 0, len);

                    PromptLine.Memories.Enqueue("Server >> " + state);

                    if (state[0] == 'S')
                    {
                        string following = state[2..];
                        Message<T> u = new(DateTime.Now.Ticks - time.Ticks, true, following, 'S');
                        try
                        {
                            u.Data.Analysis(following);
                            _onReceive.Invoke(u);
                        }
                        finally
                        {
                            KeepAliver.IsAlive = true; 
                        }
                    }
                    else if (state[0] is 'F' or 'E')
                    {
                        _onReceive.Invoke(new(DateTime.Now.Ticks - time.Ticks, false, state[2..], state[0]));
                        if (state[0] == 'E') return;
                    }
                    return;
                }
                catch
                {
                    sending = false;
                    ex = TryConnect();
                    if (ex != null)
                    {
                        var scene2 = GameStates.CurrentScene as GameMenuScene;
                        scene2?.InstanceCreate(new WarningShower("Cannot connect to server!"));
                        _onReceive.Invoke(new(DateTime.Now.Ticks - time.Ticks, false, ex.Message, 'D'));
                        _isConnected = false;
                        return;
                    }
                    try
                    {
                        while (sending)
                        {
                            Thread.Sleep(10);
                        }
                        sending = true;
                        UFSocketData.Socket.Send(infobyte);

                        int len = UFSocketData.Socket.Receive(buffer);
                        sending = false;
                        string state = Encoding.ASCII.GetString(buffer, 0, len);

                        PromptLine.Memories.Enqueue("Server >> " + state);

                        if (state[0] == 'S')
                        {
                            string following = state[2..];
                            Message<T> u = new(DateTime.Now.Ticks - time.Ticks, true, following, 'S');
                            u.Data.Analysis(following);
                            _onReceive.Invoke(u);
                            KeepAliver.IsAlive = true;
                        }
                        else if (state[0] is 'F' or 'E')
                        {
                            _onReceive.Invoke(new(DateTime.Now.Ticks - time.Ticks, false, state[2..], state[0]));
                            if (state[0] == 'E') return;
                        }
                        return;
                    }
                    catch (Exception ex3)
                    {
                        sending = false;
                        _onReceive.Invoke(new(DateTime.Now.Ticks - time.Ticks, false, ex3.Message, 'D'));
                        _isConnected = false;
                        var scene = GameStates.CurrentScene as GameMenuScene;
                        scene?.InstanceCreate(new WarningShower("Cannot connect to server!"));
                    }
                }
            });
        }
    }
}