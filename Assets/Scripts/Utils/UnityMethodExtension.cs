using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Utils
{
    public static class UnityMethodExtension
    {
        /// <summary>
        /// 【自定义拓展】限制magnitude区间
        /// </summary>
        /// <param name="vector3"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Vector3 Clamp(this Vector3 vector3, float min, float max)
        {
            if (vector3 == Vector3.zero) return Vector3.zero;
            return Mathf.Clamp(vector3.magnitude, min, max) * vector3.normalized;
        }
        /// <summary>
        /// 假装在Normalized的曲线上采样。（内部处理为从00开始，到11结束的归一化曲线）。
        /// </summary>
        /// <param name="normalizedInput">0~1之间的值</param>
        /// <returns>不一定是0~1之间的值（取决于curve极值是否在两端点）</returns>
        public static float EvaluateNormalized(this AnimationCurve curve, float normalizedInput)
        {
            if (curve.keys.Length < 2) throw new System.Exception("Evaluate不能。你这AnimationCurve怎么没头没尾的？");
            return Mathf.InverseLerp(curve.keys[0].value, curve.keys[^1].value,
                curve.Evaluate(Mathf.Lerp(curve.keys[0].time, curve.keys[^1].time, normalizedInput)));
        }
    }
}
