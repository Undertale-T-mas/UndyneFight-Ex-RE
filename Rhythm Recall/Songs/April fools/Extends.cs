using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static Extends.DrawingUtil;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;

namespace Extends
{
    public class LineImage : Entity, ICustomMotion
    {
        Vector2[] positions;
        CentreEasing.EaseBuilder[] easings;
        public LineImage(Vector2[] positions)
        {
            this.positions = positions;
            easeis = false;
        }
        public LineImage(CentreEasing.EaseBuilder[] easings)
        {
            this.easings = easings;
        }
        float timer = 0;
        Vector2[] easeresult = { };
        bool stop = true;
        bool easeis = true;
        public Func<ICustomMotion, Vector2> PositionRoute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Func<ICustomMotion, float> RotationRoute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float[] RotationRouteParam { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float[] PositionRouteParam { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public float AppearTime => timer;

        public Vector2 CentrePosition => Centre;
        public float Alpha = 1;
        public Color DrawingColor = Color.White;
        public float Width = 3.0f;
        public override void Draw()
        { 
            if (stop && easeis)
            {
                positions = new Vector2[easings.Length];
                for (int t = 0; t < easings.Length - 1; t++)
                {
                    easings[t].Run((k) => { positions[t] = k; });
                }
                stop = false;
            }
            //if (easeis) for (int t = 0; t < easings.Length; t++) positions[t] = easeresult[t];
            for (int t = 0; t < positions.Length - 1; t++)
            {
                Line l = new(positions[t], positions[t + 1])
                { Alpha = Alpha, Depth = Depth, DrawingColor = DrawingColor, Width = Width };
                CreateEntity(l);
                AddInstance(new TimeRangedEvent(0.5f, () =>
                {
                    l.Dispose();
                }));
            }

        }
        public override void Update()
        {

        }
    }
    public class Star : Entity, ICollideAble, ICustomMotion
    {
        public class StarShadow : Entity
        {
            public Vector2 speed;
            public float alpha;
            public float rotatespeed;
            public float scale;
            public Color color = Color.White;
            public StarShadow(float alpha, float angle, Vector2 center, float rotatespeed, float scale, Color color)
            {
                Image = Sprites.star;
                this.alpha = alpha;
                Rotation = angle;
                Centre = center;
                this.rotatespeed = rotatespeed;
                this.scale = scale;
                this.color = color;
            }
            public override void Draw()
            {

                Depth = 0.74f;
                alpha -= 0.08f;
                FormalDraw(Image, Centre, color * alpha, scale, Rotation * MathF.PI / 180, ImageCentre);
            }
            public override void Update()
            {
                Rotation += rotatespeed;
                if (alpha <= 0) Dispose();
            }
        }

        private int colorType = 0;
        private Color drawcolor = Color.White;
        public int ColorType
        {
            set
            {
                switch (value)
                {
                    case 0:
                        drawcolor = Color.White;
                        colorType = 0;
                        break;
                    case 1:
                        drawcolor = new Color(110, 203, 255, 255);
                        colorType = 1;
                        break;
                    case 2:
                        drawcolor = Color.Orange;
                        colorType = 2;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("rvalue", value, "The rvalue can only be 0, 1 or 2");
                }
            }
        }

        public Func<ICustomMotion, Vector2> PositionRoute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Func<ICustomMotion, float> RotationRoute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float[] RotationRouteParam { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float[] PositionRouteParam { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public float AppearTime => appeartime;

        public Vector2 CentrePosition => Centre;

        public Vector2 speed;

        public float rotatespeed = 6f;

        public bool starshadow = true;
        public bool rotate = true;

        Func<ICustomMotion, Vector2> ease;
        bool easeif = false;
        public Star(Vector2 centre, float scale)
        {
            Image = Sprites.star;
            Rotation = Rand(0, 359);
            Centre = centre; ;
            Scale = scale;
        }
        public Star(Func<ICustomMotion, Vector2> ease, float scale)
        {
            easeif = true;
            Image = Sprites.star;
            Rotation = Rand(0, 359);
            this.ease = ease;
            Scale = scale;
        }
        float appeartime = 0;
        Color light = Color.White * 0.25f;
        public override void Draw()
        {

            Depth = 0.75f;

            FormalDraw(Image, Centre, new Color(drawcolor.R + light.R, drawcolor.G + light.G, drawcolor.B + light.B, drawcolor.A + light.A), Scale, Rotation * MathF.PI / 180, ImageCentre);
            if (appeartime % 2 == 0 && starshadow)
                Shadow(Centre, Rotation);
            if (appeartime >= 12 * 60) Dispose();
        }
        public override void Update()
        {
            if (easeif) ease.Invoke(this);
            Rotation += rotatespeed;
            TestDispose();
            if (appeartime % 22 == 0) light = ChangeLight(light);

        }
        int scoreResult = 3;
        private bool hasHit = false;
        private JudgementState JudgeState
        {
            get
            {
                return GameStates.CurrentScene is SongFightingScene
                    ? (GameStates.CurrentScene as SongFightingScene).JudgeState
                    : JudgementState.Lenient;
            }
        }
        public bool MarkScore { private get; set; } = true;
        public bool AutoDispose { private get; set; } = true;
        public void GetCollide(Player.Heart heart)
        {
            float res = Collide(heart.Centre);
            if (colorType == 1 && !heart.IsMoved) return;
            if (colorType == 2 && heart.IsMoved) return;

            if (res < 0)
            {
                scoreResult = 0;
                LoseHP(heart);
            }

            int offset = 3 - (int)JudgeState;
            bool needAP = ((CurrentScene as FightScene).Mode & GameMode.PerfectOnly) != 0;
            if (res < 0) { if (!hasHit) UndyneFight_Ex.Fight.AdvanceFunctions.PushScore(0); LoseHP(Heart); hasHit = true; }
            else if (res <= 1.6f - offset * 0.4f)
            {
                if (scoreResult >= 2) { scoreResult = 1; Player.CreateCollideEffect(Color.LawnGreen, 3f); }
            }
            else if (res <= 4.2f - offset * 1.2f)
            {
                if (scoreResult >= 3) { scoreResult = 2; Player.CreateCollideEffect(Color.LightBlue, 6f); }
            }
            if (scoreResult != 3 && needAP && MarkScore)
            {
                if (!hasHit)
                {
                    UndyneFight_Ex.Fight.AdvanceFunctions.PushScore(0); LoseHP(Heart); hasHit = true;
                }
            }
        }

        public override void Dispose()
        {
            if (!hasHit && MarkScore) UndyneFight_Ex.Fight.AdvanceFunctions.PushScore(scoreResult);
            base.Dispose();
        }
        private bool hasBeenInside = false;
        public static CollideRect screen = new(-150, -150, 940, 780);
        public void TestDispose()
        {
            bool ins = screen.Contain(Centre);
            if (ins && (!hasBeenInside)) hasBeenInside = true;
            if (hasBeenInside && (!ins)) Dispose();
        }
        public void Shadow(Vector2 center, float rotate)
        {
            AddChild(new StarShadow(0.5f, rotate, center, rotatespeed, Scale, drawcolor));
        }
        public Color ChangeLight(Color light)
        {
            return light == Color.White * 0.25f ? (light = new(0, 0, 0, 0)) : (light = Color.White * 0.25f);
        }
        float Collide(Vector2 org)
        {
            return MathUtil.GetDistance(Centre, org) - 32f * Scale;
        }
    }
    public class Fireball : Entity, ICustomMotion, ICollideAble
    {
        private int colorType = 0;
        private Color drawcolor = Color.White;
        public int ColorType
        {
            set
            {
                switch (value)
                {
                    case 0:
                        drawcolor = Color.White;
                        colorType = 0;
                        break;
                    case 1:
                        drawcolor = new Color(110, 203, 255, 255);
                        colorType = 1;
                        break;
                    case 2:
                        drawcolor = Color.Orange;
                        colorType = 2;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("rvalue", value, "The rvalue can only be 0, 1 or 2");
                }
            }
        }
        public Func<ICustomMotion, Vector2> PositionRoute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Func<ICustomMotion, float> RotationRoute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float[] RotationRouteParam { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float[] PositionRouteParam { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public float AppearTime => appeartime;
        float appeartime = 0;
        public Vector2 CentrePosition => Centre;
        Func<ICustomMotion, Vector2> ease;
        bool easeif = false;
        public Fireball(Func<ICustomMotion, Vector2> ease, float scale)
        {
            this.ease = ease;
            Scale = scale;
        }
        public Fireball(Vector2 centre, float scale)
        {
            easeif = false;
            Centre = centre; ;
            Scale = scale;
        }
        int index = 0;
        public float Alpha = 1;

        public override void Draw()
        {
            Depth = 0.9f;
            DrawEvent();
            FormalDraw(Image, Centre, drawcolor * Alpha, Scale, 0, ImageCentre);
        }

        public void GetCollide(Player.Heart heart)
        {
            bool result = Collide(heart.Centre);
            if (colorType == 0 && result) LoseHP(heart);
            else if (colorType == 1 && heart.IsMoved && result) LoseHP(heart);
            else if (colorType == 2 && !heart.IsMoved && result) LoseHP(heart);
        }

        public override void Update()
        {
            controlLayer = IsHidden ? Surface.Hidden : Surface.Normal;
            if (easeif) ease.Invoke(this);
            if (Centre.X >= 880 || Centre.X <= -240 || Centre.Y >= 480 + 240 || Centre.Y <= -240) Dispose();
            if (Alpha <= 0) Dispose();
        }
        public bool IsHidden { set; private get; } = false;
        void DrawEvent()
        {
            Image = Sprites.fireball[index];
            if (appeartime % 64 == 0) { index++; index %= 2; }
        }
        bool Collide(Vector2 org)
        {
            return MathUtil.GetDistance(Centre, org) <= 6.5f * Scale;
        }
    }
    public class Gunbolt : Entity, ICustomMotion, ICollideAble
    {
        private int colorType = 0;
        private Color drawcolor = Color.White;
        public int ColorType
        {
            set
            {
                switch (value)
                {
                    case 0:
                        drawcolor = Color.White;
                        colorType = 0;
                        break;
                    case 1:
                        drawcolor = new Color(110, 203, 255, 255);
                        colorType = 1;
                        break;
                    case 2:
                        drawcolor = Color.Orange;
                        colorType = 2;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("rvalue", value, "The rvalue can only be 0, 1 or 2");
                }
            }
        }
        public Func<ICustomMotion, Vector2> PositionRoute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Func<ICustomMotion, float> RotationRoute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float[] RotationRouteParam { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float[] PositionRouteParam { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public float AppearTime => appeartime;
        float appeartime = 0;
        public Vector2 CentrePosition => Centre;
        Func<ICustomMotion, Vector2> ease;
        bool easeif = false;
        public Gunbolt(Func<ICustomMotion, Vector2> ease)
        {

            this.ease = ease;

        }
        public Gunbolt(Vector2 centre)
        {
            easeif = false;
            Centre = centre; ;
        }
        int index = 0;
        public float Alpha = 1;

        public override void Draw()
        {
            Image = Sprites.gunbolt;
            Depth = 0.9f;
            FormalDraw(Image, Centre, drawcolor * Alpha, Scale, 0, ImageCentre);
        }

        public void GetCollide(Player.Heart heart)
        {
            bool result = Collide(heart.Centre);
            if (colorType == 0 && result) LoseHP(heart);
            else if (colorType == 1 && heart.IsMoved && result) LoseHP(heart);
            else if (colorType == 2 && !heart.IsMoved && result) LoseHP(heart);
        }

        public override void Update()
        {
            if (easeif) ease.Invoke(this);
            if (Centre.X >= 880 || Centre.X <= -240 || Centre.Y >= 480 + 240 || Centre.Y <= -240) Dispose();
            if (Alpha <= 0) Dispose();
        }
        public bool IsHidden = false;
        bool Collide(Vector2 org)
        {
            return MathUtil.GetDistance(Centre, org) <= 4.5f * Scale;
        }
    }
    /*public class DeadLaser : Entity, ICollideAble
    {
        public class DeadWarn : Entity
        {
            public float warntime;
            public Vector2 center;
            Color warncolor;
            Texture2D image = Sprites.deadwarn;
            public DeadWarn(float warntime, Vector2 center)
            {
                Image = image;
                this.warntime = warntime;
                this.center = center;
            }
            public float timer=0;
            public override void Draw()
            {
                FormalDraw(image, center, warncolor, 1, ImageCentre);
                if (timer %3==0) ChangeColor(warncolor);
            }

            public override void Update()
            {
                timer++;
                if (timer >= warntime) Dispose();
                PlaySound(Sounds.deadwarn);
            }
            public Color ChangeColor(Color org)
            {
                Color ret = Color.Yellow;
                if (org == Color.Yellow) ret = Color.Orange;
                else ret = Color.Yellow;
                return ret;
            }
        }
        public float warntime;
        public float scale;
        public Vector2 center;
        public DeadLaser(float scale,float warntime,Vector2 center)
        {
            Image = Sprites.deadlaser[index];
            this.scale = scale;
            this.warntime = warntime;
        }
        public float timer = 0;
        int index = 0;
        public override void Draw()
        {
            if (timer == 1) CreateEntity(new DeadWarn(warntime, center));
            if(timer>=warntime)
            {
                FormalDraw(Image, center, Color.White, scale, 270, ImageCentre);
            }
            if (timer >= warntime && timer<=warntime+60)
            {
                scale = scale * 0.9f + 0 * 0.1f;
            }

        }
        public override void Update()
        {
            timer++;
            Image = Sprites.deadlaser[index];
            if (timer == 1) PlaySound(Sounds.largeKnife);
            if (timer % 4 == 0) { index++; index %= 2; }
            if (timer >= warntime + 60) Dispose();
        }
        public void GetCollide(Player.Heart player)
        {
            if(player.Centre.Y<=center.Y+Image.Width/2 && player.Centre.Y >= center.Y - Image.Width / 2)
            {
                LoseHP(player);
            }
        }

    }*/
    public class FakeArrow : Entity
    {
        float duration;
        public float scale;
        public float depth = 0.75f;
        public FakeArrow(int color, int rotatingType, int breakType, Vector2 center, float duration, float scale, float rotation)
        {
            Image = Sprites.arrow[color, rotatingType, breakType];
            Centre = center;
            this.scale = scale;
            this.duration = duration;
            Rotation = rotation;
        }
        float timer;
        public override void Draw()
        {
            Depth = depth;
            FormalDraw(Image, Centre, Color.White, scale, Rotation / 180 * MathF.PI, ImageCentre);
        }

        public override void Update()
        {
            timer++;
            if (timer >= duration) Dispose();
        }
        public void ChangeType(int color, int rotatingType, int breakType)
        {
            Image = Sprites.arrow[color, rotatingType, breakType];
            var v = CreateShinyEffect(Color.White);
            v.Depth = depth + 0.00001f;
            v.DarkerSpeed = 6f;
        }
    }
    public static class Someway
    {
        /// <summary>
        /// 使用一个节奏数组。
        /// 规则如下：
        /// "/"为空拍,x<表示切分拍;
        /// 箭头第一个字符-方向 的规则: R 表示随机 D 表示与上次不同 +x 表示上一个方向的 +x方向 -x 表示上一个方向的 -x方向 $x 表示固定某个方向。
        /// 箭头第二个字符-颜色 的规则：0蓝1红。
        /// 箭头第三个字符-旋转type 的规则：0普通1旋转2斜矛。
        /// 箭头组合规则：字符串内加括号表示不仅创建一个箭头,例如"(R)(R1)"。
        /// PS："R(+0)" ≠ "(R)(+0)",后者才是两个矛叠一块，第一个会无效。
        /// </summary>
        /// <param name="beat">节奏数组的间隔拍，推荐三十二分音符为间隔</param>
        /// <param name="arrowspeed">假如创建出来箭头，那么这个箭头的速度</param>
        /// <param name="starttime">延迟的时间</param>
        /// <param name="rhythm">节奏数组</param>
        public static void RhythmCreate(float beat, float arrowspeed, float starttime, string[] rhythm)
        {
            float b = starttime;
            WaveConstructor a = new(beat);
            for (int i = 0; i < rhythm.Length; i++)
            {
                if (rhythm[i].Length >= 2)
                {
                    if (rhythm[i][1] == '<') b += (beat * 8) / (rhythm[i][0] - '0');
                }
                if (rhythm[i] == "/" || rhythm[i] == "") b += beat;
                else if (rhythm[i][0] is '+' or 'R' or '$' or '(' or '-' or 'D' or '0'
                    or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9')
                {
                    if (rhythm[i].Length > 2)
                    {
                        if (rhythm[i][1] == '<')
                        {
                            a.CreateArrows(b, arrowspeed, rhythm[i].Replace(rhythm[i][0].ToString() + rhythm[i][1].ToString(), ""));
                            b += (beat * 8) / (rhythm[i][0] - '0');
                        }
                        else
                        {
                            a.CreateArrows(b, arrowspeed, rhythm[i]);
                            b += beat;
                        }
                    }
                    else
                    {
                        a.CreateArrows(b, arrowspeed, rhythm[i]);
                        b += beat;
                    }

                }
            }
        }
        /// <summary>
        /// 使用一个节奏数组,并且支持在定点释放事件。
        /// 规则如下：
        /// eventsname和events是对应的。例如第二个eventsname对应第二个events。
        /// "/"为空拍,x<表示切分拍;
        /// 箭头第一个字符-方向 的规则: R 表示随机 D 表示与上次不同 +x 表示上一个方向的 +x方向 -x 表示上一个方向的 -x方向 $x 表示固定某个方向。
        /// 箭头第二个字符-颜色 的规则：0蓝1红。
        /// 箭头第三个字符-旋转type 的规则：0普通1旋转2斜矛。
        /// 箭头组合规则：字符串内加括号表示不仅创建一个箭头,例如"(R)(R1)"。
        /// PS："R(+0)" ≠ "(R)(+0)",后者才是两个矛叠一块，第一个会无效。
        /// </summary>
        /// <param name="beat">节奏数组的间隔拍，推荐三十二分音符为间隔</param>
        /// <param name="arrowspeed">假如创建出来箭头，那么这个箭头的速度</param>
        /// <param name="starttime">延迟的时间</param>
        /// <param name="rhythm">节奏数组</param>
        /// <param name="eventsname">节奏数组内事件的名称</param>
        /// <param name="events">节奏数组内的事件</param>
        public static void RhythmCreate(float beat, float arrowspeed, float starttime, string[] rhythm, string[] eventsname, Action[] events)
        {
            float b = starttime;
            WaveConstructor a = new(beat);
            for (int i = 0; i < rhythm.Length; i++)
            {
                for (int c = 0; c < eventsname.Length; c++)
                {
                    if (rhythm[i] == eventsname[c]) AddInstance(new InstantEvent(b, events[c]));
                }
                if (rhythm[i].Length >= 2)
                {
                    if (rhythm[i][1] == '<') b += (beat * 8) / (rhythm[i][0] - '0');
                }
                if (rhythm[i] == "/" || rhythm[i] == "") b += beat;
                else if (rhythm[i][0] is '+' or 'R' or '$' or '(' or '-' or 'D' or '0'
                    or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9')
                {
                    if (rhythm[i].Length > 2)
                    {
                        if (rhythm[i][1] == '<')
                        {
                            a.CreateArrows(b, arrowspeed, rhythm[i].Replace(rhythm[i][0].ToString() + rhythm[i][1].ToString(), ""));
                            b += (beat * 8) / (rhythm[i][0] - '0');
                        }
                        else
                        {
                            a.CreateArrows(b, arrowspeed, rhythm[i]);
                            b += beat;
                        }
                    }
                    else
                    {
                        a.CreateArrows(b, arrowspeed, rhythm[i]);
                        b += beat;
                    }

                }
            }
        }
        /// <summary>
        /// 使用一个特殊的节奏数组,并且支持在定点释放事件。
        /// 规则如下：
        /// eventsname和events是对应的。例如第二个eventsname对应第二个events。
        /// GB的创建即
        /// "G001"，G表示固定方向，第二个字符为方向，第三个字符为颜色，第三个之后的字符表示持续beat拍（默认-5）
        /// "WR01"，W表示随机方向，第二个字符没有效果，第三个字符为颜色，第三个之后的字符表示持续beat拍（默认-5）
        /// "/"为空拍,x<表示切分拍;
        /// 箭头第一个字符-方向 的规则: R 表示随机 D 表示与上次不同 +x 表示上一个方向的 +x方向 -x 表示上一个方向的 -x方向 $x 表示固定某个方向。
        /// 箭头第二个字符-颜色 的规则：0蓝1红。
        /// 箭头第三个字符-旋转type 的规则：0普通1旋转2斜矛。
        /// 箭头修饰字符-箭头效果 的规则：前面加上~表示*1.2速加速，!表示右旋转，@表示左旋转
        /// 箭头组合规则：字符串内加括号表示不仅创建一个箭头,例如"(R)(R1)"。
        /// PS："R(+0)" ≠ "(R)(+0)",后者才是两个矛叠一块，第一个会无效。
        /// </summary>
        /// <param name="beat">节奏数组的间隔拍，推荐三十二分音符为间隔</param>
        /// <param name="arrowspeed">假如创建出来箭头，那么这个箭头的速度</param>
        /// <param name="starttime">延迟的时间</param>
        /// <param name="rhythm">节奏数组</param>
        /// <param name="eventsname">节奏数组内事件的名称</param>
        /// <param name="events">节奏数组内的事件</param>
        public static void SpecialRhythmCreate(float beat, float arrowspeed, float starttime, string[] rhythm, string[] eventsname, Action[] events)
        {
            float b = starttime;
            WaveConstructor a = new(beat);
            for (int i = 0; i < rhythm.Length; i++)
            {
                for (int c = 0; c < eventsname.Length; c++)
                {
                    if (rhythm[i] == eventsname[c]) AddInstance(new InstantEvent(b, events[c]));
                }
                if (rhythm[i] == "/" || rhythm[i] == "") b += beat;
                else if (rhythm[i][0] == 'G')
                {
                    string times = "";
                    for (int j = 3; j < rhythm[i].Length; j++) times += rhythm[i][j];
                    CreateGB(new GreenSoulGB(b, rhythm[i][1].ToString(), rhythm[i][2] - '0', StringToFloat(times) * beat - 5));
                }
                else if (rhythm[i][0] == 'W')
                {
                    int way = Rand(0, 3);
                    string times = "";
                    for (int j = 3; j < rhythm[i].Length; j++) times += rhythm[i][j];
                    CreateGB(new GreenSoulGB(b, way, rhythm[i][2] - '0', StringToFloat(times) * beat - 5));
                    CreateArrow(b, way, arrowspeed, rhythm[i][2] - '0', 0);
                }
                else if (rhythm[i][0] == '!')
                {
                    a.CreateArrows(b, arrowspeed * 1.05f, rhythm[i].Replace("!", ""), ArrowAttribute.RotateR);
                    b += beat;
                }
                else if (rhythm[i][0] == '@')
                {
                    a.CreateArrows(b, arrowspeed * 1.05f, rhythm[i].Replace("@", ""), ArrowAttribute.RotateL);
                    b += beat;
                }
                else if (rhythm[i][0] == '~')
                {
                    a.CreateArrows(b, arrowspeed * 1.35f, rhythm[i].Replace("~", ""), ArrowAttribute.SpeedUp);
                    b += beat;
                }

                else if (rhythm[i][0] is '+' or 'R' or '$' or '(' or '-' or 'D' or '0'
                    or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9')
                {
                    if (rhythm[i].Length > 2)
                    {
                        if (rhythm[i][1] == '<')
                        {
                            a.CreateArrows(b, arrowspeed, rhythm[i].Replace(rhythm[i][0].ToString() + rhythm[i][1].ToString(), ""));
                            b += (beat * 8) / (rhythm[i][0] - '0');
                        }
                        else
                        {
                            a.CreateArrows(b, arrowspeed, rhythm[i]);
                            b += beat;
                        }
                    }
                    else
                    {
                        a.CreateArrows(b, arrowspeed, rhythm[i]);
                        b += beat;
                    }

                }
            }
        }
        public static float ManySquare(float org, int squaretimes)
        {
            float ret = org;
            for (int i = 1; i < squaretimes; i++) ret *= org;
            if (squaretimes <= 0) for (int i = squaretimes - 1; i < 0; i++) ret *= 1 / org;
            return ret;
        }
        public static float StringToFloat(string number)
        {
            float ret = 0;
            for (int a = 0; a < number.Length; a++) { ret += (number[number.Length - a - 1] - '0') * ManySquare(10, a); }
            return ret;
        }
        public static Color ColorEF(int into)
        {
            return into == 0 ? Color.Cyan : Color.Orange;
        }
        public static int ColorE(Color into)
        {
            return into == Color.Cyan ? 1 : into == Color.Orange ? 2 : 0;
        }
        public static int Rand0or1()
        {
            return Rand(0, 1) == 0 ? 1 : -1;
        }

    }
    public class FightUtil
    {

        public class DownBonesea : Entity
        {
            public float duration = 0;
            public float quantity = 0;
            public float distance = 0;
            public float length = 0;
            public float speed = 0;
            public int colortype = 0;
            public bool way = true;
            public DownBonesea(float quantity, float distance, float length, bool way, float speed, float duration)
            {
                this.duration = duration;
                this.quantity = quantity;
                this.distance = distance;
                this.length = length;
                this.speed = speed;
                this.way = way;
            }
            public DownBonesea(float quantity, float distance, float length, bool way, float speed, float duration, int colortype)
            {
                this.duration = duration;
                this.quantity = quantity;
                this.distance = distance;
                this.length = length;
                this.speed = speed;
                this.way = way;
                this.colortype = colortype;
            }
            public override void Draw()
            {

            }
            float time = 0;
            public bool marcksore;
            private int appearTime;
            public string[] tags = { "noany" };
            public override void Update()
            {
                appearTime += 1;
                if (appearTime == 1)
                {
                    if (way)
                        for (int a = 0; a < quantity; a++)
                        {
                            float b = a * distance * speed;
                            DownBone bone1 = new(false, BoxStates.Left - b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                            CreateBone(bone1);
                        }
                    if (!way)
                        for (int a = 0; a < quantity; a++)
                        {
                            float b = a * distance * speed;
                            DownBone bone1 = new(true, BoxStates.Right + b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                            CreateBone(bone1);

                        }
                }
                if (time >= duration)
                {
                    Dispose();
                }
                time++;
            }
        }
        public class UpBonesea : Entity
        {
            public float duration = 0;
            public float quantity = 0;
            public float distance = 0;
            public float length = 0;
            public float speed = 0;
            public int colortype = 0;
            public bool way = true;
            public UpBonesea(float quantity, float distance, float length, bool way, float speed, float duration)
            {
                this.duration = duration;
                this.quantity = quantity;
                this.distance = distance;
                this.length = length;
                this.speed = speed;
                this.way = way;
            }
            public UpBonesea(float quantity, float distance, float length, bool way, float speed, float duration, int colortype)
            {
                this.duration = duration;
                this.quantity = quantity;
                this.distance = distance;
                this.length = length;
                this.speed = speed;
                this.way = way;
                this.colortype = colortype;
            }
            public override void Draw()
            {

            }
            int appearTime;
            float time = 0;
            public bool marcksore;
            public string[] tags = { "noany" };
            public override void Update()
            {
                appearTime += 1;
                if (appearTime == 1)
                {
                    if (way)
                        for (int a = 0; a < quantity; a++)
                        {
                            float b = a * distance * speed;
                            UpBone bone1 = new(false, BoxStates.Left - b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                            CreateBone(bone1);
                        }
                    if (!way)
                        for (int a = 0; a < quantity; a++)
                        {
                            float b = a * distance * speed;
                            UpBone bone1 = new(true, BoxStates.Right + b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                            CreateBone(bone1);

                        }
                }
                if (time >= duration)
                {
                    Dispose();
                }
                time++;
            }
        }
        public class LeftBonesea : Entity
        {
            public float duration = 0;
            public float quantity = 0;
            public float distance = 0;
            public float length = 0;
            public float speed = 0;
            public int colortype = 0;
            public bool way = true;
            public LeftBonesea(float quantity, float distance, float length, bool way, float speed, float duration)
            {
                this.duration = duration;
                this.quantity = quantity;
                this.distance = distance;
                this.length = length;
                this.speed = speed;
                this.way = way;
            }
            public LeftBonesea(float quantity, float distance, float length, bool way, float speed, float duration, int colortype)
            {
                this.duration = duration;
                this.quantity = quantity;
                this.distance = distance;
                this.length = length;
                this.speed = speed;
                this.way = way;
                this.colortype = colortype;
            }
            public override void Draw()
            {

            }
            int appearTime;
            float time = 0;
            public bool marcksore;
            public string[] tags = { "noany" };
            public override void Update()
            {
                appearTime++;
                if (appearTime == 1)
                {
                    if (way)
                        for (int a = 0; a < quantity; a++)
                        {
                            float b = a * distance * speed;
                            LeftBone bone1 = new(true, BoxStates.Left + b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                            CreateBone(bone1);
                        }
                    if (!way)
                        for (int a = 0; a < quantity; a++)
                        {
                            float b = a * distance * speed;
                            LeftBone bone1 = new(false, BoxStates.Right + b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                            CreateBone(bone1);

                        }
                }
                if (time >= duration)
                {
                    Dispose();
                }
                time++;
            }
        }
        public class RightBonesea : Entity
        {
            public float duration = 0;
            public float quantity = 0;
            public float distance = 0;
            public float length = 0;
            public float speed = 0;
            public int colortype = 0;
            public bool way = true;
            public RightBonesea(float quantity, float distance, float length, bool way, float speed, float duration)
            {
                this.duration = duration;
                this.quantity = quantity;
                this.distance = distance;
                this.length = length;
                this.speed = speed;
                this.way = way;
            }
            public RightBonesea(float quantity, float distance, float length, bool way, float speed, float duration, int colortype)
            {
                this.duration = duration;
                this.quantity = quantity;
                this.distance = distance;
                this.length = length;
                this.speed = speed;
                this.way = way;
                this.colortype = colortype;
            }
            public override void Draw()
            {

            }
            int appearTime;
            float time = 0;
            public bool marcksore;
            public string[] tags = { "noany" };
            public override void Update()
            {
                appearTime++;
                if (appearTime == 1)
                {
                    if (way)
                        for (int a = 0; a < quantity; a++)
                        {
                            float b = a * distance * speed;
                            RightBone bone1 = new(true, BoxStates.Left + b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                            CreateBone(bone1);
                        }
                    if (!way)
                        for (int a = 0; a < quantity; a++)
                        {
                            float b = a * distance * speed;
                            RightBone bone1 = new(false, BoxStates.Right + b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                            CreateBone(bone1);

                        }
                }
                if (time >= duration)
                {
                    Dispose();
                }
                time++;
            }
        }
        public class RotBone : Entity
        {
            public float rotate = 0;
            public float length = 0;
            public float speed = 0;
            public bool way = true;
            int colorType = 0;
            Color drawcolor = Color.White;
            public int ColorType
            {
                set
                {
                    switch (value)
                    {
                        case 0:
                            drawcolor = Color.White;
                            colorType = 0;
                            break;
                        case 1:
                            drawcolor = new Color(110, 203, 255, 255);
                            colorType = 1;
                            break;
                        case 2:
                            drawcolor = Color.Orange;
                            colorType = 2;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("rvalue", value, "The rvalue can only be 0, 1 or 2");
                    }
                }
            }
            public static Vector2 point;
            public static Vector2 deviation;
            public static Vector2 distance;
            public int pointtype;
            /*{
                set
                {
                    switch (value)
                    {
                        case 0:
                            point = new Vector2(BoxStates.Left, BoxStates.Down);
                            break; 
                        case 1:
                            point = new Vector2(BoxStates.Right, BoxStates.Down);
                            break;
                        case 2:
                            point = new Vector2(BoxStates.Right, BoxStates.Up);
                            break;
                        case 3:
                            point = new Vector2(BoxStates.Left, BoxStates.Up);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("rvalue", value, "The rvalue can only be 0, 1, 2 or 3");
                    }
                }
            }*/

            public RotBone(float length, float speed, float rotate, bool way, int pointtype)
            {
                this.rotate = rotate;
                this.length = length;
                this.speed = speed;
                this.way = way;
                this.pointtype = pointtype;
            }
            //new Vector2(Cos(90 - rotate+90*Poin) * length / 2, -Sin(90 - rotate) * length / 2)+new Vector2(length / Tan(rotate)* Cos(rotate), length / Tan(rotate)* Sin(rotate)));
            /*Vector2 vec = new Vector2(BoxStates.Left + length / Tan(rot) * Cos(rot), BoxStates.Down + length / Tan(rot) * Sin(rot));
            Vector2 vec2 = new Vector2(Cos(90 - rot) * length / 2, -Sin(90 - rot) * length / 2);
            CreateBone(new CustomBone(vec+vec2, Motions.PositionRoute.linear, rot, length)
            {
                PositionRouteParam = new float[] { Cos(30 - 180) * 2, Sin(30 - 180) * 2 },
                        IsMasked = true
                    });*/
            public override void Draw()
            {

            }
            int appeartime = 0;
            public override void Update()
            {
                appeartime++;
                if (appeartime == 1)
                {
                    if (pointtype == 0)
                        CreateBone(new CustomBone(
                            new Vector2(BoxStates.Left, BoxStates.Down)
                            + new Vector2(length / Tan(rotate) * Cos(rotate), length / Tan(rotate) * Sin(rotate))
                            + new Vector2(Cos(90 - rotate) * length / 2, -Sin(90 - rotate) * length / 2),
                            Motions.PositionRoute.linear,
                            rotate,
                            length)
                        { PositionRouteParam = new float[] { Cos(rotate - 180) * speed, Sin(rotate - 180) * speed } }
                            );
                    if (pointtype == 1)
                        CreateBone(new CustomBone(
                            new Vector2(BoxStates.Right, BoxStates.Down)
                            + new Vector2(-length / Tan(rotate + 90) * Cos(rotate + 90), -length / Tan(rotate + 90) * Sin(rotate + 90))
                            + new Vector2(Cos(90 - rotate + 90) * length / 2, -Sin(90 - rotate + 90) * length / 2),
                            Motions.PositionRoute.linear,
                            rotate + 90,
                            length)
                        { PositionRouteParam = new float[] { Cos(rotate - 180 + 90) * speed, Sin(rotate - 180 + 90) * speed } }
                            );
                    if (pointtype == 2)
                        CreateBone(new CustomBone(
                            new Vector2(BoxStates.Right, BoxStates.Up)
                            + new Vector2(length / Tan(rotate + 180) * Cos(rotate + 180), length / Tan(rotate + 180) * Sin(rotate + 180))
                            + new Vector2(Cos(90 - rotate + 180) * length / 2, -Sin(90 - rotate + 180) * length / 2),
                            Motions.PositionRoute.linear,
                            rotate + 180,
                            length)
                        { PositionRouteParam = new float[] { Cos(rotate - 180 + 180) * speed, Sin(rotate - 180 + 180) * speed } }
                            );
                    if (pointtype == 3)
                        CreateBone(new CustomBone(
                            new Vector2(BoxStates.Left, BoxStates.Up)
                            + new Vector2(-length / Tan(rotate + 270) * Cos(rotate + 270), -length / Tan(rotate + 270) * Sin(rotate + 270))
                            + new Vector2(Cos(90 - rotate + 270) * length / 2, -Sin(90 - rotate + 270) * length / 2),
                            Motions.PositionRoute.linear,
                            rotate + 270,
                            length)
                        { PositionRouteParam = new float[] { Cos(rotate - 180 + 270) * speed, Sin(rotate - 180 + 270) * speed } }
                            );
                }
            }
        }
    }
    public class FakeNote
    {
        public class LeftNote : Entity
        {
            Func<ICustomMotion, Vector2> Eases;
            float EasesTime = 0;
            float Delay = 0;
            float Speed = 0;

            public LeftNote(float delay, float speed, int color, int type, Func<ICustomMotion, Vector2> eases, float easestime)
            {
                Image = Sprites.arrow[color, type, 0];
                Delay = delay;
                Eases = eases;
                Speed = speed;
                Centre = new(Heart.Centre.X - (Delay * Speed + 42), Heart.Centre.Y);
                Rotation = 180;
                EasesTime = easestime;
            }
            int Timer = 0;
            Vector2 centre;
            public Vector2 Offset;
            public override void Update()
            {
                Depth = 0.99f;
                Timer++;
                if (Timer == 1)
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(Heart.Centre.X - (Delay * Speed + 42) + Offset.X, Heart.Centre.Y + Offset.Y));
                    ce.Insert(Delay, CentreEasing.Linear(Speed));
                    ce.Insert(EasesTime, Eases);
                    ce.Run((s) => { Centre = s; });
                }
                centre = Centre;
            }

            public override void Draw()
            {
                FormalDraw(Image, Centre, Color.White, Rotation / 180 * MathF.PI, ImageCentre);
            }
        }
        public class RightNote : Entity
        {
            Func<ICustomMotion, Vector2> Eases;
            float EasesTime = 0;
            float Delay = 0;
            float Speed = 0;

            public RightNote(float delay, float speed, int color, int type, Func<ICustomMotion, Vector2> eases, float easestime)
            {
                Image = Sprites.arrow[color, type, 0];
                Delay = delay;
                Eases = eases;
                Speed = speed;
                Centre = new(Heart.Centre.X + (Delay * Speed + 42), Heart.Centre.Y);
                Rotation = 0;
                EasesTime = easestime;
            }
            int Timer = 0;
            Vector2 centre;
            public Vector2 Offset;
            public override void Update()
            {
                Depth = 0.99f;
                Timer++;
                if (Timer == 1)
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(Heart.Centre.X + (Delay * Speed + 42) + Offset.X, Heart.Centre.Y + Offset.Y));
                    ce.Insert(Delay, CentreEasing.Linear(-Speed));
                    ce.Insert(EasesTime, Eases);
                    ce.Run((s) => { Centre = s; });
                }
                centre = Centre;
            }

            public override void Draw()
            {
                FormalDraw(Image, Centre, Color.White, Rotation / 180 * MathF.PI, ImageCentre);
            }
        }
    }
    public static class DrawingUtil
    {
        /// <summary>
        /// 交叉骨头的生成方法之一（角度之差均等）
        /// </summary>
        /// <param name="start">骨头开始的位置（废话</param>
        /// <param name="speed">骨头向x和y方向移动的速度</param>
        /// <param name="length">骨头长度（废话</param>
        /// <param name="num">生成骨头的数量</param>
        public static void CrossBone(Vector2 start, Vector2 speed, float length, float num)
        {
            for (int i = 0; i < num; i++)
            {
                CreateBone(new CustomBone(new(start.X, start.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, 114514 },
                    RotationRouteParam = new float[] { 4, (180 / num) * i },
                });
            }
        }
        /// <summary>
        /// 交叉骨头的生成方法之二（角度之差均等）
        /// </summary>
        /// <param name="start">骨头开始的位置（废话</param>
        /// <param name="speed">骨头向x和y方向移动的速度</param>
        /// <param name="length">骨头长度（废话</param>
        /// <param name="num">生成骨头的数量</param>
        /// <param name="color">生成骨头的颜色（只能写0，1，2）</param>
        public static void CrossBone(Vector2 start, Vector2 speed, float length, float num, int color)
        {
            for (int i = 0; i < num; i++)
            {
                CreateBone(new CustomBone(new(start.X, start.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, 114514 },
                    RotationRouteParam = new float[] { 4, (180 / num) * i },
                    ColorType = color
                });
            }
        }
        public static void CrossBone(Vector2 start, Vector2 speed, float length, float num, int color, float RotSpeed)
        {
            for (int i = 0; i < num; i++)
            {
                CreateBone(new CustomBone(new(start.X, start.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, 114514 },
                    RotationRouteParam = new float[] { RotSpeed, (180 / num) * i },
                    ColorType = color
                });
            }
        }
        public static void LineShadow(int times, Line line)
        {
            for (int i = 0; i < times; i++)
            {
                int t = i;
                line.InsertRetention(new(t * 0.5f, 0.24f - 0.24f / times * t));

            }
        }
        public static void LineShadow(float deep, int times, Line line)
        {
            for (int i = 0; i < times; i++)
            {
                int t = i;
                line.InsertRetention(new(t * 0.5f, deep - deep / times * t));
            }
        }
        public static void LineShadow(float delay, float deep, int times, Line line)
        {
            for (int i = 0; i < times; i++)
            {
                int t = i;
                line.InsertRetention(new(t * delay, deep - deep / times * t));
            }
        }
        public static void SetScreenScale(float size, float duration)
        {
            float start = ScreenDrawing.ScreenScale;
            float end = size;
            float del = start - end;
            float t = 0;
            AddInstance(new TimeRangedEvent(0, duration, () =>
            {
                float x = t / (duration - 1);
                float f = 2 * x - x * x;
                ScreenDrawing.ScreenScale = start - del * f;
                t++;
            }));
        }
        public static void MinusScreenScale(float MaxSize, float time)
        {
            float start = ScreenDrawing.ScreenScale;
            float end = start - MaxSize;
            float del = start - end;
            float t = 0;
            AddInstance(new TimeRangedEvent(0, time / 2f, () =>
            {
                float x = t / (time / 2f - 1);
                float f = 2 * x - x * x;
                ScreenDrawing.ScreenScale = start - del * f;
                t++;
            }));
            float t2 = 0;
            float start2 = start - MaxSize;
            float end2 = start;
            float del2 = start2 - end2;
            AddInstance(new TimeRangedEvent(time / 2f, time / 2f, () =>
            {
                float x = t2 / (time / 2f - 1);
                float f = x * x;
                ScreenDrawing.ScreenScale = start2 - del2 * f;
                t2++;
            }));
        }
        public static void ScreenAngle(float angle, float time)
        {
            float start = ScreenDrawing.ScreenAngle;
            float end = angle;
            float del = start - end;
            float t = 0;
            AddInstance(new TimeRangedEvent(0, time, () =>
            {
                float x = t / (time - 1);
                float f = 2 * x - x * x;
                ScreenDrawing.ScreenAngle = start - del * f;
                t++;
            }));
        }
        public static void PlusRotate(float MaxAngle, float time)
        {
            float start = ScreenDrawing.ScreenAngle;
            float end = start + MaxAngle;
            float del = start - end;
            float t = 0;
            AddInstance(new TimeRangedEvent(0, time / 2f, () =>
            {
                float x = t / (time / 2f);
                float f = 2 * x - x * x;
                ScreenDrawing.ScreenAngle = start - del * f;
                t++;
            }));
            float t2 = 0;
            float start2 = start + MaxAngle;
            float end2 = start;
            float del2 = start2 - end2;
            AddInstance(new TimeRangedEvent(time / 2f + 1, time / 2f, () =>
            {
                float x = t2 / (time / 2f);
                float f = x * x;
                ScreenDrawing.ScreenAngle = start2 - del2 * f;
                t2++;
            }));
        }
        public static void PlusScreenScale(float MaxSize, float time)
        {
            time = (int)time;
            float start = ScreenDrawing.ScreenScale;
            float end = start + MaxSize;
            float del = start - end;
            float t = 0;
            AddInstance(new TimeRangedEvent(0, time / 2f, () =>
            {
                float x = t / (time / 2f);
                float f = 2 * x - x * x;
                ScreenDrawing.ScreenScale = start - del * f;
                t++;
            }));
            float t2 = 0;
            float start2 = start + MaxSize;
            float end2 = start;
            float del2 = start2 - end2;
            AddInstance(new TimeRangedEvent(time / 2f + 1, time / 2f, () =>
            {
                float x = t2 / (time / 2f);
                float f = x * x;
                ScreenDrawing.ScreenScale = start2 - del2 * f;
                t2++;
            }));
        }
        public static void Shock()
        {
            for (int a = 0; a < 4; a++)
            {
                AddInstance(new TimeRangedEvent(a * 2f, 1, () =>
                { ScreenDrawing.ScreenPositionDetla = new Vector2(Rand(-2f, 2f), Rand(-2f, 2f)); }
                ));
                AddInstance(new TimeRangedEvent((a + 1) * 2f, 1, () =>
                { ScreenDrawing.ScreenPositionDetla = new Vector2(0, 0); }
));
            }
        }
        public static void Shock(float interval, float range)
        {
            for (int a = 0; a < 5; a++)
            {
                AddInstance(new TimeRangedEvent(a * interval, 1, () =>
                { ScreenDrawing.ScreenPositionDetla = new Vector2(Rand(-range, range), Rand(-range, range)); }
                ));
                AddInstance(new TimeRangedEvent((a + 1) * interval, 1, () =>
                { ScreenDrawing.ScreenPositionDetla = new Vector2(0, 0); }
));
            }
        }
        public static void Shock(float interval, float range, float times)
        {
            for (int a = 0; a < times; a++)
            {
                AddInstance(new TimeRangedEvent(a * interval, 1, () =>
                { ScreenDrawing.ScreenPositionDetla = new Vector2(Rand(-range, range), Rand(-range, range)); }
                ));
                AddInstance(new TimeRangedEvent((a + 1) * interval, 1, () =>
                {
                    ScreenDrawing.ScreenPositionDetla = new Vector2(0, 0);
                }
                ));
            }
        }
        public static void Shock(float interval, float rangeX, float rangeY, float times)
        {
            for (int a = 0; a < times; a++)
            {
                AddInstance(new TimeRangedEvent(a * interval, 1, () =>
                { ScreenDrawing.ScreenPositionDetla = new Vector2(Rand(-rangeX, rangeX), Rand(-rangeY, rangeY)); }
                ));
                AddInstance(new TimeRangedEvent((a + 1) * interval, 1, () =>
                {
                    ScreenDrawing.ScreenPositionDetla = new Vector2(0, 0);
                }
                ));
            }
        }
        public static void Rain(float speed, float rotate, bool way)
        {
            float a = way ? -45 : 45;
            Linerotatelong rain = new(Rand(-200, 860), a, rotate + 270 + Rand(-2.5f, 2.5f), 180, Rand(0.2f, 0.4f), Rand(9, 55), Color.White)
            {
                width = Rand(2, 4)
            };
            if (Rand(1, 3) == 1)
            {
                CreateEntity(rain);
            }
            else
            {
                for (int b = 0; b < 2; b++) CreateEntity(rain);
            }
            AddInstance(new TimeRangedEvent(0, 180, () =>
            {
                rain.xCenter += Cos(rotate + 90) * speed;
                rain.yCenter += Sin(rotate + 90) * speed;
            }));
        }
        public static void BlackScreen(float inDuration, float duration, float outDuration)
        {
            if (inDuration == 0)
            {
                MaskSquare maskSquare = new(0, 0, 640, 480, (int)(inDuration + duration + outDuration), Color.Black, 1);
                CreateEntity(maskSquare);
                AddInstance(new TimeRangedEvent(duration, outDuration + 1, () =>
                {
                    maskSquare.alpha -= 1 / outDuration;
                }));
            }
            else if (inDuration <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(inDuration), string.Format("the parameter {0} must be greater than 0.", nameof(inDuration)));
            }
            else
            {
                MaskSquare maskSquare = new(0, 0, 640, 480, (int)(inDuration + duration + outDuration), Color.Black, 0);
                CreateEntity(maskSquare);
                AddInstance(new TimeRangedEvent(inDuration + 1, () =>
                {
                    maskSquare.alpha += 1 / inDuration;
                }));
                AddInstance(new TimeRangedEvent(inDuration + 1 + duration, outDuration + 1, () =>
                {
                    maskSquare.alpha -= 1 / outDuration;
                }));
            }
        }
        public static void BetterBlackScreen(float induration, float duration, float outduration, Color color)
        {
            if (induration == 0)
            {
                MaskSquare maskSquare = new(0, 0, 640, 480, (int)(induration + duration + outduration), color, 1);
                CreateEntity(maskSquare);
                AddInstance(new TimeRangedEvent(duration, outduration + 1, () =>
                {
                    maskSquare.alpha -= 1 / outduration;
                }));
            }
            else if (induration <= 0)
            {
                throw new NotImplementedException();

            }
            else
            {
                MaskSquare maskSquare = new(0, 0, 640, 480, (int)(induration + duration + outduration), color, 0);
                CreateEntity(maskSquare);
                AddInstance(new TimeRangedEvent(induration + 1, () =>
                {
                    maskSquare.alpha += 1 / induration;
                }));
                AddInstance(new TimeRangedEvent(induration + 1 + duration, outduration + 1, () =>
                {
                    maskSquare.alpha -= 1 / outduration;
                }));
            }
        }
        public static void RotateWithBack(float duration, float range)
        {
            ScreenDrawing.CameraEffect.RotateTo(range, duration / 2);
            AddInstance(new InstantEvent(duration / 2 + 1, () =>
            {
                ScreenDrawing.CameraEffect.RotateTo(0, duration / 2 - 1);
            }));
        }
        public static void RotateSymmetricBack(float duration, float range)
        {
            ScreenDrawing.CameraEffect.RotateTo(range, duration / 3);
            AddInstance(new InstantEvent(duration / 3 + 1, () =>
            {
                ScreenDrawing.CameraEffect.RotateTo(-range, duration / 3 - 1);
            }));
            AddInstance(new InstantEvent(duration / 3 * 2 + 1, () =>
            {
                ScreenDrawing.CameraEffect.RotateTo(0, duration / 3 - 1);
            }));
        }
        public static void LerpGreenBox(float duration, Vector2 getto, float lerpcount)
        {
            Vector2 c = BoxStates.Centre;
            Vector2 h = Heart.Centre;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                InstantSetBox(BoxStates.Centre * (1 - lerpcount) + getto * lerpcount, 84, 84);
                InstantTP(Heart.Centre * (1 - lerpcount) + getto * lerpcount);
            }));
        }
        public static void LerpScreenPos(float duration, Vector2 getto, float lerpcount)
        {
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                ScreenDrawing.ScreenPositionDetla = ScreenDrawing.ScreenPositionDetla * (1 - lerpcount) + getto * lerpcount;
            }));
        }
        public static void LerpScreenScale(float duration, float getto, float lerpcount)
        {
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * (1 - lerpcount) + getto * lerpcount;
            }));
        }
        /*public static void SinGreenBox(float duration, Vector2 range)
        {
            float sin = 0;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                sin += 1 / duration;
                InstantSetBox(Sin(sin)*range)
            }));
        }*/
        public static void LerpNormalBox(float duration, Vector2 getto, float lerpcount)
        {
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                InstantSetBox(BoxStates.Centre * (1 - lerpcount) + getto * lerpcount, BoxStates.Width, BoxStates.Height);
            }));
        }
        public class MaskSquare : Entity
        {
            public float duration = 0;
            public float LeftUpX = 0;
            public float LeftUpY = 0;
            public float width = 0;
            public float height = 0;
            public Color color = Color.White;
            public MaskSquare(float LeftUpX, float LeftUpY, float width, float height, float duration, Color color, float alpha)
            {
                this.LeftUpX = LeftUpX;
                this.LeftUpY = LeftUpY;
                this.width = width;
                this.height = height;
                this.duration = duration;
                this.color = color;
                this.alpha = alpha;
                Depth = 0.99f;
            }
            public float alpha = 1;
            public float time = 0;
            public float speed = 1;
            public override void Draw()
            {
                FormalDraw(Sprites.pixUnit, new CollideRect(LeftUpX, LeftUpY, width, height).ToRectangle(), color * alpha);
                Depth = 0.99f;

            }
            public override void Update()
            {
                time++;
                if (time == duration)
                {
                    Dispose();
                }

            }
        }
        public class SpecialBox : RectangleBox
        {
            public float duration = 0;
            public float width = 84 - 2;
            public float height = 84 - 2;
            public float rotate = 0;
            public SpecialBox(float duration, float rotate, Player.Heart p) : base(p)
            {
                this.duration = duration;
                this.rotate = rotate;
                collidingBox = new CollideRect(0, 0, 40, 40);
            }
            public SpecialBox(CollideRect Area) : base(Area)
            {
                collidingBox = Area;
            }
            public float alpha = 1;
            public int time = 0;
            public float speed = 1;
            public override void Draw()
            {
                /*FormalDraw(Sprites.pixiv,
                    new Vector2(Heart.Centre.X + width / 2, Heart.Centre.Y + height / 2),
                    new CollideRect(Heart.Centre.X, Heart.Centre.Y, width, height).ToRectangle(),
                    Color.Black,
                    1,
                    rotate,
                    new Vector2(Heart.Centre.X + width/2, Heart.Centre.Y + height/2)
                    );*/
                for (int a = 0; a < 4; a++)
                    DrawingLab.DrawLine(Heart.Centre + MathUtil.GetVector2(MathF.Sqrt(42 * 42 * 2), 45 + rotate + a * 90), Heart.Centre + MathUtil.GetVector2(MathF.Sqrt(42 * 42 * 2), 45 + rotate + 90 + a * 90), 4.2f, Color.White * 0.5f, 0.99f);
            }
            public override void Update()
            {
                time++;
                if (time == duration)
                {
                    Dispose();
                }
            }

            public override void MoveTo(object v)
            {

            }

            public override void InstanceMove(object v)
            {

            }
        }
        public class NormalLine : Entity
        {
            public float duration = 0;
            public float x1 = 0;
            public float y1 = 0;
            public float x2 = 0;
            public float y2 = 0;
            public float width = 3;
            public Color color = Color.White;
            public NormalLine(float x1, float y1, float x2, float y2, float duration, float alpha, Color color)
            {
                this.x1 = x1;
                this.y1 = y1;
                this.x2 = x2;
                this.y2 = y2;
                this.duration = duration;
                this.alpha = alpha;
                this.color = color;
            }
            public NormalLine(float x1, float y1, float x2, float y2, float duration, float alpha) : this(x1, y1, x2, y2, duration, alpha, Color.White) { }
            public NormalLine(Vector2 pos1, Vector2 pos2, float duration, float alpha) : this(pos1.X, pos1.Y, pos1.X, pos1.Y, duration, alpha, Color.White) { }
            public NormalLine(Vector2 pos1, Vector2 pos2, float duration, float alpha, Color color)
            {
                x1 = pos1.X;
                y1 = pos1.Y;
                x2 = pos2.X;
                y2 = pos2.Y;
                this.duration = duration;
                this.alpha = alpha;
                this.color = color;
            }
            public float alpha = 1;
            public int time = 0;
            public float speed = 1;
            public float depth = 0.99f;
            public override void Draw()
            {
                DrawingLab.DrawLine(new(x1, y1), new(x2, y2), width, color * alpha, depth);
                Depth = depth;
            }
            public override void Update()
            {
                time++;
                if (time >= duration)
                {
                    Dispose();
                }

            }
        }
        public class Linerotate : Entity
        {
            public float duration = 0;
            public float xCenter = 0;
            public float yCenter = 0;
            public float rotate = 0;
            public float width = 4;
            public Color color = Color.White;
            public Linerotate(float xCenter, float yCenter, float rotate, float duration, float alpha, Color color)
            {
                this.xCenter = xCenter;
                this.yCenter = yCenter;
                this.rotate = rotate;
                this.duration = duration;
                this.alpha = alpha;
                this.color = color;
            }
            public Linerotate(float xCenter, float yCenter, float rotate, float duration, float alpha) : this(xCenter, yCenter, rotate, duration, alpha, Color.White) { }
            public float alpha = 1;
            public int time = 0;
            public float speed = 1;
            public float depth = 0.2f;
            public override void Draw()
            {
                if (rotate % 180 != 0)
                    DrawingLab.DrawLine(new(xCenter - (1f / Tan(rotate)) * yCenter, 0), new(xCenter + (1f / Tan(rotate)) * (480 - yCenter), 480), width, color * alpha, depth);
                else
                    DrawingLab.DrawLine(new(0, yCenter), new(640, yCenter), width, color * alpha, depth);
                Depth = 0.2f;
            }

            public override void Update()
            {
                time++;
                if (time >= duration)
                {
                    Dispose();
                }

            }
        }
        public static void CreateTagLine(Linerotate Line, string[] Tags)
        {
            CreateEntity(Line);
            Line.Tags = Tags;
        }
        public static void CreateTagLine(Linerotate Line, string Tag)
        {
            CreateEntity(Line);
            Line.Tags = new string[] { Tag };
        }
        public static void CreateTagLine(Linerotatelong Line, string Tag)
        {
            CreateEntity(Line);
            Line.Tags = new string[] { Tag };
        }
        public static void CreateTagLine(NormalLine Line, string[] Tags)
        {
            CreateEntity(Line);
            Line.Tags = Tags;
        }
        public static void CreateTagLine(NormalLine Line, string Tag)
        {
            CreateEntity(Line);
            Line.Tags = new string[] { Tag };
        }
        public class Linerotatelong : Entity
        {
            public float duration = 0;
            public float xCenter = 0;
            public float yCenter = 0;
            public float rotate = 0;
            public float length = 0;
            public float width = 4;
            public Color color = Color.White;
            public Linerotatelong(float xCenter, float yCenter, float rotate, float duration, float alpha, float length, Color color)
            {
                this.xCenter = xCenter;
                this.yCenter = yCenter;
                this.rotate = rotate;
                this.duration = duration;
                this.alpha = alpha;
                this.length = length;
                this.color = color;
            }
            public float alpha = 1;
            public int time = 0;
            public float speed = 1;
            public override void Draw()
            {
                DrawingLab.DrawLine(
                    new Vector2(xCenter, yCenter),
                    new Vector2(
                        xCenter + Cos(rotate) * length,
                        yCenter + Sin(rotate) * length),
                        width,
                        color * alpha,
                        0.99f);
                Depth = 0.99f;
            }

            public override void Update()
            {
                time++;
                if (time == duration)
                {
                    Dispose();
                }

            }
        }
        public class SolidPolygon : Entity
        {
            public int accuracy = 0;
            public float duration = 0;
            public float xCenter = 0;
            public float yCenter = 0;
            public float radius = 0;
            public float rotate = 0;
            public float rotatec = 0;
            public Color color = Color.White;
            public SolidPolygon(float xCenter, float yCenter, float radius, float duration, float alpha, float rotate, float rotatec, Color color, int accuracy)
            {
                this.xCenter = xCenter;
                this.yCenter = yCenter;
                this.duration = duration;
                this.alpha = alpha;
                this.color = color;
                this.accuracy = accuracy;
                this.radius = radius;
                this.rotate = rotate;
                this.rotatec = rotatec;
            }
            public float alpha = 1;
            public int time = 0;
            public float speed = 1;
            public override void Draw()
            {

                for (int i = 0; i < accuracy; i += 1)
                {
                    int rotateb = accuracy * 180 - 360;
                    DrawingLab.DrawLine(
                        new Vector2(xCenter + Cos(rotateb / accuracy * i + rotatec) * radius + Cos(rotate) * radius,
                                    yCenter + Sin(rotateb / accuracy * i + rotatec) * radius + Sin(rotate) * radius),
                        new Vector2(xCenter + Cos(rotateb / accuracy * (i + 1) + rotatec) * radius + Cos(rotate) * radius,
                                    yCenter + Sin(rotateb / accuracy * (i + 1) + rotatec) * radius + Sin(rotate) * radius),//rotatec是绕点旋转，rotate是绕中心旋转
                        4,
                        color * alpha,
                        0.99f);
                    Depth = 0.99f;
                }
            }

            public override void Update()
            {
                time++;
                if (time == duration)
                {
                    Dispose();
                }

            }
        }
        public class HollowPolygon : Entity
        {
            public int accuracy = 0;
            public float duration = 0;
            public float xCenter = 0;
            public float yCenter = 0;
            public float radius = 0;
            public Color color = Color.White;
            public HollowPolygon(float xCenter, float yCenter, float radius, float duration, float alpha, Color color, int accuracy)
            {
                this.xCenter = xCenter;
                this.yCenter = yCenter;
                this.duration = duration;
                this.alpha = alpha;
                this.color = color;
                this.accuracy = accuracy;
                this.radius = radius;
            }
            public float alpha = 1;
            public int time = 0;
            public float speed = 1;
            public override void Draw()
            {
                for (int i = 0; i < accuracy; i += 1)
                {
                    int rotate = 360;
                    DrawingLab.DrawLine(
                        new Vector2(xCenter + Cos(rotate / accuracy * i) * radius,
                                    yCenter + Sin(rotate / accuracy * i) * radius),
                        new Vector2(xCenter + Cos(rotate / accuracy * (i + 1)) * radius,
                                    yCenter + Sin(rotate / accuracy * (i + 1)) * radius),
                        4,
                        color * alpha,
                        0.99f);
                    Depth = 0.99f;
                }
            }

            public override void Update()
            {
                time++;
                if (time == duration)
                {
                    Dispose();
                }

            }
        }
    }
    public static class LineMoveLibrary
    {
        /// <summary>
        /// 线段的Alpha-Sin,该重载可以设置Sin的初始频率
        /// </summary>
        public static void AlphaSin(Linerotate Line, float duration, float range, float frequency, float startfrequency)
        {
            float sin = startfrequency;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.alpha = Sin(sin) * range;
                sin += frequency / (duration - 1);
            }));
        }
        /// <summary>
        /// 线段的Alpha-Sin,该重载可以设置初始幅度
        /// </summary>
        public static void AlphaSin(Linerotate Line, float duration, float range, float startrange, float frequency, float startfrequency)
        {
            float sin = startfrequency;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.alpha = startrange + Sin(sin) * range;
                sin += frequency / (duration - 1);
            }));
        }
        public static void AlphaSin(NormalLine Line, float duration, float range, float startrange, float frequency, float startfrequency)
        {
            float sin = startfrequency;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.alpha = startrange + Sin(sin) * range;
                sin += frequency / (duration);
            }));
        }
        /// <summary>
        /// 线段的Alpha-Sin,该重载表示经过duration时间闪烁一次(alpha=1)
        /// </summary>
        public static void AlphaSin(Linerotate Line, float duration)
        {
            float sin = 0;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.alpha = Sin(sin) * 1;
                sin += 360 / (duration);
            }));
        }
        public static void AlphaSin(NormalLine Line, float duration)
        {
            float sin = 0;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.alpha = Sin(sin) * 1;
                sin += 360 / (duration);
            }));
        }
        /// <summary>
        /// 线段的Alpha-Sin,该重载可以设置幅度
        /// </summary>
        public static void AlphaSin(Linerotate Line, float duration, float range)
        {
            float sin = 0;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.alpha = Sin(sin) * range;
                sin += 360 / (duration - 1);
            }));
        }
        /// <summary>
        /// 线段的Alpha-Sin,该重载可以设置频率
        /// </summary>
        public static void AlphaSin(Linerotate Line, float duration, float range, float frequency)
        {
            float sin = 0;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.alpha = Sin(sin) * range;
                sin += frequency / (duration - 1);
            }));
        }
        /// <summary>
        /// 线段的Alpha-Lerp缓动,count为目标的lerp插值
        /// </summary>
        public static void AlphaLerp(Linerotate Line, float duration, float lerpto, float count)
        {
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.alpha = lerpto * count + Line.alpha * (1 - count);
            }));
        }
        /// <summary>
        /// 线段的Alpha-二次方,该函数为增加range的alpha
        /// </summary>
        public static void AlphaSquare(Linerotate Line, float duration, float range)
        {
            float speed = 0;
            float realrange = MathF.Sqrt(range);
            float count = Line.alpha;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.alpha = count + realrange * realrange * speed;
                speed += 1 / (duration - 1);
            }));
        }
        /// <summary>
        /// 线段的Alpha-二次方,该函数为到达range的alpha
        /// </summary>
        public static void AlphaSquareTo(Linerotate Line, float duration, float range)
        {
            float speed = 0;
            float realrange = Line.alpha - MathF.Sqrt(range);
            float count = Line.alpha;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.alpha = count + realrange * realrange * speed;
                speed += 1 / (duration - 1);
            }));
        }
        /// <summary>
        /// 提供一些线段有的Tag,赋予这些线段的Alpha以二次方缓动
        /// </summary>
        public static void AlphaSquare(string LineTags, float duration, float range)
        {
            Linerotate[] Line = GetAll<Linerotate>(LineTags);
            if (Line.Length == 0)
            {
                for (int a = 0; a < Line.Length; a++)
                {
                    NormalLine[] L = GetAll<NormalLine>(LineTags);
                    int x = a;
                    float speed = 0;
                    float realrange = MathF.Sqrt(range);
                    float count = L[x].alpha;
                    AddInstance(new TimeRangedEvent(duration, () =>
                    {
                        L[x].alpha = count + realrange * realrange * speed;
                        speed += 1 / (duration - 1);
                    }));
                }
            }
            for (int a = 0; a < Line.Length; a++)
            {
                int x = a;
                float speed = 0;
                float realrange = MathF.Sqrt(range);
                float count = Line[x].alpha;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    Line[x].alpha = count + realrange * realrange * speed;
                    speed += 1 / (duration - 1);
                }));
            }
        }
        /// <summary>
        /// 提供一些线段有的Tag,赋予这些线段的Alpha以二次方缓动到达目标
        /// </summary>
        public static void AlphaSquareTo(string LineTags, float duration, float range)
        {
            Linerotate[] Line = GetAll<Linerotate>(LineTags);
            for (int a = 0; a < Line.Length; a++)
            {
                int x = a;
                float speed = 0;
                float realrange = Line[x].alpha - MathF.Sqrt(range);
                float count = Line[x].alpha;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    Line[x].alpha = count + realrange * realrange * speed;
                    speed += 1 / (duration - 1);
                }));
            }
        }
        /// <summary>
        /// 提供一些线段有的Tag,赋予这些线段的Alpha以Sin缓动
        /// </summary>
        public static void AlphaSin(string LineTags, float duration, float range, float startrange, float frequency, float startfrequency)
        {

            Linerotate[] Line = GetAll<Linerotate>(LineTags);
            NormalLine[] L = GetAll<NormalLine>(LineTags);
            for (int a = 0; a < L.Length; a++)
            {
                int x = a;
                float speed = startfrequency;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    L[x].alpha = startrange + Sin(speed) * range;
                    speed += 360 / frequency;
                }));
            }

            for (int a = 0; a < Line.Length; a++)
            {
                int x = a;
                float sin = startfrequency;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    Line[x].alpha = startrange + Sin(sin) * range;
                    sin += 360 / frequency;
                }));
            }
        }
        public static void AlphaSin(string LineTags, float duration)
        {
            Linerotate[] Line = GetAll<Linerotate>(LineTags);
            NormalLine[] L = GetAll<NormalLine>(LineTags);
            float sin = 0;
            for (int a = 0; a < Line.Length; a++)
            {
                int x = a;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    Line[x].alpha = Sin(sin) * 1;
                    sin += 360 / (duration);
                }));
            }
            float sin1 = 0;
            for (int a = 0; a < L.Length; a++)
            {
                int x = a;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    L[x].alpha = Sin(sin1) * 1;
                    sin1 += 360 / (duration);
                }));
            }
        }
        /// <summary>
        /// 提供一些线段有的Tag,赋予这些线段的Alpha以Lerp缓动
        /// </summary>
        public static void AlphaLerp(string LineTags, float duration, float lerpto, float count)
        {
            Linerotate[] Line = GetAll<Linerotate>(LineTags);
            for (int a = 0; a < Line.Length; a++)
            {
                int x = a;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    Line[x].alpha = lerpto * count + Line[x].alpha * (1 - count);
                }));
            }
        }
        public static void LAlphaLerp(string LineTags, float duration, float lerpto, float count)
        {
            Linerotatelong[] Line = GetAll<Linerotatelong>(LineTags);
            for (int a = 0; a < Line.Length; a++)
            {
                int x = a;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    Line[x].alpha = lerpto * count + Line[x].alpha * (1 - count);
                }));
            }
        }
        /// <summary>
        /// 线段的Vector2-Sin,该重载可以设置Sin的初始频率
        /// </summary>
        public static void VecSin(Linerotate Line, float duration, Vector2 range, float frequency, float startfrequency)
        {
            float sin = startfrequency;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.xCenter = Sin(sin) * range.X;
                Line.yCenter = Sin(sin) * range.Y;
                sin += frequency / (duration - 1);
            }));
        }
        /// <summary>
        /// 线段的Vector2-Sin,该重载可以设置初始幅度
        /// </summary>
        public static void VecSin(Linerotate Line, float duration, Vector2 range, Vector2 startrange, float frequency, float startfrequency)
        {
            float sin = startfrequency;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.xCenter = startrange.X + Sin(sin) * range.X;
                Line.yCenter = startrange.Y + Sin(sin) * range.Y;
                sin += frequency / (duration - 1);
            }));
        }
        /// <summary>
        /// 线段的Vector2-Sin,该重载可以设置幅度
        /// </summary>
        public static void VecSin(Linerotate Line, float duration, Vector2 range)
        {
            float sin = 0;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.xCenter = Sin(sin) * range.X;
                Line.yCenter = Sin(sin) * range.Y;
                sin += 360 / (duration - 1);
            }));
        }
        /// <summary>
        /// 线段的Vector2-Sin,该重载可以设置频率
        /// </summary>
        public static void VecSin(Linerotate Line, float duration, Vector2 range, float frequency)
        {
            float sin = 0;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.xCenter = Sin(sin) * range.X;
                Line.yCenter = Sin(sin) * range.Y;
                sin += frequency / (duration - 1);
            }));
        }
        /// <summary>
        /// 线段的Vector2-Lerp缓动,count为目标的lerp插值
        /// </summary>
        public static void VecLerp(Linerotate Line, float duration, Vector2 lerpto, float count)
        {
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.xCenter = Line.xCenter * count + lerpto.X * (1 - count);
                Line.xCenter = Line.yCenter * count + lerpto.Y * (1 - count);
            }));
        }
        /// <summary>
        /// 线段的Vector2-二次方,该函数为增加range的alpha
        /// </summary>
        public static void VecSquare(Linerotate Line, float duration, Vector2 range)
        {
            float speed = 0;
            float rangeX = MathF.Sqrt(range.X);
            float rangeY = MathF.Sqrt(range.X);
            float count = Line.xCenter;
            float count1 = Line.yCenter;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.xCenter = count + rangeX * rangeX * speed;
                Line.yCenter = count1 + rangeY * rangeY * speed;
                speed += 1 / (duration - 1);
            }));
        }
        /// <summary>
        /// 线段的Vector2-二次方,该函数为到达range的alpha
        /// </summary>
        public static void VecSquareTo(Linerotate Line, float duration, Vector2 range)
        {
            float speed = 0;
            float rangeX = Line.xCenter - MathF.Sqrt(range.X);
            float rangeY = Line.yCenter - MathF.Sqrt(range.Y);
            float count = Line.xCenter;
            float count1 = Line.yCenter;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.xCenter = count + rangeX * rangeX * speed;
                Line.yCenter = count1 + rangeY * rangeY * speed;
                speed += 1 / (duration - 1);
            }));
        }
        /// <summary>
        /// 提供一些线段有的Tag,赋予这些线段的Vector2以Lerp缓动
        /// </summary>
        public static void VecLerp(string LineTags, float duration, Vector2 lerpto, float count)
        {
            Linerotate[] Line = GetAll<Linerotate>(LineTags);
            for (int a = 0; a < Line.Length; a++)
            {
                int x = a;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    Line[x].xCenter = Line[x].xCenter * (1 - count) + lerpto.X * (count);
                    Line[x].yCenter = Line[x].yCenter * (1 - count) + lerpto.Y * (count);
                }));
            }
        }
        public static void LVecLerp(string LineTags, float duration, Vector2 lerpto, float count)
        {
            Linerotatelong[] Line = GetAll<Linerotatelong>(LineTags);
            for (int a = 0; a < Line.Length; a++)
            {
                int x = a;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    Line[x].xCenter = Line[x].xCenter * (1 - count) + lerpto.X * (count);
                    Line[x].yCenter = Line[x].yCenter * (1 - count) + lerpto.Y * (count);
                }));
            }
        }
        /// <summary>
        /// 提供一些线段有的Tag,赋予这些线段的Vector2给一个Vec的速度并且这个速度是lerp缓动
        /// </summary>
        public static void VecLerpAdd(string LineTags, float duration, Vector2 speed, float count)
        {
            Linerotate[] Line = GetAll<Linerotate>(LineTags);
            for (int a = 0; a < Line.Length; a++)
            {
                int x = a;
                float addx = 0;
                float addy = 0;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    Line[x].xCenter += addx;
                    Line[x].yCenter += addy;
                    addx = addx * (1 - count) + speed.X * count;
                    addy = addy * (1 - count) + speed.Y * count;
                }));
            }
        }
        public static void VecLinear(string LineTags, float duration, Vector2 speed)
        {
            Linerotate[] Line = GetAll<Linerotate>(LineTags);
            for (int a = 0; a < Line.Length; a++)
            {
                int x = a;
                float addx = speed.X;
                float addy = speed.Y;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    Line[x].xCenter += addx;
                    Line[x].yCenter += addy;
                }));
            }
        }
        /// <summary>
        /// 提供一些线段有的Tag,赋予这些线段的Vector2以二次方缓动
        /// </summary>
        public static void VecSquare(string LineTags, float duration, Vector2 range)
        {
            Linerotate[] Line = GetAll<Linerotate>(LineTags);
            for (int a = 0; a < Line.Length; a++)
            {
                int x = a;
                float speed = 0;
                float rangeX = MathF.Sqrt(range.X);
                float rangeY = MathF.Sqrt(range.X);
                float count = Line[x].xCenter;
                float count1 = Line[x].yCenter;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    Line[x].xCenter = count + rangeX * rangeX * speed;
                    Line[x].yCenter = count1 + rangeY * rangeY * speed;
                    speed += 1 / (duration - 1);
                }));
            }
        }
        /// <summary>
        /// 提供一些线段有的Tag,赋予这些线段的Vector2以二次方缓动到目标
        /// </summary>
        public static void VecSquareTo(string LineTags, float duration, Vector2 range)

        {
            Linerotate[] Line = GetAll<Linerotate>(LineTags);
            for (int a = 0; a < Line.Length; a++)
            {
                int x = a;
                float speed = 0;
                float rangeX = Line[x].xCenter - MathF.Sqrt(range.X);
                float rangeY = Line[x].yCenter - MathF.Sqrt(range.Y);
                float count = Line[x].xCenter;
                float count1 = Line[x].yCenter;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    Line[x].xCenter = count + rangeX * rangeX * speed;
                    Line[x].yCenter = count1 + rangeY * rangeY * speed;
                    speed += 1 / (duration - 1);
                }));
            }
        }
        /// <summary>
        /// 提供一些线段有的Tag,赋予这些线段的Vector2以Sin缓动
        /// </summary>
        public static void VecSin(string LineTags, float duration, Vector2 range, Vector2 startrange, float frequency, float startfrequency)
        {
            Linerotate[] Line = GetAll<Linerotate>(LineTags);
            for (int a = 0; a < Line.Length; a++)
            {
                int x = a;
                float sin = startfrequency;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    Line[x].xCenter = startrange.X + Sin(sin) * range.X;
                    Line[x].yCenter = startrange.Y + Sin(sin) * range.Y;
                    sin += frequency / (duration - 1);
                }));
            }
        }
        /// <summary>
        /// 线段的Rotate-Sin,该重载可以设置Sin的初始频率
        /// </summary>
        public static void RotSin(Linerotate Line, float duration, float range, float frequency, float startfrequency)
        {
            float sin = startfrequency;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.rotate = Sin(sin) * range;
                sin += frequency / (duration - 1);
            }));
        }
        /// <summary>
        /// 线段的Rotate-Sin,该重载可以设置初始幅度
        /// </summary>
        public static void RotSin(Linerotate Line, float duration, float range, float startrange, float frequency, float startfrequency)
        {
            float sin = startfrequency;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.rotate = startrange + Sin(sin) * range;
                sin += frequency / (duration - 1);
            }));
        }
        /// <summary>
        /// 线段的Rotate-Sin,该重载可以设置幅度
        /// </summary>
        public static void RotSin(Linerotate Line, float duration, float range)
        {
            float sin = 0;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.rotate = Sin(sin) * range;
                sin += 360 / (duration - 1);
            }));
        }
        /// <summary>
        /// 线段的Rotate-Sin,该重载可以设置频率
        /// </summary>
        public static void RotSin(Linerotate Line, float duration, float range, float frequency)
        {
            float sin = 0;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.rotate = Sin(sin) * range;
                sin += frequency / (duration - 1);
            }));
        }
        /// <summary>
        /// 线段的Rotate-Lerp缓动,count为目标的lerp插值
        /// </summary>
        public static void RotLerp(Linerotate Line, float duration, float lerpto, float count)
        {
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.rotate = lerpto * count + Line.rotate * (1 - count);
            }));
        }
        /// <summary>
        /// 线段的Rotate-二次方,该函数为增加range的alpha
        /// </summary>
        public static void RotSquare(Linerotate Line, float duration, float range)
        {
            float speed = 0;
            float realrange = MathF.Sqrt(range);
            float count = Line.rotate;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.rotate = count + realrange * realrange * speed;
                speed += 1 / (duration - 1);
            }));
        }
        /// <summary>
        /// 线段的Rotate-二次方,该函数为到达range的alpha
        /// </summary>
        public static void RotSquareTo(Linerotate Line, float duration, float range)
        {
            float speed = 0;
            float realrange = Line.rotate - MathF.Sqrt(range);
            float count = Line.rotate;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.rotate = count + realrange * realrange * speed;
                speed += 1 / (duration - 1);
            }));
        }
        /// <summary>
        /// 提供一些线段有的Tag,赋予这些线段的Rotate以Sin缓动
        /// </summary>
        public static void RotSin(string LineTags, float duration, float range, float startrange, float frequency, float startfrequency)
        {
            Linerotate[] Line = GetAll<Linerotate>(LineTags);
            for (int a = 0; a < Line.Length; a++)
            {
                int x = a;
                float sin = startfrequency;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    Line[x].rotate = startrange + Sin(sin) * range;
                    sin += frequency / (duration - 1);
                }));
            }
        }
        /// <summary>
        /// 提供一些线段有的Tag,赋予这些线段的Rotate以Lerp缓动
        /// </summary>
        public static void RotLerp(string LineTags, float duration, float lerpto, float count)
        {
            Linerotate[] Line = GetAll<Linerotate>(LineTags);
            for (int a = 0; a < Line.Length; a++)
            {
                int x = a;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    Line[x].rotate = lerpto * count + Line[x].rotate * (1 - count);
                }));
            }
        }
        public static void LRotLerp(string LineTags, float duration, float lerpto, float count)
        {
            Linerotatelong[] Line = GetAll<Linerotatelong>(LineTags);
            for (int a = 0; a < Line.Length; a++)
            {
                int x = a;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    Line[x].rotate = lerpto * count + Line[x].rotate * (1 - count);
                }));
            }
        }
        /// <summary>
        /// 提供一些线段有的Tag,赋予这些线段的Rotate以二次方缓动
        /// </summary>
        public static void RotSquare(string LineTags, float duration, float range)
        {
            Linerotate[] Line = GetAll<Linerotate>(LineTags);
            for (int a = 0; a < Line.Length; a++)
            {
                int x = a;
                float speed = 0;
                float realrange = MathF.Sqrt(range);
                float count = Line[x].rotate;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    Line[x].rotate = count + realrange * realrange * speed;
                    speed += 1 / (duration - 1);
                }));
            }
        }
        /// <summary>
        /// 提供一些线段有的Tag,赋予这些线段的Rotate以二次方缓动到目标
        /// </summary>
        public static void RotSquareTo(string LineTags, float duration, float range)
        {
            Linerotate[] Line = GetAll<Linerotate>(LineTags);
            for (int a = 0; a < Line.Length; a++)
            {
                int x = a;
                float speed = 0;
                float realrange = Line[x].rotate - MathF.Sqrt(range);
                float count = Line[x].rotate;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    Line[x].rotate = count + realrange * realrange * speed;
                    speed += 1 / (duration - 1);
                }));
            }
        }
        /// <summary>
        /// 线段的Color-Sin,该重载可以设置Sin的初始频率
        /// </summary>
        public static void ColorSin(Linerotate Line, float duration, Color range, float frequency, float startfrequency)
        {
            float sin = startfrequency;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.color.R = (byte)(int)(Sin(sin) * range.R);
                Line.color.G = (byte)(int)(Sin(sin) * range.G);
                Line.color.B = (byte)(int)(Sin(sin) * range.B);
                sin += frequency / (duration - 1);
            }));
        }
        /// <summary>
        /// 线段的Color-Sin,该重载可以设置初始幅度
        /// </summary>
        public static void ColorSin(Linerotate Line, float duration, Color range, Color startrange, float frequency, float startfrequency)
        {
            float sin = startfrequency;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.color.R = (byte)(int)(startrange.R + Sin(sin) * range.R);
                Line.color.G = (byte)(int)(startrange.G + Sin(sin) * range.G);
                Line.color.B = (byte)(int)(startrange.B + Sin(sin) * range.B);
                sin += frequency / (duration - 1);
            }));
        }
        /// <summary>
        /// 线段的Color-Sin,该重载可以设置幅度
        /// </summary>
        public static void ColorSin(Linerotate Line, float duration, Color range)
        {
            float sin = 0;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.color.R = (byte)(int)(Sin(sin) * range.R);
                Line.color.G = (byte)(int)(Sin(sin) * range.G);
                Line.color.B = (byte)(int)(Sin(sin) * range.B);
                sin += 360 / (duration - 1);
            }));
        }
        /// <summary>
        /// 线段的Color-Sin,该重载可以设置频率
        /// </summary>
        public static void ColorSin(Linerotate Line, float duration, Color range, float frequency)
        {
            float sin = 0;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.color.R = (byte)(int)(Sin(sin) * range.R);
                Line.color.G = (byte)(int)(Sin(sin) * range.G);
                Line.color.B = (byte)(int)(Sin(sin) * range.B);
                sin += frequency / (duration - 1);
            }));
        }
        /// <summary>
        /// 线段的Color-Lerp缓动,count为目标的lerp插值
        /// </summary>
        public static void ColorLerp(Linerotate Line, float duration, Color lerpto, float count)
        {
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.color = Color.Lerp(Line.color, lerpto, count);
            }));
        }
        /// <summary>
        /// 线段的Color-二次方,该函数为增加range的alpha
        /// </summary>
        public static void ColorSquare(Linerotate Line, float duration, Color range)
        {
            float speed = 0;
            float rangeR = MathF.Sqrt(range.R);
            float rangeG = MathF.Sqrt(range.G);
            float rangeB = MathF.Sqrt(range.G);
            float R = Line.color.R;
            float G = Line.color.G;
            float B = Line.color.B;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.color.R = (byte)(int)(R + rangeR * rangeR * speed);
                Line.color.G = (byte)(int)(G + rangeG * rangeG * speed);
                Line.color.B = (byte)(int)(B + rangeB * rangeB * speed);
                speed += 1 / (duration - 1);
            }));
        }
        /// <summary>
        /// 线段的Color-二次方,该函数为到达range的alpha
        /// </summary>
        public static void ColorSquareTo(Linerotate Line, float duration, Color range)
        {
            float speed = 0;
            float rangeR = range.R - MathF.Sqrt(range.R);
            float rangeG = range.G - MathF.Sqrt(range.G);
            float rangeB = range.B - MathF.Sqrt(range.G);
            float R = Line.color.R;
            float G = Line.color.G;
            float B = Line.color.B;
            AddInstance(new TimeRangedEvent(duration, () =>
            {
                Line.color.R = (byte)(int)(R + rangeR * rangeR * speed);
                Line.color.G = (byte)(int)(G + rangeG * rangeG * speed);
                Line.color.B = (byte)(int)(B + rangeB * rangeB * speed);
                speed += 1 / (duration - 1);
            }));
        }
        /// <summary>
        /// 提供一些线段有的Tag,赋予这些线段的Color以Sin缓动
        /// </summary>
        public static void ColorSin(string LineTags, float duration, Color range, Color startrange, float frequency, float startfrequency)
        {
            Linerotate[] Line = GetAll<Linerotate>(LineTags);
            for (int a = 0; a < Line.Length; a++)
            {
                int x = a;
                float sin = startfrequency;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    Line[x].color.R = (byte)(int)(startrange.R + Sin(sin) * range.R);
                    Line[x].color.G = (byte)(int)(startrange.G + Sin(sin) * range.G);
                    Line[x].color.B = (byte)(int)(startrange.B + Sin(sin) * range.B);
                    sin += frequency / (duration - 1);
                }));
            }
        }
        /// <summary>
        /// 提供一些线段有的Tag,赋予这些线段的Color以Lerp缓动
        /// </summary>
        public static void ColorLerp(string LineTags, float duration, Color lerpto, float count)
        {
            Linerotate[] Line = GetAll<Linerotate>(LineTags);
            for (int a = 0; a < Line.Length; a++)
            {
                int x = a;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    Line[x].color = Color.Lerp(Line[x].color, lerpto, count);
                }));
            }
        }
        /// <summary>
        /// 提供一些线段有的Tag,赋予这些线段的Color以二次方缓动
        /// </summary>
        public static void ColorSquare(string LineTags, float duration, Color range)
        {
            Linerotate[] Line = GetAll<Linerotate>(LineTags);
            for (int a = 0; a < Line.Length; a++)
            {
                int x = a;
                float speed = 0;
                float rangeR = MathF.Sqrt(range.R);
                float rangeG = MathF.Sqrt(range.G);
                float rangeB = MathF.Sqrt(range.G);
                float R = Line[x].color.R;
                float G = Line[x].color.G;
                float B = Line[x].color.B;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    Line[x].color.R = (byte)(int)(R + rangeR * rangeR * speed);
                    Line[x].color.G = (byte)(int)(G + rangeG * rangeG * speed);
                    Line[x].color.B = (byte)(int)(B + rangeB * rangeB * speed);
                    speed += 1 / (duration - 1);
                }));
            }
        }
        /// <summary>
        /// 提供一些线段有的Tag,赋予这些线段的Color以二次方缓动到目标
        /// </summary>
        public static void ColorSquareTo(string LineTags, float duration, Color range)
        {
            Linerotate[] Line = GetAll<Linerotate>(LineTags);
            for (int a = 0; a < Line.Length; a++)
            {
                int x = a;
                float speed = 0;
                float rangeR = range.R - MathF.Sqrt(range.R);
                float rangeG = range.G - MathF.Sqrt(range.G);
                float rangeB = range.B - MathF.Sqrt(range.G);
                float R = Line[x].color.R;
                float G = Line[x].color.G;
                float B = Line[x].color.B;
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    Line[x].color.R = (byte)(int)(R + rangeR * rangeR * speed);
                    Line[x].color.G = (byte)(int)(G + rangeG * rangeG * speed);
                    Line[x].color.B = (byte)(int)(B + rangeB * rangeB * speed);
                    speed += 1 / (duration - 1);
                }));
            }
        }
    }
    public static class ShadowLibrary
    {
        public static void LineShadow(int times, Line line)
        {
            for (int i = 0; i < times; i++)
            {
                int t = i;
                line.InsertRetention(new(t * 0.5f, 0.24f - 0.24f / times * t));

            }
        }
        public static void LineShadow(float deep, int times, Line line)
        {
            for (int i = 0; i < times; i++)
            {
                int t = i;
                line.InsertRetention(new(t * 0.5f, deep - deep / times * t));

            }
        }
        public static void LineShadow(float delay, float deep, int times, Line line)
        {
            for (int i = 0; i < times; i++)
            {
                int t = i;
                line.InsertRetention(new(t * delay, deep - deep / times * t));

            }
        }
        public static void SetOffset(Arrow arrow, float offset)
        {
            if (arrow.Way == 0) arrow.Offset = new(0, offset);
            if (arrow.Way == 1) arrow.Offset = new(offset, 0);
            if (arrow.Way == 2) arrow.Offset = new(0, -offset);
            if (arrow.Way == 3) arrow.Offset = new(-offset, 0);
        }
        public static void SetOffset2(Arrow arrow, float offset)
        {
            if (arrow.Way == 0) arrow.Offset = new(0, offset);
            if (arrow.Way == 1) arrow.Offset = new(offset, 0);
            if (arrow.Way == 2) arrow.Offset = new(0, offset);
            if (arrow.Way == 3) arrow.Offset = new(offset, 0);
        }
    }
}