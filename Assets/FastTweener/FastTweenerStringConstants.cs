using System;

namespace Kovnir.FastTweener
{
    public static class FastTweenerStringConstants
    {
        //here are not constants to allocate memory in the constructor instead of first access. Just for pretty benchmarks
        public static readonly string CALLBACK_IS_NULL = "FastTweener: Callback is null!";
        public static readonly string ON_COMPLETE_EXCEPTION = "FastTweener: Exception has been caught in OnComplete callback: {0}\n{1}";
        public static readonly string POOL_SIZE_ERROR = "FastTweener: Pool Size should be grater than zero!";
        public static readonly string DEFAULT_EASE_ERROR = "FastTweener: You can't set Default Ease to value Ease.Default";
        public static readonly string RIGIDBODY_IS_NULL = "FastTweener: Rigidbody is null!";
        public static readonly string DEFAULT_NAME = "names saving disabled";
        public static readonly string GAME_OBJECT_WAS_DESTROYED = "FastTweener: GameObject was destroyed! Name: {0}; Type: {1}";
        public static readonly string TASK_LATE = "FastTweener: Low fps. Scheduled task late: {0}";
        public static readonly string FPS_VALUE_ERROR = "FastTweener: You need to set fps grate than one or equal zero to disable warnings";
        public static readonly string ALREADY_INITIALIZED = "FastTweener: FastTweener has been already initialized!";
    }
}