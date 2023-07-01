using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.UserService;

namespace UndyneFight_Ex.Remake.Data
{
    public partial class GlobalDataRoot : DataBranch
    {
        public UserMemory Memory { get; set; }
        public GlobalDataRoot() : base("UFEx_RE_ROOT")
        {
            this.Children.Add(this.Memory = new UserMemory());
        }

        public override void Load(SaveInfo info)
        {
            if(info == null) { info = new("UFEx_RE_ROOT{"); }
            if (info.Title == "StartInfo->") info = info.Nexts["UFEx_RE_ROOT"];

            if (!info.Nexts.ContainsKey("UserMemory")) info.PushNext(new("UserMemory{")); 
            this.Memory.Load(info.Nexts["UserMemory"]);
        }
    }
}