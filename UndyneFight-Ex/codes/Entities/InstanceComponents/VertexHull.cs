using Microsoft.Xna.Framework;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex.Entities.Advanced
{
    public class VertexHull : GameObject
    {
        private Vector3 rotateSpeed;
        private readonly Vector3[] positions;
        public Vector2[] Translated { get; private set; }
        public Vector2[] Axises { get; private set; } = new Vector2[] { new(1, 0), new(0, -1), new(-0.5f, 1.732f / 2f) };

        public Vector3 rotation;

        private static Vector2 Reflect(Vector3 rot, Vector3 pos, Vector2 xAxis, Vector2 yAxis, Vector2 zAxis)
        {
            var v = Matrix.CreateRotationX(GetRadian(rot.X)) * Matrix.CreateRotationY(GetRadian(rot.Y)) * Matrix.CreateRotationZ(GetRadian(rot.Z));
            Vector3 s = (new Matrix(Vector4.Zero, Vector4.Zero, Vector4.Zero, new(pos, 0)) * v).Translation;
            return (s.X * xAxis) + (s.Y * yAxis) + (s.Z * zAxis);
        }

        public override void Update()
        {
            rotation += rotateSpeed;
            for (int i = 0; i < Translated.Length; i++)
                Translated[i] = Reflect(rotation, positions[i], Axises[0], Axises[1], Axises[2]);
        }

        public VertexHull(Vector3[] vectors, Vector3 rotateSpeed)
        {
            positions = vectors;
            this.rotateSpeed = rotateSpeed;
            Translated = new Vector2[vectors.Length];
        }
    }
}