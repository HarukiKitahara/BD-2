using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace MyProject.Utils
{
    public static class SystemMethodExtension
    {
        /// <summary>
        /// ��Array�������ȡһ��Ԫ��
        /// </summary>
        public static T GetRandomElement<T>(this T[] array)
        {
            if (array == null) return default;
            return array[Random.Range(0, array.Length)];
        }
    }
}
