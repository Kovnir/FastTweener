using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Kovnir.FastTweener.TaskManagment
{
    public class TaskManager
    {
        //here are not constants to allocate memory in the constructor instead of first access. Just for pretty benchmarks
        private static readonly string TASK_POOL_EMPTY = "FastTweener: Task pool is empty! Creating new object.";
        private static readonly string CATCHED_ERROR = "FastTweener: Exception caught in callback: {0}\n{1}";

        private readonly Stack<FastTweenTask> tasksPool;
        private readonly List<FastTweenTask> activeTasks;
        private HashSet<uint> killedTasks;
        private HashSet<uint> killedTasksSecond;

        private uint lastId = 1;

        public TaskManager(int size)
        {
            tasksPool = new Stack<FastTweenTask>(size);
            activeTasks = new List<FastTweenTask>(size);
            killedTasks = new HashSet<uint>();
            killedTasksSecond = new HashSet<uint>();
            for (int i = 0; i < size; i++)
            {
                tasksPool.Push(new FastTweenTask());
            }
        }

        public void Kill(uint id)
        {
            if (!killedTasks.Contains(id))
            {
                killedTasks.Add(id);
            }
        }

        public bool IsActive(uint id)
        {
            if (!killedTasks.Contains(id))
            {
                for (int i = 0; i < activeTasks.Count; i++)
                {
                    if (activeTasks[i].Id == id)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void SetEase(uint id, Ease ease)
        {
            if (!killedTasks.Contains(id))
            {
                for (int i = 0; i < activeTasks.Count; i++)
                {
                    if (activeTasks[i].Id == id)
                    {
                        activeTasks[i].Ease = ease;
                    }
                }
            }
        }

        public Ease GetEase(uint id)
        {
            for (int i = 0; i < activeTasks.Count; i++)
            {
                if (activeTasks[i].Id == id)
                {
                    return activeTasks[i].Ease;
                }
            }
            return FastTweener.Setting.DefaultEase;
        }

        public void SetIgnoreTimeScale(uint id, bool ignoreTimeScale)
        {
            if (!killedTasks.Contains(id))
            {
                for (int i = 0; i < activeTasks.Count; i++)
                {
                    if (activeTasks[i].Id == id)
                    {
                        activeTasks[i].IgnoreTimescale = ignoreTimeScale;
                    }
                }
            }
        }

        public bool GetIgnoreTimeScale(uint id)
        {
            for (int i = 0; i < activeTasks.Count; i++)
            {
                if (activeTasks[i].Id == id)
                {
                    return activeTasks[i].IgnoreTimescale;
                }
            }
            return false;
        }

        public void SetOnComplete(uint id, Action onComplete)
        {
            if (!killedTasks.Contains(id))
            {
                for (int i = 0; i < activeTasks.Count; i++)
                {
                    if (activeTasks[i].Id == id)
                    {
                        activeTasks[i].OnComplete = onComplete;
                    }
                }
            }
        }

        public FastTweenTask Pop()
        {
            uint id = lastId;
            lastId++;
            FastTweenTask task;
            if (tasksPool.Count == 0)
            {
                task = new FastTweenTask();
                Debug.LogWarning(TASK_POOL_EMPTY);
            }
            else
            {
                task = tasksPool.Pop();
            }

            task.Id = id;
            activeTasks.Add(task);
            return task;
        }

        public void Process()
        {
            var buf = killedTasks;
            killedTasks = killedTasksSecond; //clean hashset
            killedTasksSecond = buf; //task we killed in current iteration
            
            var unscaledDeltaTime = Time.unscaledDeltaTime;
            var deltaTime = Time.deltaTime;
            for (int i = 0; i < activeTasks.Count; i++)
            {
                var task = activeTasks[i];
                if (killedTasksSecond.Count > 0 && killedTasksSecond.Contains(task.Id))
                {
                    tasksPool.Push(task);
                    activeTasks.RemoveAt(i);
                    i--;
                    continue;
                }

                Exception exception;
                bool needToRemove = task.Proccess(unscaledDeltaTime, deltaTime, out exception);
                if (exception != null)
                {
                    Debug.LogError(String.Format(CATCHED_ERROR, exception.Message, exception.StackTrace));
                }

                if (needToRemove)
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

                    tasksPool.Push(task);
                    activeTasks.RemoveAt(i);
                    i--;
                }
            }

            if (killedTasksSecond.Count > 0)
            {
                killedTasksSecond.Clear();
            }
        }

        public int GetTasksInPoolCount()
        {
            return tasksPool.Count;
        }

        public int GetActiveTasksCount()
        {
            return activeTasks.Count;
        }
    }
}