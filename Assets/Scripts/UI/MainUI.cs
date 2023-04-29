using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Gameplay;

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
        }
    }
}
