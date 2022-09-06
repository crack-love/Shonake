using System;
using System.Collections.Generic;
using UnityEngine;
using UnityCommon;

namespace Shotake
{
    /// <summary>
    /// 생성 패턴 스폰 반경(range?), 스폰 각(range?), 스폰 빈도
    /// </summary>
    [CreateAssetMenu(fileName ="GP", menuName ="Shotake/GeneratePattern")]
    class GeneratePattern : ScriptableObject
    {
        /// <summary>
        /// curve, x=stagePercent, y=value
        /// </summary>
        public bool isRangeCurveRandom;
        public AnimationCurve RangeCurve;
        public AnimationCurve RangeCurveRandomMax;

        public bool isAngleCurveRandom;
        public AnimationCurve AngleCurve;
        public AnimationCurve AngleCurveRandomMax;

        /// <summary>
        /// generate count per sec
        /// </summary>
        public AnimationCurve CountCurve;

        public void GetTick(in float currTime, out float range, out float angle, out float count)
        {
            if (isRangeCurveRandom)
            {
                var min = RangeCurve.Evaluate(currTime);
                var max = RangeCurveRandomMax.Evaluate(currTime);
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
                range = RangeCurve.Evaluate(currTime);
            }

            if (isAngleCurveRandom)
            {
                var min = AngleCurve.Evaluate(currTime);
                var max = AngleCurveRandomMax.Evaluate(currTime);
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
                angle = AngleCurve.Evaluate(currTime) * 360f;
            }

            count = CountCurve.Evaluate(currTime) * TimeManager.Instance.DeltaTime;
        }
    }
}