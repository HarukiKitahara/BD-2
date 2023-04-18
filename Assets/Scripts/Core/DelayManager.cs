using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyProject.Core
{
    public class DelayManager : MonoBehaviourSingletonBase<DelayManager>
    {
        /// <summary>
        /// �ӳ�һ��ʱ���Invoke Action
        /// </summary>
        /// <param name="action"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public Coroutine DelayInvoke(Action action, float time)
        {
            return StartCoroutine(DelayInvokeCoroutine());
            IEnumerator DelayInvokeCoroutine()
            {
                yield return new WaitForSeconds(time);
                action.Invoke();
            }
        }
    }
}
