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
        private CharacterMovementController _movementController;
        //private IEnumerator _movingCoroutine;
        private void Start()
        {
            _movementController = GetComponent<CharacterMovementController>();
            if (_movementController == null) Debug.LogError("�Ҳ���Ҫ���Ƶ�VectorMovementController");

            InputActionAssetSingleton.Instance.Gameplay.Move.performed += OnMovePerformed;
            InputActionAssetSingleton.Instance.Gameplay.Move.canceled += OnMoveCanceled;

            InputActionAssetSingleton.Instance.Gameplay.Sprint.performed += o => _movementController.TryRun(true);
            InputActionAssetSingleton.Instance.Gameplay.Sprint.canceled += o => _movementController.TryRun(false);

            //InputActionAssetSingleton.Instance.Gameplay.LookAt.performed
        }
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
            _movementController.TryMoveToward(moveDirection);
        }
        private void OnMoveCanceled(InputAction.CallbackContext obj)
        {
            _movementController.TryMoveToward(Vector3.zero);
        }
        
    }
}
