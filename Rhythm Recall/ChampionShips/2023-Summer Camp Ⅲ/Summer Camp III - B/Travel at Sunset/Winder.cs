using Microsoft.Xna.Framework;
using UndyneFight_Ex;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    internal partial class Traveler_at_Sunset
    {
        public partial class Project
        { 
            private class Winder : Entity
            {
                public float Intensity { set; get; } = 10;
                private float Speed { set; get; } = 40f;
                public float Length { set; get; } = 300f;
                public float BasicSpeed { set; get; } = 1f;
                public Color DrawingColor { set; get; } = Color.White;
                public bool Direction { set; get; } = false;
                public float Width { get; set; } = 1.5f;

                public Winder()
                {
                    UpdateIn120 = true;
                }
                public float timer = 0;
                public override void Draw()
                {
                    
                    
                }

                public override void Update()
                {
                    timer++;
                    if (timer % Intensity < 1)
                    {
                        Speed = 40f * BasicSpeed;
                        Length = 150f * (BasicSpeed + 1);
                        CreateEntity(new Wind(Speed * Rand(0.8f, 1.4f), Length, DrawingColor, Width, Direction));
                    }
                }
                class Wind : Entity
                {
                    float Speed;
                    float Width = 1.5f;
                    Vector2 point1;
                    Vector2 point2;
                    Color color;
                    public Wind(float Speed, float length, Color color, float width, bool dir = false)
                    {
                        this.Width = width;
                        this.color = color;
                        this.Speed = Speed;
                        if (dir)
                        {
                            this.Speed = -Speed;
                            point1 = new(-20, Rand(10, 470));
                            point2 = new(-20 - length, LastRand);
                        }
                        else
                        {
                            point1 = new(660, Rand(10, 470));
                            point2 = new(660 + length, LastRand);
                        }
                    }
                    float timer = 0;
                    public float Colordepth = Rand(0.300f, 0.500f);
                    public override void Draw()
                    {
                        DrawingLab.DrawLine(point1, point2, Width, color * Colordepth, 0.1f);
                    }

                    public override void Update()
                    {
                        timer++;
                        point1 += new Vector2(-Speed, 0);
                        point2 += new Vector2(-Speed, 0);
                        if (timer >= 900 / Speed + 30) this.Dispose();
                    }
                }
            }
        }
    }
}