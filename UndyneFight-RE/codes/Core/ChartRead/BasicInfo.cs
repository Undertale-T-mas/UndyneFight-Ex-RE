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