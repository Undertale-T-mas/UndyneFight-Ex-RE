using System;

namespace UndyneFight_Ex.Remake.Network
{
    public class KeepAliver : GameObject
    {
        public static bool IsAlive { get; set; } = false;    
        public KeepAliver() { 
            this.CrossScene = true;
            time = DateTime.Now;
            IsAlive = true;
        }
        DateTime time;
        public override void Update()
        { 
            TimeSpan secondSpan = new TimeSpan(DateTime.Now.Ticks - time.Ticks);
            if(secondSpan.TotalSeconds >= 60) // 1 min
            {
                CheckAlive(this);
                time = DateTime.Now;
            }
        }

        public static void CheckAlive(KeepAliver keepAliver = null, Action<bool> afterCheck = null)
        {
            UFSocket<Empty> socket = new((s) =>
            {
                if (!s.Success)
                {
                    IsAlive = false;
                    keepAliver?.Dispose();
                }
                afterCheck?.Invoke(s.Success);
            });
            socket.SendRequest("keepAlive\\none");
        }
    }
}