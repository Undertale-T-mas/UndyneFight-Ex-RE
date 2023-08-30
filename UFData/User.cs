using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UndyneFight_Ex.Server
{
    public class AliveData
    {
        public AliveData(int timeAliveSeconds = 600) { this.timeAliveSeconds = timeAliveSeconds; }
        private int timeAliveSeconds;
        private DateTime _lastRefreshTime = DateTime.Now;
        public bool IsDead()
        {
            TimeSpan secondSpan = new(DateTime.Now.Ticks - _lastRefreshTime.Ticks);
            return secondSpan.TotalSeconds >= timeAliveSeconds;
        }
        public void Refresh()
        {
            _lastRefreshTime = DateTime.Now;
        }
    }

    public class User : AliveData
    {
        public Dictionary<string, SongResult>? SongRecord { get; set; }
        public long UUID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? PasswordHash { get; set; }
        public float Rating { get; set; } = 0;

        public void Save()
        {
            FileStream stream = new("Data/User/" + Name, FileMode.OpenOrCreate, FileAccess.Write);
            byte[] bytes = Encoding.ASCII.GetBytes(JsonSerializer.Serialize(this));
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
            stream.Close();
        }
    }
}
