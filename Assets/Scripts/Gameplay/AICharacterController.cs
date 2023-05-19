using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Gameplay
{
    public class AICharacterController : MonoBehaviour
    {
        private CharacterEntityController _entityController;
        [SerializeField] private Transform _playerTransform;
        void Start()
        {
            _entityController = GetComponent<CharacterEntityController>();
        }

        // Update is called once per frame
        void Update()
        {
            _entityController.CharacterStanceController.SetMoveDirection(_playerTransform.position - transform.position);
        }
    }
}
