using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace MyProject.Gameplay
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private float _distance30, _distance45, _distance60;
        [SerializeField]
        private float _zoomValue = 0.5f;
        private Vector3 _offset30, _offset45, _offset60;

        private CinemachineOrbitalTransposer _orbitalTransposer;
        private void Start()
        {
            var virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _orbitalTransposer = virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
            //_offset30 = new Vector3(0, _distance30 * Mathf.Sin(30), _distance30 * Mathf.Cos(30));
            _offset30 = Quaternion.Euler(-30, 0, 0) * Vector3.forward * _distance30;
            _offset45 = Quaternion.Euler(-45, 0, 0) * Vector3.forward * _distance45;
            _offset60 = Quaternion.Euler(-60, 0, 0) * Vector3.forward * _distance60;
            Debug.Log($"{_offset30},{_offset45},{_offset60}");
        }
        public void RotateCamera(float value)
        {
            _orbitalTransposer.m_Heading.m_Bias += Mathf.Clamp(value, -720f * Time.deltaTime, 720f * Time.deltaTime);
        }
        public void MovementZoomCamera(float value)
        {
            _zoomValue = Mathf.Clamp01(_zoomValue + Mathf.Sign(value) * Time.deltaTime * 2);
            if (_zoomValue <= 0.5)
            {
                _orbitalTransposer.m_FollowOffset = Vector3.Lerp(_offset30, _offset45, _zoomValue * 2);
            }
            else
            {
                _orbitalTransposer.m_FollowOffset = Vector3.Lerp(_offset45, _offset60, (_zoomValue - 0.5f) * 2);
            }
        }
    }
}
