using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Input
{
    /// <summary>
    /// TODO！现在就只是一上来就把单例打开，以后做场景切换、菜单输入切换的时候要拓展、升级为InputSwitcher
    /// </summary>
    public class PlayerInputInitializer : MonoBehaviour
    {
        void Start()
        {
            InputActionAssetSingleton.Instance.Enable();
        }
    }
}
