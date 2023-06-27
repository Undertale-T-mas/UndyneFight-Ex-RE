using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System; 
using UndyneFight_Ex.IO;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        private partial class SongSelector
        {
            private partial class ImageDrawer : Entity
            {
                public ImageDrawer()
                {
                    UpdateIn120 = true;
                }
                SongSelector _father;
                public override void Start()
                {
                    this._father = FatherObject as SongSelector;
                    base.Start();
                }
                bool onFocus, noFocus;
                float target = -1.0f;
                float alpha = 0.6f;
                public override void Update()
                {
                    float mission = this._father.SelectedID;
                    if (this._father._currentSongList.CurrentFocus != null)
                        onFocus = (this._father._currentSongList.CurrentFocus.IsMouseOn);
                    else onFocus = false;
                    if (onFocus)
                    {
                        float mission2 = this._father._currentSongList.FocusID;
                        if (mission == mission2) onFocus = false;
                        else mission = mission2;
                    }
                    noFocus = this._father.SelectedID == -1;

                    if (mission != -1)
                    {
                        if (target == -1.0f) target = mission;
                        else target = MathHelper.Lerp(target, mission, 0.12f);
                        mainPos = (int)MathF.Round(target);
                        posDelta = target - mainPos;
                    }
                    //X = 787, Y = 251

                    const float TOP = 35, BOTTOM = 465;

                    mainRect = new CollideRect();
                    Vector2 size;
                    size = mainRect.Size = new(280, 210);
                    mainRect.SetCentre(787, 251 - posDelta * size.Y);

                    if (onFocus || noFocus) alpha = MathHelper.Lerp(alpha, 0.6f, 0.12f);
                    else alpha = MathHelper.Lerp(alpha, 1.0f, 0.12f);

                    nextRect = new CollideRect();
                    nextRect.Size = size;
                    nextRect.SetCentre(mainRect.GetCentre() + new Vector2(0, size.Y));

                    float bottom = nextRect.Y + nextRect.Height;
                    if (bottom > BOTTOM)
                    {
                        nextRect.Height = BOTTOM - nextRect.Y; 
                    }
                    scaleNext = nextRect.Height / size.Y;

                    lastRect = new CollideRect();
                    lastRect.Size = size;
                    lastRect.SetCentre(mainRect.GetCentre() - new Vector2(0, size.Y));

                    float top = lastRect.Y;
                    if(top < TOP)
                    {
                        lastRect.Height = lastRect.Y + lastRect.Height - TOP;
                        lastRect.Y = TOP;
                    }
                    scaleLast = lastRect.Height / size.Y;
                }
                int mainPos; float posDelta;
                CollideRect mainRect, nextRect, lastRect;
                float scaleNext, scaleLast;
                Texture2D TryGetImage(int pos)
                {
                    Texture2D[] images = this._father._currentSongList.Images;
                    if(pos >= 0 && pos < images.Length) return images[pos];
                    return null;
                }
                public override void Draw()
                { 
                    Texture2D main = TryGetImage(mainPos);
                    if(main != null) FormalDraw(main, mainRect.ToRectangle(), Color.White * alpha);

                    Texture2D next = TryGetImage(mainPos + 1); 
                    if(next != null) FormalDraw(next, nextRect.ToRectangle(), 
                        new CollideRect(Vector2.Zero, next.Bounds.Size.ToVector2() * new Vector2(1, scaleNext)).ToRectangle(), 
                        Color.White * 0.55f);

                    Texture2D last = TryGetImage(mainPos - 1); 
                    if (last != null)
                    {
                        Vector2 bound = last.Bounds.Size.ToVector2();
                        FormalDraw(last, lastRect.ToRectangle(),
                            new CollideRect(new Vector2(0, bound.Y * (1 - scaleLast)), bound * new Vector2(1, scaleLast)).ToRectangle(),
                            Color.White * 0.55f
                        );
                    }
                }
            }
        }
    }
}
