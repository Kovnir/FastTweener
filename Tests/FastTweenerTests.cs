using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Kovnir.FastTweener;
using Kovnir.FastTweener.TaskManagment;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Ease = Kovnir.FastTweener.Ease;
using Object = UnityEngine.Object;

namespace FastTweener.Tests
{
    public sealed class FastTweenerTests
    {
        private const int INVALID_TASK_ID = 0;
        private const int DEFAULT_TASK_ID = 1;
        
        private FastTweenerComponent fastTweenerComponent;
        private TaskManager taskManager;

        [SetUp]
        public void Setup()
        {
            Kovnir.FastTweener.FastTweener.Dispose();
            Kovnir.FastTweener.FastTweener.Init(null, false);
            fastTweenerComponent = Object.FindObjectOfType<FastTweenerComponent>();
            FieldInfo myFieldInfo =
                typeof(FastTweenerComponent).GetField("taskManager", BindingFlags.Static | BindingFlags.NonPublic);

            taskManager = (TaskManager) myFieldInfo.GetValue(fastTweenerComponent);
        }

        [UnityTest]
        public IEnumerator PoolSize()
        {
            Assert.AreEqual(16, taskManager.GetTasksInPoolCount());
            Assert.AreEqual(0, taskManager.GetActiveTasksCount());

            List<FastTween> fastTweens = new List<FastTween>();
            for (int i = 0; i < 20; i++)
            {
                fastTweens.Add(Kovnir.FastTweener.FastTweener.Schedule(10, () => { }));
                int countInPool = 16 - (i + 1);
                if (countInPool < 0)
                {
                    countInPool = 0;
                }

                Assert.AreEqual(taskManager.GetTasksInPoolCount(), countInPool, "i == " + i);
                Assert.AreEqual(taskManager.GetActiveTasksCount(), i + 1, "i == " + i);
            }

            for (int i = 0; i < 20; i++)
            {
                fastTweens[i].Kill();
                yield return null; //need to wait frame to process kill task
                Assert.AreEqual(taskManager.GetTasksInPoolCount(), i + 1, "i == " + i);
                Assert.AreEqual(taskManager.GetActiveTasksCount(), 20 - (i + 1), "i == " + i);
            }
        }

        [UnityTest]
        public IEnumerator IgnoreTimeScale()
        {
            Time.timeScale = 1;
            bool done = false;
            Kovnir.FastTweener.FastTweener.Schedule(2, () => done = true, false);
            Assert.False(done);
            yield return new WaitForSecondsRealtime(0.5f);
            Assert.False(done);
            yield return new WaitForSecondsRealtime(0.5f);
            Assert.False(done);
            yield return new WaitForSecondsRealtime(0.5f);
            Assert.False(done);
            yield return new WaitForSecondsRealtime(0.5f);
            Assert.True(done);

            done = false;
            Kovnir.FastTweener.FastTweener.Schedule(2, () => done = true, true);
            Assert.False(done);
            yield return new WaitForSecondsRealtime(0.5f);
            Assert.False(done);
            yield return new WaitForSecondsRealtime(0.5f);
            Assert.False(done);
            yield return new WaitForSecondsRealtime(0.5f);
            Assert.False(done);
            yield return new WaitForSecondsRealtime(0.5f);
            Assert.True(done);

            Time.timeScale = 0;
            done = false;
            var tween = Kovnir.FastTweener.FastTweener.Schedule(2, () => done = true, false);
            Assert.False(done);
            yield return new WaitForSecondsRealtime(3f);
            Assert.False(done);
            tween.Kill();

            done = false;
            Kovnir.FastTweener.FastTweener.Schedule(2, () => done = true, true);
            Assert.False(done);
            yield return new WaitForSecondsRealtime(0.5f);
            Assert.False(done);
            yield return new WaitForSecondsRealtime(0.5f);
            Assert.False(done);
            yield return new WaitForSecondsRealtime(0.5f);
            Assert.False(done);
            yield return new WaitForSecondsRealtime(0.5f);
            Assert.True(done);

            Time.timeScale = 1;
        }

        [Test]
        public void ScheduleNull()
        {
            var inPool = taskManager.GetTasksInPoolCount();
            var alive = taskManager.GetActiveTasksCount();
            LogAssert.Expect(LogType.Error, "FastTweener: Callback is null!");
            var tween = Kovnir.FastTweener.FastTweener.Schedule(1, null);
            Assert.AreEqual(INVALID_TASK_ID, tween.Id);
            Assert.AreEqual(taskManager.GetTasksInPoolCount(), inPool);
            Assert.AreEqual(taskManager.GetActiveTasksCount(), alive);
        }

        [Test]
        public void FloatNull()
        {
            var inPool = taskManager.GetTasksInPoolCount();
            var alive = taskManager.GetActiveTasksCount();
            LogAssert.Expect(LogType.Error, "FastTweener: Callback is null!");
            var tween = Kovnir.FastTweener.FastTweener.Float(0, 1, 3, null);
            Assert.AreEqual(INVALID_TASK_ID, tween.Id);
            Assert.AreEqual(taskManager.GetTasksInPoolCount(), inPool);
            Assert.AreEqual(taskManager.GetActiveTasksCount(), alive);
        }

        [UnityTest]
        public IEnumerator FloatOnComplete()
        {
            bool done = false;
            var tween = Kovnir.FastTweener.FastTweener.Float(0, 1, 0.5f, _ => { }, () => { done = true; });
            Assert.AreEqual(DEFAULT_TASK_ID, tween.Id);
            Assert.False(done);
            yield return new WaitForSeconds(0.5f);
            Assert.True(done);
        }

        [UnityTest]
        public IEnumerator ExceptionInOnFloat()
        {
            Time.timeScale = 1;
            bool done = false;
            LogAssert.ignoreFailingMessages = true;
            var tween = Kovnir.FastTweener.FastTweener.Float(0, 1, 1, _ => throw new Exception(), () => done = true);

            Assert.False(done);
            Assert.IsTrue(tween.IsActive());
            yield return new WaitForSecondsRealtime(0.5f);
            Assert.False(done);
            Assert.IsTrue(tween.IsActive());
            yield return new WaitForSecondsRealtime(0.5f);
            Assert.True(done);
            Assert.IsFalse(tween.IsActive());
        }

        [UnityTest]
        public IEnumerator ExceptionInOnComplete()
        {
            float value = 0;
            var tween = Kovnir.FastTweener.FastTweener.Float(0, 1, 0.5f, f => { value = f; },
                () => throw new Exception("test exception"));
            Assert.AreEqual(DEFAULT_TASK_ID, tween.Id);
            Assert.AreEqual(0, value);

            Assert.IsTrue(tween.IsActive());
            LogAssert.ignoreFailingMessages = true;
            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(1, value);
            Assert.IsFalse(tween.IsActive());
        }

        [UnityTest]
        public IEnumerator KillIsAlive()
        {
            var inPool = taskManager.GetTasksInPoolCount();
            var alive = taskManager.GetActiveTasksCount();
            var tween = Kovnir.FastTweener.FastTweener.Float(0, 1, 5, f => { }, () => { });
            Assert.AreEqual(taskManager.GetTasksInPoolCount(), inPool - 1);
            Assert.AreEqual(taskManager.GetActiveTasksCount(), alive + 1);
            Assert.AreEqual(DEFAULT_TASK_ID, tween.Id);
            Assert.True(tween.IsActive());
            tween.Kill();
            yield return null;
            Assert.False(tween.IsActive());
            Assert.AreEqual(taskManager.GetTasksInPoolCount(), inPool);
            Assert.AreEqual(taskManager.GetActiveTasksCount(), alive);

            tween.Kill(); //kill killed is ok
            Assert.False(tween.IsActive());
            Assert.AreEqual(taskManager.GetTasksInPoolCount(), inPool);
            Assert.AreEqual(taskManager.GetActiveTasksCount(), alive);
        }

        [UnityTest]
        public IEnumerator GetSetEase()
        {
            var tween = Kovnir.FastTweener.FastTweener.Float(0, 1, 1, f => { });
            Assert.AreEqual(Ease.OutQuad, tween.GetEase());
            tween.SetEase(Ease.InBack);
            Assert.AreEqual(Ease.InBack, tween.GetEase());
            tween.SetEase(Ease.InCirc);
            Assert.AreEqual(Ease.InCirc, tween.GetEase());
            tween.Kill();
            yield return null;
            Assert.AreEqual(Ease.OutQuad, tween.GetEase());
        }


        [UnityTest]
        public IEnumerator GetSetIgnoreTimeScale()
        {
            Time.timeScale = 0;
            bool done = false;
            var tween = Kovnir.FastTweener.FastTweener.Schedule(2, () => done = true);
            Assert.IsFalse(tween.GetIgnoreTimeScale());

            Assert.False(done);
            yield return new WaitForSecondsRealtime(3f);
            Assert.False(done);

            tween.SetIgnoreTimeScale(true);
            Assert.IsTrue(tween.GetIgnoreTimeScale());
            Assert.False(done);
            yield return new WaitForSecondsRealtime(0.5f);
            Assert.False(done);
            yield return new WaitForSecondsRealtime(0.5f);
            Assert.False(done);
            yield return new WaitForSecondsRealtime(0.5f);
            Assert.False(done);
            yield return new WaitForSecondsRealtime(0.5f);
            Assert.True(done);
            Assert.IsFalse(tween.GetIgnoreTimeScale());

            Time.timeScale = 1;
        }
    }
}