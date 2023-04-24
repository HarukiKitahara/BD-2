using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MyProject.MyCamera
{
    /// <summary>
    /// ����DOF��focusDistanceΪ��ͷ��target����
    /// </summary>
    [RequireComponent(typeof(VolumeComponent))]
    public class VolumeDepthOfFieldFollower : MonoBehaviour
    {
        [SerializeField]
        private Transform _followTarget;
        private DepthOfField _depthOfField;
        private void Start()
        {
            var volume = GetComponent<Volume>();
            var volumeProfile = volume.profile;     // ѧ�����ѣ�������sharedProfile������ÿ�����ж����޸�assets���profile��
            if (volumeProfile == null) throw new System.Exception("��û��Volume����û��Follow");
            if (!volumeProfile.TryGet(out _depthOfField)) throw new System.Exception("��Volume��ûDepthOfField����û��Follow");
        }
        private void Update()
        {
            if (_depthOfField == null || _followTarget == null) return;
            _depthOfField.focusDistance.value = Vector3.Distance(_followTarget.position, transform.position);
        }
    }
}
