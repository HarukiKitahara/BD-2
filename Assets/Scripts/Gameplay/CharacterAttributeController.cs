using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Core;
namespace MyProject.Gameplay
{
    public class CharacterAttributeController : MonoBehaviour
    {
        public float Mass { get ; private set; }
        public float Acceleration { get; private set; }
        public CommonValue Health { get; private set; }
        private void Awake()
        {
            Health = new CommonValue(100, 0, 100);
            Health.ValueMinEvent += Die;

            Mass = 50f;
        }
        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
