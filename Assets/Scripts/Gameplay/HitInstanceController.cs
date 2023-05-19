using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Gameplay
{
    public class HitInstanceController : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 2f;
        [SerializeField] private float _delayDestroyTime = 0.5f;
        [SerializeField] private ParticleSystem _vfx;

        private float _startTime;
        private Collider _collider;
        private void Start()
        {
            _startTime = Time.time;
            _collider = GetComponent<Collider>();
        }
        private void Update()
        {
            if (Time.time > _startTime + _lifeTime )
            {
                _collider.enabled = false;
                Destroy(gameObject);
                return;
            }
            if (Time.time > _startTime + _lifeTime - _delayDestroyTime)
            {
                _vfx.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }
    }
}
