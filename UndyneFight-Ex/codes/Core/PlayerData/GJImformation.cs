using GameJolt.Objects;
using System.Collections.Generic;
using UndyneFight_Ex.IO;

namespace UndyneFight_Ex.UserService
{
    public class GJImformation : ISaveLoad
    {
        public List<ISaveLoad> Children => null;

        public Credentials Credential { get; internal set; }

        public string GameJoltID { get; set; }
        public string Token { get; set; }
        public bool Authed { get; set; } = false;

        public void Load(SaveInfo info)
        {
            if (!info.Nexts.ContainsKey("ID")) info.PushNext(new("ID:0"));
            if (!info.Nexts.ContainsKey("Token")) info.PushNext(new("Token:null"));
            GameJoltID = info.GetDirectory("ID").StringValue;
            Token = info.GetDirectory("Token").StringValue;
            if (!string.IsNullOrEmpty(GameJoltID)) LoadUser();
        }
        private void LoadUser()
        {

        }
        public void InitializeData()
        {

        }

        public SaveInfo Save()
        {
            SaveInfo res = new SaveInfo("GameJolt{");
            res.PushNext(new("ID:" + GameJoltID));
            res.PushNext(new("Token:" + Token));

            return res;
        }
    }
}