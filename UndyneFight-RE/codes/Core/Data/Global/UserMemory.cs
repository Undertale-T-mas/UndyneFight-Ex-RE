namespace UndyneFight_Ex.Remake.Data
{
    public partial class GlobalDataRoot : DataBranch
    {
        public partial class UserMemory : DataBranch
        {
            public DatumString RememberUser { get; init; }
            public DatumString PasswordMem { get; init; }
            public DatumBool AutoAuthentic { get; init; }

            public UserMemory() : base("UserMemory") {
                Children.Add(RememberUser = new("user") { InitialText = ":null" });
                Children.Add(PasswordMem = new("pc") { InitialText = ":null" });
                Children.Add(AutoAuthentic = new("auto") { InitialText = ":false" });
            } 
        }
    }
}