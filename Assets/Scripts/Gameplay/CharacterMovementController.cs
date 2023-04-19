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
        private NavMeshAgent _agent;    // ������뿪����׼������ֱ�Ӹ�NavMeshAgent�����������ײ������˭���ˣ�Ҳ���׶�λBug��
        public bool IsMoving => LastFrameVelocity.magnitude != 0;      // TODO����������Ҫ�����Ƽ������˵ȡ���ɫ�����ߣ���ʵ���ƶ��ˡ��������Ŀǰ��ͳһ��NavMeshAgent��
        public Vector3 LastFrameVelocity { get; private set; }      // ����һ֡�����ݸ��ȶ�һ�㣬��ֹ��֡�ڽű�ִ��˳����ҵ����
        public RegistrableBool CanSetVelocity { get; private set; }
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateUpAxis = false;
            _agent.updateRotation = false;
            LastFrameVelocity = Vector3.zero;
            CanSetVelocity = new(true);
        }
        private void LateUpdate() 
        {
            LastFrameVelocity = _agent.velocity;
        }
        ///<summary>���Ӽ��ٶ����ƣ�ǿ�������ٶȣ���ÿ��Update�������ٶȵĻ����������Զ�˥����0</summary>
        public void ForceSetVelocity(Vector3 velocity)
        {
            _agent.velocity = velocity;
        }
        ///<summary>����NavMeshAgent�Դ���acceleration������ƽ���ı��ٶȣ���Force�����������ǣ������ٶ�ҲҪ��ѭ���ٶȻ�����������˲��ı��ٶ�</summary>
        public void SetVelocity(Vector3 velocity)
        {
            if (!CanSetVelocity.Value) return;     // ���ܻ���ⲿ�����������ֶ������ٶ�
            var maxDeltaVelocity = _agent.acceleration * Time.deltaTime;
            _agent.velocity = velocity.Clamp(_agent.velocity.magnitude - maxDeltaVelocity, _agent.velocity.magnitude + maxDeltaVelocity);
        }
    }
}
