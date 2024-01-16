using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using static UndyneFight_Ex.DrawingLab;

namespace UndyneFight_Ex.Entities
{
    public partial class Line
    {
        private class StateStorer : GameObject
        {
            Line follow;
            public StateStorer()
            {
                UpdateIn120 = true;
            }
            public override void Start()
            {
                follow = FatherObject as Line;
            }
            public override void Update()
            {
                LineState state;
                state.p1 = follow.vec1.CentrePosition;
                state.p2 = follow.vec2.CentrePosition;
                state.time = follow.AppearTime;
                state.alpha = follow.Alpha;
                state.color = follow.DrawingColor;
                state.verticalMirror = follow.VerticalMirror;
                state.transverseMirror = follow.TransverseMirror;
                state.obliqueMirror = follow.ObliqueMirror;
                state.verticalline = follow.VerticalLine;
                DataStore.Add(follow.AppearTime, state);
            }
            public Dictionary<float, LineState> DataStore = new();
        }
        StateStorer storer;
        public void InsertRetention(RetentionEffect effect)
        {
            if (storer == null)
            {
                storer = new();
                AddChild(storer);
            }
            AddChild(effect);
        }

        public void DelayDispose(float v)
        {
            this.AddChild(new InstantEvent(v, () => { this.Dispose(); }));
        }

        public new class RetentionEffect : Entity
        {
            float timeLag;
            Func<float, float> alphaGenerator;

            Line follow;
            private RetentionEffect()
            {
                UpdateIn120 = true;
            }
            public override void Start()
            {
                follow = FatherObject as Line;

                //process timeLag to a number which can be divided by 0.5f
                timeLag = MathF.Round(timeLag * 2) / 2f;

                base.Start();
            }
            public RetentionEffect(float timeLag) : this()
            {
                this.timeLag = timeLag;
                alphaGenerator = (s) => s;
            }
            public RetentionEffect(float timeLag, float alphaFactor) : this()
            {
                this.timeLag = timeLag;
                alphaGenerator = (s) => s * alphaFactor;
            }
            public RetentionEffect(float timeLag, Func<float, float> alphaGenerator) : this()
            {
                this.timeLag = timeLag;
                this.alphaGenerator = alphaGenerator;
            }
            void DrawTargetLine(Vector2 Start, Vector2 End)
            {
                DrawLine(Start, End, follow.Width, follow.DrawingColor * alpha, Depth);
            }
            public override void Draw()
            {
                if (!available) return;
                if (alpha <= 0) return;
                float width = follow.Width;
                DrawTargetLine(vec1, vec2);
                if (verticalMirror)
                    DrawTargetLine(new Vector2(vec1.X, 480 - vec1.Y), new Vector2(vec2.X, 480 - vec2.Y));
                if (transverseMirror)
                    DrawTargetLine(new Vector2(640 - vec1.X, vec1.Y), new Vector2(640 - vec2.X, vec2.Y));
                if (obliqueMirror)
                    DrawTargetLine(new Vector2(640 - vec1.X, 480 - vec1.Y), new Vector2(640 - vec2.X, 480 - vec2.Y));
                if (verticalline)
                {
                    DrawTargetLine(new Vector2(560 - vec1.Y, vec1.X - 80), new Vector2(560 - vec2.Y, vec2.X - 80));
                    if (transverseMirror)
                        DrawTargetLine(new Vector2(640 - (560 - vec1.Y), vec1.X - 80), new Vector2(640 - (560 - vec2.Y), vec2.X - 80));
                    if (verticalMirror)
                        DrawTargetLine(new Vector2(560 - vec1.Y, 480 - (vec1.X - 80)), new Vector2(560 - vec2.Y, 480 - (vec2.X - 80)));
                    if (obliqueMirror)
                        DrawTargetLine(new Vector2(640 - (560 - vec1.Y), 480 - (vec1.X - 80)), new Vector2(640 - (560 - vec2.Y), 480 - (vec2.X - 80)));
                }
            }

            Vector2 vec1, vec2;
            float alpha;
            Color color;
            bool transverseMirror, verticalMirror, obliqueMirror, verticalline;

            bool available = false;
            public override void Update()
            {
                float key = follow.AppearTime - timeLag;
                if (!follow.storer.DataStore.ContainsKey(key))
                {
                    available = false;
                    return;
                }
                available = true;
                vec1 = follow.storer.DataStore[key].p1;
                vec2 = follow.storer.DataStore[key].p2;
                alpha = alphaGenerator(follow.storer.DataStore[key].alpha);
                color = follow.storer.DataStore[key].color;
                transverseMirror = follow.storer.DataStore[key].transverseMirror;
                verticalMirror = follow.storer.DataStore[key].verticalMirror;
                obliqueMirror = follow.storer.DataStore[key].obliqueMirror;
                verticalline = follow.storer.DataStore[key].verticalline;
            }
        }
    }
}