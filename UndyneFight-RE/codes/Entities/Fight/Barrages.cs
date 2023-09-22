using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using UndyneFight_Ex.GameInterface;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.Functions;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.SongSystem;
using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex.Remake.Entities
{
    public abstract class Barrage : AutoEntity, ICustomMotion, ICollideAble
    { 
        public class ImageMotion : GameObject
        {
            Texture2D[] images;
            float interval;
            public ImageMotion(Texture2D[] images,float interval)
            {
                UpdateIn120 = true;
                this.images = images;
                this.interval = interval;
            }
            int index=0;
            float timer = 0;
            public override void Update()
            {
                timer+=0.5f;
                if (timer >= interval) { index += 1; index %= images.Length;timer -=interval; }
                father.Image = images[index];
            }
            Barrage father;
            public override void Start()
            {
                father = (Barrage)this.FatherObject;
                
            }
        }
        public Barrage() { UpdateIn120 = true; this.PlayerMission = Heart; }

        public Func<ICustomMotion, Vector2> PositionRoute { get; set; }
        public Func<ICustomMotion, float> RotationRoute { get; set; }
        public float[] RotationRouteParam { get; set; }
        public float[] PositionRouteParam { get; set; }
        public bool Hidden { private get; set; } = false;

        public float AppearTime { get; set; }

        private bool _hasBeenInside = false;
        /// <summary>
        /// 是否自动Dispose
        /// </summary>
        public bool _autoDispose = true;

        protected Player.Heart PlayerMission { get; private init; }

        public Vector2 CentrePosition => Centre;

        protected abstract float GetDistance(Player.Heart heart);
        public override void Update()
        {
            this.AppearTime += 0.5f;
            if(PositionRoute != null) { this.Centre = this.PositionRoute.Invoke(this); }
            if(RotationRoute != null) { this.Rotation = this.RotationRoute.Invoke(this); }
            this.controlLayer = Hidden ? Surface.Hidden : Surface.Normal;
            if (_autoDispose)
            {
                bool ins = screen.Contain(this.Centre);
                if (ins && (!_hasBeenInside)) _hasBeenInside = true;
                if (_hasBeenInside && (!ins)) Dispose();
            }
        }
        public void CreateShinyEffect()
        {
            var m = base.CreateShinyEffect();
            m.Depth = this.Depth + 0.001f;
        }
        private static readonly CollideRect screen = new(-80, -80, 800, 640);

        int score = 3;
        int colorType = 0;
        bool hasHit = false;
        public bool MarkScore { get; set; } = true;
        protected float HitRadius { get; set; } = 4f;

        /// <summary>
        /// 设置颜色属性并且自动重置颜色。0->白，1->蓝，2->绿
        /// </summary>
        public float ColorType
        {
            set
            {
                switch (value)
                {
                    case 0: BlendColor = Color.White; colorType = 0; break;
                    case 1: BlendColor = new Color(110, 203, 255, 255); colorType = 1; break;
                    case 2: BlendColor = Color.Orange; colorType = 2; break;
                    default: throw new ArgumentOutOfRangeException("rvalue", value, "The rvalue can only be 0, 1 or 2");
                }
            }
        }

        private JudgementState JudgeState
        {
            get
            {
                return GameStates.CurrentScene is SongFightingScene
                    ? (GameStates.CurrentScene as SongFightingScene).JudgeState
                    : JudgementState.Lenient;
            }
        }

        public void GetCollide(Player.Heart player)
        {
            if (colorType == 1 && player.IsStable) return;
            if (colorType == 2 && player.IsMoved) return;
            if (PlayerInstance.hpControl.protectTime > 0) return;

            float res = GetDistance(player) - this.HitRadius - 6;
            int offset = 3 - (int)JudgeState;
            if (res < 0)
            {
                if (!hasHit)
                    AdvanceFunctions.PushScore(0);
                LoseHP(player);
                hasHit = true;
            }
            else if (res <= 1.6f - offset * 0.4f)
            {
                if (score >= 2) { score = 1; Player.CreateCollideEffect(Color.LawnGreen, 3f); }
            }
            else if (res <= 4.2f - offset * 1.2f)
            {
                if (score >= 3) { score = 2; Player.CreateCollideEffect(Color.LightBlue, 6f); }
            }
            bool needAP = ((CurrentScene as FightScene).Mode & GameMode.PerfectOnly) != 0;
            if (score != 3 && needAP && MarkScore)
            {
                if (!hasHit)
                {
                    AdvanceFunctions.PushScore(0);
                    LoseHP(player);
                    hasHit = true;
                }
            }
        }

        public override void Dispose()
        {
            if (!hasHit && MarkScore) AdvanceFunctions.PushScore(score);
            base.Dispose();
        }
    }

}