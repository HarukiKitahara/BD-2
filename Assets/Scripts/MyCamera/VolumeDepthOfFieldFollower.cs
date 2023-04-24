using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MyProject.MyCamera
{
    /// <summary>
    /// 锁定DOF的focusDistance为镜头到target距离
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
            var volumeProfile = volume.profile;     // 学到虚脱：不能用sharedProfile，否则每次运行都会修改assets里的profile。
            if (volumeProfile == null) throw new System.Exception("你没挂Volume，我没法Follow");
            if (!volumeProfile.TryGet(out _depthOfField)) throw new System.Exception("你Volume里没DepthOfField，我没法Follow");
        }
        private void Update()
        {
            if (_depthOfField == null || _followTarget == null) return;
            _depthOfField.focusDistance.value = Vector3.Distance(_followTarget.position, transform.position);
        }
    }
}
