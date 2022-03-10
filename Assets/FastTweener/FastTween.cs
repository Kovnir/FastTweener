using System;

namespace Kovnir.FastTweener
{
    public struct FastTween
    {
        public readonly uint Id;

        public FastTween(uint id)
        {
            Id = id;
        }
        
        public void Kill()
        {
            if (Id != 0)
            {
                FastTweener.Kill(this);
            }
        }

        public bool IsAlive()
        {
            if (Id != 0)
            {
                return FastTweener.IsAlive(this);
            }
            return false;
        }

        public FastTween SetEase(Ease ease)
        {
            if (Id != 0)
            {
                FastTweener.SetEase(this, ease);
            }
            return this;
        }

        public Ease GetEase()
        {
            return FastTweener.GetEase(this);            
        }

        public FastTween SetIgnoreTimeScale(bool ignoreTimeScale)
        {
            if (Id != 0)
            {
                FastTweener.SetIgnoreTimeScale(this, ignoreTimeScale);
            }
            return this;
        }

        public FastTween OnComplete(Action onComplete)
        {
            if (Id != 0)
            {
                FastTweener.SetOnComplete(this, onComplete);
            }
            return this;
        }
        
        public bool GetIgnoreTimeScale()
        {
            if (Id != 0)
            {
                return FastTweener.GetIgnoreTimeScale(this);
            }
            return false;
        }
    }
}