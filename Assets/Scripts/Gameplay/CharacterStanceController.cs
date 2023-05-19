using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace MyProject.Gameplay
{
    public enum EStanceCategory { normal, battle }
    public class CharacterStanceController : MonoBehaviour
    {
        private const float STANCE_SWITCH_COOLDOWN = 1f;
        private const float ATTACK_COOLDOWN = 1f;
        [SerializeField] private float _battleSpeed = 2f;
        [SerializeField] private float _normalSpeed = 4f;
        public float BattleSpeed => _battleSpeed;
        private CharacterMovementController _movementController;
        private CharacterRotationController _rotationController;
        private Vector3 _keepMoveDirection;     // 保证要么是zero，要么被normalized
        public EStanceCategory StanceCategory { get; private set; }
        public Vector3 Velocity => _movementController.LastFrameVelocity;

        private float _lastAttackTime;
        public event Action OnAttack, OnStanceChanged;
        private void Start()
        {
            _movementController = GetComponent<CharacterMovementController>();
            _rotationController = GetComponent<CharacterRotationController>();
            StanceCategory = EStanceCategory.normal;
        }
        private void Update()
        {
            if (_keepMoveDirection != Vector3.zero)
            {
                if (StanceCategory == EStanceCategory.battle)
                {
                    _movementController.SetVelocity(_keepMoveDirection.normalized * _battleSpeed);
                }
                else
                {
                    _movementController.SetVelocity(_keepMoveDirection.normalized * _normalSpeed);
                }
            }
        }
        /// <summary>
        /// 一旦设定，就会尝试保持速度方向，直到direction设为zero。不改变Stance，速度在Update里处理。（防止一帧内重复设置）
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
            if (StanceCategory == EStanceCategory.normal) _rotationController.SetDesiredRotation(direction);    // 常态朝向移动方向
        }

        public void StopMoving()
        {
            _keepMoveDirection = Vector3.zero;
        }
        // 外部输入朝向，设置DesiredRotation并退出跑步姿态
        public void TrySetLookAtRotation(Quaternion rotation)
        {
            if (StanceCategory != EStanceCategory.battle) return; // 只有Battle状态才能手动指定朝向
            _rotationController.SetDesiredRotation(rotation);
        }
        /// <summary>
        /// 如果没在跑步，就进入跑步状态，并且取消锁定状态；如果已经在跑了，那就变成正常走路。
        /// </summary>
        public void TryToggleStance()
        {
            if (Time.time < _lastAttackTime + ATTACK_COOLDOWN) return;
            if (StanceCategory == EStanceCategory.battle)
            {
                StanceCategory = EStanceCategory.normal;
            }
            else
            {
                StanceCategory = EStanceCategory.battle;
            }
            OnStanceChanged?.Invoke();
        }
        public void TryAttack()
        {
            if (Time.time > _lastAttackTime + ATTACK_COOLDOWN)
            {
                if (StanceCategory != EStanceCategory.battle) TryToggleStance();
                if (StanceCategory != EStanceCategory.battle) return;
                TrySetLookAtRotation(transform.rotation);
                OnAttack?.Invoke();
                _lastAttackTime = Time.time;
            }
        }
    }
}
