using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Gameplay;
using MyProject.Utils;
namespace MyProject.UI
{
    public class MainUI : MonoBehaviour
    {
        [SerializeField]
        private CharacterEntityController _characterEntity;
        [SerializeField]
        private CommonValueUI _healthUI;
        private void Start()
        {
            _healthUI.Bind(_characterEntity.AttributeController.health);
            HitInstanceModule_DealDamage.OnDamageDealt += ShowDamageNumber;
        }
        private void ShowDamageNumber(Transform transform, float value)
        {
            FloatingTextUI.Show(value.ToString("0"), MyUtils.GetScreenPositionByWorldPosition(transform.position), this.transform, 100f, Color.red);
        }
    }
}
