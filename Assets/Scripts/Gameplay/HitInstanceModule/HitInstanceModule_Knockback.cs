using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Core;

namespace MyProject.Gameplay
{
    /*
     * p = mv, ��v=-p/m, �����Ŀ�����ʧһ���ٶ�
     * ��׼��������50kg�������ٶ�1unit/s
     * ���50�������Խ��ƶ��е�����ѡ�ִ�ͣ����
     */
    public class HitInstanceModule_Knockback : IHitInstanceModule
    {
        public float momentumMagnitude = 500f;
        public float duration = 0.5f;
        public void OnHit(HitInstanceProperty hitInstanceProperty, CharacterEntityController characterEntityController)
        {
            // ����
            var direction = (characterEntityController.transform.position - hitInstanceProperty.transform.position).normalized;
            characterEntityController.ApplyImpluseMomentum(direction * momentumMagnitude);  

            // ���ٶȡ�����
            var tokenRotation = characterEntityController.CharacterRotationController.LockRotation();
            var tokenVelocity = characterEntityController.CharacterMovementController.CanSetVelocity.Register();

            // �������
            DelayManager.Instance.DelayInvoke(() =>
                {
                    characterEntityController.CharacterRotationController.UnlockRotation(ref tokenRotation);
                    characterEntityController.CharacterMovementController.CanSetVelocity.Deregister(ref tokenVelocity);
                }, duration);
        }
    }
}
