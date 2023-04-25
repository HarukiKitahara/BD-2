using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyProject.World;
using TMPro;

namespace MyProject.UI
{
    public class WorldMainUI : MonoBehaviour
    {
        [SerializeField]
        private WorldManager _worldManager;
        [SerializeField]
        private TMP_Text _text;
        [SerializeField]
        private GameObject _popup;
        [SerializeField]
        private Button _buttonYes, _buttonNo;
        private void Start()
        {
            WorldManager.OnTileSelected += UpdateTileInfo;
            WorldManager.OnTileInteracted += ShowPopup;
            _popup.SetActive(false);
        }
        private void UpdateTileInfo()
        {
            var coord = _worldManager.World.GetCoordinateByIndex(_worldManager.SelectedTileIndex);
            _text.text = $"TileIndex: {_worldManager.SelectedTileIndex}\nx:{coord.Item1}, y:{coord.Item2}";
        }
        private void ShowPopup()
        {
            _popup.SetActive(true);
            _buttonYes.onClick.AddListener(() => _worldManager.EnterSelectedSite());
            _buttonNo.onClick.AddListener(() => _popup.SetActive(false));
        }
    }
}
