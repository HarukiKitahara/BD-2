using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Gameplay
{
    public class CharacterSkillController : MonoBehaviour
    {
        [SerializeField] private Transform _hitInstanceRoot;
        [SerializeField] private GameObject _hitInstance;
        [SerializeField] private CharacterStanceController _stanceController;
        private void Start()
        {
            _stanceController.OnAttack += CastSkill;
        }
        private void CastSkill()
        {
            var go = Instantiate(_hitInstance, _hitInstanceRoot.position, _hitInstanceRoot.rotation);
            go.SetActive(true);
        }
    }
}
