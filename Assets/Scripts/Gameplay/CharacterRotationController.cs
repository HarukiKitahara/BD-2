using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Gameplay
{
    public class CharacterRotationController : MonoBehaviour
    {
        //private Transform _rotationRootTransform;
        private NavMeshAgentHelper _agentHelper;
        private void Start()
        {
            //_rotationRootTransform = transform.Find("Armature");
            //if (_rotationRootTransform == null) Debug.LogError("旋转根节点找不到了");
            _agentHelper = GetComponent<NavMeshAgentHelper>();
            if (_agentHelper == null) Debug.LogError("寻路Helper找不到了");
        }
        private void Update()
        {
            Debug.Log(_agentHelper.LastFrameDirection);
            FollowMovementDirection();
        }
        private void FollowMovementDirection()
        {
            transform.LookAt(transform.position + _agentHelper.LastFrameDirection);
        }

        public void TryForceLookAtPosition(Vector3 position)
        {
            
        }
        public void ReleaseLookAtPosition()
        {

        }
    }
}
