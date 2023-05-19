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
        private Vector3 _keepMoveDirection;     // ��֤Ҫô��zero��Ҫô��normalized
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
        /// һ���趨���ͻ᳢�Ա����ٶȷ���ֱ��direction��Ϊzero�����ı�Stance���ٶ���Update�ﴦ������ֹһ֡���ظ����ã�
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
            if (StanceCategory == EStanceCategory.normal) _rotationController.SetDesiredRotation(direction);    // ��̬�����ƶ�����
        }

        public void StopMoving()
        {
            _keepMoveDirection = Vector3.zero;
        }
        // �ⲿ���볯������DesiredRotation���˳��ܲ���̬
        public void TrySetLookAtRotation(Quaternion rotation)
        {
            if (StanceCategory != EStanceCategory.battle) return; // ֻ��Battle״̬�����ֶ�ָ������
            _rotationController.SetDesiredRotation(rotation);
        }
        /// <summary>
        /// ���û���ܲ����ͽ����ܲ�״̬������ȡ������״̬������Ѿ������ˣ��Ǿͱ��������·��
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
