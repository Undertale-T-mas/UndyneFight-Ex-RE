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
            info ??= new("CHART_ROOT{");
            if (info.Title == "StartInfo->") info = info.Nexts["CHART_ROOT"];

            base.Load(info);
        }
    }
}