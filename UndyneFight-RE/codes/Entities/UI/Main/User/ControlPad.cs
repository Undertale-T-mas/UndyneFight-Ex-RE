namespace UndyneFight_Ex.Remake.UI
{
    internal partial class UserUI : Entity
    {
        private class VirtualFather : GameObject
        {  
            public VirtualFather()
            {
                Login = new LoginUI();
                Register = new RegisterUI();
                AddChild(Login);
                AddChild(Register);
                Login.Activate();
                CurrentActivate = Login;
            }

            public bool Activated => true;

            public LoginUI Login { get; init; } 
            public RegisterUI Register { get; init; } 

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

        public UserUI()
        {
            CurrentScene.CurrentDrawingSettings.defaultWidth = 960f;
             
            if (PlayerManager.CurrentUser != null)
            {
                Dispose();
                GameStates.InstanceCreate(new SelectUI());
                return;
            }
            AddChild(new MouseCursor());
            AddChild(new LineDistributer());
            AddChild(new VirtualFather());
        }

        public override void Draw()
        { 

        }

        public override void Update()
        { 
        }
    }
}
