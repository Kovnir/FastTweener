using DG.Tweening;
using UnityEngine;

namespace Kovnir.FastTweener.Test
{
    public class DoTweenMove : MonoBehaviour
    {
        private void OnEnable()
        {
            DOVirtual.Float(-3, 3, 2,
                    value => { transform.position = new Vector3(transform.position.x, value, transform.position.z); })
                .SetEase(DG.Tweening.Ease.OutBounce);
        }
    }
}