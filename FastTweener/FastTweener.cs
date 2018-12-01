using System;
using UnityEngine;

namespace Kovnir.Tweener
{
    
    public static class FastTweener
    {
        public const int START_TASK_LIST_SIZE = 16;
        
        public static void Init(int poolSize = START_TASK_LIST_SIZE)
        {
            FastTweenerComponent.Init(poolSize);
        }
        
        public static FastTween Schedule(float delay, Action callback, bool ignoreTimescale = false)
        {
            return FastTweenerComponent.Schedule(delay, callback, ignoreTimescale);
        }

        public static void Kill(FastTween tween)
        {
            FastTweenerComponent.Kill(tween);
        }

        public static bool IsActive(FastTween tween)
        {
            return FastTweenerComponent.IsActive(tween);
        }
        
        public static void SetEase(FastTween fastTween, Ease ease)
        {
            FastTweenerComponent.SetEase(fastTween, ease);
        }

        //ease ignoreTimescale onComplete
        public static FastTween Float(float start, float end, float duration, Action<float> callback,
            Ease ease = Ease.OutQuad, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweenerComponent.Float(start, end, duration, callback, ease, ignoreTimescale, onComplete);
        }
        
        //ease onComplete
        public static FastTween Float(float start, float end, float duration, Action<float> callback,
            Ease ease, Action onComplete)
        {
            var r = new FastTween();
            return FastTweenerComponent.Float(start, end, duration, callback, ease, false, onComplete);
        }
        
        //ignoreTimescale onComplete
        public static FastTween Float(float start, float end, float duration, Action<float> callback,
            bool ignoreTimescale, Action onComplete = null)
        {
            return FastTweenerComponent.Float(start, end, duration, callback, Ease.OutQuad, ignoreTimescale, onComplete);
        }
        
        //onComplete
        public static FastTween Float(float start, float end, float duration, Action<float> callback, Action onComplete)
        {
            return FastTweenerComponent.Float(start, end, duration, callback, Ease.OutQuad, false, onComplete);
        }
        
        //ease ignoreTimescale onComplete        
        public static FastTween Vector3(Vector3 start, Vector3 end, float duration, Action<Vector3> callback,
            Ease ease = Ease.OutQuad, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweenerComponent.Vector3(start, end, duration, callback, ease, ignoreTimescale, onComplete);
        }
        
        //ease onComplete
        public static FastTween Vector3(Vector3 start, Vector3 end, float duration, Action<Vector3> callback,
            Ease ease, Action onComplete)
        {
            return FastTweenerComponent.Vector3(start, end, duration, callback, ease, false, onComplete);
        }
        
        //ignoreTimescale onComplete
        public static FastTween Vector3(Vector3 start, Vector3 end, float duration, Action<Vector3> callback,
            bool ignoreTimescale, Action onComplete = null)
        {
            return FastTweenerComponent.Vector3(start, end, duration, callback, Ease.OutQuad, ignoreTimescale, onComplete);
        }

        //onComplete        
        public static FastTween Vector3(Vector3 start, Vector3 end, float duration, Action<Vector3> callback, Action onComplete)
        {
            return FastTweenerComponent.Vector3(start, end, duration, callback, Ease.OutQuad, false, onComplete);
        }
    }
}