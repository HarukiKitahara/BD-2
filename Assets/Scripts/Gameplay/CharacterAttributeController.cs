using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Core;
namespace MyProject.Gameplay
{
    public class CharacterAttributeController
    {
        public readonly CommonValue health;
        public readonly CommonValue hunger;
        public CharacterAttributeController()
        {
            health = new CommonValue(100, 0, 100);
            hunger = new CommonValue(100, 0, 100);
        }
    }
}
