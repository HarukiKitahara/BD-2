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
        /// 人形生物50kg，速度基准值步行1 unit/s，所以击退的标准动量是50（可以把步行敌人打定格）
        /// </summary>
        /// <param name="momentum"></param>
        public void ApplyImpluseMomentum(Vector3 momentum)
        {
            CharacterMovementController.ForceSetVelocity(CharacterMovementController.LastFrameVelocity + momentum / AttributeController.Mass);
        }
    }
}
