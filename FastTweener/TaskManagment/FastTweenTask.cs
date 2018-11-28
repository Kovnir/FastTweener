using System;
using System.Globalization;
using UnityEngine;

namespace Kovnir.Tweener.TaskManagment
{
    public class FastTweenTask
    {
        //here are not constants to allocate memory in the constructor instead of first access. Just for pretty benchmarks
        private static readonly string TASK_LATE = "FastTweener: Low fps. Scheduled task late: ";
        
        public int Id;
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
            Ease = ease;
            IgnoreTimescale = ignoreTimescale;
            OnComplete = onComplete;
            CurrentTime = 0;
        }

        public void SetDelayCall(float delay, Action action, bool ignoreTimescale)
        {
            Type = TweenType.DelayCall;
            Duration = delay;
            OnComplete = action;
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
            CallbackVector3 = callback;
            Ease = ease;
            IgnoreTimescale = ignoreTimescale;
            OnComplete = onComplete;
            CurrentTime = 0;
            Start = 0;
            End = 1;
        }


        public bool Proccess(float unscaledDeltaTime, float deltaTime)
        {
            CurrentTime += IgnoreTimescale ? unscaledDeltaTime : deltaTime;
            switch (Type)
            {
                case TweenType.DelayCall:
                    return ProcessScheduling();
                case TweenType.Float:
                    return ProcessFloat();
                case TweenType.Vector3:
                    return ProcessVector3();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool ProcessFloat()
        {
            if (CurrentTime <= 0)
            {
                Callback(Start);
                return false;
            }
            if (CurrentTime >= Duration)
            {
                Callback(End);
                return true;
            }
            Callback(EaseCalculator.Calculate(Ease, Start, End, CurrentTime, Duration));
            return false;            
        }
        
        private bool ProcessVector3()
        {
            if (CurrentTime <= 0)
            {
                CallbackVector3(StartVector3);
                return false;
            }
            if (CurrentTime >= Duration)
            {
                CallbackVector3(EndVector3);
                return true;
            }

            float value = EaseCalculator.Calculate(Ease, Start, End, CurrentTime, Duration);
            CallbackVector3(StartVector3 + (EndVector3 - StartVector3) * value);
            return false;
        }
        
        
        private bool ProcessScheduling()
        {
            if (CurrentTime >= Duration)
            {
                //log warning if we late becouse of low fps (less then 30fps)
                if (CurrentTime - Duration < -1f/30f)
                {
                    Debug.LogWarning(TASK_LATE + CurrentTime.ToString(CultureInfo.InvariantCulture));
                }
                return true;
            }

            return false;
        }

    }
}