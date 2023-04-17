using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Gameplay;

namespace MyProject.UI
{
    public class MainUI : MonoBehaviour
    {
        [SerializeField]
        private CharacterAttributeController _attributeController;
        [SerializeField]
        private CommonValueUI _healthUI;
        private void Start()
        {
            _healthUI.Bind(_attributeController.Health);
        }
    }
}
