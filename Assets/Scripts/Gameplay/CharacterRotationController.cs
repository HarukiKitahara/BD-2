using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Core;
using System;
using MyProject.Utils;
namespace MyProject.Gameplay
{
    /// <summary>
    /// ��ɫ�����ܿأ��ṩLockRotation��SetDesiredRotation���೯����Ʒ�����
    /// </summary>
    public class CharacterRotationController : MonoBehaviour
    {
        //public Quaternion DesiredRotation { get; private set; }
        private RegistrableBool _isRotationLocked;     // ǿ����������׼������ӦLockRotation
        [SerializeField]
        private float _angularSpeed = 10f;
        private float _desiredAngle = 0f;

        private void Start()
        {
            _isRotationLocked = new(false);
        }
        private void Update()
        {
            if (!_isRotationLocked.Value)
            {
                // ƽ����ֵ��ת��Root
                transform.rotation = Quaternion.Euler(0, Mathf.LerpAngle(transform.rotation.eulerAngles.y, _desiredAngle, _angularSpeed * Time.deltaTime), 0);
            }
        }
        /// <summary>
        /// ˲���������򣬲��ٸ���DesiredRotation���³��򣬵��ǿ��Ա�����LockRotation����
        /// </summary>
        public Guid LockRotation(float angle)
        {
            transform.rotation = GetQuaternionByAngle(angle);
            return _isRotationLocked.Register();
        }
        public Guid LockRotation()
        {
            return LockRotation(transform.rotation.eulerAngles.y);
        }
        public void UnlockRotation(ref Guid token)
        {
            _isRotationLocked.Deregister(ref token);
        }
        public void SetDesiredRotation(float angle)
        {
            _desiredAngle = angle;
        }
        public void SetDesiredRotation(Vector3 direction)
        {
            direction.y = 0;
            SetDesiredRotation(Vector3.SignedAngle(Vector3.forward, direction, Vector3.up));
        }
        public void SetDesiredRotation(Quaternion quaternion)
        {
            SetDesiredRotation(quaternion.eulerAngles.y);
        }
        private Quaternion GetQuaternionByAngle(float angle)
        {
            return Quaternion.Euler(0, angle, 0);
        }
    }
}
