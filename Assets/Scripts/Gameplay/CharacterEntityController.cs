using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Gameplay
{
    /// <summary>
    /// �ܳ�����ĳ��Characterȫ����Ϣ���࣬�������ʼ������
    /// </summary>
    public class CharacterEntityController : MonoBehaviour
    {
        private HurtEventHandler _hurtEventHandler;
        public CharacterMovementController CharacterMovementController { get; private set; }
        public CharacterAttributeController AttributeController { get; private set; }
        //public CharacterStanceController CharacterStanceController { get; private set; }
        private void Start()
        {
            _hurtEventHandler = new HurtEventHandler(this);
            AttributeController = GetComponent<CharacterAttributeController>();
            CharacterMovementController = GetComponent<CharacterMovementController>();
            //CharacterStanceController = GetComponent<CharacterStanceController>();
        }
        private void OnTriggerStay(Collider other)
        {
            _hurtEventHandler.TryHandleHurtEvent(other);
        }
        /// <summary>
        /// ��������50kg���ٶȻ�׼ֵ����1 unit/s�����Ի��˵ı�׼������50�����԰Ѳ��е��˴򶨸�
        /// </summary>
        /// <param name="momentum"></param>
        public void ApplyImpluseMomentum(Vector3 momentum)
        {
            CharacterMovementController.ForceSetVelocity(CharacterMovementController.LastFrameVelocity + momentum / AttributeController.Mass);
        }
    }
}
