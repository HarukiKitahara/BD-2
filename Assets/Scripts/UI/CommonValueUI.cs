using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Core;
namespace MyProject.UI
{
    public class CommonValueUI : MonoBehaviour
    {
        [SerializeField]
        private Transform _fillTransform;
        private CommonValue _commonValue;
        //private void Start()
        //{
        //    _fillTransform = transform.Find("Fill");
        //    Debug.LogError("CommonValueUI下找不到Fill的GO");
        //}
        public void Bind(CommonValue commonValue)
        {
            _commonValue = commonValue;
            _commonValue.ValueChangeEvent += UpdateDisplay;
            UpdateDisplay();
        }
        private void UpdateDisplay()
        {
            _fillTransform.localScale = new Vector3(_commonValue.Ratio, 1, 1);
        }
    }
}

