using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Gameplay
{
    public class CharacterAnimatorController : MonoBehaviour
    {
        private enum ECharacterMovementState { idle, walk, run }
        private ECharacterMovementState _characterMovementStateLastFrame;

        private Animator _animator;
        private CharacterMovementController _movementController;
        private CharacterRotationController _rotationController;
        private NavMeshAgentHelper _agentHelper;
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _movementController = GetComponent<CharacterMovementController>();
            _rotationController = GetComponent<CharacterRotationController>();
            _agentHelper = GetComponent<NavMeshAgentHelper>();
        }

        private void Update()
        {
            //Debug.Log($"{_movementController.IsMoving}, {_movementController.IsRunning}");
            if (_movementController.IsMoving)
            {
                if (_movementController.IsRunning)
                {
                    if (_characterMovementStateLastFrame != ECharacterMovementState.run) _animator.SetTrigger("Run");
                    _animator.ResetTrigger("Walk");
                    _animator.ResetTrigger("Idle");
                    _characterMovementStateLastFrame = ECharacterMovementState.run;
                }
                else
                {
                    if (_characterMovementStateLastFrame != ECharacterMovementState.walk) _animator.SetTrigger("Walk");
                    _characterMovementStateLastFrame = ECharacterMovementState.walk;
                    _animator.ResetTrigger("Run");
                    _animator.ResetTrigger("Idle");
                }
            }
            else
            {
                if (_characterMovementStateLastFrame != ECharacterMovementState.idle) _animator.SetTrigger("Idle");
                _characterMovementStateLastFrame = ECharacterMovementState.idle;
                _animator.ResetTrigger("Walk");
                _animator.ResetTrigger("Run");
            }

            if (_rotationController.IsForcingLookAt)
            {
                var velocityDirection = Quaternion.LookRotation(_agentHelper.LastFrameVelocity);
                var locomotionQuaternion = _rotationController.DesiredRotation * velocityDirection;
                var locomotionDirection = (locomotionQuaternion * Vector3.forward).normalized;

                _animator.SetFloat("LocomotionX", locomotionDirection.x);
                _animator.SetFloat("LocomotionY", locomotionDirection.z);
            }
        }
    }
}
