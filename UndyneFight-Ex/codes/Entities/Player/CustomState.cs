using Microsoft.Xna.Framework;
using System;

namespace UndyneFight_Ex.Entities
{
    public partial class Player
    {
        public class MoveState
        {/*
                    switch (SoulType)
                    {
                        case 0:
                            if (isOranged)
                            {
                                Move.MoveAsOrange();
                            }
                            else
                            {
                                Move.MoveAsRed();
                            }

                            break;
                        case 2:
                            if (isOranged)
                            {
                                Move.MoveAsBlueOrange();
                            }
                            else
                            {
                                Move.MoveAsBlue();
                            }

                            break;
                        case 3:

                            break;
                        case 4:
                            Move.MoveAsPurple();
                            break;
                        case 5:
                            Move.MoveAsGray();
                            break;
                    }*/
            public Color StateColor { get; init; }
            public Action<Heart> MoveFunction { get; init; }
            public MoveState(Color color, Action<Heart> moveFunction)
            {
                StateColor = color;
                MoveFunction = moveFunction;
            }
        }
        public partial class Heart
        {
            public MoveState CurrentMoveState => _currentMoveState;
            private MoveState _currentMoveState;

            private static MoveState _red = new(Color.Red, (s) =>
            {
                Move.mission = s;
                if (s.isOranged)
                {
                    Move.MoveAsOrange();
                }
                else
                {
                    Move.MoveAsRed();
                }
            });
            private static MoveState _green = new(new Color(0, 255, 0), (s) =>
            {
                Move.mission = s;
            });
            private static MoveState _blue = new(Color.Blue, (s) =>
            {
                Move.mission = s;
                if (s.isOranged)
                {
                    Move.MoveAsBlueOrange();
                }
                else
                {
                    Move.MoveAsBlue();
                }
            });
            private static MoveState _purple = new(Color.MediumPurple, (s) =>
            {
                Move.mission = s;
                Move.MoveAsPurple();
            });
            private static MoveState _gray = new(Color.MediumPurple, (s) =>
            {
                Move.mission = s;
                Move.MoveAsGray();
            });
            public void ChangeState(MoveState state)
            {
                isOranged = false;
                _currentMoveState = state;
                lastChangeTime = 0;
                SoulType = -1;
                Player manager = FatherObject as Player;
                manager.GameAnalyzer.PushData(new SoulChangeData(-1, ID, Fight.Functions.GametimeF));
                CreateShinyEffect(_currentMoveState.StateColor);
            }
        }
    }
}