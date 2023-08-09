using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndyneFight_Ex.Server
{
    public class User
    {
        public int UUID { get; set; }
        public string? Name { get; set; }
        public string? PasswordHash { get; set; }
        public float Rating { get; set; }
    }
}
