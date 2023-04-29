using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace MyProject.Gameplay
{
    public enum EStanceCategory { walk, run, crouch }
    public class CharacterStanceController : MonoBehaviour
    {
        private const float ATTACK_COOLDOWN = 1f;
        [SerializeField] private float _walkSpeed = 2f;
        [SerializeField] private float _runSpeed = 4f;
        private CharacterMovementController _movementController;
        private CharacterRotationController _rotationController;
        private Vector3 _keepMoveDirection;     // 保证要么是zero，要么被normalized
        public EStanceCategory StanceCategory { get; private set; }
        public bool IsLookingAt { get; private set; }
        public Vector3 Velocity => _movementController.LastFrameVelocity;

        private float _lastAttackTime;
        public event Action OnAttack;
        private void Start()
        {
            _movementController = GetComponent<CharacterMovementController>();
            _rotationController = GetComponent<CharacterRotationController>();
        }
        private void Update()
        {
            if (_keepMoveDirection != Vector3.zero)
            {
                if (StanceCategory == EStanceCategory.run)
                {
                    _movementController.SetVelocity(_keepMoveDirection.normalized * _runSpeed);
                }
                else
                {
                    _movementController.SetVelocity(_keepMoveDirection.normalized * _walkSpeed);
                }
            }
        }
        /// <summary>
        /// 一旦设定，就会尝试保持速度方向，直到direction设为zero，不改变Stance
        /// </summary>
        /// <param name="direction"></param>
        public void SetMoveDirection(Vector3 direction)
        {
            if (direction == Vector3.zero)
            {
                StopMoving();
                return;
            }
            _keepMoveDirection = direction.normalized;
            if (!IsLookingAt) _rotationController.SetDesiredRotation(direction);
        }
        public void StopMoving()
        {
            _keepMoveDirection = Vector3.zero;
        }
        // 外部输入朝向，设置DesiredRotation并退出跑步姿态
        public void SetLookAtRotation(Quaternion rotation)
        {
            if (StanceCategory == EStanceCategory.run) StanceCategory = EStanceCategory.walk;   // 想要东张西望就不能跑步
            _rotationController.SetDesiredRotation(rotation);
            IsLookingAt = true;
        }
        public void StopLookAtRotation()
        {
            IsLookingAt = false;
        }
        /// <summary>
        /// 如果没在跑步，就进入跑步状态，并且取消锁定状态；如果已经在跑了，那就变成正常走路。
        /// </summary>
        public void TryToggleRunState()
        {
            if (Time.time < _lastAttackTime + ATTACK_COOLDOWN) return;
            if (StanceCategory == EStanceCategory.run)
            {
                StanceCategory = EStanceCategory.walk;
            }
            else
            {
                StanceCategory = EStanceCategory.run;
                if (IsLookingAt) StopLookAtRotation();  // 切到跑步状态就不能再东张西望了
            }
        }
        public void TryAttack()
        {
            if (Time.time > _lastAttackTime + ATTACK_COOLDOWN)
            {
                SetLookAtRotation(transform.rotation);
                OnAttack?.Invoke();
                _lastAttackTime = Time.time;
            }
        }
    }
}
