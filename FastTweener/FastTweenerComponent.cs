using System;
using Kovnir.FastTweener.TaskManagment;
using UnityEngine;

namespace Kovnir.FastTweener
{
    public sealed class FastTweenerComponent : MonoBehaviour
    {
        private static TaskManager taskManager;

        private static FastTweenerComponent instance;

        public static void Init(bool dontDestroyOnLoad = true)
        {
            if (instance != null)
            {
                return;
            }
            Debug.Log("FastTweenerComponent: Init");
            instance = new GameObject().AddComponent<FastTweenerComponent>();
            instance.name = "FastTweener";
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(instance);
            }

            taskManager = new TaskManager(FastTweener.Setting.TaskPoolSize); 
        }

        private FastTween ScheduleInternal(float delay, Action callback, bool ignoreTimescale)
        {
            if (callback == null)
            {
                Debug.LogError(FastTweenerStringConstants.CALLBACK_IS_NULL);
                return new FastTween();
            }
            var task = taskManager.Pop();
            task.SetDelayCall(delay, callback, ignoreTimescale);
            return new FastTween(task.Id);
        }
        
        private FastTween FloatInternal(float start, float end, float duration, Action<float> callback, Ease ease, bool ignoreTimescale, Action onComplete)
        {
            if (callback == null)
            {
                Debug.LogError(FastTweenerStringConstants.CALLBACK_IS_NULL);
                return new FastTween();
            }
            var task = taskManager.Pop();
            task.SetFloat(start, end, duration, callback, ease, ignoreTimescale, onComplete);
            return new FastTween(task.Id);
        }
        
        private FastTween Vector3Internal(Vector3 start, Vector3 end, float duration, Action<Vector3> callback, Ease ease, bool ignoreTimescale, Action onComplete)
        {
            if (callback == null)
            {
                Debug.LogError(FastTweenerStringConstants.CALLBACK_IS_NULL);
                return new FastTween();
            }
            var task = taskManager.Pop();
            task.SetVector3(start, end, duration, callback, ease, ignoreTimescale, onComplete);
            return new FastTween(task.Id);
        }

        private void KillInternal(FastTween tween) 
            => taskManager.Kill(tween.Id);

        private bool IsActiveInternal(FastTween tween) 
            => taskManager.IsActive(tween.Id);

        private void SetEaseInternal(FastTween tween, Ease ease) 
            => taskManager.SetEase(tween.Id, ease);

        private Ease GetEaseInternal(FastTween tween) 
            => taskManager.GetEase(tween.Id);

        private void SetIgnoreTimeScaleInternal(FastTween tween, bool ignoreTimeScale) 
            => taskManager.SetIgnoreTimeScale(tween.Id, ignoreTimeScale);

        private bool GetIgnoreTimeScaleInternal(FastTween tween) 
            => taskManager.GetIgnoreTimeScale(tween.Id);

        private void SetOnCompleteInternal(FastTween tween, Action onComplete) 
            => taskManager.SetOnComplete(tween.Id, onComplete);

        private void Update() => taskManager.Process();


        public static FastTween Schedule(float delay, Action callback, bool ignoreTimescale)
        {
            Init();
            return instance.ScheduleInternal(delay, callback, ignoreTimescale);
        }
        
        public static FastTween Float(float start, float end, float duration, Action<float> callback, Ease ease, bool ignoreTimescale, Action onComplete)
        {
            Init();
            return instance.FloatInternal(start, end, duration, callback, ease, ignoreTimescale, onComplete);
        }
        
        public static FastTween Vector3(Vector3 start, Vector3 end, float duration, Action<Vector3> callback, Ease ease, bool ignoreTimescale, Action onComplete)
        {
            Init();
            return instance.Vector3Internal(start, end, duration, callback, ease, ignoreTimescale, onComplete);
        }

        public static void Kill(FastTween tween)
        {
            if (instance != null)
            {
                instance.KillInternal(tween);
            }
        }

        public static bool IsActive(FastTween tween)
        {
            if (instance != null)
            {
                return instance.IsActiveInternal(tween);
            }
            return false;
        }

        public static void SetEase(FastTween tween, Ease ease)
        {
            if (instance != null)
            {
                instance.SetEaseInternal(tween, ease);
            }
        }
        
        public static Ease GetEase(FastTween tween)
        {
            if (instance != null)
            {
                return instance.GetEaseInternal(tween);
            }
            return FastTweener.Setting.DefaultEase;
        }

        public static void SetIgnoreTimeScale(FastTween tween, bool ignoreTimeScale)
        {
            if (instance != null)
            {
                instance.SetIgnoreTimeScaleInternal(tween, ignoreTimeScale);
            }
        }

        public static bool GetIgnoreTimeScale(FastTween tween)
        {
            if (instance != null)
            {
                return instance.GetIgnoreTimeScaleInternal(tween);
            }
            return false;
        }

        public static void SetOnComplete(FastTween tween, Action onComplete)
        {
            if (instance != null)
            {
                instance.SetOnCompleteInternal(tween, onComplete);
            }
        }

        //for editor
        public static void GetEditorData(out bool inited, out int tasksInPool, out int activeTasks)
        {
            inited = instance != null;
            if (inited)
            {
                tasksInPool = taskManager.GetTasksInPoolCount();
                activeTasks = taskManager.GetActiveTasksCount();
            }
            else
            {
                tasksInPool = 0;
                activeTasks = 0;
            }
        }
        
        public static void Dispose()
        {
            if (instance != null)
            {
                Destroy(instance.gameObject);
                instance = null;
                taskManager = null;
            }
        }
    }
}