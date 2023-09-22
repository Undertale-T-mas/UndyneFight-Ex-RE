using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using static UndyneFight_Ex.MathUtil;
using static UndyneFight_Ex.GameMain;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace UndyneFight_Ex
{
    public static class DrawingLab
    {
        /// <summary>
        /// 按顺时针输入点列，获得该点列的一组三角剖分
        /// </summary>
        /// <param name="pointList"></param>
        /// <returns></returns>
        public static int[] GetIndices(VertexPositionColor[] pointList)
        {
            int i;
            Vector2[] vector2s = new Vector2[pointList.Length];
            for(i = 0;  i < pointList.Length; i++)
            {
                vector2s[i] = new(pointList[i].Position.X, pointList[i].Position.Y);
            }
            List<Tuple<int, int, int>> results = GetIndices(vector2s);
            int[] indices = new int[results.Count * 3];
            i = 0;
            foreach(Tuple<int, int, int> tuple in results)
            {
                indices[i] = tuple.Item1; i++;
                indices[i] = tuple.Item2; i++;
                indices[i] = tuple.Item3; i++;
            }
            return indices;
        }
        /// <summary>
        /// 按顺时针输入点列，获得该点列的一组三角剖分
        /// </summary>
        public static List<Tuple<int, int, int>> GetIndices(VertexPositionColorTexture[] pointList)
        {
            Vector2[] vector2s = new Vector2[pointList.Length];
            for(int i = 0;  i < pointList.Length; i++)
            {
                vector2s[i] = new(pointList[i].Position.X, pointList[i].Position.Y);
            }
            return GetIndices(vector2s);
        }
        /// <summary>
        /// 按顺时针输入点列，获得该点列的一组三角剖分
        /// </summary>
        public static List<Tuple<int, int, int>> GetIndices(Vector2[] pointList) { 
            Tuple<int, Vector2>[] arr = new Tuple<int, Vector2>[pointList.Length];
            for (int i = 0; i < arr.Length; i++) arr[i] = new(i, pointList[i]);
            return GetIndices(arr);
        }
        
        /// <summary>
        /// 按顺时针输入点列，获得该点列的一组三角剖分
        /// </summary>
        public static List<Tuple<int, int, int>> GetIndices(Tuple<int, Vector2>[] pointList)
        {
            if (pointList.Length <= 2) return new();
            if(pointList.Length == 3) {
                return new List<Tuple<int, int, int>>() { new Tuple<int, int, int>(pointList[0].Item1, pointList[1].Item1, pointList[2].Item1) };
            }  
            List<Tuple<int, int, int>> result = new(); 

            List<int> reflexs = null;

            bool[] reflex = new bool[pointList.Length];
            bool existReflex = false;
            Vector2 last = pointList[0].Item2 - pointList[^1].Item2;
            for (int i = 0; i < pointList.Length; i++)
            {
                int i2 = i + 1;
                if (i2 == pointList.Length) i2 = 0;
                Vector2 cur = pointList[i2].Item2 - pointList[i].Item2;
                if(last.Cross(cur) < 0)
                {
                    if (!existReflex)
                    {
                        reflexs = new();
                        existReflex = true;
                    }
                    reflex[i] = true;
                    reflexs.Add(i);
                }
                else
                {
                    reflex[i] = false;
                }
                last = cur;
            }

            if(!existReflex) //凸多边形
            {
                for(int i = 2; i < pointList.Length; i++)
                {
                    result.Add(new(pointList[0].Item1, pointList[i - 1].Item1, pointList[i].Item1));
                }
                return result;
            }
            // 凹多边形
            int length = pointList.Length;
            bool[] used = new bool[pointList.Length];
            for(int i = 0; i < pointList.Length; i++)
            {
                if (i == pointList.Length - 1 && used[0]) break;
                if (!reflex[i]) // 可能是可以分割的顶点
                {
                    int v1 = i, v0 = i - 1, v2 = i + 1;
                    if (v0 < 0) v0 = pointList.Length - 1;
                    if (v2 >= pointList.Length) v2 = 0;

                    Vector2 pv1 = pointList[v1].Item2, pv0 = pointList[v0].Item2, pv2 = pointList[v2].Item2;

                    bool flag = true;
                    foreach (int j in reflexs) // 检验是否可以分割
                    {
                        if (j == v2 || j == v0) continue;
                        if (InTriangle(pv1, pv0, pv2, pointList[j].Item2))
                        { // 在三角形内，不可分割
                            flag = false;
                            break;
                        }
                    }
                    used[i] = flag;
                    if (flag) // 添加一组三角
                    {
                        length -= 1;
                        i++;
                        result.Add(new(pointList[v1].Item1, pointList[v0].Item1, pointList[v2].Item1));
                    }
                }
                else used[i] = false;
            }
            int k = 0;
            Tuple<int, Vector2>[] tuples = new Tuple<int, Vector2>[length];
            for(int i = 0; i < pointList.Length; i++)
            {
                if (!used[i])
                {
                    tuples[k] = pointList[i];
                    k++;
                }
            }
            result.AddRange(GetIndices(tuples));

            return result;

        }

        private struct HSV
        {
            public int H, S, V;
            public HSV(int h, int s, int v)
            {
                H = h;
                S = s;
                V = v;
            }
        }
        /// <summary>
        /// S,V,A均在[0, 255]取值，H通常在[0, 360]取值
        /// </summary>
        /// <param name="H_"></param>
        /// <param name="S_"></param>
        /// <param name="V_"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public static Vector4 HsvToRgb(float H_, int S_, int V_, int A)
        {
            int _H = (int)H_;
            _H %= 360;
            HSV hsv = new(_H, S_, V_);
            float R = 0f, G = 0f, B = 0f;
            if (hsv.S == 0)
                return new Color(hsv.V, hsv.V, hsv.V).ToVector4();
            float S = hsv.S * 1.0f / 255, V = hsv.V * 1.0f / 255;
            int H1 = (int)(hsv.H * 1.0f / 60), H = hsv.H;
            float F = H * 1.0f / 60 - H1;
            float P = V * (1.0f - S);
            float Q = V * (1.0f - F * S);
            float T = V * (1.0f - (1.0f - F) * S);
            switch (H1)
            {
                case 0: R = V; G = T; B = P; break;
                case 1: R = Q; G = V; B = P; break;
                case 2: R = P; G = V; B = T; break;
                case 3: R = P; G = Q; B = V; break;
                case 4: R = T; G = P; B = V; break;
                case 5: R = V; G = P; B = Q; break;
            }
            R *= 255;
            G *= 255;
            B *= 255;
            while (R > 255) R -= 255;
            while (R < 0) R += 255;
            while (G > 255) G -= 255;
            while (G < 0) G += 255;
            while (B > 255) B -= 255;
            while (B < 0) B += 255;
            return new Vector4(R / 255f, G / 255f, B / 255f, A / 255f);
        }

        public static void MaskDraw(Texture2D tex, Vector2 centre, Color color, float rotation, Vector2 rotateCentre, float depth, CollideRect mask)
        {
            float h = tex.Height;
            float w = tex.Width;
            MaskDraw(tex, new CollideRect(centre.X - w / 2f, centre.Y - h / 2f, w, h), color, rotation, Vector2.Zero, depth, mask);
        }

        public static void MaskDraw(Texture2D tex, CollideRect drawArea, Color color, float rotation, Vector2 rotateCentre, float depth, CollideRect mask)
        {
            color *= Surface.Normal.drawingAlpha;
            Vector2 samplerPlace = Vector2.Zero;
            Vector2 size = drawArea.Size;
            if (mask.TopLeft.X > drawArea.X)
            {
                float detla = mask.TopLeft.X - drawArea.X;
                samplerPlace.X += detla;
                drawArea.X += detla;
                drawArea.Width -= detla;
                size.X -= detla;
            }
            if (mask.TopLeft.Y > drawArea.Y)
            {
                float detla2 = mask.TopLeft.Y - drawArea.Y;
                samplerPlace.Y += detla2;
                drawArea.Y += detla2;
                drawArea.Height -= detla2;
                size.Y -= detla2;
            }
            if (drawArea.Right > mask.Right)
            {
                float detla3 = drawArea.Right - mask.Right;
                size.X -= detla3;
                drawArea.Width -= detla3;
            }
            if (drawArea.Down > mask.Down)
            {
                float detla4 = drawArea.Down - mask.Down;
                size.Y -= detla4;
                drawArea.Height -= detla4;
            }
            if (!(size.X < 0f || size.Y < 0f))
                MissionSpriteBatch.Draw(tex, drawArea, new CollideRect(samplerPlace, size), color, rotation, rotateCentre, SpriteEffects.None, depth);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="triangle">Three vertex information, first is (0, 0), second is (1, 0), third is (0, 1)</param>
        /// <param name="cur"></param>
        /// <returns></returns>
        public static Vector2 UVPosition(Vector2[] triangle, Vector2 cur)
        {
            Vector2 dirX, dirY;
            dirX = triangle[1] - triangle[0]; dirY = triangle[2] - triangle[0];
            Vector2 target = cur - triangle[0];
            float proX = Project(dirX, target), proY = Project(dirY, target);
            return new Vector2(proX / dirX.Length(), proY / dirY.Length());
        }

        public const float quarterAngle = (float)(0.5 * Math.PI);
        public static void DrawLine(Vector2 P1, Vector2 P2, float width, Color cl, float depth, Texture2D texture = null)
        {
            Vector2 Centre = (P1 + P2) / 2;
            float Angle = (float)Math.Atan2(P2.Y - P1.Y, P2.X - P1.X);
            float dist = GetDistance(P1, P2) + 2;
            DrawLine(Centre, Angle, dist, width, cl, depth, texture);
        }

        /// <summary>
        /// 画一个向量
        /// </summary>
        /// <param name="centre">向量中心位置</param>
        /// <param name="rotation">向量旋转弧度</param>
        public static void DrawVector(Vector2 centre, float rotation)
        {
            MissionSpriteBatch.Draw(GlobalResources.Sprites.debugArrow, centre, null, Color.White * 0.5f,
                rotation, new Vector2(3, 3), 1.0f, SpriteEffects.None, 0.9999f);
        }
        /// <summary>
        /// 绘制线条
        /// </summary>
        /// <param name="Centre">线条中心</param>
        /// <param name="Angle">线条绕中心旋转角</param>
        /// <param name="Length">线条长度</param>
        /// <param name="width">线宽度</param>
        /// <param name="cl">线条颜色</param>
        public static void DrawLine(Vector2 Centre, float angle, float length, float width, Color cl, float depth, Texture2D texture = null)
        {
            if (texture == null) texture = FightResources.Sprites.pixUnit; 
            angle = GetAngle(angle);
            Vector2 v1 = GetVector2(length / 2f, angle);
            Vector2 v2 = -v1;
            v1 += Centre; v2 += Centre;
            Vector2 del = GetVector2(width / 2f, angle + 90);
            Vector2 p1 = v1 + del, p2 = v2 + del;
            Vector2 p3 = v1 - del, p4 = v2 - del;
            MissionSpriteBatch.DrawVertex(texture, depth, 
                new VertexPositionColorTexture(new(p1, depth), cl, new(0, 0)),
                new VertexPositionColorTexture(new(p2, depth), cl, new(0, 1)),
                new VertexPositionColorTexture(new(p4, depth), cl, new(1, 1)),
                new VertexPositionColorTexture(new(p3, depth), cl, new(1, 0))
                );
        }

        /// <summary>
        /// 绘制一个矩形
        /// </summary>
        /// <param name="rect">矩形边界</param>
        /// <param name="color">矩形颜色</param>
        /// <param name="width">矩形边的宽度</param>
        public static void DrawRectangle(CollideRect rect, Color color, float width, float depth)
        {
            Vector2 V2 = rect.TopLeft + new Vector2(0, rect.Height);
            Vector2 V3 = rect.TopLeft + new Vector2(rect.Width, 0);
            Vector2 V4 = rect.TopLeft + new Vector2(rect.Width, rect.Height);
            DrawLine(rect.TopLeft, V2, width, color, depth);
            DrawLine(rect.TopLeft, V3, width, color, depth);
            DrawLine(V2, V4, width, color, depth);
            DrawLine(V3, V4, width, color, depth);
        }
    }
    public static class UsingShader
    {
        public static Shader BackGround { get; internal set; }
    }
    public class Shader
    {
        public Shader(Effect effect)
        {
            this.effect = effect;
        }
        private readonly Effect effect;
        private string effectName = "NormalDrawing";
        public string EffectName { get => effectName; set { effectName = value; effect.CurrentTechnique = effect.Techniques[value]; } }

        public EffectParameterCollection Parameters => effect.Parameters;
        public Dictionary<string, Action<Effect>> PartEvents { private get; set; }
        public Action<Effect> StableEvents { private get; set; }

        public bool LateApply { get; set; } = false;
        public static float TimeElapsed { get; internal set; }

        public void RegisterTexture(Texture2D tex, int index)
        {
            RegisterTextures[index - 1] = tex;
        }

        public void Update()
        {
            Shader shader = this;
            shader.StableEvents?.Invoke(shader);
            if (shader.PartEvents != null && shader.PartEvents.ContainsKey(shader.effectName))
                shader.PartEvents[shader.effectName](shader.effect);
        }

        public static implicit operator Effect(Shader shader)
        {
            return shader.effect;
        }

    }
    public class GLFont
    {
        public SpriteFont SFX;
        Dictionary<char, int> charIndex = new();
        public GLFont(string path, ContentManager cm)
        {
            SFX = cm.Load<SpriteFont>(path);
            for (int i = 0; i < SFX.Glyphs.Length; i++) {
                charIndex[SFX.Glyphs[i].Character] = i; 
            }
        }
        public void Draw(string texts, Vector2 location, Color color)
        {
            MissionSpriteBatch.DrawString(this, texts, location, color * Surface.Normal.drawingAlpha);
        }
        public void Draw(string texts, Vector2 location, Color color, SpriteBatchEX sb)
        {
            sb.DrawString(this, texts, location, color * Surface.Normal.drawingAlpha);
        }
        public void CentreDraw(string texts, Vector2 location, Color color)
        {
            MissionSpriteBatch.DrawString(this, texts, location - SFX.MeasureString(texts) / 2, color * Surface.Normal.drawingAlpha);
        }
        public void CentreDraw(string texts, Vector2 location, Color color, SpriteBatchEX sb)
        {
            sb.DrawString(this, texts, location - SFX.MeasureString(texts) / 2, color * Surface.Normal.drawingAlpha);
        }
        public void Draw(string texts, Vector2 location, Color color, float scale, float depth)
        {
            MissionSpriteBatch.DrawString(this, texts, location, color * Surface.Normal.drawingAlpha, 0, Vector2.Zero, scale, SpriteEffects.None, depth);
        }
        public void Draw(string texts, Vector2 location, Color color, float rotation, float scale, Vector2 anchor, float depth)
        {
            MissionSpriteBatch.DrawString(this, texts, location, color * Surface.Normal.drawingAlpha, rotation, anchor, scale, SpriteEffects.None, depth);
        }
        public void LimitDraw(string texts, Vector2 location, Microsoft.Xna.Framework.Color color, float lineLength, float lineDistance, float scale, float depth)
        {
            Vector2[] sizes = new Vector2[texts.Length];
            for (int i = 0; i < texts.Length; i++)
                sizes[i] = SFX.MeasureString(texts[i].ToString());

            string curLine = "";
            float cur = 0;
            List<string> strings = new();
            for (int i = 0; i < texts.Length; i++)
            {
                float v;
                bool u;
                cur += v = sizes[i].X * scale;
                if ((u = texts[i] == '\r' || texts[i] == '\n') || cur > lineLength)
                {
                    strings.Add(curLine);
                    curLine = "";
                    cur = v;
                    if (u) continue;
                }
                curLine += texts[i];
            }
            strings.Add(curLine);
            foreach (string s in strings)
            {
                MissionSpriteBatch.DrawString(this, s, location, color * Surface.Normal.drawingAlpha, 0, Vector2.Zero, scale, SpriteEffects.None, depth);
                location.Y += lineDistance;
            }
        }
        public void Draw(string texts, Vector2 location, Microsoft.Xna.Framework.Color color, float rotation, float scale, float depth)
        {
            MissionSpriteBatch.DrawString(this, texts, location, color * Surface.Normal.drawingAlpha, rotation, Vector2.Zero, scale, SpriteEffects.None, depth);
        }
        public void CentreDraw(string texts, Vector2 location, Color color, float scale, float depth)
        {
            try
            {
                if (string.IsNullOrEmpty(texts)) 
                    return;
                MissionSpriteBatch.DrawString(this, texts, location, color * Surface.Normal.drawingAlpha, 0, SFX.MeasureString(texts) / 2, scale, SpriteEffects.None, depth);
            }
            catch
            {

            }
        }
        public void CentreDraw(string texts, Vector2 location, Color color, float scale, float rotation, float depth)
        {
            MissionSpriteBatch.DrawString(this, texts, location, color * Surface.Normal.drawingAlpha, rotation, SFX.MeasureString(texts) / 2, scale, SpriteEffects.None, depth);
        }

        internal unsafe int GetGlyphIndexOrDefault(char c)
        {
            return charIndex[c];
        }

        public Vector2 MeasureChar(char ch)
        {
            return SFX.Glyphs[GetGlyphIndexOrDefault(ch)].Cropping.Size.ToVector2();
        }
    }
}