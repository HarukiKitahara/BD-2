using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using MyProject.Database;
using MyProject.Utils;
namespace MyProject.MyCamera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private CameraControllerDatabaseAsset _data;

        private CinemachineOrbitalTransposer _orbitalTransposer;
        private float _angleForZooming;
        private void Start()
        {
            var virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _orbitalTransposer = virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
            _angleForZooming = _data.initAngleForZooming;
            UpdateZooming();
        }
        /// <summary> 水平环绕 </summary>
        public void RotateCamera(float value)
        {
            _orbitalTransposer.m_Heading.m_Bias += Mathf.Clamp(value, -_data.angularVelocityForRotation * Time.deltaTime, _data.angularVelocityForRotation * Time.deltaTime);
        }
        /// <summary> 推近、拉远相机，同时改变俯视角度 </summary>
        public void MovementZoomCamera(float value)
        {
            value = Mathf.Sign(value);
            _angleForZooming = Mathf.Clamp(_angleForZooming + value * _data.angularVelocityForZooming * Time.deltaTime, _data.minAngleForZooming, _data.maxAngleForZooming);
            UpdateZooming();
        }
        /// <summary> 根据当前_angleForZooming，更新缩放效果 </summary>
        private void UpdateZooming()
        {
            var normalizedInput = Mathf.InverseLerp(_data.minAngleForZooming, _data.maxAngleForZooming, _angleForZooming);
            var distance = Mathf.Lerp(_data.minDistanceForZooming, _data.maxDistanceForZooming, _data.angleDistanceCurveForZooming.EvaluateNormalized(normalizedInput));
            _orbitalTransposer.m_FollowOffset = GetFollowOffset(_angleForZooming, distance);
            //Debug.Log($"Angle:{_angleForZooming}, NormalizedInput:{normalizedInput}, Distance:{distance}, Offset:{_orbitalTransposer.m_FollowOffset}");
        }
        /// <summary> 输入水平方向夹角和距离，输出摄像机到跟随目标的距离 </summary>
        private Vector3 GetFollowOffset(float angle, float distance)
        {
            return new Vector3(0, distance * Mathf.Sin(angle * Mathf.Deg2Rad), distance * Mathf.Cos(angle * Mathf.Deg2Rad));
        }
        //private float GetAngleForZoomingByFollowOffset()
        //{
        //    // offset.y / offset.magnitude 就是水平夹角的sin
        //    return Mathf.Asin(_orbitalTransposer.m_FollowOffset.y / _orbitalTransposer.m_FollowOffset.magnitude) * Mathf.Rad2Deg; 
        //}
    }
}
