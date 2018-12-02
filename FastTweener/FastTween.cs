namespace Kovnir.Tweener
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
            FastTweener.Kill(this);
        }

        public bool IsActive()
        {
            if (Id == 0)
            {
                return false;
            }
            return FastTweener.IsActive(this);
        }

        public FastTween SetEase(Ease ease)
        {
            FastTweener.SetEase(this, ease);
            return this;
        }

        public Ease GetEase()
        {
            return FastTweener.GetEase(this);            
        }

        public FastTween SetIgnoreTimeScale(bool ignoreTimeScale)
        {
            FastTweener.SetIgnoreTimeScale(this, ignoreTimeScale);
            return this;
        }
        
        public bool GetIgnoreTimeScale()
        {
            return FastTweener.GetIgnoreTimeScale(this);
        }
    }
}