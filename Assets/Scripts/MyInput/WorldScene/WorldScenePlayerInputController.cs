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
            if (_worldManager == null) Debug.LogError("�Ҳ���Ҫ���Ƶ�CharacterStanceController");

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
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;     // ���пӡ�ѧ�����ѣ�InputSystemû����callback��������ָ���ж��Ƿ�������UI�Ϸ���

            _worldManager.InteractTile();
        }
    }
}
