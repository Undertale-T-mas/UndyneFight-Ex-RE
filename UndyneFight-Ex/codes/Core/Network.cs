using GameJolt;
using GameJolt.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace UndyneFight_Ex.Network
{
    public abstract class DataGather<T> : GameObject where T : class
    {
        protected CancellationTokenSource source = new();
        protected T Result { get; private set; }
        protected bool ResultAvailable { get; set; } = false;

        private bool _eventActivated = false;

        public virtual bool Working => !ResultAvailable && _task != null;

        private Task<T> _task;

        protected event Action<T> ResultGet;

        protected DataGather(Func<T> getResult)
        {
            RunTask(getResult);
        }
        protected DataGather()
        {

        }
        protected void RunTask(Func<T> getResult)
        {
            _task = new(getResult);
            RunTask(_task);
        }
        protected async Task RunTask(Task<T> getResult)
        {
            _task = getResult;
            Result = null; ResultAvailable = false;
            _eventActivated = false;

            _task.Start();
            Result = await _task;
            if (_task.IsCanceled) { return; }
            ResultAvailable = true;
        }
        public sealed override void Update()
        {
            if (_eventActivated && ResultAvailable)
            {
                _eventActivated = true;
                ResultGet?.Invoke(Result);
            }
        }
        public override void Dispose()
        {
            source.Cancel();
            base.Dispose();
        }
    }
    public class GamejoltLogin : DataGather<Response<Credentials>>
    {
        public GamejoltLogin()
        {
        }
        public event Action<Response<Credentials>> LoginSuccess;
        public event Action<Response<Credentials>> LoginFailure;

        public override bool Working => working;
        bool working = false;

        public void Login(string name, string password)
        {
            working = true;
            GameJoltApi api = GameStates.GameJolt;

            //RunTask(api.Users.AuthAsync(name, password));
            api.Users.Auth(name, password, (s) =>
            {
                working = false;
                if (s.Success) LoginSuccess?.Invoke(s);
                else LoginFailure?.Invoke(s);
            });
        }
    }
}