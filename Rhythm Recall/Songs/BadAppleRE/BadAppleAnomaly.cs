﻿using Extends;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.Remake;
using UndyneFight_Ex.SongSystem;
using static Rhythm_Recall.Waves.Determination.Game;
using static UndyneFight_Ex.Entities.Player;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing;
using static UndyneFight_Ex.Fight.AdvanceFunctions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;
using static Extends.DrawingUtil;
using UndyneFight_Ex.Remake.Entities;
using UndyneFight_Ex.Entities.Advanced;
using V = Microsoft.Xna.Framework.Vector2;
using V3 = Microsoft.Xna.Framework.Vector3;
using ES = UndyneFight_Ex.Entities.SimplifiedEasing.EaseState;
using C = Microsoft.Xna.Framework.Color;
using System.Threading.Tasks;
using UndyneFight_Ex.Remake.Components;
using UndyneFight_Ex.Remake.Texts;
using Microsoft.Xna.Framework.Media;
using UndyneFight_Ex.IO;
using Microsoft.Xna.Framework.Input;
using static Rhythm_Recall.Resources.BadAppleRE;
using static Rhythm_Recall.Resources;
using System.Linq.Expressions;
using static Rhythm_Recall.Waves.BadApple_RE;
using System.Runtime.Intrinsics;
namespace Rhythm_Recall.Waves
{
    internal partial class BadApple_RE : IChampionShip
    {

        partial class Game
        {

            void BadAppleSet()
            {
                SongFightingScene.SceneParams @params =
                        new(new BadApple_RE.Game(), null, 5, "Content\\Musics\\BadAppleRE\\song",
                        GameStates.CurrentScene is SongFightingScene
                    ? (GameStates.CurrentScene as SongFightingScene).JudgeState
                    : JudgementState.Strict, GameMode.RestartDeny | GameMode.Practice | GameMode.PauseDeny);
                GameStates.ResetScene(new SongLoadingScene(@params));
#if !DEBUG
                    var customData = PlayerManager.CurrentUser.Custom;
                    if (!customData.Nexts.ContainsKey("reBadApple"))
                        customData.PushNext(new("reBadApple:value=0"));
                    PlayerManager.Save();
#endif
            }
            public class AppleSongInfo : Entity
            {
                public int difficulty { get; set; } = 0;
                private float NameX = 0;
                private Color MainCol;
                float s = 0;
                public override void Update()
                {
                    MainCol = C.White;
                }
                public override void Start()
                {
                    base.Start();
                    RunEase((s) => { NameX = MathHelper.Lerp(0, 340, s); },
                        LinkEase(Stable(0, 0), EaseOut(150, 1, EaseState.Sine), Stable(450, 0), EaseIn(70, -1.5f, ES.Sine)));
                }
                public override void Draw()
                {

                    C nameBoxColor = C.Black;
                    //Name
                    SpriteBatch.DrawVertex(0, new[] {
                    new VertexPositionColor(new(0, 30, 0), C.White),
                    new VertexPositionColor(new(NameX+2.5f, 30, 0), C.White),
                    new VertexPositionColor(new(NameX + 37.5f, 60, 0), C.White),
                    new VertexPositionColor(new(NameX+2.5f, 90, 0), C.White),
                    new VertexPositionColor(new(NameX-42.5f, 90, 0), C.White),
                    new VertexPositionColor(new(NameX-57.5f, 120, 0), C.White),
                    new VertexPositionColor(new(0, 120, 0), C.White),
                    });
                    SpriteBatch.DrawVertex(0.0001f, new[] {
                    new VertexPositionColor(new(0, 35, 0), nameBoxColor),
                    new VertexPositionColor(new(NameX, 35, 0), nameBoxColor),
                    new VertexPositionColor(new(NameX + 30, 60, 0), nameBoxColor),
                    new VertexPositionColor(new(NameX, 85, 0), nameBoxColor),
                    new VertexPositionColor(new(0, 85, 0), nameBoxColor),
                    });
                    //Desc
                    SpriteBatch.DrawVertex(0.0001f, new[] {
                    new VertexPositionColor(new(0, 115, 0), C.DarkGray),
                    new VertexPositionColor(new(NameX - 60, 115, 0), C.DarkGray),
                    new VertexPositionColor(new(NameX - 45, 85, 0), C.DarkGray),
                    new VertexPositionColor(new(0, 85, 0), C.DarkGray),
                });
                    var text = "ExtremePlus:21.5";
                    Font.NormalFont.LimitDraw(text, new(10, 85), MainCol, NameX - 50, 999999, 1, 0.2f);
                    Font.NormalFont.LimitDraw("Bad Apple!!", new V(10, 35), MainCol, NameX - 15, 99999, 1.5f, 0.2f);
                }
            }
            bool start = false;
            public enum ET 
            {
                In,Out,InOut,OutIn
            }
            private class Vertex : Entity
            {
                public List<V3> point = new();
                public List<V3> point2 = new();
                public C color=C.White;
                public new Vector3 Rotation=V3.Zero;
                private bool trianglecustom = false;
                public bool doubletriangle = false;
                public Vertex(C color,V pos, params V3[] posi) 
                {
                    this.color = color;
                    this.Centre = pos;
                    for(int i = 1; i <= posi.Length; i++) point.Add(posi[^i]);
                }
                public Vertex(C color, V pos, V3[] posi1, V3[] posi2)
                {
                    this.color = color;
                    this.Centre = pos;
                    for (int i = 0; i < posi1.Length; i++) point.Add(posi1[i]);
                    for (int i = 0; i < posi2.Length; i++) point2.Add(posi2[i]);
                    trianglecustom = true;
                }
                private void tri1() 
                {
                    List<V> point1 = new();
                    foreach (var t in this.point)
                        point1.Add(To2d(Rotation, t));
                    List<VertexPositionColor> vertex1 = new();
                    List<VertexPositionColor> vertex2 = new();
                    var centre = V.Zero;
                    for (var i = 0; i < point1.Count / 2 + 1; i++) centre += point1[i];
                    centre /= point1.Count / 2 + 1;
                    vertex1.Add(new(new Vector3(centre + Centre, 0), color));
                    centre = V.Zero;
                    for (var i = point1.Count / 2; i < point1.Count; i++) centre += point1[i];
                    centre /= point1.Count / 2;
                    vertex2.Add(new(new Vector3(centre + Centre, 0), color));
                    for (var i = 0; i < point1.Count / 2 + 1; i++) vertex1.Add(new(new Vector3(Centre + point1[i], 0), color));
                    vertex1.Add(new(new V3(Centre + (point1[0] + point1[^1]) / 2, 0), color));
                    for (var i = point1.Count / 2; i < point1.Count; i++) vertex2.Add(new(new Vector3(Centre + point1[i], 0), color));
                    vertex2.Add(new(new V3(Centre + (point1[1] + point1[^1]) / 2, 0), color));
                    SpriteBatch.DrawVertex(Depth, vertex1.ToArray());
                    SpriteBatch.DrawVertex(Depth, vertex2.ToArray());
                }
                private void tri2()
                {
                    List<V> point1 = new();
                    foreach (var t in this.point2)
                        point1.Add(To2d(Rotation, t));
                    List<VertexPositionColor> vertex1 = new();
                    List<VertexPositionColor> vertex2 = new();
                    var centre = V.Zero;
                    for (var i = 0; i < point1.Count / 2 + 1; i++) centre += point1[i];
                    centre /= point1.Count / 2 + 1;
                    vertex1.Add(new(new Vector3(centre + Centre, 0), color));
                    centre = V.Zero;
                    for (var i = point1.Count / 2; i < point1.Count; i++) centre += point1[i];
                    centre /= point1.Count / 2;
                    vertex2.Add(new(new Vector3(centre + Centre, 0), color));
                    for (var i = 0; i < point1.Count / 2 + 1; i++) vertex1.Add(new(new Vector3(Centre + point1[i], 0), color));
                    vertex1.Add(new(new V3(Centre + (point1[0] + point1[^1]) / 2, 0), color));
                    for (var i = point1.Count / 2; i < point1.Count; i++) vertex2.Add(new(new Vector3(Centre + point1[i], 0), color));
                    vertex2.Add(new(new V3(Centre + (point1[1] + point1[^1]) / 2, 0), color));
                    SpriteBatch.DrawVertex(Depth, vertex1.ToArray());
                    SpriteBatch.DrawVertex(Depth, vertex2.ToArray());
                }
                public override void Draw()
                {
                    if (doubletriangle) 
                    {
                        tri1();
                        tri2();
                        V vec = V.Zero;
                        foreach (var t in point) vec += To2d(Rotation,t);
                        foreach (var t in point2) vec += To2d(Rotation, t);
                        vec /= point.Count + point2.Count;
                        List<V> point1t = new();
                        foreach (var t in this.point)
                            point1t.Add(To2d(Rotation, t));
                        List<V> point2t = new();
                        foreach (var t in this.point2)
                            point2t.Add(To2d(Rotation, t));
                        List<VertexPositionColor> vertex = new();
                        vertex.Add(new(new V3(vec+Centre,0),color));
                        vertex.Add(new(new V3((point1t[7] + point1t[8])/2+Centre,0),color));
                        vertex.Add(new(new V3(point2t[^1]+Centre,0), color));
                        vertex.Add(new(new V3((point2t[7] + point2t[8]) / 2 + Centre, 0), color));
                        vertex.Add(new(new V3(point1t[^1] + Centre, 0), color));
                        SpriteBatch.DrawVertex(Depth,vertex.ToArray());
                    }
                    else if (!trianglecustom)
                    {
                        List<V> point = new();
                        foreach (var t in this.point)
                            point.Add(To2d(Rotation, t));
                        List<VertexPositionColor> vertex = new();
                        foreach (var t in point) vertex.Add(new(new Vector3(t + Centre, 0), color));
                        SpriteBatch.DrawVertex(Depth, vertex.ToArray());
                    }
                    else
                    {
                        List<V> point1 = new();
                        List<V> point2 = new();
                        foreach (var t in this.point)
                            point1.Add(To2d(Rotation, t));
                        foreach (var t in this.point2)
                            point2.Add(To2d(Rotation, t));
                        List<VertexPositionColor> vertex = new();
                        for (int i = 0; i < MathF.Max(point1.Count, point2.Count); i++)
                        {
                            vertex.Add(new(new V3(point1[i] + Centre, 0), color));
                            if (point2.Count > i)
                                vertex.Add(new(new V3(point2[i] + Centre, 0), color));
                        }
                        int[] ints = new int[vertex.Count * 3];
                        for (int i = 0; i < vertex.Count; i++)
                        {
                            int t1 = i * 3, t2 = i * 3 + 1, t3 = i * 3 + 2;
                            ints[t1] = i;
                            ints[t2] = i + 1;
                            ints[t3] = i + 2;
                        }
                        SpriteBatch.DrawVertex(Depth, ints, vertex.ToArray());
                    }
                }
                public Vector2 To2d(Vector3 rot,Vector3 pos)
                {
                    float x1 = Cos(rot.X) * pos.Y - Sin(rot.X) * pos.Z;
                    float x2 = Sin(rot.X) * pos.Y + Cos(rot.X) * pos.Z;
                    float y1 = Sin(rot.Y) * x2 + Cos(rot.Y) * pos.X;
                    float z1 = Cos(rot.Z) * y1 - Sin(rot.Z) * x1;
                    float z2 = Sin(rot.Z) * y1 + Cos(rot.Z) * x1;
                    return new Vector2(z1, z2);
                }
                public override void Update()
                {
                    //Rotation += new V3(0.2f);
                }
                public void RotMotion(EaseUnit<V3> pos,float delay=0)=> AddInstance(new InstantEvent(delay, () => AddChild(new Motion(pos))));


                public void PosMotion(EaseUnit<V> pos,float delay=0) => AddInstance(new InstantEvent(delay, () => RunEase(s => Centre = s, pos)));
                public void VertexMotion(float delay=0,params EaseUnit<V3>[] pos) 
                {
                    AddInstance(new InstantEvent(delay, () =>
                    {
                        point.Clear();
                        for (int i = 0; i < pos.Length; i++)
                        {
                            point.Add(pos[i].Start);
                            AddChild(new Motion(pos[i], true, i));
                        }
                    }));
                }
                public void VertexMotion(float delay,float time,EaseState type,ET t,V? vec=null,params V[] pos)
                {
                    AddInstance(new InstantEvent(delay, () =>
                    {
                        List<V> pt = new();
                        foreach (var t in point) pt.Add(new V(t.X, t.Y));
                        point.Clear();
                        int p = 0;
                        V r=vec ?? V.Zero;
                        for (int i = 0; i < pos.Length; i++)
                        {
                            point.Add(new V3(pt[p], 0));
                            AddChild(new Motion(V3Unit(
                                t==ET.In? EaseIn(time, pt[p], pos[i]+r, type):
                                t==ET.Out? EaseOut(time, pt[p], pos[i]+r, type):
                                t == ET.InOut ? EaseInOut(time, pt[p], pos[i]+r, type):
                                EaseOutIn(time, pt[p], pos[i]+r, type)), true, i));
                            if (pos.Length == pt.Count) p++;
                            else
                            p=Rand(0,pt.Count-1);
                        }
                    }));
                }
                public void VertexMotion(float delay, float time, V[] pos1, V[] pos2, EaseState type, ET t) 
                {
                    List<V3> vec1 = new(),vec2=new();
                    foreach (var r in pos1) vec1.Add(new V3(r,0));
                    foreach (var r in pos2) vec2.Add(new V3(r, 0));
                    VertexMotion(delay,time, vec1.ToArray(),vec2.ToArray(),type,t);
                }
                public void VertexMotion(float delay, float time,V3[] pos1,V3[] pos2, EaseState type, ET t, V? vec = null)
                {
                    AddInstance(new InstantEvent(delay, () =>
                    {
                        trianglecustom = true;
                        V[] v1 = new V[pos1.Length];
                        for (int i = 0; i < pos1.Length; i++) v1[i] = new V(pos1[i].X, pos1[i].Y);
                        V[] v2= new V[pos2.Length];
                        for (int i = 0; i < pos2.Length; i++) v2[i] = new V(pos2[i].X, pos2[i].Y);
                        List<V> pt = new();
                        foreach (var t in point) pt.Add(new V(t.X, t.Y));
                        foreach (var t in point2) pt.Add(new V(t.X,t.Y));
                        point.Clear();
                        point2.Clear();
                        int p = 0;
                        V r = vec ?? V.Zero;
                        for (int i = 0; i < v1.Length; i++)
                        {
                            point.Add(new V3(pt[p], 0));
                            AddChild(new Motion(V3Unit(
                                t == ET.In ? EaseIn(time, pt[p], v1[i] + r, type) :
                                t == ET.Out ? EaseOut(time, pt[p], v1[i] + r, type) :
                                t == ET.InOut ? EaseInOut(time, pt[p], v1[i] + r, type) :
                                EaseOutIn(time, pt[p], v1[i] + r, type)), true, i));
                            if (pos1.Length == pt.Count) p++;
                            else
                                p = Rand(0, pt.Count - 1);
                        }
                        for (int i = 0; i < v2.Length; i++)
                        {
                            point2.Add(new V3(pt[p], 0));
                            AddChild(new Motion(V3Unit(
                                t == ET.In ? EaseIn(time, pt[p], v2[i] + r, type) :
                                t == ET.Out ? EaseOut(time, pt[p], v2[i] + r, type) :
                                t == ET.InOut ? EaseInOut(time, pt[p], v2[i] + r, type) :
                                EaseOutIn(time, pt[p], v2[i] + r, type)), true, i,1));
                            if (pos1.Length == pt.Count) p++;
                            else
                                p = Rand(0, pt.Count - 1);
                        }
                    }));
                }
                private class Motion : GameObject, ICustomMotion
                {
                    public Func<ICustomMotion, V> PositionRoute { get; set; }
                    public Func<ICustomMotion, float> RotationRoute { get; set; }
                    public float[] RotationRouteParam { get; set; }
                    public float[] PositionRouteParam { get; set; }

                    public float AppearTime { get; set; }

                    public float Rotation { get; set; }

                    public V CentrePosition { get; set; }
                    Vertex father;
                    float time = 0;
                    bool me = false;
                    int w;
                    int to;
                    public Motion(EaseUnit<V3> v,bool f=false,int to=0,int point=0) 
                    {
                        PositionRoute = (s)=> { return new V(v.Easing(s).X, v.Easing(s).Y); };
                        RotationRoute = (s) => { return v.Easing(s).Z; };
                        time = v.Time;
                        me = f;
                        this.to = to;
                        UpdateIn120=true;
                        w = point;
                    }
                    public override void Update()
                    {
                        AppearTime += 0.5f;
                        father = FatherObject as Vertex;
                        if (me)
                            if(father.trianglecustom)
                            if(w==0)
                            father.point[to] = new V3(PositionRoute(this),RotationRoute(this));
                        else father.point2[to] = new V3(PositionRoute(this), RotationRoute(this));
                        else father.point[^(to + 1)] = new V3(PositionRoute(this), RotationRoute(this));
                        else
                        father.Rotation =new V3(PositionRoute(this),RotationRoute(this));
                        if (AppearTime > time) Dispose();
                    }
                }
                public class Motion2 : GameObject, ICustomMotion
                {
                    public Func<ICustomMotion, V> PositionRoute { get; set; }
                    public Func<ICustomMotion, float> RotationRoute { get; set; }
                    public Func<ICustomMotion, float> RotationRoute1 { get; set; }
                    public Func<ICustomMotion, float> RotationRoute2 { get; set; }
                    public float[] RotationRouteParam { get; set; }
                    public float[] PositionRouteParam { get; set; }

                    public float AppearTime { get; set; } = 0;

                    public float Rotation { get; set; }

                    public V CentrePosition { get; set; }
                    Vertex father;
                    float time = 0;
                    public Motion2(ValueEasing.EaseBuilder v1, ValueEasing.EaseBuilder v2, ValueEasing.EaseBuilder v3,float time)
                    {
                        RotationRoute = v1.GetResult();
                        RotationRoute1 = v2.GetResult();
                        RotationRoute2 = v3.GetResult();
                        this.time = time;
                        UpdateIn120 = true;
                    }
                    public override void Update()
                    {
                        AppearTime += 0.5f;
                        father = FatherObject as Vertex;
                            father.Rotation = new V3(RotationRoute(this), RotationRoute1(this),RotationRoute2(this));
                        if (AppearTime > time) Dispose();
                    }
                }
            }
            public static EaseUnit<V3> V3Unit(EaseUnit<float>x,EaseUnit<float> y,EaseUnit<float> z)
            {
                return new EaseUnit<V3>(new V3(x.Start,y.Start, z.Start), new V3(x.End,y.End, z.End), MathF.Max(x.Time, z.Time), (s) => 
                {
                    return new V3(x.Easing(s),y.Easing(s),z.Easing(s));
                });
            }
            public static EaseUnit<V3> V3Unit(EaseUnit<V> xy)
            {
                return new EaseUnit<V3>(new V3(xy.Start,0), new V3(xy.End,0), xy.Time, (s) =>
                {
                    return new V3(xy.Easing(s),0);
                });
            }
            private V screenp = V.Zero;
            private void shake() 
            {
                float vec = 15;
                ForBeat120(300, () => 
                {
                    vec *= 0.98f;
                    screenp = new V(
                        vec * Rand(0.0f, 1.0f) * RandSignal(),
                        vec * Rand(0.0f, 1.0f) * RandSignal());
                });
            }
            public static Audio audio;
            public void Hard()
            {
                if (InBeat(3.5f)) 
                {
                    audio = new(Loader.Load<SoundEffect>("Musics\\BadAppleRE\\anomaly\\anomaly_sound"));
                    audio.Play();
                }
                if (InBeat(4)) 
                {
                    shake();
                    const float delay = 2;
                    ForBeat(1000, () => ScreenPositionDelta = screenp+new V(Sin(Gametime * 1.62f) * 5, Sin(Gametime * 1.386f) * 5));
                    RunEase(s => BackGroundColor = C.White * s, EaseOut(T(2), 0.3f, 0, ES.Expo));
                    EfEn ef = new(V.Zero, Sprites.player) { Size = new V(6) };
                    ef.rotEase(false, 0, -36f, T(6), ES.Quart);
                    ef.vecEase(false, new V(320, 550), new V(320, 240), T(6), ES.Expo);
                    ef.SizeEase(true, new V(6), new V(3), T(8-delay), T(2), ES.Expo);
                    ef.SizeEase(false, new V(3), new V(1), T(10-delay), T(2), ES.Expo);
                    ef.rotEase(true, -36, -16f, T(8-delay), T(2), ES.Quart);
                    ef.rotEase(false, -16, 15f, T(10-delay), T(2), ES.Quart);
                    CreateEntity(ef);
                    DelayBeat(0, () =>
                    {
                        Shader s = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\SideFoggy"));
                        ActivateShader(s, 1);
                        ForBeat(1000, () =>
                        {
                            s.Parameters["Iintensity"].SetValue(0.7f + Rand(0.0f, 0.04f) * RandSignal());
                            s.Parameters["itime"].SetValue(Gametime / 50f);

                        });
                    });
                    DelayBeat(7-delay, () => RunEase(s => ScreenDrawing.ScreenAngle = s, EaseIn(T(4), 0, 375 / 2f, ES.Quad), EaseOut(T(4), 375 / 2f, ES.Circ)));
                    for (int i = 0; i < 70; i++)
                    {
                        var t = Rand(0f, 640f);
                        var r = new V(t + Rand(5f, 20f) * RandSignal(), Rand(10f, 470f));
                        Vertex m = new(CW, V.Zero,
                            new V3(GV(Rand(30f, 50f), 0), 0),
                            new V3(GV(Rand(30f, 50f), 60), 0),
                            new V3(GV(Rand(30f, 50f), 120), 0));
                        m.PosMotion(
                            EaseOut(T(6), new V(t, 500), r, ES.Expo));
                        var rot = GetAngle(MathF.Atan2(r.Y - 240, r.X - 320));
                        var v1 = Rand(50f, 300f) * RandSignal();
                        var v2 = Rand(50f, 300f) * RandSignal();
                        m.RotMotion(V3Unit(
                            LinkEase(
                            EaseOut(T(6), 0, v1, ES.Expo)),
                            LinkEase(
                            EaseOut(T(6), 0, v2, ES.Expo)),
                            LinkEase(
                            EaseOut(T(6), 0, rot, ES.Expo))));

                        CreateEntity(m);
                        if (i < 20 || i > 30)
                        {
                            StartP();
                            m.VertexMotion(T(8-delay), T(4), ES.Expo, ET.InOut,
                            null,
                            vertexs.Arrow);
                            int way = i % 4;
                            var alpha = Rand(1, 5) * RandSignal();
                            var randrot = RandSignal() * 360;
                            var len = (r - new V(320, 240)).Length();
                            if (len < 100) RunEase(s => len = s, Stable(T(8-delay), len), EaseInOut(T(4), len, Rand(70f, 100f), ES.Cubic));
                            ValueEasing.EaseBuilder t3 = new();
                            t3.Insert(T(3.5f), (s) =>
                            {
                                return EaseInOut(T(4), rot, way * 90 + randrot, ES.Sine).Easing(s);
                            });
                            t3.Insert(T(4f), (s) =>
                            {
                                return EaseOut(T(4), way * 90 + randrot, way * 90 + randrot + alpha, ES.Sine).Easing(s);
                            });
                            ValueEasing.EaseBuilder t5 = new();
                            t5.Insert(T(4f), (s) =>
                            {
                                return EaseInOut(T(4), v1, 360, ES.Cubic).Easing(s);
                            });
                            ValueEasing.EaseBuilder t6 = new();
                            t6.Insert(T(4f), (s) =>
                            {
                                return EaseInOut(T(4), v2, 0, ES.Cubic).Easing(s);
                            });
                            DelayBeat(8-delay, () =>
                            {
                                t3.Run(s =>
                                {
                                    m.Centre = new V(320, 240) + GV(len, s);
                                });
                                m.AddChild(new Vertex.Motion2(t5, t6, t3, T(8)));
                            });
                        }
                        else if (i < 28)
                        {
                            if (i < 22)
                            {
                                m.RotMotion(V3Unit(
                                    EaseInOut(T(4), v1, 360, ES.Expo),
                                    EaseInOut(T(4), v2, 0, ES.Quad),
                                    EaseInOut(T(4), rot, 0, ES.Circ)), T(8-delay));
                                m.VertexMotion(T(8-delay), T(4), ES.Cubic, ET.InOut, null,
                                    i % 2 == 0 ? GV(42, 180) + new V(0, -3) : GV(42, 0) + new V(0, 3),
                                    GV(42, 180) + new V(0, 3),
                                    GV(42, 0) + new V(0, -3));
                                m.PosMotion(EaseInOut(T(4), r, WCentre + GV(42, -90), ES.Cubic), T(8-delay));
                            }
                            else if (i < 24)
                            {
                                m.RotMotion(V3Unit(
                                    EaseInOut(T(4), v1, 360, ES.Expo),
                                    EaseInOut(T(4), v2, 0, ES.Quad),
                                    EaseInOut(T(4), rot, 0, ES.Circ)), T(8-delay));
                                m.VertexMotion(T(8-delay), T(4), ES.Cubic, ET.InOut, null,
                                    i % 2 == 0 ? GV(42, 90) + new V(3, 0) : GV(42, -90) + new V(-3, 0),
                                    GV(42, -90) + new V(3, 0),
                                    GV(42, 90) + new V(-3, 0));
                                m.PosMotion(EaseInOut(T(4), r, WCentre + GV(42, 0), ES.Cubic), T(8-delay));
                            }
                            else if (i < 26)
                            {
                                m.RotMotion(V3Unit(
                                    EaseInOut(T(4), v1, 360, ES.Expo),
                                    EaseInOut(T(4), v2, 0, ES.Quad),
                                    EaseInOut(T(4), rot, 0, ES.Circ)), T(8-delay));
                                m.VertexMotion(T(8-delay), T(4), ES.Cubic, ET.InOut, null,
                                    i % 2 == 0 ? GV(42, -90) + new V(-3, 0) : GV(42, 90) + new V(3, 0),
                                    GV(42, -90) + new V(3, 0),
                                    GV(42, 90) + new V(-3, 0));
                                m.PosMotion(EaseInOut(T(4), r, WCentre + GV(42, 180), ES.Cubic), T(8-delay));
                            }
                            else if (i < 28)
                            {
                                m.RotMotion(V3Unit(
                                    EaseInOut(T(4), v1, 360, ES.Expo),
                                    EaseInOut(T(4), v2, 0, ES.Quad),
                                    EaseInOut(T(4), rot, 0, ES.Circ)), T(8-delay));
                                m.VertexMotion(T(8-delay), T(4), ES.Cubic, ET.InOut, null,
                                    i % 2 == 0 ? GV(42, 180) + new V(0, -3) : GV(42, 0) + new V(0, 3),
                                    GV(42, 180) + new V(0, 3),
                                    GV(42, 0) + new V(0, -3));
                                m.PosMotion(EaseInOut(T(4), r, WCentre + GV(42, 90), ES.Cubic), T(8-delay));
                            }
                        }
                        else if (i < 30)
                        {
                            m.VertexMotion(T(8-delay), T(4), vertexs.Shield1, vertexs.Shield2, ES.Cubic, ET.InOut);
                            float rand = Rand(0, 4) * 90 + Rand(1, 45f) * RandSignal();
                            m.PosMotion(EaseInOut(T(4), r, new V(320, 240) + GV(5, rand) + GV(5, rand - 90), ES.Cubic), T(8-delay));
                            m.RotMotion(V3Unit(
                                    EaseInOut(T(4), v1, 360, ES.Expo),
                                    EaseInOut(T(4), v2, 0, ES.Quad),
                                    EaseInOut(T(8), rot, rand, ES.Circ)), T(8-delay));
                        }
                        else m.PosMotion(EaseInOut(T(4), m.Centre, new V(m.Centre.X, -200), ES.Circ), T(8-delay));
                    }
                    Shader s = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\BlockTest"));
                    s.Parameters["blocksize"].SetValue(14);
                    ActivateShader(s);
                    SC h = new(0.6f);
                    SceneRendering.InsertProduction(h);
                    btest.sigma = 1f;
                    btest2.sigma = 1f;
                    btest.intensity = 2;
                    btest2.intensity = 2;
                    btest.factor = 2;
                    btest2.factor = 2;
                    RGBS n = new(0.2f);
                    SceneRendering.InsertProduction(n);
                    ForBeat(120, () =>
                    {
                        Shader s = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\BlockTest"));
                        s.Parameters["time"].SetValue(Gametime);
                    });
                    RunEase(s =>
                    {
                        Shader t = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\BlockTest"));
                        t.Parameters["intensity"].SetValue(s);
                        n.intensity = s * 200;

                    }, Stable(T(6-delay), 0.02f), EaseInOut(T(4), 0, 0.1f, ES.Cubic), EaseInOut(T(4), 0, -0.1f, ES.Cubic));
                    RunEase(s =>
                    {
                        Shader t = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\BlockTest"));
                        t.Parameters["length"].SetValue(0);

                    }, EaseInOut(T(8-delay), 0, 0.05f, ES.Cubic));
                }
                if (InBeat(4, 1000)) 
                {
                    for (int i = 0; i < 40; i++)
                    {
                        EfEn ef = new(new V(Rand(0, 640f), Rand(0, 480f)), Sprites.lightBall) { Size = new V(0.05f) };
                        CreateEntity(ef);
                        ef.AutoDis(2);
                    }
                }
            }
            public void AnomalyStart()
            {
                AddInstance(vertexs=new());
                GametimeDelta = -1.5f;
            }

            int debug = 2;
            public void Normal()
            {
                anomaly();
            }
            static V pix=V.Zero;
            static float size = 1.5f;
            private static float Px(float count,float c=4f) 
            {
                return pix.X += count * size;
            }
            private static float Py(float count,float c=4f)
            {
                return pix.Y += count * size;
            }
            private static void StartP()
            {
                pix = new V(-5);
            }
            public void Easy() 
            {
                if (GameStates.IsKeyPressed120f(Keys.D1)) 
                {
                    shake();
                    ForBeat(1000, () => ScreenPositionDelta = screenp+new V(Sin(Gametime*1.62f)*5,Sin(Gametime*1.386f)*5));
                    RunEase(s => BackGroundColor = C.White*s, EaseOut(T(2), 0.3f, 0, ES.Expo));
                    EfEn ef = new(V.Zero, Sprites.player) {Size=new V(6) };
                    ef.rotEase(false,0,-36f,T(4),ES.Quart);
                    ef.vecEase(false,new V(320,550),new V(320,240),T(4),ES.Expo);
                    ef.SizeEase(true,new V(6),new V(3),T(4),T(2),ES.Expo);
                    ef.SizeEase(false, new V(3), new V(1), T(6), T(2), ES.Expo);
                    ef.rotEase(true, -36, -16f, T(4),T(2), ES.Quart);
                    ef.rotEase(false, -16, 15f,T(6), T(2), ES.Quart);
                    CreateEntity(ef);
                    DelayBeat(3, () => RunEase(s => ScreenDrawing.ScreenAngle=s,EaseIn(T(4),0,375/2f,ES.Quad),EaseOut(T(4),375/2f,ES.Circ)));
                    for (int i = 0; i < 70; i++)
                    {
                        var t = Rand(0f, 640f);
                        var r = new V(t + Rand(5f, 20f) * RandSignal(), Rand(10f, 470f));
                        Vertex m = new(CW, V.Zero,
                            new V3(GV(Rand(30f, 50f), 0), 0),
                            new V3(GV(Rand(30f, 50f), 60), 0),
                            new V3(GV(Rand(30f, 50f), 120), 0));
                        m.PosMotion(
                            EaseOut(T(4), new V(t, 500), r, ES.Expo));
                        var rot = GetAngle(MathF.Atan2(r.Y - 240, r.X - 320));
                        var v1 = Rand(50f, 300f) * RandSignal();
                        var v2 = Rand(50f, 300f) * RandSignal();
                        m.RotMotion(V3Unit(
                            LinkEase(
                            EaseOut(T(4),0,v1,ES.Expo)),
                            LinkEase(
                            EaseOut(T(4), 0, v2, ES.Expo)),
                            LinkEase(
                            EaseOut(T(4), 0, rot, ES.Expo))));
                        
                        CreateEntity(m);
                        if (i < 20||i>30)
                        {
                            StartP();
                            m.VertexMotion(T(4), T(4), ES.Expo, ET.InOut,
                            null,
                            vertexs.Arrow);
                            int way = i % 4;
                            var alpha = Rand(1, 5) * RandSignal();
                            var randrot = RandSignal() * 360;
                            var len = (r - new V(320, 240)).Length();
                            if (len < 100) RunEase(s => len = s, Stable(T(4), len), EaseInOut(T(4), len, Rand(70f, 100f), ES.Cubic));
                            ValueEasing.EaseBuilder t3 = new();
                            t3.Insert(T(3.5f), (s) =>
                            {
                                return EaseInOut(T(4), rot, way * 90 + randrot, ES.Sine).Easing(s);
                            });
                            t3.Insert(T(4f), (s) =>
                            {
                                return EaseOut(T(4), way * 90 + randrot, way * 90 + randrot + alpha, ES.Sine).Easing(s);
                            });
                            ValueEasing.EaseBuilder t5 = new();
                            t5.Insert(T(4f), (s) =>
                            {
                                return EaseInOut(T(4), v1, 360, ES.Cubic).Easing(s);
                            });
                            ValueEasing.EaseBuilder t6 = new();
                            t6.Insert(T(4f), (s) =>
                            {
                                return EaseInOut(T(4), v2, 0, ES.Cubic).Easing(s);
                            });
                            DelayBeat(4, () =>
                            {
                                t3.Run(s =>
                                {
                                    m.Centre = new V(320, 240) + GV(len, s);
                                });
                                m.AddChild(new Vertex.Motion2(t5, t6, t3, T(8)));
                            });
                        }
                        else if (i < 28)
                        {
                            if (i < 22)
                            {
                                m.RotMotion(V3Unit(
                                    EaseInOut(T(4), v1, 360, ES.Expo),
                                    EaseInOut(T(4), v2, 0, ES.Quad),
                                    EaseInOut(T(4), rot, 0, ES.Circ)), T(4));
                                m.VertexMotion(T(4), T(4), ES.Cubic, ET.InOut, null,
                                    i % 2 == 0 ? GV(42, 180) + new V(0, -3) : GV(42, 0) + new V(0, 3),
                                    GV(42, 180) + new V(0, 3),
                                    GV(42, 0) + new V(0, -3));
                                m.PosMotion(EaseInOut(T(4), r, WCentre + GV(42, -90), ES.Cubic), T(4));
                            }
                            else if (i < 24)
                            {
                                m.RotMotion(V3Unit(
                                    EaseInOut(T(4), v1, 360, ES.Expo),
                                    EaseInOut(T(4), v2, 0, ES.Quad),
                                    EaseInOut(T(4), rot, 0, ES.Circ)), T(4));
                                m.VertexMotion(T(4), T(4), ES.Cubic, ET.InOut, null,
                                    i % 2 == 0 ? GV(42, 90) + new V(3, 0) : GV(42, -90) + new V(-3, 0),
                                    GV(42, -90) + new V(3, 0),
                                    GV(42, 90) + new V(-3, 0));
                                m.PosMotion(EaseInOut(T(4), r, WCentre + GV(42, 0), ES.Cubic), T(4));
                            }
                            else if (i < 26)
                            {
                                m.RotMotion(V3Unit(
                                    EaseInOut(T(4), v1, 360, ES.Expo),
                                    EaseInOut(T(4), v2, 0, ES.Quad),
                                    EaseInOut(T(4), rot, 0, ES.Circ)), T(4));
                                m.VertexMotion(T(4), T(4), ES.Cubic, ET.InOut, null,
                                    i % 2 == 0 ? GV(42, -90) + new V(-3, 0) : GV(42, 90) + new V(3, 0),
                                    GV(42, -90) + new V(3, 0),
                                    GV(42, 90) + new V(-3, 0));
                                m.PosMotion(EaseInOut(T(4), r, WCentre + GV(42, 180), ES.Cubic), T(4));
                            }
                            else if (i < 28)
                            {
                                m.RotMotion(V3Unit(
                                    EaseInOut(T(4), v1, 360, ES.Expo),
                                    EaseInOut(T(4), v2, 0, ES.Quad),
                                    EaseInOut(T(4), rot, 0, ES.Circ)), T(4));
                                m.VertexMotion(T(4), T(4), ES.Cubic, ET.InOut, null,
                                    i % 2 == 0 ? GV(42, 180) + new V(0, -3) : GV(42, 0) + new V(0, 3),
                                    GV(42, 180) + new V(0, 3),
                                    GV(42, 0) + new V(0, -3));
                                m.PosMotion(EaseInOut(T(4), r, WCentre + GV(42, 90), ES.Cubic), T(4));
                            }
                        }
                        else if (i < 30)
                        {
                            m.VertexMotion(T(4), T(4), vertexs.Shield1, vertexs.Shield2, ES.Cubic, ET.InOut);
                            float rand = Rand(0, 4) * 90 + Rand(1, 45f) * RandSignal();
                            m.PosMotion(EaseInOut(T(4), r, new V(320, 240) + GV(5, rand) + GV(5, rand - 90), ES.Cubic), T(4));
                            m.RotMotion(V3Unit(
                                    EaseInOut(T(4), v1, 360, ES.Expo),
                                    EaseInOut(T(4), v2, 0, ES.Quad),
                                    EaseInOut(T(8), rot, rand, ES.Circ)), T(4));
                        }
                        else m.PosMotion(EaseInOut(T(4),m.Centre,new V(m.Centre.X,-200),ES.Circ),T(4));
                    }
                    Shader s = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\BlockTest"));
                    s.Parameters["blocksize"].SetValue(14);
                    ActivateShader(s);
                    SC h = new(0.6f);
                    SceneRendering.InsertProduction(h);
                    btest.sigma = 1f;
                    btest2.sigma = 1f;
                    btest.intensity = 2;
                    btest2.intensity = 2;
                    btest.factor = 2;
                    btest2.factor = 2;
                    RGBS n = new(0.2f);
                    SceneRendering.InsertProduction(n);
                    ForBeat(120, () => 
                    {
                        Shader s = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\BlockTest"));
                        s.Parameters["time"].SetValue(Gametime);
                    });
                    RunEase(s => 
                    {
                        Shader t = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\BlockTest"));
                        t.Parameters["intensity"].SetValue(s);
                        n.intensity = s * 200;

                    },Stable(T(2),0.02f),EaseInOut(T(4),0,0.2f,ES.Cubic),EaseInOut(T(4),0,-0.2f,ES.Cubic));
                    RunEase(s =>
                    {
                        Shader t = new(Loader.Load<Effect>("Musics\\BadAppleRE\\Shader\\BlockTest"));
                        t.Parameters["length"].SetValue(0);

                    }, EaseInOut(T(8), 0, 0.05f, ES.Cubic));
                }
                if (GameStates.IsKeyPressed120f(Keys.D2)) 
                {
                    
                    foreach (var r in GetAll<Vertex>()) r.Dispose();
                    StartP();
                    Vertex n = new(
                        CW,WCentre+new V(0,50),
                        new V3(Px(-4),Py(-3),0),
                        new V3(Px(0),Py(-2),0),
                        new V3(Px(-4),Py(0),0),
                        new V3(Px(0), Py(1), 0),
                        new V3(Px(-2),Py(0),0),
                        new V3(Px(0), Py(1), 0),
                        new V3(Px(-1), Py(0f), 0),
                        new V3(Px(0), Py(1), 0),
                        new V3(Px(-1), Py(0), 0),
                        new V3(Px(0), Py(1), 0),
                        new V3(Px(-1), Py(0), 0),
                        new V3(Px(0), Py(1), 0),
                        new V3(Px(-1), Py(0), 0),
                        new V3(Px(0), Py(1), 0),
                        new V3(Px(1), Py(0), 0),
                        new V3(Px(0), Py(1), 0),
                        new V3(Px(1), Py(0), 0),
                        new V3(Px(0), Py(1), 0),
                        new V3(Px(1), Py(0), 0),
                        new V3(Px(0), Py(1), 0),
                        new V3(Px(1), Py(0), 0),
                        new V3(Px(0), Py(1), 0),
                        new V3(Px(2), Py(0), 0),
                        new V3(Px(0), Py(1), 0),
                        new V3(Px(4), Py(0), 0),
                        new V3(Px(0), Py(-2), 0),
                        new V3(Px(13), Py(0), 0),
                        new V3(Px(0), Py(-7), 0),
                        new V3(Px(-13),Py(0),0),
                        new V3(Px(0),Py(3),0)
                        );
                    //CreateEntity(n);
                    n.RotMotion(V3Unit(
                        EaseOut(T(4),0,360,ES.Cubic),
                        EaseOut(T(4), 0, 180, ES.Cubic),
                        EaseOut(T(4), 0, 240, ES.Cubic)));
                    Vertex t=new(
                        CW,new V(500,204),
                        new V3(120,30,0),
                        new V3(-30,40,0),
                        new V3(60,-50,0)
                        );
                    CreateEntity(t);
                    //CreateEntity(new Line(500,90));
                    t.VertexMotion(100, T(4),ES.Expo,ET.InOut,
                        new V(Px(7),0),
                        new V(Px(-4), Py(-3)),
                        new V(Px(0), Py(-2)),
                        new V(Px(-4), Py(0)),
                        new V(Px(0), Py(1)),
                        new V(Px(-2), Py(0)),
                        new V(Px(0), Py(1)),
                        new V(Px(-1), Py(0f)),
                        new V(Px(0), Py(1)),
                        new V(Px(-1), Py(0)),
                        new V(Px(0), Py(1)),
                        new V(Px(-1), Py(0)),
                        new V(Px(0), Py(1)),
                        new V(Px(-1), Py(0)),
                        new V(Px(0), Py(1)),
                        new V(Px(1), Py(0)),
                        new V(Px(0), Py(1)),
                        new V(Px(1), Py(0)),
                        new V(Px(0), Py(1)),
                        new V(Px(1), Py(0)),
                        new V(Px(0), Py(1)),
                        new V(Px(1), Py(0)),
                        new V(Px(0), Py(1)),
                        new V(Px(2), Py(0)),
                        new V(Px(0), Py(1)),
                        new V(Px(4), Py(0)),
                        new V(Px(0), Py(-2)),
                        new V(Px(13), Py(0)),
                        new V(Px(0), Py(-7)),
                        new V(Px(-13), Py(0)),
                        new V(Px(0), Py(3)));
                    /*
                    t.RotMotion(V3Unit(
                        EaseOut(T(4),0,180,ES.Cubic),
                        EaseOut(T(4),0,360,ES.Cubic),
                        EaseOut(T(4),0,120,ES.Expo)),100);
                    */
                }
                if (GameStates.IsKeyPressed120f(Keys.D3)) 
                {
                    //InstantSetBox(240,1000,1000);
                    StartP();
                    Vertex vertex = new(C.White,WCentre,new V3(GV(50,0),0),new V3(GV(50,120),0),new V3(GV(50,240),0));
                    vertex.VertexMotion(T(4),T(4),vertexs.Shield1,vertexs.Shield2,ES.Cubic,ET.InOut);
                    CreateEntity(vertex);
                }
                if (GameStates.IsKeyPressed120f(Keys.D4)) 
                {
                    size = 1.5f;
                    StartP();
                    V3[] v1 = new[] 
                    {
                        new V3(Px(3),Py(0),0),
                        new V3(Px(0),Py(-12),0),
                        new V3(Px(1),Py(0),0),
                        new V3(Px(0),Py(-2),0),
                        new V3(Px(1),Py(0),0),
                        new V3(Px(0),Py(-3),0),
                        new V3(Px(-1),Py(0),0),
                        new V3(Px(0),Py(-1),0),
                        new V3(Px(-2),Py(0),0),
                        new V3(Px(0),Py(1),0),
                        new V3(Px(-1),Py(0),0),
                        new V3(Px(0),Py(1),0),
                        new V3(Px(-2),Py(0),0),
                        new V3(Px(0),Py(-1),0),
                        new V3(Px(-1),Py(0),0),
                        new V3(Px(0),Py(-1),0),
                        new V3(Px(-2),Py(0),0),
                        new V3(Px(0),Py(1),0),
                        new V3(Px(-1),Py(0),0),
                        new V3(Px(0),Py(3),0),
                        new V3(Px(1),Py(0),0),
                        new V3(Px(0),Py(2),0),
                        new V3(Px(1),Py(0),0),
                        new V3(Px(0),Py(12),0),

                    };
                    StartP();
                    V3[] v2 = new[] 
                    {
                        new V3(Px(-3),Py(0),0),
                        new V3(Px(0),Py(12),0),
                        new V3(Px(-1),Py(0),0),
                        new V3(Px(0),Py(2),0),
                        new V3(Px(-1),Py(0),0),
                        new V3(Px(0),Py(3),0),
                        new V3(Px(1),Py(0),0),
                        new V3(Px(0),Py(1),0),
                        new V3(Px(2),Py(0),0),
                        new V3(Px(0),Py(-1),0),
                        new V3(Px(1),Py(0),0),
                        new V3(Px(0),Py(-1),0),
                        new V3(Px(2),Py(0),0),
                        new V3(Px(0),Py(1),0),
                        new V3(Px(1),Py(0),0),
                        new V3(Px(0),Py(1),0),
                        new V3(Px(2),Py(0),0),
                        new V3(Px(0),Py(-1),0),
                        new V3(Px(1),Py(0),0),
                        new V3(Px(0),Py(-3),0),
                        new V3(Px(-1),Py(0),0),
                        new V3(Px(0),Py(-2),0),
                        new V3(Px(-1),Py(0),0),
                        new V3(Px(0),Py(-12),0),
                    };
                    Vertex ver = new(C.White,new V(320,240),V3.Zero,V3.Zero,V3.Zero);
                    ver.VertexMotion(T(4),T(4),vertexs.Arrow,vertexs.Arrow,ES.Cubic,ET.InOut);
                    CreateEntity(ver);
                    DelayBeat(4f, () => ver.doubletriangle=true);
                }
                if (GameStates.IsKeyPressed120f(Keys.D5)) 
                {
                    var rgb = new RGBSplitting(0.3f)
                    {
                        Disturbance = true,
                        RandomDisturb = 10
                    };
                    SceneRendering.InsertProduction(rgb);
                }
            }
            public void Extreme() 
            {
            }
            public VertexDictionary vertexs;
        }
        private class anomaly : Scene
        {
            Game game;
            public anomaly()
            {
                this.AddChild(game = new Game());
                UpdateIn120 = true;
            }


            public override void Draw()
            {
                base.Draw();
            }
            bool start = true;
            public override void Update()
            {
                if (start)
                {
                    CreateEntity();
                    start = false;
                    game.AnomalyStart();
                }
                game.Hard();
                base.Update();

#if DEBUG
                if (GameStates.IsKeyPressed120f(InputIdentity.Reset))
                    BadApple_RE.Game.anomaly();
                if (GameStates.IsKeyPressed120f(Keys.D)) 
                {
                    SongFightingScene.SceneParams @params =
                        new(new BadApple_RE.Game(), null, 5, "Content\\Musics\\BadAppleRE\\song",
                        GameStates.CurrentScene is SongFightingScene
                    ? (GameStates.CurrentScene as SongFightingScene).JudgeState
                    : JudgementState.Strict);
                    GameStates.ResetScene(new SongLoadingScene(@params));
                }

#endif
            }

        }

        public static void IntoUnlockScene()
        {
            var t = new anomaly();
            GameStates.ResetScene(t);
        }
    }
}
