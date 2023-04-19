using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using MyProject.Gameplay;

namespace MyProject.UI
{
    public class DebugUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _debugText;
        [SerializeField]
        private CharacterStanceController _stanceController;

        //private void Update()
        //{
        //    _debugText.text = $"Stance: {_stanceController.StanceCategory}\nIsLookingAtTarget: {_stanceController.IsForcingLookingAt}";
        //}
    }
}
