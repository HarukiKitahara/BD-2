using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Input
{
    /// <summary>
    /// TODO�����ھ�ֻ��һ�����Ͱѵ����򿪣��Ժ��������л����˵������л���ʱ��Ҫ��չ������ΪInputSwitcher
    /// </summary>
    public class PlayerInputInitializer : MonoBehaviour
    {
        void Start()
        {
            InputActionAssetSingleton.Instance.Enable();
        }
    }
}
