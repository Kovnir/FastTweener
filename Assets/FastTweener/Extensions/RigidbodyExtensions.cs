using System;
using System.Collections.Generic;
using UnityEngine;
using Kovnir.FastTweener;

namespace Kovnir.FastTweener.Extension
{
    public static class RigidbodyExtensions
    {
        private class RigidbodyExtensionTween
        {
            public Rigidbody Rigidbody;
            
            public Action OnComplete;
            public string Name;
            public Action<float> UpdateFloatAction;
            public Action<Vector3> UpdateVector3Action;
            public Action OnCompleteAction;
            
            public enum TweenType
            {
                Move,
                MoveX,
                MoveY,
                MoveZ,
                
                Rotate,
            }

            public TweenType Type;
            
            public RigidbodyExtensionTween()
            {
                UpdateFloatAction = UpdateInternal;
                UpdateVector3Action = UpdateInternal;
                OnCompleteAction = OnCompleteInternal;
            }
            
            private void UpdateInternal(Vector3 value)
            {
                if (Rigidbody == null)
                {
                    Debug.LogError(FastTweenerStringConstants.RIGIDBODY_IS_NULL);
                    return;
                }
                switch (Type)
                {
                    case TweenType.Move:
                        Rigidbody.position = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            private void UpdateInternal(float value)
            {
                if (Rigidbody == null)
                {
                    Debug.LogError(FastTweenerStringConstants.RIGIDBODY_IS_NULL);
                    return;
                }
                switch (Type)
                {
                    case TweenType.MoveX:
                        Rigidbody.position = new Vector3(value, Rigidbody.position.y, Rigidbody.position.z);
                        break;
                    case TweenType.MoveY:
                        Rigidbody.position = new Vector3(Rigidbody.position.x, value, Rigidbody.position.z);
                        break;
                    case TweenType.MoveZ:
                        Rigidbody.position = new Vector3(Rigidbody.position.x, Rigidbody.position.y, value);
                        break;
                    case TweenType.Rotate:
                        Rigidbody.rotation = Quaternion.Lerp(startQuaternion, endQuaternion, value);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            private void OnCompleteInternal()
            {
                if (OnComplete != null)
                {
                    try
                    {
                        OnComplete();
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(string.Format(FastTweenerStringConstants.ON_COMPLETE_EXCEPTION, e.Message, e.StackTrace));
                    }
                }

                pool.Push(this);
            }

            private Quaternion startQuaternion;
            private Quaternion endQuaternion;
            public void SetQuaternion(Quaternion start, Quaternion end)
            {
                startQuaternion = start;
                endQuaternion = end;
            }
        }

        private static Stack<RigidbodyExtensionTween> pool;

        private static RigidbodyExtensionTween Pop(Rigidbody rigidbody, RigidbodyExtensionTween.TweenType type,
            Action onComplete)
        {
            RigidbodyExtensionTween toReturn;
            if (pool == null)
            {
                pool = new Stack<RigidbodyExtensionTween>();
                for (int i = 0; i < FastTweener.Setting.RigidbodyExtensionsPoolSize; i++)
                {
                    pool.Push(new RigidbodyExtensionTween());
                }
            }
            if (pool.Count > 0)
            {
                toReturn = pool.Pop();
            }
            else
            {
                toReturn = new RigidbodyExtensionTween();
            }

            toReturn.Rigidbody = rigidbody;
            toReturn.Type = type;
            
            if (FastTweener.Setting.SaveGameObjectName)
            {
                toReturn.Name = rigidbody.name;
            }
            else
            {
                toReturn.Name = FastTweenerStringConstants.DEFAULT_NAME;
            }

            toReturn.OnComplete = onComplete;
            return toReturn;
        }
        
        //ease ignoreTimescale onComplete        
        public static FastTween TweenMove(this Rigidbody target, Vector3 endValue, float duration, Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null)
        {
            var tween = Pop(target, RigidbodyExtensionTween.TweenType.Move, onComplete);
            return FastTweener.Vector3(target.position, endValue, duration, tween.UpdateVector3Action, ease, ignoreTimescale, tween.OnCompleteAction);
        }

        //ease onComplete
        public static FastTween TweenMove(this Rigidbody target, Vector3 endValue, float duration, Ease ease, Action onComplete)
        {
            var tween = Pop(target, RigidbodyExtensionTween.TweenType.Move, onComplete);
            return FastTweener.Vector3(target.position, endValue, duration, tween.UpdateVector3Action, ease, tween.OnCompleteAction);
        }
        
        //ignoreTimescale onComplete
        public static FastTween TweenMove(this Rigidbody target, Vector3 endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            var tween = Pop(target, RigidbodyExtensionTween.TweenType.Move, onComplete);
            return FastTweener.Vector3(target.position, endValue, duration, tween.UpdateVector3Action, ignoreTimescale, tween.OnCompleteAction);
        }

        //onComplete
        public static FastTween TweenMove(this Rigidbody target, Vector3 endValue, float duration, Action onComplete)
        {
            var tween = Pop(target, RigidbodyExtensionTween.TweenType.Move, onComplete);
            return FastTweener.Vector3(target.position, endValue, duration, tween.UpdateVector3Action, tween.OnCompleteAction);
        }

        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenMoveX(this Rigidbody target, float endValue, float duration, Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null)
        {
            var tween = Pop(target, RigidbodyExtensionTween.TweenType.MoveX, onComplete);
            return FastTweener.Float(target.position.x, endValue,
                duration, tween.UpdateFloatAction, ease, ignoreTimescale, tween.OnCompleteAction);
        }
        
        //ease onComplete
        public static FastTween TweenMoveX(this Rigidbody target, float endValue, float duration, Ease ease, Action onComplete)
        {
            var tween = Pop(target, RigidbodyExtensionTween.TweenType.MoveX, onComplete);
            return FastTweener.Float(target.position.x, endValue,
                duration, tween.UpdateFloatAction, ease, tween.OnCompleteAction);
        }
        
        //ignoreTimescale onComplete
        public static FastTween TweenMoveX(this Rigidbody target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            var tween = Pop(target, RigidbodyExtensionTween.TweenType.MoveX, onComplete);
            return FastTweener.Float(target.position.x, endValue,
                duration, tween.UpdateFloatAction, ignoreTimescale, tween.OnCompleteAction);
        }
        
        //onComplete
        public static FastTween TweenMoveX(this Rigidbody target, float endValue, float duration, Action onComplete)
        {
            var tween = Pop(target, RigidbodyExtensionTween.TweenType.MoveX, onComplete);
            return FastTweener.Float(target.position.x, endValue,
                duration, tween.UpdateFloatAction, tween.OnCompleteAction);
        }

        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenMoveY(this Rigidbody target, float endValue, float duration, Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null)
        {
            var tween = Pop(target, RigidbodyExtensionTween.TweenType.MoveY, onComplete);
            return FastTweener.Float(target.position.y, endValue,
                duration, tween.UpdateFloatAction, ease, ignoreTimescale, tween.OnCompleteAction);
        }
        
        //ease onComplete
        public static FastTween TweenMoveY(this Rigidbody target, float endValue, float duration, Ease ease, Action onComplete)
        {
            var tween = Pop(target, RigidbodyExtensionTween.TweenType.MoveY, onComplete);
            return FastTweener.Float(target.position.y, endValue,
                duration, tween.UpdateFloatAction, ease, tween.OnCompleteAction);
        }
        
        //ignoreTimescale onComplete
        public static FastTween TweenMoveY(this Rigidbody target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            var tween = Pop(target, RigidbodyExtensionTween.TweenType.MoveY, onComplete);
            return FastTweener.Float(target.position.y, endValue,
                duration, tween.UpdateFloatAction, ignoreTimescale, tween.OnCompleteAction);
        }
        
        //onComplete
        public static FastTween TweenMoveY(this Rigidbody target, float endValue, float duration, Action onComplete)
        {
            var tween = Pop(target, RigidbodyExtensionTween.TweenType.MoveY, onComplete);
            return FastTweener.Float(target.position.y, endValue,
                duration, tween.UpdateFloatAction, tween.OnCompleteAction);
        }
        
        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenMoveZ(this Rigidbody target, float endValue, float duration, Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null)
        {
            var tween = Pop(target, RigidbodyExtensionTween.TweenType.MoveZ, onComplete);
            return FastTweener.Float(target.position.z, endValue,
                duration, tween.UpdateFloatAction, ease, ignoreTimescale, tween.OnCompleteAction);
        }
        
        //ease onComplete
        public static FastTween TweenMoveZ(this Rigidbody target, float endValue, float duration, Ease ease, Action onComplete)
        {
            var tween = Pop(target, RigidbodyExtensionTween.TweenType.MoveZ, onComplete);
            return FastTweener.Float(target.position.z, endValue,
                duration, tween.UpdateFloatAction, ease, tween.OnCompleteAction);
        }
        
        //ignoreTimescale onComplete
        public static FastTween TweenMoveZ(this Rigidbody target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            var tween = Pop(target, RigidbodyExtensionTween.TweenType.MoveZ, onComplete);
            return FastTweener.Float(target.position.z, endValue,
                duration, tween.UpdateFloatAction, ignoreTimescale, tween.OnCompleteAction);
        }
        
        //onComplete
        public static FastTween TweenMoveZ(this Rigidbody target, float endValue, float duration, Action onComplete)
        {
            var tween = Pop(target, RigidbodyExtensionTween.TweenType.MoveZ, onComplete);
            return FastTweener.Float(target.position.z, endValue,
                duration, tween.UpdateFloatAction, tween.OnCompleteAction);
        }

        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenRotate(this Rigidbody target, Vector3 endValue, float duration, Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null)
        {
            var start = target.rotation;
            var end = Quaternion.Euler(endValue);
            var tween = Pop(target, RigidbodyExtensionTween.TweenType.Rotate, onComplete);
            tween.SetQuaternion(start, end);
            return FastTweener.Float(0, 1, duration,
                tween.UpdateFloatAction, ease, ignoreTimescale, tween.OnCompleteAction);
        }

        //ease onComplete                
        public static FastTween TweenRotate(this Rigidbody target, Vector3 endValue, float duration, Ease ease, Action onComplete)
        {
            var start = target.rotation;
            var end = Quaternion.Euler(endValue);
            var tween = Pop(target, RigidbodyExtensionTween.TweenType.Rotate, onComplete);
            tween.SetQuaternion(start, end);
            return FastTweener.Float(0, 1, duration,
                tween.UpdateFloatAction, ease, tween.OnCompleteAction);
        }

        //ignoreTimescale onComplete                
        public static FastTween TweenRotate(this Rigidbody target, Vector3 endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            var start = target.rotation;
            var end = Quaternion.Euler(endValue);
            var tween = Pop(target, RigidbodyExtensionTween.TweenType.Rotate, onComplete);
            tween.SetQuaternion(start, end);
            return FastTweener.Float(0, 1, duration,
                tween.UpdateFloatAction, ignoreTimescale, tween.OnCompleteAction);
        }

        //onComplete                
        public static FastTween TweenRotate(this Rigidbody target, Vector3 endValue, float duration, Action onComplete)
        {
            var start = target.rotation;
            var end = Quaternion.Euler(endValue);
            var tween = Pop(target, RigidbodyExtensionTween.TweenType.Rotate, onComplete);
            tween.SetQuaternion(start, end);
            return FastTweener.Float(0, 1, duration,
                tween.UpdateFloatAction, tween.OnCompleteAction);
        }
    }
}