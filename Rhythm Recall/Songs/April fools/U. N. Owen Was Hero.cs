using Extends;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Remake.Texts;
using UndyneFight_Ex.Remake;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.AdvanceFunctions;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;
using col = Microsoft.Xna.Framework.Color;
using UI = UndyneFight_Ex.Fight.Functions.ScreenDrawing.UISettings;
using vec2 = Microsoft.Xna.Framework.Vector2;

namespace Rhythm_Recall.Waves
{
	class UNOwen : WaveConstructor, IWaveSet
	{
		public UNOwen() : base(62.5f / (152f / 60f)) { }
		public string Music => "U. N. Owen Was Hero";
		public string FightName => "U. N. Owen Was Hero";
		public SongInformation Attributes => new Information();
		class Information : SongInformation
		{
			public override string SongAuthor => "Toby Fox X ZUN";
			public override string BarrageAuthor => "TK + Eden";
			public override string AttributeAuthor => "TK";
			public override string Extra => GameStates.difficulty == 3 ? "Kagura Stage" : "Danmaku Stage";
			public override Dictionary<Difficulty, float> CompleteDifficulty => new(
				new KeyValuePair<Difficulty, float>[]
				{
					new(Difficulty.Hard, 20),
					new(Difficulty.Extreme, 28),
				}
			);
			public override Dictionary<Difficulty, float> ComplexDifficulty => new(
				new KeyValuePair<Difficulty, float>[]
				{
					new(Difficulty.Hard, 20),
					new(Difficulty.Extreme, 28),
				}
			);
			public override Dictionary<Difficulty, float> APDifficulty => new(
				new KeyValuePair<Difficulty, float>[]
				{
					new(Difficulty.Hard, 22),
					new(Difficulty.Extreme, 28.8f),
				}
			);
		}
		public void Start()
		{
			SetGreenBox();
			TP();
			SetSoul(1);
			ScreenDrawing.ScreenAngle = 0;
			Settings.GreenTap = true;
			if (GameStates.difficulty == 4)
			{
				CreateEntity(new SideBar() { game = this });
				UI.HPShowerPos = new(320, 9999);
				UI.NameShowerPos = new(320, 9999);
				InstantSetBox(999, 0, 0);
				InstantTP(320, 999);
				SetSoul(0);
				Interactive.AddPerfectEvent(() => Score += (int)(GameStates.CurrentScene as SongFightingScene).JudgeState switch { 3 => 100, 2 => 98, 1 => 96 });
				Interactive.AddNiceEvent(() => Score += 40);
				HeartAttribute.MaxHP = 9;
				HeartAttribute.HP = 3;
				for (int i = 0; i < Bullet.Length; Bullet[i] = Loader.Load<Texture2D>($"{Root}Bullet {i++}")) { }
				for (int i = 1; i < 16; ScoreExtend.Add(new((int)Math.Pow(2, i++) * 10000, false))) { }
			}
#if DEBUG
			GametimeDelta = -0.01f;
			bool jump = false;
			if (jump)
			{
				float beat = 0;
				beat = GameStates.difficulty == 3 ? 0 : 397;
				GametimeDelta += BeatTime(beat);
				PlayOffset = BeatTime(beat);
				if (beat > 24)
				{
					UI.HPShowerPos = new(320, 443);
					UI.NameShowerPos = new(20, 457);
					ScreenDrawing.ThemeColor = ScreenDrawing.UIColor = ScreenDrawing.BoxBackColor = col.Transparent;
					ScreenDrawing.MasterAlpha = 1;
					ScreenDrawing.HPBar.AreaOccupied = new(0, 0, 0, 0);
					Heart.Speed = 3;
					InstantSetBox(new vec2(210, 240), 360, 440);
					InstantTP(210, 400);
					CreateEntity(FlanEnemy[0] = new Flan());
				}
			}
#endif
		}
		public void Hard()
		{
			if (InBeat(0))
			{
				CreateChart(0, BeatTime(2), 6, new string[]
				{
					"$0", "", "", "", "", "", "", "R",
					"", "", "", "", "", "R", "", "",
					"", "", "", "R", "", "", "", "",
					"", "R", "", "", "", "", "", "R",
					"", "R", "", "R", "", "R", "", "",
					"", "", "", "R", "", "", "", "",
					"", "R", "", "", "", "R", "", "",
					"", "R", "", "R", "", "R", "", "",
					"R", "", "", "", "", "", "", "",
				});
			}
			if (InBeat(12))
			{
				CreateChart(BeatTime(4), BeatTime(2), 6, new string[]
				{
					"", "R", "", "", "R", "", "", "",
					"R", "", "", "R", "", "", "", "",
					"", "R", "", "R", "", "R", "", "",
					"", "", "", "", "", "", "", "",
					"", "R", "", "", "", "", "", "R",
					"", "", "", "", "", "R", "", "",
					"", "", "", "R", "", "", "", "",
					"", "R", "", "", "", "", "", "R",
					"", "R", "", "R", "", "R", "", "",
					"", "", "", "R", "", "", "", "",
				});
			}
			if (InBeat(32))
			{
				string arr = "(R1)(R)";
				CreateChart(BeatTime(4), BeatTime(2), 6, new string[]
				{
					"", arr, "", "", "", arr, "", "",
					"", arr, "", arr, "", "", arr, "",
					"", arr, "", "", "", arr, "", "",
				});
			}
			if (InBeat(42))
			{
				ScreenDrawing.WhiteOut(BeatTime(3));
				DrawingUtil.SetScreenScale(3, BeatTime(2));
			}
			if (InBeat(45))
			{
				HeartAttribute.MaxHP = 6;
				HeartAttribute.HP = 6;
				DrawingUtil.SetScreenScale(1, BeatTime(1));
				for (int i = 0; i < 7; i++)
					CreateChart(BeatTime(i * 4), BeatTime(2), 6, new string[]
					{
						"", "(^$3'2)(^$111'2)", "", "", "D", "", "", "D",
						"", "", "D", "", "", "(D)(D1)", "", "D"
					});
			}
			if (InBeat(45.25f, 69.25f) && AtKthBeat(8, BeatTime(5.25f)))
				ScreenDrawing.SceneOut(col.White * 0.75f, BeatTime(0.25f));
			if (InBeat(70))
			{
				string str = "(D)(D1)(Zoom)";
				RegisterFunctionOnce("Zoom", () =>
				{
					RunEase((s) => ScreenDrawing.ScreenScale = s, false, EaseOut(BeatTime(1), ScreenDrawing.ScreenScale, ScreenDrawing.ScreenScale + 0.15f, EaseState.Elastic));
					RunEase((s) => ScreenDrawing.ScreenAngle = s, false, EaseOut(BeatTime(0.1f), ScreenDrawing.ScreenAngle, ScreenDrawing.ScreenAngle < 0 ? 20 : -20, EaseState.Quad));
				});
				CreateChart(BeatTime(3.25f), BeatTime(2), 6.5f, new string[]
				{
					str, "", "", str, "", "", str, "",
					"", str, "", "", str, "", "", "",
				});
			}
			if (InBeat(75))
			{
				for (int i = 0; i < 7; i++)
					CreateChart(BeatTime(2.5f + i * 4f), BeatTime(2), 6.5f, new string[]
					{
						"R", "", "R", "+1", "", "R", "", "R1",
						"R", "", "R", "+1", "R1", "R", "-1", "",
					});
			}
			if (InBeat(77))
				RunEase((s) => ScreenDrawing.ScreenScale = s, false, EaseOut(BeatTime(2), ScreenDrawing.ScreenScale, 1, EaseState.Circ));
			if (InBeat(77, 109))
				ScreenDrawing.ScreenAngle = Cos((GametimeF - BeatTime(77)) * 0.7f) * -20;
			if (InBeat(102))
			{
				CreateChart(BeatTime(3.25f), BeatTime(2), 6.5f, new string[]
				{
					"(R)(+0'2)", "", "(+1)(+0'2)", "", "(+1)(+0'2)", "", "(+1)(+0'2)", "",
					"(R1)(+01'2)", "", "(-11)(+01'2)", "", "(-11)(+01'2)", "", "(-11)(+01'2)", "",
				});
			}
			if (InBeat(105.25f, 108.75f) && AtKthBeat(0.5f, 0.25f))
				DrawingUtil.SetScreenScale(ScreenDrawing.ScreenScale + 0.1f, BeatTime(0.25f));
			if (InBeat(109))
			{
				ScreenDrawing.ScreenScale = 1;
				ScreenDrawing.ScreenAngle = 0;
				ScreenDrawing.MasterAlpha = 0;
				string arr = "(R)(+211)";
				string[] str = new string[]
				{
					"$01", "", "$01", "", "$01", "", "$01", "",
					"$01", "", "$01", "", "$01", "", "", "",
					"", arr, "", arr, "", arr, "", arr,
					"", arr, "", "", "", "", "", arr,
					"", "", "", "", "", arr, "", "",
					"", "", "", arr, "", "", "", "",
				};
				for (int i = 0; i < 16; i++)
					str[i] = $"({str[i]})(~_$1)";
				CreateChart(BeatTime(0), BeatTime(2), 5, str);
			}
			if (InBeat(109, 113) && At0thBeat(0.1f))
				ScreenDrawing.MasterAlpha += 1f / 80f;
			if (InBeat(117.25f))
			{
				string arr = "(R1)(+201)";
				string[] str = new string[]
				{
					"N1", "", "N1", "", "N1", "", "N1", "",
					"N1", "", "N1", "", "N1", "", "", "",
					arr, "", arr, "", arr, "", arr, "",
					arr, "", "", "", arr, "", "", "",
					arr, "", "", arr, "+1", "-1", "", "",
					"", "", "", "", "", "", "", "",
				};
				for (int i = 0; i < 16; i++)
					str[i] = $"({str[i]})(~_$11)";
				CreateChart(BeatTime(4), BeatTime(2), 5, str);
			}
			if (InBeat(129.25f))
			{
				string arr = "(R)(+211)";
				string[] str = new string[]
				{
					"N11", "", "N11", "", "N11", "", "N11", "",
					"N11", "", "N11", "", "N11", "", "", "",
					arr, "", arr, "", arr, "", arr, "",
					arr, "", "", "", "", arr, "", "",
					"", "", "", "", arr, "", "", "",
					"", "", arr, "", "", "", "", "",
				};
				for (int i = 0; i < 16; i++)
					str[i] = $"({str[i]})(~_$1)";
				CreateChart(BeatTime(4), BeatTime(2), 5, str);
			}
			if (InBeat(140.5f))
			{
				CreateChart(BeatTime(4), BeatTime(2), 5, new string[]
				{
					"", "", "R", "", "", "", "", "",
					"R", "", "R", "", "R", "", "R", "",
					"", "", "R", "", "", "", "R", "",
					"", "", "R", "", "", "", "", "",
					"", "", "", "", "", "", "R", "",
				});
			}
			if (InBeat(151.5f))
				CreateChart(BeatTime(4), BeatTime(2), 5, new string[] { "R", "", "+0", "", "+0", "", "", "" });
			if (InBeat(155.5f) || InBeat(156f) || InBeat(156.5f))
			{
				ScreenDrawing.MasterAlpha += 1f / 6f;
				ScreenDrawing.ScreenScale += 0.5f;
			}
			if (InBeat(157))
				ScreenDrawing.SceneOut(col.White, BeatTime(0.5f));
			if (InBeat(157.5f))
			{
				ScreenDrawing.ScreenScale = 1;
				string R1 = "N01)(+01", D1 = "N01)(+01", R2 = "N21)(+01", D2 = "N21)(+01";
				string[][] strtemp = new string[][]
				{
					new string[] { "$0", "~$0", "$0", "~$0", "$0", "~$0", "$0", "~$0", },
					new string[] { "$2", "~$2", "$2", "~$2", "$2", "~$2", "$2", "~$2", },
				};
				string[] drum = new string[] { }, chart = new string[]
				{
					"", "", "", "", "", R1, "", "",
					"", "", "", R1, "", "", "", "",
					"", R1, "", "", "", "", "", R1,
					"", "", "", "", "", R1, "", R1,
					"", R1, "", R1, "", "", "", "",
					"", R1, "", "", "", "", R1, "",

					"", "", "", R2, "", "", "", R2,
					"", R2, "", R2, "", "", R2, "",
					"", "", "", R2, "", "", "", R2,
					"", "", "", R2, "", "", "", R2,
					"", R2, "", R2, "", "", "", "",
					"", "", "", R2, "", R2, "", D1,

					"", "", "", "", D1, "", "", "",
					D1, "", "", D1, "", "", "", D1,
					"", "", "", D1, "", "", "", D1,
					"", "", "", D1, "", "", "", D1,
					"", "", "", D1, "", "", "", D1,
					"", "", "", D1, "", "", "", "",

					D2, "", "", "", "", "", "", D2,
					"", "", "", D2, "", "", "", D2,
					"", "", "", D2, "", "", "", D2,
					"", "", "", "", "", "", "", D2,
					"", D2, "", D2, "", "", "", "",
					"", "", "", "", "", "", "", "",
				};
				for (int i = 0; i < 24; i++)
					drum = drum.Concat(strtemp[(i < 6 || (i >= 12 && i < 18)) ? 0 : 1]).ToArray();
				for (int i = 0; i < chart.Length; i++)
					chart[i] = $"({drum[i]})({chart[i]})";
				CreateChart(0, BeatTime(2), 7, chart);
			}
			if (InBeat(157, 180.5f) && AtKthBeat(1, BeatTime(0.5f)))
				ScreenDrawing.CameraEffect.Convulse(25, BeatTime(1), GametimeF <= BeatTime(169));
			if (InBeat(181, 204.5f) && AtKthBeat(1, BeatTime(0.5f)))
				ScreenDrawing.CameraEffect.Convulse(25, BeatTime(1), GametimeF <= BeatTime(193));
			if (InBeat(181.5f))
				foreach (Arrow arr in GetAll<Arrow>())
					arr.ResetColor(1 - arr.ArrowColor);
			if (InBeat(201.25f))
			{
				string[][] strtemp = new string[][]
				{
					new string[] { "R", "", "R", "", "(R)(^R1)", "", "R", "", },
					new string[] { "R1", "", "R1", "", "(^R)(R1)", "", "R1", "", },
					new string[] { "R", "", "R", "", "(R)(^R11)", "", "R", "", },
					new string[] { "R1", "", "R1", "", "(^R01)(R1)", "", "R1", "", },
				};
				string[] chart = new string[] { };
				for (int i = 0; i < 24; i++)
					chart = chart.Concat(strtemp[(int)MathF.Floor(i / 6)]).ToArray();
				CreateChart(BeatTime(4), BeatTime(2), 7, chart);
			}
			if (InBeat(205.25f, 253.25f))
			{
				if (AtKthBeat(1, BeatTime(0.125f)))
					ScreenDrawing.CameraEffect.SizeExpand(AtKthBeat(2, BeatTime(0.125f)) ? 3 : 1.5f, BeatTime(1));
				if (At0thBeat(0.1f))
				{
					vec2 Center = new vec2(Rand(0, 640), 490);
					CreateEntity(new Particle(col.White, new vec2(0, -Rand(1, 4)), Rand(10, 20), Center, Sprites.spear)
					{
						AutoRotate = true,
						RotateSpeed = Rand(3, 6) * RandSignal(),
						DarkingSpeed = Center.X >= 270 && Center.X <= 360 ? Rand(4, 7) : 3
					});
					if (GametimeF >= BeatTime(229.25f))
						DrawingUtil.Rain(Rand(6, 12), Rand(10, 20), true);
				}
			}
			if (InBeat(253.5f))
			{
				foreach (Particle pt in GetAll<Particle>())
					pt.Dispose();
				foreach (DrawingUtil.Linerotatelong line in GetAll<DrawingUtil.Linerotatelong>())
					line.Dispose();
				ScreenDrawing.MasterAlpha = 0;
				InstantSetBox(240, 560, 300);
				SetSoul(0);
				for (int i = 0; i < 72; i++)
					for (int ii = 0; ii < 12; ii++)
						CreateSpear(new CircleSpear(new(320, 240), 9, 0.4f, 500 + ii * 50, i * 5 + ii * 1.5f, 0) { MarkScore = false });
			}
			if (InBeat(253.5f, 256.5f) && At0thBeat(0.1f))
				ScreenDrawing.MasterAlpha += 1f / 90f;
			if (InBeat(254, 262) && At0thBeat(2))
			{
				for (float i = 0, dir = Rand(0, 359); i < 8; i++)
					CreateSpear(new CircleSpear(Heart.Centre, 3, 2, 200, i * 45 + dir));
				for (float i = 0, dir = Rand(0, 359); i < 3; i++)
					CreateSpear(new CircleSpear(Heart.Centre, -3, 2, 200, i * 120 - dir));
			}
			if (InBeat(264, 272) && At0thBeat(2))
			{
				for (float i = 0, dir = Rand(0, 359); i < 4; i++)
				{
					CreateSpear(new CircleSpear(Heart.Centre, 3, 2, 200, i * 90 + dir, 90, 0.01f));
					CreateSpear(new CircleSpear(Heart.Centre, -3, 2, 200, i * 90 + dir, -90, 0.01f));
				}
			}
			if (InBeat(278, 288))
			{
				if (At0thBeat(0.5f) && GametimeF <= BeatTime(287))
				{
					vec2 Center = new(Rand(40, 600), Rand(90, 390));
					while (vec2.Distance(Heart.Centre, Center) < 100)
						Center = new(Rand(40, 600), Rand(90, 390));
					CreateSpear(new NormalSpear(Center) { IsMute = true });
				}
				if (At0thBeat(2))
					for (float i = 0, dir = Rand(0, 359), rot = RandSignal(); i < 4; i++)
						CreateSpear(new CircleSpear(Heart.Centre, 3 * rot, 3, 200, i * 90 + dir, 45, 0.01f));
			}
			if (InBeat(294))
				RunEase((s) => ScreenDrawing.ScreenScale = s, EaseOut(BeatTime(3), 1, 2.5f, EaseState.Expo));
			if (InBeat(297))
			{
				foreach (CircleSpear spear in GetAll<CircleSpear>())
				{
					RunEase((s) => spear.linearSpeed = s, EaseOut(BeatTime(6), spear.linearSpeed, 0, EaseState.Sine));
					RunEase((s) => spear.rotateSpeed = s, EaseOut(BeatTime(6), spear.rotateSpeed, 0, EaseState.Sine));
				}
				ScreenDrawing.SceneOut(col.White, BeatTime(4));
			}
			if (InBeat(297, 301))
				ScreenDrawing.ScreenPositionDelta = new(Rand(-15, 15), Rand(-15, 15));
			if (InBeat(301))
			{
				ScreenDrawing.ScreenPositionDelta = new(0);
				SetSoul(1);
				InstantSetGreenBox();
				InstantTP(320, 240);
				ScreenDrawing.MasterAlpha = 1;
				ScreenDrawing.ScreenScale = 1;
				foreach (CircleSpear spear in GetAll<CircleSpear>())
					spear.Dispose();
				string[] chart = new string[] { };
				string[][] strtemp = new string[][]
				{
					new string[]
					{
						"+0", "", "+0", "", "D", "", "D", "",
						"(D)(D1)", "", "(D)(D1)", "", "(D)(D1)", "", "", "",
						"D", "", "D", "", "D", "", "D", "",
						"#1#D", "", "+2", "", "+0", "", "#1#D1", "",
						"+21", "", "+01", "", "#1#D", "", "+2", "",
						"+0", "", "#1#D1", "", "+21", "", "+01", "",
					},
					new string[]
					{
						"D1", "", "D1", "", "D1", "", "D1", "",
						"(D)(D1)", "", "(D)(D1)", "", "(D)(D1)", "", "", "",
						"D1", "", "D1", "", "D1", "", "D1", "",
						"#1#D", "", "+2", "", "#1#D1", "", "+21", "",
						"#1#D", "", "+2", "", "#2#D1", "+21", "+0", "+01",
						"+0", "+01", "+0", "+01", "+0", "", "", "",
					}
				};
				chart = chart.Concat(strtemp[0]).ToArray();
				strtemp[0][0] = "D";
				strtemp[0][2] = "D";
				chart = chart.Concat(strtemp[1]).ToArray();
				chart = chart.Concat(strtemp[0]).ToArray();
				ArrowApply("X", (s) => s.CentreRotationOffset = Rand(0, 15) * RandSignal());
				ArrowApply("Y", (s) =>
				{
					s.CentreRotationOffset = Rand(20, 40) * RandSignal();
					s.SelfRotationOffset = -s.CentreRotationOffset / 2;
				});
				CreateChart(BeatTime(0.25f), BeatTime(2), 7, chart = chart.Concat(new string[] {
					"(^D)(^+0'2)", "", "", "", "", "", "D", "",
					"D", "", "D", "", "(D)(D1)", "", "", "",
					"(D)(D1)", "", "", "", "(D)(D1)", "", "", "",
					"(#2.5#D)(+0)", "+0@X", "+0@Y", "+0@X", "+0@Y", "+0@X", "+0@Y", "+0@X",
					"+0@Y", "+0@X", "+0@Y", "+0@X", "(#2.5#D)(+0)", "+0@X", "+0@Y", "+0@X",
					"+0@Y", "+0@X", "+0@Y", "+0@X", "+0@Y", "+0@X", "+0@Y", "+0@X",
				}).ToArray());
			}
			if (InBeat(346))
			{
				string[] chart = new string[] { };
				string[][] strtemp = new string[][]
				{
					new string[]
					{
						"D", "", "D", "", "D", "", "D", "",
						"(D)(D1)", "", "(D)(D1)", "", "(D)(D1)", "", "", "",
						"D", "", "D", "", "D", "", "D", "",
						"#1#D", "", "+2", "", "+0", "", "#1#D1", "",
						"+21", "", "+01", "", "#1#D", "", "+2", "",
						"+0", "", "#1#D1", "", "+21", "", "+01", "",
					},
					new string[]
					{
						"D1", "", "D1", "", "D1", "", "D1", "",
						"(D)(D1)", "", "(D)(D1)", "", "(D)(D1)", "", "", "",
						"D1", "", "D1", "", "D1", "", "D1", "",
						"#1#D", "", "+2", "", "#1#D1", "", "+21", "",
						"#1#D", "", "+2", "", "#2#D1", "+21", "+0", "+01",
						"+0", "+01", "+0", "+01", "+0", "", "", "",
					}
				};
				chart = chart.Concat(strtemp[0]).ToArray();
				strtemp[0][0] = "D";
				strtemp[0][2] = "D";
				chart = chart.Concat(strtemp[1]).ToArray();
				chart = chart.Concat(strtemp[0]).ToArray();
				ArrowApply("X", (s) => s.CentreRotationOffset = Rand(0, 15) * RandSignal());
				ArrowApply("Y", (s) =>
				{
					s.CentreRotationOffset = Rand(20, 40) * RandSignal();
					s.SelfRotationOffset = -s.CentreRotationOffset / 2;
				});
				CreateChart(BeatTime(3.25f), BeatTime(2), 7, chart = chart.Concat(new string[] {
					"(^D)(^+0'2)", "", "", "", "", "", "D", "",
					"D1", "", "D1", "", "(D)(D1)", "", "", "",
					"(D)(D1)", "", "", "", "(D)(D1)", "", "", "",
					"(#2.5#D1)(+01)", "+01@X", "+01@Y", "+01@X", "+01@Y", "+01@X", "+01@Y", "+01@X",
					"+01@Y", "+01@X", "+01@Y", "+01@X", "(#2.5#D1)(+01)", "+01@X", "+01@Y", "+01@X",
					"+01@Y", "+01@X", "+01@Y", "+01@X", "+01@Y", "+01@X", "+01@Y", "+01@X",
				}).ToArray());
			}
			if (InBeat(301, 396))
			{
				DrawingUtil.Rain(Rand(8, 16), -ScreenDrawing.ScreenAngle, true);
				if (At0thBeat(1))
					ScreenDrawing.CameraEffect.Convulse(25, !At0thBeat(4));
				if (At0thBeat(3))
				{
					for (float i = 0, dir = Rand(0, 359); i < 2; i++, dir += 180)
					{
						vec2 Start = new vec2(320, 240) + GetVector2(520, dir), End = Start - GetVector2(250, dir);
						Particle pt;
						CreateEntity(pt = new(col.White, new(0), 100, Start, Sprites.spear)
						{ DarkingSpeed = 0, Rotation = (dir + 180) * MathF.PI / 180 });
						RunEase((s) => pt.Centre = s, LinkEase(false,
							EaseOut(BeatTime(1), Start, End, EaseState.Quad),
							EaseOut(BeatTime(1), End, End + GetVector2(500, dir + (RandBool() ? 90 : -90)), EaseState.Quad)));
						DelayBeat(3, () => pt.Dispose());
					}
				}
				if (AtKthBeat(3, BeatTime(1.5f)))
					RunEase((s) => ScreenDrawing.BackGroundColor = col.Lerp(col.White * 0.75f, col.Black, s), EaseOut(BeatTime(1), 0, 1, EaseState.Quad));
			}
			if (InBeat(393))
			{
				CreateChart(BeatTime(4), BeatTime(2), 7, new string[]
				{
					"", "^R", "", "", "", "", "", "R",
					"", "R", "", "R", "", "R", "", "",
					"", "R", "", "", "", "R", "", "",
					"", "R", "", "", "", "", "", "",
				});
			}
			if (InBeat(403))
			{
				ScreenDrawing.CameraEffect.SizeShrink(12, BeatTime(3));
				foreach (Arrow arr in GetAll<Arrow>())
				{
					arr.Delay(BeatTime(3.25f));
					RunEase((s) => arr.CentreRotationOffset = s, EaseOut(BeatTime(3.25f), 0, 720 * RandSignal(), EaseState.Circ));
				}
			}
			if (InBeat(397))
				CreateGB(new GreenSoulGB(BeatTime(9.5f), "+0", 0, BeatTime(12)));
			if (InBeat(406.25f, 422) && At0thBeat(0.1f))
				ScreenDrawing.ScreenScale += 0.075f;
			if (InBeat(422, 428) && At0thBeat(0.1f))
				ScreenDrawing.MasterAlpha -= 0.05f;
		}
		public void Extreme()
		{
			if (GametimeF < BeatTime(406.5f))
			{
				for (int i = 0; i < ScoreExtend.Count; i++)
					if (!ScoreExtend[i].Item2 && Score >= ScoreExtend[i].Item1)
					{
						PlaySound(Sounds.heal);
						HeartAttribute.HP++;
						ScoreExtend[i] = new(ScoreExtend[i].Item1, true);
					}
			}
			foreach (AccuracyBar bar in GetAll<AccuracyBar>())
				bar.Centre = new(320, 900);
			if (InBeat(24.5f))
			{
				InstantSetBox(new vec2(210, 240), 360, 440);
				InstantTP(210, 400);
				Heart.Speed = 0;
				CreateEntity(FlanEnemy[0] = new Flan());
			}
			if (InBeat(26, 40) && At0thBeat(4))
				FlanEnemy[0].RandMove(100, 320, 100, 100);
			if (InBeat(44))
			{
				ScreenDrawing.SceneOut(col.White, BeatTime(1.5f));
				FlanEnemy[0].RandMove(210, 210, 30, 30);
			}
			if (InBeat(44.9f, 72))
			{
				vec2 FlanCenter = FlanEnemy[0].Center;
				if (AtKthBeat(8, BeatTime(5)))
				{
					FlanEnemy[0].RandMove(40, 320, 30, 130);
					for (float i = 0, dir = Rand(0, 359); i < 40; i++)
					{
						CreateBullet(FlanCenter, dir, 4, 0);
						CreateBullet(FlanCenter, dir += 9, 3, 0);
					}
				}
				for (int i = 0; i < 12; i++)
					if (AtKthBeat(8, BeatTime((5f + i / 1.5f) % 8)))
						CreateBullet(FlanCenter, Rand(45, 225), 5, 4, (int)BulletMode.Bounce).BounceCount = 2;
			}
			if (InBeat(73.5f, 76) && At0thBeat(0.5f) && !InBeat(74.5f))
			{
				vec2 FlanCenter = FlanEnemy[0].Center;
				for (float i = 0, dir = Rand(0, 359), ii = 0; i < 4; i++, ii = 0)
				{
					for (; ii < 24; ii++)
						CreateBullet(FlanCenter + GetVector2(Rand(-20, 20), Rand(0, 359)), dir, 8 - ii / 12, 6);
					CreateBullet(FlanCenter, dir += 90, 8, 4);
				}
			}
			for (int i = 1; i <= 3; i++)
			{
				if (InBeat(76.7f + i * 0.1f))
				{
					CreateEntity(FlanEnemy[i] = new Flan());
					FlanEnemy[i].Center = new(30 + i * 70, -100);
					FlanEnemy[i].Target = new(30 + i * 70, 100);
				}
			}
			if (InBeat(76.7f))
				FlanEnemy[0].Target = new(310, 100);
			if (InBeat(77.5f))
			{
				RegisterFunctionOnce("Shoot", () =>
				{
					PlaySound(Sounds.pierce);
					vec2 FlanCenter = FlanEnemy[Rand(0, 3)].Center;
					for (float dir = Rand(0, 359), ii = 0; ii < 40; ii++)
						CreateBullet(FlanCenter, dir += 9, 3, 0);
				});
				for (int i = 0; i < 9; i++)
					CreateChart(BeatTime(i * 3), BeatTime(2), 6.5f, new string[]
					{
						"Shoot", "", "", "Shoot", "", "", "Shoot", "",
						"", "Shoot", "Shoot", "", "", "Shoot", "", "Shoot"
					});
			}
			for (int ii = 1; ii <= 4; ii++)
			{
				if (InBeat(104.75f + ii * 0.5f))
				{
					ScreenDrawing.SceneOut(col.White, 5);
					PlaySound(Sounds.spearAppear);
					for (float i = 0, dir = Rand(0, 359); i < 4; i++)
					{
						FlanBullet bul = CreateBullet(FlanEnemy[ii % 4].Center, dir, 0, 4);
						DelayBeat(2, () =>
						{
							bul.Speed = GetVector2(6, dir += 90);
							PlaySound(Sounds.spearShoot);
						});
					}
					if ((ii % 4) != 0)
						FlanEnemy[ii].Dispose();
				}
			}
			if (InBeat(109.65f))
			{
				FlanEnemy[0].Target = FlanEnemy[0].Center = new(210, 100);
				foreach (FlanBullet bul in GetAll<FlanBullet>())
					bul.Dispose();
			}
			if (InBeat(109))
			{
				ScreenDrawing.SceneOut(col.Black, BeatTime(1));
				RegisterFunctionOnce("RB", () =>
				{
					for (int i = 0, dir = Rand(0, 359); i < 4; i++)
					{
						FlanBullet bul = CreateBullet(new(Rand(100, 300), Rand(30, 100)), dir += 90, 4);
						bul.Alpha = 0;
						bul.AlphaFade = 15f;
					}
					PlaySound(Sounds.pierce);
				});
				RegisterFunctionOnce("Sweep", () =>
				{
					bool way = Arguments[0] == 0;
					for (int i = 0, ii = 0; i < 30; i++)
					{
						Delay(i, () =>
						{
							FlanBullet bul = CreateBullet(new(way ? (390 - ii * 6) : (30 + ii * 6), 20), 90, 6f - ii / 30f);
							bul.Alpha = 0;
							bul.AlphaFade = 5f;
							PlaySound(Sounds.spearAppear);
							ii++;
						});
					}
				});
				RegisterFunctionOnce("FlashBul", () =>
				{
					ScreenDrawing.SceneOut(col.White, 5);
					float dir = Rand(0, 359);
					for (int i = 0; i < 360; i += 60)
					{
						FlanBullet b = CreateBullet(Heart.Centre + GetVector2(200, i + dir), i + dir + 180, 0, 1);
						RunEase((s) => b.Speed = GetVector2(s, b.Rotation), EaseIn(BeatTime(1), -2, 8, EaseState.Back));
					}
				});
				CreateChart(BeatTime(0), BeatTime(2), 7, new string[]
				{
					"RB", "", "RB", "", "RB", "", "RB", "",
					"RB", "", "RB", "", "RB", "", "", "",
					"", "RB", "", "RB", "", "RB", "", "RB",
					"", "<0>Sweep", "", "", "", "", "", "<1>Sweep",
					"", "", "", "", "", "<0>Sweep", "", "",
					"", "", "", "<1>Sweep", "", "", "", "",

					"RB", "", "RB", "", "RB", "", "RB", "",
					"RB", "", "RB", "", "RB", "", "", "",
					"", "RB", "", "RB", "", "RB", "", "RB",
					"", "FlashBul", "", "", "", "FlashBul", "", "",
					"", "FlashBul", "", "", "", "FlashBul", "", "",
					"", "", "", "", "", "", "", "",

					"RB", "", "RB", "", "RB", "", "RB", "",
					"RB", "", "RB", "", "RB", "", "", "",
					"", "RB", "", "RB", "", "RB", "", "RB",
					"", "<0>Sweep", "", "", "", "", "", "<1>Sweep",
					"", "", "", "", "", "<0>Sweep", "", "",
					"", "", "", "<1>Sweep", "", "", "", "",
				});
			}
			if (InBeat(146))
			{
				FlanEnemy[0].Target = new(50, 100);
				FlanBullet bul = CreateBullet(new(75, 130), 0, 0, 7, (int)BulletMode.LaserBody);
				bul.Scale.X = 0;
				bul.Rotation = 270;
				bul.Duration = BeatTime(6);
				bul = CreateBullet(new(75, 130), 0, 0, 8, (int)BulletMode.LaserHead);
				bul.Scale = Vector2.Zero;
				bul.Duration = BeatTime(6);
			}
			if (InBeat(149, 152))
			{
				FlanEnemy[0].Target.X += 2;
				if (At0thBeat(0.2f))
				{
					for (int i = 0; i < 5; i++)
					{
						FlanBullet bul = CreateBullet(FlanEnemy[0].Center + new vec2(0, 50 + i * 50 + (GametimeF - BeatTime(149)) / 2), 0, 0);
						RunEase((s) => bul.Speed = GetVector2(s, 0), EaseOut(BeatTime(2), 0.5f, 1, EaseState.Quad));
					}
				}
			}
			if (InBeat(153))
			{
				FlanBullet bul = CreateBullet(Vector2.Zero, 0, 0, 7, (int)BulletMode.LaserBody);
				bul.Scale.X = 0;
				bul.Rotation = 270;
				bul.Duration = BeatTime(3);
				bul = CreateBullet(Vector2.Zero, 0, 0, 8, (int)BulletMode.LaserHead);
				bul.Scale = Vector2.Zero;
				bul.Duration = BeatTime(3);
			}
			if (InBeat(153, 155))
			{
				FlanEnemy[0].Target.X -= 3;
				if (At0thBeat(0.2f))
				{
					for (int i = 0; i < 5; i++)
					{
						FlanBullet bul = CreateBullet(FlanEnemy[0].Center + new vec2(0, 50 + i * 50 + (GametimeF - BeatTime(153)) / 2), 0, 0);
						RunEase((s) => bul.Speed = GetVector2(s, 0), EaseOut(BeatTime(2), 0.5f, 1, EaseState.Quad));
					}
				}
			}
			if (InBeat(156) || InBeat(156.5f) || InBeat(157))
			{
				for (int i = 0; i < 8; i++)
					for (int j = 0; j < 3; j++)
					{
						CreateBullet(FlanEnemy[0].Center, Direction(FlanEnemy[0].Centre, Heart.Centre) + 25 + i * 45, 5 + j, 5);
					}
			}
			if (InBeat(157.5f))
			{
				FlanEnemy[0].Center = FlanEnemy[0].Target = new(210, 60);
				ScreenDrawing.WhiteOut(BeatTime(0.5f));
				foreach (FlanBullet b in GetAll<FlanBullet>())
					b.Dispose();
				InstantTP(210, 320);
			}
			if (InBeat(158, 182) && At0thBeat(0.25f))
			{
				for (int i = 0; i < 2; i++)
				{
					vec2 Target = GetVector2(300, GametimeF * 1.5f + i * 180) + new vec2(210, 240);
					Target = vec2.Clamp(Target, new vec2(BoxStates.Left, BoxStates.Up), new vec2(BoxStates.Right, BoxStates.Down));
					FlanBullet b = CreateBullet(Target, Direction(Target, new vec2(210, 240)), 2, 1);
					b.Alpha = 0;
					b.AlphaFade = 5;
				}
			}
			if (InBeat(182, 204) && At0thBeat(0.25f))
			{
				for (int i = 0; i < 2; i++)
				{
					vec2 Target = GetVector2(300, GametimeF * 1.5f * (i == 0 ? 1 : -1)) + new vec2(210, 240);
					Target = vec2.Clamp(Target, new vec2(BoxStates.Left, BoxStates.Up), new vec2(BoxStates.Right, BoxStates.Down));
					FlanBullet b = CreateBullet(Target, Direction(Target, Heart.Centre), 2, 2);
					b.Alpha = 0;
					b.AlphaFade = 5;
				}
			}
			if (InBeat(205, 250))
			{
				if (At0thBeat(2))
					FlanEnemy[0].RandMove(100, 320, 50, 150);
				if (At0thBeat(GametimeF <= BeatTime(230) ? 6 : 3))
				{
					if (At0thBeat(GametimeF <= BeatTime(230) ? 12 : 6))
					{
						for (int i = -1; i <= 1; i++)
							CreateBullet(FlanEnemy[0].Center, Direction(FlanEnemy[0].Center, Heart.Centre) + i * 30, 4, 5, (int)BulletMode.HitTheHitAndMove);
					}
					else
						CreateBullet(FlanEnemy[0].Center, Direction(FlanEnemy[0].Center, Heart.Centre), 4, 5, (int)BulletMode.HitTheHitAndMove);
				}
				else if (At0thBeat(GametimeF <= BeatTime(230) ? 1 : 0.5f))
				{
					vec2 Init = vec2.Zero;
					Init = new vec2(Rand(BoxStates.Left + 90, BoxStates.Right - 90), Rand(BoxStates.Up +120, BoxStates.Down - 120));
					while (GetDistance(Init, Heart.Centre) < 70)
						Init = new vec2(Rand(BoxStates.Left + 90, BoxStates.Right - 90), Rand(BoxStates.Up + 120, BoxStates.Down - 120));
					float dir = Direction(Init, Heart.Centre);
					dir = Rand(dir - 390, dir - 30);
					dir = MathF.Round(dir / 45) * 45;
					while (new CollideRect(new vec2(BoxStates.Left, BoxStates.Up), new vec2(BoxStates.Width * 2, BoxStates.Height * 2)).Contain(Init))
						Init += GetVector2(1, dir);
					for (int i = 0; i < 60; i++)
					{
						vec2 Target = GetVector2(-i * 25, dir) + Init;
						FlanBullet b = CreateBullet(Target, 0, 0, 3, (int)BulletMode.HitAndMove);
						b.Alpha = 0;
						b.AlphaFade = 20 + i * 2;
					}
				}
				foreach (FlanBullet b in GetAll<FlanBullet>())
				{
					if (b.Image == Bullet[3] && !new CollideRect(new vec2(BoxStates.Left - 10, BoxStates.Up - 10), new vec2(BoxStates.Width + 20, BoxStates.Height + 20)).Contain(b.Centre))
					{
						b.MarkScore = false;
						b.Dispose();
					}
				}
			}
			if (InBeat(253.5f))
			{
				ScreenDrawing.WhiteOut(10);
				foreach (FlanBullet b in GetAll<FlanBullet>())
					b.Dispose();
			}
			if (InBeat(254, 290))
			{
				if (At0thBeat(1))
				{
					float dir = Rand(0, 359);
					for (int i = 0, imax = GametimeF < BeatTime(277) ? 10 : 15; i < imax; i++)
					{
						FlanBullet b = CreateBullet(Heart.Centre + GetVector2(90, dir + i * 360 / imax), dir + i * 360 / imax, 0);
						b.Alpha = 0;
						b.AlphaFade = BeatTime(1.25f);
						DelayBeat(2, () => RunEase((s) => b.Speed = GetVector2(s, b.Rotation), EaseIn(BeatTime(2), 0, imax == 10 ? -2 : -3, EaseState.Back)));
					}
					if (GametimeF >= BeatTime(277) && At0thBeat(2))
					{
						FlanEnemy[0].RandMove(80, 340, 80, 150);
						dir = Rand(0, 359);
						for (int i = 0; i < 15; i++)
						{
							FlanBullet b = CreateBullet(FlanEnemy[0].Center, dir + i * 24, 0);
							b.Alpha = 0;
							b.AlphaFade = BeatTime(1.25f);
							DelayBeat(2, () => RunEase((s) => b.Speed = GetVector2(s, b.Rotation), EaseIn(BeatTime(2), 0, -4, EaseState.Back)));
						}
					}
				}
			}
			if (InBeat(292))
				FlanEnemy[0].Target = new(210, 60);
			if (InBeat(297, 300))
			{
				FlanEnemy[0].Target.Y += 2;
				if (At0thBeat(0.4f))
				{
					for (int i = 0; i < 5; i++)
						for (int j = 0; j < 3; j++)
							for (int k = -1; k < 2; k++)
								CreateBullet(FlanEnemy[0].Center, Direction(FlanEnemy[0].Center, Heart.Centre) + i * 72 + k * 5, 7 + j, 9);
				}
			}
			if (InBeat(300, 349))
			{
				if (At0thBeat(20))
					CreateFiveBounceBullet(0);
				else if (AtKthBeat(20, BeatTime(4)))
					CreateFiveBounceBullet(30);
				else if (AtKthBeat(20, BeatTime(8)))
					CreateFiveBounceBullet(-30);
				else if (AtKthBeat(20, BeatTime(12)))
					FlanEnemy[0].Target = new(60, 400);
				else if (AtKthBeat(20, BeatTime(14)))
					RunEase((s) =>
					FlanEnemy[0].Target = FlanEnemy[0].Center = new(s, 400),
					EaseOut(BeatTime(6), 60, 360, EaseState.Linear));
				if (AtKthBeat(20, BeatTime(14)) || AtKthBeat(20, BeatTime(15)) || AtKthBeat(20, BeatTime(16)) || AtKthBeat(20, BeatTime(17)) || AtKthBeat(20, BeatTime(18)))
				{
					int MedAmt = Rand(5, 10);
					vec2 Spawn = FlanEnemy[0].Center;
					float Dir = -90;
					CreateBullet(Spawn, Dir, 5, 5, (int)BulletMode.Bounce).BounceCount = 5;
					for (int j = 0; j < 20; j++)
					{
						bool Small = j < MedAmt;
						CreateBullet(Spawn + GetVector2(Rand(-3, 3f), Dir), Dir + Rand(-2f, 2), Rand(Small ? 3 : 4, Small ? 4 : 5), Small ? 1 : 10, (int)BulletMode.Bounce).BounceCount = 5;
					}
				}
			}
			if (InBeat(349))
			{
				foreach (FlanBullet b in GetAll<FlanBullet>())
					b.BounceCount = 0;
				FlanEnemy[0].Target = new(210, 60);
			}
			if (InBeat(349.25f))
			{
				Heart.Scale = 0.5f;
				RegisterFunctionOnce("B", () =>
				{
					PlaySound(Sounds.pierce);
					vec2 Pos = new(Rand(50, 370), Rand(50, 120));
					float dir = Rand(0, 359);
					for (int i = 0; i < 36; i++)
						CreateBullet(Pos, dir + i * 10, 2.5f, 9, (int)BulletMode.Bounce).BounceCount = 1;
				});
				string[] chart = new string[] { };
				string[][] strtemp = new string[][]
				{
					new string[]
					{
						"B", "", "", "", "B", "", "", "",
						"B", "", "", "", "B", "", "", "",
						"B", "", "", "", "B", "", "", "",
						"(B)(B)", "", "", "", "", "", "(B)(B)", "",
						"", "", "", "", "(B)(B)", "", "", "",
						"", "", "(B)(B)", "", "", "", "", "",
					},
					new string[]
					{
						"B", "", "", "", "B", "", "", "",
						"B", "", "", "", "B", "", "", "",
						"B", "", "", "", "B", "", "", "",
						"(B)(B)", "", "", "", "(B)(B)", "", "", "",
						"(B)(B)", "", "", "", "(B)(B)", "", "", "",
						"", "", "", "", "", "", "", "",
					}
				};
				chart = chart.Concat(strtemp[0]).ToArray();
				chart = chart.Concat(strtemp[1]).ToArray();
				chart = chart.Concat(strtemp[0]).ToArray();
				CreateChart(0, BeatTime(2), 7, chart = chart.Concat(new string[] {
					"B", "", "", "", "", "", "B", "",
					"B", "", "B", "", "B", "", "", "",
					"B", "", "", "", "B", "", "", "",
					"B", "", "B", "", "B", "", "B", "",
					"B", "", "B", "", "B", "", "B", "",
					"B", "", "B", "", "B", "", "B", "",
				}).ToArray());
			}
			if (InBeat(397.5f))
			{
				ScreenDrawing.WhiteOut(10);
				foreach (FlanBullet b in GetAll<FlanBullet>())
					b.Dispose();
			}
			if (InBeat(406.5f))
			{
				ScreenDrawing.WhiteOut(2);
				JudgementState JudgeState = GameStates.CurrentScene is SongFightingScene
					? (GameStates.CurrentScene as SongFightingScene).JudgeState
					: JudgementState.Lenient;
				int Score = JudgeState switch
				{
					JudgementState.Strict => 400000,
					JudgementState.Balanced => 392000,
					JudgementState.Lenient => 384000
				};
				for (int i = 0; i < 4000; i++) CreateBullet(Vector2.Zero, 0, 0).Dispose();
			}
			if (InBeat(435))
				EndSong();
		}

		#region Vars
		static string Root = "Musics\\U. N. Owen Was Hero\\";
		private static Texture2D LoadingScreen, SideCircle, Tex, SpellBG, Portrait;
		/// <summary>
		/// Bullet: Small red, Small Blue Orb, Small Purple Orb, Small Green Orb, Big Red, Big Blue, Small Red Orb, Red Laser, Medium Red, Small Blue, Medium Blue
		/// </summary>
		private static Texture2D[] Word = new Texture2D[5], Idle = new Texture2D[4], Move = new Texture2D[2], Bullet = new Texture2D[11];
		private static vec2 ScrCen = new(320, 240);
		private static Flan[] FlanEnemy = new Flan[4];
		int Score = 0;
		/// <summary>
		/// Score reuqired, achieved
		/// </summary>
		private List<Tuple<int, bool>> ScoreExtend = new();
		private FlanBullet CreateBullet(vec2 Center, float dir, float speed, int index = 0, int mode = 0)
		{
			FlanBullet En;
			CreateEntity(En = new FlanBullet(Center, index, GetVector2(speed, dir)) { Mode = mode, game = this, Rotation = dir });
			return En;
		}
		private class SideBar : Entity
		{
			float alpha = 0;
			int state = 0;
			public UNOwen game;
			static vec2[] WordPos = new vec2[] { new(440, 280), new(510, 280), new(510, 350), new(510, 410), new(580, 410) };
			public SideBar()
			{
				LoadingScreen = Loader.Load<Texture2D>(Root + "Loading");
				SideCircle = Loader.Load<Texture2D>(Root + "Side Circle");
				Tex = Loader.Load<Texture2D>(Root + "Tex");
				for (int i = 0; i < 5; i++)
					Word[i] = Loader.Load<Texture2D>($"{Root}Word {i + 1}");
				SpellBG = Loader.Load<Texture2D>(Root + "Spell BG");
				Portrait = Loader.Load<Texture2D>(Root + "Portrait");
			}
			public override void Update()
			{
				if (GametimeF < 600)
					state = 0;
				else if (GametimeF < 9999)
					state = 1;
				switch (state)
				{
					case 0:
						ScreenDrawing.UIColor = col.Transparent;
						if (GametimeF < 300)
							alpha += 1 / 300f;
						if (GametimeF > 360)
							alpha -= 1 / 240f;
						break;
					case 1:
						UI.HPShowerPos = new(320, 443);
						UI.NameShowerPos = new(20, 457);
						ScreenDrawing.ThemeColor = col.Transparent;
						ScreenDrawing.MasterAlpha = (GametimeF - 600) / 120f;
						ScreenDrawing.HPBar.AreaOccupied = new(0, 0, 0, 0);
						ScreenDrawing.BoxBackColor = col.Transparent;
						Heart.Speed = 3;
						break;
				}
				alpha = Clamp(alpha, 0, 1);
			}
			public override void Draw()
			{
				switch (state)
				{
					case 0:
						FormalDraw(LoadingScreen, ScrCen, col.White * alpha, 1, 0, ScrCen);
						break;
					case 1:
						for (int i = 0; i < 4; i++)
							for (int ii = 0; ii < 30; ii++)
								FormalDraw(Tex, new vec2(i * 160, ii * 16), col.White, 1, 0, new(0));
						FormalDraw(SideCircle, new(447, 290), col.White, 1.25f, 0, new(0));
						for (int i = 0; i < 5; i++)
							FormalDraw(Word[i], WordPos[i], col.White, 1, 0, new(0));
						FormalDraw(SpellBG, new(30, 20), col.White, new vec2(360f / 256f, 440f / 256f), 0, new(0));
						GLFont F = FightResources.Font.NormalFont;
						int d = (int)(GametimeF / 62.5f * 60f);
						if (d < 0) d = 0;
						int min = d / 3600, sec = d / 60 % 60, ms = d % 60;
						F.CentreDraw($"{min}:{(sec < 10 ? "0" : "") + sec}:{(ms < 10 ? "0" : "") + ms}",
							new Vector2(520, 60), col.White, 0.75f, 0.5f);
						F.CentreDraw("Score: ", new vec2(460, 100), col.White, 0.5f, 0.5f);
						string Score = game.Score.ToString();
						while (Score.Length < 9)
							Score = "0" + Score;
						F.Draw(Score, new vec2(480, 87), col.White, 0.75f, 0.5f);
						F.Draw("HP: ", new vec2(455, 115), col.White, 0.5f, 0.5f);
#if DEBUG
						F.Draw($"Inst C: {GetAll<Entity>().Length}", new vec2(455, 175), col.White, 0.5f, 0.5f);
						F.Draw($"Beat: {MathF.Round(GametimeF / game.SingleBeat, 2)}", new vec2(455, 195), col.White, 0.5f, 0.5f);
#endif
						for (int i = 0; i < HeartAttribute.HP; i++)
							FormalDraw(Sprites.player, new vec2(480 + i * 14, 120), col.Red, 0.7f, 0, new(0));
						if (game.InBeat(26, 45))
						{
							float TextAlpha = 0;
							if (game.InBeat(26, 35))
								TextAlpha = GametimeF < game.BeatTime(32) ? (GametimeF - game.BeatTime(26)) / 120f : (game.BeatTime(35) - GametimeF) / 120f;
							else
								TextAlpha = GametimeF < game.BeatTime(40) ? (GametimeF - game.BeatTime(35)) / 120f : (game.BeatTime(45) - GametimeF) / 120f;
							float PorAlpha = Math.Min(1, (GametimeF - game.BeatTime(26)) / 120f);
							FormalDraw(Portrait, new(200, 268), col.White * PorAlpha, 0.75f, 0, new(0));
							DrawingLab.DrawLine(new(80, 420), new(330, 420), 50, col.Red * 0.5f * PorAlpha, 0.5f);
							FightResources.Font.Japanese.Draw(GametimeF < game.BeatTime(35) ? "Did you come here to play?" : "Then let's begin!", new(90, 400), col.White * TextAlpha, 0.75f, 0.6f);
						}
						if (game.InBeat(398, 435))
						{
							float TextAlpha = 0;
							if (game.InBeat(398, 407))
								TextAlpha = GametimeF < game.BeatTime(402) ? (GametimeF - game.BeatTime(398)) / 120f : (game.BeatTime(407) - GametimeF) / 120f;
							else
								TextAlpha = GametimeF < game.BeatTime(430) ? (GametimeF - game.BeatTime(407)) / 120f : (game.BeatTime(435) - GametimeF) / 120f;
							float PorAlpha = Math.Min(1, (GametimeF - game.BeatTime(398)) / 120f);
							FormalDraw(Portrait, new(200, 268), col.White * PorAlpha, 0.75f, 0, new(0));
							DrawingLab.DrawLine(new(80, 420), new(330, 420), 50, col.Red * 0.5f * PorAlpha, 0.5f);
							FightResources.Font.Japanese.Draw(GametimeF < game.BeatTime(407) ? "I didn't expect you to get\nthis far..." : "Come play again!\nTK gives his thanks!", new(90, 400), col.White * TextAlpha, 0.75f, 0.6f);
						}
						if (GametimeF > game.BeatTime(406.5f))
						{
							FightResources.Font.NormalFont.CentreDraw("Spell Card Bonus!", new vec2(210, 150), col.Red, 0.5f, 0.6f);
							JudgementState JudgeState = GameStates.CurrentScene is SongFightingScene
					? (GameStates.CurrentScene as SongFightingScene).JudgeState
					: JudgementState.Lenient;
							string score = JudgeState switch
							{
								JudgementState.Strict => "400000",
								JudgementState.Balanced => "392000",
								JudgementState.Lenient => "384000"
							};
							FightResources.Font.NormalFont.CentreDraw($"+{score}", new vec2(210, 180), col.Pink, 2, 0.6f);
						}
						break;
				}
			}
		}
		private class Flan : Entity
		{
			public vec2 Center, Target, LastCenter = new(210, 60);
			vec2[] IdleDisplace = new vec2[] { new(0), new(2, 4), new(4, 12), new(1, 4) };
			bool IsIdle = true, IsMoving = false;
			int IdleTimer = 0, ReturnTimer = 0, MoveIndex;
			Texture2D sprite = Idle[0];
			public Flan()
			{
				Center = Target = LastCenter;
				for (int i = 0; i < 4; i++)
				{
					Idle[i] = Loader.Load<Texture2D>($"{Root}Idle {i}");
					if (i < 2)
						Move[i] = Loader.Load<Texture2D>($"{Root}Move {i}");
				}
			}
			public void RandMove(float MinX, float MaxX, float MinY, float MaxY)
			{
				while ((Target - Center).Length() < 60)
					Target = new((int)Rand(MinX, MaxX), (int)Rand(MinY, MaxY));
			}
			public override void Update()
			{
				if (Target != Center)
					Center = vec2.Ceiling(vec2.SmoothStep(Center, Target, 0.12f));
				if (IsIdle = !(IsMoving = (Target - Center).Length() >= 40))
					IdleTimer++;
				else
					IdleTimer = 0;
			}
			public override void Draw()
			{
				controlLayer = Surface.Hidden;
				int index = (int)(IdleTimer / 15) % 4;
				vec2 Displace = IsIdle ? IdleDisplace[index] : new(0);
				sprite = IsIdle ? Idle[index] : Move[1];
				float Dist = (Target - Center).Length();
				if (Dist >= 40 && Dist <= 55)
					sprite = Move[0];
				FormalDraw(sprite, Center + Displace, col.White, new vec2((Target.X - Center.X) >= 0 ? 1 : -1, 1), 0, new(sprite.Width / 2, sprite.Height / 2));
			}
		}
		private class FlanBullet : Entity, ICollideAble
		{
			bool beenIn = false;
			int scoreResult = 3, Index = 0;
			/// <summary>
			/// BulletMode enum
			/// </summary>
			public int Mode = 0, BounceCount = 1;
			private bool hasHit = false;
			public vec2 Speed = Vector2.Zero, Scale = Vector2.One;
			public UNOwen game;
			public float Alpha = 1, AlphaFade = 0, Duration = -1;
			public bool MarkScore = true;
			private JudgementState JudgeState
			{
				get
				{
					return GameStates.CurrentScene is SongFightingScene
						? (GameStates.CurrentScene as SongFightingScene).JudgeState
						: JudgementState.Lenient;
				}
			}
			public FlanBullet(vec2 pos, int index, vec2 speed)
			{
				Centre = pos;
				Image = Bullet[index];
				Index = index;
				Speed = speed;
			}
			public override void Update()
			{
				if (Alpha < 1)
					Alpha += 1f / AlphaFade;
				float ImgW = Image.Width, ImgH = Image.Height;
				bool Inside = new CollideRect(-ImgW, -ImgH, 640 + ImgW, 480 + ImgH).Contain(Centre += Speed);
				if (!beenIn)
					beenIn = Inside;
				else if (!Inside)
					Dispose();
				switch ((BulletMode)Mode)
				{
					case BulletMode.Bounce:
						RectangleBox box = FightBox.instance as RectangleBox;
						int Normal = 0;
						if (Centre.X <= box.Left)
							Normal = 90;
						if (Centre.Y <= box.Up)
							Normal = 180;
						if (Centre.X >= box.Right)
							Normal = 270;

						if (Normal != 0 && BounceCount > 0)
						{
							float pre_rot = Rotation;
							Rotation = 2 * Normal - Rotation;
							Speed = MathUtil.Rotate(Speed, Rotation - pre_rot);
							BounceCount--;
						}
						break;
					case BulletMode.LaserHead:
						Centre = FlanEnemy[0].Center + new vec2(0, 30);
						if (Duration > 0)
						{
							if (Scale.X < 1.5f)
								Scale += new Vector2(0.1f);
							Duration--;
						}
						else
						{
							if (Scale.X > 0)
								Scale -= new Vector2(0.1f);
							else
								Dispose();
						}
						break;
					case BulletMode.LaserBody:
						Centre = FlanEnemy[0].Center + new vec2(0, 30);
						if (Duration > 0)
						{
							if (Scale.X < 1)
								Scale.X += 0.1f;
							Duration--;
						}
						else
						{
							if (Scale.X > 0)
								Scale.X -= 0.1f;
							else
								Dispose();
						}
						break;
					case BulletMode.HitTheHitAndMove:
						foreach (FlanBullet b in GetAll<FlanBullet>())
						{
							float Dist = GetDistance(b.Centre, Centre);

							if (b.Alpha >= 0.8f && b.Mode == (int)BulletMode.HitAndMove && Dist < 70 && b.Speed.Length() == 0)
							{
								RunEase((s) => b.Speed = Speed * s,
									EaseOut(60, 0.1f, Rand(0.6f, 1), EaseState.Linear));
							}
						}
						break;
				}
			}
			public override void Draw()
			{
				controlLayer = Surface.Hidden;
				if (Mode == (int)BulletMode.LaserBody)
				{
					vec2 FinScale = new Vector2(1.5f, ((FightBox.instance as RectangleBox).Down - Centre.Y) / Image.Height) * Scale;

					FormalDraw(Image, Centre, col.White * Alpha, FinScale, GetRadian(Rotation + 90), new(Image.Width / 2, 0));
				}
				else
					FormalDraw(Image, Centre, col.White * Alpha, Scale, GetRadian(Rotation + 90), new(Image.Width / 2, Image.Height / 2));
			}
			public override void Dispose()
			{
				if (!hasHit && MarkScore)
					PushScore(scoreResult);
				base.Dispose();
			}
			public void GetCollide(Player.Heart heart)
			{
				if (Alpha < 1) return;
				float res = 0;
				if (Mode != (int)BulletMode.LaserBody)
					res = MathUtil.GetDistance(Centre, heart.Centre) - Math.Min(Image.Width, Image.Height) / 2;
				else
				{
					res = MathF.Abs(Centre.X - heart.Centre.X) - 16;
					if (heart.Centre.Y < Centre.Y)
						res = 10;
				}

				if (Index == 4 || Index == 5) res += 8;
				if (Index == 7) res += 4;
				if (res < 0) { scoreResult = 0; LoseHP(heart); }
				int offset = 3 - (int)JudgeState;
				bool needAP = ((CurrentScene as FightScene).Mode & GameMode.PerfectOnly) != 0;
				if (res < 1)
				{
					if (!hasHit)
						PushScore(0);
					LoseHP(Heart);
					hasHit = true;
				}
				else if (res <= 1.6f - offset * 0.4f)
				{
					if (scoreResult >= 2)
					{ scoreResult = 1; Player.CreateCollideEffect(Color.LawnGreen, 3f); }
				}
				else if (res <= 4.2f - offset * 1.2f)
				{
					if (scoreResult >= 3)
					{ scoreResult = 2; Player.CreateCollideEffect(Color.LightBlue, 6f); }
				}
				if (scoreResult != 3 && needAP && MarkScore && !hasHit)
				{
					PushScore(0);
					LoseHP(Heart);
					hasHit = true;
				}
			}
		}
		enum BulletMode
		{
			None = 0,
			Bounce = 1,
			LaserHead = 2,
			LaserBody = 3,
			HitAndMove = 4,
			HitTheHitAndMove = 5
		}
		private void CreateFiveBounceBullet(float DirPlus = 0)
		{
			float displace = Rand(-10f, 10);
			int MedAmt = Rand(5, 10);
			for (int i = -2; i < 3; i++)
			{
				vec2 Spawn = FlanEnemy[0].Center;
				float Dir = displace - 90 + i * 30 + DirPlus;
				CreateBullet(Spawn, Dir, 5, 5, (int)BulletMode.Bounce).BounceCount = 5;
				for (int j = 0; j < 20; j++)
				{
					bool Small = j < MedAmt;
					CreateBullet(Spawn + GetVector2(Rand(-3, 3f), Dir), Dir + Rand(-2f, 2), Rand(Small ? 4 : 4.5f, Small ? 4.5f : 5), Small ? 1 : 10, (int)BulletMode.Bounce).BounceCount = 5;
				}
			}
			FlanEnemy[0].RandMove(100, 320, 360, 420);
		}
		#endregion
		public void Easy() { }
		public void ExtremePlus() { }
		public void Noob() { }
		public void Normal() { }
	}
}