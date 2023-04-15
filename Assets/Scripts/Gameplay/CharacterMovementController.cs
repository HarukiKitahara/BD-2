using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyProject.Gameplay
{
    public class CharacterMovementController : MonoBehaviour
    {
        [SerializeField] private float _baseSpeed = 3f;
        [SerializeField] private float _sprintSpeedMultiplier = 3f;
        //[SerializeField] private float _sprintStaminaCostPerSecond = 40f;

        private Vector3 _direction;

        private NavMeshAgentHelper _agentHelper;

        private bool _canRun = true;
        private bool _canMove = true;

        private bool _isRunning = false;
        public bool IsRunning => _isRunning;
        public bool IsMoving => _agentHelper.IsMoving;
        private void Start()
        {
            _agentHelper = GetComponent<NavMeshAgentHelper>();
        }
        private void Update()
        {
            if (!_canMove) return;
            var actualSpeed = _baseSpeed;
            if (_isRunning && _canRun)      // 满足不了条件就只能走
            {
                actualSpeed = _baseSpeed * _sprintSpeedMultiplier;
            }
            _agentHelper.Agent.velocity = _direction * actualSpeed;
            //Debug.Log($"PreferedDirection: {_direction * actualSpeed}, ActualVelocity: {_agentHelper.Agent.velocity}");
        }
        public void TryMoveToward(Vector3 direction)
        {
            _direction = direction.normalized;
        }
        public void TryRun(bool state)
        {
            _isRunning = state;
        }
    }
}
