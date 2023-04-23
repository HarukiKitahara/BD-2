using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Utils
{
    public static class UnityMethodExtension
    {
        /// <summary>
        /// ���Զ�����չ������magnitude����
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
        /// ��װ��Normalized�������ϲ��������ڲ�����Ϊ��00��ʼ����11�����Ĺ�һ�����ߣ���
        /// </summary>
        /// <param name="normalizedInput">0~1֮���ֵ</param>
        /// <returns>��һ����0~1֮���ֵ��ȡ����curve��ֵ�Ƿ������˵㣩</returns>
        public static float EvaluateNormalized(this AnimationCurve curve, float normalizedInput)
        {
            if (curve.keys.Length < 2) throw new System.Exception("Evaluate���ܡ�����AnimationCurve��ôûͷûβ�ģ�");
            return Mathf.InverseLerp(curve.keys[0].value, curve.keys[^1].value,
                curve.Evaluate(Mathf.Lerp(curve.keys[0].time, curve.keys[^1].time, normalizedInput)));
        }
    }
}
