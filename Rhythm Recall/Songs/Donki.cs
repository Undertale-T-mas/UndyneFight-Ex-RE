﻿using Extends;
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
			private class Circle : Entity
			{
				public float Alpha = 0.5f, StartAng = 0, EndAng = 360, Radius, Thickness;
				public Color color;
				public Circle(Vector2 pos, float rad, float thick, Color? col = null)
				{
					Centre = pos;
					Radius = rad;
					Thickness = thick;
					color = col ??= Color.Lerp(ScreenDrawing.ThemeColor, Color.Black, Alpha);
				}
				public override void Update() { }
				public override void Draw()
				{
					DrawingLab.DrawCircleSections(Centre, Radius, 512, Thickness, color, 1, StartAng, EndAng);
				}
			}
			public void Start()
			{
				SetSoul(1);
				TP();
				SetGreenBox();
				Settings.GBAppearVolume = 0;
				Settings.GreenTap = true;
				bool jump = true;
				if (jump)
				{
					float beat = 357;
					PlayOffset = GametimeDelta = BeatTime(beat);
				}
				if (GameStates.difficulty == 5)
				{
					CreateEntity(new Circle(new(320, 240), 220, 5, Color.White));
					for (int i = 0; i < 8; i++)
						CreateEntity(new Circle(new Vector2(320, 240) + MathUtil.GetVector2(220, i * 45), 1, 15, Color.White));

					ScreenDrawing.UISettings.NameShowerPos = new Vector2(-1000, 0);
					ScreenDrawing.UISettings.HPShowerPos = new(250, 390);
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
				if (InBeat(2.25f))
				{
					CreateChart(BeatTime(4.5f), BeatTime(4), 6, new string[]
					{
						"", "", "", $"(R0{Rand(0, 1) * 2})(R1{Rand(0, 1) * 2})",
						"", "", "", $"(R0{Rand(0, 1) * 2})(R1{Rand(0, 1) * 2})",
						"", "", "", $"(R0{Rand(0, 1) * 2})(R1{Rand(0, 1) * 2})",
						"", "", "", $"(R0{Rand(0, 1) * 2})(R1{Rand(0, 1) * 2})",
						
						"", "", "", $"(R0{Rand(0, 1) * 2})(R1{Rand(0, 1) * 2})(R{Rand(0, 1)}{Rand(0, 1) * 2})",
						"", "", "", $"(R0{Rand(0, 1) * 2})(R1{Rand(0, 1) * 2})(R{Rand(0, 1)}{Rand(0, 1) * 2})",
						"", "", "", $"(R0{Rand(0, 1) * 2})(R1{Rand(0, 1) * 2})(R{Rand(0, 1)}{Rand(0, 1) * 2})",
						"", "", "", $"(R0{Rand(0, 1) * 2})(R1{Rand(0, 1) * 2})(R{Rand(0, 1)}{Rand(0, 1) * 2})",

						"", "", "", $"(R0{Rand(0, 1) * 2})(R1{Rand(0, 1) * 2})(R{Rand(0, 1)}{Rand(0, 1) * 2})",
						"", "", "", $"(R0{Rand(0, 1) * 2})(R1{Rand(0, 1) * 2})(R{Rand(0, 1)}{Rand(0, 1) * 2})",
						"", "", "", $"(R0{Rand(0, 1) * 2})(R1{Rand(0, 1) * 2})(R{Rand(0, 1)}{Rand(0, 1) * 2})",
						"", "", "", $"(R0{Rand(0, 1) * 2})(R1{Rand(0, 1) * 2})(R{Rand(0, 1)}{Rand(0, 1) * 2})",
						"", "", "", $"(R0{Rand(0, 1) * 2})(R1{Rand(0, 1) * 2})(R{Rand(0, 1)}{Rand(0, 1) * 2})",
					});
				}
				if (InBeat(30.5f))
				{
					CreateChart(BeatTime(4), BeatTime(2), 6, new string[]
					{
						"(R)(+21)", "", "(-1)(+21)", "", "(-1)(+21)", "", "(-1)(+21)", "",
						"", "(R)(R1)", "", "", "", "", "", "(R)(R1)",
						"", "", "", "", "", "(R)(R1)", "", "",
						"", "", "", "", "(R)(D02)(R1)(D12)", "", "", "",
						"(R)(+002)(R1)(D12)", "", "", "", "(R)(+002)(R1)(+012)", "", "", "",
						"(R)(+002)(R1)(D12)", "", "", "", "(R)(+002)(R1)(+012)", "", "", "",
						"(R)(D02)(R1)(D12)", "", "", "", "(R)(+002)(R1)(+012)", "", "", "",
						"(R)(+002)(R1)(D12)", "", "", "", "(R)(+002)(R1)(+012)", "", "", "",
						"(R)(D02)(R1)(D12)", "", "", "", "(R)(+002)(R1)(+012)", "", "", "",
						"(R)(+002)(R1)(D12)", "", "", "", "(R)(+002)(R1)(+012)", "", "", "",
						"(R)(D02)(R1)(D12)", "", "", "", "(R)(+002)(R1)(+012)", "", "", "",
						"(R)(+002)(R1)(D12)", "", "", "", "(R)(+002)(R1)(+012)", "", "", "",
						"(R)(D02)(R1)(D12)", "", "", "", "(R)(+002)(R1)(+012)", "", "", "",
						"(R)(+002)(R1)(D12)", "", "", "", "(R)(+002)(R1)(+012)", "", "", "",
						"(R)(D02)(R1)(D12)", "", "", "", "(R)(+002)(R1)(+012)", "", "", "",
						"(R)(+002)(R1)(D12)", "", "", "", "(R)(+002)(R1)(+012)", "", "", "",
						"(R)(+002)(R1)(D12)", "", "", "", "(R)(+002)(R1)(+012)", "", "", "",
						"(R)(D02)(R1)(D12)", "", "", "", "(R)(+002)(R1)(+012)", "", "", "",
						"(R)(+002)(R1)(D12)", "", "", "", "(R)(+002)(R1)(+012)", "", "", "",
					});
				}
				if (InBeat(68.5f))
				{
					string B2 = "(R)(R1)(R02)", R2 = "(R)(R1)(R12)",
							BR2 = "(R01)(R1)(R02)", RR2 = "(R)(R11)(R12)";
					CreateChart(BeatTime(4), BeatTime(2), 6, new string[]
					{
						BR2, "", "", "", R2, "", "", "",
						B2, "", "", "", R2, "", "", "",
						B2, "", "", "", R2, "", "", "",
						B2, "", "", "", R2, "", "", "",
						BR2, "", "", "", R2, "", "", "",
						B2, "", "", "", R2, "", "", "",
						B2, "", "", "", R2, "", "", "",
						B2, "", "", "", R2, "", "", "",

						B2, "", "", "", RR2, "", "", "",
						B2, "", "", "", R2, "", "", "",
						B2, "", "", "", R2, "", "", "",
						B2, "", "", "", R2, "", "", "",
						B2, "", "", "", RR2, "", "", "",
						B2, "", "", "", R2, "", "", "",
						B2, "", "", "", R2, "", "", "",
						B2, "", "", "", R2, "", "", "",

						BR2, "", "", "", R2, "", "", "",
						B2, "", "", "", R2, "", "", "",
						B2, "", "", "", R2, "", "", "",
						B2, "", "", "", R2, "", "", "",
						B2, "", "", "", RR2, "", "", "",
						B2, "", "", "", R2, "", "", "",
						BR2, "", "", "", R2, "", "", "",
						B2, "", "", "", R2, "", "", "",
						BR2, "", "", "", R2, "", "", "",
						B2, "", "", "", RR2, "", "", "",
						"(D)(D02)(D1)(D12)", "", "", "", "", "", "", "",
						"(D)(D02)(D1)(D12)", "", "", "", "", "", "", "",
						"(D)(D02)(D1)(D12)", "", "", "", "(D)(D02)(D1)(D12)", "", "", "",
						"(D)(D02)(D1)(D12)", "", "", "", "(D)(D02)(D1)(D12)", "", "", "",
						"(R)(R1)", "", "", "", "(R)(R1)", "", "", "",
						"(R)(R1)", "", "", "", "(R)(R1)", "", "", "",
					});
				}
				if (InBeat(132.5f))
				{
					string str4 = "(D)(D02)(D1)(D12)",
						str3b = "(D)(D02)(D1)",
						str3r = "(D)(D12)(D1)";
					CreateChart(BeatTime(4), BeatTime(2), 6, new string[]
					{
						str4, "", "", "", str4, "", "", "",
						str4, "", "", "", str3b, "", "", "",
						str4, "", "", "", str4, "", "", "",
						"(D)(D1)", "", "", "", str3r, "", "", "",
						"(D)(D1)", "", "", "", "(D02)(D12)", "", "", "",
						"", "", "(D)(D1)", "", "(D)(D1)", "", "", str4,
						"", "", "", "", "", "", "", "",
						"", "", "", "", "", "", "", str3b,
						"", "", "", str3b, "", "", "", str3r,
						"", "", "", str3r, "", "", "", str3b,
						"", "", "", str3b, "", "", "", str3r,
						"", "", "", str3r, "", "", "", str4,
						"", "", "", "", str4, "", "", "",
						str4, "", "", "", str4, "", "", "",
						str4, "", "", "", str4, "", "", "",
						str4, "", "", "", str4, "", "", "",
					});
				}
				if (InBeat(164.5f))
				{
					string str4 = "(D)(D02)(D1)(D12)",
						str3b = "(D)(D02)(D1)",
						str3r = "(D)(D12)(D1)";
					CreateChart(BeatTime(4), BeatTime(2), 6, new string[]
					{
						str4, "", "", "", str4, "", "", "",
						str4, "", "", "", str3b, "", "", "",
						str4, "", "", "", str4, "", "", "",
						"(D)(D1)", "", "", "", str3r, "", "", "",
						"(D)(D1)", "", "", "", "(D02)(D12)", "", "", "",
						"", "", "(D)(D1)", "", "(D)(D1)", "", "", str4,
						"", "", "", "", "", "", "", "",
						"", "", "", "", "", "", "", str3b,
						"", "", "", str3b, "", "", "", str3r,
						"", "", "", str3r, "", "", "", str3b,
						"", "", "", str3b, "", "", "", str3r,
						"", "", "", str3r, "", "", "", str4,
						"", "", "", "", str4, "", "", "",
						str4, "", "", "", str4, "", "", "",
						str4, "", "", "", str4, "", "", "",
						str4, "", "", "", str4, "", "", "",
						str4, "", "", "", str4, "", "", "",
						str4, "", "", "", str4, "", "", "",
					});
				}
				if (InBeat(197.5f) || InBeat(213.5f))
				{
					CreateChart(BeatTime(4), BeatTime(8), 6, new string[]
					{
						"(R)(R1)", "(R)(R)(R1)(R1)", "(R)(R1)", "(R)(R)(R1)(R1)",
						"(R)(R1)", "(R)(R)(R1)(R1)", "(R)(R1)", "(R)(R)(R1)(R1)",
						"(R)(R1)", "(R)(R)(R1)(R1)", "(R)(R1)", "(R)(R)(R1)(R1)",
						"(R)(R1)", "(R)(R)(R1)(R1)", "(R)(R1)", "(R)(R)(R1)(R1)",
					});
				}
				if (InBeat(229.5f))
				{
					CreateChart(BeatTime(4), BeatTime(4), 6, new string[]
					{
						"(R)(R)(R1)", "", "(R)(R1)(R1)", "", "(R)(R)(R1)", "", "(R)(R1)(R1)", "",
						"(R)(R)(R1)", "", "(R)(R1)(R1)", "", "(R)(R)(R1)", "", "(R)(R1)(R1)", "",
						"(R)(R)(R1)", "", "(R)(R1)(R1)", "", "(R)(R)(R1)", "", "(R)(R1)(R1)", "",
						"(R)(R)(R1)", "", "(R)(R1)(R1)", "", "(R)(R)(R1)", "", "(R)(R1)(R1)", "",

						"(R)(R02)(R1)", "", "(R)(R12)(R1)", "",  "(R)(R02)(R1)", "", "(R)(R12)(R1)", "", 
						"(R)(R02)(R1)", "", "(R)(R12)(R1)", "",  "(R)(R02)(R1)", "", "(R)(R12)(R1)", "", 
						"(R)(R02)(R1)", "", "(R)(R12)(R1)", "",  "(R)(R02)(R1)", "", "(R)(R02)(R1)", "", 
						"", "(R)(R02)(R1)", "", "", "(R)(R02)(R1)", "", "", "",
					});
				}
				if (InBeat(260.5f))
				{
					string D3 = "($0)($1)($2)($01)($11)($21)", D4 = "(R)(R)(R1)(R1)";
					CreateChart(BeatTime(4), BeatTime(2), 6, new string[]
					{
						D3, D3, D3, D3, D3, D3, D3, D3,
						D3, "", "", "", D3, "", "", "",
						D3, "", "", "", "", "(R)(R1)", "", "",
						"(R)(R1)", "", "", "", "(R)(R1)", "", "", "",
						D4, "", "", "", D4, "", "", "",
						D4, "", "", "", D4, "", "", "",
						D4, "", "", "", D4, "", "", "",
						D4, "", "", "", D4, "", "", "",
						D4, "", "", "", D4, "", "", "",
						D4, "", "", "", D4, "", "", "",
						D4, "", "", "", D4, "", "", "",
						D4, "", "", "", D4, "", "", "",
						D4, "", "", "", D4, "", "", "",
						D4, "", "", "", D4, "", "", "",
						D4, "", "", "", D4, "", "", "",
						D4, "", "", "", D4, "", "", "",
					});
				}
				if (InBeat(292.5f))
				{
					string D3 = "($0)($3)($2)($01)($31)($21)", D4 = "(R)(R)(R1)(R1)";
					CreateChart(BeatTime(4), BeatTime(2), 6, new string[]
					{
						D3, D3, D3, D3, D3, D3, D3, D3,
						D3, "", "", "", D3, "", "", "",
						D3, "", "", "", "", "(R)(R1)", "", "",
						"(R)(R1)", "", "", "", "(R)(R1)", "", "", "",
						D4, "", "", "", D4, "", "", "",
						D4, "", "", "", D4, "", "", "",
						D4, "", "", "", D4, "", "", "",
						D4, "", "", "", D4, "", "", "",
						D4, "", "", "", D4, "", "", "",
						D4, "", "", "", D4, "", "", "",
						D4, "", "", "", D4, "", "", "",
						D4, "", "", "", D4, "", "", "",
						D4, "", "", "", D4, "", "", "",
						D4, "", "", "", D4, "", "", "",
						D4, "", "", "", D4, "", "", "",
						D4, "", "", "", D4, "", "", "",
					});
				}
				for (float i = 325.5f; i < 357.5f; i += 4)
				{
					if (InBeat(i))
					{
						CreateArrow(BeatTime(4), "R", 6, 0, 0);
						CreateArrow(BeatTime(5), "R", 6, 0, 0);
						CreateArrow(BeatTime(5), "R", 6, 1, 0);
						CreateArrow(BeatTime(6), "R", 6, 0, 0);
						CreateArrow(BeatTime(6), "R", 6, Rand(0, 1), 0);
						CreateArrow(BeatTime(6), "R", 6, 1, 0);
						CreateArrow(BeatTime(7), "R", 6, 0, 0);
						CreateArrow(BeatTime(7), "R", 6, 0, 0);
						CreateArrow(BeatTime(7), "R", 6, 1, 0);
						CreateArrow(BeatTime(7), "R", 6, 1, 0);
					}
				}
				if (InBeat(357.5f))
				{
					string DB3 = "($0)($1)($2)", DR3 = "($01)($11)($21)";
					CreateChart(BeatTime(4), BeatTime(2), 6, new string[]
					{
						DB3, DR3, "", "", DB3, DR3, "", "",
						DB3, DR3, "", "", DB3, DR3, "", "",
						DB3, DR3, "", "", DB3 + DR3, "", DB3 + DR3, "",
						DB3 + DR3, "", DB3 + DR3, "", "", DB3 + DR3, "", "",
						"", "(D)(D1)", "", "", "", "(D)(D1)", "", "",
						"", "(D)(D1)", "", "", "", "(D)(D1)", "", "",
						"", "", DB3 + "($01)($21)($31)", "", "", "", "", "",
						DB3 + DR3 + "($3)($31)", "",
					});
				}
			}
		}
	}
}