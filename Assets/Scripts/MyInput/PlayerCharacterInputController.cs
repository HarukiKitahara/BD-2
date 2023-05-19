using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MyProject.Gameplay;
using MyProject.Utils;

namespace MyProject.MyInput
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

            InputActionAssetSingleton.Instance.Gameplay.StanceChange.performed += o => _stanceController.TryToggleStance();
        }
        private void Update()
        {
            TryLookAtPointerPosition(); // 不在这里判断能不能朝向目标，交给StanceController判断，这里只负责输入
            if (Input.GetMouseButton(0) && !MyUtils.IsPointerOverGameObject())
            {
                _stanceController.TryAttack();
            }
        }
        /// <summary>
        /// 角色朝向控制
        /// </summary>
        private void TryLookAtPointerPosition()
        {
            _stanceController.TrySetLookAtRotation(Quaternion.LookRotation(GetLookAtDirection()));
            Vector3 GetLookAtDirection()
            {
                var ray = MyUtils.GetMouseRayAgainstMainCamera();
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
