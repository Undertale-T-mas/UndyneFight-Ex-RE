using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        private partial class SongSelector
        {
            private class DiffComplexMode : DiffMode
            {
                public DiffComplexMode(SongSelector father) : base(father)
                {
                    LinkedList<SongPack> packs = SortDifficulty(father._virtualFather.CurrentDifficulty);

                    Generate(packs);
                }
                internal override void ReSort(Difficulty dif)
                {
                    base.ReSort(dif);

                    IWaveSet waveSet = this._father._virtualFather.SongSelected;

                    this.Generate(SortDifficulty(dif), waveSet.Music, waveSet.FightName);
                    this.SetObjects();
                }

                private static LinkedList<SongPack> SortDifficulty(Difficulty difSelected)
                {
                    SortedDictionary<int, List<Type>> diffPacks = new();
                    void TryAdd(int dif, Type set)
                    {
                        if (!diffPacks.ContainsKey(dif)) diffPacks.Add(dif, new());
                        diffPacks[dif].Add(set);

                        if (dif == 19)
                        {
                            ;
                        }
                    }

                    // Re-generate the pack
                    Difficulty cur = difSelected;

                    foreach (Type type in FightSystem.AllSongs.Values)
                    {
                        IWaveSet waveSet;
                        object obj = Activator.CreateInstance(type);
                        if (obj is IWaveSet)
                        {
                            waveSet = obj as IWaveSet;
                        }
                        else
                        {
                            waveSet = (obj as IChampionShip).GameContent;
                        }
                        if (waveSet.Attributes == null || waveSet.Attributes.Hidden) continue;
                        var clearDifs = waveSet.Attributes.ComplexDifficulty;

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
                        foreach (Type song in pair.Value)
                        {
                            set.Push(song);
                        }

                        SongPack pack = new(set, pair.Key.ToString());
                        packs.AddFirst(pack);
                    }

                    return packs;
                }
            }
            private class DiffClearMode : DiffMode
            {
                public DiffClearMode(SongSelector father) : base(father)
                {
                    LinkedList<SongPack> packs = SortDifficulty(father._virtualFather.CurrentDifficulty);

                    Generate(packs);
                }
                internal override void ReSort(Difficulty dif)
                {
                    base.ReSort(dif);

                    IWaveSet waveSet = this._father._virtualFather.SongSelected;

                    this.Generate(SortDifficulty(dif), waveSet.Music, waveSet.FightName);
                    this.SetObjects();
                }
                internal override void ReSort(bool order)
                {
                    base.ReSort(order);

                    IWaveSet waveSet = this._father._virtualFather.SongSelected;

                    this.Generate(SortDifficulty(LastDifficulty), waveSet?.Music, waveSet?.FightName);
                    this.SetObjects();
                }
                private static LinkedList<SongPack> SortDifficulty(Difficulty difSelected)
                {
                    SortedDictionary<int, List<Type>> diffPacks = new();
                    void TryAdd(int dif, Type set)
                    {
                        if (!diffPacks.ContainsKey(dif)) diffPacks.Add(dif, new());
                        diffPacks[dif].Add(set);

                        if (dif == 19)
                        {
                            ;
                        }
                    }

                    // Re-generate the pack
                    Difficulty cur = difSelected;

                    foreach (Type type in FightSystem.AllSongs.Values)
                    {
                        IWaveSet waveSet;
                        object obj = Activator.CreateInstance(type);
                        if (obj is IWaveSet)
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
                        foreach (Type song in pair.Value)
                        {
                            set.Push(song);
                        }

                        SongPack pack = new(set, pair.Key.ToString());
                        packs.AddFirst(pack);
                    }

                    return packs;
                }
            }
            private class DiffMode : SongList
            {
                public DiffMode(SongSelector father) : base(father)
                { 
                }

                protected Difficulty LastDifficulty => this.last;

                Difficulty last = Difficulty.NotSelected;
                protected List<RootSelection> rootSelections;
                protected void Generate(IEnumerable<SongPack> diffPacks, string playDefault = null, string fightDefault = null)
                {
                    if(cancelGenerate)
                    {
                        cancelGenerate = false;
                        return;
                    }
                    if (curOrder)
                    { // reverse
                        diffPacks = diffPacks.Reverse();
                    }
                    rootSelections = new();
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
                            string fullName = waveSet.Music + waveSet.FightName;
                            this.AddChild(selection = new LeafSelection(root, curPosition + new Vector2(12, 0), waveSet.Attributes.DisplayName)
                            {
                                DefaultScale = 1.1f,
                                SongAvailable = pack.Availables.Contains(waveSet.Music),
                                Illustration = pack.Images.ContainsKey(fullName) ? pack.Images[fullName] : null,
                                FightObject = pack.ChampionshipMap.ContainsKey(waveSet) ? pack.ChampionshipMap[waveSet] : waveSet
                            });
                            if(waveSet.Music == playDefault && waveSet.FightName == fightDefault)
                            {
                                root.State = SelectState.Selected; 
                                GameStates.InstanceCreate(
                                    new InstantEvent(1, () => {
                                        this.focusID = -1;
                                        this._currentFocus = this._lastSelected = selection;
                                        selection.State = SelectState.Selected;
                                        this._father.SelectedID = this.SelectedID = FocusID;
                                    })
                                    ); ;
                            }
                            curPosition.Y += 55;
                        }
                        last = root;
                        Head = root;
                        rootSelections.Add(root);
                    }
                     
                }

                public override void Draw()
                { 
                }
                private bool cancelGenerate = false;

                internal virtual void ReSort(Difficulty dif)
                {
                    if(last == dif)
                    {
                        cancelGenerate = true;
                        return;
                    }
                    last = dif;
                    this.ChildObjects.Clear();
                    curOrder = this._father._sortOrder.IsReverse;
                }
                bool curOrder = false;
                internal virtual void ReSort(bool order)
                {
                    curOrder = order;
                    this.ChildObjects.Clear(); 
                }
            }
        }
    }
}
