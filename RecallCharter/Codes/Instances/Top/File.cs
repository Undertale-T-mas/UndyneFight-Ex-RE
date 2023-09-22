using Microsoft.Xna.Framework;
using UndyneFight_Ex;
using UndyneFight_Ex.GameInterface;

namespace RecallCharter
{
    internal class FileButton : TextButton
    {
        Panel panel;
        public FileButton()
        {
            this.AddChild(panel = new Panel()
            {
                CollidingBox = new(new(0, 42), new(180, 42 * 5)),
                BackgroundColor = Color.DimGray * 0.6f,
                BorderColor = Color.Gold,
                Depth = 0.12f
            }); ;
            TextButton fileNew, fileOpen, fileSave, fileClone, fileSaveAll;
            panel.AddChild(fileNew = new TextButton()
            {
                Text = "新建    (1)",
                Depth = 0.15f,
                CollidingBox = new(new(3, 3), new(174, 40)),
                FontColor = Color.White,
                MouseOnColor = Color.Gray,
            });
            panel.AddChild(fileOpen = new TextButton()
            {
                Text = "打开    (2)",
                Depth = 0.15f,
                CollidingBox = new(new(3, 3 + 41), new(174, 40)),
                FontColor = Color.White,
                MouseOnColor = Color.Gray
            });
            panel.AddChild(fileSave = new TextButton()
            {
                Text = "保存    (3)",
                Depth = 0.15f,
                CollidingBox = new(new(3, 3 + 41 * 2), new(174, 40)),
                FontColor = Color.White,
                MouseOnColor = Color.Gray
            });
            panel.AddChild(fileClone = new TextButton()
            {
                Text = "备份    (4)",
                Depth = 0.15f,
                CollidingBox = new(new(3, 3 + 41 * 3), new(174, 40)),
                FontColor = Color.White,
                MouseOnColor = Color.Gray
            });
            panel.AddChild(fileSaveAll = new TextButton()
            {
                Text = "全部保存(5)",
                Depth = 0.15f,
                CollidingBox = new(new(3, 3 + 41 * 4), new(174, 40)),
                FontColor = Color.White,
                MouseOnColor = Color.Gray
            });
            panel.ChildObjects.ForEach(s => 
            { 
                TextButton t = s as TextButton; 
                t.OnClick += (u, v) =>
                {
                    t.Deattach();
                };
            });
            fileNew.OnClick += (u, v) => {
                Window window;
                MasterControl.AddControl(window = new Window() { Depth = 0.5f, CollidingBox = new(360, 250, 360, 250), 
                    Title =  "命名新谱面", ForceFocus = true });
                window.Attach();
                window.AddChild(new Label()
                {
                    CollidingBox = new(new Vector2(14, 41), new Vector2(0, 0)),
                    Text = "文件夹名",
                    Depth = 0.51f,
                    FontColor = Color.White
                }); 
                window.AddChild(new Label()
                {
                    CollidingBox = new(new Vector2(9, 94), new Vector2(0, 0)),
                    Text = "创建新谱面后，将音乐自行\n放入该文件夹中",
                    Depth = 0.51f,
                    Scale = 0.9f,
                    FontColor = Color.White
                });
                Button a, b;
                window.AddChild(a = new TextButton()
                {
                    CollidingBox = new(new Vector2(37, 171), new Vector2(100, 50)),
                    Text = "确定",
                    Scale = 1.2f,
                    Depth = 0.51f,
                    FontColor = Color.White,
                    MouseOnColor = Color.Gray,
                    BackgroundColor = Color.Gray * 0.6f,
                });
                window.AddChild(b = new TextButton()
                {
                    CollidingBox = new(new Vector2(211, 171), new Vector2(100, 50)),
                    Text = "取消",
                    Scale = 1.2f,
                    Depth = 0.51f,
                    FontColor = Color.White,
                    MouseOnColor = Color.Gray,
                    BackgroundColor = Color.Gray * 0.6f,
                });
                a.OnClick += (u, v) =>
                {
                    window.Dispose();
                    System.Diagnostics.Process.Start("Explorer.exe", System.Environment.CurrentDirectory);
                };
                b.OnClick += (u, v) =>
                {
                    window.Dispose();
                };
            };

            this.FontColor = Color.White;
            this.Depth = 0.1f;
            this.Text = "文件";
            this.MouseOnColor = Color.Gray;
            this.collidingBox = new(new Vector2(0, 0), new Vector2(90, 42));
        } 
    }
}