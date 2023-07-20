using Microsoft.Xna.Framework.Graphics; 
using System;
using System.Collections;
using System.Collections.Generic;

namespace UndyneFight_Ex
{
    partial class SpriteBatchEX
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
                for(int i = 0; i < _current.Length; i++)
                {
                    if (effect == null)
                        this.DrawItem(_current[i]);
                    else this.DrawItem(effect, _current[i]);
                }
            }

            private void DrawItem(SpriteBatchItem item)
            { 
            }
            private void DrawItem(Effect effect, SpriteBatchItem item)
            { 
            }
        }
    }
}