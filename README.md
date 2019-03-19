<img height="250" src="Documentation/logo.png">


# Description

**FastTweener** - is a one more simple tweener, but **entirely without memory allocation**!

Will be released in AssetStore soon.


Source of inspiration - <a href="http://dotween.demigiant.com/" target="_blank">DoTween</a>. DoTween is a really powerfull and user-friendly Tween Engine, we use it and love it. But when we faced with extra memory allocation problem we decide to make our own solution, because **DoTween allocate a lot of memory**. This is connected not with pooling (DoTween has a good recycling system), but with DoTween allocate memory while working.


# Benchmarks

Benchmarks were measured on Apple MacBook Pro 2017.

It was created to show the difference of memory allocation.

Time metrics aren’t super-accurate and made only to make sure that doesn't slower than DoTween.

You can repeat measures by yourself, all sources are in `Benchmark` folder.

<details><summary><b><i>DoTween.Init()</i> vs <i>FastTween.Init()</i> with default settings</b></summary>

<table>
   <tr>
    <th colspan="2"></th>
    <th>DoTween</th>
    <th>FastTweener</th>
   </tr>
   
   <tr>
   <td rowspan="2" ><i>DoTween.Init()</i> vs <i>FastTween.Init()</i> with default settings
      <br><sub>For FastTween used a pool with 16 base Tweens + 16 rigidbody Tweens + 16 transform Tweens.
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
   <td rowspan="2"><i>BehaviourUpdate</i> with worked schedule Tween</td>
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
   <td rowspan="2"><i>BehaviourUpdate</i> with worked schedule Tween</td>
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
   <td rowspan="2"><i>BehaviourUpdate</i> with worked schedule Tween</td>
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


So, if you don't need all power of DoTween and your goal is memory allocation optimization - this plugin is made for you!



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

//this ease will be used for each Tween if nothing another ease set explicitly
settings.DefaultEase = Ease.Linear; //default - Ease.OutQuad

//size of the pool of the Transform extensions like transform.TweenScale();
settings.TransformExtensionsPoolSize = 32; //default - 16

//size of the pool of the Rigidbody extensions like rigidbody.TweenMove();
settings.RigidbodyExtensionsPoolSize = 32; //default - 16

//size of the pool of the general Tweens (common + extensions)
settings.TaskPoolSize = 32; //default - 16

//if true - FastTweener will write a name of the GameObject in Errors, but it will allocate addition memory
settings.SaveGameObjectName = true; //default - false 

//FastTweener will write Warnings if actual fps is lower then this value and Tweens late. Set 0 to disable Warnings
settings.CriticalFpsToLogWarning = 50; //default - 30 

FastTweener.Init(settings);
```

If you don't do that `FastTweener` will be auto-initialized with the default settings. To get initialization status use `bool FastTweener.IsInitialized` property.

**WARNING: If you want to use manual initialization you need to do it before creating your first Tween!**

## Create a Tween

There is a several ways to create a new Tween:
* Using FastTweener class
```c#
FastTweener.Float(floatFrom, floatTo, duration, x => { /* Your logic here */ });
FastTweener.Vector3(vectorFrom, vectorTo, duration, x => { /* Your logic here */ });
FastTweener.Schedule(delay, () => { /* Your logic here */ });
```
* Using Extensions for Transform class
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
* Using Extensions for Rigidbody class
```c#
rigidbody.TweenMove(vectorTo, duration);
rigidbody.TweenMoveX(floatTo, duration);
rigidbody.TweenMoveY(floatTo, duration);
rigidbody.TweenMoveZ(floatTo, duration);

rigidbody.TweenRotate(vectorTo, duration);
```

All extension methods made without closure and don't allocate memory too.

When you create a Tween it will start to play automatically.

Each method has required parameters:
```c#
T endValue              //finish value of Tween. Vector3 or float depends on the specific method
float duration          //duration of Tween in seconds

// Only for Tweens created via FastTweener class:

T startValue            //finish value of Tween. Vector3 or float depends on the specific method
Action<T> callback      //Action<Vector3> or Action<float> depends on the specific method
```

And optional parameter:
```c#
Ease ease               //ease for tweening (default one you set in Init, or OutQuad if you didn't set it)
bool ignoreTimescale    //should Tween ignore timescale (default false)
Action onComplete       //the callback will be called when Tween completed (default null)
```

`FastTweener.Schedule` is the only exception. It contains only three parameters:
```c#
float delay             //delay before Action executing
Action callback         //action to execute after Delay
bool ignoreTimescale    //should Tween ignore timescale (default false)
```

Each method contains overloads for each combination of optional parameters. For example:
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

## Work with Tween

You can get or set Tween parameters after Tween creation. To make it you should save `FastTween` instance during Tween creation and call his methods.

```c#
FastTween tween = transform.TweenLocalMoveY(floatTo, duration);

uint id = tween.Id;

Ease ease = tween.GetEase();
tween.SetEase(Ease.InCirc);

bool ignoreTimeScale = tween.GetIgnoreTimeScale();
tween.SetIgnoreTimeScale(true);

tween.OnComplete(() => Debug.Log("Done!"));

bool isAlive = tween.IsAlive();
tween.Kill();
```

Also you can use chaining (Linq) style:
```c#
tween.SetEase(Ease.Linear).SetIgnoreTimeScale(true).OnComplete(doSomething);
```

Under the hood `FastTween` call static methods of `FastTweener` class, so you can use it too. It is the same.
```c#
FastTween tween = transform.TweenLocalMoveY(floatTo, duration);

Ease ease = FastTweener.GetEase(tween);
FastTweener.SetEase(tween, Ease.InCirc);

bool ignoreTimeScale = FastTweener.GetIgnoreTimeScale(tween);
FastTweener.SetIgnoreTimeScale(tween,true);

FastTweener.SetOnComplete(tween,() => Debug.Log("Done!"));

bool isAlive = FastTweener.IsAlive(tween);
FastTweener.Kill(tween);
```

**WARNING: Read [Performance hints](#performance-hints) before using this methods for the best performance!**


## Ease Types

To set Default Ease that was set in the settings during [Initialization](#initialize) use `tween.SetEase(Ease.Default)`.

You can use one of the next Eases:
* Linear
* InSine
* OutSine
* InOutSine
* InQuad
* OutQuad
* InOutQuad
* InCubic
* OutCubic
* InOutCubic
* InQuart
* OutQuart
* InOutQuart
* InQuint
* OutQuint
* InOutQuint
* InExpo
* OutExpo
* InOutExpo
* InCirc
* OutCirc
* InOutCirc
* InElastic
* OutElastic
* InOutElastic
* InBack
* OutBack
* InOutBack
* InBounce
* OutBounce
* InOutBounce

Formulas for simple eases were found at [gizma.com](http://gizma.com/easing/#l) (Action Script 3)

Bounce Eases from [tweenman-as3](https://github.com/danro/tweenman-as3) GitHub repository (Action Script 3)

Elastic and Back formulas were taken from here [processing penner easing](https://github.com/jesusgollonet/processing-penner-easing) GitHub repository (Java)


## Monitoring

`Real-time Monitoring` allow you to see the actual count of alive Tweens and count of Tweens in the pool. To see this data find GameObject with name `FastTweener` in the root of `DontDestroyOnLoad` section in the `Hierarchy` window during the Play mode.

<img width="700" src="Documentation/monitoring.gif">


# Performance Hints

`FastTween` is just a struct with `Tween Task` id. We can't set instance of `Tween Task` to `FastTween` instance because in future this Tween will be used for another Tween. So all functions `IsActive`, `GetEase`, `SetEase`, `GetIgnoreTimeScale`, `SetIgnoreTimeScale`, and `OnComplete` required to find a `Tween Task` in the `Tween Tasks` list. But when you send these parameters during a Tween creating it won't take additional time.

For example, this code is faster:
```c#
FastTween tween = FastTweener.Float(-3, 3, 0.5f, value => DoSomething, Ease.OutBounce, OnComplete);
```
Than this code:
```c#
FastTween tween = FastTweener.Float(-3, 3, 0.5f, value => DoSomething);
tween.SetEase(Ease.OutBounce);
tween.OnComplete(OnComplete);
```

For the same reasons, receiving data from `FastTween` can be not so fast as we want. So, it will be better to cache Tween parameters if it’s possible.

For example, this code is faster:
```c#
private Ease tweenEase;
public void SomeMethod(FastTween someTween)
{
    tweenEase = someTween.GetEase();
}
public void Update()
{
    if (tweenEase == Ease.Linear)
    {
        //Do some logic
    }
}
```
Than this code:
```c#
private FastTween tween;
public void SomeMethod(FastTween someTween)
{
    tween = someTween;
}
public void Update()
{
    if (tween.GetEase() == Ease.Linear)
    {
        //Do some logic
    }
}
```
