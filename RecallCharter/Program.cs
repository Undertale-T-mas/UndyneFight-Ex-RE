using RecallCharter;
using UndyneFight_Ex;
using UndyneFight_Ex.GameInterface;

UndyneFight_Ex.Remake.Initialize.MainInitialize();
GameStartUp.CheckLevelExist = false;
GameStates.Aspect = 1.5f;
Entity.DrawOptimize = false;
UndyneFight_Ex.Settings.SettingsManager.DataLibrary.DrawFPS = 999f;

GameStartUp.MainSceneIntro = () => { GameStates.InstanceCreate(new MasterControl()); };

GameStartUp.StartGame();