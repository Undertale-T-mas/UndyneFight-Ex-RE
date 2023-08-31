using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace UndyneFight_Ex
{
    internal class ResourcesPackManager
    {
        private readonly SortedDictionary<int, ResourcesPack> packs = new();
        public KeyValuePair<int, ResourcesPack>[] AllPackages => packs.ToArray();
        public ResourcesPackManager()
        {

        }
        public Texture2D SoulCollide { get { foreach (var v in packs) if (v.Value.SoulCollide != null) return v.Value.SoulCollide; throw new System.IO.FileNotFoundException(); } }
        public Texture2D BoneHead { get { foreach (var v in packs) if (v.Value.BoneHead != null) return v.Value.BoneHead; throw new System.IO.FileNotFoundException(); } }
        public Texture2D BoneSlab { get { foreach (var v in packs) if (v.Value.BoneSlab != null) return v.Value.BoneSlab; throw new System.IO.FileNotFoundException(); } }
        public Texture2D WarningLine { get { foreach (var v in packs) if (v.Value.WarningLine != null) return v.Value.WarningLine; throw new System.IO.FileNotFoundException(); } }
        public Texture2D GBLaser { get { foreach (var v in packs) if (v.Value.GBLaser != null) return v.Value.GBLaser; throw new System.IO.FileNotFoundException(); } }
        public Texture2D ExplodeTrigger { get { foreach (var v in packs) if (v.Value.ExplodeTrigger != null) return v.Value.ExplodeTrigger; throw new System.IO.FileNotFoundException(); } }
        public Texture2D Player { get { foreach (var v in packs) if (v.Value.Player != null) return v.Value.Player; throw new System.IO.FileNotFoundException(); } }
        public Texture2D BrokenHeart { get { foreach (var v in packs) if (v.Value.BrokenHeart != null) return v.Value.BrokenHeart; throw new System.IO.FileNotFoundException(); } }
        public Texture2D LeftHeart { get { foreach (var v in packs) if (v.Value.LeftHeart != null) return v.Value.LeftHeart; throw new System.IO.FileNotFoundException(); } }
        public Texture2D RightHeart { get { foreach (var v in packs) if (v.Value.RightHeart != null) return v.Value.RightHeart; throw new System.IO.FileNotFoundException(); } }
        public Texture2D ShinyShield { get { foreach (var v in packs) if (v.Value.ShinyShield != null) return v.Value.ShinyShield; throw new System.IO.FileNotFoundException(); } }
        public Texture2D HpText { get { foreach (var v in packs) if (v.Value.HpText != null) return v.Value.HpText; throw new System.IO.FileNotFoundException(); } }
        public Texture2D HpBar { get { foreach (var v in packs) if (v.Value.HpBar != null) return v.Value.HpBar; throw new System.IO.FileNotFoundException(); } }
        public Texture2D BoneBody { get { foreach (var v in packs) if (v.Value.BoneBody != null) return v.Value.BoneBody; throw new System.IO.FileNotFoundException(); } }
        public Texture2D BoneTail { get { foreach (var v in packs) if (v.Value.BoneTail != null) return v.Value.BoneTail; throw new System.IO.FileNotFoundException(); } }
        public Texture2D Target { get { foreach (var v in packs) if (v.Value.Target != null) return v.Value.Target; throw new System.IO.FileNotFoundException(); } }
        public Texture2D Bullet { get { foreach (var v in packs) if (v.Value.Bullet != null) return v.Value.Bullet; throw new System.IO.FileNotFoundException(); } }
        public Texture2D FirePartical { get { foreach (var v in packs) if (v.Value.FirePartical != null) return v.Value.FirePartical; throw new System.IO.FileNotFoundException(); } }
        public Texture2D LightBall { get { foreach (var v in packs) if (v.Value.LightBall != null) return v.Value.LightBall; throw new System.IO.FileNotFoundException(); } }
        public Texture2D LightBallS { get { foreach (var v in packs) if (v.Value.LightBallS != null) return v.Value.LightBallS; throw new System.IO.FileNotFoundException(); } }
        public Texture2D Square { get { foreach (var v in packs) if (v.Value.Square != null) return v.Value.Square; throw new System.IO.FileNotFoundException(); } }
        public Texture2D Spear { get { foreach (var v in packs) if (v.Value.Spear != null) return v.Value.Spear; throw new System.IO.FileNotFoundException(); } }
        public Texture2D Spike { get { foreach (var v in packs) if (v.Value.Spike != null) return v.Value.Spike; throw new System.IO.FileNotFoundException(); } }
        public Texture2D Spider { get { foreach (var v in packs) if (v.Value.Spider != null) return v.Value.Spider; throw new System.IO.FileNotFoundException(); } }
        public Texture2D Pixiv { get { foreach (var v in packs) if (v.Value.Pixiv != null) return v.Value.Pixiv; throw new System.IO.FileNotFoundException(); } }
        public Texture2D BoxPiece { get { foreach (var v in packs) if (v.Value.BoxPiece != null) return v.Value.BoxPiece; throw new System.IO.FileNotFoundException(); } }
        public Texture2D Stuck1 { get { foreach (var v in packs) if (v.Value.Stuck1 != null) return v.Value.Stuck1; throw new System.IO.FileNotFoundException(); } }
        public Texture2D Stuck2 { get { foreach (var v in packs) if (v.Value.Stuck2 != null) return v.Value.Stuck2; throw new System.IO.FileNotFoundException(); } }
        public Texture2D Aimer { get { foreach (var v in packs) if (v.Value.Aimer != null) return v.Value.Aimer; throw new System.IO.FileNotFoundException(); } }
        public Texture2D DialogBox { get { foreach (var v in packs) if (v.Value.DialogBox != null) return v.Value.DialogBox; throw new System.IO.FileNotFoundException(); } }
        public Texture2D StopBar { get { foreach (var v in packs) if (v.Value.StopBar != null) return v.Value.StopBar; throw new System.IO.FileNotFoundException(); } }
        public Texture2D MovingBar { get { foreach (var v in packs) if (v.Value.MovingBar != null) return v.Value.MovingBar; throw new System.IO.FileNotFoundException(); } }
        public Texture2D[] platformSide { get { foreach (var v in packs) if (v.Value.platformSide[0] != null) return v.Value.platformSide; throw new System.IO.FileNotFoundException(); } }
        public Texture2D[] platform { get { foreach (var v in packs) if (v.Value.platform[0] != null) return v.Value.platform; throw new System.IO.FileNotFoundException(); } }
        public Texture2D[] heartPieces { get { foreach (var v in packs) if (v.Value.heartPieces[0] != null) return v.Value.heartPieces; throw new System.IO.FileNotFoundException(); } }
        public Texture2D[] GBStart { get { foreach (var v in packs) if (v.Value.GBStart[0] != null) return v.Value.GBStart; throw new System.IO.FileNotFoundException(); } }
        public Texture2D[] explodes { get { foreach (var v in packs) if (v.Value.explodes[0] != null) return v.Value.explodes; throw new System.IO.FileNotFoundException(); } }
        public Texture2D[] Act { get { foreach (var v in packs) if (v.Value.Act[0] != null) return v.Value.Act; throw new System.IO.FileNotFoundException(); } }
        public Texture2D[] Mercy { get { foreach (var v in packs) if (v.Value.Mercy[0] != null) return v.Value.Mercy; throw new System.IO.FileNotFoundException(); } }
        public Texture2D[] Item { get { foreach (var v in packs) if (v.Value.Item[0] != null) return v.Value.Item; throw new System.IO.FileNotFoundException(); } }
        public Texture2D[] Fight { get { foreach (var v in packs) if (v.Value.Fight[0] != null) return v.Value.Fight; throw new System.IO.FileNotFoundException(); } }
        public Texture2D[] GBShooting { get { foreach (var v in packs) if (v.Value.GBShooting[0] != null) return v.Value.GBShooting; throw new System.IO.FileNotFoundException(); } }
        public Texture2D[] Shield { get { foreach (var v in packs) if (v.Value.Shield[0] != null) return v.Value.Shield; throw new System.IO.FileNotFoundException(); } }
        public Texture2D[] Slides { get { foreach (var v in packs) if (v.Value.Slides[0] != null) return v.Value.Slides; throw new System.IO.FileNotFoundException(); } }
        public Texture2D[,,] Arrow { get { foreach (var v in packs) if (v.Value.Arrow[0, 0, 0] != null) return v.Value.Arrow; throw new System.IO.FileNotFoundException(); } }
        public void Load(ContentManager loader, string filePath)
        {
            ResourcesPack v;
            packs.Add(packs.Count, v = new ResourcesPack());
            v.Load(loader, filePath);
        }
    }
    public class ResourcesPack
    {
        public Texture2D SoulCollide { get; private set; }
        public Texture2D BoneHead { get; private set; }
        public Texture2D BoneSlab { get; private set; }
        public Texture2D WarningLine { get; private set; }
        public Texture2D GBLaser { get; private set; }
        public Texture2D ExplodeTrigger { get; private set; }
        public Texture2D Player { get; private set; }
        public Texture2D BrokenHeart { get; private set; }
        public Texture2D LeftHeart { get; private set; }
        public Texture2D RightHeart { get; private set; }
        public Texture2D ShinyShield { get; private set; }
        public Texture2D HpText { get; private set; }
        public Texture2D HpBar { get; private set; }
        public Texture2D BoneBody { get; private set; }
        public Texture2D BoneTail { get; private set; }
        public Texture2D Target { get; private set; }
        public Texture2D Bullet { get; private set; }
        public Texture2D FirePartical { get; private set; }
        public Texture2D LightBall { get; private set; }
        public Texture2D LightBallS { get; private set; }
        public Texture2D Square { get; private set; }
        public Texture2D Spear { get; private set; }
        public Texture2D Spike { get; private set; }
        public Texture2D Spider { get; private set; }
        public Texture2D Pixiv { get; private set; }
        public Texture2D BoxPiece { get; private set; }
        public Texture2D Stuck1 { get; private set; }
        public Texture2D Stuck2 { get; private set; }
        public Texture2D Aimer { get; private set; }
        public Texture2D DialogBox { get; private set; }
        public Texture2D StopBar { get; private set; }
        public Texture2D MovingBar { get; private set; }
        public Texture2D[] platformSide { get; private set; } = new Texture2D[2];
        public Texture2D[] platform { get; private set; } = new Texture2D[2];
        public Texture2D[] heartPieces { get; private set; } = new Texture2D[5];
        public Texture2D[] GBStart { get; private set; } = new Texture2D[5];
        public Texture2D[] explodes { get; private set; } = new Texture2D[4];
        public Texture2D[] Act { get; private set; } = new Texture2D[2];
        public Texture2D[] Mercy { get; private set; } = new Texture2D[2];
        public Texture2D[] Item { get; private set; } = new Texture2D[2];
        public Texture2D[] Fight { get; private set; } = new Texture2D[2];
        public Texture2D[] GBShooting { get; private set; } = new Texture2D[2];
        public Texture2D[] Shield { get; private set; } = new Texture2D[2];
        public Texture2D[] Slides { get; private set; } = new Texture2D[5];
        public Texture2D[,,] Arrow { get; private set; } = new Texture2D[2, 4, 4];

        private Texture2D TryLoad(ContentManager loader, string path)
        {
            return System.IO.File.Exists(loader.RootDirectory + "\\" + "path" + ".xnb") ? loader.Load<Texture2D>(path) : null;
        }
        public void Load(ContentManager loader, string path)
        {
            string old = loader.RootDirectory;
            Player = TryLoad(loader, "Sprites\\SOUL\\original");
            BrokenHeart = TryLoad(loader, "Sprites\\SOUL\\break");
            LeftHeart = TryLoad(loader, "Sprites\\SOUL\\leftSoul");
            RightHeart = TryLoad(loader, "Sprites\\SOUL\\rightSoul");
            SoulCollide = TryLoad(loader, "Sprites\\SOUL\\collide");
            for (int i = 0; i < 5; i++)
            {
                heartPieces[i] = TryLoad(loader, "Sprites\\SOUL\\shard" + i);
                GBStart[i] = TryLoad(loader, "Sprites\\GB\\s\\frame_" + i);
            }
            for (int i = 0; i < 4; i++)
                explodes[i] = TryLoad(loader, "Sprites\\Explodes\\smallExplode" + (i + 1));
            for (int i = 0; i < 2; i++)
                GBShooting[i] = TryLoad(loader, "Sprites\\GB\\p\\frame_" + i);

            Spear = TryLoad(loader, "Sprites\\bullet\\spear");
            Spike = TryLoad(loader, "Sprites\\Bone\\bone_spike");
            Spider = TryLoad(loader, "Sprites\\OtherBarrages\\spider");
            Pixiv = TryLoad(loader, "Sprites\\others\\pixiv");
            FirePartical = TryLoad(loader, "Sprites\\others\\firePartical");
            LightBall = TryLoad(loader, "Sprites\\others\\lightBall");
            LightBallS = TryLoad(loader, "Sprites\\others\\lightBallS");
            Square = TryLoad(loader, "Sprites\\others\\square");
            BoxPiece = TryLoad(loader, "Sprites\\others\\boxPiece");

            Stuck1 = TryLoad(loader, "Sprites\\others\\GBStuck1");
            Stuck2 = TryLoad(loader, "Sprites\\others\\GBStuck2");

            for (int i = 0; i < 4; i++)
            {
                Arrow[0, 0, i] = TryLoad(loader, "Sprites\\bullet\\blue" + i);
                Arrow[1, 0, i] = TryLoad(loader, "Sprites\\bullet\\red" + i);
                Arrow[0, 1, i] = TryLoad(loader, "Sprites\\bullet\\circle_blue" + i);
                Arrow[1, 1, i] = TryLoad(loader, "Sprites\\bullet\\circle_red" + i);
                Arrow[0, 2, i] = TryLoad(loader, "Sprites\\bullet\\rot_blue" + i);
                Arrow[1, 2, i] = TryLoad(loader, "Sprites\\bullet\\rot_red" + i);
                Arrow[0, 3, i] = TryLoad(loader, "Sprites\\bullet\\tran_blue" + i);
                Arrow[1, 3, i] = TryLoad(loader, "Sprites\\bullet\\tran_red" + i);
            }
            Target = TryLoad(loader, "Sprites\\bullet\\target");
            Bullet = TryLoad(loader, "Sprites\\bullet\\gunBullet");

            Shield[0] = TryLoad(loader, "Sprites\\SOUL\\shield_blue");
            Shield[1] = TryLoad(loader, "Sprites\\SOUL\\shield_red");
            ShinyShield = TryLoad(loader, "Sprites\\SOUL\\shield_shiny");

            HpText = TryLoad(loader, "Sprites\\hp_show\\hp");
            HpBar = TryLoad(loader, "Sprites\\hp_show\\hp_bar");

            BoneBody = TryLoad(loader, "Sprites\\Bone\\bone_body");
            BoneTail = TryLoad(loader, "Sprites\\Bone\\bone_down");
            BoneHead = TryLoad(loader, "Sprites\\Bone\\bone_up");
            BoneSlab = TryLoad(loader, "Sprites\\Bone\\bone_slab");
            WarningLine = TryLoad(loader, "Sprites\\Bone\\warning_line");

            GBLaser = TryLoad(loader, "Sprites\\GB\\laser");

            ExplodeTrigger = TryLoad(loader, "Sprites\\Explodes\\explodeTrigger");

            platform[0] = TryLoad(loader, "Sprites\\Platform\\platform_body");
            platformSide[1] = TryLoad(loader, "Sprites\\Platform\\platform_body2");
            platformSide[0] = TryLoad(loader, "Sprites\\Platform\\platform_side");
            platformSide[1] = TryLoad(loader, "Sprites\\Platform\\platform_side2");

            for (int i = 0; i < 2; i++)
            {
                Fight[i] = TryLoad(loader, "Sprites\\FightSprites\\atk_" + i);
                Act[i] = TryLoad(loader, "Sprites\\FightSprites\\act_" + i);
                Item[i] = TryLoad(loader, "Sprites\\FightSprites\\itm_" + i);
                Mercy[i] = TryLoad(loader, "Sprites\\FightSprites\\mry_" + i);
            }
            Aimer = TryLoad(loader, "Sprites\\FightSprites\\aimer");
            DialogBox = TryLoad(loader, "Sprites\\FightSprites\\dialogBox");
            StopBar = TryLoad(loader, "Sprites\\FightSprites\\stop_bar");
            MovingBar = TryLoad(loader, "Sprites\\FightSprites\\moving_bar");
            for (int i = 0; i <= 5; i++)
                Slides[i] = TryLoad(loader, "Sprites\\FightSprites\\frames\\frame_" + i);
            loader.RootDirectory = old;
        }
    }
}