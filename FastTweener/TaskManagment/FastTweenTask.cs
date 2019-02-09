using System;
using System.Globalization;
using UnityEngine;

namespace Kovnir.FastTweener.TaskManagment
{
    public class FastTweenTask
    {
        public uint Id;
        public float Duration;
        public bool IgnoreTimescale;
        public Action OnComplete;
        public TweenType Type;
        public float CurrentTime;
        
        public Ease Ease;

        public float Start;
        public float End;
        public Action<float> Callback;

        public Vector3 StartVector3;
        public Vector3 EndVector3;
        public Action<Vector3> CallbackVector3;
        

        public void SetFloat(float start, float end, float duration, Action<float> callback, Ease ease, bool ignoreTimescale, Action onComplete)
        {
            Type = TweenType.Float;
            Start = start;
            End = end;
            Duration = duration;
            Callback = callback;
            CallbackVector3 = null;
            Ease = GetRealEase(ease);
            IgnoreTimescale = ignoreTimescale;
            OnComplete = onComplete;
            CurrentTime = 0;
        }

        private Ease GetRealEase(Ease ease)
        {
            if (ease == Ease.Default)
            {
                return FastTweener.Setting.DefaultEase;
            }
            return ease;
        }

        public void SetDelayCall(float delay, Action action, bool ignoreTimescale)
        {
            Type = TweenType.DelayCall;
            Duration = delay;
            OnComplete = action;
            Callback = null;
            CallbackVector3 = null;
            IgnoreTimescale = ignoreTimescale;
            CurrentTime = 0;
        }

        public void SetVector3(Vector3 start, Vector3 end, float duration, Action<Vector3> callback, Ease ease,
            bool ignoreTimescale, Action onComplete)
        {
            Type = TweenType.Vector3;
            StartVector3 = start;
            EndVector3 = end;
            Duration = duration;
            Callback = null;
            CallbackVector3 = callback;
            Ease = GetRealEase(ease);
            IgnoreTimescale = ignoreTimescale;
            OnComplete = onComplete;
            CurrentTime = 0;
            Start = 0;
            End = 1;
        }


        public bool Proccess(float unscaledDeltaTime, float deltaTime, out Exception exception)
        {
            CurrentTime += IgnoreTimescale ? unscaledDeltaTime : deltaTime;
            switch (Type)
            {
                case TweenType.DelayCall:
                    exception = null;
                    return ProcessScheduling();
                case TweenType.Float:
                    return ProcessFloat(out exception);
                case TweenType.Vector3:
                    return ProcessVector3(out exception);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Exception CallFloatCallback(float value)
        {
            try
            {
                Callback(value);
            }
            catch (Exception e)
            {
                return e;
            }
            return null;
        }

        private Exception CallVector3Callback(Vector3 value)
        {
            try
            {
                CallbackVector3(value);
            }
            catch (Exception e)
            {
                return e;
            }
            return null;
        }

        private bool ProcessFloat(out Exception exception)
        {
            if (CurrentTime <= 0)
            {
                exception = CallFloatCallback(Start);
                return false;
            }
            if (CurrentTime >= Duration)
            {
                exception = CallFloatCallback(End);
                return true;
            }
            exception = CallFloatCallback(EaseCalculator.Calculate(Ease, Start, End, CurrentTime, Duration));
            return false;            
        }
        
        private bool ProcessVector3(out Exception exception)
        {
            if (CurrentTime <= 0)
            {
                exception = CallVector3Callback(StartVector3);
                return false;
            }
            if (CurrentTime >= Duration)
            {
                exception = CallVector3Callback(EndVector3);
                return true;
            }

            float value = EaseCalculator.Calculate(Ease, Start, End, CurrentTime, Duration);
            exception = CallVector3Callback(StartVector3 + (EndVector3 - StartVector3) * value);
            return false;
        }
        
        
        private bool ProcessScheduling()
        {
            if (CurrentTime >= Duration)
            {
                if (FastTweener.Setting.CriticalFpsToLogWarning != 0)
                {
                    if (Duration - CurrentTime > 1f / FastTweener.Setting.CriticalFpsToLogWarning)
                    {
                        //log warning if we late because of low fps
                        Debug.LogWarning(String.Format(FastTweenerStringConstants.TASK_LATE,
                            CurrentTime.ToString(CultureInfo.InvariantCulture)));
                    }
                }

                return true;
            }
            return false;
        }

    }
}