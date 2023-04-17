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
        public CharacterAttributeController AttributeController { get; private set; }
        private void Start()
        {
            _hurtEventHandler = new HurtEventHandler(this);
            AttributeController = GetComponent<CharacterAttributeController>();
        }
        private void OnTriggerStay(Collider other)
        {
            _hurtEventHandler.HandelHurtEvent(other);
        }
    }
}
