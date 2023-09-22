
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.Collections.Generic;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.FightResources.Font;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Remake.UI
{
    internal class SmartInputer : TextInputer
    {
        private class InputShow : Entity
        {
            private int _length;
            CollideRect _originArea;
            float alpha = 0.0f;
            Vector2 delta = new(300, 0);

            public InputShow(CollideRect area, int length) { 
                this._originArea = this.collidingBox = area; 
                this._length = length;
                UpdateIn120 = true;
            }
            public override void Start()
            {
                base.Start();
                this._father = FatherObject as SmartInputer;
                this._father.ResultChanged += () => _secondaryTip = "";
            }
            SmartInputer _father;

            private bool _enabled = false;
            public bool Enabled => _enabled;

            public override void Draw()
            {
                this.Depth = 0.65f;
                this.FormalDraw(FightResources.Sprites.pixUnit, this.collidingBox.ToRectangle(), Color.DarkSlateGray * alpha);

                DrawingLab.DrawRectangle(this.collidingBox, Color.White * alpha, 3f, 0.7f);

                if(_secondaryEnabled)
                {
                    NormalFont.Draw(_secondaryTip, new Vector2(_father.RightPosition, _father.collidingBox.Up + 2), Color.Silver * 0.7f, _father.FontScale, 0.4f);
                }

                Vector2 pos = this.collidingBox.TopLeft + new Vector2(17, 7);
                if (string.IsNullOrEmpty(_father.Result)) return;
                for(int i = 0; i < Math.Min(_length, _father._tips.Length); i++)
                {
                    NormalFont.Draw(_father._tips[i], pos, Color.White * alpha, 1f, 0.9f);
                    NormalFont.Draw(MathF.Max(0, _father.similarities[i].Value).ToString("P1"), new Vector2(this.collidingBox.Right - 90, pos.Y), Color.Silver * alpha, 1f, 0.9f);
                    pos.Y += 40;
                }
            }
            private bool _secondaryEnabled;

            public override void Update()
            {
                this._enabled = _father.ModuleSelected;
                this._secondaryEnabled = false;

                if(_father._tips == null ||  _father._tips.Length == 0) { this._enabled = this._secondaryEnabled = false; return; }
                if (_enabled)
                {
                    if (string.IsNullOrEmpty(this._father.Result)) this._enabled = false; 
                    else if (_father.Result == _father._tips[0]) this._enabled = false;

                    if (this._enabled)
                    { 
                        this._secondaryEnabled = true;

                        if (_father._tips.Length >= 2 && this._father.similarities[1].Value >= 0.5f ||
                            (_father._tips[0].Length == _father.Result.Length && _father.similarities[0].Value > 0.8f))
                        {
                            this._secondaryEnabled = false;
                        }
                        else this._enabled = false;
                    }
                }
                if (this._secondaryEnabled)
                {
                    SecondaryUpdate();
                }

                if (this._enabled)
                {
                    this.alpha = MathHelper.Lerp(alpha, 1.0f, 0.07f);
                    this.delta.X = MathHelper.Lerp(this.delta.X, 0, 0.1f);
                }
                else
                {
                    this.alpha = MathHelper.Lerp(alpha, 0.0f, 0.06f);
                    if (delta.X < 300)
                        this.delta.X += (70 + this.delta.X) * 0.04f;
                }
                this.Centre = this._originArea.GetCentre() + delta;

            }

            private void SecondaryUpdate()
            {
                if (string.IsNullOrEmpty(_secondaryTip))
                {
                    string result = _father.Result;
                    string origin = _father._tips[0];
                    int len = result.Length;
                    if (result.Length > origin.Length)
                    {
                        return;
                    }
                    for (int i = 0; i < len; i++)
                    {
                        if (SimilarityChar(result[i], origin[i]) == 0)
                        {
                            this._secondaryEnabled = false;
                            this._enabled = true;
                            return;
                        }
                    }
                    this._secondaryTip = origin[result.Length..];

                    //test if the other canon
                    for (int j = 1; j < _father._tips.Length; j++)
                    {
                        if (_father.similarities[j].Value < 0.3f) return;
                        origin = _father._tips[j];
                        if (result.Length > origin.Length)
                        {
                            return;
                        }

                        bool flag = true;
                        for (int i = 0; i < len; i++)
                        {
                            if (SimilarityChar(result[i], origin[i]) == 0)
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (flag) this._secondaryTip += "/" + origin[result.Length..];
                    }
                }
                if (IsKeyPressed120f(InputIdentity.Tab))
                {
                    this._father.SetString(_father._tips[0]);
                }
            }

            string _secondaryTip;
        }

        private string[] _tips;

        private KeyValuePair<string, float>[] similarities;
        InputShow shower;

        public SmartInputer(string[] tips, ISelectChunk father, CollideRect area) : base(father, area)
        {
            this._tips = tips;
            this.ResultChanged += SortTip;
            // Vector2(571, 66)
            this.AddChild(shower = new InputShow(new CollideRect(662 - 571 + area.Left, 125 - 66 + area.Up, 266, 130), 3));
        }
        private void SortTip()
        {
            similarities = new KeyValuePair<string, float>[_tips.Length];
            for (int i = 0; i < similarities.Length; i++)
                similarities[i] = new(_tips[i], SimilarityString(_tips[i], Result));

            Array.Sort(similarities, (s, t) => {
                if (s.Value == t.Value) return 0; 
                else if (s.Value > t.Value) return -1; 
                return 1; 
            });

            for(int i = 0; i < similarities.Length; i++)
                _tips[i] = similarities[i].Key;
        }
       static  float SimilarityChar(char a, char b)
        {
            if (a == b) return 1f;
            if (a >= 'a' && a <= 'z') a = (char)(a - ('a' - 'A'));
            if (b >= 'a' && b <= 'z') b = (char)(b - ('a' - 'A'));
            if (a == b) return 0.5f;
            return 0;
        }
       static float SimilarityString(string origin, string str2)
        {
            if (origin == str2) return 1;
            int len1, len2, minLength, maxLength;
            len1 = origin.Length;
            len2 = str2.Length;
            minLength = Math.Min(len1, len2);
            maxLength = Math.Max(len1, len2);

            // The similarity of Head (0.3f)
            float total = 0;
            for(int i = 0; i < minLength; i++)
            {
                float v = SimilarityChar(origin[i], str2[i]);
                if (v == 0) break;
                total += v;
            }
            float result1 = total / (len2 > len1 ? maxLength : minLength);

            // The similarity of Distance (0.7f)
            float[,] dp = new float[len1 + 1, len2 + 1];
            for (int i = 1; i <= len1; i++) dp[i, 0] = i;
            for (int j = 1; j <= len2; j++) dp[0, j] = j;

            for (int i = 1; i <= len1; i++)
                for (int j = 1; j <= len2; j++)
                {
                    float flag = 1 - SimilarityChar(origin[i - 1], str2[j - 1]);
                    float temp1 = dp[i - 1, j] + 1;
                    temp1 = MathF.Min(temp1, dp[i, j - 1] + 1); 
                    temp1 = MathF.Min(temp1, dp[i - 1, j - 1] + flag);

                    dp[i, j] = temp1;
                }
            float result2 = (len1 - dp[len1, len2]) / len1;
            return result1 * 0.3f + result2 * 0.7f;
        }

        public override void Update()
        {
            base.Update();
            this.KeyLocked = this.shower.Enabled;
        }
    }
}