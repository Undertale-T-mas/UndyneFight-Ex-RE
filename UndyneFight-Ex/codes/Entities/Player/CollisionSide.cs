namespace UndyneFight_Ex.Entities
{
    public partial class Player
    {
        public partial class Heart
        {
            public partial class Shield
            {
                public class CollisionSide : Entity
                {
                    private const float FarTime = 90;
                    Shield father;
                    bool[] blockedArrow = { false, false, false, false };
                    float[] timeDelayed = { FarTime, FarTime, FarTime, FarTime };
                    float[] tapTime = { FarTime, FarTime, FarTime, FarTime };
                    float[] holdTime = { FarTime, FarTime, FarTime, FarTime };

                    public CollisionSide()
                    {
                        UpdateIn120 = true;
                    }
                    public override void Start()
                    {
                        father = FatherObject as Shield;
                        base.Start();
                    }
                    public override void Update()
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (!blockedArrow[i])
                                timeDelayed[i] += 0.5f;
                            tapTime[i] += 0.5f;
                            holdTime[i] += 0.5f;
                            if (GameStates.IsKeyPressed120f(father.UpdateKeys[i]))
                            {
                                blockedArrow[i] = false;
                                timeDelayed[i] = 0;
                                tapTime[i] = 0;
                            }
                            if (blockedArrow[i] && father.Way != i)
                            {
                                blockedArrow[i] = false;
                                timeDelayed[i] = FarTime;
                            }
                            if (GameStates.IsKeyDown(father.UpdateKeys[i])) holdTime[i] = 0;
                            if (GameStates.IsKeyPressed120f(father.UpdateKeys[i]) || (father.AttachingGB && father.attachedGB.Way == i))
                            {
                                timeDelayed[i] = 0;
                            }
                        }
                    }
                    public override void Draw()
                    {
                        /*   Centre = new(320, 240);
                           for(int i = 0; i < 4; i++)
                           {
                               Vector2 position = Centre + MathUtil.GetVector2(77, i * 90) + new Vector2(0, father.ColorType * 30);
                               FightResources.Font.NormalFont.CentreDraw(timeDelayed[i].ToString("F1"), position, 0.5f * (blockedArrow[i] ? Color.Gold : father.drawingColor));
                           }*/
                    }
                    public void ArrowBlock(int direction)
                    {
                        blockedArrow[direction] = (father.Way == direction);
                        timeDelayed[direction] = blockedArrow[direction] ? 0 : FarTime;
                        tapTime[direction] = FarTime;
                    }

                    public float TimeOf(int way)
                    {
                        return timeDelayed[way];
                    }
                    public float TapTimeOf(int way)
                    {
                        return tapTime[way];
                    }
                    public float HoldTimeOf(int way)
                    {
                        return holdTime[way];
                    }
                }
            }
        }
    }
}