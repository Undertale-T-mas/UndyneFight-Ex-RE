using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace UndyneFight_Ex
{
    partial class SpriteBatchEX
    {
        private readonly SpriteBatcherEX _batcher;

        private SpriteSortMode _sortMode;
        private BlendState _blendState;
        private SamplerState _samplerState;
        private DepthStencilState _depthStencilState;
        private RasterizerState _rasterizerState;

        private Effect _effect;

        protected GraphicsDevice _graphicDevice;

        private SpriteEffect _spriteEffect;
        private readonly EffectPass _spritePass;

        public SpriteBatchEX(GraphicsDevice graphicsDevice) 
        {
            _graphicDevice = graphicsDevice;
            _spriteEffect = new SpriteEffect(graphicsDevice);
            _spritePass = _spriteEffect.CurrentTechnique.Passes[0];
            _beginCalled = false;
            _batcher = new(graphicsDevice);
        }
        private bool _beginCalled = false;

        private void CheckValid(Texture2D texture)
        {
            if (texture == null)
            {
                throw new ArgumentNullException("texture");
            }

            if (!_beginCalled)
            {
                throw new InvalidOperationException("Draw was called, but Begin has not yet been called. Begin must be called successfully before you can call Draw.");
            }
        }

        private void CheckValid(SpriteFont spriteFont, string text)
        {
            if (spriteFont == null)
            {
                throw new ArgumentNullException("spriteFont");
            }

            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            if (!_beginCalled)
            {
                throw new InvalidOperationException("DrawString was called, but Begin has not yet been called. Begin must be called successfully before you can call DrawString.");
            }
        }

        public void Begin(
            SpriteSortMode sortMode = SpriteSortMode.Deferred, 
            BlendState blendState = null,
            SamplerState samplerState = null,
            DepthStencilState depthStencilState = null,
            RasterizerState rasterizerState = null,
            Effect effect = null,
            Matrix? transform = null
        )
        { 
            if (_beginCalled)
            {
                throw new InvalidOperationException("Begin cannot be called again until End has been successfully called.");
            }

            _sortMode = sortMode;
            _blendState = blendState ?? BlendState.AlphaBlend;
            _samplerState = samplerState ?? SamplerState.LinearClamp;
            _depthStencilState = depthStencilState ?? DepthStencilState.None;
            _rasterizerState = rasterizerState ?? RasterizerState.CullCounterClockwise;
            _effect = effect;
            _spriteEffect.TransformMatrix = transform;
            
            if (sortMode == SpriteSortMode.Immediate)
            {
                Setup();
            }

            _beginCalled = true;
        }
        public void End()
        {
            if (!_beginCalled)
            {
                throw new InvalidOperationException("Begin must be called before calling End.");
            }

            _beginCalled = false;
            if (_sortMode != SpriteSortMode.Immediate)
            {
                Setup();
            }

            _batcher.DrawBatch(_sortMode, _effect);
        }
        private void Setup()
        {
            GraphicsDevice obj = _graphicDevice;
            obj.BlendState = _blendState;
            obj.DepthStencilState = _depthStencilState;
            obj.RasterizerState = _rasterizerState;
            obj.SamplerStates[0] = _samplerState;
            _spritePass.Apply();
        }

        private float TextureSortKey(float depth) => _sortMode switch {
            SpriteSortMode.Texture => throw new Exception("SB monogame"),
            SpriteSortMode.FrontToBack => depth * 10,
            SpriteSortMode.BackToFront => depth * (-10),
            _ => 0
        };

        public void Draw(Texture2D texture, CollideRect origin, CollideRect? source, Color color, float rotation, Vector2 anchor, Vector2 scale, SpriteEffects effects, float depth) { 
            float sortKey = TextureSortKey(depth);
            Vector2 realAnchor = anchor + origin.TopLeft;
            Vector2[] pos = { 
                -anchor,                                // TL
                new Vector2(origin.Width, 0) - anchor,  // TR
                new Vector2(0, origin.Height) - anchor, // BL
                origin.Size - anchor                    // BR
            }; 
            if ((effects & SpriteEffects.FlipVertically) != 0)
            {
                scale.Y *= -1;
            }

            if ((effects & SpriteEffects.FlipHorizontally) != 0)
            {
                scale.X *= -1;
            }
            if (MathF.Abs((rotation + 0.005f) % 360) > 0.01f) // need for rotating
            {
                for(int i = 0; i < 4; i++)
                {
                    pos[i] = MathUtil.Rotate(pos[i], rotation) * scale;
                }
            }
            VertexPositionColorTexture vTL, vTR, vBL, vBR;
            vTL.Position = new(pos[0] + realAnchor, 0);
            vTR.Position = new(pos[1] + realAnchor, 0);
            vBL.Position = new(pos[2] + realAnchor, 0);
            vBR.Position = new(pos[3] + realAnchor, 0);
            vTL.Color = color;
            vTR.Color = color;
            vBL.Color = color;
            vBR.Color = color;

            if(source.HasValue)
            {
                CollideRect area = source.Value;
                Rectangle full = texture.Bounds;
                float l = area.X / full.Width;
                float r = area.Right / full.Width;
                float t = area.Y / full.Height;
                float b = area.Down / full.Height;

                vTL.TextureCoordinate = new(l, t);
                vTR.TextureCoordinate = new(r, t);
                vBL.TextureCoordinate = new(l, b);
                vBR.TextureCoordinate = new(r, b);
            }
            else
            {
                vTL.TextureCoordinate = Vector2.Zero;
                vTR.TextureCoordinate = Vector2.UnitX;
                vBL.TextureCoordinate = Vector2.UnitY;
                vBR.TextureCoordinate = Vector2.One;
            }
            FlushIfNeeded();
        }
        public void Draw(Texture2D texture, Vector2 position, CollideRect? sourceRectangle, Color color, float rotation, Vector2 anchor, Vector2 scale, SpriteEffects effects, float depth) {
            CheckValid(texture);
            Vector2 topLeft = position - anchor;
            CollideRect rect = new(topLeft, sourceRectangle.HasValue ? 
                sourceRectangle.Value.Size : 
                texture.Bounds.Size.ToVector2());

            this.Draw(texture, rect, sourceRectangle, color, rotation, anchor, scale, effects, depth);
        }
        internal void FlushIfNeeded()
        {
            if (_sortMode == SpriteSortMode.Immediate)
            {
                _batcher.DrawBatch(_sortMode, _effect);
            }
        }
    }
}