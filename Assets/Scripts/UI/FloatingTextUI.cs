using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
namespace MyProject.UI
{
    public static class FloatingTextUI
    {
        private static GameObject _prefab;
        public static void Show(string text, Vector3 position, Transform parent, float offsetY = 0f, Color color = default)
        {
            if (_prefab == null) _prefab = Resources.Load("Prefabs/UI/FloatingTextUI") as GameObject;
            if (_prefab == null)
            {
                Debug.LogWarning("找不到FloatingText的Prefab，请检查路径是否正确");
                return;
            }
            var go = Object.Instantiate(_prefab, parent);
            position.y += offsetY;
            go.transform.position = position;
            go.SetActive(true);
            var tmp_text = go.GetComponent<TMP_Text>();
            tmp_text.text = text;
            tmp_text.DOFade(0f, 2f);
            go.transform.DOMoveY(parent.position.y + 50f, 2f).OnComplete(() => Object.Destroy(go));
            if (color != default) tmp_text.color = color;
        }
    }
}
