using System;

namespace UndyneFight_Ex.Entities
{
    public partial class Arrow
    {
        private class DelayControl : GameObject
        {
            internal enum DelayType
            {
                Pull = 0,
                Stop = 1
            }
            private float delay = 0;
            private readonly DelayType type;
            public DelayControl(float delay, DelayType delayType)
            {
                UpdateIn120 = true;
                type = delayType;
                this.delay = delay;
            }
            public override void Update()
            {
                Arrow control = FatherObject as Arrow;
                float del = type == DelayType.Pull
                    ? Math.Max(0.5f, MathF.Min(2.5f, delay * 0.1f))
                    : Math.Max(0.4f, MathF.Min(1, (delay > 10 ? 10 : MathF.Sqrt(delay * 2)) * 0.3f));
                if (delay < del)
                    del = delay;
                del /= 2;
                control.shootShieldTime += del;
                delay -= del;
                if (delay <= 0f) Dispose();
            }
            public override void Dispose()
            {
                base.Dispose();
            }
        }

        public void Delay(float delay)
        {
            AddChild(new DelayControl(delay, DelayControl.DelayType.Pull));
        }
        public void Stop(float delay)
        {
            AddChild(new DelayControl(delay, DelayControl.DelayType.Stop));
        }
    }
}