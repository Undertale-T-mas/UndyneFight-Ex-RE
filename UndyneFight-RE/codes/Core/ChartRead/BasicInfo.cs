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
        public partial class BasicInfo : DataBranch
        {
            DatumString Charter { get; set; }
            DatumString MusicComposer { get; set; }
            DatumString Attributes { get; set; }
            DatumString Paint { get; set; }
            public BasicInfo() : base("BasicInfo")
            {
                Children.Add(Charter = new("") { InitialText = ":Unknown" });
                Children.Add(MusicComposer = new("") { InitialText = ":Unknown" });
                Children.Add(Attributes = new("") { InitialText = ":Unknown" });
                Children.Add(Paint = new("") { InitialText = ":Unknown" });
            }
        }
    }
}