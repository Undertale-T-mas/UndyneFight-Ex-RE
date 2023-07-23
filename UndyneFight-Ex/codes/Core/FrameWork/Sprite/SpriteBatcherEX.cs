using Microsoft.Xna.Framework.Graphics; 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace UndyneFight_Ex
{
    public partial class SpriteBatchEX
    {
        private class SpriteBatcherEX
        {
            private GraphicsDevice _graphicsDevice; 

            private List<SpriteBatchItem> _items = new(256);

            internal void Insert(SpriteBatchItem item)
            {
                _items.Add(item);
            }

            public SpriteBatcherEX(GraphicsDevice graphicsDevice)
            {
                this._graphicsDevice = graphicsDevice;
            }

            internal void DrawBatch(SpriteSortMode sortMode, Effect effect)
            {
                if (effect != null && effect.IsDisposed)
                {
                    throw new ObjectDisposedException("effect");
                }

                SpriteBatchItem[] _current;
                _current = _items.ToArray();
                if (_current.Length == 0)
                {
                    return;
                }
                if ((uint)(sortMode - 2) <= 2u)
                {
                    Array.Sort(_current);
                }
                if (GameMain.RegisterTextures[0] == null)
                    for (int i = 0; i < _current.Length; i++)
                    {
                        if (effect == null)
                            this.DrawItem(_current[i]);
                        else this.DrawItem(effect, _current[i]);
                    }
                else
                {
                    for (int i = 0; i < _current.Length; i++)
                        this.DrawItemSampler(effect, _current[i]);
                }
                _items.Clear();
            }

            VertexPositionColorTexture[] buffer = new VertexPositionColorTexture[3];
            private void DrawItem(SpriteBatchItem item)
            {
                    _graphicsDevice.Textures[0] = item.Texture;
                    _graphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, item.Vertexs, 0, item.Vertexs.Length, item.Indices, 0, item.PrimitiveCount, VertexPositionColorTexture.VertexDeclaration);

            }
            private void DrawItem(Effect effect, SpriteBatchItem item)
            {
                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    _graphicsDevice.Textures[0] = item.Texture;
                    _graphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, item.Vertexs, 0, item.Vertexs.Length, item.Indices, 0, item.PrimitiveCount, VertexPositionColorTexture.VertexDeclaration);
                }
            }
            private void DrawItemSampler(Effect effect, SpriteBatchItem item)
            {
                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    _graphicsDevice.Textures[0] = item.Texture;
                    for (int i = 0; i < 3; i++)
                    {
                        if (GameMain.RegisterTextures[i] == null) break;
                        _graphicsDevice.Textures[i + 1] = GameMain.RegisterTextures[i];
                    }

                    _graphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, item.Vertexs, 0, item.Vertexs.Length, item.Indices, 0, item.PrimitiveCount, VertexPositionColorTexture.VertexDeclaration);
                }
            }
        }
    }
}