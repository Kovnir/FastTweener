using System;
using System.Globalization;
using Kovnir.Tweener.TaskManagment;
using UnityEngine;

namespace Kovnir.Tweener
{
    public class FastTweenerComponent : MonoBehaviour
    {
        //here are not constants to allocate memory in the constructor instead of first access. Just for pretty benchmarks
        private static readonly string SCHEDULE_CALLBACK_NULL = "FastTweener: Schedule callback is null!";
        private static readonly string FLOAT_CALLBACK_NULL = "FastTweener: Float callback is null!";
        private static readonly string TASK_LATE = "FastTweener: Low fps. Scheduled task late: ";
        private static readonly string CATCHED_ERROR = "FastTween: Catched an exception in OnComplete callback: {0}\n{1}";

        private static TaskManager<SchedutedTask> scheduling;
        private static TaskManager<FloatTask> floatTasks;

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
            scheduling = new TaskManager<SchedutedTask>(poolSize, ProcessScheduling); 
            floatTasks = new TaskManager<FloatTask>(poolSize, ProcessFloating); 
        }

        private int ScheduleInternal(float delay, Action callback, bool ignoreTimescale)
        {
            if (callback == null)
            {
                Debug.LogError(SCHEDULE_CALLBACK_NULL);
                return 0;
            }
            var task = scheduling.Set();
            task.Set(delay, callback, ignoreTimescale);
            return task.Id;
        }
        
        private void UnscheduleInternal(int id)
        {
            scheduling.Cancel(id);
        }

        private int FloatInternal(float start, float end, float duration, Action<float> callback, Ease ease, bool ignoreTimescale, Action onComplete)
        {
            if (callback == null)
            {
                Debug.LogError(FLOAT_CALLBACK_NULL);
                return 0;
            }
            var task = floatTasks.Set();
            task.Set(start, end, duration, callback, ease, ignoreTimescale, onComplete);
            return task.Id;
        }

        private void CancelFloatInternal(int id)
        {
            floatTasks.Cancel(id);
        }


        private static float unscaledDeltaTime;
        private static float deltaTime;
        
        private void Update()
        {
            unscaledDeltaTime = Time.unscaledDeltaTime;
            deltaTime = Time.deltaTime;
            scheduling.Process();
            floatTasks.Process();
        }

        private static bool ProcessScheduling(SchedutedTask task)
        {
            task.Delay -= task.IgnoreTimescale ? unscaledDeltaTime : deltaTime;
            if (task.Delay <= 0)
            {
                if (task.Delay < -1f/30f)
                {
                    Debug.LogWarning(TASK_LATE + task.Delay.ToString(CultureInfo.InvariantCulture));
                }

                if (task.Action != null)
                {
                    task.Action();
                }

                return true;
            }

            return false;
        }

        private static bool ProcessFloating(FloatTask task)
        {
            if (task.Proccess(unscaledDeltaTime, deltaTime))
            {
                if (task.OnComplete != null)
                {
                    try
                    {
                        task.OnComplete();
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(String.Format(CATCHED_ERROR, e.Message, e.StackTrace));
                    }
                }

                return true;
            }
            return false;
        }

        public static int Schedule(float delay, Action callback, bool ignoreTimescale)
        {
            Init();
            return instance.ScheduleInternal(delay, callback, ignoreTimescale);
        }

        public static void Unschedule(int id)
        {
            if (instance != null)
            {
                instance.UnscheduleInternal(id);
            }
        }

        public static int Float(float start, float end, float duration, Action<float> callback, Ease ease, bool ignoreTimescale, Action onComplete)
        {
            Init();
            return instance.FloatInternal(start, end, duration, callback, ease, ignoreTimescale, onComplete);
        }

        public static void CancelFloat(int id)
        {
            if (instance != null)
            {
                instance.CancelFloatInternal(id);
            }
        }

        //for editor
        public static void GetEditorData(out bool inited, out int schedulingTasksInPool, out int activeSchedulingTask, out int floatTasksInPool, out int activeFloatTasks)
        {
            inited = instance != null;
            if (inited)
            {
                schedulingTasksInPool = scheduling.GetTasksInPoolCount();
                activeSchedulingTask = scheduling.GetActiveTasksCount();
                floatTasksInPool = floatTasks.GetTasksInPoolCount();
                activeFloatTasks = floatTasks.GetActiveTasksCount();
            }
            else
            {
                schedulingTasksInPool = 0;
                activeSchedulingTask = 0;
                floatTasksInPool = 0;
                activeFloatTasks = 0;                
            }
        }
    }
}