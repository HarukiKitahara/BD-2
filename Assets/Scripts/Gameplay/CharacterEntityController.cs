using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Gameplay
{
    /// <summary>
    /// 管场景中某个Character全部信息的类，还负责初始化加载
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
