using System;
using UndyneFight_Ex.SongSystem;

using vec2 = Microsoft.Xna.Framework.Vector2;
using rect = UndyneFight_Ex.CollideRect;
using col = Microsoft.Xna.Framework.Color;
using VPCT = Microsoft.Xna.Framework.Graphics.VertexPositionColorTexture;
using VPC = Microsoft.Xna.Framework.Graphics.VertexPositionColor;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        private partial class Extra
        {
            private class Info : Entity
            {
                public Info()
                {

                }
                Extra _efa;
                VirtualFather _vfa;
                
                public override void Start()
                {
                    _efa = (this.FatherObject as Extra);
                    _vfa = (this.FatherObject as Extra)._vfa;
                    base.Start();
                }
                public override void Draw()
                {
                    if (_vfa.CurrentDifficulty == Difficulty.NotSelected) return;
                    if (this.Centre.Y > 677) return;
                    GLFont font = FightResources.Font.NormalFont;
                    float y = this.Centre.Y + 30;
                    var father = _vfa.SongSelected;
                    string FinalText = father.Attributes.DisplayName;
                    font.Draw(FinalText == "" ? father.FightName : father.Attributes.DisplayName, new vec2(184, y + 22), col.White * alpha, 1.21f, 0.6f);
                    string s = _vfa.CurrentDifficulty.ToString();
                    font.Draw(s, new vec2(752 - font.SFX.MeasureString(s).X, y + 22), _vfa.CurrentDifficulty switch
                    {
                        Difficulty.Noob => col.White,
                        Difficulty.Easy => col.Lime,
                        Difficulty.Normal => col.LightBlue,
                        Difficulty.Hard => col.MediumPurple,
                        Difficulty.Extreme => col.Gold,
                        Difficulty.ExtremePlus => col.Silver,
                        _ => throw new Exception()
                    } * alpha, 1.21f, 0.62f);
                    
                    DrawingLab.DrawLine(new(172, y + 66), new(960 - 172, y + 66), 3.0f, col.Silver * alpha, 0.61f);
                    DrawingLab.DrawLine(new(172, y + 116), new(960 - 172, y + 116), 3.0f, col.Silver * alpha, 0.61f);
                }
                float alpha;
                public override void Update()
                {
                    this.alpha = _efa.alpha;
                    float y = _efa.pos.Y + 122;
                    _efa.score.ResetPosition(new(256, y));
                    _efa.basicInfo.ResetPosition(new(460, y));
                    _efa.friend.ResetPosition(new(680, y));
                }
            }
        }
    }
}