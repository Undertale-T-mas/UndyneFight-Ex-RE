using Microsoft.Xna.Framework;
using Rhythm_Recall.Engine;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
	internal class Donki : IChampionShip
	{
		Dictionary<string, Difficulty> dif = new();
		public Donki()
		{
			dif.Add("Master", Difficulty.Hard);
			dif.Add("U-TA-GE", Difficulty.ExtremePlus);
		}
		public IWaveSet GameContent => new Project();
		public Dictionary<string, Difficulty> DifficultyPanel => dif;
		class Project : WaveConstructor, IWaveSet
		{
			public Project() : base(62.5f / (166.99f / 60f)) { }
			public string Music => "Donki";
			public string FightName => "Donki";
			public SongInformation Attributes => new Information();
			class Information : SongInformation
			{
				public override string SongAuthor => "Maimi Tanaka";
				public override string BarrageAuthor => "TanaKa";
				public override string DisplayName => !AprilSettings.IsAprilFool ? "Miracle Shopping": "Minecraft Revenge";
				public override string Extra => GameStates.difficulty == 3 ? "Master" : "Re: Master  :thinking:";
				public override Dictionary<Difficulty, float> CompleteDifficulty => new(
					new KeyValuePair<Difficulty, float>[]
					{
						new(Difficulty.Hard, 12),
					});
				public override Dictionary<Difficulty, float> ComplexDifficulty => new(
					new KeyValuePair<Difficulty, float>[]
					{
						new(Difficulty.Hard, 12),
					});
				public override Dictionary<Difficulty, float> APDifficulty => new(
					new KeyValuePair<Difficulty, float>[]
					{
						new(Difficulty.Hard, 15),
					});
			}
			public void Start()
			{
				SetSoul(1);
				TP();
				SetGreenBox();
				Settings.GBAppearVolume = 0;
				Settings.GreenTap = true;
				bool jump = false;
				if (jump)
				{
					float beat = 357;
					PlayOffset = GametimeDelta = BeatTime(beat);
				}
			}
			#region non
			public void Noob()
			{

			}
			public void Easy()
			{

			}
			public void Normal()
			{

			}
			public void Extreme()
			{

			}
			#endregion

			public void Hard()
			{
				if (InBeat(2.25f))
				{
					CreateChart(BeatTime(4), BeatTime(2), 6, new string[]
					{
						"R", "", "R", "", "R", "", "", "(#5#R)(R1)",
						"", "", "", "", "", "", "", "",
						"R1", "", "R1", "", "", "", "R1", "",
						"", "", "R1", "", "R1", "", "", "",

						"R1", "", "R1", "", "R1", "", "", "(#5#R1)(R)",
						"", "", "", "", "", "", "", "",
						"R", "", "R", "", "R", "", "R", "",
						"", "", "", "R", "", "", "", "R",
						"", "", "", "R", "", "", "", "(#5#R)(R1)",
						"", "", "", "", "", "", "", "",
						"R1", "", "", "R1", "", "", "", "R1",
						"", "", "", "R1", "", "", "", "R1",
						"", "", "R1", "", "R1", "", "R1", "",
					});
				}
				if (InBeat(30.5f))
				{
					CreateChart(BeatTime(4), BeatTime(2), 6, new string[]
					{
						"R", "", "+1", "", "+1", "", "+1", "",
						"", "(R)(R1)", "", "", "", "", "", "(R)(R1)",
						"", "", "", "", "", "(R)(R1)", "", "",
						"", "", "", "", "R", "D", "", "",
						"R", "D", "", "", "R", "D", "", "",
						"R", "D", "", "", "R", "D", "", "",
						"R", "D", "", "", "R", "D", "", "",
						"R1", "D1", "", "", "R1", "D1", "", "",
						"R1", "D1", "", "", "R1", "D1", "", "",
						"R1", "D1", "", "", "R1", "D1", "", "",
						"R1", "D1", "", "", "R1", "D1", "", "",
						"($0)($2)", "", "", "", "R", "D", "", "",
						"($0)($2)", "", "", "", "R", "D", "", "",
						"($0)($2)", "", "", "", "R", "D", "", "",
						"($0)($2)", "", "", "", "R", "D", "", "",
						"($01)($21)", "", "", "", "R1", "D1", "", "",
						"($01)($21)", "", "", "", "R1", "D1", "", "",
						"($01)($21)", "", "", "", "R1", "D1", "", "",
						"($01)($21)", "", "", "", "R1", "D1", "", "",
					});
				}
				if (InBeat(68.5f))
				{
					CreateChart(BeatTime(4), BeatTime(2), 6, new string[]
					{
						"#1.5#R", "", "", "", "D", "", "", "",
						"#1.5#R", "", "", "", "D", "", "", "",
						"#1.5#R", "", "", "", "D", "", "", "",
						"#1.5#R", "", "", "", "D", "", "", "",
						"#3.5#R", "", "", "", "D", "", "", "",
						"D", "", "", "", "D", "", "", "",
						"#3.5#R", "", "", "", "D", "", "", "",
						"D", "", "", "", "D", "", "", "",

						"#1.5#R1", "", "", "", "D1", "", "", "",
						"#1.5#R1", "", "", "", "D1", "", "", "",
						"#1.5#R1", "", "", "", "D1", "", "", "",
						"#1.5#R1", "", "", "", "D1", "", "", "",
						"#3.5#R1", "", "", "", "D1", "", "", "",
						"D1", "", "", "", "D1", "", "", "",
						"#3.5#R1", "", "", "", "D1", "", "", "",
						"D1", "", "", "", "D1", "", "", "",

						"#1.5#R", "", "", "", "D", "", "", "",
						"#1.5#R1", "", "", "", "D1", "", "", "",
						"#1.5#R", "", "", "", "D", "", "", "",
						"#1.5#R1", "", "", "", "D1", "", "", "",
						"#3.5#R", "", "", "", "D", "", "", "",
						"D", "", "", "", "D", "", "", "",
						"#3.5#R1", "", "", "", "D1", "", "", "",
						"D1", "", "", "", "D1", "", "", "",
						"#1.5#R", "", "", "", "D", "", "", "",
						"#1.5#R1", "", "", "", "D1", "", "", "",
						"(#3#R1)(#3#D)", "", "", "", "", "", "", "",
						"", "", "", "", "", "", "", "",
						"D", "D", "D", "D", "D", "D", "D", "D",
						"D", "D", "D", "D", "D", "D", "D", "D",
						"(R)(R1)", "", "", "", "(R)(R1)", "", "", "",
						"(R)(R1)", "", "", "", "(R)(R1)", "", "", "",
					});
				}
				if (InBeat(132.5f))
				{
					CreateChart(BeatTime(4), BeatTime(2), 6, new string[]
					{
						"R", "", "", "", "(#0.75#+0)(*+0)", "", "", "<<1.75",
						"D1", "", "", "", "(#0.75#+01)(*+01)", "", "", "",
						"(D)(D1)", "", "", "", "R", "R1", "", "",
						"", "(#1#R)(#1#D1)", "", "", "", "", "", "",
						"R", "", "", "", "(#0.75#+0)(*+0)", "", "", "<<1.75",
						"D1", "", "", "", "(#0.75#+01)(*+01)", "", "", "",
						"#2#R", "", "", "", "#1#R1", "", "", "",
						"", "", "R", "", "R", "", "", "#2#R",
						"", "", "", "", "", "", "", "",
						"", "", "", "", "", "", "", "R",
						"D", "", "", "R", "D", "", "", "R",
						"D", "", "", "R", "D", "", "", "R",
						"D", "", "", "R", "D", "", "", "R",
						"D", "", "", "R", "D", "", "", "#7#R",
						"", "", "", "", "D", "", "", "",
						"D", "", "", "", "D", "", "", "",
						"D", "", "", "", "D", "", "", "",
						"D", "", "", "", "D", "", "", "",
					});
				}
				if (InBeat(164.5f))
				{
					CreateChart(BeatTime(4), BeatTime(2), 6, new string[]
					{
						"R", "", "", "", "(#0.75#+0)(*+0)", "", "", "<<1.75",
						"D1", "", "", "", "(#0.75#+01)(*+01)", "", "", "",
						"(D)(D1)", "", "", "", "R", "R1", "", "",
						"", "(#1#R)(#1#D1)", "", "", "", "", "", "",
						"R", "", "", "", "(#0.75#+0)(*+0)", "", "", "<<1.75",
						"D1", "", "", "", "(#0.75#+01)(*+01)", "", "", "",
						"#2#R", "", "", "", "#1#R1", "", "", "",
						"", "", "R1", "", "R1", "", "", "#2#R1",
						"", "", "", "", "", "", "", "",
						"", "", "", "", "", "", "", "R1",
						"D1", "", "", "R1", "D1", "", "", "R1",
						"D1", "", "", "R1", "D1", "", "", "R1",
						"D1", "", "", "R1", "D1", "", "", "R1",
						"D1", "", "", "R1", "D1", "", "", "#5#R1",
						"", "", "", "", "D1", "", "", "",
						"D1", "", "", "", "D1", "", "", "",
						"", "", "", "(R)(R1)", "", "", "", "(R)(R1)",
						"", "", "", "(R)(R1)", "", "(R)(R1)", "", "(#1#R)(#1#D1)",
					});
				}
				if (InBeat(197.5f) || InBeat(213.5f))
				{
					CreateChart(BeatTime(4), BeatTime(2), 6, new string[]
					{
						"R", "", "", "", "R", "", "", "",
						"R", "", "", "", "#1#R", "", "", "",
						"", "", "#1#R", "", "", "", "", "",
						"R", "", "", "", "R1", "", "", "",
						"R1", "", "", "", "R1", "", "", "",
						"R1", "", "", "", "#1#R1", "", "", "",
						"", "", "#1#R1", "", "", "", "", "",
						"R1", "", "", "", "R", "", "", "",
					});
				}
				if (InBeat(229.5f))
				{
					CreateChart(BeatTime(4), BeatTime(2), 6, new string[]
					{
						"#8#R", "", "", "", "+1", "", "", "",
						"+1", "", "", "", "+1", "", "", "",
						"+1", "", "", "", "+1", "", "", "",
						"+1", "", "", "", "+1", "", "", "",
						"#8#D1", "", "", "", "+11", "", "", "",
						"+11", "", "", "", "+11", "", "", "",
						"+11", "", "", "", "+11", "", "", "",
						"+11", "", "", "", "+11", "", "", "",

						"#8#R", "", "", "", "-1", "", "", "",
						"-1", "", "", "", "-1", "", "", "",
						"-1", "", "", "", "-1", "", "", "",
						"-1", "", "", "", "-1", "", "", "",
						"#6#D1", "", "", "", "-11", "", "", "",
						"-11", "", "", "", "-11", "", "", "",
						"-11", "", "", "", "-11", "", "", "",
						"-11", "", "", "", "-11", "", "", "",
					});
				}
				if (InBeat(261.5f))
				{
					CreateChart(BeatTime(4), BeatTime(2), 6, new string[]
					{
						"R", "", "", "", "(#0.75#+0)(*+0)", "", "", "<<1.75",
						"D1", "", "", "", "(#0.75#+01)(*+01)", "", "", "",
						"(D)(D1)", "", "", "", "R", "R1", "", "",
						"", "(#0.5#R)(#0.5#D1)", "", "",
						"R", "", "", "", "(#0.75#+0)(*+0)", "", "", "<<1.75",
						"D1", "", "", "", "(#0.75#+01)(*+01)", "", "", "",
						"#2#R", "", "", "", "#1#R1", "", "", "",
						"", "", "R", "", "R", "", "", "#2#R",
						"", "", "", "", "", "", "", "",
						"", "", "", "", "", "", "", "R",
						"D", "", "", "R", "D", "", "", "R",
						"D", "", "", "R", "D", "", "", "R",
						"D", "", "", "R", "D", "", "", "R",
						"D", "", "", "R", "D", "", "", "#7#R",
						"", "", "", "", "D", "", "", "",
						"D", "", "", "", "D", "", "", "",
						"D", "", "", "", "D", "", "", "",
						"D", "", "", "", "D", "", "", "",
					});
				}
				if (InBeat(293.5f))
				{
					CreateChart(BeatTime(4), BeatTime(2), 6, new string[]
					{
						"R", "", "", "", "(#0.75#+0)(*+0)", "", "", "<<1.75",
						"D1", "", "", "", "(#0.75#+01)(*+01)", "", "", "",
						"(D)(D1)", "", "", "", "R", "R1", "", "",
						"", "(#0.5#R)(#0.5#D1)", "", "",
						"R", "", "", "", "(#0.75#+0)(*+0)", "", "", "<<1.75",
						"D1", "", "", "", "(#0.75#+01)(*+01)", "", "", "",
						"#2#R", "", "", "", "#1#R1", "", "", "",
						"", "", "R1", "", "R1", "", "", "#2#R1",
						"", "", "", "", "", "", "", "",
						"", "", "", "", "", "", "", "R1",
						"D1", "", "", "R1", "D1", "", "", "R1",
						"D1", "", "", "R1", "D1", "", "", "R1",
						"D1", "", "", "R1", "D1", "", "", "R1",
						"D1", "", "", "R1", "D1", "", "", "#5#R1",
						"", "", "", "", "D1", "", "", "",
						"D1", "", "", "", "D1", "", "", "",
						"", "", "", "(R)(R1)", "", "", "", "(R)(R1)",
						"", "", "", "(R)(R1)", "", "(R)(R1)", "", "(#1#R)(#1#D1)",
					});
				}
				for (float i = 325.5f; i < 353.5f; i += 8)
				{
					if (InBeat(i))
					{
						CreateGB(new GreenSoulGB(BeatTime(4), "D", 0, BeatTime(1)));
						CreateGB(new GreenSoulGB(BeatTime(4), "D", 1, BeatTime(1)));
						for (int ii = 0; ii < 6; ii++)
						{
							CreateArrow(BeatTime(6 + ii), "D", 6, ii % 2, 0);
						}
					}
				}
				if (InBeat(357.5f))
				{
					CreateChart(BeatTime(4), BeatTime(2), 6, new string[]
					{
						"R", "D", "", "", "R", "D", "", "",
						"R", "D", "", "", "R", "D", "", "",
						"R", "D", "", "D", "D", "D", "D", "D",
						"D", "", "D", "", "", "(D)(D1)", "", "",
						"", "(D)(D1)", "", "", "", "(D)(D1)", "", "",
						"", "(D)(D1)", "", "", "", "(D)(D1)", "", "",
						"", "", "(D)(D1)", "", "", "", "", "",
						"(D)(D1)", "(D)(D1)",
					});
				}
			}

			public void ExtremePlus()
			{
				foreach (Arrow arr in GetAll<Arrow>())
				{
					arr.Visible = Vector2.Distance(arr.Centre, Heart.Centre) < 220;
				}
				if (InBeat(0, 4) && At0thBeat(1))
					PlaySound(Sounds.ArrowStuck);
			}
		}
	}
}