using System;

namespace UndyneFight_Ex.Remake.Network
{
    public class KeepAliver : GameObject
    {
        public KeepAliver() { 
            this.CrossScene = true;
            time = DateTime.Now;
        }
        DateTime time;
        public override void Update()
        { 
            TimeSpan secondSpan = new TimeSpan(DateTime.Now.Ticks - time.Ticks);
            if(secondSpan.TotalSeconds >= 60) // 5 min
            {
                UFSocket<Empty> socket = new((s) => {
                    ;
                });
                time = DateTime.Now;
                socket.SendRequest("keepAlive\\none");
            }
        }
    }
}