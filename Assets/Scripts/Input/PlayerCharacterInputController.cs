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
            if (_stanceController == null) Debug.LogError("找不到要控制的CharacterStanceController");

            InputActionAssetSingleton.Instance.Gameplay.Move.performed += OnMovePerformed;
            InputActionAssetSingleton.Instance.Gameplay.Move.canceled += OnMoveCanceled;

            InputActionAssetSingleton.Instance.Gameplay.Sprint.performed += o => _stanceController.TryToggleRunState();
            InputActionAssetSingleton.Instance.Gameplay.LookAt.performed += OnLookAtStart;
        }
        #region 角色朝向控制
        private void OnLookAtStart(InputAction.CallbackContext obj)
        {
            if (_stanceController.IsLookingAt)
            {
                _stanceController.StopLookAtRotation();
                Debug.Log("取消朝向锁定，不再持续更新朝向");
            }
            else
            {
                LookAtPointerPosition();
                Debug.Log("开始朝向鼠标位置");
            }
        }
        private void LookAtPointerPosition()
        {
            _stanceController.SetLookAtRotation(Quaternion.LookRotation(GetLookAtDirection()));
            Vector3 GetLookAtDirection()
            {
                var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                var plane = new Plane(Vector3.up, _stanceController.transform.position);  // TODO: 如果地形有起伏，这个方法就不能用了
                if (plane.Raycast(ray, out float enter))
                {
                    return ray.GetPoint(enter) - _stanceController.transform.position;
                }
                else
                {
                    Debug.LogWarning("要么视角太低打到天空，要么解析几何不存在了");
                    return _stanceController.Velocity;
                }
            }
        }
        private void Update()
        {
            if (_stanceController.IsLookingAt) LookAtPointerPosition();
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
            _stanceController.SetMoveDirection(moveDirection);
        }
        private void OnMoveCanceled(InputAction.CallbackContext obj)
        {
            _stanceController.SetMoveDirection(Vector3.zero);
        }
        #endregion
    }
}
