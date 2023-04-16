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
        private Vector3 _keepMoveDirection;     // 保证要么是zero，要么被normalized
        public EStanceCategory StanceCategory { get; private set; }
        public bool IsForcingLookingAt => !_movementController.UpdateRotation;  // NavMesh在转，就说明没锁定朝向
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
                transform.rotation = Quaternion.RotateTowards(transform.rotation, _forceLookAtRotation, 720f * Time.deltaTime);    // 平滑插值，转动Root                
            }
        }
        /// <summary>
        /// 一旦设定，就会尝试保持速度方向，直到direction设为zero，不改变Stance
        /// </summary>
        /// <param name="direction"></param>
        public void TryKeepMoveDirection(Vector3 direction)
        {
            _keepMoveDirection = (direction == Vector3.zero) ? Vector3.zero : direction.normalized;
        }
        /// <summary>
        /// 开始锁定目标，会取消run状态
        /// </summary>
        /// <param name="transform"></param>
        public void TryStartForceLookAt()
        {
            _movementController.UpdateRotation = false;
            if (StanceCategory == EStanceCategory.run) StanceCategory = EStanceCategory.walk;
        }
        public void TrySetLookAtRotation(Quaternion rotation)
        {
            if (_movementController.UpdateRotation) return;  // 如果还在UpdateRotation说明没有在锁
            _forceLookAtRotation = rotation;
        }
        public void TryStopForceLookAt()
        {
            _movementController.UpdateRotation = true;
        }
        /// <summary>
        /// 如果没在跑步，就进入跑步状态，并且取消锁定状态；如果已经在跑了，那就变成正常走路。
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
