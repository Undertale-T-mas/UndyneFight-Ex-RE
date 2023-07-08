using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.SongSystem;
using UndyneFight_Ex.UserService;

using static UndyneFight_Ex.FightResources.Font;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        private class DataShow : Entity
        {
            VirtualFather _virtualFather;
            User _user;
            public override void Start()
            {
                _virtualFather = this.FatherObject as VirtualFather;
                GetColor();
            }
            public DataShow(User user)
            {
                _user = user;
            }
            Difficulty _drawingDifficulty;
            public override void Draw()
            {
                NormalFont.CentreDraw(_user.PlayerName, new(121, 387), Color.White, 1.2f, 0.1f);
                //_skillColor
                DrawLine(new Vector2(60, 410), new(182, 410), _skillColor);
                NormalFont.Draw(MathUtil.FloatToString(_user.Skill, 2), new(37, 419), Color.Silver, 1.2f, 0.1f);

                DrawLine(new Vector2(30, 460), new(133, 460), Color.Silver);
                DrawLine(new Vector2(120, 460), new(140, 480), Color.Silver);
                DrawLine(new Vector2(133, 460), new(140, 450), Color.Silver);
                DrawLine(new Vector2(223, 480), new(140, 480), Color.Silver);

                this.Image = GlobalResources.Sprites.medal;
                this.Depth = 0.24f;

                if ( _user.Skill > 60)
                {
                    if (_user.Skill > 90)
                        this.FormalDraw(GlobalResources.Sprites.starMedal, new Vector2(157, 444), Color.White, 0, ImageCentre);
                    else
                        this.FormalDraw(Image, new Vector2(157, 444), Color.White, 0, ImageCentre);
                }
                if ( _user.Skill > 60)
                {
                    if (_user.Skill > 90)
                        this.FormalDraw(GlobalResources.Sprites.starMedal, new Vector2(182, 444), Color.White, 0, ImageCentre);
                    else
                        this.FormalDraw(Image, new Vector2(182, 444), Color.White, 0, ImageCentre);
                }
                if ( _user.Skill > 60)
                {
                    if (_user.Skill > 90)
                        this.FormalDraw(GlobalResources.Sprites.starMedal, new Vector2(207, 444), Color.White, 0, ImageCentre);
                    else
                        this.FormalDraw(Image, new Vector2(207, 444), Color.White, 0, ImageCentre);
                }
                FightFont.CentreDraw(_infoA, new Vector2(30, 480), Color.Aqua * _infoAlpha);
                FightFont.CentreDraw("-", new Vector2(52, 480), Color.Aqua * _infoAlpha);
                FightFont.CentreDraw(_infoB, new Vector2(74, 480), Color.Aqua * _infoAlpha);
                FightFont.CentreDraw("-", new Vector2(96, 480), Color.Aqua * _infoAlpha);
                FightFont.CentreDraw(_infoC, new Vector2(118, 480), Color.Aqua * _infoAlpha);

                if (_drawingInfo == "NO DATA")
                {
                    NormalFont.CentreDraw("NO DATA", new Vector2(130, 534), Color.Red, 1.6f, MathHelper.ToRadians(15), 0.5f);
                }
                else
                {
                    FightFont.CentreDraw("score", new(56, 507), Color.White * _infoAlpha, 1.0f, 0.2f);
                    NormalFont.CentreDraw(_drawingInfo, new(159, 503), Color.White * _infoAlpha, 1.2f, 0.2f);
                    if (_accuracy > 0.75f)
                        NormalFont.CentreDraw(_accuracy.ToString("P2"), new(168, 564), _markColor * _infoAlpha, 1.05f, 0.2f);
                    else
                        NormalFont.CentreDraw("Failed", new(168, 564), _markColor * _infoAlpha, 1.05f, 0.2f);

                    if (this._AP)
                    {
                        FightFont.CentreDraw("ALL PERFECT", new(126, 537), Color.Gold * _infoAlpha, 1.00f, 0.2f);
                    }
                    else if(this._NoHit)
                    {
                        FightFont.CentreDraw("NO HIT", new(123, 537), Color.Orange * _infoAlpha, 1.05f, 0.2f);
                    }
                }
            }

            private Color MarkColor(SkillMark mark)
            {
                return mark switch
                {
                    SkillMark.Impeccable => Color.Goldenrod,
                    SkillMark.Eminent => Color.OrangeRed,
                    SkillMark.Excellent => Color.MediumPurple,
                    SkillMark.Respectable => Color.LightSkyBlue,
                    SkillMark.Acceptable => Color.SpringGreen,
                    SkillMark.Ordinary => Color.Green,
                    SkillMark.Failed => Color.DarkRed,
                    _ => Color.White
                };
            }

            private void DrawLine(Vector2 start, Vector2 end, Color color, float size = 3f)
            {
                DrawingLab.DrawLine(start, end, size, color, 0.45f);
            }
            Color _skillColor;
            float _infoAlpha = 1.0f;
            string _drawingInfo = "";
            string _infoA = "", _infoB = "", _infoC = "";
            SkillMark _skillMark;
            Color _markColor;
            private bool _NoHit, _AP;
            private float _accuracy;

            private void GetColor()
            {
                float skill = _user.Skill;
                _skillColor = Color.Green;
                Color[] SkillColors = { Color.Lime, Color.LawnGreen, Color.Blue,
                Color.MediumPurple, Color.Red, Color.OrangeRed, Color.Orange, Color.Gold};
                if (skill >= 20)
                    for (int i = 2; i < 9; i++)
                    {
                        if (skill >= i * 10)
                            _skillColor = SkillColors[i - 2];
                    }
            }

            public override void Update()
            {
                this._drawingDifficulty = _virtualFather.CurrentDifficulty;
                Difficulty dif2 = _virtualFather.DiffSelect.FocusDifficulty;
                if (dif2 != _drawingDifficulty && _virtualFather.DiffSelect.CurrentFocus.IsMouseOn)
                {
                    this._drawingDifficulty = dif2;
                    this._infoAlpha = 0.6f;
                }
                else this._infoAlpha = 1.0f;

                string res;
                _drawingInfo = "NO DATA";
                if (_virtualFather.SongSelected != null)
                {
                    var attribute = _virtualFather.SongSelected.Attributes;
                    string requireName = _virtualFather.SongSelected.FightName;
                    if (_user.SongPlayed(requireName))
                    {
                        SongData data = _user.GetSongData(requireName);
                        if (data.CurrentSongStates.ContainsKey(_drawingDifficulty))
                        {
                            SongData.SongState state = data.CurrentSongStates[_drawingDifficulty];
                            this._skillMark = state.Mark;
                            this._NoHit = state.AC;
                            this._AP = state.AP;
                            this._accuracy = state.Accuracy;
                            this._drawingInfo = state.Score.ToString();
                            this._markColor = MarkColor(_skillMark);
                        }
                    }
                    string infoA, infoB, infoC;
                    if (attribute != null)
                    {
                        string GetFull(float dif)
                        {
                            string s = ((int)dif).ToString(); 
                            return s;
                        }
                        infoA = attribute.CompleteDifficulty.ContainsKey(_drawingDifficulty) ?
                            GetFull(attribute.CompleteDifficulty[_drawingDifficulty]) : "?";
                        infoB = attribute.ComplexDifficulty.ContainsKey(_drawingDifficulty) ?
                            GetFull(attribute.ComplexDifficulty[_drawingDifficulty]) : "?";
                        infoC = attribute.APDifficulty.ContainsKey(_drawingDifficulty) ?
                            GetFull(attribute.APDifficulty[_drawingDifficulty]) : "?";
                        _infoA = infoA; _infoB = infoB; _infoC = infoC;
                    }
                    else _infoA = _infoB = _infoC = "?";
                }
                else _infoA = _infoB = _infoC = "?"; 
            }

        }
    }
}