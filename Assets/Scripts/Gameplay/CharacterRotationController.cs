using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Core;
using System;
namespace MyProject.Gameplay
{
    /// <summary>
    /// ��ɫ�����ܿأ��ṩLockRotation��SetDesiredRotation���೯����Ʒ�����
    /// </summary>
    public class CharacterRotationController : MonoBehaviour
    {
        public Quaternion DesiredRotation { get; private set; }
        private RegistrableBool _isRotationLocked;     // ǿ����������׼������ӦLockRotation
        private bool _isDesiredRotationEnabled;    // ���ȼ���ǿ�������ͣ������ᱻ���Σ���ӦSetRotation
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
                transform.rotation = Quaternion.RotateTowards(transform.rotation, DesiredRotation, _angularSpeed * Time.deltaTime);    // ƽ����ֵ��ת��Root
            }
        }
        /// <summary>
        /// ˲���������򣬲��ٸ���DesiredRotation���³��򣬵��ǿ��Ա�����LockRotation����
        /// </summary>
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
        /// <summary>
        /// �������볯���ܽ��ٶ�����
        /// </summary>
        /// <param name="quaternion"></param>
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
