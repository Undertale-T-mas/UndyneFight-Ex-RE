using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using System.Net.Sockets;
using UndyneFight_Ex.SongSystem;
using System.ComponentModel.Design.Serialization;
using Microsoft.Xna.Framework.Graphics;
using UndyneFight_Ex.Entities;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SettingUI : Entity
    {
        private partial class VirtualFather : GameObject
        {
            public VirtualFather()
            { 
                this.AddChild(_audioSetting = new());  
                _audioSetting.Activate();
                CurrentActivate = _audioSetting;  
            }
            AudioSetting _audioSetting;

            public bool Activated => true; 

            public ISelectChunk CurrentActivate { get; set; }

            public void Select(ISelectChunk module)
            {
                if (CurrentActivate == module) return;
                CurrentActivate.Deactivate();
                CurrentActivate = module;
            }

            public override void Update()
            {
            }
        }

        public SettingUI()
        {
            CurrentScene.CurrentDrawingSettings.defaultWidth = 960f;
             
            if (PlayerManager.CurrentUser != null)
            {
                this.Dispose();
                GameStates.InstanceCreate(new SelectUI());
                return;
            }
            this.AddChild(new MouseCursor());
            this.AddChild(new LineDistributer());
            this.AddChild(new VirtualFather());
        }

        public override void Draw()
        {

        }

        public override void Update()
        {
        }
    }
}