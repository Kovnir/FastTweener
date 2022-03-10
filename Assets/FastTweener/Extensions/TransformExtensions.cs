using System;
using System.Collections.Generic;
using UnityEngine;
using Kovnir.FastTweener;

namespace Kovnir.FastTweener.Extension
{
    public static class TransformExtensions
    {
        private class TransformExtensionTween
        {
            public Transform Transform;
            
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
                
                LocalMove,
                LocalMoveX,
                LocalMoveY,
                LocalMoveZ,
                
                Rotate,
                LocalRotate,
                
                Scale,
                ScaleX,
                ScaleY,
                ScaleZ 
            }

            public TweenType Type;
            
            public TransformExtensionTween()
            {
                UpdateFloatAction = UpdateInternal;
                UpdateVector3Action = UpdateInternal;
                OnCompleteAction = OnCompleteInternal;
            }
            
            private void UpdateInternal(Vector3 value)
            {
                if (Transform == null)
                {
                    Debug.LogError(string.Format(FastTweenerStringConstants.GAME_OBJECT_WAS_DESTROYED, Name, Type));
                    return;
                }
                switch (Type)
                {
                    case TweenType.Move:
                        Transform.position = value;
                        break;
                    case TweenType.LocalMove:
                        Transform.localPosition = value;
                        break;
                    case TweenType.Scale:
                        Transform.localScale = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            private void UpdateInternal(float value)
            {
                if (Transform == null)
                {
                    Debug.LogError("TransformExtensions: Transform is null! Name: " + Name + "; Type " + Type);
                    return;
                }
                switch (Type)
                {
                    case TweenType.MoveX:
                        Transform.position = new Vector3(value, Transform.position.y, Transform.position.z);
                        break;
                    case TweenType.MoveY:
                        Transform.position = new Vector3(Transform.position.x, value, Transform.position.z);
                        break;
                    case TweenType.MoveZ:
                        Transform.position = new Vector3(Transform.position.x, Transform.position.y, value);
                        break;
                    case TweenType.LocalMoveX:
                        Transform.localPosition = new Vector3(value, Transform.localPosition.y, Transform.localPosition.z);
                        break;
                    case TweenType.LocalMoveY:
                        Transform.localPosition = new Vector3(Transform.localPosition.x, value, Transform.localPosition.z);
                        break;
                    case TweenType.LocalMoveZ:
                        Transform.localPosition = new Vector3(value, Transform.localPosition.y, Transform.localPosition.z);
                        break;
                    case TweenType.ScaleX:
                        Transform.localScale = new Vector3(value, Transform.localScale.y, Transform.localScale.z);
                        break;
                    case TweenType.ScaleY:
                        Transform.localScale = new Vector3(Transform.localScale.x, value, Transform.localScale.z);
                        break;
                    case TweenType.ScaleZ:
                        Transform.localScale = new Vector3(Transform.localScale.x, Transform.localScale.y, value);
                        break;
                    case TweenType.Rotate:
                        Transform.rotation = Quaternion.Lerp(startQuaternion, endQuaternion, value);
                        break;
                    case TweenType.LocalRotate:
                        Transform.localRotation = Quaternion.Lerp(startQuaternion, endQuaternion, value);
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
                        Debug.LogError(String.Format(FastTweenerStringConstants.ON_COMPLETE_EXCEPTION, e.Message, e.StackTrace));
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

        private static Stack<TransformExtensionTween> pool;

        private static TransformExtensionTween Pop(Transform transform, TransformExtensionTween.TweenType type,
            Action onComplete)
        {
            TransformExtensionTween toReturn;
            if (pool == null)
            {
                pool = new Stack<TransformExtensionTween>();
                for (int i = 0; i < FastTweener.Setting.TransformExtensionsPoolSize; i++)
                {
                    pool.Push(new TransformExtensionTween());
                }
            }
            if (pool.Count > 0)
            {
                toReturn = pool.Pop();
            }
            else
            {
                toReturn = new TransformExtensionTween();
            }

            toReturn.Transform = transform;
            toReturn.Type = type;
            
            if (FastTweener.Setting.SaveGameObjectName)
            {
                toReturn.Name = transform.name;
            }
            else
            {
                toReturn.Name = FastTweenerStringConstants.DEFAULT_NAME;                
            }

            toReturn.OnComplete = onComplete;
            return toReturn;
        }

        //ease ignoreTimescale onComplete        
        public static FastTween TweenMove(this Transform target, Vector3 endValue, float duration, Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.Move, onComplete);
            return FastTweener.Vector3(target.position, endValue, duration, tween.UpdateVector3Action, ease, ignoreTimescale, tween.OnCompleteAction);
        }

        //ease onComplete
        public static FastTween TweenMove(this Transform target, Vector3 endValue, float duration, Ease ease, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.Move, onComplete);
            return FastTweener.Vector3(target.position, endValue, duration, tween.UpdateVector3Action, ease, tween.OnCompleteAction);
        }
        
        //ignoreTimescale onComplete
        public static FastTween TweenMove(this Transform target, Vector3 endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.Move, onComplete);
            return FastTweener.Vector3(target.position, endValue, duration, tween.UpdateVector3Action, ignoreTimescale, tween.OnCompleteAction);
        }

        //onComplete
        public static FastTween TweenMove(this Transform target, Vector3 endValue, float duration, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.Move, onComplete);
            return FastTweener.Vector3(target.position, endValue, duration, tween.UpdateVector3Action, tween.OnCompleteAction);
        }

        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenMoveX(this Transform target, float endValue, float duration, Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.MoveX, onComplete);
            return FastTweener.Float(target.position.x, endValue,
                duration, tween.UpdateFloatAction, ease, ignoreTimescale, tween.OnCompleteAction);
        }
        
        //ease onComplete
        public static FastTween TweenMoveX(this Transform target, float endValue, float duration, Ease ease, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.MoveX, onComplete);
            return FastTweener.Float(target.position.x, endValue,
                duration, tween.UpdateFloatAction, ease, tween.OnCompleteAction);
        }
        
        //ignoreTimescale onComplete
        public static FastTween TweenMoveX(this Transform target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.MoveX, onComplete);
            return FastTweener.Float(target.position.x, endValue,
                duration, tween.UpdateFloatAction, ignoreTimescale, tween.OnCompleteAction);
        }
        
        //onComplete
        public static FastTween TweenMoveX(this Transform target, float endValue, float duration, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.MoveX, onComplete);
            return FastTweener.Float(target.position.x, endValue,
                duration,tween.UpdateFloatAction, tween.OnCompleteAction);
        }

        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenMoveY(this Transform target, float endValue, float duration, Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.MoveY, onComplete);
            return FastTweener.Float(target.position.y, endValue,
                duration, tween.UpdateFloatAction, ease, ignoreTimescale, tween.OnCompleteAction);
        }
        
        //ease onComplete
        public static FastTween TweenMoveY(this Transform target, float endValue, float duration, Ease ease, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.MoveY, onComplete);
            return FastTweener.Float(target.position.y, endValue,
                duration, tween.UpdateFloatAction, ease, tween.OnCompleteAction);
        }
        
        //ignoreTimescale onComplete
        public static FastTween TweenMoveY(this Transform target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.MoveY, onComplete);
            return FastTweener.Float(target.position.y, endValue,
                duration, tween.UpdateFloatAction, ignoreTimescale, tween.OnCompleteAction);
        }
        
        //onComplete
        public static FastTween TweenMoveY(this Transform target, float endValue, float duration, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.MoveY, onComplete);
            return FastTweener.Float(target.position.y, endValue,
                duration, tween.UpdateFloatAction, tween.OnCompleteAction);
        }
        
        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenMoveZ(this Transform target, float endValue, float duration, Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.MoveZ, onComplete);
            return FastTweener.Float(target.position.z, endValue,
                duration, tween.UpdateFloatAction, ease, ignoreTimescale, tween.OnCompleteAction);
        }
        
        //ease onComplete
        public static FastTween TweenMoveZ(this Transform target, float endValue, float duration, Ease ease, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.MoveZ, onComplete);
            return FastTweener.Float(target.position.z, endValue,
                duration, tween.UpdateFloatAction, ease, tween.OnCompleteAction);
        }
        
        //ignoreTimescale onComplete
        public static FastTween TweenMoveZ(this Transform target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.MoveZ, onComplete);
            return FastTweener.Float(target.position.z, endValue,
                duration, tween.UpdateFloatAction, ignoreTimescale, tween.OnCompleteAction);
        }
        
        //onComplete
        public static FastTween TweenMoveZ(this Transform target, float endValue, float duration, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.MoveZ, onComplete);
            return FastTweener.Float(target.position.z, endValue,
                duration, tween.UpdateFloatAction, tween.OnCompleteAction);
        }

        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenLocalMove(this Transform target, Vector3 endValue, float duration, Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.LocalMove, onComplete);
            return FastTweener.Vector3(target.localPosition, endValue, duration, tween.UpdateVector3Action, ease, ignoreTimescale, tween.OnCompleteAction);
        }

        //ease onComplete                
        public static FastTween TweenLocalMove(this Transform target, Vector3 endValue, float duration, Ease ease, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.LocalMove, onComplete);
            return FastTweener.Vector3(target.localPosition, endValue, duration, tween.UpdateVector3Action, ease, tween.OnCompleteAction);
        }

        //ignoreTimescale onComplete                
        public static FastTween TweenLocalMove(this Transform target, Vector3 endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.LocalMove, onComplete);
            return FastTweener.Vector3(target.localPosition, endValue, duration, tween.UpdateVector3Action, ignoreTimescale, tween.OnCompleteAction);
        }

        //onComplete                
        public static FastTween TweenLocalMove(this Transform target, Vector3 endValue, float duration, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.LocalMove, onComplete);
            return FastTweener.Vector3(target.localPosition, endValue, duration, tween.UpdateVector3Action, tween.OnCompleteAction);
        }


        //ease ignoreTimescale onComplete                
        public static FastTween TweenLocalMoveX(this Transform target, float endValue, float duration, Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.LocalMoveX, onComplete);
            return FastTweener.Float(target.localPosition.x, endValue,
                duration,tween.UpdateFloatAction, ease, ignoreTimescale, tween.OnCompleteAction);
        }
      
        //ease onComplete                
        public static FastTween TweenLocalMoveX(this Transform target, float endValue, float duration, Ease ease, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.LocalMoveX, onComplete);
            return FastTweener.Float(target.localPosition.x, endValue,
                duration, tween.UpdateFloatAction, ease, tween.OnCompleteAction);
        }
      
        //ignoreTimescale onComplete                
        public static FastTween TweenLocalMoveX(this Transform target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.LocalMoveX, onComplete);
            return FastTweener.Float(target.localPosition.x, endValue,
                duration,tween.UpdateFloatAction, ignoreTimescale, tween.OnCompleteAction);
        }
      
        //onComplete                
        public static FastTween TweenLocalMoveX(this Transform target, float endValue, float duration, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.LocalMoveX, onComplete);
            return FastTweener.Float(target.localPosition.x, endValue,
                duration, tween.UpdateFloatAction, tween.OnCompleteAction);
        }

        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenLocalMoveY(this Transform target, float endValue, float duration, Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.LocalMoveY, onComplete);
            return FastTweener.Float(target.localPosition.y, endValue,
                duration, tween.UpdateFloatAction, ease, ignoreTimescale, tween.OnCompleteAction);
        }

        //ease onComplete                
        public static FastTween TweenLocalMoveY(this Transform target, float endValue, float duration, Ease ease, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.LocalMoveY, onComplete);
            return FastTweener.Float(target.localPosition.y, endValue,
                duration, tween.UpdateFloatAction, ease, tween.OnCompleteAction);
        }

        //ignoreTimescale onComplete                
        public static FastTween TweenLocalMoveY(this Transform target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.LocalMoveY, onComplete);
            return FastTweener.Float(target.localPosition.y, endValue,
                duration, tween.UpdateFloatAction, ignoreTimescale, tween.OnCompleteAction);
        }

        //onComplete                
        public static FastTween TweenLocalMoveY(this Transform target, float endValue, float duration, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.LocalMoveY, onComplete);
            return FastTweener.Float(target.localPosition.y, endValue,
                duration, tween.UpdateFloatAction, tween.OnCompleteAction);
        }


        //ease ignoreTimescale onComplete                
        public static FastTween TweenLocalMoveZ(this Transform target, float endValue, float duration, Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.LocalMoveZ, onComplete);
            return FastTweener.Float(target.localPosition.z, endValue,
                duration, tween.UpdateFloatAction, ease, ignoreTimescale, tween.OnCompleteAction);
        }

        //ease onComplete                
        public static FastTween TweenLocalMoveZ(this Transform target, float endValue, float duration, Ease ease, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.LocalMoveZ, onComplete);
            return FastTweener.Float(target.localPosition.z, endValue,
                duration, tween.UpdateFloatAction, ease, tween.OnCompleteAction);
        }

        //ignoreTimescale onComplete                
        public static FastTween TweenLocalMoveZ(this Transform target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.LocalMoveZ, onComplete);
            return FastTweener.Float(target.localPosition.z, endValue,
                duration, tween.UpdateFloatAction, ignoreTimescale, tween.OnCompleteAction);
        }

        //onComplete                
        public static FastTween TweenLocalMoveZ(this Transform target, float endValue, float duration, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.LocalMoveZ, onComplete);
            return FastTweener.Float(target.localPosition.z, endValue,
                duration, tween.UpdateFloatAction, tween.OnCompleteAction);
        }


        //ease ignoreTimescale onComplete                
        public static FastTween TweenRotate(this Transform target, Vector3 endValue, float duration, Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null)
        {
            var start = target.rotation;
            var end = Quaternion.Euler(endValue);
            var tween = Pop(target, TransformExtensionTween.TweenType.Rotate, onComplete);
            tween.SetQuaternion(start, end);
            return FastTweener.Float(0, 1, duration,
                tween.UpdateFloatAction, ease, ignoreTimescale, tween.OnCompleteAction);
        }

        //ease onComplete                
        public static FastTween TweenRotate(this Transform target, Vector3 endValue, float duration, Ease ease, Action onComplete)
        {
            var start = target.rotation;
            var end = Quaternion.Euler(endValue);
            var tween = Pop(target, TransformExtensionTween.TweenType.Rotate, onComplete);
            tween.SetQuaternion(start, end);
            return FastTweener.Float(0, 1, duration,
                tween.UpdateFloatAction, ease, tween.OnCompleteAction);
        }

        //ignoreTimescale onComplete                
        public static FastTween TweenRotate(this Transform target, Vector3 endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            var start = target.rotation;
            var end = Quaternion.Euler(endValue);
            var tween = Pop(target, TransformExtensionTween.TweenType.Rotate, onComplete);
            tween.SetQuaternion(start, end);
            return FastTweener.Float(0, 1, duration,
                tween.UpdateFloatAction, ignoreTimescale, tween.OnCompleteAction);
        }

        //onComplete                
        public static FastTween TweenRotate(this Transform target, Vector3 endValue, float duration, Action onComplete)
        {
            var start = target.rotation;
            var end = Quaternion.Euler(endValue);
            var tween = Pop(target, TransformExtensionTween.TweenType.Rotate, onComplete);
            tween.SetQuaternion(start, end);
            return FastTweener.Float(0, 1, duration,
                tween.UpdateFloatAction, tween.OnCompleteAction);
        }

        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenLocalRotate(this Transform target, Vector3 endValue, float duration, Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null)
        {
            var start = target.localRotation;
            var end = Quaternion.Euler(endValue);
            var tween = Pop(target, TransformExtensionTween.TweenType.LocalRotate, onComplete);
            tween.SetQuaternion(start, end);
            return FastTweener.Float(0, 1, duration,
                tween.UpdateFloatAction, ease, ignoreTimescale, tween.OnCompleteAction);
        }

        //ease onComplete                
        public static FastTween TweenLocalRotate(this Transform target, Vector3 endValue, float duration, Ease ease, Action onComplete)
        {
            var start = target.localRotation;
            var end = Quaternion.Euler(endValue);
            var tween = Pop(target, TransformExtensionTween.TweenType.LocalRotate, onComplete);
            tween.SetQuaternion(start, end);
            return FastTweener.Float(0 ,1 , duration,
                tween.UpdateFloatAction, ease, tween.OnCompleteAction);
        }

        //ignoreTimescale onComplete                
        public static FastTween TweenLocalRotate(this Transform target, Vector3 endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            var start = target.localRotation;
            var end = Quaternion.Euler(endValue);
            var tween = Pop(target, TransformExtensionTween.TweenType.LocalRotate, onComplete);
            tween.SetQuaternion(start, end);
            return FastTweener.Float(0, 1, duration,
                tween.UpdateFloatAction, ignoreTimescale, tween.OnCompleteAction);
        }

        //onComplete                
        public static FastTween TweenLocalRotate(this Transform target, Vector3 endValue, float duration, Action onComplete)
        {
            var start = target.localRotation;
            var end = Quaternion.Euler(endValue);
            var tween = Pop(target, TransformExtensionTween.TweenType.LocalRotate, onComplete);
            tween.SetQuaternion(start, end);
            return FastTweener.Float(0, 1, duration,
                tween.UpdateFloatAction, tween.OnCompleteAction);
        }


        //ease ignoreTimescale onComplete                
        public static FastTween TweenScale(this Transform target, Vector3 endValue, float duration, Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.Scale, onComplete);
            return FastTweener.Vector3(target.localScale, endValue, duration, tween.UpdateVector3Action, ease, ignoreTimescale, tween.OnCompleteAction);
        }

        //ease onComplete                
        public static FastTween TweenScale(this Transform target, Vector3 endValue, float duration, Ease ease, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.Scale, onComplete);
            return FastTweener.Vector3(target.localScale, endValue, duration, tween.UpdateVector3Action, ease, tween.OnCompleteAction);
        }

        //ignoreTimescale onComplete                
        public static FastTween TweenScale(this Transform target, Vector3 endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.Scale, onComplete);
            return FastTweener.Vector3(target.localScale, endValue, duration, tween.UpdateVector3Action, ignoreTimescale, tween.OnCompleteAction);
        }

        //onComplete                
        public static FastTween TweenScale(this Transform target, Vector3 endValue, float duration, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.Scale, onComplete);
            return FastTweener.Vector3(target.localScale, endValue, duration, tween.UpdateVector3Action, tween.OnCompleteAction);
        }


        //ease ignoreTimescale onComplete                
        public static FastTween TweenScale(this Transform target, float endValue, float duration, Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.Scale, onComplete);
            var newVector = new Vector3(endValue, endValue, endValue);
            return FastTweener.Vector3(target.localScale, newVector, duration, tween.UpdateVector3Action, ease, ignoreTimescale, tween.OnCompleteAction);
        }

        //ease onComplete                
        public static FastTween TweenScale(this Transform target, float endValue, float duration, Ease ease, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.Scale, onComplete);
            var newVector = new Vector3(endValue, endValue, endValue);
            return FastTweener.Vector3(target.localScale, newVector, duration, tween.UpdateVector3Action, ease,
                tween.OnCompleteAction);
        }

        //ignoreTimescale onComplete                
        public static FastTween TweenScale(this Transform target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.Scale, onComplete);
            var newVector = new Vector3(endValue, endValue, endValue);
            return FastTweener.Vector3(target.localScale, newVector, duration, tween.UpdateVector3Action, ignoreTimescale, tween.OnCompleteAction);
        }

        //onComplete                
        public static FastTween TweenScale(this Transform target, float endValue, float duration, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.Scale, onComplete);
            var newVector = new Vector3(endValue, endValue, endValue);
            return FastTweener.Vector3(target.localScale, newVector, duration, tween.UpdateVector3Action, tween.OnCompleteAction);
        }

        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenScaleX(this Transform target, float endValue, float duration, Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.ScaleX, onComplete);
            return FastTweener.Float(target.localScale.x, endValue,
                duration, tween.UpdateFloatAction, ease, ignoreTimescale, tween.OnCompleteAction);
        }
        
        //ease onComplete                
        public static FastTween TweenScaleX(this Transform target, float endValue, float duration, Ease ease, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.ScaleX, onComplete);
            return FastTweener.Float(target.localScale.x, endValue,
                duration, tween.UpdateFloatAction, ease,
                tween.OnCompleteAction);
        }
        
        //ignoreTimescale onComplete                
        public static FastTween TweenScaleX(this Transform target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.ScaleX, onComplete);
            return FastTweener.Float(target.localScale.x, endValue,
                duration,tween.UpdateFloatAction, ignoreTimescale, tween.OnCompleteAction);
        }
        
        //onComplete                
        public static FastTween TweenScaleX(this Transform target, float endValue, float duration, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.ScaleX, onComplete);
            return FastTweener.Float(target.localScale.x, endValue,
                duration,tween.UpdateFloatAction, tween.OnCompleteAction);
        }

        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenScaleY(this Transform target, float endValue, float duration, Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.ScaleY, onComplete);
            return FastTweener.Float(target.localScale.y, endValue,
                duration, tween.UpdateFloatAction, ease, ignoreTimescale, tween.OnCompleteAction);
        }

        //ease onComplete                
        public static FastTween TweenScaleY(this Transform target, float endValue, float duration, Ease ease, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.ScaleY, onComplete);
            return FastTweener.Float(target.localScale.y, endValue,
                duration, tween.UpdateFloatAction, ease, tween.OnCompleteAction);
        }

        //ignoreTimescale onComplete                
        public static FastTween TweenScaleY(this Transform target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.ScaleY, onComplete);
            return FastTweener.Float(target.localScale.y, endValue,
                duration, tween.UpdateFloatAction, ignoreTimescale, tween.OnCompleteAction);
        }

        //onComplete                
        public static FastTween TweenScaleY(this Transform target, float endValue, float duration, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.ScaleY, onComplete);
            return FastTweener.Float(target.localScale.y, endValue,
                duration, tween.UpdateFloatAction, tween.OnCompleteAction);
        }

        
        //ease ignoreTimescale onComplete                
        public static FastTween TweenScaleZ(this Transform target, float endValue, float duration, Ease ease = Ease.Default, bool ignoreTimescale = false, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.ScaleZ, onComplete);
            return FastTweener.Float(target.localScale.z, endValue,
                duration, tween.UpdateFloatAction, ease, ignoreTimescale, tween.OnCompleteAction);
        }
        
        //ease onComplete                
        public static FastTween TweenScaleZ(this Transform target, float endValue, float duration, Ease ease, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.ScaleZ, onComplete);
            return FastTweener.Float(target.localScale.z, endValue,
                duration, tween.UpdateFloatAction, ease, tween.OnCompleteAction);
        }
        
        //ignoreTimescale onComplete                
        public static FastTween TweenScaleZ(this Transform target, float endValue, float duration, bool ignoreTimescale, Action onComplete = null)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.ScaleZ, onComplete);
            return FastTweener.Float(target.localScale.z, endValue,
                duration, tween.UpdateFloatAction, ignoreTimescale, tween.OnCompleteAction);
        }
        
        //onComplete                
        public static FastTween TweenScaleZ(this Transform target, float endValue, float duration, Action onComplete)
        {
            var tween = Pop(target, TransformExtensionTween.TweenType.ScaleZ, onComplete);
            return FastTweener.Float(target.localScale.z, endValue,
                duration, tween.UpdateFloatAction, tween.OnCompleteAction);
        }
    }
}