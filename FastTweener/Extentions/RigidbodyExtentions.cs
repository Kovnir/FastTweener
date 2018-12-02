using System;
using UnityEngine;

namespace Kovnir.Tweener.Extention
{
    public static class RigidbodyExtentions
    {
        //ease ignoreTimescale onComplete        
        public static FastTween TweenMove(this Rigidbody target, Vector3 endValue, float duration, Ease ease = FastTweener.DEFAULT_EASE, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweener.Vector3(target.position, endValue, duration, x => target.position = x, ease, ignoreTimescale, onComplete);
        }

        //ease onComplete
        public static FastTween TweenMove(this Rigidbody target, Vector3 endValue, float duration, Ease ease, Action onComplete = null)
        {
            return FastTweener.Vector3(target.position, endValue, duration, x => target.position = x, ease, onComplete);
        }
        
        //ignoreTimescale onComplete
        public static FastTween TweenMove(this Rigidbody target, Vector3 endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            return FastTweener.Vector3(target.position, endValue, duration, x => target.position = x, ignoreTimescale, onComplete);
        }

        //onComplete
        public static FastTween TweenMove(this Rigidbody target, Vector3 endValue, float duration, Action onComplete)
        {
            return FastTweener.Vector3(target.position, endValue, duration, x => target.position = x, onComplete);
        }

        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenMoveX(this Rigidbody target, float endValue, float duration, Ease ease = FastTweener.DEFAULT_EASE, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweener.Float(target.position.x, endValue,
                duration, x => target.position = new Vector3(x, target.position.y, target.position.z), ease, ignoreTimescale, onComplete);
        }
        
        //ease onComplete
        public static FastTween TweenMoveX(this Rigidbody target, float endValue, float duration, Ease ease, Action onComplete = null)
        {
            return FastTweener.Float(target.position.x, endValue,
                duration, x => target.position = new Vector3(x, target.position.y, target.position.z), ease, onComplete);
        }
        
        //ignoreTimescale onComplete
        public static FastTween TweenMoveX(this Rigidbody target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            return FastTweener.Float(target.position.x, endValue,
                duration, x => target.position = new Vector3(x, target.position.y, target.position.z), ignoreTimescale, onComplete);
        }
        
        //onComplete
        public static FastTween TweenMoveX(this Rigidbody target, float endValue, float duration, Action onComplete)
        {
            return FastTweener.Float(target.position.x, endValue,
                duration, x => target.position = new Vector3(x, target.position.y, target.position.z), onComplete);
        }

        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenMoveY(this Rigidbody target, float endValue, float duration, Ease ease = FastTweener.DEFAULT_EASE, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweener.Float(target.position.y, endValue,
                duration, x => target.position = new Vector3(target.position.x, x, target.position.z), ease, ignoreTimescale, onComplete);
        }
        
        //ease onComplete
        public static FastTween TweenMoveY(this Rigidbody target, float endValue, float duration, Ease ease, Action onComplete = null)
        {
            return FastTweener.Float(target.position.y, endValue,
                duration, x => target.position = new Vector3(target.position.x, x, target.position.z), ease, onComplete);
        }
        
        //ignoreTimescale onComplete
        public static FastTween TweenMoveY(this Rigidbody target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            return FastTweener.Float(target.position.y, endValue,
                duration, x => target.position = new Vector3(target.position.x, x, target.position.z), ignoreTimescale, onComplete);
        }
        
        //onComplete
        public static FastTween TweenMoveY(this Rigidbody target, float endValue, float duration, Action onComplete)
        {
            return FastTweener.Float(target.position.y, endValue,
                duration, x => target.position = new Vector3(target.position.x, x, target.position.z), onComplete);
        }
        
        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenMoveZ(this Rigidbody target, float endValue, float duration, Ease ease = FastTweener.DEFAULT_EASE, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweener.Float(target.position.z, endValue,
                duration, x => target.position = new Vector3(target.position.x, target.position.y, x), ease, ignoreTimescale, onComplete);
        }
        
        //ease onComplete
        public static FastTween TweenMoveZ(this Rigidbody target, float endValue, float duration, Ease ease, Action onComplete = null)
        {
            return FastTweener.Float(target.position.z, endValue,
                duration, x => target.position = new Vector3(target.position.x, target.position.y, x), ease, onComplete);
        }
        
        //ignoreTimescale onComplete
        public static FastTween TweenMoveZ(this Rigidbody target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            return FastTweener.Float(target.position.z, endValue,
                duration, x => target.position = new Vector3(target.position.x, target.position.y, x), ignoreTimescale, onComplete);
        }
        
        //onComplete
        public static FastTween TweenMoveZ(this Rigidbody target, float endValue, float duration, Action onComplete)
        {
            return FastTweener.Float(target.position.z, endValue,
                duration, x => target.position = new Vector3(target.position.x, target.position.y, x), onComplete);
        }

        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenRotate(this Rigidbody target, Vector3 endValue, float duration, Ease ease = FastTweener.DEFAULT_EASE, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweener.Vector3(target.rotation.eulerAngles, endValue, duration,
                x => target.rotation = Quaternion.Euler(x), ease, ignoreTimescale, onComplete);
        }

        //ease onComplete                
        public static FastTween TweenRotate(this Rigidbody target, Vector3 endValue, float duration, Ease ease, Action onComplete = null)
        {
            return FastTweener.Vector3(target.rotation.eulerAngles, endValue, duration,
                x => target.rotation = Quaternion.Euler(x), ease, onComplete);
        }

        //ignoreTimescale onComplete                
        public static FastTween TweenRotate(this Rigidbody target, Vector3 endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            return FastTweener.Vector3(target.rotation.eulerAngles, endValue, duration,
                x => target.rotation = Quaternion.Euler(x), ignoreTimescale, onComplete);
        }

        //onComplete                
        public static FastTween TweenRotate(this Rigidbody target, Vector3 endValue, float duration, Action onComplete)
        {
            return FastTweener.Vector3(target.rotation.eulerAngles, endValue, duration,
                x => target.rotation = Quaternion.Euler(x), onComplete);
        }
    }
}