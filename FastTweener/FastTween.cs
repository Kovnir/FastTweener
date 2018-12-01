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

        public void SetEase(Ease ease)
        {
            FastTweener.SetEase(this, ease);            
        }
    }
}