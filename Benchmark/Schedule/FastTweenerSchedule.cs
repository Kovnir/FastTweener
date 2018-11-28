using UnityEngine;

namespace Kovnir.Tweener.Test
{
    public class FastTweenerSchedule : MonoBehaviour
    {
        public bool Done = false;

        void Start()
        {
            FastTweener.Schedule(1f, () => { Done = true; });
        }
    }
}