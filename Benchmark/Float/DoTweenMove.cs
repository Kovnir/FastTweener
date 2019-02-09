using DG.Tweening;
using UnityEngine;

namespace Kovnir.FastTweener.Test
{
    public class DoTweenMove : MonoBehaviour
    {
        private void Start()
        {
            DOVirtual.Float(-3, 3, 0.5f,
                    value => { transform.position = new Vector3(transform.position.x, value, transform.position.z); })
                .SetEase(DG.Tweening.Ease.OutBounce);
        }
    }
}