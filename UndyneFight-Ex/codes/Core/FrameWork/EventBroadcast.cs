using System;

namespace UndyneFight_Ex
{
    public class GameEventArgs : EventArgs
    {
        public GameEventArgs(GameObject gameObject, string info)
        {
            this.Source = gameObject;
            this.ActionName = info;
        }

        public string ActionName { get; set; }
        public GameObject Source { get; set; }

        public void Dispose()
        {
            this.Disposed = true;
        }
        public bool Disposed { get; private set; }
    }
}