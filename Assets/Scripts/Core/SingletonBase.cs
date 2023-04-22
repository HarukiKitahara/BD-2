using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace MyProject.Core
{
    /// <summary>
    /// ����c#�����Ļ���
    /// </summary>
    public abstract class SingletonBase<T> where T : class, new()
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                    Debug.Log($"û��{typeof(T)}�����������ǳ��Ե��ã������Զ����ɡ�");
                }
                return _instance;
            }
        }
    }
}
