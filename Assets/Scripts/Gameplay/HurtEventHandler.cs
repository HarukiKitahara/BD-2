using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyProject.Gameplay
{
    public class HurtEventHandler
    {
        private struct HurtArgs
        {
            public HitInstanceProperty hitInstanceProperty;
            public float time;
            public HurtArgs(HitInstanceProperty hitboxProperty, float time)
            {
                this.hitInstanceProperty = hitboxProperty;
                this.time = time;
            }
        }
        private readonly Dictionary<Collider, HurtArgs> hurtRecords = new Dictionary<Collider, HurtArgs>();
        private CharacterEntityController _characterEntityController;
        public HurtEventHandler(CharacterEntityController characterEntityController)
        {
            _characterEntityController = characterEntityController;
        }

        public void HandelHurtEvent(Collider collision)
        {
            if (!hurtRecords.ContainsKey(collision))     // 先检查缓存，看有没有挨过打
            {
                if (collision.gameObject.name.Contains("HitInstance"))  // 先看看是不是HitInstance
                {
                    var hitInstanceProperty = collision.GetComponent<HitInstanceProperty>();
                    if(hitInstanceProperty!=null && CheckIFF(hitInstanceProperty, _characterEntityController))  // 再看看满不满足敌我判定条件
                    {
                        hurtRecords.Add(collision, new HurtArgs(hitInstanceProperty, Time.time));  
                        HandleHit(collision);
                    }
                }
                return;
            }
            if (hurtRecords[collision].time + hurtRecords[collision].hitInstanceProperty.triggerInterval < Time.time) HandleHit(collision);
        }
        /// <summary>阵营检查，有效受击返回true</summary>
        private bool CheckIFF(HitInstanceProperty hitInstanceProperty, CharacterEntityController characterEntityController)
        {
            if (hitInstanceProperty.iff == EHitInstanceIFF.friendOrFoe) return true;    // 无论敌我就直接打
            if (hitInstanceProperty.iff == EHitInstanceIFF.none) return false;
            var isFriend = hitInstanceProperty.gameObject.CompareTag(characterEntityController.gameObject.tag);
            if (hitInstanceProperty.iff == EHitInstanceIFF.friend) return isFriend; else return !isFriend;  // 索定队友就打队友
        }

        private void HandleHit(Collider collision)
        {
            
            var hitInstanceProperty = hurtRecords[collision].hitInstanceProperty;
            hurtRecords[collision] = new HurtArgs(hitInstanceProperty, Time.time);
            foreach(var module in hitInstanceProperty.modules)
            {
                module.OnHit(_characterEntityController);
            }
        }
    }
}

