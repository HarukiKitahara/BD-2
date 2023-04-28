using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Utils;
using MyProject.Core;
namespace MyProject.Gameplay
{
    /// <summary>
    /// ����һ�ּ��裺������ƽ̹�ģ������ڳ���Ϸ�����
    /// </summary>
    public class DayNightController : MonoBehaviour
    {
        //[SerializeField] private Transform _lightGroupRoot;
        //[SerializeField] private Light _sunLight, _moonLight;
        //[SerializeField] [Range(0,1)]private float _dayTime;
        //private void Update()
        //{
        //    transform.rotation = Quaternion.Euler(0, 0, _dayTime * 360f);
        //    var value = MyUtils.Mod(_dayTime, 1);
        //    if (value > 0.25 && value < 0.75)
        //    {
        //        _sunLight.enabled = true;
        //        _moonLight.enabled = false;
        //    }
        //    else
        //    {
        //        _sunLight.enabled = false;
        //        _moonLight.enabled = true;
        //    }
        //}
        [SerializeField] private Light _sunLight;
        [SerializeField] private Light _moonLight;
        [SerializeField] private float _latitude = 100;
        [SerializeField] private float _sunHeight = 100;
        [SerializeField] private float _angle;  // �����뾭�����ɼнǣ�����90��, ��������0��Ӷ�������
        private void Start()
        {
            GameTimeManager.Instance.UnpauseGame();
        }
        public void Update()
        {
            _angle = (GameTimeManager.Instance.GetDayProgress() - 0.25f) * 360f;
            UpdateAngle();

        }
        private void UpdateAngle()
        {
            var angle = MyUtils.Mod(_angle, 360f);
            if (angle < 180f)
            {
                _sunLight.enabled = true;
                _moonLight.enabled = false;
                _sunLight.transform.LookAt(-SimulateSunPosition(angle % 180f - 90f));
                _moonLight.transform.LookAt(-SimulateSunPosition(angle % 180f - 90f, true));
            }
            else
            {
                _sunLight.enabled = false;
                _moonLight.enabled = true;
                _moonLight.transform.LookAt(-SimulateSunPosition(angle % 180f - 90f));
                _sunLight.transform.LookAt(-SimulateSunPosition(angle % 180f - 90f, true));
            }
        }
        private Vector3 SimulateSunPosition(float angle, bool isUnderGround = false)
        {
            angle = -angle;
            var sign = isUnderGround ? -1f : 1f;
            return new Vector3(_latitude * Mathf.Tan(angle * Mathf.Deg2Rad) * sign, _sunHeight * sign, -_latitude);
        }
    }
}
