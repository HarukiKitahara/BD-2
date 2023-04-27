using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Gameplay
{
    public class DayNightController : MonoBehaviour
    {
        [SerializeField] private Light _sunLight;
        [SerializeField] private float _latitude = 100;
        [SerializeField] private float _sunHeight = 100;
        [SerializeField] private float _angle;
        private void Update()
        {
            _sunLight.transform.LookAt(-SimulateSunPosition());
        }
        private Vector3 SimulateSunPosition()
        {
            return new Vector3(_latitude * Mathf.Tan(_angle * Mathf.Deg2Rad), _sunHeight, -_latitude);
        }
    }
}
