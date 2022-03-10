using System;
using UnityEngine;

namespace Kovnir.FastTweener
{
    public enum Ease
    {
        Linear,
        InSine,
        OutSine,
        InOutSine,
        InQuad,
        OutQuad,
        InOutQuad,
        InCubic,
        OutCubic,
        InOutCubic,
        InQuart,
        OutQuart,
        InOutQuart,
        InQuint,
        OutQuint,
        InOutQuint,
        InExpo,
        OutExpo,
        InOutExpo,
        InCirc,
        OutCirc,
        InOutCirc,
        InElastic,
        OutElastic,
        InOutElastic,
        InBack,
        OutBack,
        InOutBack,
        InBounce,
        OutBounce,
        InOutBounce,
//        Flash,
//        InFlash,
//        OutFlash,
//        InOutFlash,
        Default
    }

    public static class EaseCalculator
    {
        //Simple formulas from here:
        //http://gizma.com/easing/#l
        //Elastic and Back formulas from here (java):
        //https://github.com/jesusgollonet/processing-penner-easing/tree/master/src
        //Bounce (as3):
        //https://github.com/danro/tweenman-as3/blob/master/Easing.as

        public static float Calculate(Ease ease, float start, float end, float currentTime, float duration)
        {
            float t = currentTime;
            float b = start;
            float d = duration;
            float c = end - start;
            switch (ease)
            {
                case Ease.Linear:
                    return c * t / d + b;
                case Ease.InSine:
                    return -c * Mathf.Cos(t / d * (Mathf.PI / 2)) + c + b;
                case Ease.OutSine:
                    return c * Mathf.Sin(t / d * (Mathf.PI / 2)) + b;
                case Ease.InOutSine:
                    return -c / 2 * (Mathf.Cos(Mathf.PI * t / d) - 1) + b;
                case Ease.InQuad:
                    t /= d;
                    return c * t * t + b;
                case Ease.OutQuad:
                    t /= d;
                    return -c * t * (t - 2) + b;
                case Ease.InOutQuad:
                    t /= d / 2;
                    if (t < 1) return c / 2 * t * t + b;
                    t--;
                    return -c / 2 * (t * (t - 2) - 1) + b;
                case Ease.InCubic:
                    t /= d;
                    return c * t * t * t + b;
                case Ease.OutCubic:
                    t /= d;
                    t--;
                    return c * (t * t * t + 1) + b;
                case Ease.InOutCubic:
                    t /= d / 2;
                    if (t < 1) return c / 2 * t * t * t + b;
                    t -= 2;
                    return c / 2 * (t * t * t + 2) + b;
                case Ease.InQuart:
                    t /= d;
                    return c * t * t * t * t + b;
                case Ease.OutQuart:
                    t /= d;
                    t--;
                    return -c * (t * t * t * t - 1) + b;
                case Ease.InOutQuart:
                    t /= d / 2;
                    if (t < 1) return c / 2 * t * t * t * t + b;
                    t -= 2;
                    return -c / 2 * (t * t * t * t - 2) + b;
                case Ease.InQuint:
                    t /= d;
                    return c * t * t * t * t * t + b;
                case Ease.OutQuint:
                    t /= d;
                    t--;
                    return c * (t * t * t * t * t + 1) + b;
                case Ease.InOutQuint:
                    t /= d / 2;
                    if (t < 1) return c / 2 * t * t * t * t * t + b;
                    t -= 2;
                    return c / 2 * (t * t * t * t * t + 2) + b;
                case Ease.InExpo:
                    return c * Mathf.Pow(2, 10 * (t / d - 1)) + b;
                case Ease.OutExpo:
                    return c * (-Mathf.Pow(2, -10 * t / d) + 1) + b;
                case Ease.InOutExpo:
                    if (t < 1) return c / 2 * Mathf.Pow(2, 10 * (t - 1)) + b;
                    t--;
                    return c / 2 * (-Mathf.Pow(2, -10 * t) + 2) + b;
                case Ease.InCirc:
                    t /= d;
                    return -c * (Mathf.Sqrt(1 - t * t) - 1) + b;
                case Ease.OutCirc:
                    t /= d;
                    t--;
                    return c * Mathf.Sqrt(1 - t * t) + b;
                case Ease.InOutCirc:
                    t /= d / 2;
                    if (t < 1) return -c / 2 * (Mathf.Sqrt(1 - t * t) - 1) + b;
                    t -= 2;
                    return c / 2 * (Mathf.Sqrt(1 - t * t) + 1) + b;
                case Ease.InElastic:
                    if (t == 0) return b;
                    if ((t /= d) == 1) return b + c;
                    float p = d * .3f;
                    float a = c;
                    float s = p / 4;
                    return -(a * Mathf.Pow(2, 10 * (t -= 1)) *
                             Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p)) + b;
                case Ease.OutElastic:
                    if (t == 0) return b;
                    if ((t /= d) == 1) return b + c;
                    p = d * .3f;
                    a = c;
                    s = p / 4;
                    return (a * Mathf.Pow(2, -10 * t) *
                            Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p) + c + b);
                case Ease.InOutElastic:
                    if (t == 0) return b;
                    if ((t /= d / 2) == 2) return b + c;
                    p = d * (.3f * 1.5f);
                    a = c;
                    s = p / 4;
                    if (t < 1)
                        return -.5f * (a * Mathf.Pow(2, 10 * (t -= 1)) *
                                       Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p)) + b;
                    return a * Mathf.Pow(2, -10 * (t -= 1)) *
                           Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p) * .5f + c + b;
                case Ease.InBack:
                    s = 1.70158f;
                    return c * (t /= d) * t * ((s + 1) * t - s) + b;
                case Ease.OutBack:
                    s = 1.70158f;
                    return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b;
                case Ease.InOutBack:
                    s = 1.70158f;
                    if ((t /= d / 2) < 1) return c / 2 * (t * t * (((s *= (1.525f)) + 1) * t - s)) + b;
                    return c / 2 * ((t -= 2) * t * (((s *= (1.525f)) + 1) * t + s) + 2) + b;
                case Ease.InBounce:
                    return EaseInBounce(t, b, c, d);
                case Ease.OutBounce:
                    return EaseOutBounce(t, b, c, d);
                case Ease.InOutBounce:
                    if (t < d / 2) return EaseInBounce(t * 2, 0, c, d) * .5f + b;
                    else return EaseOutBounce(t * 2 - d, 0, c, d) * .5f + c * .5f + b;
                default:
                    throw new ArgumentOutOfRangeException("ease", ease, null);
            }
        }

        private static float EaseInBounce(float t, float b, float c, float d)
        {
            return c - EaseOutBounce(d - t, 0, c, d) + b;
        }

        private static float EaseOutBounce(float t, float b, float c, float d)
        {
            if ((t /= d) < (1f / 2.75f))
            {
                return c * (7.5625f * t * t) + b;
            }
            else if (t < (2f / 2.75f))
            {
                return c * (7.5625f * (t -= (1.5f / 2.75f)) * t + .75f) + b;
            }
            else if (t < (2.5f / 2.75f))
            {
                return c * (7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f) + b;
            }
            else
            {
                return c * (7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f) + b;
            }
        }
    }
}