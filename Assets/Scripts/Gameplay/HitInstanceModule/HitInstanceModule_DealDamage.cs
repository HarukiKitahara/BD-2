using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Gameplay
{
    public class HitInstanceModule_DealDamage : IHitInstanceModule
    {
        public void OnHit(HitInstanceProperty hitInstanceProperty, CharacterEntityController characterEntityController)
        {
            characterEntityController.AttributeController.health.TryChangeValue(-10f);
        }
    }
}
