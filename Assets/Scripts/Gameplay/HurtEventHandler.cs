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
            if (!hurtRecords.ContainsKey(collision))     // �ȼ�黺�棬����û�а�����
            {
                if (collision.gameObject.name.Contains("HitInstance"))  // �ȿ����ǲ���HitInstance
                {
                    var hitInstanceProperty = collision.GetComponent<HitInstanceProperty>();
                    if(hitInstanceProperty!=null && CheckIFF(hitInstanceProperty, _characterEntityController))  // �ٿ���������������ж�����
                    {
                        hurtRecords.Add(collision, new HurtArgs(hitInstanceProperty, Time.time));  
                        HandleHit(collision);
                    }
                }
                return;
            }
            if (hurtRecords[collision].time + hurtRecords[collision].hitInstanceProperty.triggerInterval < Time.time) HandleHit(collision);
        }
        /// <summary>��Ӫ��飬��Ч�ܻ�����true</summary>
        private bool CheckIFF(HitInstanceProperty hitInstanceProperty, CharacterEntityController characterEntityController)
        {
            if (hitInstanceProperty.iff == EHitInstanceIFF.friendOrFoe) return true;    // ���۵��Ҿ�ֱ�Ӵ�
            if (hitInstanceProperty.iff == EHitInstanceIFF.none) return false;
            var isFriend = hitInstanceProperty.gameObject.CompareTag(characterEntityController.gameObject.tag);
            if (hitInstanceProperty.iff == EHitInstanceIFF.friend) return isFriend; else return !isFriend;  // �������Ѿʹ����
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

