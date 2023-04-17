using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Gameplay
{
    public interface IHitInstanceModule
    {
        public abstract void OnHit(CharacterEntityController characterEntityController);
    }
}
