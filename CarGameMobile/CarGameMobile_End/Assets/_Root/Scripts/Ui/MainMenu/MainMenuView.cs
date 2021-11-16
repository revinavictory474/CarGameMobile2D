using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonInventory;


        public void Init(UnityAction startGame, UnityAction settings, UnityAction inventory)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settings);
            _buttonInventory.onClick.AddListener(inventory);
        }

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonInventory.onClick.RemoveAllListeners();
        }
    }
}
