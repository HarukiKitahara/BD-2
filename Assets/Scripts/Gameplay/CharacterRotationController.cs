using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Core;
using System;
namespace MyProject.Gameplay
{
    /// <summary>
    /// 角色朝向总控，提供LockRotation和SetDesiredRotation两类朝向控制方法。
    /// </summary>
    public class CharacterRotationController : MonoBehaviour
    {
        public Quaternion DesiredRotation { get; private set; }
        private RegistrableBool _isRotationLocked;     // 强制锁定朝向不准动，对应LockRotation
        private bool _isDesiredRotationEnabled;    // 优先级比强制锁定低，但不会被屏蔽，对应SetRotation
        [SerializeField]
        private float _angularSpeed = 720f;
        private void Start()
        {
            _isRotationLocked = new(false);
        }
        private void Update()
        {
            if (!_isRotationLocked.Value && _isDesiredRotationEnabled)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, DesiredRotation, _angularSpeed * Time.deltaTime);    // 平滑插值，转动Root
            }
        }
        public Guid LockRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
            return _isRotationLocked.Register();
        }
        public Guid LockRotation()
        {
            return LockRotation(transform.rotation);
        }
        public void UnlockRotation(ref Guid token)
        {
            _isRotationLocked.Deregister(ref token);
        }
        public void SetDesiredRotation(Quaternion quaternion)
        {
            _isDesiredRotationEnabled = true;
            DesiredRotation = quaternion;
        }
        public void SetDesiredRotation(Vector3 direction)
        {
            SetDesiredRotation(Quaternion.FromToRotation(Vector3.forward, direction));
        }
        public void ResetDesiredRotation()
        {
            _isDesiredRotationEnabled = false;
        }
    }
}
