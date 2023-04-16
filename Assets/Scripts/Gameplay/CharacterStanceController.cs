using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Gameplay
{
    public enum EStanceCategory { walk, run, crouch }
    public class CharacterStanceController : MonoBehaviour
    {
        [SerializeField] private float _walkSpeed = 3f;
        [SerializeField] private float _runSpeed = 6f;
        private CharacterMovementController _movementController;
        private Vector3 _keepMoveDirection;     // ��֤Ҫô��zero��Ҫô��normalized
        public EStanceCategory StanceCategory { get; private set; }
        public bool IsForcingLookingAt => !_movementController.UpdateRotation;  // NavMesh��ת����˵��û��������
        private Quaternion _forceLookAtRotation;
        public Vector3 Velocity => _movementController.LastFrameVelocity;
        private void Start()
        {
            _movementController = GetComponent<CharacterMovementController>();
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

            if (!_movementController.UpdateRotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, _forceLookAtRotation, 720f * Time.deltaTime);    // ƽ����ֵ��ת��Root                
            }
        }
        /// <summary>
        /// һ���趨���ͻ᳢�Ա����ٶȷ���ֱ��direction��Ϊzero�����ı�Stance
        /// </summary>
        /// <param name="direction"></param>
        public void TryKeepMoveDirection(Vector3 direction)
        {
            _keepMoveDirection = (direction == Vector3.zero) ? Vector3.zero : direction.normalized;
        }
        /// <summary>
        /// ��ʼ����Ŀ�꣬��ȡ��run״̬
        /// </summary>
        /// <param name="transform"></param>
        public void TryStartForceLookAt()
        {
            _movementController.UpdateRotation = false;
            if (StanceCategory == EStanceCategory.run) StanceCategory = EStanceCategory.walk;
        }
        public void TrySetLookAtRotation(Quaternion rotation)
        {
            if (_movementController.UpdateRotation) return;  // �������UpdateRotation˵��û������
            _forceLookAtRotation = rotation;
        }
        public void TryStopForceLookAt()
        {
            _movementController.UpdateRotation = true;
        }
        /// <summary>
        /// ���û���ܲ����ͽ����ܲ�״̬������ȡ������״̬������Ѿ������ˣ��Ǿͱ��������·��
        /// </summary>
        public void TryToggleRunState()
        {
            if (StanceCategory == EStanceCategory.run)
            {
                StanceCategory = EStanceCategory.walk;
            }
            else
            {
                StanceCategory = EStanceCategory.run;
                if (IsForcingLookingAt) TryStopForceLookAt();
            }
        }
    }
}
