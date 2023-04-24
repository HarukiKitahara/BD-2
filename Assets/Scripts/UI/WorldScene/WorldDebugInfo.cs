using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.World;
using TMPro;
namespace MyProject.UI
{
    public class WorldDebugInfo : MonoBehaviour
    {
        [SerializeField]
        private WorldManager _worldManager;
        [SerializeField]
        private TMP_Text _text;
        private void Start()
        {
            _worldManager.OnTileSelected += UpdateTileInfo;
            _worldManager.OnTileInteracted += ShowInteraction;
        }

        private void UpdateTileInfo()
        {
            var coord = _worldManager.World.GetCoordinateByIndex(_worldManager.SelectedTileIndex);
            _text.text = $"TileIndex: {_worldManager.SelectedTileIndex}\nx:{coord.Item1}, y:{coord.Item2}";
        }
        private void ShowInteraction()
        {
            Debug.Log("TODO£º»¥¶¯Ð§¹û");
        }
    }
}
