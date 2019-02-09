using UnityEngine;

namespace Kovnir.FastTweener.Test
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