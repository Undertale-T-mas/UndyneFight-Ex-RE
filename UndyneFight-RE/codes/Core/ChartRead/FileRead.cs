using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.Remake.Data;

namespace UndyneFight_Ex.Remake.ChartRead
{
    public partial class ChartInfo : DataBranch
    {
        public BasicInfo Basics { get; set; }
        public ChartInfo() : base("CHART_ROOT")
        {
            this.Children.Add(this.Basics = new BasicInfo());
        }
        public override void Load(SaveInfo info)
        {
            if (info == null) { info = new("CHART_ROOT{"); }
            if (info.Title == "StartInfo->") info = info.Nexts["CHART_ROOT"];

            base.Load(info);
        }
    }
}