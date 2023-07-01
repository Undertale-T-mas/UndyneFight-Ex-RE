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

                if (true || _user.Skill > 60)
                {
                    if (_user.Skill > 90)
                        this.FormalDraw(GlobalResources.Sprites.starMedal, new Vector2(156, 444), Color.White, 0, ImageCentre);
                    else
                        this.FormalDraw(Image, new Vector2(156, 444), Color.White, 0, ImageCentre);
                }
                if (true || _user.Skill > 60)
                {
                    if (_user.Skill > 90)
                        this.FormalDraw(GlobalResources.Sprites.starMedal, new Vector2(181, 444), Color.White, 0, ImageCentre);
                    else
                        this.FormalDraw(Image, new Vector2(181, 444), Color.White, 0, ImageCentre);
                }
                if (true || _user.Skill > 60)
                {
                    if (_user.Skill > 90)
                        this.FormalDraw(GlobalResources.Sprites.starMedal, new Vector2(206, 444), Color.White, 0, ImageCentre);
                    else
                        this.FormalDraw(Image, new Vector2(206, 444), Color.White, 0, ImageCentre);
                }
                FightFont.CentreDraw(_infoA, new Vector2(30, 480), Color.Aqua * _infoAlpha);
                FightFont.CentreDraw("-", new Vector2(52, 480), Color.Aqua * _infoAlpha);
                FightFont.CentreDraw(_infoB, new Vector2(74, 480), Color.Aqua * _infoAlpha);
                FightFont.CentreDraw("-", new Vector2(96, 480), Color.Aqua * _infoAlpha);
                FightFont.CentreDraw(_infoC, new Vector2(118, 480), Color.Aqua * _infoAlpha);
            }

            private void DrawLine(Vector2 start, Vector2 end, Color color, float size = 3f)
            {
                DrawingLab.DrawLine(start, end, size, color, 0.45f);
            }
            Color _skillColor;
            float _infoAlpha = 1.0f;
            string _drawingInfo;
            string _infoA = "", _infoB = "", _infoC = "";
            public override void Update()
            {
                float skill = _user.Skill;
                Color[] SkillColors = { Color.Lime, Color.LawnGreen, Color.Blue,
                Color.MediumPurple, Color.Red, Color.OrangeRed, Color.Orange, Color.Gold};
                if (skill >= 20)
                    for (int i = 2; i < 9; i++)
                    {
                        if (skill >= i * 10)
                            _skillColor = SkillColors[i - 2];
                    }
                this._drawingDifficulty = _virtualFather.CurrentDifficulty;
                Difficulty dif2 = _virtualFather.DiffSelect.FocusDifficulty;
                if (dif2 != _drawingDifficulty)
                {
                    this._drawingDifficulty = dif2;
                    this._infoAlpha = 0.6f;
                }
                else this._infoAlpha = 1.0f;

                string res;
                if (_virtualFather.SongSelected != null)
                {
                    var attribute = _virtualFather.SongSelected.Attributes;
                    string requireName = _virtualFather.SongSelected.FightName;
                    if (_user.SongPlayed(requireName))
                    {
                        SongData data = _user.GetSongData(requireName);
                        if (data.CurrentSongStates.ContainsKey(_drawingDifficulty))
                        {
                            SongData.SongState _state = data.CurrentSongStates[_drawingDifficulty];
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