using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace MyProject.Core
{
    /// <summary>
    /// 所有Mono单例的基类
    /// </summary>
    public abstract class MonoBehaviourSingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject go = new GameObject(typeof(T).ToString());
                        instance = go.AddComponent<T>();
                        Debug.Log($"场景里没有{typeof(T)}单例，但还是尝试调用，现已自动生成。");
                    }
                }
                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<T>();
            }
            //DontDestroyOnLoad(gameObject);
            InitOnAwake();
        }

        protected virtual void InitOnAwake()
        {

        }
    }
}
