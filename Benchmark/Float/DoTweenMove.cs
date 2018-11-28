using DG.Tweening;
using UnityEngine;

namespace Kovnir.Tweener.Test
{
    public class DoTweenMove : MonoBehaviour
    {
        void Start()
        {
            DOVirtual.Float(-3, 3, 2,
                    value => { transform.position = new Vector3(transform.position.x, value, transform.position.z); })
                .SetEase(DG.Tweening.Ease.OutBounce);
        }
    }
}