using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex.Entities.Advanced
{
    public static class BarrageExtend
    {
        private class SplitBone : Bone
        {
            public const float rotateSpeed = -2f;
            private Vector2 speed;
            public SplitBone(Vector2 centre, Vector2 speed, float rotation, float length)
            {
                Rotation = rotation;
                this.speed = speed;
                Length = length;
                alpha = 1;
                Centre = centre;
            }
            public override void Update()
            {
                Rotation += rotateSpeed;
                speed.Y += 0.02f;
                Centre += speed;
                base.Update();
            }
        }
        private class PlatformShinyEffect : Entity
        {
            private readonly Platform following;
            private readonly int length, platformType;
            private float alpha = 1f, size = 1f;
            public PlatformShinyEffect(Platform p, int length, int platformType)
            {
                controlLayer = Surface.Hidden;
                following = p;
                this.length = length;
                this.platformType = platformType;
                Image = Sprites.platform[platformType];
            }

            public override void Draw()
            {
                if (!following.isMasked) return;
                Vector2 delta = GetVector2(length / 2, Rotation) * size;
                var side = Sprites.platformSide[platformType];
                GameMain.MissionSpriteBatch.Draw(Image, Centre, new Rectangle(0, 0, length, 12), Color.White * alpha, GetRadian(Rotation), new Vector2(length / 2, 6), size, SpriteEffects.None, 0.35f);
                Depth = 0.35F;
                FormalDraw(side, Centre - delta, Color.White * alpha, GetRadian(Rotation), new Vector2(0, 6));
                FormalDraw(side, Centre + delta, Color.White * alpha, GetRadian(Rotation), new Vector2(0, 6));
            }

            public override void Update()
            {
                Centre = following.Centre;
                Rotation = following.Rotation;
                alpha -= 0.06f;
                size += 0.1f;
                if (alpha < 0) Dispose();
            }
        }
        public static void CreateShinyEffect(this Platform pt)
        {
            Vector2 data = pt.GetData();
            GameStates.InstanceCreate(new PlatformShinyEffect(pt, (int)data.X, (int)data.Y));
        }
        public static Bone[] Split(this Bone bone)
        {
            Vector2 c1 = bone.Centre;
            Vector2 detla = GetVector2(bone.Length / 4f, bone.Rotation + 90);
            bone.Dispose();
            return new Bone[] { new SplitBone(c1 + detla, GetVector2(1, Rand(0, 359)), bone.Rotation, bone.Length / 2 - 1){ IsMasked = bone.IsMasked },
                new SplitBone(c1 - detla, GetVector2(1, Rand(0, 359)),bone.Rotation,  bone.Length / 2 - 1){ IsMasked = bone.IsMasked }};
        }
    }
    public static class BarrageStruct
    {
        public static void BoneWindfall(Vector2 centre, int boneCount, float rotationSpeed, float length, float duration, int colorType, bool isAlphaIncrease)
        {
            for (int i = 0; i < boneCount; i++)
            {
                float detla = 180 / boneCount * i;
                CreateBone(new CustomBone(Vector2.Zero, (x) => { return centre; }, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    LengthRouteParam = new float[] { length, duration },
                    RotationRouteParam = new float[] { rotationSpeed, 0 },
                    RotationDetla = detla,
                    AlphaIncrease = isAlphaIncrease,
                    ColorType = colorType
                });
            }
        }
        public static void BoneWindfall(int boneCount, float rotationSpeed, float length, float duration)
        {
            for (int i = 0; i < boneCount; i++)
            {
                float detla = 180 / boneCount * i;
                CreateBone(new CustomBone(Vector2.Zero, (x) => { return (x as CustomBone).ControlingBox.Centre; }, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    LengthRouteParam = new float[] { length, duration },
                    RotationRouteParam = new float[] { rotationSpeed, 0 },
                    RotationDetla = detla,
                    AlphaIncrease = true
                });
            }
        }
        public static void BoneWindfall(Vector2 centre, int boneCount, ICustomMotion motion, float length, float duration)
        {
            for (int i = 0; i < boneCount; i++)
            {
                float detla = 180 / boneCount * i;
                CreateBone(new CustomBone(centre, motion.PositionRoute, Motions.LengthRoute.autoFold, motion.RotationRoute)
                {
                    PositionRouteParam = motion.PositionRouteParam,
                    LengthRouteParam = new float[] { length, duration },
                    RotationRouteParam = motion.RotationRouteParam,
                    RotationDetla = detla,
                    AlphaIncrease = true
                });
            }
        }
        public static void BoneWindfall(Vector2 centre, int boneCount, Func<ICustomMotion, Vector2> positionRoute, float[] positionRouteParam, float rotationSpeed, float length, float duration)
        {
            for (int i = 0; i < boneCount; i++)
            {
                float detla = 180 / boneCount * i;
                CreateBone(new CustomBone(centre, positionRoute, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = positionRouteParam,
                    LengthRouteParam = new float[] { length, duration >= 0 ? duration : 0x3f3f3f3f },
                    RotationRouteParam = new float[] { rotationSpeed, 0 },
                    RotationDetla = detla,
                    AlphaIncrease = duration >= 0
                });
            }
        }
        public static void BoneWindfall(Vector2 centre, int boneCount, ICustomMotion motion, ICustomLength lengthControl)
        {
            for (int i = 0; i < boneCount; i++)
            {
                float detla = 180 / boneCount * i;
                CreateBone(new CustomBone(centre, motion.PositionRoute, lengthControl.LengthRoute, motion.RotationRoute)
                {
                    PositionRouteParam = motion.PositionRouteParam,
                    LengthRouteParam = lengthControl.LengthRouteParam,
                    RotationRouteParam = motion.RotationRouteParam,
                    RotationDetla = detla
                });
            }
        }
    }
}