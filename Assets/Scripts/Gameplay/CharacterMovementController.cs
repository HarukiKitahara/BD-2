using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyProject.Gameplay
{
    public class CharacterMovementController : MonoBehaviour
    {
        private NavMeshAgent _agent;    // ������뿪����׼������ֱ�Ӹ�NavMeshAgent�����������ײ������˭���ˣ�Ҳ���׶�λBug��
        public bool IsMoving => LastFrameVelocity.magnitude != 0;      // TODO����������Ҫ�����Ƽ������˵ȡ���ɫ�����ߣ���ʵ���ƶ��ˡ��������Ŀǰ��ͳһ��NavMeshAgent��
        public Vector3 LastFrameVelocity { get; private set; }      // ����һ֡�����ݸ��ȶ�һ�㣬��ֹ��֡�ڽű�ִ��˳����ҵ����
        public bool UpdateRotation { get => _agent.updateRotation; set => _agent.updateRotation = value; }  // ��Ҫ��_agent�Ի���Ӧ��������getset
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
