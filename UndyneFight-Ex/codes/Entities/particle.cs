using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex.Entities
{
    public class ParticleGather : Entity
    {
        private int appearTime = 0;
        public ParticleGather(Vector2 centre, int count, float duration, Color color)
        {
            Image ??= FightResources.Sprites.lightBall;
            Centre = centre;
            this.count = count;
            this.duration = duration;

            rotations = new float[count];
            sizes = new float[count];
            speeds = new float[count];
            for (int i = 0; i < count; i++)
            {
                rotations[i] = Rand(0, 359);
                speeds[i] = Rand(20, 50) / 10f;
                sizes[i] = Rand(4, 9) / 10f;
            }

            drawingColor = color;
        }
        public ParticleGather(Vector2 centre, int count, float duration, float sizeAverage, Color color)
        {
            Image ??= FightResources.Sprites.lightBall;
            Centre = centre;
            this.count = count;
            this.duration = duration;

            rotations = new float[count];
            sizes = new float[count];
            speeds = new float[count];
            for (int i = 0; i < count; i++)
            {
                rotations[i] = Rand(0, 359);
                speeds[i] = Rand(20, 50) / 10f;
                sizes[i] = Rand(7, 13) / 10f * sizeAverage;
            }

            drawingColor = color;
        }

        public Texture2D Image_ { set => Image = value; }

        private Color drawingColor;
        private readonly float[] rotations;
        private readonly float[] speeds;
        private readonly float[] sizes;
        private readonly int count;
        private readonly float duration;
        private float timeLeft;

        public override void Update()
        {
            appearTime++;
            timeLeft = duration - appearTime;
            if (timeLeft < 0) Dispose();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public override void Draw()
        {
            for (int i = 0; i < count; i++)
            {
                FormalDraw(Image, Centre + GetVector2(speeds[i]
                    * timeLeft, rotations[i]), drawingColor * MathHelper.Min(0.7f, appearTime / (duration / 1.3f)), sizes[i], 0, ImageCentre);
            }
        }
    }
    public class Particle : Entity
    {
        public static void CreateParticles(Color color, float speed, float size, Vector2 centre, int count, float darkingSpeed)
        {
            for (int i = 0; i < count; i++)
            {
                float sizeV = GetRandom(10, 20) / 15f * size;
                Vector2 speedV = GetVector2(speed * (GetRandom(10, 20) / 15f), GetRandom(0, 365));
                GameStates.InstanceCreate(new Particle(color, speedV, sizeV, centre) { darkingSpeed = darkingSpeed });
            }
        }
        public static void CreateParticles(Color color, float speed, float size, Vector2 centre, int count)
        {
            for (int i = 0; i < count; i++)
            {
                float sizeV = GetRandom(10, 20) / 15f * size;
                Vector2 speedV = GetVector2(speed * (GetRandom(10, 20) / 15f), GetRandom(0, 365));
                GameStates.InstanceCreate(new Particle(color, speedV, sizeV, centre));
            }
        }

        public Particle(Color color, Vector2 speed, float size, Vector2 centre, Texture2D image)
        {
            this.size = size / 20f;
            Centre = centre;
            this.color = color;
            this.speed = speed;
            Image = image;
            Depth = 0.45f;
        }
        public Particle(Color color, Vector2 speed, float size, Vector2 centre)
        {
            this.size = size / 20f;
            Centre = centre;
            this.color = color;
            this.speed = speed;
            Image = FightResources.Sprites.lightBall;
            Depth = 0.45f;
        }

        /// <summary>
        /// 默认值：3f
        /// </summary>
        public float DarkingSpeed
        {
            private get { return darkingSpeed; }
            set { darkingSpeed = value; }
        }
        public float Alpha
        {
            private get { return light; }
            set { light = value; }
        }
        public float RotateSpeed
        {
            private get { return rotateSpeed; }
            set { rotateSpeed = value; }
        }

        private float darkingSpeed = 3;
        private readonly float size;
        private float light = 1.0f;

        bool autoRotate = false;
        public bool AutoRotate
        {
            get
            {
                return autoRotate;
            }
            set
            {
                autoRotate = value;
                rotateSpeed = value ? Rand(-42, 42) / 20f : 0f;
            }
        }

        public float SlowLerp { private get; set; }

        private float rotateSpeed = 0.0f;

        private Color color;
        private Vector2 speed;

        public override void Draw()
        {
            FormalDraw(Image, Centre, color * light, size, Rotation, ImageCentre);
        }

        public override void Update()
        {
            if (light >= darkingSpeed / 255f)
                light -= darkingSpeed / 255f;
            else Dispose();
            Centre += speed;
            speed = speed * (1 - SlowLerp) + Vector2.Zero * SlowLerp;
            if (autoRotate) Rotation += rotateSpeed / 180f * MathHelper.Pi;
        }
    }
}