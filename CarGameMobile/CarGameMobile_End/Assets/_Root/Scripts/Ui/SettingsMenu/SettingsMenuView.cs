using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.SettingsMenu
{
    public class SettingsMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBack;


        public void Init(UnityAction back) =>
            _buttonBack.onClick.AddListener(back);

        public void OnDestroy() =>
            _buttonBack.onClick.RemoveAllListeners();
    }
}