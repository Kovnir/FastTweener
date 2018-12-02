﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Kovnir.Tweener;
using Kovnir.Tweener.TaskManagment;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class FastTweenerTests
{
    private FastTweenerComponent fastTweenerComponent;
    private TaskManager taskManager;

    [SetUp]
    public void Setup()
    {
        FastTweener.Init();
        fastTweenerComponent = GameObject.FindObjectOfType<FastTweenerComponent>();
        FieldInfo myFieldInfo =
            typeof(FastTweenerComponent).GetField("taskManager", BindingFlags.Static | BindingFlags.NonPublic);

        taskManager = (TaskManager) myFieldInfo.GetValue(fastTweenerComponent);

    }

    [UnityTest]
    [Order(0)]
    public IEnumerator PoolSize()
    {
        Assert.AreEqual(taskManager.GetTasksInPoolCount(), 16);
        Assert.AreEqual(taskManager.GetActiveTasksCount(), 0);

        List<FastTween> fastTweens = new List<FastTween>();
        for (int i = 0; i < 20; i++)
        {
            fastTweens.Add(FastTweener.Schedule(10, () => { }));
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
        FastTweener.Schedule(2, () => done = true, false);
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
        FastTweener.Schedule(2, () => done = true, true);
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
        var tween = FastTweener.Schedule(2, () => done = true, false);
        Assert.False(done);
        yield return new WaitForSecondsRealtime(3f);
        Assert.False(done);
        tween.Kill();
        
        done = false;
        FastTweener.Schedule(2, () => done = true, true);
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
        var active = taskManager.GetActiveTasksCount();
        LogAssert.Expect(LogType.Error, "FastTweener: Callback is null!");
        var tween = FastTweener.Schedule(1, null);
        Assert.AreEqual(tween.Id, 0);
        Assert.AreEqual(taskManager.GetTasksInPoolCount(), inPool);
        Assert.AreEqual(taskManager.GetActiveTasksCount(), active);
    }

    [Test]
    public void FloatNull()
    {
        var inPool = taskManager.GetTasksInPoolCount();
        var active = taskManager.GetActiveTasksCount();
        LogAssert.Expect(LogType.Error, "FastTweener: Callback is null!");
        var tween = FastTweener.Float(0, 1, 3, null);
        Assert.AreEqual(tween.Id, 0);
        Assert.AreEqual(taskManager.GetTasksInPoolCount(), inPool);
        Assert.AreEqual(taskManager.GetActiveTasksCount(), active);
    }

    [UnityTest]
    public IEnumerator FloatOnComplete()
    {
        bool done = false;
        var tween = FastTweener.Float(0, 1, 0.5f, f => { }, () => { done = true; });
        Assert.AreNotEqual(tween.Id, 0);
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
        var tween = FastTweener.Float(0, 1, 1, f => { throw new Exception(); }, () => done = true);
    
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
        var tween = FastTweener.Float(0, 1, 0.5f, f => { value = f; }, () => { throw new Exception("test exception");});
        Assert.AreNotEqual(tween.Id, 0);
        Assert.AreEqual(value, 0);

        Assert.IsTrue(tween.IsActive());
        LogAssert.ignoreFailingMessages = true;
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(value, 1);
        Assert.IsFalse(tween.IsActive());
    }
    
    [UnityTest]
    public IEnumerator KillIsActive()
    {
        var inPool = taskManager.GetTasksInPoolCount();
        var active = taskManager.GetActiveTasksCount();
        var tween = FastTweener.Float(0, 1, 5, f => { }, () => { });
        Assert.AreEqual(taskManager.GetTasksInPoolCount(), inPool - 1);
        Assert.AreEqual(taskManager.GetActiveTasksCount(), active + 1);
        Assert.AreNotEqual(tween.Id, 0);
        Assert.True(tween.IsActive());
        tween.Kill();
        yield return null;
        Assert.False(tween.IsActive());
        Assert.AreEqual(taskManager.GetTasksInPoolCount(), inPool);
        Assert.AreEqual(taskManager.GetActiveTasksCount(), active);
        
        tween.Kill(); //kill killed is ok
        Assert.False(tween.IsActive());
        Assert.AreEqual(taskManager.GetTasksInPoolCount(), inPool);
        Assert.AreEqual(taskManager.GetActiveTasksCount(), active);
    }

    [UnityTest]
    public IEnumerator GetSetEase()
    {
        var tween = FastTweener.Float(0, 1, 1, f => { });
        Assert.AreEqual(tween.GetEase(), FastTweener.DEFAULT_EASE);
        tween.SetEase(Ease.InBack);
        Assert.AreEqual(tween.GetEase(), Ease.InBack);
        tween.SetEase(Ease.InCirc);
        Assert.AreEqual(tween.GetEase(), Ease.InCirc);
        tween.Kill();
        yield return null;
        Assert.AreEqual(tween.GetEase(), FastTweener.DEFAULT_EASE);
    }
    
    
    [UnityTest]
    public IEnumerator GetSetIgnoreTimeScale()
    {
        Time.timeScale = 0;
        bool done = false;
        var tween = FastTweener.Schedule(2, () => done = true);
        Assert.AreEqual(tween.GetIgnoreTimeScale(), false);
        
        Assert.False(done);
        yield return new WaitForSecondsRealtime(3f);
        Assert.False(done);

        tween.SetIgnoreTimeScale(true);
        Assert.AreEqual(tween.GetIgnoreTimeScale(), true);
        Assert.False(done);
        yield return new WaitForSecondsRealtime(0.5f);
        Assert.False(done);
        yield return new WaitForSecondsRealtime(0.5f);
        Assert.False(done);
        yield return new WaitForSecondsRealtime(0.5f);
        Assert.False(done);
        yield return new WaitForSecondsRealtime(0.5f);
        Assert.True(done);
        Assert.AreEqual(tween.GetIgnoreTimeScale(), false);

        Time.timeScale = 1;
    }
    
    
}