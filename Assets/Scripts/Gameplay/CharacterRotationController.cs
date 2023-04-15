using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Gameplay
{
    public class CharacterRotationController : MonoBehaviour
    {
        private NavMeshAgentHelper _agentHelper;

        private bool _isForcingLookAt;
        private Quaternion _desiredRotation;
        private void Start()
        {
            _agentHelper = GetComponent<NavMeshAgentHelper>();
            if (_agentHelper == null) Debug.LogError("寻路Helper找不到了");
        }
        private void Update()
        {
            if (_isForcingLookAt)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, _desiredRotation, 0.8f);
            }
        }
        // NavMesh自带这个功能，就不重复造轮子了
        //private void UpdateDesiredRotationByMovementDirection()
        //{
        //    _desiredRotation = Quaternion.LookRotation(_forceLookAtDirection);
        //}

        public void TryForceLookAtDirection(Vector3 direction)
        {
            _isForcingLookAt = true;
            _agentHelper.UpdateRotation = false;

            _desiredRotation = Quaternion.LookRotation(direction);
        }
        public void ReleaseLookAtPosition()
        {
            _isForcingLookAt = false;
            _agentHelper.UpdateRotation = true;
        }
    }
}
