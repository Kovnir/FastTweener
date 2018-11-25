using System;

namespace Kovnir.Tweener.TaskManagment
{
    public class SchedutedTask : ITask
    {
        public float Delay;
        public Action Action;
        public bool IgnoreTimescale;
        public int Id { get; set; }

        public SchedutedTask Set(float delay, Action action, bool ignoreTimescale)
        {
            this.Delay = delay;
            this.Action = action;
            this.IgnoreTimescale = ignoreTimescale;
            return this;
        }

    }
}