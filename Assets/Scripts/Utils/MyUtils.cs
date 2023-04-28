using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Utils
{
    public static class MyUtils
    {
        /// <summary> ���������� </summary>
        public static Ray GetMouseRayAgainstMainCamera()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        // ����ɵ�ƣ����ֳɵ�LerpAngle����
        /// <summary>
        /// ��angle1��ת��angle2�����������0~360֮��
        /// </summary>
        //public static float LerpAngle(float angle1, float angle2, float value)
        //{
        //    value = Mathf.Clamp01(value);
        //    angle1 = Mathf.Clamp(value, 0, 360);
        //    angle2 = Mathf.Clamp(value, 0, 360);
        //    if (Mathf.Abs(angle1 - angle2) <= 180)
        //    {
        //        if (angle2 < angle1) Swap(ref angle1, ref angle2);
                
        //    }
        //}
        public static void Swap<T>(ref T lhs, ref T rhs)
        {
            var temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
        /// <summary>
        /// ������modulo��������Ǹ���
        /// https://github.com/dotnet/csharplang/discussions/4744
        /// </summary>
        public static float Mod(float a, float b)
        {
            float c = a % b;
            if ((c < 0 && b > 0) || (c > 0 && b < 0))
            {
                c += b;
            }
            return c;
        }
    }
}
