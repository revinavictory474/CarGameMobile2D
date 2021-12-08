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
        [SerializeField] private Button _buttonRewards;
        [SerializeField] private Button _buttonShed;
        [SerializeField] private Button _buttonReward;
        [SerializeField] private Button _buttonBuy;
        [SerializeField] private Button _buttonExit;


        public void Init(UnityAction startGame, UnityAction settings, UnityAction rewards, 
            UnityAction shed, UnityAction reward, UnityAction<string> buy, UnityAction exit)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settings);
            _buttonRewards.onClick.AddListener(rewards);
            _buttonShed.onClick.AddListener(shed);
            _buttonReward.onClick.AddListener(reward);
            _buttonBuy.onClick.AddListener(() => buy(_productId));
            _buttonExit.onClick.AddListener(exit);
        }

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonRewards.onClick.RemoveAllListeners();
            _buttonShed.onClick.RemoveAllListeners();
            _buttonReward.onClick.RemoveAllListeners();
            _buttonBuy.onClick.RemoveAllListeners();
            _buttonExit.onClick.RemoveAllListeners();
        }
    }
}
