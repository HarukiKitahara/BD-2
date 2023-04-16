using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Gameplay;
using UnityEngine.InputSystem;
using System;

namespace MyProject.Input
{
    public class CameraInputController : MonoBehaviour
    {
        [SerializeField]
        private CameraController _cameraController;
        private bool _isRotatingCamera;
        private void Start()
        {
            if (_cameraController == null) Debug.LogError("ÕÒ²»µ½CameraController");

            InputActionAssetSingleton.Instance.Gameplay.RotateCamera.started += o => _isRotatingCamera = true;
            InputActionAssetSingleton.Instance.Gameplay.RotateCamera.canceled += o => _isRotatingCamera = false;

            InputActionAssetSingleton.Instance.Gameplay.MovementZoomCamera.performed += o => _cameraController.MovementZoomCamera(o.ReadValue<Vector2>().y);
        }
        private void Update()
        {
            if (_isRotatingCamera)
            {
                _cameraController.RotateCamera(Mouse.current.delta.ReadValue().x);
            }
        }

    }
}
