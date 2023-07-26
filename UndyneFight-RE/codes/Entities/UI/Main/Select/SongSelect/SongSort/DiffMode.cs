using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Net.Security;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        private partial class SongSelector
        {
            private class DiffClearMode : DiffMode
            {
                public DiffClearMode(SongSelector father) : base(father)
                {
                    SortedDictionary<int, List<Type>> diffPacks = new();

                    void TryAdd(int dif, Type set)
                    {
                        if (!diffPacks.ContainsKey(dif)) diffPacks.Add(dif, new());
                        diffPacks[dif].Add(set);

                        if(dif == 19) {
                            ; }
                    }

                    // Re-generate the pack
                    Difficulty cur = father._virtualFather.DiffSelect.FocusDifficulty;

                    foreach (Type type in FightSystem.AllSongs.Values)
                    {
                        IWaveSet waveSet;
                        object obj = Activator.CreateInstance(type);
                        if(obj is IWaveSet)
                        {
                            waveSet = obj as IWaveSet;
                        }
                        else
                        {
                            waveSet = (obj as IChampionShip).GameContent;
                        }
                        if (waveSet.Attributes == null || waveSet.Attributes.Hidden) continue;
                        var clearDifs = waveSet.Attributes.CompleteDifficulty;

                        if (clearDifs.ContainsKey(cur)) TryAdd((int)clearDifs[cur], type);
                        else
                            for (Difficulty dif = cur - 1; dif >= 0; dif--)
                            {
                                if (clearDifs.ContainsKey(dif))
                                {
                                    TryAdd((int)clearDifs[dif], type);
                                    break;
                                }
                            }
                    }
                    LinkedList<SongPack> packs = new();
                    foreach (var pair in diffPacks)
                    {
                        SongSet set = new(pair.Key.ToString());
                        foreach(Type song in pair.Value)
                        {
                            set.Push(song);
                        }

                        SongPack pack = new(set, pair.Key.ToString());
                        packs.AddFirst(pack);
                    }
                    Generate(packs);
                } 
            }
            private class DiffMode : SongList
            {
                public DiffMode(SongSelector father) : base(father)
                { 
                }

                protected RootSelection Generate(IEnumerable<SongPack> diffPacks)
                {
                    RootSelection last = null;
                    foreach (var pack in diffPacks)
                    {
                        Vector2 curPosition = new(280, 59);
                        RootSelection root;
                        this.AddChild(root = new RootSelection(this, curPosition, pack.Title));
                        if (last != null) { root.Last = last; }
                        curPosition.Y += 63;
                        root.DefaultScale = 1.3f;
                        foreach (IWaveSet waveSet in pack.AllSongs)
                        {
                            if (waveSet.Attributes != null && waveSet.Attributes.Hidden) continue;
                            LeafSelection selection;
                            this.AddChild(selection = new LeafSelection(root, curPosition + new Vector2(12, 0), waveSet.FightName)
                            {
                                DefaultScale = 1.1f,
                                SongAvailable = pack.Availables.Contains(waveSet.Music),
                                Illustration = pack.Images.ContainsKey(waveSet.Music) ? pack.Images[waveSet.Music] : null,
                                FightObject = pack.ChampionshipMap.ContainsKey(waveSet) ? pack.ChampionshipMap[waveSet] : waveSet
                            });
                            curPosition.Y += 55;
                        }
                        last = root;
                        Head = root;
                    }

                    return last;
                }

                public override void Draw()
                { 
                }
            }
        }
    }
}
