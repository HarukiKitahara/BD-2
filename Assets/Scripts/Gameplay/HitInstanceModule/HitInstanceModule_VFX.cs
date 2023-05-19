using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Gameplay
{
    public class HitInstanceModule_VFX : IHitInstanceModule
    {
        private GameObject _vfx;
        public HitInstanceModule_VFX(GameObject vfx)
        {
            _vfx = vfx;
        }
        public void OnHit(HitInstanceProperty hitInstanceProperty, CharacterEntityController characterEntityController)
        {
            if (_vfx != null)
            {
                Object.Instantiate(_vfx, characterEntityController.transform.position, characterEntityController.transform.rotation);
            }
        }
    }
}
