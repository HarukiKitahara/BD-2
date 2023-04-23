using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Database
{
    [CreateAssetMenu(fileName = "CameraControllerDatabaseAsset_DEFAULT", menuName = "MyDatabase/CameraController")]
    public class CameraControllerDatabaseAsset : DatabaseAssetBase
    {
        // ����ͬʱ�ı�������Ƕȣ�Խ���ӽ�Խƽ��
        // �Ƕȶ���Ϊ��ˮƽ��нǣ�0�����ƽ�У�90��ֱ���¸��ӡ�
        [Header("���Ų���")]
        // ���ŽǶ�
        [Range(0, 90)] public float minAngleForZooming;
        [Range(0, 90)] public float maxAngleForZooming;
        [Range(0, 90)] public float initAngleForZooming;
        // ���ž���
        public float minDistanceForZooming;         
        public float maxDistanceForZooming;
        // ��(minAngle,minDistance)��(maxAngle, maxDistance)������
        public AnimationCurve angleDistanceCurveForZooming;
        // ÿ��ת���ٶ�
        public float angularVelocityForZooming;
        [Header("��ת����")]
        // ��λdegree/s
        public float angularVelocityForRotation;

    }
}
