using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string _productId;

        [Header("Buttons")]
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonShed;
        [SerializeField] private Button _buttonReward;
        [SerializeField] private Button _buttonBuy;


        public void Init(UnityAction startGame, UnityAction settings, UnityAction shed,
            UnityAction reward, UnityAction<string> buy)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settings);
            _buttonShed.onClick.AddListener(shed);
            _buttonReward.onClick.AddListener(reward);
            _buttonBuy.onClick.AddListener(() => buy(_productId));
        }

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonShed.onClick.RemoveAllListeners();
            _buttonReward.onClick.RemoveAllListeners();
            _buttonBuy.onClick.RemoveAllListeners();
        }
    }
}
