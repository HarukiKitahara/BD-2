using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyProject.Gameplay
{
    public class CharacterMovementController : MonoBehaviour
    {
        private NavMeshAgent _agent;    // 坚决隔离开，不准其他类直接改NavMeshAgent！这样更容易查参数被谁改了，也容易定位Bug。
        public bool IsMoving => LastFrameVelocity.magnitude != 0;      // TODO：后续可能要考虑推挤、击退等“角色不想走，但实际移动了”的情况，目前先统一走NavMeshAgent。
        public Vector3 LastFrameVelocity { get; private set; }      // 用上一帧的数据更稳定一点，防止单帧内脚本执行顺序混乱的情况
        public bool UpdateRotation { get => _agent.updateRotation; set => _agent.updateRotation = value; }  // 需要与_agent对话都应该像这样getset
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateUpAxis = false;
            LastFrameVelocity = Vector3.zero;
        }
        private void LateUpdate() 
        {
            LastFrameVelocity = _agent.velocity;
        }
        public void SetVelocity(Vector3 velocity)
        {
            _agent.velocity = velocity;
        }
    }
}
