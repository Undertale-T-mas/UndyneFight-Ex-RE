using System;

namespace UndyneFight_Ex
{
    public class GameEventArgs : EventArgs
    {
        public GameEventArgs(GameObject gameObject, string info)
        {
            Source = gameObject;
            ActionName = info;
        }

        public string ActionName { get; set; }
        public GameObject Source { get; set; }

        public void Dispose()
        {
            Disposed = true;
        }
        public bool Disposed { get; private set; }
    }
}