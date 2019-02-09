using UnityEngine;

namespace Kovnir.FastTweener.Test
{
    public class FastTweenerMove : MonoBehaviour
    {
        void Start()
        {
            FastTweener.Float(-3, 3, 2,
                value => { transform.position = new Vector3(transform.position.x, value, transform.position.z); },
                Ease.OutBounce);
        }
    }
}