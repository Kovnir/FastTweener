using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Kovnir.Tweener.TaskManagment
{
    public class TaskManager<T> where T : ITask, new()
    {
        //here are not constants to allocate memory in the constructor instead of first access. Just for pretty benchmarks
        private static readonly string TASK_POOL_EMPTY = "FastTweener: Task pool is empty! Creating new object.";
        private static readonly string CATCHED_ERROR = "FastTween: Catched an exception in callback: {0}\n{1}";
        
        private readonly Stack<T> tasksPool;
        private readonly List<T> activeTasks;
        private readonly HashSet<int> cancelledTasks;
        private readonly Func<T, bool> process;

        private int lastId = 0;
        
        public TaskManager(int size, Func<T, bool> process)
        {
            tasksPool = new Stack<T>(size);
            activeTasks = new List<T>(size);
            cancelledTasks = new HashSet<int>();
            this.process = process;
            for (int i = 0; i < size; i++)
            {
                tasksPool.Push(new T());
            }
        }

        public void Cancel(int id)
        {
            if (!cancelledTasks.Contains(id))
            {
                cancelledTasks.Add(id);
            }
        }

        public T Set()
        {
            int id = lastId;
            lastId++;
            T task;
            if (tasksPool.Count == 0)
            {
                task = new T();
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
                    needToRemove = process(task);
                }
                catch (Exception e)
                {
                    Debug.LogError(String.Format(CATCHED_ERROR, e.Message, e.StackTrace));
                }

                if (needToRemove)
                {
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