using System;
using System.Collections.Generic;
using UnityEngine;
using UnityCommon;

namespace Shotake
{
    [CreateAssetMenu(fileName ="New Generate Pattern", menuName ="Shotake/GeneratePattern")]
    class GeneratePattern : ScriptableObject
    {
        [Tooltip("x=wavePercent, y=value")]
        [SerializeField] bool isRangeCurveRandom;
        [SerializeField] AnimationCurve RangeCurve;
        [Condition("isRangeCurveRandom")]
        [SerializeField] AnimationCurve RangeCurveRandomMax;

        [Tooltip("x=wavePercent, y=value")]
        [SerializeField] bool isAngleCurveRandom;
        [SerializeField] AnimationCurve AngleCurve;
        [Condition("isAngleCurveRandom")]
        [SerializeField] AnimationCurve AngleCurveRandomMax;

        [Tooltip("Generate count per sec")]
        [SerializeField] AnimationCurve CountCurve;

        /// <summary>
        /// count as per frame
        /// </summary>
        public void GetTick(in float currTimePer, out float range, out float angle, out float count)
        {
            if (isRangeCurveRandom)
            {
                var min = RangeCurve.Evaluate(currTimePer);
                var max = RangeCurveRandomMax.Evaluate(currTimePer);
                if (min > max)
                {
                    float tmp = min;
                    min = max;
                    max = tmp;
                }
                range = UnityEngine.Random.Range(min, max);
            }
            else
            {
                range = RangeCurve.Evaluate(currTimePer);
            }

            if (isAngleCurveRandom)
            {
                var min = AngleCurve.Evaluate(currTimePer);
                var max = AngleCurveRandomMax.Evaluate(currTimePer);
                if (min > max)
                {
                    float tmp = min;
                    min = max;
                    max = tmp;
                }
                angle = UnityEngine.Random.Range(min, max) * 360f;
            }
            else
            {
                angle = AngleCurve.Evaluate(currTimePer) * 360f;
            }

            count = CountCurve.Evaluate(currTimePer) * TimeManager.Instance.DeltaTime;
        }
    }
}