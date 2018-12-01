using System;
using System.Globalization;
using Kovnir.Tweener.TaskManagment;
using UnityEngine;

namespace Kovnir.Tweener
{
    public class FastTweenerComponent : MonoBehaviour
    {
        //here are not constants to allocate memory in the constructor instead of first access. Just for pretty benchmarks
        private static readonly string CALLBACK_NULL = "FastTweener: Callback is null!";
        private static readonly string CATCHED_ERROR = "FastTween: Catched an exception in OnComplete callback: {0}\n{1}";

        private static TaskManager taskManager;

        private static FastTweenerComponent instance;

        public static void Init(int poolSize = FastTweener.START_TASK_LIST_SIZE)
        {
            if (instance != null)
            {
                return;
            }
            instance = new GameObject().AddComponent<FastTweenerComponent>();
            instance.name = "FastTweener";
            DontDestroyOnLoad(instance);
            taskManager = new TaskManager(poolSize); 
        }

        private FastTween ScheduleInternal(float delay, Action callback, bool ignoreTimescale)
        {
            if (callback == null)
            {
                Debug.LogError(CALLBACK_NULL);
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
                Debug.LogError(CALLBACK_NULL);
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
                Debug.LogError(CALLBACK_NULL);
                return new FastTween();
            }
            var task = taskManager.Pop();
            task.SetVector3(start, end, duration, callback, ease, ignoreTimescale, onComplete);
            return new FastTween(task.Id);
        }

        private void KillInternal(FastTween tween)
        {
            taskManager.Kill(tween.Id);
        }
        
        private bool IsActiveInternal(FastTween tween)
        {
            return taskManager.IsActive(tween.Id);
        }
        
        private bool SetEaseInternal(FastTween tween, Ease ease)
        {
            return taskManager.SetEase(tween.Id, ease);
        }
        
        private void Update()
        {
            taskManager.Process();
        }
        

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

        public static void SetEase(FastTween fastTween, Ease ease)
        {
            if (instance != null)
            {
                instance.SetEaseInternal(fastTween, ease);
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
    }
}