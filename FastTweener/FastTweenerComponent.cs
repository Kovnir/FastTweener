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

        private int ScheduleInternal(float delay, Action callback, bool ignoreTimescale)
        {
            if (callback == null)
            {
                Debug.LogError(CALLBACK_NULL);
                return 0;
            }
            var task = taskManager.Pop();
            task.SetDelayCall(delay, callback, ignoreTimescale);
            return task.Id;
        }
        
        private int FloatInternal(float start, float end, float duration, Action<float> callback, Ease ease, bool ignoreTimescale, Action onComplete)
        {
            if (callback == null)
            {
                Debug.LogError(CALLBACK_NULL);
                return 0;
            }
            var task = taskManager.Pop();
            task.SetFloat(start, end, duration, callback, ease, ignoreTimescale, onComplete);
            return task.Id;
        }
        
        private int Vector3Internal(Vector3 start, Vector3 end, float duration, Action<Vector3> callback, Ease ease, bool ignoreTimescale, Action onComplete)
        {
            if (callback == null)
            {
                Debug.LogError(CALLBACK_NULL);
                return 0;
            }
            var task = taskManager.Pop();
            task.SetVector3(start, end, duration, callback, ease, ignoreTimescale, onComplete);
            return task.Id;
        }

        private void CancelInternal(int id)
        {
            taskManager.Cancel(id);
        }
        
        private void Update()
        {
            taskManager.Process();
        }

        public static int Schedule(float delay, Action callback, bool ignoreTimescale)
        {
            Init();
            return instance.ScheduleInternal(delay, callback, ignoreTimescale);
        }
        
        public static int Float(float start, float end, float duration, Action<float> callback, Ease ease, bool ignoreTimescale, Action onComplete)
        {
            Init();
            return instance.FloatInternal(start, end, duration, callback, ease, ignoreTimescale, onComplete);
        }
        
        public static int Vector3(Vector3 start, Vector3 end, float duration, Action<Vector3> callback, Ease ease, bool ignoreTimescale, Action onComplete)
        {
            Init();
            return instance.Vector3Internal(start, end, duration, callback, ease, ignoreTimescale, onComplete);
        }

        public static void Cancel(int id)
        {
            if (instance != null)
            {
                instance.CancelInternal(id);
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