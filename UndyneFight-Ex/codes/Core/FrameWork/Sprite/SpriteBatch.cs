using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace UndyneFight_Ex
{
    public partial class SpriteBatchEX
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

        private SamplerState _defaultState;

        public static SamplerState NearestSample { get; set; }
        public SamplerState DefaultState { set => _defaultState = value; }

        public SpriteBatchEX(GraphicsDevice graphicsDevice)
        {
            _graphicDevice = graphicsDevice;
            _spriteEffect = GameMain.SpriteEffect;
            _spritePass = GameMain.SpritePass;
            _beginCalled = false;
            _batcher = new(graphicsDevice);

            if (NearestSample == null)
            {
                SamplerState state = new();
                state.AddressU = TextureAddressMode.Clamp;
                state.AddressV = TextureAddressMode.Clamp;
                state.AddressW = TextureAddressMode.Clamp;
                state.MaxMipLevel = 0;
                state.MipMapLevelOfDetailBias = 0;
                state.MaxAnisotropy = 0;

                state.ComparisonFunction = CompareFunction.Never;
                state.Filter = TextureFilter.Point;
                _defaultState = state;
                NearestSample = _defaultState;
            }
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

        private float _depthBuffer = 0.0000f;

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
            _samplerState = samplerState ?? _defaultState;
            _depthStencilState = depthStencilState ?? DepthStencilState.None;
            _rasterizerState = rasterizerState ?? RasterizerState.CullNone;
            _effect = effect;
            _spriteEffect.TransformMatrix = transform;

            if (sortMode == SpriteSortMode.Immediate)
            {
                Setup();
            }

            _beginCalled = true; _depthBuffer = 0.0000f;
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

            for (int i = 0; i < 3; i++)
            {
                if (GameMain.RegisterTextures[i] == null) break;
                GameMain.RegisterTextures[i] = null;
            }
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

        private float TextureSortKey(float depth) => _sortMode switch
        {
            SpriteSortMode.Texture => throw new Exception("SB monogame"),
            SpriteSortMode.FrontToBack => depth * 10,
            SpriteSortMode.BackToFront => depth * (-10),
            _ => 0
        };

        public void Draw(Texture2D texture, CollideRect origin, CollideRect? source, Color color, float rotation, Vector2 anchor, Vector2 scale, SpriteEffects effects, float depth)
        {
            float sortKey = TextureSortKey(depth) + _depthBuffer;
            _depthBuffer += 0.00001f;
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
            if (scale != Vector2.One)
            {
                for (int i = 0; i < 4; i++)
                {
                    pos[i] = pos[i] * scale;
                }
            }
            if (MathF.Abs((MathUtil.GetAngle(rotation) + 0.005f) % 360) > 0.01f) // need for rotating
            {
                for (int i = 0; i < 4; i++)
                {
                    pos[i] = MathUtil.RotateRadian(pos[i], rotation);
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

            if (source.HasValue)
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
            _batcher.Insert(new RectangleItem(vTL, vTR, vBL, vBR, texture, sortKey));
            FlushIfNeeded();
        }
        public void Draw(Texture2D texture, Vector2 position, CollideRect? sourceRectangle, Color color, float rotation, Vector2 anchor, Vector2 scale, SpriteEffects effects, float depth)
        {
            CheckValid(texture);
            Vector2 topLeft = position - anchor;
            CollideRect rect = new(topLeft, sourceRectangle.HasValue ?
                sourceRectangle.Value.Size :
                texture.Bounds.Size.ToVector2());

            Draw(texture, rect, sourceRectangle, color, rotation, anchor, scale, effects, depth);
        }
        public void Draw(Texture2D texture, Vector2 centre, CollideRect? sourceRectangle, Color color, float rotation, Vector2 anchor, float scale, SpriteEffects effects, float depth)
        {
            Draw(texture, centre, sourceRectangle, color, rotation, anchor, new Vector2(scale, scale), effects, depth);
        }
        public void Draw(Texture2D texture, Vector2 centre, Color color, float rotation, Vector2 anchor, float scale, float depth)
        {
            Draw(texture, centre, null, color, rotation, anchor, new Vector2(scale, scale), SpriteEffects.None, depth);
        }
        public void Draw(Texture2D texture, Vector2 centre, Color color)
        {
            Draw(texture, centre, null, color, 0, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 0);
        }
        public void Draw(Texture2D texture, Vector2 centre, CollideRect? sourceRectangle, Color color, float rotation, Vector2 anchor, float scale, float depth)
        {
            Draw(texture, centre, sourceRectangle, color, rotation, anchor, new Vector2(scale, scale), SpriteEffects.None, depth);
        }
        public void Draw(Texture2D texture, CollideRect rect1, CollideRect? rect2, Color color, float rotation, Vector2 anchor, SpriteEffects se, float depth)
        {
            Draw(texture, rect1, rect2, color, rotation, anchor, Vector2.One, se, depth);
        }
        public void Draw(Texture2D texture, CollideRect rect1, CollideRect? rect2, Color color)
        {
            Draw(texture, rect1, rect2, color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f);
        }

        public void Draw(Texture2D texture, CollideRect bounds, Color color)
        {
            Draw(texture, bounds, null, color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f);
        }


        /// <summary>
        /// Give the vertexs information of sprite to draw on the current RenderTarget
        /// </summary>
        /// <param name="texture">the texture sprite</param>
        /// <param name="vertexs">The vertexs given. Make them in the order of clockwise! </param>
        /// <param name="depth">The depth to sort</param>
        public void DrawVertex(Texture2D texture, float depth, params VertexPositionColorTexture[] vertexs)
        {
            _batcher.Insert(new VertexItem(texture, TextureSortKey(depth), vertexs));
        }
        /// <summary>
        /// Give the vertexs information of sprite to draw on the current RenderTarget
        /// </summary>
        /// <param name="texture">the texture sprite</param>
        /// <param name="vertexs">The vertexs given. Make them in the order of clockwise! </param>
        /// <param name="depth">The depth to sort</param>
        public void DrawVertex(float depth, params VertexPositionColor[] vertexs)
        {
            _batcher.Insert(new PrimitiveItem(TextureSortKey(depth), vertexs));
        }
        /// <summary>
        /// Give the vertexs information of sprite to draw on the current RenderTarget
        /// </summary>
        /// <param name="texture">the texture sprite</param>
        /// <param name="vertexs">The vertexs given. Make them in the order of clockwise! </param>
        /// <param name="depth">The depth to sort</param>
        public void DrawSortedVertex(float depth, params VertexPositionColor[] vertexs)
        {
            Vector2[] list = new Vector2[vertexs.Length];
            for (int i = 0; i < list.Length; i++) list[i] = new(vertexs[i].Position.X, vertexs[i].Position.Y);
            var raw = DrawingLab.GetIndices(list);
            int[] indices = new int[raw.Count * 3];
            for (int i = 0; i < raw.Count; i++)
            {
                indices[(i * 3) + 0] = raw[i].Item1;
                indices[(i * 3) + 1] = raw[i].Item2;
                indices[(i * 3) + 2] = raw[i].Item3;
            }
            _batcher.Insert(new PrimitiveItem(TextureSortKey(depth), indices, vertexs));
        }
        /// <summary>
        /// Give the vertexs information of sprite to draw on the current RenderTarget
        /// </summary>
        /// <param name="texture">the texture sprite</param>
        /// <param name="vertexs">The vertexs given. Make them in the order of clockwise! </param>
        /// <param name="depth">The depth to sort</param>
        public void DrawVertex(Texture2D texture, float depth, int[] indices, params VertexPositionColorTexture[] vertexs)
        {
            _batcher.Insert(new VertexItem(texture, TextureSortKey(depth), indices, vertexs));
        }
        /// <summary>
        /// Give the vertexs information of sprite to draw on the current RenderTarget
        /// </summary>
        /// <param name="texture">the texture sprite</param>
        /// <param name="vertexs">The vertexs given. Make them in the order of clockwise! </param>
        /// <param name="depth">The depth to sort</param>
        public void DrawVertex(Texture2D texture, float depth, params VertexPositionColor[] vertexs)
        {
            VertexPositionColorTexture[] vpctVertexs = new VertexPositionColorTexture[vertexs.Length];
            float t = 99999f, b = -99999f, l = 99999f, r = -99999f;
            for (int i = 0; i < vertexs.Length; i++)
            {
                t = MathF.Min(t, vertexs[i].Position.Y);
                b = MathF.Max(b, vertexs[i].Position.Y);
                l = MathF.Min(l, vertexs[i].Position.X);
                r = MathF.Max(r, vertexs[i].Position.X);
            }

            for (int i = 0; i < vertexs.Length; i++)
            {
                float u = (vertexs[i].Position.X - l) / (r - l);
                float v = (vertexs[i].Position.Y - t) / (b - t);
                vpctVertexs[i] = new(vertexs[i].Position, vertexs[i].Color, new(u, v));
            }

            _batcher.Insert(new VertexItem(texture, TextureSortKey(depth), vpctVertexs));
        }
        /// <summary>
        /// Give the vertexs information of sprite to draw on the current RenderTarget
        /// </summary>
        /// <param name="texture">the texture sprite</param>
        /// <param name="vertexs">The vertexs given. Make them in the order of clockwise! </param>
        /// <param name="depth">The depth to sort</param>
        public void DrawVertex(Texture2D texture, float depth, int[] indices, params VertexPositionColor[] vertexs)
        {
            VertexPositionColorTexture[] vpctVertexs = new VertexPositionColorTexture[vertexs.Length];
            float t = 99999f, b = -99999f, l = 99999f, r = -99999f;
            for (int i = 0; i < vertexs.Length; i++)
            {
                t = MathF.Min(t, vertexs[i].Position.Y);
                b = MathF.Max(b, vertexs[i].Position.Y);
                l = MathF.Min(l, vertexs[i].Position.X);
                r = MathF.Max(r, vertexs[i].Position.X);
            }

            for (int i = 0; i < vertexs.Length; i++)
            {
                float u = (vertexs[i].Position.X - l) / (r - l);
                float v = (vertexs[i].Position.Y - t) / (b - t);
                vpctVertexs[i] = new(vertexs[i].Position, vertexs[i].Color, new(u, v));
            }

            _batcher.Insert(new VertexItem(texture, TextureSortKey(depth), indices, vpctVertexs));
        }
        public unsafe void DrawString(GLFont font, string text, Vector2 position, Color color)
        {
            SpriteFont spriteFont = font.SFX;
            CheckValid(spriteFont, text);
            float sortKey = 0;
            Vector2 zero = Vector2.Zero;
            bool flag = true;
            fixed (SpriteFont.Glyph* ptr = spriteFont.Glyphs)
            {
                foreach (char c in text)
                {
                    switch (c)
                    {
                        case '\n':
                            zero.X = 0f;
                            zero.Y += spriteFont.LineSpacing;
                            flag = true;
                            continue;
                        case '\r':
                            continue;
                    }

                    SpriteFont.Glyph* ptr2 = ptr + font.GetGlyphIndexOrDefault(c);
                    if (flag)
                    {
                        zero.X = Math.Max(ptr2->LeftSideBearing, 0f);
                        flag = false;
                    }
                    else
                    {
                        zero.X += spriteFont.Spacing + ptr2->LeftSideBearing;
                    }

                    Vector2 vector = zero;
                    vector.X += ptr2->Cropping.X;
                    vector.Y += ptr2->Cropping.Y;
                    vector += position;

                    Vector2 _texCoordTL, _texCoordBR;
                    _texCoordTL.X = ptr2->BoundsInTexture.X;
                    _texCoordTL.Y = ptr2->BoundsInTexture.Y;
                    _texCoordBR.X = ptr2->BoundsInTexture.X + ptr2->BoundsInTexture.Width;
                    _texCoordBR.Y = ptr2->BoundsInTexture.Y + ptr2->BoundsInTexture.Height;
                    Vector2 _uvTL, _uvBR;
                    _uvTL.X = _texCoordTL.X / spriteFont.Texture.Width;
                    _uvTL.Y = _texCoordTL.Y / spriteFont.Texture.Height;
                    _uvBR.X = _texCoordBR.X / spriteFont.Texture.Width;
                    _uvBR.Y = _texCoordBR.Y / spriteFont.Texture.Height;
                    SpriteBatchItem spriteBatchItem;
                    spriteBatchItem = new RectangleItem(vector.X, vector.Y, ptr2->BoundsInTexture.Width, ptr2->BoundsInTexture.Height, color, _uvTL, _uvBR, 0.0f, spriteFont.Texture, sortKey);

                    _batcher.Insert(spriteBatchItem);
                    zero.X += ptr2->Width + ptr2->RightSideBearing;
                }
            }

            FlushIfNeeded();
        }
        public void DrawString(GLFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            Vector2 scale2 = new(scale, scale);
            DrawString(spriteFont, text, position, color, rotation, origin, scale2, effects, layerDepth);
        }

        public unsafe void DrawString(GLFont font, string text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            SpriteFont spriteFont = font.SFX;
            CheckValid(spriteFont, text);
            float sortKey = TextureSortKey(layerDepth);
            Vector2 zero = Vector2.Zero;
            bool flag = (effects & SpriteEffects.FlipVertically) == SpriteEffects.FlipVertically;
            bool flag2 = (effects & SpriteEffects.FlipHorizontally) == SpriteEffects.FlipHorizontally;
            if (flag || flag2)
            {
                Vector2 size = spriteFont.MeasureString(text);
                if (flag2)
                {
                    origin.X *= -1f;
                    zero.X = 0f - size.X;
                    scale.X *= -1f;
                }

                if (flag)
                {
                    origin.Y *= -1f;
                    zero.Y = spriteFont.LineSpacing - size.Y;
                    scale.Y *= -1f;
                }
            }

            Matrix matrix = Matrix.Identity;
            float num = 0f;
            float num2 = 0f;
            if (rotation == 0f)
            {
                matrix.M11 = flag2 ? (0f - scale.X) : scale.X;
                matrix.M22 = flag ? (0f - scale.Y) : scale.Y;
                matrix.M41 = ((zero.X - origin.X) * matrix.M11) + position.X;
                matrix.M42 = ((zero.Y - origin.Y) * matrix.M22) + position.Y;
            }
            else
            {
                num = MathF.Cos(rotation);
                num2 = MathF.Sin(rotation);
                matrix.M11 = (flag2 ? (0f - scale.X) : scale.X) * num;
                matrix.M12 = (flag2 ? (0f - scale.X) : scale.X) * num2;
                matrix.M21 = (flag ? (0f - scale.Y) : scale.Y) * (0f - num2);
                matrix.M22 = (flag ? (0f - scale.Y) : scale.Y) * num;
                matrix.M41 = ((zero.X - origin.X) * matrix.M11) + ((zero.Y - origin.Y) * matrix.M21) + position.X;
                matrix.M42 = ((zero.X - origin.X) * matrix.M12) + ((zero.Y - origin.Y) * matrix.M22) + position.Y;
            }

            Vector2 zero2 = Vector2.Zero;
            bool flag3 = true;
            fixed (SpriteFont.Glyph* ptr = spriteFont.Glyphs)
            {
                foreach (char c in text)
                {
                    switch (c)
                    {
                        case '\n':
                            zero2.X = 0f;
                            zero2.Y += spriteFont.LineSpacing;
                            flag3 = true;
                            continue;
                        case '\r':
                            continue;
                    }

                    SpriteFont.Glyph* ptr2 = ptr + font.GetGlyphIndexOrDefault(c);
                    if (flag3)
                    {
                        zero2.X = Math.Max(ptr2->LeftSideBearing, 0f);
                        flag3 = false;
                    }
                    else
                    {
                        zero2.X += spriteFont.Spacing + ptr2->LeftSideBearing;
                    }

                    Vector2 position2 = zero2;
                    if (flag2)
                    {
                        position2.X += ptr2->BoundsInTexture.Width;
                    }

                    position2.X += ptr2->Cropping.X;
                    if (flag)
                    {
                        position2.Y += ptr2->BoundsInTexture.Height - spriteFont.LineSpacing;
                    }

                    position2.Y += ptr2->Cropping.Y;
                    Vector2.Transform(ref position2, ref matrix, out position2);

                    Vector2 _texCoordTL, _texCoordBR;
                    _texCoordTL.X = ptr2->BoundsInTexture.X;
                    _texCoordTL.Y = ptr2->BoundsInTexture.Y;
                    _texCoordBR.X = ptr2->BoundsInTexture.X + ptr2->BoundsInTexture.Width;
                    _texCoordBR.Y = ptr2->BoundsInTexture.Y + ptr2->BoundsInTexture.Height;
                    Vector2 _uvTL, _uvBR;
                    _uvTL.X = _texCoordTL.X / spriteFont.Texture.Width;
                    _uvTL.Y = _texCoordTL.Y / spriteFont.Texture.Height;
                    _uvBR.X = _texCoordBR.X / spriteFont.Texture.Width;
                    _uvBR.Y = _texCoordBR.Y / spriteFont.Texture.Height;
                    SpriteBatchItem spriteBatchItem = rotation == 0f
                        ? new RectangleItem(position2.X, position2.Y, ptr2->BoundsInTexture.Width * scale.X, ptr2->BoundsInTexture.Height * scale.Y, color, _uvTL, _uvBR, layerDepth, spriteFont.Texture, sortKey)
                        : (SpriteBatchItem)new RectangleItem(position2.X, position2.Y, 0f, 0f, ptr2->BoundsInTexture.Width * scale.X, ptr2->BoundsInTexture.Height * scale.Y, num2, num, color, _uvTL, _uvBR, layerDepth, spriteFont.Texture, sortKey);
                    _batcher.Insert(spriteBatchItem);
                    zero2.X += ptr2->Width + ptr2->RightSideBearing;
                }
            }

            FlushIfNeeded();
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