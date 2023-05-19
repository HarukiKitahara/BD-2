using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace MyProject.Gameplay
{
    public class HitInstanceModule_DealDamage : IHitInstanceModule
    {
        public static event Action<Transform, float> OnDamageDealt;
        public void OnHit(HitInstanceProperty hitInstanceProperty, CharacterEntityController characterEntityController)
        {
            characterEntityController.AttributeController.health.TryChangeValue(-10f);
            OnDamageDealt?.Invoke(characterEntityController.transform, 10f);
        }
    }
}
