using DG.Tweening;
using UnityEngine;

namespace Kovnir.FastTweener.Test
{
    public class DoTweenSchedule : MonoBehaviour
    {
        public bool Done = false;

        private void OnEnable()
        {
            DOVirtual.DelayedCall(1f, () => { Done = true; });
        }
    }
}