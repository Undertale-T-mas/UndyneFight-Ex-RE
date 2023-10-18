using UndyneFight_Ex.IO;

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
            info ??= new("UFEx_RE_ROOT{");
            if (info.Title == "StartInfo->") info = info.Nexts["UFEx_RE_ROOT"];
            base.Load(info);
        }
    }
}