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
        //public bool IsMoving => LastFrameVelocity.magnitude != 0;      // TODO����������Ҫ�����Ƽ������˵ȡ���ɫ�����ߣ���ʵ���ƶ��ˡ��������Ŀǰ��ͳһ��NavMeshAgent��
        public Vector3 LastFrameVelocity { get; private set; }      // ����һ֡�����ݸ��ȶ�һ�㣬��ֹ��֡�ڽű�ִ��˳����ҵ����
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

            _velocity = _velocity.normalized * Mathf.Clamp(_velocity.magnitude - ACCELERATION * Time.deltaTime, 0, MAX_SPEED);    // �ٶ���Ȼ˥��
        }
        ///<summary>���Ӽ��ٶ����ƣ�ǿ�������ٶȣ���ÿ��Update�������ٶȵĻ����������Զ�˥����0</summary>
        public void ForceSetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
        }
        ///<summary>�����ٶ�Ӱ����ٶȸı�ϵͳ</summary>
        public void SetVelocity(Vector3 velocity)
        {
            if (!CanSetVelocity.Value) return;     // ���ܻ���ⲿ�����������ֶ������ٶ�
            _velocity = velocity;   // TODO������𲽼��ٶȣ���Ҫ˲�䵯���𲽡�
        }
    }
}
