using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        private partial class SongSelector
        {
            private class LetterMode : SongList
            {
                public LetterMode(SongSelector father) : base(father)
                {
                    LinkedList<SongPack> packs = SortChar( );

                    Generate(packs);
                }
                static char Specifier(char ch)
                {
                    if (ch >= 'a') ch = (char)(ch - ('a' - 'A'));
                    return ch;
                }
                private static LinkedList<SongPack> SortChar()
                {
                    SortedDictionary<char, List<Type>> letterPacks = new();
                    void TryAdd(char ch, Type set)
                    {
                        if (!letterPacks.ContainsKey(ch)) letterPacks.Add(ch, new());
                        letterPacks[ch].Add(set); 
                    }

                    // Re-generate the pack  
                    foreach (Type type in FightSystem.GetAllAvailables())
                    {
                        IWaveSet waveSet;
                        object obj = Activator.CreateInstance(type);
                        if (obj is IWaveSet) waveSet = obj as IWaveSet;
                        else waveSet = (obj as IChampionShip).GameContent;
                        if (waveSet.Attributes == null || waveSet.Attributes.Hidden) continue;
                        var letter = waveSet.FightName[0];
                        if (!string.IsNullOrEmpty(waveSet.Attributes.DisplayName))
                            letter = waveSet.Attributes.DisplayName[0];

                        TryAdd(Specifier(letter), type);
                    }
                    LinkedList<SongPack> packs = new();
                    foreach (var pair in letterPacks)
                    {
                        SongSet set = new(pair.Key.ToString());
                        foreach (Type song in pair.Value)
                        {
                            set.Push(song);
                        }

                        SongPack pack = new(set, pair.Key.ToString());
                        packs.AddLast(pack);
                    }

                    return packs;
                }

                public void Generate(IEnumerable<SongPack> packs)
                {
                    RootSelection last = null;
                    foreach (SongPack pack in packs)
                    {
                        Vector2 curPosition = new(280, 59);
                        RootSelection root;
                        this.AddChild(root = new RootSelection(this, curPosition, pack.Title));
                        if (last != null) { root.Last = last; }
                        curPosition.Y += 63;
                        root.DefaultScale = 1.3f;
                        foreach (IWaveSet waveSet in pack.AllSongs)
                        {
                            var Attributes = waveSet.Attributes;
                            if (Attributes != null && Attributes.Hidden) continue;
                            LeafSelection selection;
                            string fullName = waveSet.Music + waveSet.FightName;
                            string DisplayName = waveSet.FightName;
                            if (!string.IsNullOrEmpty(Attributes.DisplayName)) DisplayName = Attributes.DisplayName;
                            this.AddChild(selection = new LeafSelection(root, curPosition + new Vector2(12, 0), DisplayName)
                            {
                                DefaultScale = 1.1f,
                                SongAvailable = pack.Availables.Contains(waveSet.Music),
                                Illustration = pack.Images.ContainsKey(fullName) ? pack.Images[fullName] : null,
                                FightObject = pack.ChampionshipMap.ContainsKey(waveSet) ? pack.ChampionshipMap[waveSet] : waveSet
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
