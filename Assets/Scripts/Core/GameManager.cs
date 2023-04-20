using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace MyProject.Core
{
    public class GameManager : MonoBehaviourSingletonBase<GameManager>
    {
        protected sealed override void InitOnAwake()
        {
            base.InitOnAwake();
            DontDestroyOnLoad(this);
        }
        public void NewGame()
        {
            SceneManager.LoadSceneAsync("MyTestScene");
        }
    }
}
