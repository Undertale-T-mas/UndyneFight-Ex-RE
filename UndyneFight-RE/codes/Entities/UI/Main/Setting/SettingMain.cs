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
using UndyneFight_Ex.Fight;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SettingUI : Entity
    {
        private partial class VirtualFather : GameObject
        {
            public VirtualFather()
            { 
                this.AddChild(_audioSetting = new());  
                this.AddChild(_videoSetting = new());  
                this.AddChild(_playSetting = new());  
                this.AddChild(_inputSetting = new());  
                _audioSetting.Activate();
                _focusChunk = CurrentActivate = _audioSetting;
                UpdateIn120 = true;
            }
            AudioSetting _audioSetting;
            VideoSetting _videoSetting;
            PlaySetting _playSetting;
            InputSetting _inputSetting;

            private bool _keyEnabled = false;

            SettingChunk[] all;

            public override void Start()
            {
                this.all = new SettingChunk[] {_audioSetting, _videoSetting, _playSetting, _inputSetting};
                base.Start();
            }

            public bool Activated => true; 

            private SettingChunk CurrentActivate { get; set; }

            public void Select(ISelectChunk module)
            {
                if (CurrentActivate == module) return;
                CurrentActivate.Deactivate();
                CurrentActivate = module as SettingChunk;
                this._keyEnabled = false;
            }
            int _focusID = 0;
            int FocusID
            {
                get
                {
                    if(_focusID != -1) return _focusID;
                    for(int i = 0; i < all.Length; i++)
                    {
                        if (all[i] == _focusChunk) return i;
                    }
                    return -1;
                }
            }
            SettingChunk _focusChunk;

            private void OnFocus(SettingChunk chunk)
            {
                _focusChunk?.KeyOff();
                _focusChunk = chunk;
                _focusID = -1;
            }

            public override void Update()
            {
                if (_keyEnabled)
                {
                    if (GameStates.IsKeyPressed120f(InputIdentity.MainDown))
                    {
                        int id = FocusID;
                        id++;
                        if (id < all.Length)
                        {
                            _focusID = id;
                            all[_focusID].KeyOn();
                            Functions.PlaySound(FightResources.Sounds.changeSelection);
                        }
                    }
                    else if (GameStates.IsKeyPressed120f(InputIdentity.MainUp))
                    {
                        int id = FocusID;
                        id--;
                        if (id >= 0)
                        {
                            _focusID = id;
                            all[_focusID].KeyOn();
                            Functions.PlaySound(FightResources.Sounds.changeSelection);
                        };
                    }
                    if (GameStates.IsKeyPressed120f(InputIdentity.Confirm))
                    {
                        all[FocusID].Activate();
                        Select(all[FocusID]);
                        this._keyEnabled = false;
                    }
                }
            }
        }

        public SettingUI()
        {
            CurrentScene.CurrentDrawingSettings.defaultWidth = 960f;
              
            this.AddChild(new MouseCursor());
            this.AddChild(new LineDistributer());
            this.AddChild(new VirtualFather());
        }

        public override void Draw()
        {

        }

        public override void Update()
        {
            if (GameStates.IsKeyPressed(InputIdentity.Cancel))
            {
                this.Dispose();
                GameStates.InstanceCreate(new DEBUG.IntroUI());
            }
        }
    }
}