using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MyProject.Utils;
using MyProject.Core;

namespace MyProject.Gameplay
{
    public class CharacterMovementController : MonoBehaviour
    {
        private NavMeshAgent _agent;    // 坚决隔离开，不准其他类直接改NavMeshAgent！这样更容易查参数被谁改了，也容易定位Bug。
        public bool IsMoving => LastFrameVelocity.magnitude != 0;      // TODO：后续可能要考虑推挤、击退等“角色不想走，但实际移动了”的情况，目前先统一走NavMeshAgent。
        public Vector3 LastFrameVelocity { get; private set; }      // 用上一帧的数据更稳定一点，防止单帧内脚本执行顺序混乱的情况
        public bool IsUpdatingRotation { get => _agent.updateRotation; }  // 需要与_agent对话都应该像这样getset
        public RegistrableBool EnableUpdateRotation { get; private set; }
        /// <summary>锁住不让改速度，不影响自然衰减和destination导航，也不影响ForceSetVelocity</summary>
        public RegistrableBool CanSetVelocity { get; private set; }
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateUpAxis = false;
            LastFrameVelocity = Vector3.zero;
            CanSetVelocity = new();
            EnableUpdateRotation = new();
            EnableUpdateRotation.OnValueChanged += () =>
            {
                _agent.updateRotation = EnableUpdateRotation.Value;
                Debug.Log(_agent.updateRotation);
            };
        }
        private void LateUpdate() 
        {
            LastFrameVelocity = _agent.velocity;
        }
        ///<summary>无视加速度限制，强行设置速度（不每个Update都重设速度的话，会慢慢自动衰减到0</summary>
        public void ForceSetVelocity(Vector3 velocity)
        {
            _agent.velocity = velocity;
        }
        ///<summary>利用NavMeshAgent自带的acceleration参数，平滑改变速度（与Force方法的区别是，设置速度也要遵循加速度基本法，不能瞬间改变速度</summary>
        public void SetVelocity(Vector3 velocity)
        {
            if (!CanSetVelocity.Value) return;     // 可能会从外部锁死，不让手动设置速度
            var maxDeltaVelocity = _agent.acceleration * Time.deltaTime;
            _agent.velocity = velocity.Clamp(_agent.velocity.magnitude - maxDeltaVelocity, _agent.velocity.magnitude + maxDeltaVelocity);
        }
    }
}
