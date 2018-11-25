using System;

namespace Kovnir.Tweener
{
    public class FastTweener
    {
        public const int START_TASK_LIST_SIZE = 8;
        
        public static void Init(int poolSize = START_TASK_LIST_SIZE)
        {
            FastTweenerComponent.Init(poolSize);
        }

        public static int Schedule(float delay, Action callback, bool ignoreTimescale = false)
        {
            return FastTweenerComponent.Schedule(delay, callback, ignoreTimescale);
        }

        public static void Unschedule(int id)
        {
            FastTweenerComponent.Unschedule(id);
        }

        public static int Float(float start, float end, float duration, Action<float> callback,
            Ease ease = Ease.OutQuad, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweenerComponent.Float(start, end, duration, callback, ease, ignoreTimescale, onComplete);
        }
        
        public static int Float(float start, float end, float duration, Action<float> callback,
            Ease ease, Action onComplete)
        {
            return FastTweenerComponent.Float(start, end, duration, callback, ease, false, onComplete);
        }
        
        public static int Float(float start, float end, float duration, Action<float> callback, Action onComplete)
        {
            return FastTweenerComponent.Float(start, end, duration, callback, Ease.OutQuad, false, onComplete);
        }
        
        public static void CancelFloat(int id)
        {
            FastTweenerComponent.CancelFloat(id);
        }
    }
}