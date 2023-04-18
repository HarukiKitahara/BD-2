using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyProject.Core
{
    /// <summary>
    /// 含有注册token系统的Boolean，无人注册时为true，有人注册为false
    /// </summary>同时发个token，可以此为凭证解除禁令（如果解除后还有其他人上锁了，那还是false）
    public class RegistrableBool
    {
        public bool Value => tokens.Count == 0;
        public event Action OnValueChanged;

        private readonly List<Guid> tokens = new();
        ///<summary>注册即上锁，送你把钥匙token</summary>
        public Guid Register()
        {
            var token = Guid.NewGuid();
            tokens.Add(token);
            if (tokens.Count == 1) OnValueChanged?.Invoke();
            return token;
        }
        ///<summary>防止你忘了还token，必须传ref进来，我来帮你设置成default</summary>
        public void Deregister(ref Guid token)
        {
            if (tokens.Remove(token))
                if (tokens.Count == 0) OnValueChanged?.Invoke();
            token = default;
        }
    }
}
