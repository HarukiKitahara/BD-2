using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace MyProject.Core
{
    /// <summary>
    /// 所有c#单例的基类
    /// </summary>
    public abstract class SingletonBase<T> where T : class, new()
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                    Debug.Log($"没有{typeof(T)}单例，但还是尝试调用，现已自动生成。");
                }
                return instance;
            }
        }
    }
}
