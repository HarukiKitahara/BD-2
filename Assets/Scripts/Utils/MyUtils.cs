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
    }
}
