using Microsoft.Xna.Framework.Graphics;
using System;

namespace UndyneFight_Ex
{
    partial class SpriteBatchEX
    {
        private abstract class SpriteBatchItem : IComparable<SpriteBatchItem>
        {
            public SpriteBatchItem(Texture2D tex, float key)
            {
                this.Texture = tex;
                this.SortKey = key;
            }

            public float SortKey { get; init; }
            public Texture2D Texture { get; init; }

            public int[] Indices { get; protected set; }
            public VertexPositionColorTexture[] Vertexs { get; protected set; }

            public int CompareTo(SpriteBatchItem other)
            {
                return SortKey.CompareTo(other.SortKey);
            }
        }

        private abstract class RectangleItem : SpriteBatchItem
        {
            public static readonly int[] _indices = { 0, 1, 2, 1, 3, 2 }; 
            public RectangleItem(
                VertexPositionColorTexture topLeft,
                VertexPositionColorTexture topRight,
                VertexPositionColorTexture bottomLeft,
                VertexPositionColorTexture bottomRight,
                Texture2D tex, float key) : base(tex, key) {
                this.Indices = _indices;
                this.Vertexs = new[] { topLeft, topRight, bottomLeft, bottomRight };
            }
        }
    }
}