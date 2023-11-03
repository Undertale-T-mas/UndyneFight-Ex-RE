using Microsoft.Xna.Framework;

namespace RecallCharter
{
    internal class TextButton : Button
    {
        public string Text { get; set; } 
        public Color FontColor { get; set; } 

        public override void Draw()
        {
            if (!Father.IsEnabled) return;
            UndyneFight_Ex.Remake.Resources.Font.Normal.CentreDraw(Text, this.Centre, FontColor, Scale, Depth);
            base.Draw();
        }
    }
}
