## Description

**FastTweener** - is a one more simple tweener, but **without memory allocation**!

Will be released in AssetStore soon.


Source of inspiration - [DoTween](http://dotween.demigiant.com/). This is realy powerfull and userfreandly tween engine, we use it and love it. But when we faced with extra memory allocation problem we decide to make our own solution, becouse **DoTween is allocate a lot of memory**.

## Banchmarks

<table>
   <tr>
    <th colspan="2"></th>
    <th>DoTween</th>
    <th>FastTweener</th>
   </tr>
   
   <tr>
   <td rowspan="2"><i>DoTween.Init()</i> vs <i>FastTween.Init()</i></td>
   <td>Memory (kb)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>
   <tr>
   <td>Time (ms)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>

   <td rowspan="2">First <i>MonoBehaviour.Update()</i></td>
   <td>Memory (kb)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>
   <tr>
   <td>Time (ms)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>
   
   <td rowspan="2">Total</td>
   <td>Memory (kb)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>
   <tr>
   <td>Time (ms)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>


   <tr>
   <td colspan="4"></td>
   </tr>

   
   <tr>
   <td rowspan="2"><i>DOVirtual.Float()</i> vs <i>FastTween.Float()</i></td>
   <td>Memory (kb)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>
   <tr>
   <td>Time (ms)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>

   <td rowspan="2">First <i>MonoBehaviour.Update()</i></td>
   <td>Memory (kb)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>
   <tr>
   <td>Time (ms)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>
   
   <td rowspan="2">Total</td>
   <td>Memory (kb)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>
   <tr>
   <td>Time (ms)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>


   <tr>
   <td colspan="4"></td>
   </tr>


   <tr>
   <td rowspan="2"><i>MonoBehaviour.Update()</i> with worked float tween</td>
   <td>Memory (kb)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>
   <tr>
   <td>Time (ms)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>


   <tr>
   <td colspan="4"></td>
   </tr>


   <tr>
   <td rowspan="2"><i>On Tween completed</td>
   <td>Memory (kb)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>
   <tr>
   <td>Time (ms)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>


   <tr>
   <td colspan="4"></td>
   </tr>


   <tr>
   <td rowspan="2"><i>DOVirtual.DelayCall</i> vs <i>FastTween.Schedule()</i></td>
   <td>Memory (kb)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>
   <tr>
   <td>Time (ms)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>

   <td rowspan="2">First <i>MonoBehaviour.Update()</i></td>
   <td>Memory (kb)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>
   <tr>
   <td>Time (ms)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>
   
   <td rowspan="2">Total</td>
   <td>Memory (kb)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>
   <tr>
   <td>Time (ms)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>


   <tr>
   <td colspan="4"></td>
   </tr>



   <tr>
   <td rowspan="2"><i>MonoBehaviour.Update()</i> with worked scheduling tween</td>
   <td>Memory (kb)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>
   <tr>
   <td>Time (ms)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>


   <tr>
   <td colspan="4"></td>
   </tr>


   <tr>
   <td rowspan="2"><i>On Tween completed</td>
   <td>Memory (kb)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>
   <tr>
   <td>Time (ms)</td>
   <td>3,5</td>
   <td>36</td>
   </tr>

</table>

Benchmarks was measured on Apple MacBook Pro 2017.
It was created to show memory allocation difference. It is const every time so we doesn't exequte test a big amount.
Time metrics is not super-accurate and made only to ensure it not slower than DoTween. 



So, if you don't need all power of DoTween and your goal is memory allocation optimisation - this is plugin for you!


