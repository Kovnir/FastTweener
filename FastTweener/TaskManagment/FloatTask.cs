using System;
using UnityEngine;

namespace Kovnir.Tweener.TaskManagment
{
    public class FloatTask : ITask
    {
        public int Id { get; set; }
        public float Start;
        public float End;
        public float Duration;
        public Action<float> Callback;
        public Action OnComplete;
        public Ease Ease;
        public bool IgnoreTimescale;
        public float CurrentTime;

        public void Set(float start, float end, float duration, Action<float> callback, Ease ease, bool ignoreTimescale, Action onComplete)
        {
            Start = start;
            End = end;
            Duration = duration;
            Callback = callback;
            Ease = ease;
            IgnoreTimescale = ignoreTimescale;
            OnComplete = onComplete;
            CurrentTime = 0;
        }

        public bool Proccess(float unscaledDeltaTime, float deltaTime)
        {
            CurrentTime += IgnoreTimescale ? unscaledDeltaTime : deltaTime;
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
    }
}