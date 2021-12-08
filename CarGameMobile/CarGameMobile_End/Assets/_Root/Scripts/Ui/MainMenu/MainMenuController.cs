using Profile;
using Services;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui.MainMenu
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath =
            new ResourcePath("Prefabs/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, Settings, Rewards, Shed, Reward, Buy, Exit);
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;

        private void Settings() =>
            _profilePlayer.CurrentState.Value = GameState.Settings;

        private void Shed() =>
            _profilePlayer.CurrentState.Value = GameState.Shed;

        private void Rewards() =>
            _profilePlayer.CurrentState.Value = GameState.Rewards;


        private void Reward()
        {
            SubscribeRewardedPlayer();
            ServiceLocator.AdsService.RewardedPlayer.Play();
        }

        private void Buy(string productId)
        {
            SubscribeIAP();
            ServiceLocator.IAPService.Buy(productId);
        }

        private void SubscribeIAP()
        {
            ServiceLocator.IAPService.PurchaseSucceed.AddListener(OnIAPSucceed);
            ServiceLocator.IAPService.PurchaseFailed.AddListener(OnIAPFailed);
        }

        private void UnsubscribeIAP()
        {
            ServiceLocator.IAPService.PurchaseSucceed.RemoveListener(OnIAPSucceed);
            ServiceLocator.IAPService.PurchaseFailed.RemoveListener(OnIAPFailed);
        }

        private void OnIAPSucceed()
        {
            UnsubscribeIAP();
        }

        private void OnIAPFailed()
        {
            UnsubscribeIAP();
        }


        private void SubscribeRewardedPlayer()
        {
            ServiceLocator.AdsService.RewardedPlayer.Finished += OnAdsFinished;
            ServiceLocator.AdsService.RewardedPlayer.Failed += OnAdsCancelled;
            ServiceLocator.AdsService.RewardedPlayer.Skipped += OnAdsCancelled;
        }

        private void UnsubscribeRewardedPlayer()
        {
            ServiceLocator.AdsService.RewardedPlayer.Finished -= OnAdsFinished;
            ServiceLocator.AdsService.RewardedPlayer.Failed -= OnAdsCancelled;
            ServiceLocator.AdsService.RewardedPlayer.Skipped -= OnAdsCancelled;
        }

        private void OnAdsFinished()
        {
            UnsubscribeRewardedPlayer();
        }

        private void OnAdsCancelled()
        {
            UnsubscribeRewardedPlayer();
        }

        private void Exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
