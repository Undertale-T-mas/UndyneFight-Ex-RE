using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using UndyneFight_Ex.Entities;

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

        private class PrimitiveItem : SpriteBatchItem
        {
            public PrimitiveItem(float key, VertexPositionColor[] vertexs) : base(FightResources.Sprites.pixiv, key)
            {
                this.Vertexs = new VertexPositionColorTexture[vertexs.Length];
                for (int i = 0; i < vertexs.Length; i++)
                    this.Vertexs[i] = new(vertexs[i].Position, vertexs[i].Color, new(0, 0));
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
            public RectangleItem(float x, float y, float dx, float dy, float w, float h, float sin, float cos, Color color, Vector2 texCoordTL, Vector2 texCoordBR, float depth,
                Texture2D tex, float key
                ) : base(tex, key) 
            {
                VertexPositionColorTexture vertexTL, vertexTR, vertexBL, vertexBR;
                vertexTL.Position.X = x + dx * cos - dy * sin;
                vertexTL.Position.Y = y + dx * sin + dy * cos;
                vertexTL.Position.Z = depth;
                vertexTL.Color = color;
                vertexTL.TextureCoordinate.X = texCoordTL.X;
                vertexTL.TextureCoordinate.Y = texCoordTL.Y;
                vertexTR.Position.X = x + (dx + w) * cos - dy * sin;
                vertexTR.Position.Y = y + (dx + w) * sin + dy * cos;
                vertexTR.Position.Z = depth;
                vertexTR.Color = color;
                vertexTR.TextureCoordinate.X = texCoordBR.X;
                vertexTR.TextureCoordinate.Y = texCoordTL.Y;
                vertexBL.Position.X = x + dx * cos - (dy + h) * sin;
                vertexBL.Position.Y = y + dx * sin + (dy + h) * cos;
                vertexBL.Position.Z = depth;
                vertexBL.Color = color;
                vertexBL.TextureCoordinate.X = texCoordTL.X;
                vertexBL.TextureCoordinate.Y = texCoordBR.Y;
                vertexBR.Position.X = x + (dx + w) * cos - (dy + h) * sin;
                vertexBR.Position.Y = y + (dx + w) * sin + (dy + h) * cos;
                vertexBR.Position.Z = depth;
                vertexBR.Color = color;
                vertexBR.TextureCoordinate.X = texCoordBR.X;
                vertexBR.TextureCoordinate.Y = texCoordBR.Y;
                this.Indices = _indices;
                this.Vertexs = new[] { vertexTL, vertexTR, vertexBL, vertexBR };
                this.PrimitiveCount = 2;
            }
            public RectangleItem(float x, float y, float w, float h, Color color, Vector2 texCoordTL, Vector2 texCoordBR, float depth,
                       Texture2D tex, float key
                ) : base(tex, key)
            {
                VertexPositionColorTexture vertexTL, vertexTR, vertexBL, vertexBR;
                vertexTL.Position.X = x;
                vertexTL.Position.Y = y;
                vertexTL.Position.Z = depth;
                vertexTL.Color = color;
                vertexTL.TextureCoordinate.X = texCoordTL.X;
                vertexTL.TextureCoordinate.Y = texCoordTL.Y;
                vertexTR.Position.X = x + w;
                vertexTR.Position.Y = y;
                vertexTR.Position.Z = depth;
                vertexTR.Color = color;
                vertexTR.TextureCoordinate.X = texCoordBR.X;
                vertexTR.TextureCoordinate.Y = texCoordTL.Y;
                vertexBL.Position.X = x;
                vertexBL.Position.Y = y + h;
                vertexBL.Position.Z = depth;
                vertexBL.Color = color;
                vertexBL.TextureCoordinate.X = texCoordTL.X;
                vertexBL.TextureCoordinate.Y = texCoordBR.Y;
                vertexBR.Position.X = x + w;
                vertexBR.Position.Y = y + h;
                vertexBR.Position.Z = depth;
                vertexBR.Color = color;
                vertexBR.TextureCoordinate.X = texCoordBR.X;
                vertexBR.TextureCoordinate.Y = texCoordBR.Y;
                this.Indices = _indices;
                this.Vertexs = new[] { vertexTL, vertexTR, vertexBL, vertexBR };
                this.PrimitiveCount = 2;
            }
        }
    }
}