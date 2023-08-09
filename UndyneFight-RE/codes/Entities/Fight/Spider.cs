﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using UndyneFight_Ex.GameInterface;
using UndyneFight_Ex.Entities;
using Microsoft.Xna.Framework.Graphics;
using UndyneFight_Ex.Fight;
using static UndyneFight_Ex.Fight.Functions;
using System.Runtime.ExceptionServices;
using System.Net.Http;
using UndyneFight_Ex;
using System.Diagnostics;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.FightResources;
using System.Runtime.CompilerServices;

namespace UndyneFight_Ex.Remake.Entities
{
    public class Spider : Barrage
    {
        public Spider(EaseUnit<Vector2> _positionRoute) : this() {
            this.PositionRoute = _positionRoute.Easing;
            var ease = _positionRoute;
            Rotation=MathUtil.Direction(ease.Start, ease.End);
        }
        public Spider(Func<ICustomMotion, Vector2> _positionRoute) : this() {
            this.PositionRoute = _positionRoute;
        }
        public Spider(EaseUnit<Vector2>_positionRoute,EaseUnit<float> _rotationRoute) : this()
        {
            this.PositionRoute=_positionRoute.Easing;
            this.RotationRoute = _rotationRoute.Easing;
        }
        public Spider(EaseUnit<Vector2> _positionRoute, float Rotation) : this()
        {
            this.PositionRoute = _positionRoute.Easing;
            this.Rotation = Rotation;
        }
        public Spider(Func<ICustomMotion, Vector2> _positionRoute, Func<ICustomMotion, float> _rotationRoute) : this()
        {
            this.PositionRoute = _positionRoute;
            this.RotationRoute = _rotationRoute;
        }
        public bool AutoDispose { set;  get; } = true;
        Spider()
        {
            this.Image = Sprites.spider;
            this.Depth = 0.5f;
            this._autoDispose = true;
            this.Alpha = 1.0f;
            this.UpdateIn120 = true;
            this.HitRadius = 4f;
            this.Hidden = true;
            this.AngelMode = true;
        }
        protected override float GetDistance(Player.Heart heart)
        {
            return Vector2.Distance(heart.Centre, this.Centre);
        }
        public override void Start()
        {
            _autoDispose = AutoDispose;
            this.HitRadius = 5f * Scale;
        }
        public class LineSpider : Spider
        {
            protected float speed;
            private float count;
            public float Speed { set => speed = value; get => speed; }
            public LineSpider(int CountLine, bool IsLeftOrRight, float Speed)
            {
                collidingBox.X = IsLeftOrRight ? BoxStates.Centre.X - BoxStates.Width / 2f  : BoxStates.Centre.X + BoxStates.Width / 2f;
                Rotation =IsLeftOrRight ? 0 : 180;
                this.speed = Speed;
                movingWay = IsLeftOrRight;
                count = CountLine;
            }
            private readonly bool movingWay;
            public override void Draw()
            {
                base.Draw();
            }
            public override void Update()
            {
                collidingBox.X += speed * 0.5f * (movingWay ? 1 : -1);
                collidingBox.Y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1f) * count;
                base.Update(); 
            }
        }
    }
    public class FakeSpider : Entity
    {
        public float scale;
        public float depth = 0.75f;
        public float alpha = 1f;
        public Color color=Color.White;
        public FakeSpider(Vector2 center, float scale, float rotation, float alpha)
        {
            Image = Sprites.spider;
            Centre = center;
            this.scale = scale;
            Rotation = rotation;
            this.alpha = alpha;
        }
        float timer;
        public override void Draw()
        {
            Depth = depth;
            FormalDraw(Image, Centre, color * alpha, scale, Rotation / 180 * MathF.PI, ImageCentre);
        }

        public override void Update()
        {

        }
    }
}