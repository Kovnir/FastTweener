using DG.Tweening;
using UnityEngine;

namespace Kovnir.Tweener.Test
{
    public class DoTweenSchedule : MonoBehaviour
    {
        public bool Done = false;

        void Start()
        {
            DOVirtual.DelayedCall(1f, () => { Done = true; });
        }
    }
}