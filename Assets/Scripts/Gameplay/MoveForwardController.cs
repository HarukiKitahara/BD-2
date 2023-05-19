using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Gameplay
{
    public class MoveForwardController : MonoBehaviour
    {
        [SerializeField]
        private float _desiredVelocity = 1f;
        [SerializeField]
        private float _randomVelocityHalfWidth = 0f;
        [SerializeField]
        private float _decayPerSecond = 0f;

        private float _velocity;

        private void Start()
        {
            _velocity = Random.Range(-_randomVelocityHalfWidth, _randomVelocityHalfWidth) + _desiredVelocity;
        }
        private void Update()
        {
            transform.position = transform.position + _velocity * Time.deltaTime * transform.forward;
            _velocity -= _decayPerSecond * Time.deltaTime;
        }
    }
}

