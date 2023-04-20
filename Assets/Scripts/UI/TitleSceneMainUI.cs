using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyProject.Core;

namespace MyProject.UI
{
    public class TitleSceneMainUI : MonoBehaviour
    {
        private Button _newGame;
        private void Start()
        {
            _newGame = GetComponentInChildren<Button>();
            _newGame.onClick.AddListener(GameManager.Instance.NewGame);
        }
    }
}
