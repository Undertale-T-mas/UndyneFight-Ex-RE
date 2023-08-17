using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.GameInterface;
using UndyneFight_Ex.Remake.UI.DEBUG;

namespace UndyneFight_Ex.Remake.Network
{
    internal static class UFSocketData
    {
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

        public static bool sending = false;

    }
    public class UFSocket<T> where T : IMessageResult, new()
    {
        Action<Message<T>> _onReceive;
        byte[] buffer = new byte[1024 * 2];
        public UFSocket(Action<Message<T>> OnReceive) { this._onReceive = OnReceive; }

        public void SendRequest(string info)
        {
            PromptLine.Memories.Enqueue("Local >> " + info);
            Task.Run(() => {
                Exception ex = UFSocketData.TryConnect();
                if (ex != null)
                {
                    var scene = (GameStates.CurrentScene as GameMenuScene);
                    if (scene != null) scene.InstanceCreate(new WarningShower("Cannot connect to server!"));
                    _onReceive.Invoke(new(false, ex.Message, 'D'));
                    UFSocketData._isConnected = false;
                    return;
                }
                try
                {
                    byte[] infobyte = new byte[info.Length + 1];
                    Encoding.ASCII.GetBytes(info, 0, info.Length, infobyte, 0);
                    infobyte[info.Length] = 1;

                    while (UFSocketData.sending) {
                        Thread.Sleep(10);
                    }
                    UFSocketData.sending = true;
                    UFSocketData.Socket.Send(infobyte);
                    
                    int len = UFSocketData.Socket.Receive(buffer);
                    UFSocketData.sending = false;
                    string state = Encoding.ASCII.GetString(buffer, 0, len);

                    PromptLine.Memories.Enqueue("Server >> " + state);

                    if (state[0] == 'S')
                    {
                        string following = state[2..];
                        Message<T> u = new(true, following, 'S');
                        u.Data.Analysis(following);
                        _onReceive.Invoke(u);
                        KeepAliver.IsAlive = true;
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
                    UFSocketData.sending = false;
                    _onReceive.Invoke(new(false, ex2.Message, 'D'));
                    UFSocketData._isConnected = false;
                    var scene = (GameStates.CurrentScene as GameMenuScene);
                    if (scene != null) scene.InstanceCreate(new WarningShower("Cannot connect to server!")); 
                }
            });
        }
    }
}