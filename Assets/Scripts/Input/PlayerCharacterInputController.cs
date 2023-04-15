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
            if (_movementController == null) Debug.LogError("找不到要控制的VectorMovementController");

            InputActionAssetSingleton.Instance.Gameplay.Move.performed += OnMovePerformed;
            InputActionAssetSingleton.Instance.Gameplay.Move.canceled += OnMoveCanceled;

            InputActionAssetSingleton.Instance.Gameplay.Sprint.performed += o => _movementController.TryRun(true);
            InputActionAssetSingleton.Instance.Gameplay.Sprint.canceled += o => _movementController.TryRun(false);

            //InputActionAssetSingleton.Instance.Gameplay.LookAt.performed
        }
        /// <summary>
        /// 控制移动。需要根据摄像机角度修正朝向（玩家视角下的前不一定是WorldSpace下的Vector3.forward）
        /// </summary>
        /// <param name="obj"></param>
        private void OnMovePerformed(InputAction.CallbackContext obj)
        {
            // 获取键盘输入方向、摄像机角度，计算WorldSpace下的移动方向
            // 坑点：V2转V3
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
