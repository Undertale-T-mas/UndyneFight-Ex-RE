using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using UndyneFight_Ex.Entities;

namespace UndyneFight_Ex.Remake
{
    public partial class Souls
    {
        private static bool AbleDash = true;
        static float Timer = 0;
        private static bool FirstPress = false;
        private static bool Matain = true;
        private static bool Matain2 = true;
        private static bool PressTimer = false;
        static float Timer2 = 30;
        static float Speed=15;
        
        public static Player.MoveState CyanSoul { get; private set; } = new(Color.Cyan, (s) => {
            SoulMove(s);
            if (GameStates.IsKeyPressed120f(InputIdentity.Alternate)&&Matain2==true) { FirstPress = true;Matain2 = false; }
            if (FirstPress == true) 
            { 
                Timer2--;
                if (Timer2<=0&&Timer2 < 30)
                {
                    if (GameStates.IsKeyPressed120f(InputIdentity.Alternate) && Matain == true) { Matain = false; AbleDash = true;}
                }
                if(Timer2<0)
                {
                    FirstPress = false;
                    Matain2 = true;
                    Timer2 = 30;
                }
            }
            if (AbleDash)
            {
                Timer++;
                for (int i = 0; i < 4; i++)
                {
                    if (GameStates.IsKeyDown(s.movingKey[i]))
                    {
                        s.InstantTP(s.Centre+MathUtil.GetVector2(Speed, i * 90));
                        Speed = Speed * 0.9f + 0.1f * 0;
                    }
                }
                if(Timer>=60)
                {
                    Matain = true;
                    AbleDash = false;
                    Speed = 15;
                }
            }
        });
    }
}