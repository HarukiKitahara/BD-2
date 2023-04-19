using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MyProject.Gameplay;
using System;
using UnityEngine.Events;

namespace MyProject.Input
{
    public class PlayerCharacterInputController : MonoBehaviour
    {
        [SerializeField]
        private CharacterStanceController _stanceController;
        private void Start()
        {
            //_stanceController = GetComponent<CharacterStanceController>();
            if (_stanceController == null) Debug.LogError("�Ҳ���Ҫ���Ƶ�CharacterStanceController");

            InputActionAssetSingleton.Instance.Gameplay.Move.performed += OnMovePerformed;
            InputActionAssetSingleton.Instance.Gameplay.Move.canceled += OnMoveCanceled;

            InputActionAssetSingleton.Instance.Gameplay.Sprint.performed += o => _stanceController.TryToggleRunState();
            InputActionAssetSingleton.Instance.Gameplay.LookAt.performed += OnLookAtStart;
        }
        #region ��ɫ�������
        private void OnLookAtStart(InputAction.CallbackContext obj)
        {
            if (_stanceController.IsLookingAt)
            {
                _stanceController.StopLookAtRotation();
                Debug.Log("ȡ���������������ٳ������³���");
            }
            else
            {
                LookAtPointerPosition();
                Debug.Log("��ʼ�������λ��");
            }
        }
        private void LookAtPointerPosition()
        {
            _stanceController.SetLookAtRotation(Quaternion.LookRotation(GetLookAtDirection()));
            Vector3 GetLookAtDirection()
            {
                var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                var plane = new Plane(Vector3.up, _stanceController.transform.position);  // TODO: ����������������������Ͳ�������
                if (plane.Raycast(ray, out float enter))
                {
                    return ray.GetPoint(enter) - _stanceController.transform.position;
                }
                else
                {
                    Debug.LogWarning("Ҫô�ӽ�̫�ʹ���գ�Ҫô�������β�������");
                    return _stanceController.Velocity;
                }
            }
        }
        private void Update()
        {
            if (_stanceController.IsLookingAt) LookAtPointerPosition();
        }
        #endregion
        #region ��ɫ�ƶ�����
        /// <summary>
        /// �����ƶ�����Ҫ����������Ƕ�������������ӽ��µ�ǰ��һ����WorldSpace�µ�Vector3.forward��
        /// </summary>
        /// <param name="obj"></param>
        private void OnMovePerformed(InputAction.CallbackContext obj)
        {
            // ��ȡ�������뷽��������Ƕȣ�����WorldSpace�µ��ƶ�����
            // �ӵ㣺V2תV3
            var inputDirection = obj.ReadValue<Vector2>();     
            var cameraRotationY = Camera.main.transform.rotation.eulerAngles.y;     
            var moveDirection = Quaternion.Euler(0, cameraRotationY, 0) * new Vector3(inputDirection.x, 0, inputDirection.y);
            _stanceController.SetMoveDirection(moveDirection);
        }
        private void OnMoveCanceled(InputAction.CallbackContext obj)
        {
            _stanceController.SetMoveDirection(Vector3.zero);
        }
        #endregion
    }
}
