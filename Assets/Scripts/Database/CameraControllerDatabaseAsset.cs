using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Database
{
    [CreateAssetMenu(fileName = "CameraControllerDatabaseAsset_DEFAULT", menuName = "MyDatabase/CameraController")]
    public class CameraControllerDatabaseAsset : DatabaseAssetBase
    {
        // 缩放同时改变摄像机角度，越近视角越平。
        // 角度定义为与水平面夹角，0与地面平行，90垂直向下俯视。
        [Header("缩放参数")]
        // 缩放角度
        [Range(0, 90)] public float minAngleForZooming;
        [Range(0, 90)] public float maxAngleForZooming;
        [Range(0, 90)] public float initAngleForZooming;
        // 缩放距离
        public float minDistanceForZooming;         
        public float maxDistanceForZooming;
        // 从(minAngle,minDistance)到(maxAngle, maxDistance)的曲线
        public AnimationCurve angleDistanceCurveForZooming;
        // 每秒转多少度
        public float angularVelocityForZooming;
        [Header("旋转参数")]
        // 单位degree/s
        public float angularVelocityForRotation;

    }
}
