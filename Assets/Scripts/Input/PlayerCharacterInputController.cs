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
        private CharacterRotationController _rotationController;
        private Coroutine _lookAtCoroutine;
        private void Start()
        {
            _movementController = GetComponent<CharacterMovementController>();
            if (_movementController == null) Debug.LogError("找不到要控制的CharacterMovementController");

            _rotationController = GetComponent<CharacterRotationController>();
            if (_rotationController == null) Debug.LogError("找不到要控制的CharacterRotationController");

            InputActionAssetSingleton.Instance.Gameplay.Move.performed += OnMovePerformed;
            InputActionAssetSingleton.Instance.Gameplay.Move.canceled += OnMoveCanceled;

            InputActionAssetSingleton.Instance.Gameplay.Sprint.performed += o => _movementController.TryRun(true);
            InputActionAssetSingleton.Instance.Gameplay.Sprint.canceled += o => _movementController.TryRun(false);

            InputActionAssetSingleton.Instance.Gameplay.LookAt.performed += OnLookAtPerformed;
            InputActionAssetSingleton.Instance.Gameplay.LookAt.canceled += OnLookAtCanceled;
        }
        #region 角色朝向控制
        private void OnLookAtPerformed(InputAction.CallbackContext obj)
        {
            _lookAtCoroutine = StartCoroutine(LookAtCoroutine());
        }
        private void OnLookAtCanceled(InputAction.CallbackContext obj)
        {
            _rotationController.ReleaseLookAtPosition();
            StopCoroutine(_lookAtCoroutine);
        }
        IEnumerator LookAtCoroutine()
        {
            while (true)
            {
                _rotationController.TryForceLookAtDirection(GetLookAtDirection());
                yield return 0;
            }

            Vector3 GetLookAtDirection()
            {
                var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                var plane = new Plane(Vector3.up, _rotationController.transform.position);  // TODO: 如果地形有起伏，这个方法就不能用了
                if (plane.Raycast(ray, out float enter))
                {
                    return ray.GetPoint(enter) - _rotationController.transform.position;
                }
                else { throw new Exception("物理学不存在了"); }
            }
        }
        #endregion
        #region 角色移动控制
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
        #endregion
    }
}
