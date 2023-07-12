using Microsoft.Xna.Framework;
using System.Threading.Tasks;

namespace UndyneFight_Ex.UserService
{
    public partial class User
    {
        private class InfoText : Entity
        {
            static InfoText last;
            public InfoText(string text, Color color)
            {
                if (last != null) last.Dispose();
                last = this;
                this.text = text;
                this.color = color;
                Centre = new(320, 288);
            }

            string text; Color color;

            public override void Draw()
            {
                GlobalResources.Font.NormalFont.CentreDraw(text, Centre, color * alpha, 0.7f, 0.5f);
            }
            float alpha = 1; float time = 0;

            public override void Update()
            {
                time += 0.5f;
                if (time > 30f) alpha -= 0.02f;
                if (alpha < 0) Dispose();
            }
        }
        public void Upload()
        {
            if (!GameJoltInformation.Authed) { return; }
            GameStates.InstanceCreate(new InfoText("Please wait", Color.White));
            Task<GameJolt.Response> task1 = UploadRating();
            Task<GameJolt.Response>[] all = { task1 };
            Task.Run(() =>
            {
                Task.WaitAll(all);
                bool[] result = new bool[all.Length];
                bool temp = true;
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = all[i].Result.Success;
                    if (!result[i]) temp = false;
                }

                if (temp)
                {
                    //create an info entity to show successfully uploaded data.
                    GameStates.InstanceCreate(new InfoText("Success!", Color.Lime));
                }
                else
                {
                    //the data is not succssfully uploaded. also create an info entity.
                    GameStates.InstanceCreate(new InfoText("Failed", Color.Red));
                }
            });
        }
        private async Task<GameJolt.Response> UploadRating()
        {
            var GJ = GameStates.GameJolt;
            return await GJ.Scores.AddAsync(GameJoltInformation.Credential, (int)(Skill * 1.0 * 1000000), MathUtil.FloatToString(Skill, 4), "", 716130);
        }
    }
}