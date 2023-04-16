using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Gameplay
{
    public class AICharacterController : MonoBehaviour
    {
        private CharacterStanceController _stanceController;
        void Start()
        {
            _stanceController = GetComponent<CharacterStanceController>();
        }

        // Update is called once per frame
        void Update()
        {
            _stanceController.TryKeepMoveDirection(Vector3.forward);
        }
    }
}
