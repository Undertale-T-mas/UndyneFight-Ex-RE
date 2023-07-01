using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.UserService;

namespace UndyneFight_Ex.Remake.Data
{
    public partial class GlobalDataRoot : DataBranch
    {
        public partial class UserMemory : DataBranch
        {
            public DatumString RememberUser { get; init; }
            public DatumBool AutoAuthentic { get; init; }

            public UserMemory() : base("UserMemory") {
                this.Children.Add(RememberUser = new("user"));
                this.Children.Add(AutoAuthentic = new("auto"));
            }
            
            public override void Load(SaveInfo info)
            { 
                if(!info.Nexts.ContainsKey("user")) { info.PushNext(new("user:null")); }
                if(!info.Nexts.ContainsKey("auto")) { info.PushNext(new("auto:false")); }

                RememberUser.Load(info.Nexts["user"]);
                AutoAuthentic.Load(info.Nexts["auto"]);
            }
        }
    }
}