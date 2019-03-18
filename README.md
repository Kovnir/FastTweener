<img height="250" src="Documentation/logo.png">


# Description

**FastTweener** - is a one more simple tweener, but **entirely without memory allocation**!

Will be released in AssetStore soon.


Source of inspiration - <a href="http://dotween.demigiant.com/" target="_blank">DoTween</a>. This is realy powerfull and userfreandly tween engine, we use it and love it. But when we faced with extra memory allocation problem we decide to make our own solution, becouse **DoTween is allocate a lot of memory**.

# Benchmarks

Benchmarks was measured on Apple MacBook Pro 2017.
It was created to show memory allocation difference.
Time metrics is not super-accurate and made only to ensure it not slower than DoTween. 
You can reapeat measures by yourself, all sources are in Benchmark folder.

<details><summary><b><i>DoTween.Init()</i> vs <i>FastTween.Init()</i> with default settings</b></summary>

<table>
   <tr>
    <th colspan="2"></th>
    <th>DoTween</th>
    <th>FastTweener</th>
   </tr>
   
   <tr>
   <td rowspan="2" ><i>DoTween.Init()</i> vs <i>FastTween.Init()</i> with default settings
      <br><sub>For FastTween used pool with 16 base tweens + 16 rigidbody tweens + 16 transform tweens.
      <br>DoTween initialisation containce from <i>DoTween.Init()</i>, <i>DoTween..cctor()</i>, and <i>TweenManager..cctor()</i></sub></td>
   <td>Memory<br>(kb)</td>
   <td>7,9</td>
   <td>5,3</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>6,013</td>
   <td>5,602</td>
   </tr>

   <td rowspan="2">First <i>BehaviourUpdate</i></td>
   <td>Memory<br>(kb)</td>
   <td>0</td>
   <td>0</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>0</td>
   <td>0,482</td>
   </tr>

   <td rowspan="2">Total</td>
   <td>Memory<br>(kb)</td>
   <td>7,9</td>
   <td>5,3</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>6,013</td>
   <td>6,084</td>
   </tr>
</table>

</details>
<br>
<details><summary><b><i>DOVirtual.DelayCall()</i> vs <i>FastTween.Schedule()</i> first call</b></summary>

First and second call has a big difference in measures.

<table>
   <tr>
    <th colspan="2"  width="800"></th>
    <th>DoTween</th>
    <th>FastTweener</th>
   </tr>

   <tr>
   <td rowspan="2"><i>DOVirtual.DelayCall()</i> vs <i>FastTween.Schedule()</i>  first call</td>
   <td>Memory<br>(kb)</td>
   <td>0,8</td>
   <td>0</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>2,19</td>
   <td>0,47</td>
   </tr>

   <td rowspan="2">First <i>BehaviourUpdate</i></td>
   <td>Memory<br>(kb)</td>
   <td>0,136</td>
   <td>0</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>2,77</td>
   <td>0,285</td>
   </tr>
   
   <td rowspan="2">Total</td>
   <td>Memory<br>(kb)</td>
   <td>0,936</td>
   <td>0</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>4,96</td>
   <td>0,755</td>
   </tr>


   <tr>
   <td colspan="4"></td>
   </tr>


   <tr>
   <td rowspan="2"><i>BehaviourUpdate</i> with worked schedule tween</td>
   <td>Memory<br>(kb)</td>
   <td>0</td>
   <td>0</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>0,0065</td>
   <td>0,0028</td>
   </tr>


   <tr>
   <td colspan="4"></td>
   </tr>


   <tr>
   <td rowspan="2"><i>On Tween completed</td>
   <td>Memory<br>(kb)</td>
   <td>0,6</td>
   <td>0</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>1,56</td>
   <td>0,285</td>
   </tr>
</table>

</details>
<br>
<details><summary><b><i>DOVirtual.DelayCall()</i> vs <i>FastTween.Schedule()</i> second call</b></summary>


<table>
   <tr>
    <th colspan="2"  width="800"></th>
    <th>DoTween</th>
    <th>FastTweener</th>
   </tr>
   <tr>
   <td rowspan="2"><i>DOVirtual.DelayCall()</i> vs <i>FastTween.Schedule()</i>  second call</td>
   <td>Memory<br>(kb)</td>
   <td>0,344</td>
   <td>0</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>0,41</td>
   <td>0,01</td>
   </tr>

   <td rowspan="2">First <i>BehaviourUpdate</i></td>
   <td>Memory<br>(kb)</td>
   <td>0,104</td>
   <td>0</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>0,05</td>
   <td>0</td>
   </tr>
   
   <td rowspan="2">Total</td>
   <td>Memory<br>(kb)</td>
   <td>0,448</td>
   <td>0</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>0,46</td>
   <td>0,01</td>
   </tr>


   <tr>
   <td colspan="4"></td>
   </tr>


   <tr>
   <td rowspan="2"><i>BehaviourUpdate</i> with worked schedule tween</td>
   <td>Memory<br>(kb)</td>
   <td>0</td>
   <td>0</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>0,007</td>
   <td>0,004</td>
   </tr>


   <tr>
   <td colspan="4"></td>
   </tr>


   <tr>
   <td rowspan="2"><i>On Tween completed</td>
   <td>Memory<br>(kb)</td>
   <td>0</td>
   <td>0</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>0,03</td>
   <td>0,025</td>
   </tr>
</table>

</details>
<br>
<details><summary><b><i>DOVirtual.Float()</i> vs <i>FastTweener.Float()</i> first call</b></summary>


<table>
   <tr>
    <th colspan="2" width="800"></th>
    <th>DoTween</th>
    <th>FastTweener</th>
   </tr>
   <tr>
   <td rowspan="2"><i>DOVirtual.Float()</i> vs <i>FastTweener.Float()</i> first call</td>
   <td>Memory<br>(kb)</td>
   <td>1,3</td>
   <td>0</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>1,255</td>
   <td>0,48</td>
   </tr>

   <td rowspan="2">First <i>BehaviourUpdate</i></td>
   <td>Memory<br>(kb)</td>
   <td>0,21</td>
   <td>0</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>3</td>
   <td>1,17</td>
   </tr>
   
   <td rowspan="2">Total</td>
   <td>Memory<br>(kb)</td>
   <td>1,51</td>
   <td>0</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>5,255</td>
   <td>1,65</td>
   </tr>


   <tr>
   <td colspan="4"></td>
   </tr>


   <tr>
   <td rowspan="2"><i>BehaviourUpdate</i> with worked schedule tween</td>
   <td>Memory<br>(kb)</td>
   <td>0</td>
   <td>0</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>0,02</td>
   <td>0,017</td>
   </tr>


   <tr>
   <td colspan="4"></td>
   </tr>


   <tr>
   <td rowspan="2"><i>On Tween completed</td>
   <td>Memory<br>(kb)</td>
   <td>0,466</td>
   <td>0</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>0,78</td>
   <td>0,06</td>
   </tr>
</table>

</details>
<br>
<details><summary><b><i>DOVirtual.Float()</i> vs <i>FastTweener.Float()</i> second call</b></summary>


<table>
   <tr>
    <th colspan="2" width="800"></th>
    <th>DoTween</th>
    <th>FastTweener</th>
   </tr>
   <tr>
   <td rowspan="2"><i>DOVirtual.Float()</i> vs <i>FastTweener.Float()</i> second call</td>
   <td>Memory<br>(kb)</td>
   <td>0,6</td>
   <td>0</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>0,31</td>
   <td>0,015</td>
   </tr>

   <td rowspan="2">First <i>BehaviourUpdate</i></td>
   <td>Memory<br>(kb)</td>
   <td>0</td>
   <td>0</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>0,025</td>
   <td>0,02</td>
   </tr>
   
   <td rowspan="2">Total</td>
   <td>Memory<br>(kb)</td>
   <td>0,6</td>
   <td>0</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>0,335</td>
   <td>0,035</td>
   </tr>


   <tr>
   <td colspan="4"></td>
   </tr>


   <tr>
   <td rowspan="2"><i>BehaviourUpdate</i> with worked schedule tween</td>
   <td>Memory<br>(kb)</td>
   <td>0</td>
   <td>0</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>0,02</td>
   <td>0,016</td>
   </tr>


   <tr>
   <td colspan="4"></td>
   </tr>


   <tr>
   <td rowspan="2"><i>On Tween completed</td>
   <td>Memory<br>(kb)</td>
   <td>0</td>
   <td>0</td>
   </tr>
   <tr>
   <td>Time<br>(ms)</td>
   <td>0,045</td>
   <td>0,02</td>
   </tr>
</table>
</details>
<br>

So, if you don't need all power of DoTween and your goal is memory allocation optimisation - this is plugin for you!



# How to Use

## Get Started

### Importing

You can download `FastTweener` from this repository or from AssetStore (coming soon). You can unzip it anywhere in your Unity Assets folder, except Editor folder. No additional setups needed, `FastTweener` is ready to use!

### Namespace

To use `FastTweener` you need to add namespace in each class where you want to use it.
```c#
using DG.Tweening;
```

### Initialize

You can initialize `FastTweener` to setup some global options:

```c#
FastTweenerSettings settings = new FastTweenerSettings();
//this ease will used for each tween if nothing else set explicitly
settings.DefaultEase = Ease.OutQuad;
//size of pool for Transform extensions, like tranform.TweenScale();
settings.TransformExtensionsPoolSize = 16;
//size of pool for Rigidbody extensions, like rigidbody.TweenMove();
settings.RigidbodyExtensionsPoolSize = 16;
//size of pool of general Tweens (common + extensions)
settings.TaskPoolSize = 16;
//if true - FastTweener will write name of GameObject in Errors, but it will allocate addition memory
settings.SaveGameObjectName = false;
//FastTweener will write Warnings if actual fps is lower then this value and tweens late. Set 0 to disable Warnings
settings.CriticalFpsToLogWarning = 30;
FastTweener.Init(settings);
```

If you don't do that `FastTweener` will be auto-initialized with the default settings.

**WARNING: If you want to use manual initialization you need to do that before creating your first Tween!**

## Create a Tween

There is a several ways to create a new Tween:
* Using FastTweener class
```c#
FastTweener.Float(floatFrom, floatTo, duration, x => { /* Your logic here */ });
FastTweener.Vector3(vectorFrom, vectorTo, duration, x => { /* Your logic here */ });
FastTweener.Schedule(delay, () => { /* Your logic here */ });
```
* Using Extentions for Transform class
```c#
transform.TweenMove(vectorTo, duration);
transform.TweenMoveX(floatTo, duration);
transform.TweenMoveY(floatTo, duration);
transform.TweenMoveZ(floatTo, duration);

transform.TweenLocalMove(vectorTo, duration);
transform.TweenLocalMoveX(floatTo, duration);
transform.TweenLocalMoveY(floatTo, duration);
transform.TweenLocalMoveZ(floatTo, duration);

transform.TweenScale(vectorTo, duration);
transform.TweenScaleX(floatTo, duration);
transform.TweenScaleY(floatTo, duration);
transform.TweenScaleZ(floatTo, duration);

transform.TweenRotate(vectorTo, duration);
transform.TweenLocalRotate(vectorTo, duration);
```
* Using Extentions for Rigidbody class
```c#
rigidbody.TweenMove(vectorTo, duration);
rigidbody.TweenMoveX(floatTo, duration);
rigidbody.TweenMoveY(floatTo, duration);
rigidbody.TweenMoveZ(floatTo, duration);

rigidbody.TweenRotate(vectorTo, duration);
```

Each method has required parametrs:
```c#
T endValue              //finish value of tween. Vector3 or float depends on method
float duration          //duration of Tween in seconds

Only for Tweens created via FastTweener class:

T startValue            //finish value of tween. Vector3 or float depends on method
Action<T> callback      //Action<Vector3> or Action<float> depends on method
```

And optional paramert:
```c#
Ease ease               //(default one you set in Init, or OutQuad if you didn't set it)
bool ignoreTimescale    //Should Tween ignire timescale (default false)
Action onComplete       //callback will called when tween completed (default null)
```

`FastTweener.Schedule` is only exception. It contains only three parametrs:
```c#
float delay             //Delay before Action executing
Action callback         //Action to execute after Delay
bool ignoreTimescale    //Should Tween ignore timescale (default false)
```

Each methods contains overloads for each combination of optional parametrs. For example:
```c#
//No optional parameters
transform.TweenMove(vectorTo, duration);
//One parameter:
//ease
transform.TweenMove(vectorTo, duration, Ease.InElastic);
//ignoreTimescale
transform.TweenMove(vectorTo, duration, true);
//onComplete
transform.TweenMove(vectorTo, duration, OnComplete);
//Parameters combinations:
//ease & ignoreTimescale
transform.TweenMove(vectorTo, duration, Ease.InElastic, true);
//ease & onComplete
transform.TweenMove(vectorTo, duration, Ease.InElastic, OnComplete);
//ignoreTimescale & onComplete
transform.TweenMove(vectorTo, duration, true, OnComplete);
```



Other docs are coming soon...


# Performance hints

`FastTween` is a just struct with tween id. So all functions `IsActive`, `GetEase`, `SetEase`, `GetIgnoreTimeScale`, `SetIgnoreTimeScale`, and `OnComplete` required to find a tween task in the tween tasks list. But when you send this parameters during a tween creating it will not take additional time.

For example this code is faster:
```c#
FastTween tween = FastTweener.Float(-3, 3, 0.5f, value => DoSomething, Ease.OutBounce, OnComplete);
```
Than this code:
```c#
FastTween tween = FastTweener.Float(-3, 3, 0.5f, value => DoSomething);
tween.SetEase(Ease.OutBounce);
tween.OnComplete(OnComplete);
```
