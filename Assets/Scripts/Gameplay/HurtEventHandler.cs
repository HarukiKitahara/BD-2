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
        /// <summary>�ж��ǲ�����ķ������ܻ��¼�������ǣ��ٷַ�����</summary>
        public void TryHandleHurtEvent(Collider collision)
        {
            if (!collision.gameObject.name.Contains("HitInstance")) return;     // �ȿ��ǲ���HitInstance
            var hitInstanceProperty = collision.GetComponent<HitInstanceProperty>();
            if (hitInstanceProperty == null) return;        // �ٿ��ǲ��������HitInstanceProperty
            if (!CheckIFF(hitInstanceProperty, _characterEntityController)) return;     // Ȼ����е���ʶ��

            if (!hitInstanceProperty.timestamps.ContainsKey(this))      // ����ǵ�һ�ΰ��򣬾�ע��ʱ���
            {
                hitInstanceProperty.timestamps.Add(this, Time.time);
                HandleHurtEvent(hitInstanceProperty);
                return;
            }

            if (hitInstanceProperty.timestamps[this] + hitInstanceProperty.triggerInterval <= Time.time)    // ����Ѿ������򣬾Ϳ�ʱ����������
            {
                hitInstanceProperty.timestamps[this] = Time.time;
                HandleHurtEvent(hitInstanceProperty);
                return;
            }
        }
        /// <summary>��Ӫ��飬�������Ч�ܻ��򷵻�true</summary>
        private bool CheckIFF(HitInstanceProperty hitInstanceProperty, CharacterEntityController characterEntityController)
        {
            if (hitInstanceProperty.iff == EHitInstanceIFF.friendOrFoe) return true;    // ���۵��Ҿ�ֱ�Ӵ�
            if (hitInstanceProperty.iff == EHitInstanceIFF.none) return false;
            var isFriend = hitInstanceProperty.gameObject.CompareTag(characterEntityController.gameObject.tag);
            //Debug.Log($"{isFriend}, {characterEntityController.gameObject.tag}");
            if (hitInstanceProperty.iff == EHitInstanceIFF.friend) return isFriend; else return !isFriend;  // �������Ѿʹ����
        }
        /// <summary>ȷ�������ܻ��¼�����ʼ���δ���OnHit����</summary>
        private void HandleHurtEvent(HitInstanceProperty hitInstanceProperty)
        {
            // ��ʵӦ�ð�ʱ�����Ҳ������ģ�����Ҫ�ظ��жϣ����Ի���д��try�������
            foreach(var module in hitInstanceProperty.modules)
            {
                module.OnHit(hitInstanceProperty, _characterEntityController);
            }
        }
    }
}

