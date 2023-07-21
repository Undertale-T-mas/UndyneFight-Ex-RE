using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace UndyneFight_Ex
{
    public partial  class SpriteBatchEX
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
            public int PrimitiveCount { get; protected set; }

            public int CompareTo(SpriteBatchItem other)
            {
                return SortKey.CompareTo(other.SortKey);
            }
        }

        private class VertexItem : SpriteBatchItem
        {
            public VertexItem(Texture2D tex, float key, VertexPositionColorTexture[] vertexs) : base(tex, key)
            {
                this.Vertexs = vertexs;
                int count = vertexs.Length - 2;
                Indices = new int[count * 3];

                for (int i = 0; i < count; i++)
                {
                    int i1 = i * 3, i2 = i * 3 + 1, i3 = i * 3 + 2;
                    Indices[i1] = 0;
                    Indices[i2] = i + 1;
                    Indices[i3] = i + 2;
                }
                PrimitiveCount = count;
            }
            public VertexItem(Texture2D tex, float key, int[] indices, VertexPositionColorTexture[] vertexs) : base(tex, key)
            {
                this.Vertexs = vertexs;
                int count = vertexs.Length - 2;
                Indices = indices;
                PrimitiveCount = count;
            }
        }

        private class RectangleItem : SpriteBatchItem
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
                this.PrimitiveCount = 2;
            }
        }
    }
}