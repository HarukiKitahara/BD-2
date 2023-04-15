using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace MyProject.Gameplay
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshAgentHelper : MonoBehaviour
    {
        private NavMeshAgent _agent;
        public NavMeshAgent Agent => _agent;
        private bool _isMoving;
        public bool IsMoving => _isMoving;
        private Vector3 _lastFrameDirection;
        public Vector3 LastFrameDirection => _lastFrameDirection;
        public bool UpdateRotation { get => _agent.updateRotation; set => _agent.updateRotation = value; }
        //private bool _isKeepingVelocity = false;
        //private Vector2 _velocityKept = Vector2.zero;
        //private float _keepVelocityUntilTime;
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            //_agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _lastFrameDirection = Vector3.forward;
            //_lastFrameDirection = Vector2.up;
        }
        private void FixedUpdate()
        {
            _isMoving = _agent.velocity.magnitude != 0;
        }
        private void LateUpdate()
        {
            if (_agent.velocity != Vector3.zero) _lastFrameDirection = _agent.velocity.normalized;
            //Debug.Log($"LateUpdate: _lastFrameVelocity: {_lastFrameVelocity},_agent.velocity: {_agent.velocity}");
        }
        //private void LateUpdate()
        //{
        //    if (_agent.velocity.ToVector2() != Vector2.zero) _lastFrameDirection = _agent.velocity;

        //    if (_isKeepingVelocity)
        //    {
        //        _agent.velocity = _velocityKept;
        //        //Debug.Log(_agent.velocity + "Should be kept at" + _velocityKept);
        //        if (Time.time > _keepVelocityUntilTime)
        //        {
        //            _isKeepingVelocity = false;
        //        }
        //    }
        //}

        //private void Update()
        //{
        //    if (_isKeepingVelocity)
        //    {
        //        _agent.velocity = _velocityKept;
        //        //Debug.Log(_agent.velocity + "Should be kept at" + _velocityKept);
        //        if (Time.time > _keepVelocityUntilTime)
        //        {
        //            _isKeepingVelocity = false;
        //        }
        //    }
        //}

        //public void KeepVelocity(Vector2 velocity, float time)
        //{
        //    _velocityKept = velocity;
        //    _keepVelocityUntilTime = Time.time + time;
        //    _isKeepingVelocity = true;
        //}
    }
}
