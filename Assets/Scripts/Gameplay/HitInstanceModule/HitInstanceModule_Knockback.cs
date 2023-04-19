using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Core;

namespace MyProject.Gameplay
{
    /*
     * p = mv, δv=-p/m, 挨打的目标会损失一定速度
     * 标准人型生物50kg，步行速度1unit/s
     * 因此50动量可以将移动中的人形选手打停下来
     */
    public class HitInstanceModule_Knockback : IHitInstanceModule
    {
        public float momentumMagnitude = 500f;
        public float duration = 0.5f;
        public void OnHit(HitInstanceProperty hitInstanceProperty, CharacterEntityController characterEntityController)
        {
            // 击退
            var direction = (characterEntityController.transform.position - hitInstanceProperty.transform.position).normalized;
            characterEntityController.ApplyImpluseMomentum(direction * momentumMagnitude);  

            // 锁速度、朝向
            var tokenRotation = characterEntityController.CharacterRotationController.LockRotation();
            var tokenVelocity = characterEntityController.CharacterMovementController.CanSetVelocity.Register();

            // 解除锁定
            DelayManager.Instance.DelayInvoke(() =>
                {
                    characterEntityController.CharacterRotationController.UnlockRotation(ref tokenRotation);
                    characterEntityController.CharacterMovementController.CanSetVelocity.Deregister(ref tokenVelocity);
                }, duration);
        }
    }
}
