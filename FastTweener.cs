using System;
using Kovnir.FastTweener.Extension;
using UnityEngine;

namespace Kovnir.FastTweener
{
    public static class FastTweener
    {
        private static FastTweenerSettings settings;

        public static FastTweenerSettings Setting
        {
            get
            {
                if (settings == null)
                {
                    settings = new FastTweenerSettings();
                }
                return settings;
            }
        }

        public static bool IsInitialized
        {
            get { return FastTweenerComponent.IsInitialized; }
        }

        public static void Init(FastTweenerSettings settings = null)
        {
            if (IsInitialized)
            {
                Debug.LogError(FastTweenerStringConstants.ALREADY_INITIALIZED);
                return;
            }
            if (settings != null)
            {
                FastTweener.settings = settings;
            }

            FastTweenerComponent.Init();
        }
        
        public static FastTween Schedule(float delay, Action callback, bool ignoreTimescale = false)
        {
            return FastTweenerComponent.Schedule(delay, callback, ignoreTimescale);
        }

        public static void Kill(FastTween tween)
        {
            if (tween.Id == 0)
            {
                return;
            }
            FastTweenerComponent.Kill(tween);
        }

        public static bool IsAlive(FastTween tween)
        {
            return FastTweenerComponent.IsAlive(tween);
        }
        
        public static void SetEase(FastTween tween, Ease ease)
        {
            FastTweenerComponent.SetEase(tween, ease);
        }

        public static Ease GetEase(FastTween tween)
        {
            return FastTweenerComponent.GetEase(tween);
        }
        
        public static void SetIgnoreTimeScale(FastTween tween, bool ignoreTimeScale)
        {
            FastTweenerComponent.SetIgnoreTimeScale(tween, ignoreTimeScale);
        }

        public static bool GetIgnoreTimeScale(FastTween tween)
        {
            return FastTweenerComponent.GetIgnoreTimeScale(tween);
        }

        public static void SetOnComplete(FastTween tween, Action onComplete)
        {
            FastTweenerComponent.SetOnComplete(tween, onComplete);
        }

        //ease ignoreTimescale onComplete
        public static FastTween Float(float start, float end, float duration, Action<float> callback,
            Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweenerComponent.Float(start, end, duration, callback, ease, ignoreTimescale, onComplete);
        }
        
        //ease onComplete
        public static FastTween Float(float start, float end, float duration, Action<float> callback,
            Ease ease, Action onComplete)
        {
            return FastTweenerComponent.Float(start, end, duration, callback, ease, false, onComplete);
        }
        
        //ignoreTimescale onComplete
        public static FastTween Float(float start, float end, float duration, Action<float> callback,
            bool ignoreTimescale, Action onComplete = null)
        {
            return FastTweenerComponent.Float(start, end, duration, callback, Ease.Default, ignoreTimescale, onComplete);
        }
        
        //onComplete
        public static FastTween Float(float start, float end, float duration, Action<float> callback, Action onComplete)
        {
            return FastTweenerComponent.Float(start, end, duration, callback, Ease.Default, false, onComplete);
        }
        
        //ease ignoreTimescale onComplete        
        public static FastTween Vector3(Vector3 start, Vector3 end, float duration, Action<Vector3> callback,
            Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null)
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
            return FastTweenerComponent.Vector3(start, end, duration, callback, Ease.Default, ignoreTimescale, onComplete);
        }

        //onComplete        
        public static FastTween Vector3(Vector3 start, Vector3 end, float duration, Action<Vector3> callback, Action onComplete)
        {
            return FastTweenerComponent.Vector3(start, end, duration, callback, Ease.Default, false, onComplete);
        }
    }
}