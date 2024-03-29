using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Entities.Advanced;
using UndyneFight_Ex.SongSystem;
using static Extends.DrawingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.CameraEffect;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources.Sounds;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
	public class Galileo : IChampionShip
	{
		public Galileo()
		{

			difficulties = new()
			{
				{ "div.2", Difficulty.Hard },
				{ "div.1", Difficulty.Extreme }
			};
		}

		private readonly Dictionary<string, Difficulty> difficulties = new();
		public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

		public IWaveSet GameContent => new Game();
		public class Game : WaveConstructor, IWaveSet
		{
			public Game() : base(62.5f / (172 / 60f)) { }
			public string Music => "Galileo";

			public string FightName => "Galileo";
			private class ThisInformation : SongInformation
			{
				public override Dictionary<Difficulty, float> CompleteDifficulty => new(
						new KeyValuePair<Difficulty, float>[] { }
					);
				public override Dictionary<Difficulty, float> ComplexDifficulty => new(
						new KeyValuePair<Difficulty, float>[] { }
					);
				public override Dictionary<Difficulty, float> APDifficulty => new(
						new KeyValuePair<Difficulty, float>[] { }
					);
				public override string BarrageAuthor => "TK";
				public override string PaintAuthor => "Masaharu Fukuyama";
				public override string SongAuthor => "Masaharu Fukuyama + Akira Inoue";
			}
			public SongInformation Attributes => new ThisInformation();
			public static Game game;
			public float TempVar;
			#region Not Yet
			public void Hard()
			{
				if (Gametime < 0) return;
			}

			public void Noob()
			{
				if (Gametime < 0) return;
			}
			public void Easy()
			{
				if (Gametime < 0) return;
			}
			public void Normal()
			{
				if (Gametime < 0) return;
			}
			public void ExtremePlus()
			{
				if (Gametime < 0) return;
			}
			#endregion
			public void Extreme()
			{
				#region Intro

				#endregion
				#region Bass Guitar

				#endregion
				#region Pursue

				#endregion
				#region Break
				
				#endregion
			}
			public void Start()
			{
				SetSoul(1);
				TP();
				SetGreenBox();
				PlayOffset = GametimeDelta;
			}
			#region Functions
			private static void ThrowBones()
			{
				PlaySound(pierce);
				float dir = Rand(0f, 359);
				int spd = 5;
				for (int i = 0; i < 10; ++i)
				{
					float ang = dir + i * 36;
					spd *= 2;
					CreateBone(new CustomBone(new(320, 50), Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
					{
						PositionRouteParam = new float[] { spd * Cos(ang), spd * Sin(ang) },
						IsMasked = false,
						RotationRouteParam = new float[] { 6f, Rand(0f, 359) },
						LengthRouteParam = new float[] { 30f },
						ColorType = 1
					});
					spd /= 2;
					CreateBone(new CustomBone(new(320, 50), Motions.PositionRoute.linear, ang + 90, 30)
					{
						PositionRouteParam = new float[] { spd * Cos(ang), spd * Sin(ang) },
						IsMasked = false,
					});
				}
			}
			#endregion
		}
	}
}