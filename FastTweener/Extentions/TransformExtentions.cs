using System;
using UnityEngine;

namespace Kovnir.Tweener.Extention
{
    public static class TransformExtentions
    {
        //ease ignoreTimescale onComplete        
        public static FastTween TweenMove(this Transform target, Vector3 endValue, float duration, Ease ease = FastTweener.DEFAULT_EASE, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweener.Vector3(target.position, endValue, duration, x => target.position = x, ease, ignoreTimescale, onComplete);
        }

        //ease onComplete
        public static FastTween TweenMove(this Transform target, Vector3 endValue, float duration, Ease ease, Action onComplete = null)
        {
            return FastTweener.Vector3(target.position, endValue, duration, x => target.position = x, ease, onComplete);
        }
        
        //ignoreTimescale onComplete
        public static FastTween TweenMove(this Transform target, Vector3 endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            return FastTweener.Vector3(target.position, endValue, duration, x => target.position = x, ignoreTimescale, onComplete);
        }

        //onComplete
        public static FastTween TweenMove(this Transform target, Vector3 endValue, float duration, Action onComplete)
        {
            return FastTweener.Vector3(target.position, endValue, duration, x => target.position = x, onComplete);
        }

        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenMoveX(this Transform target, float endValue, float duration, Ease ease = FastTweener.DEFAULT_EASE, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweener.Float(target.position.x, endValue,
                duration, x => target.position = new Vector3(x, target.position.y, target.position.z), ease, ignoreTimescale, onComplete);
        }
        
        //ease onComplete
        public static FastTween TweenMoveX(this Transform target, float endValue, float duration, Ease ease, Action onComplete = null)
        {
            return FastTweener.Float(target.position.x, endValue,
                duration, x => target.position = new Vector3(x, target.position.y, target.position.z), ease, onComplete);
        }
        
        //ignoreTimescale onComplete
        public static FastTween TweenMoveX(this Transform target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            return FastTweener.Float(target.position.x, endValue,
                duration, x => target.position = new Vector3(x, target.position.y, target.position.z), ignoreTimescale, onComplete);
        }
        
        //onComplete
        public static FastTween TweenMoveX(this Transform target, float endValue, float duration, Action onComplete)
        {
            return FastTweener.Float(target.position.x, endValue,
                duration, x => target.position = new Vector3(x, target.position.y, target.position.z), onComplete);
        }

        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenMoveY(this Transform target, float endValue, float duration, Ease ease = FastTweener.DEFAULT_EASE, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweener.Float(target.position.y, endValue,
                duration, x => target.position = new Vector3(target.position.x, x, target.position.z), ease, ignoreTimescale, onComplete);
        }
        
        //ease onComplete
        public static FastTween TweenMoveY(this Transform target, float endValue, float duration, Ease ease, Action onComplete = null)
        {
            return FastTweener.Float(target.position.y, endValue,
                duration, x => target.position = new Vector3(target.position.x, x, target.position.z), ease, onComplete);
        }
        
        //ignoreTimescale onComplete
        public static FastTween TweenMoveY(this Transform target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            return FastTweener.Float(target.position.y, endValue,
                duration, x => target.position = new Vector3(target.position.x, x, target.position.z), ignoreTimescale, onComplete);
        }
        
        //onComplete
        public static FastTween TweenMoveY(this Transform target, float endValue, float duration, Action onComplete)
        {
            return FastTweener.Float(target.position.y, endValue,
                duration, x => target.position = new Vector3(target.position.x, x, target.position.z), onComplete);
        }
        
        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenMoveZ(this Transform target, float endValue, float duration, Ease ease = FastTweener.DEFAULT_EASE, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweener.Float(target.position.z, endValue,
                duration, x => target.position = new Vector3(target.position.x, target.position.y, x), ease, ignoreTimescale, onComplete);
        }
        
        //ease onComplete
        public static FastTween TweenMoveZ(this Transform target, float endValue, float duration, Ease ease, Action onComplete = null)
        {
            return FastTweener.Float(target.position.z, endValue,
                duration, x => target.position = new Vector3(target.position.x, target.position.y, x), ease, onComplete);
        }
        
        //ignoreTimescale onComplete
        public static FastTween TweenMoveZ(this Transform target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            return FastTweener.Float(target.position.z, endValue,
                duration, x => target.position = new Vector3(target.position.x, target.position.y, x), ignoreTimescale, onComplete);
        }
        
        //onComplete
        public static FastTween TweenMoveZ(this Transform target, float endValue, float duration, Action onComplete)
        {
            return FastTweener.Float(target.position.z, endValue,
                duration, x => target.position = new Vector3(target.position.x, target.position.y, x), onComplete);
        }

        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenLocalMove(this Transform target, Vector3 endValue, float duration, Ease ease = FastTweener.DEFAULT_EASE, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweener.Vector3(target.localPosition, endValue, duration, x => target.localPosition = x, ease, ignoreTimescale, onComplete);
        }

        //ease onComplete                
        public static FastTween TweenLocalMove(this Transform target, Vector3 endValue, float duration, Ease ease, Action onComplete = null)
        {
            return FastTweener.Vector3(target.localPosition, endValue, duration, x => target.localPosition = x, ease, onComplete);
        }

        //ignoreTimescale onComplete                
        public static FastTween TweenLocalMove(this Transform target, Vector3 endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            return FastTweener.Vector3(target.localPosition, endValue, duration, x => target.localPosition = x, ignoreTimescale, onComplete);
        }

        //onComplete                
        public static FastTween TweenLocalMove(this Transform target, Vector3 endValue, float duration, Action onComplete)
        {
            return FastTweener.Vector3(target.localPosition, endValue, duration, x => target.localPosition = x, onComplete);
        }


        //ease ignoreTimescale onComplete                
        public static FastTween TweenLocalMoveX(this Transform target, float endValue, float duration, Ease ease = FastTweener.DEFAULT_EASE, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweener.Float(target.localPosition.x, endValue,
                duration, x => target.localPosition = new Vector3(x, target.localPosition.y, target.localPosition.z), ease, ignoreTimescale, onComplete);
        }
      
        //ease onComplete                
        public static FastTween TweenLocalMoveX(this Transform target, float endValue, float duration, Ease ease, Action onComplete = null)
        {
            return FastTweener.Float(target.localPosition.x, endValue,
                duration, x => target.localPosition = new Vector3(x, target.localPosition.y, target.localPosition.z), ease, onComplete);
        }
      
        //ignoreTimescale onComplete                
        public static FastTween TweenLocalMoveX(this Transform target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            return FastTweener.Float(target.localPosition.x, endValue,
                duration, x => target.localPosition = new Vector3(x, target.localPosition.y, target.localPosition.z), ignoreTimescale, onComplete);
        }
      
        //onComplete                
        public static FastTween TweenLocalMoveX(this Transform target, float endValue, float duration, Action onComplete)
        {
            return FastTweener.Float(target.localPosition.x, endValue,
                duration, x => target.localPosition = new Vector3(x, target.localPosition.y, target.localPosition.z), onComplete);
        }

        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenLocalMoveY(this Transform target, float endValue, float duration, Ease ease = FastTweener.DEFAULT_EASE, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweener.Float(target.localPosition.y, endValue,
                duration, x => target.localPosition = new Vector3(target.localPosition.x, x, target.localPosition.z), ease, ignoreTimescale, onComplete);
        }

        //ease onComplete                
        public static FastTween TweenLocalMoveY(this Transform target, float endValue, float duration, Ease ease, Action onComplete = null)
        {
            return FastTweener.Float(target.localPosition.y, endValue,
                duration, x => target.localPosition = new Vector3(target.localPosition.x, x, target.localPosition.z), ease, onComplete);
        }

        //ignoreTimescale onComplete                
        public static FastTween TweenLocalMoveY(this Transform target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            return FastTweener.Float(target.localPosition.y, endValue,
                duration, x => target.localPosition = new Vector3(target.localPosition.x, x, target.localPosition.z), ignoreTimescale, onComplete);
        }

        //onComplete                
        public static FastTween TweenLocalMoveY(this Transform target, float endValue, float duration, Action onComplete)
        {
            return FastTweener.Float(target.localPosition.y, endValue,
                duration, x => target.localPosition = new Vector3(target.localPosition.x, x, target.localPosition.z), onComplete);
        }


        //ease ignoreTimescale onComplete                
        public static FastTween TweenLocalMoveZ(this Transform target, float endValue, float duration, Ease ease = FastTweener.DEFAULT_EASE, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweener.Float(target.localPosition.z, endValue,
                duration, x => target.localPosition = new Vector3(target.localPosition.x, target.localPosition.y, x), ease, ignoreTimescale, onComplete);
        }

        //ease onComplete                
        public static FastTween TweenLocalMoveZ(this Transform target, float endValue, float duration, Ease ease, Action onComplete = null)
        {
            return FastTweener.Float(target.localPosition.z, endValue,
                duration, x => target.localPosition = new Vector3(target.localPosition.x, target.localPosition.y, x), ease, onComplete);
        }

        //ignoreTimescale onComplete                
        public static FastTween TweenLocalMoveZ(this Transform target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            return FastTweener.Float(target.localPosition.z, endValue,
                duration, x => target.localPosition = new Vector3(target.localPosition.x, target.localPosition.y, x), ignoreTimescale, onComplete);
        }

        //onComplete                
        public static FastTween TweenLocalMoveZ(this Transform target, float endValue, float duration, Action onComplete)
        {
            return FastTweener.Float(target.localPosition.z, endValue,
                duration, x => target.localPosition = new Vector3(target.localPosition.x, target.localPosition.y, x), onComplete);
        }


        //ease ignoreTimescale onComplete                
        public static FastTween TweenRotate(this Transform target, Vector3 endValue, float duration, Ease ease = FastTweener.DEFAULT_EASE, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweener.Vector3(target.rotation.eulerAngles, endValue, duration,
                x => target.rotation = Quaternion.Euler(x), ease, ignoreTimescale, onComplete);
        }

        //ease onComplete                
        public static FastTween TweenRotate(this Transform target, Vector3 endValue, float duration, Ease ease, Action onComplete = null)
        {
            return FastTweener.Vector3(target.rotation.eulerAngles, endValue, duration,
                x => target.rotation = Quaternion.Euler(x), ease, onComplete);
        }

        //ignoreTimescale onComplete                
        public static FastTween TweenRotate(this Transform target, Vector3 endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            return FastTweener.Vector3(target.rotation.eulerAngles, endValue, duration,
                x => target.rotation = Quaternion.Euler(x), ignoreTimescale, onComplete);
        }

        //onComplete                
        public static FastTween TweenRotate(this Transform target, Vector3 endValue, float duration, Action onComplete)
        {
            return FastTweener.Vector3(target.rotation.eulerAngles, endValue, duration,
                x => target.rotation = Quaternion.Euler(x), onComplete);
        }

        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenLocalRotate(this Transform target, Vector3 endValue, float duration, Ease ease = FastTweener.DEFAULT_EASE, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweener.Vector3(target.localEulerAngles, endValue, duration,
                x => target.localRotation = Quaternion.Euler(x), ease, ignoreTimescale, onComplete);
        }

        //ease onComplete                
        public static FastTween TweenLocalRotate(this Transform target, Vector3 endValue, float duration, Ease ease, Action onComplete = null)
        {
            return FastTweener.Vector3(target.localEulerAngles, endValue, duration,
                x => target.localRotation = Quaternion.Euler(x), ease, onComplete);
        }

        //ignoreTimescale onComplete                
        public static FastTween TweenLocalRotate(this Transform target, Vector3 endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            return FastTweener.Vector3(target.localEulerAngles, endValue, duration,
                x => target.localRotation = Quaternion.Euler(x), ignoreTimescale, onComplete);
        }

        //onComplete                
        public static FastTween TweenLocalRotate(this Transform target, Vector3 endValue, float duration, Action onComplete)
        {
            return FastTweener.Vector3(target.localEulerAngles, endValue, duration,
                x => target.localRotation = Quaternion.Euler(x), onComplete);
        }


        //ease ignoreTimescale onComplete                
        public static FastTween TweenScale(this Transform target, Vector3 endValue, float duration, Ease ease = FastTweener.DEFAULT_EASE, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweener.Vector3(target.localScale, endValue, duration, x => target.localScale = x, ease, ignoreTimescale, onComplete);
        }

        //ease onComplete                
        public static FastTween TweenScale(this Transform target, Vector3 endValue, float duration, Ease ease, Action onComplete = null)
        {
            return FastTweener.Vector3(target.localScale, endValue, duration, x => target.localScale = x, ease, onComplete);
        }

        //ignoreTimescale onComplete                
        public static FastTween TweenScale(this Transform target, Vector3 endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            return FastTweener.Vector3(target.localScale, endValue, duration, x => target.localScale = x, ignoreTimescale, onComplete);
        }

        //onComplete                
        public static FastTween TweenScale(this Transform target, Vector3 endValue, float duration, Action onComplete)
        {
            return FastTweener.Vector3(target.localScale, endValue, duration, x => target.localScale = x, onComplete);
        }


        //ease ignoreTimescale onComplete                
        public static FastTween TweenScale(this Transform target, float endValue, float duration, Ease ease = FastTweener.DEFAULT_EASE, bool ignoreTimescale = false, Action onComplete = null)
        {
            var newVector = new Vector3(endValue, endValue, endValue);
            return FastTweener.Vector3(target.localScale, newVector, duration, x => target.localScale = x, ease, ignoreTimescale, onComplete);
        }

        //ease onComplete                
        public static FastTween TweenScale(this Transform target, float endValue, float duration, Ease ease, Action onComplete = null)
        {
            var newVector = new Vector3(endValue, endValue, endValue);
            return FastTweener.Vector3(target.localScale, newVector, duration, x => target.localScale = x, ease,
                onComplete);
        }

        //ignoreTimescale onComplete                
        public static FastTween TweenScale(this Transform target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            var newVector = new Vector3(endValue, endValue, endValue);
            return FastTweener.Vector3(target.localScale, newVector, duration, x => target.localScale = x, ignoreTimescale, onComplete);
        }

        //onComplete                
        public static FastTween TweenScale(this Transform target, float endValue, float duration, Action onComplete)
        {
            var newVector = new Vector3(endValue, endValue, endValue);
            return FastTweener.Vector3(target.localScale, newVector, duration, x => target.localScale = x, onComplete);
        }

        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenScaleX(this Transform target, float endValue, float duration, Ease ease = FastTweener.DEFAULT_EASE, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweener.Float(target.localScale.x, endValue,
                duration, x => target.localScale = new Vector3(x, target.localScale.y, target.localScale.z), ease, ignoreTimescale, onComplete);
        }
        
        //ease onComplete                
        public static FastTween TweenScaleX(this Transform target, float endValue, float duration, Ease ease, Action onComplete = null)
        {
            return FastTweener.Float(target.localScale.x, endValue,
                duration, x => target.localScale = new Vector3(x, target.localScale.y, target.localScale.z), ease,
                onComplete);
        }
        
        //ignoreTimescale onComplete                
        public static FastTween TweenScaleX(this Transform target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            return FastTweener.Float(target.localScale.x, endValue,
                duration, x => target.localScale = new Vector3(x, target.localScale.y, target.localScale.z), ignoreTimescale, onComplete);
        }
        
        //onComplete                
        public static FastTween TweenScaleX(this Transform target, float endValue, float duration, Action onComplete)
        {
            return FastTweener.Float(target.localScale.x, endValue,
                duration, x => target.localScale = new Vector3(x, target.localScale.y, target.localScale.z), onComplete);
        }

        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenScaleY(this Transform target, float endValue, float duration, Ease ease = FastTweener.DEFAULT_EASE, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweener.Float(target.localScale.y, endValue,
                duration, x => target.localScale = new Vector3(target.localScale.x, x, target.localScale.z), ease, ignoreTimescale, onComplete);
        }

        //ease onComplete                
        public static FastTween TweenScaleY(this Transform target, float endValue, float duration, Ease ease, Action onComplete = null)
        {
            return FastTweener.Float(target.localScale.y, endValue,
                duration, x => target.localScale = new Vector3(target.localScale.x, x, target.localScale.z), ease, onComplete);
        }

        //ignoreTimescale onComplete                
        public static FastTween TweenScaleY(this Transform target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            return FastTweener.Float(target.localScale.y, endValue,
                duration, x => target.localScale = new Vector3(target.localScale.x, x, target.localScale.z), ignoreTimescale, onComplete);
        }

        //onComplete                
        public static FastTween TweenScaleY(this Transform target, float endValue, float duration, Action onComplete)
        {
            return FastTweener.Float(target.localScale.y, endValue,
                duration, x => target.localScale = new Vector3(target.localScale.x, x, target.localScale.z), onComplete);
        }

        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenScaleZ(this Transform target, float endValue, float duration, Ease ease = FastTweener.DEFAULT_EASE, bool ignoreTimescale = false, Action onComplete = null)
        {
            return FastTweener.Float(target.localScale.z, endValue,
                duration, x => target.localScale = new Vector3(target.localScale.x, target.localScale.y, x), ease, ignoreTimescale, onComplete);
        }
        
        //ease onComplete                
        public static FastTween TweenScaleZ(this Transform target, float endValue, float duration, Ease ease, Action onComplete = null)
        {
            return FastTweener.Float(target.localScale.z, endValue,
                duration, x => target.localScale = new Vector3(target.localScale.x, target.localScale.y, x), ease, onComplete);
        }
        
        //ignoreTimescale onComplete                
        public static FastTween TweenScaleZ(this Transform target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            return FastTweener.Float(target.localScale.z, endValue,
                duration, x => target.localScale = new Vector3(target.localScale.x, target.localScale.y, x), ignoreTimescale, onComplete);
        }
        
        //onComplete                
        public static FastTween TweenScaleZ(this Transform target, float endValue, float duration, Action onComplete)
        {
            return FastTweener.Float(target.localScale.z, endValue,
                duration, x => target.localScale = new Vector3(target.localScale.x, target.localScale.y, x), onComplete);
        }
    }
}