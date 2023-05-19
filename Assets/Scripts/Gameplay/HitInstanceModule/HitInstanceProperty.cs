using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyProject.Gameplay
{
    /// <summary>
    /// Identification, friend or foe
    /// </summary>
    public enum EHitInstanceIFF 
    { 
        none = 0,
        friend = 1,
        foe = 2,
        friendOrFoe = 4
    }
    public class HitInstanceProperty : MonoBehaviour
    {
        public EHitInstanceIFF iff = EHitInstanceIFF.foe;
        /// <summary>最小受击间隔</summary>
        public float triggerInterval = 0.5f;
        public GameObject vfx;
        //public float hitStopIntensity = 0f;
        //public GameObject visualEffects;
        public IHitInstanceModule[] modules;
        public readonly Dictionary<HurtEventHandler, float> timestamps = new();

        /// <summary>
        /// TODO：纯测试
        /// </summary>
        private void Start()
        {
            modules = new IHitInstanceModule[3];
            modules[0] = new HitInstanceModule_DealDamage();
            modules[1] = new HitInstanceModule_Knockback();
            modules[2] = new HitInstanceModule_VFX(vfx);
        }
    }
}

