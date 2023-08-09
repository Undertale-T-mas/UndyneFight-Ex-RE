using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndyneFight_Ex.Server
{
    public class User
    {
        public long UUID { get; set; }
        public string? Name { get; set; }
        public string? PasswordHash { get; set; }
        public float Rating { get; set; } = 0;

        private DateTime _lastRefreshTime;
        public bool IsDead()
        {
            TimeSpan secondSpan = new TimeSpan(DateTime.Now.Ticks - _lastRefreshTime.Ticks);
            return secondSpan.TotalSeconds >= 500;
        }
        public void Refresh()
        {
            _lastRefreshTime = DateTime.Now;
        }
    }
}
