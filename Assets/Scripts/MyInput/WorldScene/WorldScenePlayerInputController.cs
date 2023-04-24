using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.World;
using MyProject.Utils;
namespace MyProject.MyInput
{
    public class WorldScenePlayerInputController : MonoBehaviour
    {
        private const float RAYCAST_MAX_DISTANCE = 100f;
        [SerializeField]
        private WorldManager _worldManager;
        [SerializeField]
        private Collider _worldCollider;
        private void Start()
        {
            //_stanceController = GetComponent<CharacterStanceController>();
            if (_worldManager == null) Debug.LogError("找不到要控制的CharacterStanceController");

            // InputActionAssetSingleton.Instance.Gameplay.Click.performed += o => TryInteract();
        }
        private void Update()
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;
            var ray = MyUtils.GetMouseRayAgainstMainCamera();
            if (_worldCollider.Raycast(ray, out var hit, RAYCAST_MAX_DISTANCE))
            {
                var tileIndex = _worldManager.World.GetIndexByPosition(hit.point);
                _worldManager.SelectTile(tileIndex);
                if (Input.GetMouseButtonDown(0)) TryInteract();
            }
        }
        private void TryInteract()
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;     // 【有坑】学到虚脱：InputSystem没法在callback中用这条指令判断是否悬浮在UI上方。

            _worldManager.InteractTile();
        }
    }
}
