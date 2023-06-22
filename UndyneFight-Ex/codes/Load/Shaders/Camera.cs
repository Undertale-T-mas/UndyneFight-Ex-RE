using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class CameraShader : Shader
            {
                public CameraShader(Effect eff) : base(eff)
                {
                    StableEvents = (x) =>
                    {
                        x.Parameters["itextureSize"].SetValue(TextureSize);
                        x.Parameters["iProjectAxisX"].SetValue(ProjectAxisX);
                        x.Parameters["iProjectAxisY"].SetValue(ProjectAxisY);
                        x.Parameters["iProjectAxisZ"].SetValue(ProjectAxisZ);
                        x.Parameters["iProjectPoint"].SetValue(ProjectPoint);
                        x.Parameters["iProjectPointOffect"].SetValue(ProjectPointOffect);

                        x.Parameters["iVisuospatial"].SetValue(Visuospatial);
                        x.Parameters["iPosition"].SetValue(CameraPosition);
                        x.Parameters["iRotation"].SetValue(CameraRotation);
                        x.Parameters["iAhead"].SetValue(CameraAhead);
                        x.Parameters["iRight"].SetValue(CameraRight);
                        x.Parameters["iDown"].SetValue(CameraDown);
                    };
                }

                public Vector2 TextureSize { get; set; } = new Vector2(640, 480);
                public Vector3 ProjectAxisX { get; set; } = new Vector3(1, 0, 0);
                public Vector3 ProjectAxisY { get; set; } = new Vector3(0, 1, 0);
                public Vector3 ProjectAxisZ { get; set; } = new Vector3(0, 0, 1);
                public Vector3 ProjectPoint { get; set; } = new Vector3(0, 0, 300);
                public Vector2 ProjectPointOffect { get; set; } = new Vector2(320, 240);

                public Vector3 Visuospatial { get; set; } = new Vector3(640, 480, 200);
                public Vector3 CameraPosition { get; set; } = new Vector3(0, 0, 0);
                public Vector3 CameraRotation { get; set; } = new Vector3(0, 0, 0);
                public Vector3 CameraAhead { get; set; } = new Vector3(0, 0, 1);
                public Vector3 CameraRight { get; set; } = new Vector3(1, 0, 0);
                public Vector3 CameraDown { get; set; } = new Vector3(0, 1, 0);
            }
        }
    }
}