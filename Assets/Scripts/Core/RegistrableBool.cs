using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyProject.Core
{
    /// <summary>
    /// ����ע��tokenϵͳ��Boolean������ע��ʱΪtrue������ע��Ϊfalse
    /// </summary>ͬʱ����token�����Դ�Ϊƾ֤���������������������������ˣ��ǻ���false��
    public class RegistrableBool
    {
        public bool Value => !(tokens.Count == 0 ^ _unregisteredIsTrue);
        public event Action OnValueChanged;
        private readonly List<Guid> tokens = new();
        private bool _unregisteredIsTrue;
        private RegistrableBool() { }
        public RegistrableBool(bool unregisteredIsTrue)
        {
            _unregisteredIsTrue = unregisteredIsTrue;
        }

        ///<summary>ע�ἴ�����������Կ��token</summary>
        public Guid Register()
        {
            var token = Guid.NewGuid();
            tokens.Add(token);
            if (tokens.Count == 1) OnValueChanged?.Invoke();
            return token;
        }
        ///<summary>��ֹ�����˻�token�����봫ref�����������������ó�default</summary>
        public void Deregister(ref Guid token)
        {
            if (tokens.Remove(token))
                if (tokens.Count == 0) OnValueChanged?.Invoke();
            token = default;
        }
    }
}
