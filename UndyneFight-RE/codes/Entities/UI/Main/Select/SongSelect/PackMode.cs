﻿using Microsoft.Xna.Framework;
using System; 
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        private partial class SongSelector
        {
            private class PackMode : SongList
            {
                public PackMode(SongSelector father) : base(father)
                {
                    RootSelection last = null;
                    foreach(SongPack pack in songPacks)
                    {
                        Vector2 curPosition = new(280, 59);
                        RootSelection root;
                        this.AddChild(root = new RootSelection(this, curPosition, pack.Title));
                        if (last != null) { root.Last = last; }
                        curPosition.Y += 63;
                        root.DefaultScale = 1.3f;
                        foreach(IWaveSet waveSet in pack.AllSongs)
                        {
                            LeafSelection selection;
                            this.AddChild(selection = new LeafSelection(root, curPosition + new Vector2(12, 0), waveSet.FightName)
                            {
                                DefaultScale = 1.1f,
                                SongAvailable = pack.Availables.Contains(waveSet.Music)
                            });
                            curPosition.Y += 55;
                        }
                        last = root;
                        Head = root;
                    }
                } 

                public override void Draw()
                { 
                }
            }
        }
    }
}
