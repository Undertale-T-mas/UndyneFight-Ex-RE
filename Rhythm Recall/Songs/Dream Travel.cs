using Extends;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Entities.Advanced;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.AdvanceFunctions;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
	internal class DreamTravel : IChampionShip
	{
		Dictionary<string, Difficulty> dif = new();
		public DreamTravel()
		{
			dif.Add("Div.1", Difficulty.Extreme);
		}
		public IWaveSet GameContent => new Project();
		public Dictionary<string, Difficulty> DifficultyPanel => dif;
		class Project : WaveConstructor, IWaveSet
		{
			public Project() : base(62.5f / (200f / 60f)) { }
			public string Music => "Dream Travel";
			public string FightName => "Dream Travel";
			public SongInformation Attributes => new Information();
			class Information : SongInformation
			{
				public override string BarrageAuthor => "Dream.TK";
				public override string AttributeAuthor => "Dream.TK";
				public Information() { this.MusicOptimized = true; }
				public override Dictionary<Difficulty, float> CompleteDifficulty => new(
					new KeyValuePair<Difficulty, float>[] {
						new(Difficulty.Easy, 0f),
						new(Difficulty.Hard, 0f),
						new(Difficulty.Extreme, 0f),
					});
				public override Dictionary<Difficulty, float> ComplexDifficulty => new(
					new KeyValuePair<Difficulty, float>[] {
						new(Difficulty.Easy, 0f),
						new(Difficulty.Hard, 0f),
						new(Difficulty.Extreme, 0f),
				});
				public override Dictionary<Difficulty, float> APDifficulty => new(
					new KeyValuePair<Difficulty, float>[] {
						new(Difficulty.Easy, 0f),
						new(Difficulty.Hard, 0f),
						new(Difficulty.Extreme, 0f),
					});
			}
			public void Start()
			{
				RegisterFunction("Rot", () =>
				{
					RunEase((s) => ScreenDrawing.ScreenAngle = s, EaseOut(BeatTime(2), ScreenDrawing.ScreenAngle, ScreenDrawing.ScreenAngle + 90, EaseState.Expo));
					DelayBeat(2, () =>
					RunEase((s) => Heart.InstantSetRotation(s), EaseOut(BeatTime(2), Heart.Rotation, Heart.Rotation + 450, EaseState.Expo)));
				});
				Settings.GreenTap = true;
				HeartAttribute.MaxHP = HeartAttribute.HP = 41;
				HeartAttribute.DamageTaken = 4;
				SetSoul(0);
				InstantSetBox(new Vector2(-10, -10), 0, 0);
				RunEase((s) => ScreenDrawing.MasterAlpha = s, EaseOut(BeatTime(32), 0, 1, EaseState.Linear));
			}
			Circle CircleBox;
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
			private class FakeBox : Entity
			{
				private float dist = MathF.Sqrt(42 * 42 * 2) + 2.1f;
				private Color col = Color.Lerp(Color.White, Color.Black, 0.5f);
				private Texture2D texture = FightResources.Sprites.pixUnit;
				public FakeBox() { }
				public override void Update() { Centre = Heart.Centre; }
				public override void Draw()
				{
					float angle = ScreenDrawing.ScreenAngle;
					for (int a = 0; a < 4; a++)
						DrawingLab.DrawLine(Heart.Centre + MathUtil.GetVector2(dist, 45 + angle + a * 90), Heart.Centre + MathUtil.GetVector2(dist, 45 + angle + (a + 1) * 90), 4.2f, col, 0.99f);

					Vector2 v1 = GetVector2(42, angle), v2 = -v1;
					v1 += Centre; v2 += Centre;
					Vector2 del = GetVector2(42, angle + 90),
							p1 = v1 + del, p2 = v2 + del, p3 = v1 - del, p4 = v2 - del;
					ScreenDrawing.SpriteBatch.DrawVertex(texture, Heart.Depth,
						new VertexPositionColorTexture(new(p1, Heart.Depth), Color.Black, new(0, 0)),
						new VertexPositionColorTexture(new(p2, Heart.Depth), Color.Black, new(0, 1)),
						new VertexPositionColorTexture(new(p4, Heart.Depth), Color.Black, new(1, 1)),
						new VertexPositionColorTexture(new(p3, Heart.Depth), Color.Black, new(1, 0)));
				}
			}
			private class GravityLine : Entity
			{
				float Angle, Rotate;
				Vector2 Speed, Gravity;
				public GravityLine(Vector2 cen, float ang, Vector2 spd, Vector2 grav, float rot)
				{
					Centre = cen;
					Speed = spd;
					Gravity = grav;
					Angle = ang;
					Rotate = rot;
				}
				public override void Update()
				{
					Centre += (Speed += Gravity);
					Angle += Rotate;
					if (Centre.Y > 600) Dispose();
				}
				public override void Draw()
				{
					DrawingLab.DrawLine(Centre, Angle, 84, 5, Color.Lerp(Color.White, Color.Black, 0.5f), 1);
				}
			}
			private class CircleBeat : Entity, ICollideAble
			{
				Color color;
				float Distance, Speed, EndDist = MathF.Sqrt(42 * 42 * 2);
				private JudgementState JudgeState
				{
					get
					{
						return GameStates.CurrentScene is SongFightingScene
							? (GameStates.CurrentScene as SongFightingScene).JudgeState
							: JudgementState.Lenient;
					}
				}
				public CircleBeat(Color col, float delay, float speed)
				{
					color = col;
					Distance = delay * speed + EndDist;
					Speed = speed;
				}
				public override void Update()
				{
					Distance -= Speed;
					GetCollide(Heart);
				}
				public override void Draw()
				{
					DrawingLab.DrawCircle(Heart.Centre, Distance, 256, 5, color, 1);
				}
				public void GetCollide(Player.Heart heart)
				{
					CircleBeat[] c = GetAll<CircleBeat>();
					float[] dists = new float[c.Length];
					for (int i = 0; i < c.Length; i++) dists[i] = c[i].Distance;
					if (Distance != dists.Min() || Distance > 200) return;

					bool KeyPress = GameStates.IsKeyPressed120f(InputIdentity.Alternate), auto = false;
#if DEBUG
					auto = true;
#endif
					float AllowDist = (JudgeState) switch
					{
						JudgementState.Strict => 18.75f,
						JudgementState.Balanced => 28.125f,
						JudgementState.Lenient => 36.5f
					};
					if (Distance < 37 || (Distance > (AllowDist + EndDist) && KeyPress))
					{
						PushScore(0);
						LoseHP(heart);
						Dispose();
					}
					else if (KeyPress || (auto && Distance <= EndDist))
					{
						PlaySound(Sounds.Ding);
						PushScore(3);
						Dispose();
					}
				}

			}
			private void Effect()
			{
				if (GametimeF == 144 * 62.5f) EndSong();
				bool IsEx = GameStates.difficulty == 4;
				if (InBeat(0, 120) && At0thBeat(2))
				{
					CreateEntity(new Line(
						EaseOut(BeatTime(6), new Vector2(640, 360), new Vector2(0, 420), EaseState.Linear),
						EaseOut(BeatTime(6), new Vector2(840, 480), new Vector2(-200, 540), EaseState.Linear))
					{ ObliqueMirror = true, DrawingColor = IsEx ? Color.IndianRed : Color.DarkGray });
				}
				if (InBeat(2))
					for (int i = 0; i < 4; i++)
					{
						int Y = 360 + i * 30;
						CreateEntity(new Line(
							EaseOut(BeatTime(6), new Vector2(640 + i * 50, Y), new Vector2(-i * 50, Y + 60), EaseState.Linear),
							Stable(0, new Vector2(640, Y)))
						{ ObliqueMirror = true, DrawingColor = IsEx ? Color.IndianRed : Color.DarkGray });
					}
				if (InBeat(32))
				{
					SetSoul(1);
					InstantTP(320, 240);
					InstantSetGreenBox();
					InstanceCreate(new ScreenShaker(5, 5, 3));
				}
				if (InBeat(64, 88) && At0thBeat(8))
				{
					RunEase((s) => ScreenDrawing.ScreenScale = s,
						LinkEase(
							EaseOut(BeatTime(0.75f), 1, 1.25f, EaseState.Cubic),
							EaseOut(BeatTime(0.75f), 1.25f, 1, EaseState.Cubic),
							EaseOut(BeatTime(0.75f), 1, 1.25f, EaseState.Cubic),
							EaseOut(BeatTime(0.75f), 1.25f, 1, EaseState.Cubic)));
					DelayBeat(3, () => InstanceCreate(new ScreenShaker(20, 10, 2)));
				}
				if (InBeat(96, 120) && At0thBeat(1))
				{
					CreateEntity(new Line(
						EaseOut(BeatTime(1.5f), new Vector2(276, 198), new Vector2(0, 150), EaseState.Quad),
						EaseOut(BeatTime(1.5f), new Vector2(276, 282), new Vector2(0, 330), EaseState.Quad))
					{
						Alpha = 0.3f,
						TransverseMirror = true,
						Width = 2
					});
				}
				if (InBeat(120, 124) && At0thBeat(0.1f))
					ScreenDrawing.MasterAlpha = ScreenDrawing.MasterAlpha == 1 ? 0 : 1;
				if (InBeat(124))
				{
					DrawingUtil.ScreenAngle(10, BeatTime(2));
					RunEase((s) => ScreenDrawing.ScreenPositionDelta = s,
						EaseOut(BeatTime(2), Vector2.Zero, new(-30), EaseState.Quad));
					RunEase((s) => ScreenDrawing.MasterAlpha = s,
						EaseOut(BeatTime(2), 1, 0, EaseState.Linear));
				}
				if (InBeat(126)) foreach (Line l in GetAll<Line>()) l.Dispose();
				if (InBeat(128))
				{
					ScreenDrawing.ScreenPositionDelta = Vector2.Zero;
					ScreenDrawing.ScreenAngle = 0;
					ScreenDrawing.MasterAlpha = 1;
					RunEase((s) =>
					{
						InstantTP(320, s);
						InstantSetBox(s, 84, 84);
					}, EaseOut(BeatTime(1), 140, 240, EaseState.Elastic));
				}
				if (InBeat(128, 176) && At0thBeat(16))
				{
					for (int i = 0; i < 24; i++)
					{
						DelayBeat(i * 0.5f, () =>
						{
							float X = (GametimeF * 5.5f) % 200, dis = (GametimeF * 2) % 50;
							Vector2 Pos = new(X + 400, 200);
							Color col = new(DrawingLab.HsvToRgb(GametimeF % 255, 255, 255, 255));
							Line[] lines = {
								new(Pos, 90 - dis) { TransverseMirror = true, DrawingColor = col},
								new(Pos, 90 + dis) { TransverseMirror = true, DrawingColor = col }
							};
							CreateEntity(lines);
							DelayBeat(0.25f, () => { foreach (Line l in lines) l.Dispose(); });
						});
					}
				}
				if (InBeat(192, 220) && At0thBeat(1))
				{
					ScreenDrawing.CameraEffect.Convulse(20, BeatTime(0.5f), GametimeF <= BeatTime(208));
					ScreenDrawing.CameraEffect.SizeShrink(2, BeatTime(0.5f));
					int Y = GametimeF <= BeatTime(208) ? 0 : 480;
					Line l = new(EaseOut(BeatTime(1), new Vector2(320, Y), new Vector2(320, Y == 0 ? 100 : 380), EaseState.Quad), Stable(0, 0));
					l.AlphaDecrease(BeatTime(1));
					CreateEntity(l);
				}
				if (InBeat(204) || InBeat(216))
				{
					InstanceCreate(new ScreenShaker(50, 7, 2, 0, 180));
					EaseUnit<Vector2>[] Ease = new EaseUnit<Vector2>[64];
					for (int i = 0; i < 64; i++)
						Ease[i] = Stable(1, new Vector2(MathF.Sin(i * 2) * i * 5 + 320, 240));
					Line l = new(LinkEase(false, Ease), Stable(0, 90));
					CreateEntity(l);
					RunEase((s) => l.Width = s, EaseOut(64, 30, 2, EaseState.Linear));
					Delay(64, () => l.Dispose());

					Line[] lines = new Line[36];
					EaseUnit<float>[] EaseAng;
					for (int i = 0; i < 36; i++)
					{
						Ease = new EaseUnit<Vector2>[64];
						EaseAng = new EaseUnit<float>[64];
						for (int ii = 0; ii < 64; ii++)
						{
							float Base = i * 10 + ii * 360f / 64f;
							Ease[ii] = Stable(1, GetVector2(240 - ii * (240 - 42 * MathF.Sqrt(2)) / 64f, Base) + new Vector2(320, 240));
							EaseAng[ii] = Stable(1, Base + 90);
						}
						lines[i] = new(LinkEase(false, Ease), LinkEase(false, EaseAng));
						lines[i].Alpha = 0;
						lines[i].AlphaIncreaseAndDecrease(64, 0.75f);
					}
					CreateEntity(lines);
				}
				if (InBeat(210))
				{
					ScreenDrawing.UISettings.CreateUISurface();
					foreach (Line l in GetAll<Line>()) l.Dispose();
				}
				if ((InBeat(224, 225) && At0thBeat(0.5f)))
				{
					Line[] lines =
					{
						new(new Vector2(320, 230), 0) { DrawingColor = Color.MediumPurple },
						new(new Vector2(320, 250), 0) { DrawingColor = Color.MediumPurple }
					};
					RunEase((s) =>
					{
						lines[0].Width = s;
						lines[1].Width = s;
					}, LinkEase(false, EaseOut(BeatTime(0.25f), 0, 5, EaseState.Linear),
					EaseOut(BeatTime(0.25f), 5, 0, EaseState.Linear)));
					Delay(BeatTime(0.5f), () =>
					{
						lines[0].Dispose();
						lines[1].Dispose();
					});
					CreateEntity(lines);
				}
				if (InBeat(226))
				{
					GameStates.ForceDisableTimeTips = true;
					Heart.Split();
					Heart.Depth -= 1;
					SetPlayerBoxMission(1);
					InstantSetBox(-1000, 8, 8);
					InstantTP(new(-1000, -1000));
					SetPlayerBoxMission(0);
					CreateEntity(new FakeBox());
					FightBox.boxs[0].Visible = false;
					ScreenDrawing.BoxBackColor = Color.Transparent;
				}
				if (InBeat(226, 251) && At0thBeat(1))
				{
					RunEase((s) =>
					{
						ScreenDrawing.ScreenAngle = s;
						Heart.InstantSetRotation(s);
						Heart.Shields.Circle.Rotation = s * MathF.PI / 180f ;
					}, EaseOut(BeatTime(1), ScreenDrawing.ScreenAngle, ScreenDrawing.ScreenAngle + (1080f / 26f), EaseState.Quint));
					RunEase((s) =>
					{
						foreach (Line l in GetAll<Line>()) l.DrawingColor = Color.Lerp(Color.Yellow, Color.Black, s);
					}, EaseOut(BeatTime(1), 1, 0.5f, EaseState.Linear));
					EaseUnit<Vector2>[] Ease = new EaseUnit<Vector2>[12];
					for (int i = 0; i < 12; i++)
						Ease[i] = EaseOut(BeatTime(1), new Vector2(720 - i * 80, 240), new Vector2(640 - i * 80, 240), EaseState.Circ);
					Line l = new(LinkEase(false, Ease), Stable(0, 90));
					l.DrawingColor = Color.Transparent;

					Ease = new EaseUnit<Vector2>[12];
					for (int i = 0; i < 12; i++)
						Ease[i] = EaseOut(BeatTime(1), new Vector2(320, 720 - i * 80), new Vector2(320, 640 - i * 80), EaseState.Circ);
					Line l2 = new(LinkEase(false, Ease), Stable(0, 0));
					l2.DrawingColor = Color.Yellow;
					DelayBeat(12, () => { l.Dispose(); l2.Dispose(); });
					CreateEntity(new Line[] { l, l2 });
				}
				if (InBeat(253))
				{
					foreach (Line l in GetAll<Line>())
					{
						EaseUnit<Vector2> Ease = LinkEase(false,
							EaseIn(BeatTime(Rand(2f, 4f)), l.Centre, l.Centre + new Vector2(Rand(-100f, 100f), Rand(800f, 1200f)), EaseState.Back));
						EaseUnit<float> Rot = LinkEase(false,
							EaseOut(BeatTime(Rand(2f, 4f)), l.Rotation, Rand(-400f, 400f), EaseState.Linear));
						Line l2; 
						CreateEntity(l2 = new(Ease, Rot) { DrawingColor = l.DrawingColor });
						l2.AlphaDecrease(BeatTime(Rand(4f, 6f)));
						l.Dispose();
					}
					ScreenDrawing.UISettings.RemoveUISurface();
					RunEase((s) => ScreenDrawing.MasterAlpha = s, LinkEase(false,
						EaseOut(BeatTime(1.5f), 1, 0, EaseState.Linear),
						EaseOut(BeatTime(2), 0, 0.5f, EaseState.Linear)));
				}
				if (InBeat(254.5f))
				{
					GameStates.ForceDisableTimeTips = false;
					SetPlayerMission(1);
					Heart.Dispose();
					SetPlayerMission(0);
					Heart.Depth++;
					Heart.InstantSetRotation(0);
					Heart.Shields.Circle.Rotation = 0;
					ScreenDrawing.BoxBackColor = Color.Black;
					ScreenDrawing.ScreenAngle = 0;
					ScreenDrawing.ScreenScale = 1.2f;
					FightBox.boxs[0].Visible = true;
					foreach (FakeBox f in GetAll<FakeBox>()) f.Dispose();
					foreach (Line l in GetAll<Line>()) l.Dispose();
				}
				if (InBeat(257, 319) && At0thBeat(1))
				{
					Circle c = new(new(Rand(0, 640), Rand(0, 480)), 0, 1, new Color((uint)Rand(0, uint.MaxValue)));
					float thick = Rand(20f, 40f);
					RunEase((s) => c.Thickness = s, LinkEase(false,
					EaseOut(BeatTime(2), 1, thick, EaseState.Quad),
					EaseOut(BeatTime(1.5f), thick, 0, EaseState.Quad)));
					RunEase((s) => c.Radius = s, LinkEase(false,
						Stable(BeatTime(1), 1),
						EaseIn(BeatTime(3), 1, thick * 2f, EaseState.Quad)));
					CreateEntity(c);
					DelayBeat(3.5f, () => c.Dispose());
				}
				if (InBeat(320))
				{
					for (int i = 0; i < 4; i++)
						CreateEntity(new GravityLine(GetVector2(42, i * 90) + new Vector2(320, 240), i * 90 + 90, GetVector2(2, Rand(180, 359)), GetVector2(0.2f, Rand(70f, 110f)), Rand(0.1f, 0.3f) * RandSignal()));
					FightBox.boxs[0].Visible = !IsEx;
					RunEase((s) => ScreenDrawing.ScreenScale = s,
						EaseOut(BeatTime(3), 1.2f, 0.8f, EaseState.Quad));
					RunEase((s) => ScreenDrawing.MasterAlpha = s,
						EaseOut(BeatTime(3), 0.5f, 0, EaseState.Quad));
				}
				if (InBeat(328, 330) && At0thBeat(1))
				{
					ScreenDrawing.MasterAlpha += 1f / 3f;
					ScreenDrawing.ScreenScale += 0.35f;
				}
				if (InBeat(331)) ScreenDrawing.WhiteOut(BeatTime(5));
				if (InBeat(335))
				{
					ScreenDrawing.ScreenScale = 1;
					foreach (Circle c in GetAll<Circle>()) c.Dispose();
				}
				if (InBeat(336, 462))
				{
					float X = RandBool() ? Rand(0, 240f) : Rand(400, 640f), Length = Rand(10, 50f), Speed = Rand(10, 16f);
					Line l;
					CreateEntity(l = new(
						EaseOut((480 + Length) / Speed, new Vector2(X, -Length - 100), new Vector2(X, 580), EaseState.Linear),
						EaseOut((480 + Length) / Speed, new Vector2(X, -100), new Vector2(X, 580 + Length), EaseState.Linear))
					{
						DrawingColor = Color.Lerp(Color.White, Color.Transparent, Rand(0.5f, 0.8f)),
						Width = Rand(1, 2f)
					});
					Delay((580 + Length) / Speed, () => l.Dispose());
					float time = GametimeF - BeatTime(336);
					ScreenDrawing.UISettings.HPShowerPos = new(320 + MathF.Sin(time / 30) * 5, 443 + MathF.Sin(time / 20) * 5);
					ScreenDrawing.UISettings.NameShowerPos = new(20 + MathF.Sin(time / 35) * 5, 457 + MathF.Sin(time / 25) * 5);
					ScreenDrawing.ScreenPositionDelta = new(MathF.Sin(time / 15) * 5, MathF.Sin(time / 25) * 5);
					if (At0thBeat(1))
					{
						ScreenDrawing.CameraEffect.SizeExpand(1, BeatTime(0.5f));
						if (IsEx && GametimeF >= 120 * 62.5f)
						{
							Circle[] c = GetAll<Circle>();
							RunEase((s) => c[0].color = Color.Lerp(Color.White, Color.Black, s),
								EaseOut(BeatTime(0.5f), 0, 0.5f, EaseState.Linear));
							Vector2 Rnd = new(Rand(300, 340), Rand(220, 260));
							RunEase((s) =>
							{
								InstantTP(s);
								CircleBox.Centre = s;
							}, EaseOut(BeatTime(1), Heart.Centre, Rnd, EaseState.Quad));
						}
					}
					if (IsEx && GametimeF < BeatTime(396) && At0thBeat(4))
						CreateEntity(new CircleBeat(Color.Red, BeatTime(4), 5));
				}
			}
			public void Easy()
			{
				Effect();
			}
			public void Hard()
			{
				Effect();
			}
			public void Extreme()
			{
				Effect();
				if (InBeat(32))
				{
					CreateChart(0, BeatTime(2), 7, new string[]
					{
						"$0", "", "$0", "", "$0", "", "$0", "",
						"R", "", "R", "", "R", "", "R", "",
						"R", "", "R", "+01", "", "", "R", "",
						"", "", "R", "+01", "R", "", "R", "",
						"R1", "", "R1", "", "R1", "", "R1", "",
						"R1", "", "R1", "", "R1", "", "R1", "",
						"R1", "", "R1", "+0", "", "", "R1", "",
						"", "", "R1", "+0", "R1", "", "R1", "",
						"$0", "", "$0", "", "$0", "", "$0", "",
						"R", "", "R", "", "R", "", "R", "",
						"R", "", "R", "+01", "", "", "R", "",
						"", "", "R", "+01", "R", "", "R", "",
						"R1", "", "R1", "", "R1", "", "R1", "",
						"R1", "", "R1", "", "R1", "", "R1", "",
						"R1", "", "R1", "", "R1", "", "R1", "",
						"D", "D1", "D", "D1", "D", "D1", "D", "D1",
					});
				}
				if (InBeat(60, 84) && AtKthBeat(8, BeatTime(4)))
				{
					Arrow[] arrs =
					{
						MakeArrow(BeatTime(4), "R", 40, 0, 0),
						MakeArrow(BeatTime(4), "R", 40, 1, 0),
						MakeArrow(BeatTime(6), "R", 40, 0, 0),
						MakeArrow(BeatTime(6), "R", 40, 1, 0)
					};
					Delay(BeatTime(4) - 1f, () =>
					{
						arrs[0].Stop(BeatTime(3.25f));
						arrs[1].Stop(BeatTime(3.25f));
						PlaySound(Sounds.spearAppear);
					});
					Delay(BeatTime(6) - 1f, () =>
					{
						arrs[2].Stop(BeatTime(1.25f));
						arrs[3].Stop(BeatTime(1.25f));
						PlaySound(Sounds.spearAppear);
					});
					CreateEntity(arrs);
					CreateChart(BeatTime(9.5f), BeatTime(1), 7, new string[]
					{
						"", "", "R", "",
						"R1", "", "R", "", "R1", "", "", "",
						"(D)(D1)", "", "", "", "(D)(D1)", "", "", "",
					});
				}
				if (InBeat(92))
				{
					for (int i = 0; i < 7; i++)
						CreateChart(BeatTime(4 + i * 2), BeatTime(1), 8, new string[]
						{
							"(D)(+0)", "", "+1", "", "+1", "", "", "",
							"(D1)(+01)", "", "-11", "", "-11", "", "", "",
						});
					for (int i = 7; i < 14; i++)
						CreateChart(BeatTime(4 + i * 2), BeatTime(1), 8, new string[]
						{
							"(D)(+0)", "", "-1", "", "-1", "", "", "",
							"(D1)(+01)", "", "+11", "", "+11", "", "", "",
						});
				}
				if (InBeat(128))
				{
					string B2 = "($0)($2)", R2 = "($01)($21)";
					for (int i = 0; i < 4; i++)
					{
						string[] def = new string[]
						{
							B2 + B2, "", B2, "", B2, "", B2, "",
							B2 + "*^R12'2", "", B2, "", B2, "", B2, "",
							R2 + R2, "", R2, "", R2, "", R2, "",
							R2 + "*^R02'2", "", R2, "", R2, "", R2, "",
							B2 + B2, "", B2, "", B2, "", B2, "",
							B2 + "*^R12'2", "", B2, "", B2, "", B2, "",
						},
						variant1 = new string[]
						{
							"R1", "+21", "+21", "", "", "", "", "", "(^D'3)(^D1'3)"
						},
						variant2 = new string[]
						{
							"(R)(R1)", "", "(R)(R1)", "", "(R)(R1)", "", "(R)(R1)", "",
							"(R)(R1)", "", "(R)(R1)", "", "(R)(R1)",
						};
						CreateChart(BeatTime(i * 16), BeatTime(2), 5, def.Concat(i != 1 ? variant1 : variant2).ToArray());
						for (int ii = 0; ii < 6; ii++)
							DelayBeat(i * 16 + ii * 2, () =>
								ScreenDrawing.CameraEffect.SizeExpand((ii & 1) == 1 ? 1 : 4, BeatTime(1)));
						if (i != 1)
							DelayBeat(i * 16 + 14, () =>
							ScreenDrawing.CameraEffect.SizeExpand(6, BeatTime(1)));
						{
							RunEase((s) =>
							{
								InstantTP(320, s);
								InstantSetBox(s, 84, 84);
							}, LinkEase(false,
								Stable(BeatTime(28), 240),
								EaseOut(BeatTime(0.5f), 240, 250, EaseState.Quad),
								EaseOut(BeatTime(0.5f), 250, 230, EaseState.Quad),
								EaseOut(BeatTime(0.5f), 230, 250, EaseState.Quad),
								EaseOut(BeatTime(0.5f), 250, 230, EaseState.Quad),
								EaseOut(BeatTime(0.5f), 230, 250, EaseState.Quad),
								EaseOut(BeatTime(0.5f), 250, 230, EaseState.Quad),
								EaseOut(BeatTime(0.5f), 230, 240, EaseState.Quad)
							));
						}
					}
				}
				if (InBeat(188))
				{
					string T = "(R)(R1)";
					for (int i = 0; i <= 1; i++)
						CreateChart(BeatTime(4 + i * 8), BeatTime(1), 7, new string[]
						{
							T, ">>0.375", "R", ">>0.375", T, ">>0.375", "R1", ">>0.375",
							T, ">>0.375", "R", ">>0.375", T, ">>0.375", "R1", ">>0.375",
							T, ">>0.375", "R", ">>0.375", T, ">>0.375", "R1", ">>0.375",
							T, ">>0.375", "R", ">>0.375", T, ">>0.375", "R1", ">>0.375",
							"R", "+01", "+2", "+01", "+2", "+01", "", "",
						});
				}
				if (InBeat(204))
				{
					for (int i = 0; i < 7; i++)
						CreateChart(BeatTime(4.5f + i * 2), BeatTime(1), 7, new string[]
						{
							"R", "", "+2", "", "+2", "", "", "",
							"R1", "", "+21", "", "+21", "", "", "",
						});
				}
				if (InBeat(220))
				{
					string B2 = "($0)($2)($01)($21)";
					CreateChart(BeatTime(4), BeatTime(4), 7, new string[] { B2, B2, B2 });
					for (int i = 0; i < 4; i++)
						CreateChart(BeatTime(6 + i * 4), BeatTime(4), 7, new string[]
						{ B2, "", B2, "", B2, "", B2, "", });
				}
				if (InBeat(227.5f) || InBeat(232.5f) || InBeat(236.5f))
				{
					CreateArrow(BeatTime(4), 0, 7, 0, 0);
					CreateArrow(BeatTime(4), 0, 7, 1, 0);
					CreateArrow(BeatTime(4), 2, 7, 0, 0);
					CreateArrow(BeatTime(4), 2, 7, 1, 0);
				}
				for (int i = 0; i <= 1; i++)
					if (InBeat(232.25f + i * 0.5f))
					{
						CreateArrow(BeatTime(4), 0, 7, i, 0);
						CreateArrow(BeatTime(4), 2, 7, i, 0);
					}
				if (InBeat(238))
				{
					string B2 = "($0)($2)($01)($21)", BR = "($0)($21)", BL = "($2)($01)";
					CreateChart(BeatTime(4), BeatTime(1), 7, new string[]
					{
						B2, ">>0.875", B2, ">>0.875", "(D)(D1)(D)(D1)", ">>0.875", B2, ">>0.875",
						B2, ">>0.875", "(D)(D1)(D)(D1)", ">>0.375", B2, ">>0.375", B2, ">>0.375",
						B2, ">>0.375", B2, ">>0.375", B2, ">>0.375", B2, ">>0.375",
						BR, ">>0.125", BL, ">>0.125", BR, ">>0.375", BL, ">>0.125",
						BR, ">>0.125", BL
					});
				}
				if (InBeat(254.5f))
				{
					CreateChart(BeatTime(1.5f), BeatTime(2), 6, new string[]
					{
						"$0", "", "+1", "", "+1", "", "+1", "",
						"D1", "+21", "+21", "", "R1", "", "R", "",
						"+2", "", "+1", "", "+1", "", "+1", "",
						"-1", "-1", "-1", "", "+0", "", "+0", "",
						"+2", "", "D", "+2", "+2", "", "D1", "+21",
						"+21", "", "D", "+1", "+1", "", "D1", "+11",
						"+11", "", "", "", "", "", "", "",
					});
				}
				if (InBeat(266))
				{
					CreateChart(BeatTime(4), BeatTime(1), 6, new string[]
					{
						"R", "+01", "+1", "+01", "+1", "+01", "+1", "+01",
						"-2", "+01", "+1", "+01", "+1", "+01", "-2", "+01",
						"+1", "+01", "", "", "R", "", "-1", "",
						"+2", "", "", "", "R1", "", "-11", "",
						"+21", "", "", "", "R", "", "", "",
						"R", "", "+2", "", "+2", "", "", "",
						"+0", "", "", "", "R", "", "-1", "",
						"-1", "", "", "", "R1", "", "+11", "",
						"+11", "", "+21", "", "+21", "", "+21", "",
						"+21", "", "+21", "", "+21", "", "+21", "",
						"+21", "", "+21", "", "", "", "", "",
					});
				}
				if (InBeat(276.5f))
				{
					CreateChart(BeatTime(4), BeatTime(2), 6, new string[]
					{
						"*R", "*>+0", "*<+0", "", "*R1", "*<+01", "*>+01", "",
						"*R", "*>+0", "*<+0", "", "(R)(R1)", "", "(R)(R1)", "",
						"", "", "", "", "", "", "R", "-1",
						"+21", "-11", "+2", "-1", "R", "+2", "+2", "",
						"D", "+1", "", "", "D", "-1", "", "",
						"D", "+1", "", "", "D", "", "+2", "",
						"D", "+1", "+1", "", "", "D", "-1", "-1",
						"", "+0", "", "+0", "", "+1", "", "+1",
						"", "+1", "-2", "+2", "D", "D", "D", "D",
						"+1", "+1", "+1", "D1", "-11", "-11", "-11", "D",
						"D", "D", "D", "", "", "", "", "",
					});
				}
				if (InBeat(297.5f))
				{
					CreateChart(BeatTime(4), BeatTime(1), 6, new string[]
					{
						"D", "", "+2", "", "+2", "", "D", "+01",
						"+2", "+01", "+2", "+01", "+2", "+01", "+2", "+01",
						"+2", "+01", "+2", "+01", "+2", "+01", "", "",
						"+2", "+01", "", "", "", "", "", "",
					});
					for (int i = 0; i < 17; i++)
					{
						CreateArrow(BeatTime(7.5f + i * 0.5f), 1, 6, 0, 0, ArrowAttribute.Hold | ArrowAttribute.Void);
						if (i == 5 || i == 12)
							CreateArrow(BeatTime(7.75f + i * 0.5f), 3, 6, 0, 0, ArrowAttribute.Tap | ArrowAttribute.ForceGreen);
						if (i < 15)
						{
							int tmp = (i % 4) switch { 0 => 4, 1 => 0, 2 => 1, 3 => 2 };
							CreateArrow(BeatTime(7.5f + i), "+1", 6, 1, 0, (ArrowAttribute)tmp);
						}
					}
					CreateChart(BeatTime(17.5f), BeatTime(2), 6, new string[]
					{
						"R", "R", "R", "", "R", "", "R", "+2",
						"+2", "", "+0", "", "+0", "", "*D", "*+0",
						"", "", "", "", "($0)($1)($2)($01)($11)($21)", "", "", "",
					});
				}
				if (InBeat(328))
				{
					CircleBox = new(new(320, 240), MathF.Sqrt(42 * 42 * 2), 5) { StartAng = -90 };
					RunEase((s) => CircleBox.EndAng = s, LinkEase(false,
						EaseOut(BeatTime(1), -90, 30, EaseState.Quad),
						EaseOut(BeatTime(1), 30, 170, EaseState.Quad),
						EaseOut(BeatTime(1), 170, 270, EaseState.Quad)));
					CreateEntity(CircleBox);
				}
				if (InBeat(335))
				{
					ScreenDrawing.BoxBackColor = Color.Transparent;
					CreateEntity(CircleBox = new Circle(new(320, 240), MathF.Sqrt(42 * 42 * 2), 5));
					CreateEntity(new TextPrinter(BeatTime(4), "$$$$Spacebar!",
						new(220, 120),
						new TextAttribute[]
						{
						new TextColorAttribute(Color.White),
						new TextSizeAttribute(1.3f),
						new TextSpeedAttribute(100),
						new TextFadeoutAttribute(BeatTime(2), BeatTime(2))
						}));
					CreateChart(BeatTime(1), BeatTime(2), 7, new string[]
					{
						"$0", "", "$0", "", "$0", "", "$0", "",
						"D", "D", "D", "", "(R)(R1)", "", "(R)(R1)", "",
						"R", "", "R", "", "R", "", "R", "",
						"R", "R", "R", "", "(R)(R1)", "", "(R)(R1)", "",
						"R", "", "R", "+1", "-1", "", "D", "+1",
						"-1", "", "(R)(R1)", "", "(R)(R1)", "", "(R)(R1)", "",
						"(R)(+0)(D1)(+01)(Rot)"
					});
				}
				if (InBeat(348))
				{
					CreateChart(BeatTime(4), BeatTime(2), 7, new string[]
					{
						"R1", "", "R1", "+11", "-11", "", "D1", "+11",
						"-11", "", "(R)(R1)", "", "(R)(R1)", "+1", "-1", "",
						"R", "", "R", "+1", "-1", "", "+1", "+1",
						"+1", "+1", "-1", "-1", "-1", "-1", "(R)(R1)", "",
						"R", "", "R1", "+21", "-21", "", "D1", "+21",
						"-21", "", "D", "+2", "-2", "", "R", "",
						"(R)(+0)(D1)(+01)(Rot)"
					});
				}
				if (InBeat(364))
				{
					RegisterFunctionOnce("QScrRot", () =>
					{
						RunEase((s) => ScreenDrawing.ScreenAngle = s, EaseOut(BeatTime(1), ScreenDrawing.ScreenAngle, ScreenDrawing.ScreenAngle + 90, EaseState.Elastic));
					});
					RegisterFunctionOnce("QSoulRot", () =>
					{
						RunEase((s) => Heart.InstantSetRotation(s), EaseOut(BeatTime(1), Heart.Rotation, Heart.Rotation + 90, EaseState.Elastic));
					});
					CreateChart(BeatTime(4), BeatTime(2), 7, new string[]
					{
						"R", "", "*D", "", "*+0", "", "*D", "",
						"*+0", "", "*D1", "", "*+01", "", "*+01", "",
						"R", "", "*D", "", "*+0", "", "*D", "",
						"*+0", "", "*D1", "", "*+01", "", "*+01", "",
						"R", "", "*D", "", "*+0", "", "*D", "",
						"*+0", "", "*D1", "", "*+01", "", "*+01", "",
						"R", "", "*D", "", "*+0", "", "*D", "",
						"*+0", "", "*D1", "", "*+01", "", "*+01", "",
						"R", "", "*D", "", "*+0", "", "*D", "",
						"*+0", "", "*D1", "", "*+01", "", "*+01", "",
						"R", "", "*D", "", "*+0", "", "", "",
						"QScrRot", "", "", "QSoulRot", "", "", "QScrRot", "",
						"QSoulRot", "", "", "", "", "", "", "",
					});
				}
				if (InBeat(389))
				{
					CreateChart(BeatTime(4), BeatTime(2), 7, new string[]
					{
						"R", "", "R", "", "R", "", "R", "",
						"(D)(+01)", "(D)(+01)", "(D)(+01)", "", "(D)(+01)", "", "", "R",
						"+2", "", "", "R1", "+21", "", "", "R",
						"+2", "", "", "", "", "", "", "",
					});
				}
				if (InBeat(396))
				{
					CreateChart(BeatTime(4), BeatTime(4), 7, new string[]
					{
						"R", "R", "R", "R", "R", "R", "R", "(R)(R1)",
						"", "(R)(R1)", "", "(R)(R1)", "", "R", "R", "R",
						"R1", "R1", "R1", "R1", "R1", "R1", "", "(R)(R1)",
						"", "(R)(R1)", "", "(R)(R1)", "", "R1", "R1", "R1",

						"R", "R", "R", "R", "R", "R", "", "(R)(R1)",
						"", "(R)(R1)", "", "(R)(R1)", "", "R", "R", "R",
						"R1", "R1", "R1", "R1", "R1", "R1", "", "(R)(R1)",
						"(R)(R1)", "(R)(R1)", "", "(R)(R1)", "R1", "R1", "R1", "R1",
					});
				}
				if (InBeat(428))
				{
					CreateChart(BeatTime(4), BeatTime(2), 7, new string[]
					{
						"(D)(+2)(D12)", "", "(D)(+2)(D12)", "", "(D)(+2)(D12)", "", "(D)(+2)(D12)", "",
						"(D)(+2)(D12)", "", "(D)(+2)(D12)", "", "", "", "(R)(R1)", "",
						"", "", "(R)(R1)", "", "", "", "(R)(R1)", "",
						"", "", "(R)(+21)", "", "(-1)(+21)", "", "(-1)(+21)", "",

						"(D1)(+21)(D02)", "", "(D1)(+21)(D02)", "", "(D1)(+21)(D02)", "", "(D1)(+21)(D02)", "",
						"(D1)(+21)(D02)", "", "(D1)(+21)(D02)", "", "", "", "(R)(R1)", "",
						"", "", "(R)(R1)", "", "", "", "(R)(R1)", "",
						"D", "D", "D", "D", "D", "D", "D", "D",
					});
				}
				if (InBeat(432))
				{
					for (int i = 0; i < 240; i++)
					{
						EaseUnit<Vector2>[] Ease = new EaseUnit<Vector2>[8];
						Ease[0] = EaseOut(BeatTime(1), new Vector2(320, 840 - i), new Vector2(0, 600 - i), EaseState.Quad);
						for (int ii = 1; ii < 3; ii++)
						{
							Ease[ii * 2] = EaseInOut(BeatTime(4), new Vector2(600 - i), new Vector2(540 - i), EaseState.Sine);
							Ease[ii * 2 + 1] = EaseInOut(BeatTime(4), new Vector2(540 - i), new Vector2(600 - i), EaseState.Sine);
						}
						Ease[7] = EaseInOut(BeatTime(4), new Vector2(600 - i), new Vector2(840 - i), EaseState.Sine);
						CreateEntity(new Line(LinkEase(false, Ease), Stable(0, 0))
						{
							DrawingColor = Color.Lerp(Color.Aqua, Color.Transparent, i / 240f),
							Width = 2
						});

						Ease = new EaseUnit<Vector2>[99];
						Ease[0] = EaseOut(BeatTime(1), new Vector2(320, i - 360), new Vector2(0, i - 120), EaseState.Quad);
						for (int ii = 1; ii < 49; ii++)
						{
							Ease[ii * 2] = EaseInOut(BeatTime(4), new Vector2(i - 120), new Vector2(i - 60), EaseState.Sine);
							Ease[ii * 2 + 1] = EaseInOut(BeatTime(4), new Vector2(i - 60), new Vector2(i - 120), EaseState.Sine);
						}
						CreateEntity(new Line(LinkEase(false, Ease), Stable(0, 0))
						{
							DrawingColor = Color.Lerp(Color.Pink, Color.Transparent, i / 240f),
							Width = 2
						});
					}
				}
				if (InBeat(446, 448) && At0thBeat(0.25f))
				{
					ScreenDrawing.ScreenAngle = Rand(-15f, 15f);
					ScreenDrawing.ScreenScale += 0.05f;
				}
				if (InBeat(448))
				{
					ScreenDrawing.ScreenAngle = 0;
					DrawingUtil.SetScreenScale(1, BeatTime(0.5f));
				}
				if (InBeat(444))
				{
					RegisterFunctionOnce("Circ", () =>
					CreateEntity(new CircleBeat(Color.Red, BeatTime(4), 7)));
					CreateChart(BeatTime(4), BeatTime(2), 8, new string[]
					{
						"(R02)(R12)", "", "(R02)(R12)", "", "(R02)(R12)", "", "(R02)(R12)", "",
						"(R02)(R12)", "", "(R02)(R12)", "", "", "", "(R)(R1)", "",
						"", "", "(R)(R1)", "", "", "", "(R)(R1)", "",
						"", "", "(R)(+21)", "", "(-1)(+21)", "", "(-1)(+21)", "",

						"(R)(+011)(Circ)", "", "(R)(+01)(Circ)", "", "(R)(+012)", "", "(R)(+01)", "(Circ)",
						"(R)(+011)", "", "(R)(+01)", "", "(R)(+012)(Circ)", "", "(R)(+01)", "",
					});
				}
				if (InBeat(464))
				{
					RunEase((s) => CircleBox.Centre = s,
						EaseIn(BeatTime(4), CircleBox.Centre, new Vector2(Rand(200, 440f), 600), EaseState.Sine));
					RunEase((s) =>
					{
						Heart.InstantTP(s);
						InstantSetBox(s, 84, 84);
					}, EaseIn(BeatTime(4), Heart.Centre, new Vector2(Rand(200, 440f), Rand(500, 600f)), EaseState.Quad));
					RunEase((s) => Heart.InstantSetRotation(s), EaseIn(BeatTime(4), 0, Rand(30, 70f) * RandSignal(), EaseState.Quad));
					RunEase((s) => ScreenDrawing.UISettings.NameShowerPos += s, EaseIn(BeatTime(4), Vector2.Zero, new Vector2(Rand(-3f, 3), Rand(1f, 3)), EaseState.Quad));
					RunEase((s) => ScreenDrawing.UISettings.HPShowerPos += s, EaseIn(BeatTime(4), Vector2.Zero, new Vector2(Rand(-3f, 3), Rand(1f, 3)), EaseState.Quad));
					RunEase((s) => ScreenDrawing.MasterAlpha = s, EaseOut(BeatTime(4), 1, 0, EaseState.Linear));
				}
			}
			#region No
			public void ExtremePlus() { }
			public void Noob() { }
			public void Normal() { }
			#endregion
		}
	}
}