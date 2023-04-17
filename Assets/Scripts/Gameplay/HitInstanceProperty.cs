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
        /// <summary>
        /// 受击最小间隔
        /// </summary>
        public float triggerInterval = 0.5f;
        //public float damage = 10f;
        //public float knockbackVelocity = 0;
        //public float knockbackDuration = 0;
        //public float hitStopIntensity = 0f;
        //public GameObject visualEffects;
        public IHitInstanceModule[] modules;


        /// <summary>
        /// TODO：纯测试
        /// </summary>
        private void Start()
        {
            modules = new IHitInstanceModule[1];
            modules[0] = new HitInstanceModule_DealDamage();
        }
    }
}

