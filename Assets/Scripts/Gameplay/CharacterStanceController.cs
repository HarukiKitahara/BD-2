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
        private Vector3 _keepMoveDirection;     // ��֤Ҫô��zero��Ҫô��normalized
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
        /// һ���趨���ͻ᳢�Ա����ٶȷ���ֱ��direction��Ϊzero�����ı�Stance
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
        // �ⲿ���볯������DesiredRotation���˳��ܲ���̬
        public void SetLookAtRotation(Quaternion rotation)
        {
            if (StanceCategory == EStanceCategory.run) StanceCategory = EStanceCategory.walk;   // ��Ҫ���������Ͳ����ܲ�
            _rotationController.SetDesiredRotation(rotation);
            IsLookingAt = true;
        }
        public void StopLookAtRotation()
        {
            IsLookingAt = false;
        }
        /// <summary>
        /// ���û���ܲ����ͽ����ܲ�״̬������ȡ������״̬������Ѿ������ˣ��Ǿͱ��������·��
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
                if (IsLookingAt) StopLookAtRotation();  // �е��ܲ�״̬�Ͳ����ٶ���������
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
