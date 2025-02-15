﻿using System;
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

        private static bool initialized;

        public static void Init(FastTweenerSettings settings = null, bool dontDestroyOnLoad = true)
        {
            if (initialized)
            {
                Debug.LogError(FastTweenerStringConstants.ALREADY_INITIALIZED);
                return;
            }

            if (settings != null)
            {
                FastTweener.settings = settings;
            }

            FastTweenerComponent.Init(dontDestroyOnLoad);
        }

        public static void Dispose()
        {
            initialized = false;
            FastTweenerComponent.Dispose();
        }

        public static FastTween Schedule(float delay, Action callback, bool ignoreTimescale = false)
            => FastTweenerComponent.Schedule(delay, callback, ignoreTimescale);

        public static void Kill(FastTween tween)
        {
            if (tween.Id == 0)
            {
                return;
            }

            FastTweenerComponent.Kill(tween);
        }

        public static bool IsActive(FastTween tween) => FastTweenerComponent.IsActive(tween);

        public static void SetEase(FastTween tween, Ease ease) => FastTweenerComponent.SetEase(tween, ease);

        public static Ease GetEase(FastTween tween) => FastTweenerComponent.GetEase(tween);

        public static void SetIgnoreTimeScale(FastTween tween, bool ignoreTimeScale) =>
            FastTweenerComponent.SetIgnoreTimeScale(tween, ignoreTimeScale);

        public static bool GetIgnoreTimeScale(FastTween tween) => FastTweenerComponent.GetIgnoreTimeScale(tween);

        public static void SetOnComplete(FastTween tween, Action onComplete) =>
            FastTweenerComponent.SetOnComplete(tween, onComplete);

        //ease ignoreTimescale onComplete
        public static FastTween Float(float start, float end, float duration, Action<float> callback,
            Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null) =>
            FastTweenerComponent.Float(start, end, duration, callback, ease, ignoreTimescale, onComplete);

        //ease onComplete
        public static FastTween Float(float start, float end, float duration, Action<float> callback,
            Ease ease, Action onComplete) =>
            FastTweenerComponent.Float(start, end, duration, callback, ease, false, onComplete);

        //ignoreTimescale onComplete
        public static FastTween Float(float start, float end, float duration, Action<float> callback,
            bool ignoreTimescale, Action onComplete = null) =>
            FastTweenerComponent.Float(start, end, duration, callback, Ease.Default, ignoreTimescale,
                onComplete);

        //onComplete
        public static FastTween
            Float(float start, float end, float duration, Action<float> callback, Action onComplete) =>
            FastTweenerComponent.Float(start, end, duration, callback, Ease.Default, false, onComplete);

        //ease ignoreTimescale onComplete        
        public static FastTween Vector3(Vector3 start, Vector3 end, float duration, Action<Vector3> callback,
            Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null) =>
            FastTweenerComponent.Vector3(start, end, duration, callback, ease, ignoreTimescale, onComplete);

        //ease onComplete
        public static FastTween Vector3(Vector3 start, Vector3 end, float duration, Action<Vector3> callback,
            Ease ease, Action onComplete) =>
            FastTweenerComponent.Vector3(start, end, duration, callback, ease, false, onComplete);

        //ignoreTimescale onComplete
        public static FastTween Vector3(Vector3 start, Vector3 end, float duration, Action<Vector3> callback,
            bool ignoreTimescale, Action onComplete = null) =>
            FastTweenerComponent.Vector3(start, end, duration, callback, Ease.Default, ignoreTimescale,
                onComplete);

        //onComplete        
        public static FastTween Vector3(Vector3 start, Vector3 end, float duration, Action<Vector3> callback,
            Action onComplete) =>
            FastTweenerComponent.Vector3(start, end, duration, callback, Ease.Default, false, onComplete);
    }
}