using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources.Sounds;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Entities
{

    internal partial class AccountManager
    {
        private partial class GamejoltManager
        {
            private class GamejoltModifyUI : Selector
            {
                public GamejoltModifyUI()
                {
                    AutoDispose = false; bool a = false;
                    SelectChanged += () =>
                    {
                        if (a)
                            PlaySound(changeSelection, 0.9f);
                    };
                    a = true;
                    Selected += () =>
                    {
                        if (currentSelect > 2)
                            PlaySound(select);
                    };
                    SelectChanger += () =>
                    {
                        if (CharInput != 1) return;
                        if (IsKeyPressed120f(InputIdentity.MainUp))
                        {
                            currentSelect--;
                        }
                        else if (IsKeyPressed120f(InputIdentity.MainDown))
                        {
                            currentSelect++;
                        }
                        currentSelect = MathUtil.Posmod(currentSelect, SelectionCount);
                    };
                    PushSelection(new Async());
                    PushSelection(new UploadSongs());
                }
                public override void Draw()
                {
                    GlobalResources.Font.NormalFont.CentreDraw("Gamejolt", new Vector2(320, 45), Color.White);
                }
                public override void Update()
                {
                    base.Update();
                }
                private class UploadSongs : TextSelection
                {
                    public UploadSongs() : base("Sync songs and scores", new Vector2(320, 210)) { }
                    public override void SelectionEvent()
                    {
                        var user = PlayerManager.CurrentUser;
                        user.Upload();
                        base.SelectionEvent();
                    }
                }
                private class Async : TextSelection
                {
                    public Async() : base("Sync achievements", new Vector2(320, 150)) { }

                    Task s;
                    public override void SelectionEvent()
                    {
                        if (s != null && !s.IsCompleted) return;

                        var user = PlayerManager.CurrentUser;

                        //async achievements;
                        Task a = Task.Run(() =>
                        {
                            var ach = user._achievement;
                            foreach (var v in ach.AchievementObjects.Values)
                            {
                                var achUnit = v.TargetAchievement;
                                if (achUnit.Achieved && !achUnit.OnlineAchieved)
                                {
                                    achUnit.OnlineAsync();
                                }
                            }
                        });
                        s = a;
                    }
                }
            }
        }
    }
}