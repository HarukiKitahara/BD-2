using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MyProject.Utils;
using MyProject.Core;

namespace MyProject.Gameplay
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovementController : MonoBehaviour
    {
        private const float MAX_SPEED = 20f;
        private const float FALLING_SPEED = 10f;
        private const float ACCELERATION = 8f;
        //public bool IsMoving => LastFrameVelocity.magnitude != 0;      // TODO：后续可能要考虑推挤、击退等“角色不想走，但实际移动了”的情况，目前先统一走NavMeshAgent。
        public Vector3 LastFrameVelocity { get; private set; }      // 用上一帧的数据更稳定一点，防止单帧内脚本执行顺序混乱的情况
        public RegistrableBool CanSetVelocity { get; private set; }
        private Vector3 _velocity;
        private CharacterController _characterController;
        private bool IsGrounded { get => _characterController.isGrounded; } //{ get => Physics.Raycast(transform.position, Vector3.down, GROUND_DISTANCE, LayerMask.NameToLayer("Ground")); }
        private void Awake()
        {
            CanSetVelocity = new(true);
            _characterController = GetComponent<CharacterController>();
        }
        private void Update()
        {
            if(!IsGrounded) _characterController.Move(FALLING_SPEED * Time.deltaTime * Vector3.down);
            _velocity.y = 0;
            _characterController.Move(_velocity * Time.deltaTime);

            _velocity = _velocity.normalized * Mathf.Clamp(_velocity.magnitude - ACCELERATION * Time.deltaTime, 0, MAX_SPEED);    // 速度自然衰减
        }
        ///<summary>无视加速度限制，强行设置速度（不每个Update都重设速度的话，会慢慢自动衰减到0</summary>
        public void ForceSetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
        }
        ///<summary>受锁速度影响的速度改变系统</summary>
        public void SetVelocity(Vector3 velocity)
        {
            if (!CanSetVelocity.Value) return;     // 可能会从外部锁死，不让手动设置速度
            _velocity = velocity;   // TODO：添加起步加速度，不要瞬间弹射起步。
        }
    }
}
