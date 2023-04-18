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
    }
}
