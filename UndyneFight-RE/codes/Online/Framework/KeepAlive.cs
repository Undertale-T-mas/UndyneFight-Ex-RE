using System;
using static UndyneFight_Ex.PlayerManager;

namespace UndyneFight_Ex.Remake.Network
{
    public class KeepAliver : GameObject
    {
        private static bool exist = false;
        public static bool IsAlive { get; set; } = false;    
        private KeepAliver() {
            exist = true;
            this.CrossScene = true;
            time = DateTime.Now;
        }
        public static void TryCreate()
        {
            if (!exist) GameStates.InstanceCreate(new KeepAliver());
        }
        DateTime time;
        public override void Update()
        { 
            TimeSpan secondSpan = new(DateTime.Now.Ticks - time.Ticks);
            if(secondSpan.TotalSeconds >= 60) // 1 min
            {
                CheckAlive(this);
                time = DateTime.Now;
            }
        }

        public static void CheckAlive(KeepAliver keepAliver = null, Action<bool> afterCheck = null)
        {
            bool type = false;
            UFSocket<Empty> socket = null;
            socket = new((s) =>
            {
                if (type)
                {
                    if(!s.Success)
                    {
                        IsAlive = false;
                        exist = false;
                        afterCheck?.Invoke(false);
                        keepAliver?.Dispose();
                        return;
                    }
                    if (s.Info[0..4] == "<RSA")
                    {
                        string newPassword = MathUtil.Encrypt(CurrentUser.PasswordMemory, s.Info);
                        socket.SendRequest("Log\\in\\" + currentPlayer + "\\" + newPassword);
                    }
                    else
                    {
                        IsAlive = true; 
                        afterCheck?.Invoke(true);
                    }
                }
                else {
                    if (!s.Success)
                    {
                        exist = false;
                        IsAlive = false;
                        keepAliver?.Dispose();
                        afterCheck?.Invoke(false);
                        return;
                    }

                    if (s.Info == "none") {
                        if (UserLogin && CurrentUser.OnlineAsync)
                        {
                            type = true;
                            socket.SendRequest($"Log\\key\\none");
                        }
                        else
                        {
                            IsAlive = true;
                            afterCheck?.Invoke(true);
                        }
                    }
                    else
                    {
                        IsAlive = true;
                        afterCheck?.Invoke(true);
                    }
                }
            });
            socket.SendRequest("keepAlive\\none");
        }
    }
}