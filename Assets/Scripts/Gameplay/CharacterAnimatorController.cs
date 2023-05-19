using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Gameplay
{
    public class CharacterAnimatorController : MonoBehaviour
    {
        private enum ECharacterMovementState { idle, walk, run }
        private float NormalizedSpeedRelativeToBattle => _stanceController.Velocity.magnitude / _stanceController.BattleSpeed;
        private Animator _animator;
        private CharacterStanceController _stanceController;
        [SerializeField] private GameObject _weaponModelOnHand, _weaponModelOnBack; 
        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _stanceController = GetComponent<CharacterStanceController>();
            _stanceController.OnAttack += OnAttack;
            SetModelState(_stanceController.StanceCategory);
            _stanceController.OnStanceChanged += () => SetModelState(_stanceController.StanceCategory);
        }

        private void Update()
        {
            _animator.SetFloat("Speed", NormalizedSpeedRelativeToBattle);
            if (_stanceController.StanceCategory == EStanceCategory.battle)
            {
                var velocity = _stanceController.Velocity;
                var forwardDirection = transform.forward;
                var locomotionDirection = Quaternion.FromToRotation(forwardDirection, velocity) * Vector3.forward;
                //Debug.Log($"velocity: {velocity}, forwardDirection: {forwardDirection}, locomotionDirection: {locomotionDirection}");
                _animator.SetFloat("LocomotionX", locomotionDirection.x);
                _animator.SetFloat("LocomotionY", locomotionDirection.z);
            }
            //if (_stanceController.Velocity.magnitude == 0)
            //{
            //    _animator.SetTrigger("Idle");
            //    _animator.ResetTrigger("Walk");
            //    _animator.ResetTrigger("Run");
            //}
            //else
            //{
            //    if (_stanceController.StanceCategory == EStanceCategory.battle)
            //    {
            //        _animator.SetTrigger("Run");
            //        _animator.ResetTrigger("Idle");
            //        _animator.ResetTrigger("Walk");
            //        return;
            //    }

            //    _animator.SetTrigger("Walk");
            //    _animator.ResetTrigger("Idle");
            //    _animator.ResetTrigger("Run");

            //    var velocity = _stanceController.Velocity;
            //    var forwardDirection = transform.forward;
            //    var locomotionDirection = Quaternion.FromToRotation(forwardDirection, velocity) * Vector3.forward;
            //    //Debug.Log($"velocity: {velocity}, forwardDirection: {forwardDirection}, locomotionDirection: {locomotionDirection}");
            //    _animator.SetFloat("LocomotionX", locomotionDirection.x);
            //    _animator.SetFloat("LocomotionY", locomotionDirection.z);
            //}
        }
        private void OnAttack()
        {
            _animator.SetTrigger("Attack");
        }
        private void SetModelState(EStanceCategory stanceCategory)
        {
            if(_weaponModelOnBack != null && _weaponModelOnHand != null)
            if (stanceCategory == EStanceCategory.battle)
            {
                _weaponModelOnHand.SetActive(true);
                _weaponModelOnBack.SetActive(false);
            }
            else
            {
                _weaponModelOnHand.SetActive(false);
                _weaponModelOnBack.SetActive(true);
            }
        }
    }
}
