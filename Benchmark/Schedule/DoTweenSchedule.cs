using DG.Tweening;
using UnityEngine;

namespace Kovnir.FastTweener.Test
{
    public class DoTweenSchedule : MonoBehaviour
    {
        public bool Done = false;

        private void Start()
        {
            DOVirtual.DelayedCall(0.5f, () => { Done = true; });
        }
    }
}