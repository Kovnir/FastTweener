using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Kovnir.Tweener.TaskManagment
{
    public class TaskManager
    {
        //here are not constants to allocate memory in the constructor instead of first access. Just for pretty benchmarks
        private static readonly string TASK_POOL_EMPTY = "FastTweener: Task pool is empty! Creating new object.";
        private static readonly string CATCHED_ERROR = "FastTween: Catched an exception in callback: {0}\n{1}";
        
        private readonly Stack<FastTweenTask> tasksPool;
        private readonly List<FastTweenTask> activeTasks;
        private readonly HashSet<uint> killedTasks;

        private uint lastId = 1;
        
        public TaskManager(int size)
        {
            tasksPool = new Stack<FastTweenTask>(size);
            activeTasks = new List<FastTweenTask>(size);
            killedTasks = new HashSet<uint>();
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
        
        public bool SetEase(uint id, Ease ease)
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
            return false;
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
            var unscaledDeltaTime = Time.unscaledDeltaTime;
            var deltaTime = Time.deltaTime;
            for (int i = 0; i < activeTasks.Count; i++)
            {
                var task = activeTasks[i];
                if (killedTasks.Count > 0 && killedTasks.Contains(task.Id))
                {
                    tasksPool.Push(task);
                    activeTasks.RemoveAt(i);
                    i--;
                    continue;
                }

                bool needToRemove = false;
                try
                {
                    needToRemove = task.Proccess(unscaledDeltaTime, deltaTime);
                }
                catch (Exception e)
                {
                    Debug.LogError(String.Format(CATCHED_ERROR, e.Message, e.StackTrace));
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

            if (killedTasks.Count > 0)
            {
                killedTasks.Clear();
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