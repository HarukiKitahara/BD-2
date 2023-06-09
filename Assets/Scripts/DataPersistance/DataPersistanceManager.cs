using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MyProject.World;
using MyProject.Core;
namespace MyProject.DataPersistance 
{
    public class DataPersistanceManager : MonoBehaviourSingletonBase<DataPersistanceManager>
    {
        private const string _FILE_NAME = "MySaveData.txt";
        private GameData _gameData;
        private FileDataHandler _fileDataHandler;
        [SerializeField]
        private WorldManager _worldManager;

        protected override void InitOnAwake()
        {
            _fileDataHandler = new FileDataHandler(_FILE_NAME, Application.persistentDataPath);
            WorldManager.OnEnterSite += SaveGame;
        }
        public void SaveGame()
        {
            _gameData = new GameData();
            //gameData.siteData = MainSceneManager.Instance.site.SaveData();
            _gameData.worldManagerDataPersistace = _worldManager.SaveWorld();
            _fileDataHandler.Save(_gameData);
        }
        public void LoadGame()
        {
            TryLoadGame();
        }
        public bool TryLoadGame()
        {
            _gameData = _fileDataHandler.Load();
            if (_gameData == null)
            {
                Debug.LogWarning("没有有效存档，开始新游戏");
                return false;
            }
            _worldManager.LoadWorld(_gameData.worldManagerDataPersistace);
            return true;
        }
    }
}


