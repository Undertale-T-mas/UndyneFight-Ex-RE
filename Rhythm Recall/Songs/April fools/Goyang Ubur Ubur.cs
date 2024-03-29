using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;
namespace Rhythm_Recall.Waves
{
	public class Goyang : IChampionShip
	{
		Dictionary<string, Difficulty> dif = new();
		public Goyang()
		{
			dif.Add("div.1", Difficulty.Extreme);
		}
		public IWaveSet GameContent => new Project();
		public Dictionary<string, Difficulty> DifficultyPanel => dif;
		class Project : WaveConstructor, IWaveSet
		{
			public Project() : base(62.5f / (152f / 60f)) { }
			public string Music => "Goyang Ubur Ubur";

			public string FightName => "Goyang Ubur Ubur";

			public SongInformation Attributes => new Information();
			class Information : SongInformation
			{
				public Information() { MusicOptimized = true; }
				public override string SongAuthor => "Diego Takupaz";
				public override string BarrageAuthor => "TaKupaz";
				public override Dictionary<Difficulty, float> CompleteDifficulty => new(
				new KeyValuePair<Difficulty, float>[]
					{

					}
				);
				public override Dictionary<Difficulty, float> ComplexDifficulty => new(
					new KeyValuePair<Difficulty, float>[]
						{

						}
					);
				public override Dictionary<Difficulty, float> APDifficulty => new(
					new KeyValuePair<Difficulty, float>[]
						{

						}
					);
			}
			#region
			public void Noob() { }
			public void Easy() { }
			public void Normal() { }
			public void Hard() { }
			public void ExtremePlus() { }
			#endregion
			public void Extreme()
			{
				#region Intro
				if (InBeat(1))
				{
					CreateChart(BeatTime(4), BeatTime(1.175f), 6, new string[]
					{
						"", "R", "", "", "", "R", "", "",
						"", "", "", "", "", "", "", "",
						"", "", "", "", "", "", "", "",
						"R", "", "R", "", "", "", "R", "",
						"R", "", "", "R", "", "R", "", "",
						"", "", "", "", "", "", "", "",
						"", "", "", "", "", "", "", "",
						"", "", "R", "", "", "R", "", "",
						"", "", "R", "", "", "", "", "",
						"R", "", "", "", "", "", "", "",
						"", "", "", "", "", "", "", "",
						"", "", "", "R", "", "", "R", "",
						"", "R", "", "", "", "", "", "",
						"", "", "", "", "", "", "", "",
						"", "", "R", "", "R", "", "", "R",
						"", "", "R", "", "", "", "R", "",
						"", "", "R", "", "", "", "", "",
						"", "", "", "", "", "", "", "",
						"", "", "", "", "", "", "", "",
						"R", "", "", "", "R", "", "", "",
						"R", "", "", "", "R", "", "", "",
						"R", "", "", "", "R", "", "", "",
						"", "", "", "", "", "", "", "",
					});
				}
				if (InBeat(33.25f))
				{
					foreach (Line l in GetAll<Line>())
					{
						RunEase((s) => l.Alpha = s,
							EaseOut(BeatTime(4), 0.3f, 0, EaseState.Linear));
						DelayBeat(4, () => l.Dispose());
					}
				}
				#endregion
				#region Part 1 Blue
				if (InBeat(36))
				{
					SetSoul(0);
					SetBox(240, 180, 180);
				}
				if (InBeat(36, 72) && AtKthBeat(3, BeatTime(1.5f)))
				{
					SetSoul(2);
					int dir = Rand(0, 3) * 90;
					Heart.GiveInstantForce(dir, 5);
					CustomBone bone;
					int AppearCol = Rand(0, 2);
					CreateEntity(new Boneslab(dir, 30, BeatTime(1.5f), BeatTime(1)));
					CreateEntity(new Boneslab(dir, 60, BeatTime(2.5f), BeatTime(0.25f)));
					PlaySound(Sounds.boneSlabSpawn);
					DelayBeat(1.5f, () => PlaySound(Sounds.pierce));
					DelayBeat(2.5f, () => PlaySound(Sounds.pierce));
					for (int i = -10; i <= 10; i++)
					{
						if (Posmod(i, 3) != AppearCol) continue;

						bone = new(Stable(0, new Vector2(320, 240) + GetVector2(95, dir) + GetVector2(i * 10, dir + 90)), EaseOut(BeatTime(3), dir + 90, dir, EaseState.Quad), 0);
						bone.LengthRoute = Motions.LengthRoute.sin;
						bone.LengthRouteParam = new float[] { 60, BeatTime(4), 0, BeatTime(2) };
						bone.ColorType = Posmod(i, 3);
						bone.AlphaIncrease = true;
						CreateBone(bone);

						bone = new(Stable(0, new Vector2(320, 240) - GetVector2(95, dir) + GetVector2(i * 10, dir - 90)), EaseOut(BeatTime(3), dir - 90, dir, EaseState.Quad), 0);
						bone.LengthRoute = Motions.LengthRoute.sin;
						bone.LengthRouteParam = new float[] { 60, BeatTime(4), 0, BeatTime(2) };
						bone.ColorType = Posmod(i, 3);
						bone.AlphaIncrease = true;
						CreateBone(bone);
					}
					dir -= 90;
					Vector2 Start = GetVector2(100, dir) + new Vector2(320, 240),
							Displace = GetVector2(120, dir);
					bone = new(LinkEase(Stable(BeatTime(1), Start),
						EaseOut(BeatTime(1), -Displace, EaseState.Quad),
						EaseOut(BeatTime(1), Displace, EaseState.Quad)),
						Stable(0, dir), 175);
					CreateBone(bone);
					DelayBeat(3, () => bone.Dispose());
				}
				#endregion
				#region Part 2 Blue
				if (InBeat(74))
				{
					SetBox(290, 320, 160);
					SetSoul(2);
					Heart.InstantSetRotation(0);
					for (int i = 0; i < 4; i++)
						CreateBone(new SwarmBone(70, BeatTime(12), BeatTime(i * 3), BeatTime(38)) { ColorType = (i % 2) + 1, Extras = 0 });
				}
				if (InBeat(74, 110))
				{
					if (At0thBeat(3))
					{
						EaseUnit<Vector2>[] ease = new EaseUnit<Vector2>[12];
						for (int i = 0; i < 12; i++)
							ease[i] = EaseOut(BeatTime(2.5f), new Vector2(-140, 0), EaseState.Quad);
						CreatePlatform(new(1, new Vector2(500, 320), LinkEase(ease), 15, 50));
					}
					if (GametimeF > BeatTime(75) && At0thBeat(0.1f))
						CreateBone(new DownBone(true, 4, Rand(10f, 40f)) { Rotation = Rand(-10f, 10f), MarkScore = false });
					if (AtKthBeat(2.35f, BeatTime(1.35f)))
					{
						CreateGB(new NormalGB(new Vector2(Rand(100, 540), 180), Heart.Centre, new(1, 0.5f), BeatTime(2), BeatTime(0.4f)));
					}
				}
				if (InBeat(111)) SetSoul(0);
				if (InBeat(110, 145))
				{
					bool[] TimeCheck =
					{
						GametimeF <= BeatTime(125) && At0thBeat(4.5f),
						GametimeF >= BeatTime(125) && AtKthBeat(1.125f, BeatTime(0.25f)) && GametimeF <= BeatTime(135),
						GametimeF >= BeatTime(135) && At0thBeat(0.5f) && GametimeF <= BeatTime(140),
						GametimeF >= BeatTime(140) && At0thBeat(0.25f)
					};
					if (TimeCheck[0] || TimeCheck[1] || TimeCheck[2] || TimeCheck[3])
					{
						PlaySound(Sounds.pierce);
						CreateBone(new DownBone(true, TimeCheck[3] ? 4 : 2, 154) { ColorType = TimeCheck[1] ? Rand(1, 2) : 1 });
					}
					if (GametimeF <= BeatTime(125) && At0thBeat(2))
					{
						CreateBone(new UpBone(false, 3, 75));
						CreateBone(new DownBone(true, 3, 75));
					}
				}
				if (InBeat(145)) SetBox(270, 240, 120);
				if (InBeat(146)) SetBox(260, 180, 100);
				if (InBeat(147)) SetBox(250, 120, 90);
				if (InBeat(148)) { SetGreenBox(); TP(); }
				if (InBeat(148.5f)) SetSoul(1);
				if (InBeat(145, 148) && At0thBeat(1))
				{
					float Cur = ScreenDrawing.ScreenScale;
					RunEase((s) => ScreenDrawing.ScreenScale = s, EaseOut(BeatTime(1), Cur, Cur + 0.3f, EaseState.Back));
				}
				if (InBeat(148.75f))
				{
					RunEase((s) => ScreenDrawing.ScreenScale = s, EaseOut(BeatTime(1), ScreenDrawing.ScreenScale, 1, EaseState.Quad));
				}
				#endregion
				#region Part 3 Green
				if (InBeat(144.6f))
				{
					string R2 = "(R)(R1)";
					CreateChart(BeatTime(4), BeatTime(2.35f), 7, new string[]
					{
						R2 + R2, "", "", "", R2, "", R2, "",
						R2, "", R2, "", R2, "", R2, "",
						R2, "", R2, "", R2, "", R2, "",
						R2, "", R2, "", R2, "", R2, "",
						R2, "", R2, R2, R2, "", "", "",
						R2, "", R2, R2, R2, "", R2, "",
						R2, "", R2, "", R2, "", R2, "",
						R2, "", R2, "", R2, "", R2, "",
						R2, R2, R2,
					});
					/* Among Us
					CreateChart(BeatTime(4), BeatTime(2.35f), 7, new string[]
					{
						"", R2, "", "", "", R2, "", "", "",
						R2, "", "", "", R2, "", R2, "",
						R2, "", "", R2, R2, R2, R2, R2,
						R2, "", "", R2, R2, R2, R2, R2,
						R2, "", "", R2, R2, "", R2, "",
						R2, "", "", R2, R2, "", R2, "",
						R2, R2, R2, "", R2, R2, R2, "",
						R2, "", R2, "", R2, "", R2, "",
						R2, "", R2, "", R2, "", R2, "",
					});
					*/
				}
				#endregion
				#region Part 4 Red + Green
				if (InBeat(185))
				{
					string L = "($21)", R = "($01)";
					CreateChart(BeatTime(3.25f), BeatTime(2.35f), 6, new string[]
					{
						R, "", R, "", R, "", "", "",
						L, "", R, "", L, R, L, "",
						R, L, R, "", L + R, "", "", "",
						R, "", R, "", L + R, "", "", "",

						L, "", L, "", L, "", "", "", R,
						"", L, "", R, L, R, "", L,
						R, L, "", R, "", R, "", L,
						"", L, "", L + R, "", "", "",

						R, "", R, "", R, "", "", "",
						L, "", R, "", L, R, L, "",
						R, L, R, "", L + R, "", "", "",
						R, "", R, "", L + R, "", "", "",

						L, "", L, "", L, "", "", "", R,
						"", L, "", R, L, R, "", L,
						R, L, "", R, "", R, "", L,
						"", L, "", L + R, "", "", "",
					});
				}
				if (InBeat(187))
				{
					Heart.Split();
					SetPlayerBoxMission(1);
					SetSoul(0);
					SetBox(new Vector2(320, 240), 190, 190);
					TP(320, 170);
				}
				if (InBeat(187, 220))
				{
					SetPlayerBoxMission(1);
					Vector2 cen = Heart.Centre;
					RectangleF box = new(320 - 42 - 18, 240 - 42 - 18, 84 + 36, 84 + 36);
					if (box.Contains(new RectangleF(cen.X - 8, cen.Y - 8, 16, 16)))
						InstantTP(Heart.LastCentre);
					SetPlayerBoxMission(0);
					if (At0thBeat(2.25f))
					{
						for (int i = 0; i < 4; i++)
						{
							SetBoxMission(1);
							CreateBone(new SideCircleBone(i * 90, 3, 20, BeatTime(1)));
							SetPlayerBoxMission(0);
						}
					}
					if (AtKthBeat(2.25f, BeatTime(1.125f)))
					{
						float dir = Rand(0, 359f);
						for (int i = 0; i < 15; i++)
						{
							CreateBone(new CustomBone(
								LinkEase(Stable(0, new Vector2(320, 240) + GetVector2(320, i * 24 + dir)), InfLinear(GetVector2(-5, i * 24 + dir)))
								, Stable(0, i * 24 + 90 + dir), 30)
							{
								IsMasked = false,
								AlphaIncrease = true
							});
						}
					}
				}
				if (InBeat(225))
				{
					SetPlayerBoxMission(1);
					TP(320, 240);
					SetGreenBox();
					Heart.Speed = 0;
				}
				if (InBeat(226))
				{
					SetPlayerBoxMission(0);
					Heart.MergeAll();
				}
				#endregion
				#region Part 5 Green
				if (InBeat(225))
				{
					SetPlayerBoxMission(0);
					for (int i = 0, iii = 0; i < 4; i++)
					{
						for (int ii = 0; ii < 8; ii++)
						{
							float Beat = 4 + i * 9.4f + ii * 1.175f;
							CreateArrow(BeatTime(Beat), i, 7, 1, 0, ArrowAttribute.ForceGreen | ArrowAttribute.Tap);
							CreateArrow(BeatTime(Beat), $"N{i}", 7, 0, 0);
							CreateArrow(BeatTime(Beat + 0.5375f), $"N{i}", 7, 0, 0);
							DelayBeat(Beat, () =>
								ScreenDrawing.CameraEffect.Convulse(10, BeatTime(1), (iii++ % 16) < 8));
						}
					}
					SetPlayerBoxMission(0);
				}
				if (InBeat(262.35f))
				{
					for (int i = 0, iii = 0; i < 4; i++)
					{
						for (int ii = 0; ii < 8; ii++)
						{
							float Beat = 4 + i * 9.4f + ii * 1.175f;
							CreateArrow(BeatTime(Beat), i, 7, 0, 0, ArrowAttribute.ForceGreen | ArrowAttribute.Tap);
							CreateArrow(BeatTime(Beat), $"N{i}", 7, 1, 0);
							CreateArrow(BeatTime(Beat + 0.5375f), $"N{i}", 7, 1, 0);
							DelayBeat(Beat, () =>
								ScreenDrawing.CameraEffect.Convulse(10, BeatTime(1), (iii++ % 16) < 8));
						}
					}
				}
				#endregion
				#region Part 6 Purple
				if (InBeat(304))
				{
					Heart.PurpleLineCount = 5;
					SetSoul(4);
					SetBox(240, 150, 84);
				}
				if (InBeat(305))
				{
					Heart.PurpleLineCount = 10;
					SetSoul(4);
					SetBox(240, 150, 300);
				}
				if (InBeat(307))
				{
					CreateBone(new CustomBone(new Vector2(240, 240), Motions.PositionRoute.YAxisSin, 0, 295, BeatTime(34))
					{
						PositionRouteParam = new float[] { 0, 80, BeatTime(4), 0 }
					});
					CreateBone(new CustomBone(new Vector2(400, 240), Motions.PositionRoute.YAxisSin, 0, 295, BeatTime(34))
					{
						PositionRouteParam = new float[] { 0, 80, BeatTime(4), 0 }
					});
				}
				if (InBeat(307, 341))
				{
					if (AtKthBeat(1.4f, SingleBeat))
						SetBox(240, Rand(100f, 200), 300);
					if (AtKthBeat(2.8f, BeatTime(2.4f)))
					{
						CreateGB(new NormalGB(new(100, Rand(90f, 390)), Heart.Centre, Vector2.One, 0, BeatTime(2.8f), BeatTime(1.4f)));
						CreateGB(new NormalGB(new(Rand(BoxStates.Left, BoxStates.Right), 50), Heart.Centre, new Vector2(1, 0.5f), 90, BeatTime(2.8f), BeatTime(0.14f)));
					}
					if (AtKthBeat(2.8f, SingleBeat))
					{
						CreateGB(new NormalGB(new(540, Rand(90f, 390)), Heart.Centre, Vector2.One, 180, BeatTime(1.4f), BeatTime(1.4f)));
						CreateGB(new NormalGB(new(Rand(BoxStates.Left, BoxStates.Right), 430), Heart.Centre, new Vector2(1, 0.5f), 270, BeatTime(1.4f), BeatTime(0.14f)));
					}
				}
				#endregion
				#region Part 7 Blue
				if (InBeat(345))
				{
					SetBox(240, 150, 300);
					Heart.ChangeColor(2);
					Heart.GiveInstantForce(270, 5);
					CustomBone[] bones = new CustomBone[4];
					for (int i = 0; i < 4; i++)
					{
						bones[i] = new(Stable(0, 320, 0), Stable(0, 0), 300) { Tags = new[] { i.ToString() }};
						bones[i].screenC = new(Vector2.Zero, Vector2.Zero);
						bones[i].LengthRoute = Motions.LengthRoute.autoFold;
						bones[i].LengthRouteParam = new float[] { 300, BeatTime(38) };
					}
					CreateEntity(bones);
				}
				if (InBeat(345, 416))
				{
					if (GametimeF < BeatTime(380))
					{
						if (AtKthBeat(4.7f, 10))
						{
							SetSoul(2);
							PlaySound(Sounds.Ding);
							Heart.GiveForce(AtKthBeat(9.4f, 10) ? 90 : 270, 1);
						}
						if (At0thBeat(2.1f))
							for (int i = 0, Y = Rand(80, 100); i < 8; i++)
								CreateSpear(new NormalSpear(new Vector2(100, Y + i * 45), 0, 5) { IsMute = i > 0 });
						if (At0thBeat(1.4f))
							CreatePlatform(new(0, new(Rand(180f, 460), 400), InfLinear(new(0, -3)), Rand(Heart.Rotation - 25f, Heart.Rotation + 25), 30));
					}
					for (int i = 0; i < 4; i++)
						foreach (CustomBone b in GetAll<CustomBone>(i.ToString()))
						{
							float dir = GametimeF + i * 90;
							Vector2 SinDist = GetVector2(200 + (i > 2 ? Sin(dir * 1.5f) : Cos(dir * 1.5f)) * 30, dir);
							Vector2 Target = Vector2.Lerp(b.Centre - SinDist, Heart.Centre, 0.2f);
							b.PositionRoute = Stable(0, Target + SinDist);;
							b.RotationRoute = Stable(0, dir + 90);
						}
					///Side Bones
					if (At0thBeat(2.35f))
					{
						bool Late = GametimeF >= BeatTime(383);
						Vector2 Displace = new Vector2(20, 0);
						CustomBone[] bones = new CustomBone[84];
						for (int i = 0; i < 21; i++)
						{
							Vector2 Start = new(230, 90 + i * 20);
							bones[i * 4] = new(LinkEase(false,
								Stable(i * 2, Start),
								EaseOut(10, Start, Start + Displace, EaseState.Quad),
								Stable(3, Start + Displace),
								EaseIn(10, Start + Displace, Start, EaseState.Circ)),
								Stable(0, 90), 15);
							Start += new Vector2(0, 10);
							bones[i * 4 + 1] = new(LinkEase(false,
								Stable(i * 2 + 10, Start),
								EaseOut(10, Start, Start + Displace, EaseState.Quad),
								Stable(3, Start + Displace),
								EaseIn(10, Start + Displace, Start, EaseState.Circ)),
								Stable(0, 90), 15) { ColorType = 2, MarkScore = false };

							Start = new(410, 390 - i * 20);
							bones[i * 4 + 2] = new(LinkEase(false,
								Stable(i * 2, Start),
								EaseOut(10, Start, Start - Displace, EaseState.Quad),
								Stable(3, Start - Displace),
								EaseIn(10, Start - Displace, Start, EaseState.Circ)),
								Stable(0, 90), 15);
							Start -= new Vector2(0, 10);
							bones[i * 4 + 3] = new(LinkEase(false,
								Stable(i * 2 + 10, Start),
								EaseOut(10, Start, Start - Displace, EaseState.Quad),
								Stable(3, Start - Displace),
								EaseIn(10, Start - Displace, Start, EaseState.Circ)),
								Stable(0, 90), 15) { ColorType = 2, MarkScore = false };
						}
						Delay(75, () => { foreach (CustomBone b in bones) b.Dispose(); });
						CreateEntity(bones);
					}
					if (InBeat(383))
					{
						Heart.GiveForce(0, 5);
						for (int i = 0; i < 4; i++)
						{
							float Rnd = Rand(0, BeatTime(2)); ;
							for (int j = 0; j < 2; j++)
							{
								CreateBone(new CustomBone(new Vector2(240 + j * 160, 330 - i * 60), Motions.PositionRoute.YAxisSin, 90, 100, BeatTime(33))
								{
									PositionRouteParam = new[] { 0, 60, BeatTime(4), Rnd },
									Alpha = 0, AlphaIncrease = true
								});
							}
							if (i < 3)
							{
								float time = (i + 1) * 4.85f, dur = i == 2 ? BeatTime(9) : BeatTime(1);
								CreateEntity(new Boneslab(0, (i + 1) * 60, BeatTime(time), dur));
								CreateEntity(new Boneslab(180, 180 - i * 60, BeatTime(time), dur));
								DelayBeat(time, () => PlaySound(Sounds.pierce));
							}
						}
					}
					if (InBeat(384))
					{
						SetSoul(5);
						Heart.UmbrellaAvailable = true;
						CreateEntity(new Boneslab(270, 150, BeatTime(0.5f), BeatTime(22)) { ColorType = 2 });
					}
					if (InBeat(398, 398 + 1.175f * 7) && AtKthBeat(1.175f, BeatTime(0.225f)))
					{
						PlaySound(Sounds.pierce);
						float dir = GametimeF * 45;
						for (int i = 0; i < 4; i++)
						{
							CreateBone(new CustomBone(GetVector2(300, dir + i * 90) + Heart.Centre, InfLinear(GetVector2(10, dir + i * 90 + 180)), dir + i * 90 + 90, 30));
						}
					}
					if (InBeat(406.5f, 411) && AtKthBeat(1.175f / 2, BeatTime(0.225f)))
					{
						PlaySound(Sounds.pierce);
						SetBox(FightBox.instance.Centre.Y - 4, 150, BoxStates.Height - 30);
					}
					if (InBeat(411, 416) && AtKthBeat(1.175f / 4, BeatTime(0.225f)))
					{
						SetSoul(2);
						Heart.GiveInstantForce(AtKthBeat(1.175f / 2, BeatTime(0.225f)) ? 180 : 0, 5);
					}
				}
				#endregion
				#region Part 8 Green
				if (InBeat(416.5f))
				{
					SetGreenBox();
					RegisterFunctionOnce("Shr", () =>
					{
						ScreenDrawing.CameraEffect.Convulse(3, BeatTime(1.175f), RandBool());
						if (RandBool())
							ScreenDrawing.CameraEffect.SizeShrink(3, BeatTime(1.175f));
						else
							ScreenDrawing.CameraEffect.SizeExpand(3, BeatTime(1.175f));
					});
					CreateChart(BeatTime(2.5f), BeatTime(1.175f), 7, new string[]
					{
						"R", "+1", "+1", "", "(*D)(*R1)(Shr)", "", "", "",
						"", "", "", "", "", "", "(R)(+21)", "",
						"", "", "", "", "$0", "$1", "$2", "",
						"$21", "$11", "$01", "", "$0", "$1", "$2", "",
						"$21", "$11", "$01", "", "$0", "$1", "$2", "",
						"$21", "$11", "$01", "", "(*D)(*R1)(Shr)", "", "", "",
						"", "", "", "", "$2", "$1", "$0", "",
						"$01", "$11", "$21", "", "$2", "$1", "$0", "",
						"$01", "$11", "$21", "", "$2", "$1", "$0", "",
						"$01", "$11", "$21", "", "(*D)(*R1)(Shr)", "", "", "",
						"", "", "", "", "$0", "$1", "$2", "",
						"$21", "$11", "$01", "", "$0", "$1", "$2", "",
						"$21", "$11", "$01", "", "$0", "$1", "$2", "",
						"$21", "$11", "$01", "", "(*D)(*R1)(Shr)", "", "", "",
						"(*D)(*D1)", "", "", "", "(*D)(*D1)", "", "", "",
						"(*D)(*D1)", "", "", "", "(*D)(*D1)", "", "", "",
						"(*D)(*D1)", "", "", "", "(*D)(*D1)", "", "", "",
						"(*D)(*D1)", "", "", "", "(*D)(*D1)(Shr)", "", "", "",
						"", "", "", "", "R1", "+21", "+21", "",
						"R", "+2", "+2", "", "R1", "+21", "+21", "",
						"R", "+2", "+2", "", "R1", "+21", "+21", "",
						"R", "+2", "+2", "", "(*D)(*D1)(Shr)", "", "", "",
						"", "", "", "", "R1", "+21", "+21", "",
						"R", "+2", "+2", "", "R1", "+21", "+21", "",
						"R", "+2", "+2", "", "R1", "+21", "+21", "",
						"R", "+2", "+2", "", "(*D)(*D1)(Shr)", "", "", "",
						"(*D)(*D1)", "", "", "", "R1", "+0", "+01", "",
						"R", "+01", "+0", "", "R1", "+0", "+01", "",
						"R", "+01", "+0", "", "R1", "+0", "+01", "",
						"R", "+01", "+0", "", "", "(*D)(*D1)", "", "", "", "",
						"", "", "(*D)(*D1)", "", "", "", "", "",
						"", "", "(*D)(*D1)", "", "", "", "", "",
						"", "", "(*D)(*D1)", "", "", "", "(*D)(*D1)", "",
					});
				}
				if (InBeat(417.5f))
				{
					SetSoul(1);
					TP();
					foreach (Bone b in GetAll<Bone>())
						b.Dispose();
				}
				if (InBeat(454.3625f) || InBeat(473.1625f))
				{
					CreateChart(BeatTime(4), BeatTime(1.175f), 7, new string[]
					{
						"(^R)(^R)(^R1)(^R1)", "", "", "", "D", ">+0", "<+0", "^+0",
						"D", "-1", "-1", "", "+01", "+11", "+11", "",
						"+0", "-1", "-1", "", "+01", "+11", "+11", "",
						"+0", "-1", "-1", "", "+01", "+11", "+11", "",
						"(D01)(+211)", "", "", "", "D1", ">+01", "<+01", "^+01",
						"D", "+2", "*+0", "", "D1", "+21", "*+01", "",
						"D", "+2", "*+0", "", "D1", "+21", "*+01", "",
						"D", "+2", "*+0", "", "D1", "+21", "*+01", "",
						"(D02)(D12)", "", "", "", "(D)(+21)", "(>+2)(<+21)", "(<+2)(>+21)", "(^+2)(^+21)",
						"*$1", "<+102", "+102", "", "*$11", "<+112", ">+112", "",
						"*$1", "<+102", ">+102", "", "*$11", "<+112", ">+112", "",
						"*$1", "<+102", ">+102", "", "*$11", "<+112", ">+112", "",
						"(^D)(^D1)", "", "", "", "($3)($11)", "($0)($21)", "($1)($31)", "($2)($01)",
						"*$3", ">+102", "<+102", "", "*$31", ">+112", "<+112", "",
						"*$3", ">+102", "<+102", "", "*$31", ">+112", "<+112", "",
						"*$3", ">+102", "<+102", "", "*$31", ">+112", "<+112", "",
					});
				}
				#endregion
				#region Part 9 Blue
				if (InBeat(498))
				{
					SetSoul(0);
					TP(320, 320);
					SetBox(320, 260, 180);
				}
				if (InBeat(499, 536.6f))
				{
					InstantSetBox(320, 260, 180 + Sin((GametimeF - BeatTime(499)) * 3) * 30);
					if (At0thBeat(2.375f))
					{
						CreateBone(new DownBone(true, 3.5f, 90));
						CreateBone(new UpBone(false, 5f, 120) { ColorType = 2 });
						CreateBone(new UpBone(true, 3.5f, 30) { ColorType = 1 });
						PlaySound(FightResources.Sounds.pierce);
					}
					if (AtKthBeat(2.375f, BeatTime(1.175f)))
					{
						CreateBone(new UpBone(false, 3.5f, 90));
						CreateBone(new DownBone(true, 5f, 120) { ColorType = 2 });
						CreateBone(new DownBone(false, 3.5f, 30) { ColorType = 1 });
						PlaySound(FightResources.Sounds.pierce);
					}
				}
				if (InBeat(537))
				{
					SetSoul(2);
					float height = BoxStates.Height;
					SetBox(320, height, height);
					Heart.GiveInstantForce(180, 10);
					CreateBone(new SideCircleBone(0, 3, 50, BeatTime(33)));
					CreateBone(new SideCircleBone(180, 3, 50, BeatTime(33)));
					CreatePlatform(new(1, new Vector2(320), RandBool() ? Motions.PositionRoute.cameFromDown : Motions.PositionRoute.cameFromUp, 180, 40, BeatTime(33)));
				}
				if (InBeat(540, 570))
				{
					if (At0thBeat(1.175f))
					{
						CreateBone(new CustomBone(new Vector2(200, 320), InfLinear(Vector2.Zero, GetVector2(4, 0)), 0, 50)); ;
					}
					if (At0thBeat(4.7f))
					{
						bool Down = At0thBeat(9.4f);
						for (int i = 0; i < 8; i++)
							CreateBone(Down ? new DownBone(true, BoxStates.Right + i * 25, 7, BoxStates.Height / 2 - 5) : new UpBone(false, BoxStates.Left - i * 25, 7, BoxStates.Height / 2 - 5));
					}
				}
				#endregion
				#region Part Final Green
				if (InBeat(164) || InBeat(571) || InBeat(589.8f))
				{
					string R2 = "(R)(R1)";
					CreateChart(BeatTime(4.3f), BeatTime(2.35f), 7, new string[]
					{
						R2 + R2, "", "", "", R2, "", R2, "",
						R2, "", R2, "", R2, "", R2, "",
						R2, "", R2, "", R2, "", R2, "",
						R2, "", R2, "", R2, "", R2, R2,
						R2, "", "", "", R2, "", R2, R2,
						R2, "", R2, "", R2, "", R2, "",
						R2, "", R2, "", R2, "", R2, "",
						R2, "", R2, "", R2, R2, R2, R2,
					});
				}
				if (InBeat(574))
				{
					TP();
					SetGreenBox();
					SetSoul(1);
					Heart.RotateTo(0);
				}
				if (InBeat(608.7f) || InBeat(627.5f))
				{
					string R2 ="(R)(R1)", D2 = "(D)(D1)";
					CreateChart(BeatTime(4), BeatTime(2.35f), 7, new string[]
					{
						R2 + R2, "", "", "", R2, "", R2, "",
						R2, "", "", "", R2, "", R2, "",
						"R", "+2", "+2", "", "R1", "+21", "+21", "",
						R2, "", "", "", R2, "", R2, "",
						R2, "", "", "", R2, "", R2, "",
						R2, "", "", "", R2, "", R2, "",
						"R", "+2", "+2", "", "R1", "+21", "+21", "",
						D2 , "", D2, "", D2, "", D2, "",
					});
				}
				#endregion
			}
			public void Start()
			{
				SetGreenBox();
				TP();
				SetSoul(1);
				Settings.GreenTap = true;
				HeartAttribute.MaxHP = HeartAttribute.HP = 10;
				bool jump = false;
				if (!jump)
				{
					Extends.DrawingUtil.BlackScreen(0, 0, BeatTime(12));
					//Lines
					RegisterFunctionOnce("L", () =>
					{
						Vector2 Start = new(320, Rand(20, 620));
						float AngStart = Rand(-15, 15), Dur = BeatTime(Rand(2f, 4f));
						Line l = new(
							LinkEase(false, Stable(BeatTime(33) - GametimeF, Start),
							EaseIn(Dur, Start, new Vector2(Rand(0, 640), Rand(700, 800)), EaseState.Back)),

							LinkEase(false, Stable(BeatTime(33) - GametimeF, AngStart),
							EaseIn(Dur, AngStart, -AngStart * 2, EaseState.Linear)));
						l.AlphaDecrease(BeatTime(2), 0.8f);
						CreateEntity(l);
					});
					for (int i = 0; i < 7; i++)
						CreateChart(BeatTime(i * 4.75f), BeatTime(1), 0, new string[]
						{
							"L", "", "", "", "L", "", "", "",
							"", "", "", "", "", "", "L", "",
							"", "", "L", "", "", "", "L", "",
							"", "", "L", "", "", "", "L", "",
						});
				}
				else
				{
					GametimeDelta = BeatTime(184);
					PlayOffset = GametimeDelta;
				}
			}
		}
	}
}