using Microsoft.Xna.Framework;
using static UndyneFight_Ex.Fight.FightStates;
using static UndyneFight_Ex.FightResources.FightSprites;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Fight
{
    public class MenuButtons
    {
        public class FightButton : FightSelection
        {
            internal static FightButton instance;
            public FightButton() : base(fight[0])
            {
                instance = this;
                Depth = 0.2f;
                Centre = new Vector2(128 - 50, 450);
            }

            private bool isCollide = false;
            public override Vector2 HeartStayPosition => Centre - new Vector2(38, 0);

            public override void Draw()
            {
                FormalDraw(fight[isCollide ? 1 : 0], collidingBox.ToRectangle(), Color.White * ClassicFight.InterActive.UIAlpha);
            }
            public override void Update()
            {
                isCollide = GetCollide();
                base.Update();
            }
            public override void Dispose()
            {
                base.Dispose();
            }
            public override void ZPressed()
            {
                firstDecision = 0;
                InstanceCreate(EnemyShowing.GetEnemySelector());
            }
        }
        public class ActButton : FightSelection
        {
            internal static ActButton instance;
            public ActButton() : base(act[0])
            {
                instance = this;
                Depth = 0.2f;
                Centre = new Vector2(128 * 2 - 20, 450);
            }
            private bool isCollide = false;
            public override Vector2 HeartStayPosition => Centre - new Vector2(38, 0);

            public override void Draw()
            {
                FormalDraw(act[isCollide ? 1 : 0], collidingBox.ToRectangle(), Color.White * ClassicFight.InterActive.UIAlpha);
            }
            public override void Update()
            {
                isCollide = GetCollide();
                base.Update();
            }

            public override void ZPressed()
            {
                firstDecision = 1;
                InstanceCreate(EnemyShowing.GetEnemySelector());
            }
        }
        public class ItemButton : FightSelection
        {
            internal static ItemButton instance;
            public ItemButton() : base(item[0])
            {
                instance = this;
                Depth = 0.2f;
                Centre = new Vector2(128 * 3 + 20, 450);
            }

            private bool isCollide = false;
            public override Vector2 HeartStayPosition => Centre - new Vector2(38, 0);

            public override void Draw()
            {
                FormalDraw(item[isCollide ? 1 : 0], collidingBox.ToRectangle(), Color.White * ClassicFight.InterActive.UIAlpha);
            }
            public override void Update()
            {
                isCollide = GetCollide();
                base.Update();
            }

            public override void ZPressed()
            {
                if (items != null && items.Count > 0)
                    InstanceCreate(ItemShowing.GetItemSelector());
            }
        }
        public class MercyButton : FightSelection
        {
            internal static MercyButton instance;
            public MercyButton() : base(mercy[0])
            {
                instance = this;
                Depth = 0.2f;
                Centre = new Vector2(128 * 4 + 50, 450);
            }
            private bool isCollide = false;
            public override Vector2 HeartStayPosition => Centre - new Vector2(38, 0);

            public override void Draw()
            {
                FormalDraw(mercy[isCollide ? 1 : 0], collidingBox.ToRectangle(), Color.White * ClassicFight.InterActive.UIAlpha);
            }
            public override void Update()
            {
                isCollide = GetCollide();
                base.Update();
            }

            public override void ZPressed()
            {
                firstDecision = 0;
                InstanceCreate(EnemyShowing.GetEnemySelector());
            }
        }
    }
}