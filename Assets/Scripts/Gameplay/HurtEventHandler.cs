using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyProject.Gameplay
{
    public class HurtEventHandler
    {
        private CharacterEntityController _characterEntityController;
        public HurtEventHandler(CharacterEntityController characterEntityController)
        {
            _characterEntityController = characterEntityController;
        }
        /// <summary>判断是不是真的发生了受击事件。如果是，再分发处理。</summary>
        public void TryHandleHurtEvent(Collider collision)
        {
            if (!collision.gameObject.name.Contains("HitInstance")) return;     // 先看是不是HitInstance
            var hitInstanceProperty = collision.GetComponent<HitInstanceProperty>();
            if (hitInstanceProperty == null) return;        // 再看是不是真的有HitInstanceProperty
            if (!CheckIFF(hitInstanceProperty, _characterEntityController)) return;     // 然后进行敌我识别

            if (!hitInstanceProperty.timestamps.ContainsKey(this))      // 如果是第一次挨打，就注册时间戳
            {
                hitInstanceProperty.timestamps.Add(this, Time.time);
                HandleHurtEvent(hitInstanceProperty);
                return;
            }

            if (hitInstanceProperty.timestamps[this] + hitInstanceProperty.triggerInterval <= Time.time)    // 如果已经挨过打，就看时间间隔够不够
            {
                hitInstanceProperty.timestamps[this] = Time.time;
                HandleHurtEvent(hitInstanceProperty);
                return;
            }
        }
        /// <summary>阵营检查，如果是有效受击则返回true</summary>
        private bool CheckIFF(HitInstanceProperty hitInstanceProperty, CharacterEntityController characterEntityController)
        {
            if (hitInstanceProperty.iff == EHitInstanceIFF.friendOrFoe) return true;    // 无论敌我就直接打
            if (hitInstanceProperty.iff == EHitInstanceIFF.none) return false;
            var isFriend = hitInstanceProperty.gameObject.CompareTag(characterEntityController.gameObject.tag);
            //Debug.Log($"{isFriend}, {characterEntityController.gameObject.tag}");
            if (hitInstanceProperty.iff == EHitInstanceIFF.friend) return isFriend; else return !isFriend;  // 索定队友就打队友
        }
        /// <summary>确定发生受击事件，开始依次处理OnHit请求</summary>
        private void HandleHurtEvent(HitInstanceProperty hitInstanceProperty)
        {
            // 其实应该把时间更新也放在这的，但是要重复判断，所以还是写在try的最后了
            foreach(var module in hitInstanceProperty.modules)
            {
                module.OnHit(hitInstanceProperty, _characterEntityController);
            }
        }
    }
}

