using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace MyProject.Core
{
    /// <summary>
    /// ����Mono�����Ļ���
    /// </summary>
    public abstract class MonoBehaviourSingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        GameObject go = new(typeof(T).ToString());
                        _instance = go.AddComponent<T>();
                        Debug.Log($"������û��{typeof(T)}�����������ǳ��Ե��ã������Զ����ɡ�");
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
            }
            //DontDestroyOnLoad(gameObject);
            InitOnAwake();
        }

        protected virtual void InitOnAwake()
        {

        }
    }
}
