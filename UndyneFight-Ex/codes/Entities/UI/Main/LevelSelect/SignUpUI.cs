using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources.Sounds;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.GlobalResources.Font;

namespace UndyneFight_Ex.Entities
{
    internal class SignUpUI : Entity
    {
        private readonly ChampionShipCard card;
        private readonly string title;

        public SignUpUI(ChampionShipCard csc)
        {
            title = csc.championShip.Title;
            Centre = csc.Centre;
            card = csc;
        }

        private bool correctlySigned = false;
        private int appearTime = 0;
        private string fileStates = "";

        public override void Draw()
        {
            card.Draw();
            if (appearTime % 60 <= 29 || appearTime >= 180)
            {
                NormalFont.CentreDraw("Press Z for signing up the championship.", new Vector2(320, 315), Color.Gold);
                NormalFont.CentreDraw("Press X for cancel.", new Vector2(320, 365), Color.Yellow);
            }
            NormalFont.CentreDraw("Sign Up", new Vector2(320, 45), Color.White);
            if (!string.IsNullOrEmpty(fileStates))
                NormalFont.CentreDraw(fileStates, new Vector2(320, 435), fileStates[0] == 'O' ? Color.Lime : Color.IndianRed);
        }

        public override void Update()
        {
            Centre = (Centre * 0.85f) + (new Vector2(320, 180) * 0.15f);
            card.Update();
            card.Centre = Centre;
            appearTime++;
            if (IsKeyPressed(InputIdentity.Confirm))
            {
                if (correctlySigned)
                {
                    Dispose();
                    InstanceCreate(new ChampionShipSelector());
                    return;
                }
                fileStates = SelectFile();
                if (fileStates[0] == 'O')
                {
                    PlayerManager.Save();
                    correctlySigned = true;
                    PlaySound(Ding);
                }
                else
                {
                    PlaySound(die1);
                }
            }
            if (IsKeyPressed(InputIdentity.Cancel))
            {
                Dispose();
                InstanceCreate(new ChampionShipSelector());
            }
        }

        public bool CheckFile(string s)
        {
            string format = @"div=\d" + "\n" + @"name=+.";
            return System.Text.RegularExpressions.Regex.IsMatch(s, format);
        }
        public string SelectFile()
        {
            try
            {
                string container = "Licences";
                string[] all = System.IO.Directory.GetFiles(container);
                if (all.Length == 0) return "No valid licences found!";
                foreach (string file in all)
                {
                    List<string> lines = IO.IOEvent.ByteToString(IO.IOEvent.ReadCustomFile(file));
                    if (lines.Count != 1) continue;
                    string res = lines[0];

                    string div = IO.IOProcess.Divider(res, ':')[1];
                    div = div.TrimEnd('\n');
                    PlayerManager.CurrentUser.SignUpChampionShip(title, div);
                    return "Registered!";
                }
                return "No correct file!";
            }
            catch
            {
                return "An error occurred!";
            }
        }
    }
}