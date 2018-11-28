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
        private readonly HashSet<int> cancelledTasks;

        private int lastId = 0;
        
        public TaskManager(int size)
        {
            tasksPool = new Stack<FastTweenTask>(size);
            activeTasks = new List<FastTweenTask>(size);
            cancelledTasks = new HashSet<int>();
            for (int i = 0; i < size; i++)
            {
                tasksPool.Push(new FastTweenTask());
            }
        }

        public void Cancel(int id)
        {
            if (!cancelledTasks.Contains(id))
            {
                cancelledTasks.Add(id);
            }
        }

        public FastTweenTask Pop()
        {
            int id = lastId;
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
                if (cancelledTasks.Count > 0 && cancelledTasks.Contains(task.Id))
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

            if (cancelledTasks.Count > 0)
            {
                cancelledTasks.Clear();
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