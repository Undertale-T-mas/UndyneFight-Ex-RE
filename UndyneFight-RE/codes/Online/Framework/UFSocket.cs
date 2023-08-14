using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.GameInterface;

namespace UndyneFight_Ex.Remake.Network
{
    public class UFSocket<T> where T : IMessageResult, new()
    {
        private static bool _isConnected = false;
        private static Socket _socketClient = null;
        private static IPAddress _ipAddress = null;
        private static Exception TryConnect()
        {
            Socket socketClient; IPAddress ipAddress;
            if (_isConnected)
            {
                if (_socketClient.Connected) return null;
                else
                {
                    socketClient = _socketClient;
                    ipAddress = _ipAddress;
                }
            }
            else
            {
                // try connect
                string url = UFEXSettings.MainServerURL;
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

                socketClient.Connect(new[] { ipAddress }, UFEXSettings.MainServerPort);
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

        Action<Message<T>> _onReceive;
        byte[] buffer = new byte[256];
        public UFSocket(Action<Message<T>> OnReceive) { this._onReceive = OnReceive; }
        public void SendRequest(string info)
        {
            Task.Run(() => {
                Exception ex = TryConnect();
                if (ex != null)
                {
                    var scene = (GameStates.CurrentScene as GameMenuScene);
                    if (scene != null) scene.InstanceCreate(new WarningShower("Cannot connect to server!"));
                    _onReceive.Invoke(new(false, ex.Message, 'D'));
                    return;
                }
                try
                {
                    byte[] infobyte = new byte[info.Length + 1];
                    Encoding.ASCII.GetBytes(info, 0, info.Length, infobyte, 0);
                    infobyte[info.Length] = 1;
                    _socketClient.Send(infobyte);

                    int len = _socketClient.Receive(buffer);
                    string state = Encoding.ASCII.GetString(buffer, 0, len);
                    if (state[0] == 'S')
                    {
                        string following = state[2..];
                        Message<T> u = new(true, following, 'S');
                        u.Data.Analysis(following);
                        _onReceive.Invoke(u);
                    }
                    else if (state[0] == 'F')
                    {
                        _onReceive.Invoke(new(false, state[2..], 'F'));
                    }
                    else if (state[0] == 'E')
                    {
                        _onReceive.Invoke(new(false, state[2..], 'E'));
                        throw new Exception(state[2..]);
                    }
                    return;
                }
                catch (Exception ex2)
                {
                    _onReceive.Invoke(new(false, ex2.Message, 'D'));
                    var scene = (GameStates.CurrentScene as GameMenuScene);
                    if (scene != null) scene.InstanceCreate(new WarningShower("Cannot connect to server!"));
                }
            });
        }
    }
}