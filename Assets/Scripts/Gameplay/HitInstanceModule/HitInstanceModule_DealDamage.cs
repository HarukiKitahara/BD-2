using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Gameplay
{
    public class HitInstanceModule_DealDamage : IHitInstanceModule
    {
        public void OnHit(CharacterEntityController characterEntityController)
        {
            characterEntityController.AttributeController.Health.TryChangeValue(-10f);
        }
    }
}
