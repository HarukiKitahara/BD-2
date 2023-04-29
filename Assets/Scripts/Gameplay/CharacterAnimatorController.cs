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
        private CharacterStanceController _stanceController;
        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _stanceController = GetComponent<CharacterStanceController>();
            _stanceController.OnAttack += OnAttack;
        }

        private void Update()
        {
            if (_stanceController.Velocity.magnitude == 0)
            {
                _animator.SetTrigger("Idle");
                _animator.ResetTrigger("Walk");
                _animator.ResetTrigger("Run");
            }
            else
            {
                if (_stanceController.StanceCategory == EStanceCategory.run)
                {
                    _animator.SetTrigger("Run");
                    _animator.ResetTrigger("Idle");
                    _animator.ResetTrigger("Walk");
                    return;
                }

                _animator.SetTrigger("Walk");
                _animator.ResetTrigger("Idle");
                _animator.ResetTrigger("Run");

                var velocity = _stanceController.Velocity;
                var forwardDirection = transform.forward;
                var locomotionDirection = Quaternion.FromToRotation(forwardDirection, velocity) * Vector3.forward;
                //Debug.Log($"velocity: {velocity}, forwardDirection: {forwardDirection}, locomotionDirection: {locomotionDirection}");
                _animator.SetFloat("LocomotionX", locomotionDirection.x);
                _animator.SetFloat("LocomotionY", locomotionDirection.z);
            }
        }
        private void OnAttack()
        {
            _animator.SetTrigger("Attack");
        }
    }
}
